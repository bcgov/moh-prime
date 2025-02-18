using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using DelegateDecompiler.EntityFrameworkCore;

using Prime.Engines;
using Prime.HttpClients;
using Prime.HttpClients.DocumentManagerApiDefinitions;
using Prime.Models;
using Prime.Models.Api;
using Prime.Services.Razor;
using Prime.ViewModels;

namespace Prime.Services
{
    public class SubmissionService : BaseService, ISubmissionService
    {
        private readonly IAgreementService _agreementService;
        private readonly IBusinessEventService _businessEventService;
        private readonly IEmailService _emailService;
        private readonly IEnrolleeAgreementService _enrolleeAgreementService;
        private readonly IEnrolleeService _enrolleeService;
        private readonly IEnrolleeSubmissionService _enrolleeSubmissionService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IPrivilegeService _privilegeService;
        private readonly ISubmissionRulesService _submissionRulesService;
        private readonly IVerifiableCredentialService _verifiableCredentialService;
        private readonly IRazorConverterService _razorConverterService;
        private readonly IPdfService _pdfService;
        private readonly IDocumentManagerClient _documentManagerClient;

        public SubmissionService(
            ApiDbContext context,
            ILogger<SubmissionService> logger,
            IAgreementService agreementService,
            IBusinessEventService businessEventService,
            IEmailService emailService,
            IEnrolleeAgreementService enrolleeAgreementService,
            IEnrolleeService enrolleeService,
            IEnrolleeSubmissionService enrolleeSubmissionService,
            IHttpContextAccessor httpContext,
            IPrivilegeService privilegeService,
            ISubmissionRulesService submissionRulesService,
            IVerifiableCredentialService verifiableCredentialService,
            IRazorConverterService razorConverterService,
            IPdfService pdfService,
            IDocumentManagerClient documentManagerClient)
            : base(context, logger)
        {
            _agreementService = agreementService;
            _businessEventService = businessEventService;
            _emailService = emailService;
            _enrolleeAgreementService = enrolleeAgreementService;
            _enrolleeService = enrolleeService;
            _enrolleeSubmissionService = enrolleeSubmissionService;
            _httpContext = httpContext;
            _privilegeService = privilegeService;
            _submissionRulesService = submissionRulesService;
            _verifiableCredentialService = verifiableCredentialService;
            _razorConverterService = razorConverterService;
            _pdfService = pdfService;
            _documentManagerClient = documentManagerClient;
        }

