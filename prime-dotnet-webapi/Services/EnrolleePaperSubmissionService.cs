using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using DelegateDecompiler.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

using Prime.Engines;
using Prime.HttpClients;
using Prime.HttpClients.DocumentManagerApiDefinitions;
using Prime.Models;
using Prime.ViewModels.PaperEnrollees;

namespace Prime.Services
{
    public class EnrolleePaperSubmissionService : BaseService, IEnrolleePaperSubmissionService
    {
        private const string PaperGpidPrefix = "NOBCSC";

        private readonly IBusinessEventService _businessEventService;
        private readonly IDocumentManagerClient _documentClient;
        private readonly IEnrolleeAgreementService _enrolleeAgreementService;
        private readonly IEnrolleeSubmissionService _enrolleeSubmissionService;
        private readonly IMapper _mapper;

        public EnrolleePaperSubmissionService(
            ApiDbContext context,
            ILogger<EnrolleePaperSubmissionService> logger,
            IBusinessEventService businessEventService,
            IDocumentManagerClient documentClient,
            IEnrolleeAgreementService enrolleeAgreementService,
            IEnrolleeSubmissionService enrolleeSubmissionService,
            IMapper mapper)
            : base(context, logger)
        {
            _businessEventService = businessEventService;
            _documentClient = documentClient;
            _enrolleeAgreementService = enrolleeAgreementService;
            _enrolleeSubmissionService = enrolleeSubmissionService;
            _mapper = mapper;
        }

        public async Task<bool> PaperSubmissionIsEditableAsync(int enrolleeId)
        {
            var dto = await _context.Enrollees
                .AsNoTracking()
                .Where(e => e.Id == enrolleeId)
                .Select(e => new
                {
                    e.GPID,
                    CurrentStatusCode = e.CurrentStatus.StatusCode
                })
                .DecompileAsync()
                .SingleOrDefaultAsync();

            if (dto == null
                || dto.GPID == null
                || dto.CurrentStatusCode != (int)StatusType.UnderReview)
            {
                return false;
            }

            return dto.GPID.StartsWith(PaperGpidPrefix);
        }

        public async Task<Enrollee> CreateEnrolleeAsync(PaperEnrolleeDemographicViewModel viewModel)
        {
            viewModel.ThrowIfNull(nameof(viewModel));

            var enrollee = _mapper.Map<Enrollee>(viewModel);

            enrollee.UserId = Guid.NewGuid();
            enrollee.GPID = Gpid.NewGpid(PaperGpidPrefix);
            enrollee.Addresses = new[]
            {
                new EnrolleeAddress
                {
                    Address = _mapper.Map<PhysicalAddress>(viewModel.PhysicalAddress)
                }
            };
            enrollee.Submissions = new[]
            {
                new Submission
                {
                    Confirmed = true,
                    CreatedDate = DateTimeOffset.Now,
                    ProfileSnapshot = new JObject()
                }
            };
            enrollee.AddEnrolmentStatus(StatusType.Editable);
            enrollee.AddEnrolmentStatus(StatusType.UnderReview)
                .AddStatusReason(StatusReasonType.PaperEnrollee);

            _context.Enrollees.Add(enrollee);
            await _context.SaveChangesAsync();

            await _businessEventService.CreateEnrolleeEventAsync(enrollee.Id, "Enrollee Paper Submission Created");

            return enrollee;
        }

