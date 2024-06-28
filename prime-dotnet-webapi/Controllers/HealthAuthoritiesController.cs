using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

using Prime.Configuration.Auth;
using Prime.Services;
using Prime.Models.HealthAuthorities;
using Prime.ViewModels.Parties;
using Prime.ViewModels.HealthAuthorities;
using Prime.ViewModels;
using Prime.ViewModels.HealthAuthoritySites;
using System.Linq;
using System;
using Prime.Models;
using Prime.Models.Api;

namespace Prime.Controllers
{
    [Produces("application/json")]
    [Route("api/health-authorities")]
    [ApiController]
    public class HealthAuthoritiesController : PrimeControllerBase
    {
        private readonly IBusinessEventService _businessEventService;
        private readonly IDocumentService _documentService;
        private readonly IHealthAuthorityService _healthAuthorityService;
        private readonly IHealthAuthoritySiteService _healthAuthoritySiteService;
        private readonly ISiteService _siteService;

        public HealthAuthoritiesController(
            IBusinessEventService businessEventService,
            IDocumentService documentService,
            IHealthAuthorityService healthAuthorityService,
            IHealthAuthoritySiteService healthAuthoritySiteService,
            ISiteService siteService
        )
        {
            _businessEventService = businessEventService;
            _documentService = documentService;
            _healthAuthorityService = healthAuthorityService;
            _healthAuthoritySiteService = healthAuthoritySiteService;
            _siteService = siteService;
        }

