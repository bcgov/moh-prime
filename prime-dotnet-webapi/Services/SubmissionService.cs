using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using DelegateDecompiler.EntityFrameworkCore;

using Prime.Engines;
using Prime.Models;
using Prime.Models.Api;
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

        private readonly int[] PharmanetSpecificStatusReasons = new[]
            {
                (int) StatusReasonType.PharmanetError,
                (int) StatusReasonType.NotInPharmanet,
                (int) StatusReasonType.BirthdateDiscrepancy,
                (int) StatusReasonType.NameDiscrepancy,
                (int) StatusReasonType.Practicing
            };

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
            IVerifiableCredentialService verifiableCredentialService)
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
        }

        public async Task SubmitApplicationAsync(int enrolleeId, EnrolleeUpdateModel updatedProfile)
        {
            var enrollee = await _context.Enrollees
                .Include(e => e.Addresses)
                    .ThenInclude(ea => ea.Address)
                .Include(e => e.Certifications)
                    .ThenInclude(c => c.License)
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
                .Include(e => e.SelfDeclarations)
                .Include(e => e.Submissions)
                .SingleOrDefaultAsync(e => e.Id == enrolleeId);

            bool minorUpdate = await _submissionRulesService.QualifiesAsMinorUpdateAsync(enrollee, updatedProfile);
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
                    await CancelToaAssignmentAsync(enrollee);
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
            await _emailService.SendReminderEmailAsync(enrollee.Id);
            await _businessEventService.CreateEmailEventAsync(enrollee.Id, "Notified Enrollee");
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

        public async Task RerunRulesAsync()
        {
            var enrollees = GetBaseQueryForEnrolleeApplicationRules()
                .Where(e => e.Adjudicator == null)
                .Where(e => e.CurrentStatus.StatusCode == (int)StatusType.UnderReview)
                .Where(e => e.CurrentStatus.EnrolmentStatusReasons.Any(esr => PharmanetSpecificStatusReasons.Contains(esr.StatusReasonCode)))
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

        private async Task CancelToaAssignmentAsync(Enrollee enrollee)
        {
            var newStatus = enrollee.AddEnrolmentStatus(StatusType.UnderReview);
            newStatus.AddStatusReason(StatusReasonType.Manual, "Adjudicator cancelled TOA assignment");
            await _enrolleeSubmissionService.CreateEnrolleeSubmissionAsync(enrollee.Id, false);
            await _context.SaveChangesAsync();

            await _businessEventService.CreateStatusChangeEventAsync(enrollee.Id, "Adjudicator cancelled TOA assignment");
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
                .Include(e => e.Submissions)
                .Include(e => e.Addresses)
                    .ThenInclude(ea => ea.Address)
                .Include(e => e.SelfDeclarations)
                .Include(e => e.EnrolmentStatuses)
                    .ThenInclude(es => es.EnrolmentStatusReasons)
                .Include(e => e.Certifications)
                    .ThenInclude(c => c.License);
        }
    }
}