        public async Task UpdateDemographicsAsync(int enrolleeId, PaperEnrolleeDemographicViewModel viewModel)
        {
            var enrollee = await _context.Enrollees
                .Include(e => e.Addresses)
                .ThenInclude(a => a.Address)
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            _mapper.Map(viewModel, enrollee);
            _mapper.Map(viewModel.PhysicalAddress, enrollee.PhysicalAddress);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateCareSettingsAsync(int enrolleeId, PaperEnrolleeCareSettingViewModel viewModel)
        {
            var newCareSettings = viewModel.CareSettings.Select(code => new EnrolleeCareSetting
            {
                CareSettingCode = code
            });

            var newHealthAuthorities = viewModel.HealthAuthorities.Select(code => new EnrolleeHealthAuthority
            {
                HealthAuthorityCode = code
            });

            await ReplaceCollection(enrolleeId, newCareSettings);
            await ReplaceCollection(enrolleeId, newHealthAuthorities);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateOboSitesAsync(int enrolleeId, IEnumerable<PaperEnrolleeOboSiteViewModel> viewModels)
        {
            var newSites = _mapper.Map<IEnumerable<OboSite>>(viewModels);

            await ReplaceCollection(enrolleeId, newSites);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateCertificationsAsync(int enrolleeId, IEnumerable<PaperEnrolleeCertificationViewModel> viewModels)
        {
            var newCerts = _mapper.Map<IEnumerable<Certification>>(viewModels);

            await ReplaceCollection(enrolleeId, newCerts);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateSelfDeclarationsAsync(int enrolleeId, IEnumerable<PaperEnrolleeSelfDeclarationViewModel> viewModels)
        {
            var newDeclarations = _mapper.Map<IEnumerable<SelfDeclaration>>(viewModels);

            await ReplaceCollection(enrolleeId, newDeclarations);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Also updates the Submission for the Enrollee (to set the assigned Agreement Type).
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="viewModel"></param>
        public async Task UpdateAgreementAsync(int enrolleeId, PaperEnrolleeAgreementViewModel viewModel)
        {
            var submission = await _context.Submissions
                .SingleOrDefaultAsync(s => s.EnrolleeId == enrolleeId);

            submission.AgreementType = viewModel.AgreementType;

            var agreement = await _context.Agreements
                .SingleOrDefaultAsync(a => a.EnrolleeId == enrolleeId);

            if (agreement != null)
            {
                _context.Agreements.Remove(agreement);
            }

            await _context.SaveChangesAsync();

            await _enrolleeAgreementService.CreateEnrolleeAgreementAsync(enrolleeId);
            await _enrolleeAgreementService.AcceptCurrentEnrolleeAgreementAsync(enrolleeId);
        }

        public async Task AddEnrolleeAdjudicationDocumentsAsync(int enrolleeId, int adminId, IEnumerable<PaperEnrolleeDocumentViewModel> documents)
        {
            foreach (var document in documents)
            {
                var filename = await _documentClient.FinalizeUploadAsync(document.DocumentGuid, DestinationFolders.EnrolleeAdjudicationDocuments);

                if (string.IsNullOrWhiteSpace(filename))
                {
                    _logger.LogError($"Could not finalize document {document}");
                    continue;
                }

                var adjudicationDocument = new EnrolleeAdjudicationDocument
                {
                    DocumentGuid = document.DocumentGuid,
                    EnrolleeId = enrolleeId,
                    Filename = filename,
                    UploadedDate = DateTimeOffset.Now,
                    AdjudicatorId = adminId,
                    DocumentType = document.DocumentType
                };
                _context.EnrolleeAdjudicationDocuments.Add(adjudicationDocument);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EnrolleeAdjudicationDocument>> GetEnrolleeAdjudicationDocumentsAsync(int enrolleeId)
        {
            return await _context.EnrolleeAdjudicationDocuments
                .Include(d => d.Adjudicator)
                .Where(d => d.EnrolleeId == enrolleeId)
                .OrderByDescending(d => d.UploadedDate)
                .ToListAsync();
        }

        public async Task SetProfileCompletedAsync(int enrolleeId)
        {
            var enrollee = await _context.Enrollees
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            enrollee.ProfileCompleted = true;

            await _context.SaveChangesAsync();
        }

        public async Task FinalizeSubmissionAsync(int enrolleeId)
        {
            var enrollee = await _context.Enrollees
                .Include(e => e.EnrolmentStatuses)
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            enrollee.AddEnrolmentStatus(StatusType.RequiresToa)
                .AddStatusReason(StatusReasonType.Automatic);
            enrollee.AddEnrolmentStatus(StatusType.Editable);

            var submission = await _context.Submissions.SingleOrDefaultAsync(s => s.EnrolleeId == enrollee.Id);
            _context.Remove(submission);

            var newSubmission = await _enrolleeSubmissionService.CreateEnrolleeSubmissionAsync(enrolleeId, false);
            newSubmission.CreatedDate = submission.CreatedDate;
            newSubmission.AgreementType = submission.AgreementType;

            await _context.SaveChangesAsync();
        }

        private async Task ReplaceCollection<T>(int enrolleeId, IEnumerable<T> newItems) where T : class, IEnrolleeNavigationProperty
        {
            var oldItems = await _context.Set<T>()
                .Where(x => x.EnrolleeId == enrolleeId)
                .ToListAsync();

            var itemList = newItems.ToList();
            itemList.ForEach(x => x.EnrolleeId = enrolleeId);

            _context.Set<T>().RemoveRange(oldItems);
            _context.Set<T>().AddRange(itemList);
        }

        public async Task<bool> PotentialReturneeExistsAsync(DateTime dateOfBirth)
        {
            var confirmedLinks = await _context.EnrolleeLinkedEnrolment
                .AsNoTracking()
                .Where(cl => cl.Confirmed)
                .Select(cl => cl.PaperEnrolleeId)
                .ToListAsync();

            return await _context.Enrollees
                .AsNoTracking()
                .AnyAsync(
                    e => e.GPID.StartsWith(PaperGpidPrefix)
                    && !confirmedLinks.Contains(e.Id)
                    && e.DateOfBirth.Date == dateOfBirth.Date
                );
        }

        public async Task<IEnumerable<Enrollee>> GetPotentialPaperEnrolleeReturneesAsync(DateTime dateOfBirth)
        {
            var confirmedLinks = await _context.EnrolleeLinkedEnrolment
                .AsNoTracking()
                .Where(cl => cl.Confirmed)
                .Select(cl => cl.PaperEnrolleeId)
                .ToListAsync();

            return await _context.Enrollees
                .AsNoTracking()
                .Where(
                    e => e.GPID.StartsWith(PaperGpidPrefix)
                    && e.DateOfBirth.Date == dateOfBirth.Date
                    && !confirmedLinks.Contains(e.Id)
                )
                .ToListAsync();
        }

        public async Task<bool> LinkEnrolmentToPaperEnrolmentAsync(int enrolleeId, int paperEnrolleeId, bool isConfirmed = false)
        {
            var paperEnrollee = await _context.Enrollees
                .Where(
                    pe => pe.GPID.StartsWith(PaperGpidPrefix)
                    && pe.Id == paperEnrolleeId
                    && _context.EnrolleeLinkedEnrolment.Any(
                        link => link.PaperEnrolleeId == pe.Id
                        )
                )
                .AnyAsync();

            if (paperEnrollee)
            {
                return false;
            }

            var enrolleeLinkedEnrolment = await _context.EnrolleeLinkedEnrolment
                .Where(ele => ele.EnrolleeId == enrolleeId)
                .SingleOrDefaultAsync();

            enrolleeLinkedEnrolment.PaperEnrolleeId = paperEnrolleeId;
            enrolleeLinkedEnrolment.EnrolmentLinkDate = DateTime.Now;
            enrolleeLinkedEnrolment.Confirmed = isConfirmed;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task CreateInitialLinkAsync(int enrolleeId, string userProvidedGpid)
        {
            var enrolleeLinkedEnrolment = await _context.EnrolleeLinkedEnrolment
                .SingleOrDefaultAsync(ele => ele.EnrolleeId == enrolleeId);

            if (enrolleeLinkedEnrolment != null)
            {
                return;
            }

            var newLinkedEnrolment = new EnrolleeLinkedEnrolment
            {
                EnrolleeId = enrolleeId,
                UserProvidedGpid = userProvidedGpid,
            };

            _context.Add(newLinkedEnrolment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLinkedGpidAsync(int enrolleeId, string userUpdatedGpid)
        {
            var enrolleeLinkedEnrolment = await _context.EnrolleeLinkedEnrolment
                .Where(ele => ele.EnrolleeId == enrolleeId && !ele.Confirmed)
                .SingleOrDefaultAsync();

            enrolleeLinkedEnrolment.UserProvidedGpid = userUpdatedGpid;
            _context.SaveChanges();
        }

        public async Task<string> GetLinkedGpidAsync(int enrolleeId)
        {
            var linkedGpid = await _context.EnrolleeLinkedEnrolment
            .AsNoTracking()
            .Where(ele => ele.EnrolleeId == enrolleeId)
            .Select(ele => ele.UserProvidedGpid)
            .SingleOrDefaultAsync();

            return linkedGpid;
        }
    }
}