        // GET: api/health-authorities
        /// <summary>
        /// Gets all of the Health Authority Organizations.
        /// </summary>
        [HttpGet(Name = nameof(GetHealthAuthorities))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<HealthAuthorityListViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetHealthAuthorities()
        {
            return Ok(await _healthAuthorityService.GetHealthAuthoritiesAsync());
        }

        // GET: api/health-authorities/5
        /// <summary>
        /// Gets a specific Health Authority Organization.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        [HttpGet("{healthAuthorityId:int}", Name = nameof(GetHealthAuthorityById))]
        [Authorize(Roles = Roles.PrimeEnrollee + "," + Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<HealthAuthorityViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetHealthAuthorityById(int healthAuthorityId)
        {
            var healthAuthority = await _healthAuthorityService.GetHealthAuthorityAsync(healthAuthorityId);
            if (healthAuthority == null)
            {
                return NotFound();
            }

            return Ok(healthAuthority);
        }

        // GET: api/health-authorities/5/authorized-users
        /// <summary>
        /// Get the Authorized Users for a Health Authority
        /// </summary>
        // <param name="healthAuthorityId"></param>
        [HttpGet("{healthAuthorityId}/authorized-users", Name = nameof(GetAuthorizedUsers))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<AuthorizedUserViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAuthorizedUsers(int healthAuthorityId)
        {
            if (!await _healthAuthorityService.HealthAuthorityExistsAsync(healthAuthorityId))
            {
                return NotFound($"Health Authority not found with id {healthAuthorityId}");
            }

            var users = await _healthAuthorityService.GetAuthorizedUsersAsync(healthAuthorityId);
            return Ok(users);
        }

        // GET: api/health-authorities/sites
        /// <summary>
        /// Gets all sites for any health authority.
        /// </summary>
        [HttpGet("sites", Name = nameof(GetAllHealthAuthoritySites))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<HealthAuthoritySiteAdminListViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllHealthAuthoritySites()
        {
            var sites = await _healthAuthoritySiteService.GetSitesAsync();

            var notifiedIds = await _siteService.GetNotifiedSiteIdsForAdminAsync(User);
            foreach (var site in sites)
            {
                site.HasNotification = notifiedIds.Contains(site.Id);
            }

            return Ok(sites);
        }

        // GET: api/health-authorities/sites-query
        /// <summary>
        /// Gets all sites for any health authority.
        /// </summary>
        [HttpGet("sites-query", Name = nameof(SearchHealthAuthoritySites))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiResultResponse<IEnumerable<HealthAuthoritySiteAdminListViewModel>>), StatusCodes.Status200OK)]
        public async Task<ActionResult> SearchHealthAuthoritySites([FromQuery] HealthAuthoritySiteSearchOptions search)
        {
            if (search.AssignToMe)
            {
                search.AdminUserName = User.GetPrimeUsername();
            }

            var sites = await _healthAuthoritySiteService.GetSitesAsync(search);

            var notifiedIds = await _siteService.GetNotifiedSiteIdsForAdminAsync(User);
            foreach (var site in sites)
            {
                site.HasNotification = notifiedIds.Contains(site.Id);
            }

            return Ok(sites);
        }

        // PUT: api/health-authorities/5/care-types
        /// <summary>
        /// Updates a specific Health Authority's care types.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="careTypes"></param>
        [HttpPut("{healthAuthorityId}/care-types", Name = nameof(UpdateCareTypes))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateCareTypes(int healthAuthorityId, IEnumerable<string> careTypes)
        {
            if (careTypes == null)
            {
                return BadRequest("Health authority care types cannot be null.");
            }

            if (!await _healthAuthorityService.HealthAuthorityExistsAsync(healthAuthorityId))
            {
                return NotFound($"Health Authority not found with id {healthAuthorityId}");
            }

            if (careTypes.Count() != careTypes.Distinct().Count())
            {
                return BadRequest("Unable to update care types. Duplicate care types provided");
            }

            if (!await _healthAuthorityService.UpdateCareTypesAsync(healthAuthorityId, careTypes))
            {
                return BadRequest("Unable to update care types. One or more health authority care types are in use");
            }

            return NoContent();
        }

        // PUT: api/health-authorities/5/vendors
        /// <summary>
        /// Updates a specific Health authorities vendors.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="vendors"></param>
        [HttpPut("{healthAuthorityId}/vendors", Name = nameof(UpdateVendors))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateVendors(int healthAuthorityId, IEnumerable<int> vendors)
        {
            if (vendors == null)
            {
                return BadRequest("Health authority vendors cannot be null.");
            }

            if (!await _healthAuthorityService.HealthAuthorityExistsAsync(healthAuthorityId))
            {
                return NotFound($"Health Authority not found with id {healthAuthorityId}");
            }

            if (vendors.Count() != vendors.Distinct().Count())
            {
                return BadRequest("Unable to update care types. Duplicate vendors provided");
            }

            if (!await _healthAuthorityService.UpdateVendorsAsync(healthAuthorityId, vendors))
            {
                return BadRequest("Unable to update care types. One or more health authority vendors are in use");
            }

            return NoContent();
        }

        // PUT: api/health-authorities/5/care-type-vendor
        /// <summary>
        /// Updates a specific Health authorities care type vendor mapping.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="careTypeVendors"></param>
        [HttpPut("{healthAuthorityId}/care-type-vendor", Name = nameof(UpdateCareTypeVendor))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateCareTypeVendor(int healthAuthorityId, IEnumerable<HealthAuthorityCareTypeVendorModel> careTypeVendors)
        {

            if (!await _healthAuthorityService.HealthAuthorityExistsAsync(healthAuthorityId))
            {
                return NotFound($"Health Authority not found with id {healthAuthorityId}");
            }

            if (careTypeVendors.Count() == 0)
            {
                return BadRequest("Unable to update care type vendor ");
            }

            if (careTypeVendors == null)
            {
                return BadRequest("Health authority vendors cannot be null.");
            }

            if (!await _healthAuthorityService.UpdateCareTypeVendorsAsync(healthAuthorityId, careTypeVendors))
            {
                return BadRequest("Unable to update care type vendor");
            }

            return NoContent();
        }

        // PUT: api/health-authorities/1/privacy-office
        /// <summary>
        /// Updates the Privacy Office on a Health Authority
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="privacyOffice"></param>
        [HttpPut("{healthAuthorityId}/privacy-office", Name = nameof(UpdatePrivacyOffice))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdatePrivacyOffice(int healthAuthorityId, PrivacyOfficeViewModel privacyOffice)
        {
            await _healthAuthorityService.UpdatePrivacyOfficeAsync(healthAuthorityId, privacyOffice);
            return NoContent();
        }

        // PUT: api/health-authorities/1/privacy-officers
        /// <summary>
        /// Updates the Privacy Officer contacts on a Health Authority
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="contacts"></param>
        [HttpPut("{healthAuthorityId}/privacy-officers", Name = nameof(UpdatePrivacyOfficerContacts))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdatePrivacyOfficerContacts(int healthAuthorityId, IEnumerable<ContactViewModel> contacts)
        {
            await _healthAuthorityService.UpdateContactsAsync<HealthAuthorityPrivacyOfficer>(healthAuthorityId, contacts);
            return NoContent();
        }

        // PUT: api/health-authorities/1/technical-supports
        /// <summary>
        /// Updates the Technical Support contacts on a Health Authority
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="contacts"></param>
        [HttpPut("{healthAuthorityId}/technical-supports", Name = nameof(UpdateTechnicalSupportContacts))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateTechnicalSupportContacts(int healthAuthorityId, IEnumerable<TechnicalSupportContactViewModel> contacts)
        {
            await _healthAuthorityService.UpdateContactsAsync<HealthAuthorityTechnicalSupport>(healthAuthorityId, contacts);
            return NoContent();
        }

        // PUT: api/health-authorities/1/pharmanet-administrators
        /// <summary>
        /// Updates the Pharmanet Administrator contacts on a Health Authority
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="contacts"></param>
        [HttpPut("{healthAuthorityId}/pharmanet-administrators", Name = nameof(UpdatePharmanetAdministratorContacts))]
        [Authorize(Roles = Roles.EditSite)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdatePharmanetAdministratorContacts(int healthAuthorityId, IEnumerable<ContactViewModel> contacts)
        {
            await _healthAuthorityService.UpdateContactsAsync<HealthAuthorityPharmanetAdministrator>(healthAuthorityId, contacts);
            return NoContent();
        }

        // GET: api/health-authorities/5/vendors/5/sites
        /// <summary>
        /// returns a list of site ids for a given vendor and health authority
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="healthAuthorityVendorId"></param>
        [HttpGet("{healthAuthorityId}/vendors/{healthAuthorityVendorId}/sites", Name = nameof(GetSitesByVendor))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> GetSitesByVendor(int healthAuthorityId, int healthAuthorityVendorId)
        {
            if (!await _healthAuthorityService.HealthAuthorityExistsAsync(healthAuthorityId))
            {
                return NotFound($"Health Authority not found with id {healthAuthorityId}");
            }

            var siteIds = await _healthAuthorityService.GetSitesByVendorAsync(healthAuthorityId, healthAuthorityVendorId);

            return Ok(siteIds);
        }


        // GET: api/health-authorities/5/care-types/5/sites
        /// <summary>
        /// returns a list of site ids for a given care type and health authority
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="healthAuthorityCareTypeId"></param>
        [HttpGet("{healthAuthorityId}/care-types/{healthAuthorityCareTypeId}/sites", Name = nameof(GetSitesByCareType))]
        [Authorize(Roles = Roles.ViewSite)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> GetSitesByCareType(int healthAuthorityId, int healthAuthorityCareTypeId)
        {
            if (!await _healthAuthorityService.HealthAuthorityExistsAsync(healthAuthorityId))
            {
                return NotFound($"Health Authority not found with id {healthAuthorityId}");
            }

            var siteIds = await _healthAuthorityService.GetSitesByCareTypeAsync(healthAuthorityId, healthAuthorityCareTypeId);

            return Ok(siteIds);
        }

        // PUT: api/health-authorities/1/organization-agreement
        /// <summary>
        ///    Create or Update health auth organization agreement
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        /// <param name="documentGuid"></param>
        [HttpPut("{healthAuthorityId}/organization-agreement", Name = nameof(CreateOrUpdateOgranizationAgreement))]
        [Authorize(Roles = Roles.PrimeMaintenance)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<HealthAuthorityOrganizationAgreementDocument>), StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateOrUpdateOgranizationAgreement(int healthAuthorityId, [FromQuery] Guid documentGuid)
        {
            if (!await _healthAuthorityService.HealthAuthorityExistsAsync(healthAuthorityId))
            {
                return NotFound($"Health Authority not found with id {healthAuthorityId}");
            }

            var document = await _healthAuthorityService.AddOrReplaceBusinessLicenceDocumentAsync(healthAuthorityId, documentGuid);
            if (document == null)
            {
                return BadRequest("Organization Agreement Document could not be created; network error or upload is already submitted");
            }

            // TODO: Potentially get a ticket to add business events to the Health Authority Organization Side of the app.

            return Ok(document); ;
        }

        // GET: api/health-authorities/1/organization-agreement/token
        /// <summary>
        /// Gets a download token for the latest organization agreement for a health authority.
        /// </summary>
        /// <param name="healthAuthorityId"></param>
        [HttpGet("{healthAuthorityId}/organization-agreement/token", Name = nameof(GetOgranizationAgreementDocumentToken))]
        [Authorize(Roles = Roles.PrimeMaintenance)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ApiMessageResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResultResponse<string>), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> GetOgranizationAgreementDocumentToken(int healthAuthorityId)
        {
            if (!await _healthAuthorityService.HealthAuthorityExistsAsync(healthAuthorityId))
            {
                return NotFound($"Health Authority not found with id {healthAuthorityId}");
            }

            var token = await _documentService.GetDownloadTokenForHealthAuthorityOrgAgreementDocument(healthAuthorityId);
            if (token == null)
            {
                return NotFound($"No Organization Agreement Document found for Health Authority with id {healthAuthorityId}");
            }

            return Ok(token);
        }
    }
}
