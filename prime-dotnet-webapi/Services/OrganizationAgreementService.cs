using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Prime.Models;
using Prime.Models.Api;
using Prime.ViewModels;
using Prime.ViewModels.Agreements;
using Prime.HttpClients;
using Prime.HttpClients.DocumentManagerApiDefinitions;
using Prime.Services.Razor;

namespace Prime.Services
{
    public class OrganizationAgreementService : BaseService, IOrganizationAgreementService
    {
        private readonly IMapper _mapper;
        private readonly IAgreementService _agreementService;
        private readonly IPdfService _pdfService;
        private readonly IRazorConverterService _razorConverterService;
        private readonly IDocumentManagerClient _documentClient;

        public OrganizationAgreementService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            IAgreementService agreementService,
            IPdfService pdfService,
            IRazorConverterService razorConverterService,
            IDocumentManagerClient documentClient)
            : base(context, httpContext)
        {
            _mapper = mapper;
            _agreementService = agreementService;
            _pdfService = pdfService;
            _razorConverterService = razorConverterService;
            _documentClient = documentClient;
        }
    }
}
