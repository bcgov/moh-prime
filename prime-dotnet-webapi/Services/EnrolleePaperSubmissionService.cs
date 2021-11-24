using AutoMapper;
using DelegateDecompiler.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task UpdateDeviceProviderAsync(int enrolleeId, string deviceProviderIdentifier)
        {
            var enrollee = await _context.Enrollees
                .SingleAsync(e => e.Id == enrolleeId);

            enrollee.DeviceProviderIdentifier = deviceProviderIdentifier;

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

        public async Task<bool> MatchingSubmissionExistsAsync(DateTime dateOfBirth)
        {
            return await _context.Enrollees
                .AsNoTracking()
                .AnyAsync(e => e.GPID.StartsWith(PaperGpidPrefix)
                    && e.DateOfBirth.Date == dateOfBirth.Date
                    && !_context.EnrolleeLinkedEnrolments
                        .Any(link => link.PaperEnrolleeId == e.Id));
        }

        public async Task<bool> GetIsEnrolleeApprovedAsync(int enrolleeId)
        {
            return await _context.Enrollees
                    .AsNoTracking()
                    .DecompileAsync()
                    .AnyAsync(e => e.Id == enrolleeId
                        && e.ApprovedDate.HasValue);
        }

        public async Task<IEnumerable<Enrollee>> GetPotentialPaperEnrolleeReturneesAsync(DateTime dateOfBirth)
        {
            // We want all unlinked paper enrollees with a matching DOB
            // Handle the linkage in the LinkEnrolmentToPaperEnrolmentAsync
            return await _context.Enrollees
                .AsNoTracking()
                .Where(
                    e => e.GPID.StartsWith(PaperGpidPrefix)
                    && e.DateOfBirth.Date == dateOfBirth.Date
                    && !_context.EnrolleeLinkedEnrolments
                        .Any(link => link.PaperEnrolleeId == e.Id)
                )
                .ToListAsync();
        }

        /// <summary>
        /// Links an Enrollee to a Paper Enrollee.
        /// Requires that the EnrolleeLinkedEnrolment already exist (see SetLinkedGpidAsync)
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="paperEnrolleeId"></param>
        public async Task<bool> LinkEnrolleeToPaperEnrolmentAsync(int enrolleeId, int paperEnrolleeId)
        {
            var linkedEnrolment = await _context.EnrolleeLinkedEnrolments
                .SingleOrDefaultAsync(link => link.EnrolleeId == enrolleeId);

            var enrolleeIsPaper = await _context.Enrollees
                .AsNoTracking()
                .AnyAsync(e => e.Id == enrolleeId
                    && e.GPID.StartsWith(PaperGpidPrefix));

            var paperIsPaper = await _context.Enrollees
                .AsNoTracking()
                .AnyAsync(e => e.Id == paperEnrolleeId
                    && e.GPID.StartsWith(PaperGpidPrefix));

            if (enrolleeIsPaper
                || !paperIsPaper
                || linkedEnrolment == null
                || linkedEnrolment.PaperEnrolleeId.HasValue)
            {
                // Cannot create link from a Paper Enrollee, to a regular Enrollee, or from an Enrollee that is already linked.
                return false;
            }

            linkedEnrolment.PaperEnrolleeId = paperEnrolleeId;
            linkedEnrolment.EnrolmentLinkDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Sets the GPID on the Enrollee's EnrolleeLinkedEnrolment, creating one if necessary.
        /// Cannot set the Linked GPID on a Paper Enrollee or on an Enrollee that has already been linked to a Paper Enrolment.
        /// </summary>
        /// <param name="enrolleeId"></param>
        /// <param name="userProvidedGpid"></param>
        public async Task<bool> SetLinkedGpidAsync(int enrolleeId, string userProvidedGpid)
        {
            var linkedEnrolment = await _context.EnrolleeLinkedEnrolments
                .SingleOrDefaultAsync(link => link.EnrolleeId == enrolleeId);

            var enrolleeIsPaper = await _context.Enrollees
                .AsNoTracking()
                .AnyAsync(e => e.Id == enrolleeId
                    && e.GPID.StartsWith(PaperGpidPrefix));

            if (enrolleeIsPaper || linkedEnrolment?.PaperEnrolleeId != null)
            {
                // Cannot set linked GPID on Paper Enrollees or Enrollees that are already linked.
                return false;
            }

            if (linkedEnrolment == null)
            {
                linkedEnrolment = new EnrolleeLinkedEnrolment
                {
                    EnrolleeId = enrolleeId,
                };
                _context.EnrolleeLinkedEnrolments.Add(linkedEnrolment);
            }

            linkedEnrolment.UserProvidedGpid = userProvidedGpid;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<string> GetLinkedGpidAsync(int enrolleeId)
        {
            var linkedGpid = await _context.EnrolleeLinkedEnrolments
                .AsNoTracking()
                .Where(link => link.EnrolleeId == enrolleeId)
                .Select(link => link.UserProvidedGpid)
                .SingleOrDefaultAsync();

            return linkedGpid;
        }
    }
}