        public async Task SubmitApplicationAsync(int enrolleeId, EnrolleeUpdateModel updatedProfile)
        {
            var enrollee = await _context.Enrollees
                .Include(e => e.Addresses)
                    .ThenInclude(ea => ea.Address)
                .Include(e => e.Certifications)
                    .ThenInclude(c => c.License)
                        .ThenInclude(l => l.LicenseDetails)
                .Include(e => e.OboSites)
                    .ThenInclude(s => s.PhysicalAddress)
                .Include(e => e.EnrolleeRemoteUsers)
                .Include(e => e.RemoteAccessSites)
                    .ThenInclude(ras => ras.Site)
                .Include(e => e.RemoteAccessLocations)
                    .ThenInclude(ral => ral.PhysicalAddress)
                .Include(e => e.EnrolleeCareSettings)
                .Include(e => e.EnrolleeHealthAuthorities)
                .Include(e => e.Agreements)
                    .ThenInclude(a => a.AgreementVersion)
                .Include(e => e.SelfDeclarations)
                .Include(e => e.Submissions)
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            var agreementDtos = await _context.AgreementVersions
                .Select(av => new
                {
                    av.Id,
                    av.AgreementType,
                    av.EffectiveDate
                })
                .ToListAsync();

            var newestAgreementVersionIds = agreementDtos
                .GroupBy(av => av.AgreementType)
                .Select(group => group.OrderByDescending(av => av.EffectiveDate).First().Id)
                .ToList();

            var minorUpdate = await _submissionRulesService.QualifiesAsMinorUpdateAsync(enrollee, updatedProfile, newestAgreementVersionIds);
            await _enrolleeService.UpdateEnrolleeAsync(enrolleeId, updatedProfile);

            if (minorUpdate)
            {
                return;
            }

            await _enrolleeSubmissionService.CreateEnrolleeSubmissionAsync(enrolleeId);
            enrollee.AddEnrolmentStatus(StatusType.UnderReview);
            await _businessEventService.CreateStatusChangeEventAsync(enrolleeId, "Submitted");

            if (_httpContext.HttpContext.User.HasVCIssuance())
            {
                try
                {
                    await _verifiableCredentialService.RevokeCredentialsAsync(enrollee.Id);
                    await _verifiableCredentialService.CreateConnectionAsync(enrollee);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error occurred attempting to create a connection invitation through the Verifiable Credential agent: ${ex}", ex);
                }
            }

            await ProcessEnrolleeApplicationRules(enrolleeId);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Performs a Status Action on an Enrollee.
        /// Returns true if the Action was successfully performed.
        /// </summary>
        public async Task<bool> PerformEnrolleeStatusActionAsync(int enrolleeId, EnrolleeStatusAction action, object additionalParameters = null)
        {
            var enrollee = await _context.Enrollees
                .Include(e => e.Addresses)
                    .ThenInclude(ea => ea.Address)
                .Include(e => e.EnrolmentStatuses)
                    .ThenInclude(es => es.Status)
                .Include(e => e.EnrolmentStatuses)
                    .ThenInclude(es => es.EnrolmentStatusReasons)
                        .ThenInclude(esr => esr.StatusReason)
                .Include(e => e.Certifications)
                    .ThenInclude(cer => cer.College)
                .Include(e => e.Certifications)
                    .ThenInclude(l => l.License)
                .Include(e => e.Agreements)
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            if (!EnrolleeStatusStateEngine.AllowableAction(action, enrollee.CurrentStatus))
            {
                return false;
            }

            return await HandleEnrolleeStatusActionAsync(action, enrollee, additionalParameters);
        }

        public async Task UpdateAlwaysManualAsync(int enrolleeId, bool alwaysManual)
        {
            var enrollee = await _context.Enrollees
                .SingleAsync(e => e.Id == enrolleeId);

            enrollee.AlwaysManual = alwaysManual;
            await _context.SaveChangesAsync();
        }

        public async Task ConfirmLatestSubmissionAsync(int enrolleeId)
        {
            var submission = await _context.Submissions
                .Where(s => s.EnrolleeId == enrolleeId)
                .OrderByDescending(s => s.CreatedDate)
                .FirstAsync();

            submission.Confirmed = true;
            await _context.SaveChangesAsync();
        }

        public async Task BulkRerunRulesAsync()
        {
            var pharmanetStatusReasons = new[]
            {
                (int) StatusReasonType.PharmanetError,
                (int) StatusReasonType.NotInPharmanet,
                (int) StatusReasonType.BirthdateDiscrepancy,
                (int) StatusReasonType.NameDiscrepancy,
                (int) StatusReasonType.Practicing
            };

            var enrollees = GetBaseQueryForEnrolleeApplicationRules()
                .Where(e => e.Adjudicator == null)
                .Where(e => e.CurrentStatus.StatusCode == (int)StatusType.UnderReview)
                .Where(e => e.CurrentStatus.EnrolmentStatusReasons.Any(esr => pharmanetStatusReasons.Contains(esr.StatusReasonCode)))
                // Need `DecompileAsync` due to computed property `CurrentStatus`
                .DecompileAsync()
                .ToList();

            foreach (var enrollee in enrollees)
            {
                // Group results of the rules under a new enrollment status
                enrollee.AddEnrolmentStatus(StatusType.UnderReview);
                await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Cron Job running the enrollee application rules");

                _logger.LogDebug($"RerunRulesAsync on {enrollee.FullName} (Id {enrollee.Id})");
                if (await _submissionRulesService.QualifiesForAutomaticAdjudicationAsync(enrollee))
                {
                    await AdjudicatedAutomatically(enrollee, "Cron Job Automatically Approved");
                }
                // We don't perform a `_enrolleeService.RemoveNotificationsAsync`
            }
            await _context.SaveChangesAsync();
        }

        public async Task RerunRulesForNaturopathsAsync(bool listOnly)
        {
            var pharmanetStatusReasons = new[]
            {
                (int) StatusReasonType.BirthdateDiscrepancy,
                // NotInPharmanet included due to earlier mistake running against non-PROD PharmaNet API
                (int) StatusReasonType.NotInPharmanet
            };

            var enrollees = GetBaseQueryForEnrolleeApplicationRules()
                .Where(e => e.Adjudicator == null)
                .Where(e => e.CurrentStatus.StatusCode == (int)StatusType.UnderReview)
                .Where(e => e.CurrentStatus.EnrolmentStatusReasons.Any(esr => pharmanetStatusReasons.Contains(esr.StatusReasonCode)))
                // Looking for Full Naturopaths
                .Where(e => e.Certifications.Any(c => c.LicenseCode == 78))
                // Need `DecompileAsync` due to computed property `CurrentStatus`
                .DecompileAsync()
                .ToList();

            foreach (var enrollee in enrollees)
            {
                Console.WriteLine($"RerunRulesAsync on {enrollee.FullName} (Id {enrollee.Id}, DOB: {enrollee.DateOfBirth})");
                if (!listOnly)
                {
                    // Group results of the rules under a new enrollment status
                    enrollee.AddEnrolmentStatus(StatusType.UnderReview);
                    await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Cron Job running the enrollee application rules");

                    if (await _submissionRulesService.QualifiesForAutomaticAdjudicationAsync(enrollee, true))
                    {
                        Console.WriteLine($"Cron Job Automatically Approved {enrollee.FullName} (Id {enrollee.Id})");
                        await AdjudicatedAutomatically(enrollee, "Cron Job Automatically Approved");
                    }
                    // We don't perform a `_enrolleeService.RemoveNotificationsAsync`
                }
            }
            await _context.SaveChangesAsync();
        }

        private async Task<bool> HandleEnrolleeStatusActionAsync(EnrolleeStatusAction action, Enrollee enrollee, object additionalParameters)
        {
            switch (action)
            {
                case EnrolleeStatusAction.Approve:
                    await ApproveApplicationAsync(enrollee);
                    break;

                case EnrolleeStatusAction.AcceptToa:
                    return await AcceptToaAsync(enrollee, additionalParameters);

                case EnrolleeStatusAction.DeclineToa:
                    await DeclineToaAsync(enrollee);
                    break;

                case EnrolleeStatusAction.ChangeToa:
                    await ChangeToaAsync(enrollee, additionalParameters);
                    break;

                case EnrolleeStatusAction.EnableEditing:
                    await EnableEditingAsync(enrollee);
                    break;

                case EnrolleeStatusAction.LockProfile:
                    await LockProfileAsync(enrollee);
                    break;

                case EnrolleeStatusAction.DeclineProfile:
                    await DeclineProfileAsync(enrollee);
                    break;

                case EnrolleeStatusAction.RerunRules:
                    await RerunRulesAsync(enrollee);
                    break;

                case EnrolleeStatusAction.CancelToaAssignment:
                    await _enrolleeAgreementService.DeleteObsoleteEnrolleeAgreementAsync(enrollee.Id);
                    await CancelToaAssignmentAsync(enrollee);
                    break;

                case EnrolleeStatusAction.UnlockedProfile:
                    await UnlockProfileAsync(enrollee);
                    break;

                default:
                    throw new InvalidOperationException($"Action {action} is not recognized in {nameof(HandleEnrolleeStatusActionAsync)}");
            }
            await _enrolleeService.RemoveNotificationsAsync(enrollee.Id);

            return true;
        }

        private async Task ApproveApplicationAsync(Enrollee enrollee)
        {
            var newStatus = enrollee.AddEnrolmentStatus(StatusType.RequiresToa);
            newStatus.AddStatusReason(StatusReasonType.Manual);

            await _enrolleeAgreementService.CreateEnrolleeAgreementAsync(enrollee.Id);

            await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Manually Approved");
            await _context.SaveChangesAsync();
            await _emailService.SendReminderEmailAsync(enrollee.Id);
            await _businessEventService.CreateEmailEventAsync(enrollee.Id, "Notified Enrollee");
            // Manually Approved submissions are automatically confirmed
            await ConfirmLatestSubmissionAsync(enrollee.Id);
        }

        private async Task<bool> ChangeToaAsync(
            Enrollee enrollee,
            object changeToaParameters)
        {
            if (changeToaParameters is ChangeToaUpdateViewModel param &&
            param.AgreementType is AgreementType type)
            {
                var enrolleeSubmissions = await _enrolleeSubmissionService.GetEnrolleeSubmissionsAsync(enrollee.Id);
                string currentAgreementType = enrolleeSubmissions.OrderByDescending(s => s.CreatedDate).First().AgreementType.ToString();

                var newStatus = enrollee.AddEnrolmentStatus(StatusType.RequiresToa);
                newStatus.AddStatusReason(StatusReasonType.Manual);
                await _context.SaveChangesAsync();

                await _enrolleeService.AssignToaAgreementType(enrollee.Id, type);

                await _enrolleeAgreementService.CreateEnrolleeAgreementAsync(enrollee.Id);

                await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, $"TOA changed from {currentAgreementType} to {param.AgreementType}, Note: {param.Note}");

                await _emailService.SendReminderEmailAsync(enrollee.Id);
                await _businessEventService.CreateEmailEventAsync(enrollee.Id, "Notified Enrollee");

                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<bool> AcceptToaAsync(Enrollee enrollee, object additionalParameters)
        {
            // Currently (as of 2021-09-27), only BCSC identity accepted so the following code will not execute
            if (enrollee.IdentityAssuranceLevel < 3)
            {
                // Enrollees with lower assurance levels cannot electronically sign, and so must upload a signed Agreement
                if (additionalParameters is Guid documentGuid)
                {
                    var agreement = await _enrolleeAgreementService.GetCurrentAgreementAsync(enrollee.Id);
                    var agreementDocument = await _agreementService.AddSignedAgreementDocumentAsync(agreement.Id, documentGuid);
                    if (agreementDocument == null)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                // for regular enrollee, get the pending agreement, create the PDF and store it in document manager
                var pendingAgreementId = enrollee.Agreements.OrderByDescending(a => a.CreatedDate).Select(a => a.Id).First();
                Agreement agreement = await _enrolleeAgreementService.GetEnrolleeAgreementAsync(enrollee.Id, pendingAgreementId, true);
                var html = await _razorConverterService.RenderTemplateToStringAsync(RazorTemplates.Agreements.PdfNoSignature, agreement);
                var pdfbinary = _pdfService.Generate(html);
                var filename = "Terms-Of-Access.pdf";
                var documentGuid = await _documentManagerClient.SendFileAsync(new System.IO.MemoryStream(pdfbinary), filename, DestinationFolders.SignedAgreements);

                var agreementDocument = await _agreementService.AddSignedAgreementDocumentAsync(agreement.Id, documentGuid, filename);
                if (agreementDocument == null)
                {
                    return false;
                }
            }

            enrollee.AddEnrolmentStatus(StatusType.Editable);
            await SetGpid(enrollee);
            await _enrolleeAgreementService.AcceptCurrentEnrolleeAgreementAsync(enrollee.Id);
            await _privilegeService.AssignPrivilegesToEnrolleeAsync(enrollee.Id, enrollee);
            await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Accepted TOA");

            if (enrollee.AdjudicatorId != null)
            {
                await _enrolleeService.UpdateEnrolleeAdjudicator(enrollee.Id);
                await _businessEventService.CreateAdminActionEventAsync(enrollee.Id, "Admin disclaimed after TOA accepted");
            }

            await _context.SaveChangesAsync();

            return true;
        }

        private async Task DeclineToaAsync(Enrollee enrollee)
        {
            enrollee.AddEnrolmentStatus(StatusType.Editable);
            await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Declined TOA");
            await _context.SaveChangesAsync();
        }

        private async Task EnableEditingAsync(Enrollee enrollee)
        {
            enrollee.AddEnrolmentStatus(StatusType.Editable);
            await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Enabled Editing");
            await _context.SaveChangesAsync();
            await _emailService.SendReminderEmailAsync(enrollee.Id);
            await _businessEventService.CreateEmailEventAsync(enrollee.Id, "Notified Enrollee");
        }

        private async Task LockProfileAsync(Enrollee enrollee)
        {
            enrollee.AddEnrolmentStatus(StatusType.Locked);
            await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Locked");
            await _context.SaveChangesAsync();
        }

        private async Task DeclineProfileAsync(Enrollee enrollee)
        {
            enrollee.AddEnrolmentStatus(StatusType.Declined);
            await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Declined");
            await _context.SaveChangesAsync();
            await _enrolleeAgreementService.ExpireCurrentEnrolleeAgreementAsync(enrollee.Id);

            if (_httpContext.HttpContext.User.HasVCIssuance())
            {
                try
                {
                    await _verifiableCredentialService.RevokeCredentialsAsync(enrollee.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error occurred attempting to revoke credentials through the Verifiable Credential agent: ${ex}", ex);
                }
            }
        }

        private async Task RerunRulesAsync(Enrollee enrollee)
        {
            enrollee.AddEnrolmentStatus(StatusType.UnderReview);
            await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Adjudicator manually ran the enrollee application rules");
            await ProcessEnrolleeApplicationRules(enrollee.Id);
            await _context.SaveChangesAsync();
        }

        private async Task CancelToaAssignmentAsync(Enrollee enrollee)
        {
            var newStatus = enrollee.AddEnrolmentStatus(StatusType.UnderReview);
            newStatus.AddStatusReason(StatusReasonType.Manual, "Adjudicator cancelled TOA assignment");
            await _enrolleeSubmissionService.CreateEnrolleeSubmissionAsync(enrollee.Id, false);
            await _context.SaveChangesAsync();

            await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Adjudicator cancelled TOA assignment");
        }

        private async Task UnlockProfileAsync(Enrollee enrollee)
        {
            enrollee.AddEnrolmentStatus(StatusType.UnderReview);
            await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Unlocked");
            await _context.SaveChangesAsync();
        }

        private async Task SetGpid(Enrollee enrollee)
        {
            if (string.IsNullOrWhiteSpace(enrollee.GPID))
            {
                do
                {
                    enrollee.GPID = Gpid.NewGpid();
                }
                while (await _enrolleeService.GpidExistsAsync(enrollee.GPID));
            }
        }

        private async Task ProcessEnrolleeApplicationRules(int enrolleeId)
        {
            // TODO: UpdateEnrollee re-fetches the model, removing the includes we need for the adjudication rules. Fix how this model loading is done.
            var enrollee = await GetBaseQueryForEnrolleeApplicationRules()
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            if (await _submissionRulesService.QualifiesForAutomaticAdjudicationAsync(enrollee))
            {
                await AdjudicatedAutomatically(enrollee, "Automatically Approved");
            }
        }

        private async Task AdjudicatedAutomatically(Enrollee enrollee, string businessEventDesc)
        {
            var newStatus = enrollee.AddEnrolmentStatus(StatusType.RequiresToa);
            newStatus.AddStatusReason(StatusReasonType.Automatic);

            await _enrolleeAgreementService.CreateEnrolleeAgreementAsync(enrollee.Id);
            await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, businessEventDesc);
        }

        private IQueryable<Enrollee> GetBaseQueryForEnrolleeApplicationRules()
        {
            return _context.Enrollees
                .Include(e => e.EnrolleeCareSettings)
                    .ThenInclude(e => e.CareSetting)
                .Include(e => e.Submissions)
                .Include(e => e.Addresses)
                    .ThenInclude(ea => ea.Address)
                .Include(e => e.SelfDeclarations)
                .Include(e => e.EnrolmentStatuses)
                    .ThenInclude(es => es.EnrolmentStatusReasons)
                .Include(e => e.Certifications)
                    .ThenInclude(c => c.License)
                        .ThenInclude(l => l.LicenseDetails)
                .AsSplitQuery();
        }
    }
}
