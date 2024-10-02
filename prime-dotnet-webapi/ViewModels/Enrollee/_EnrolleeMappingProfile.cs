using AutoMapper;
using System.Linq;
using System;

using Prime.DTOs.AgreementEngine;
using Prime.Models;
using Prime.Models.Api;
namespace Prime.ViewModels.Profiles
{
    public class EnrolleeMappingProfile : Profile
    {
        public EnrolleeMappingProfile()
        {
            CreateMap<EnrolleeCreateModel, Enrollee>();

            IQueryable<int> newestAgreementIds = null;
            IQueryable<Enrollee> unlinkedPaperEnrolments = null;
            CreateMap<Enrollee, EnrolleeListViewModel>()
                .ForMember(dest => dest.CurrentStatusCode, opt => opt.MapFrom(src => src.CurrentStatus.StatusCode))
                .ForMember(dest => dest.AdjudicatorIdir, opt => opt.MapFrom(src => src.Adjudicator.IDIR))
                .ForMember(dest => dest.HasNewestAgreement, opt => opt.MapFrom(src => newestAgreementIds.Any(id => id == src.CurrentAgreementId)))
                .ForMember(dest => dest.RemoteAccess, opt => opt.MapFrom(src => src.EnrolleeRemoteUsers.Any()))
                .ForMember(dest => dest.CareSettingCodes, opt => opt.MapFrom(src => src.EnrolleeCareSettings.Select(ecs => ecs.CareSettingCode)))
                .ForMember(dest => dest.RequiresConfirmation, opt => opt.MapFrom(src =>
                    !src.Submissions.OrderByDescending(s => s.CreatedDate).FirstOrDefault().Confirmed
                    && src.PreviousStatus.StatusCode == (int)StatusType.RequiresToa
                ))
                .ForMember(dest => dest.LinkedEnrolleeId, opt => opt.MapFrom(src => (src.EnrolleeToPaperLink == null) ? src.PaperToEnrolleeLink.EnrolleeId : src.EnrolleeToPaperLink.PaperEnrolleeId))
                .ForMember(dest => dest.PossiblePaperEnrolmentMatch, opt => opt.MapFrom(src => unlinkedPaperEnrolments.Any(e => e.DateOfBirth.Date == src.DateOfBirth.Date)))
                .ForMember(dest => dest.Confirmed, opt => opt.MapFrom(src => src.Submissions.OrderByDescending(s => s.CreatedDate).FirstOrDefault().Confirmed == true));

            CreateMap<Enrollee, EnrolleeDTO>()
                .ForMember(dest => dest.Confirmed, opt => opt.MapFrom(src => src.Submissions.OrderByDescending(s => s.CreatedDate).FirstOrDefault().Confirmed == true))
                .ForMember(dest => dest.LinkedEnrolleeId, opt => opt.MapFrom(src => (src.EnrolleeToPaperLink == null) ? src.PaperToEnrolleeLink.EnrolleeId : src.EnrolleeToPaperLink.PaperEnrolleeId))
                .ForMember(dest => dest.PossiblePaperEnrolmentMatch, opt => opt.MapFrom(src => (src.GPID != null && src.GPID.Contains(Enrollee.PaperGpidPrefix)) ? false : unlinkedPaperEnrolments.Any(e => e.DateOfBirth.Date == src.DateOfBirth.Date)))
                .ForMember(dest => dest.HasNewestAgreement, opt => opt.MapFrom(src => newestAgreementIds.Any(id => id == src.CurrentAgreementId)))
                .ForMember(dest => dest.HasDeviceProviderCareSetting, opt => opt.MapFrom(src => src.HasCareSetting(CareSettingType.DeviceProvider)));

            CreateMap<EnrolleeDTO, EnrolleeViewModel>();

            CreateMap<EnrolleeNote, EnrolleeNoteViewModel>();

            CreateMap<EnrolleeAbsence, EnrolleeAbsenceViewModel>()
                .ForMember(dest => dest.StartTimestamp, opt => opt.MapFrom(src => src.StartTimestamp.ToLocalTime()))
                .ForMember(dest => dest.EndTimestamp, opt =>
                {
                    opt.PreCondition(src => src.EndTimestamp != null);
                    opt.MapFrom(src => src.EndTimestamp.Value.ToLocalTime());
                });

            CreateMap<Enrollee, AgreementEngineDto>()
                .ForMember(dest => dest.CareSettingCodes, opt => opt.MapFrom(src => src.EnrolleeCareSettings.Select(ecs => ecs.CareSettingCode)));

            // ------- Children -------
            CreateMap<AccessAgreementNote, AccessAgreementNoteViewModel>();
            CreateMap<Certification, CertificationDto>();
            CreateMap<Certification, CertificationViewModel>();
            CreateMap<EnrolleeRemoteUser, EnrolleeRemoteUserViewModel>();
            CreateMap<EnrolleeDeviceProvider, EnrolleeDeviceProviderViewModel>();
            CreateMap<OboSite, OboSiteViewModel>();
            CreateMap<RemoteAccessLocation, RemoteAccessLocationViewModel>();
            CreateMap<RemoteAccessSite, RemoteAccessSiteViewModel>()
                .ForMember(dest => dest.Site, opt => opt.MapFrom(src => src.Site as CommunitySite));
            CreateMap<CommunitySite, RemoteAccessSiteViewModel.SiteViewModel>();
            CreateMap<SiteVendor, RemoteAccessSiteViewModel.VendorViewModel>();
            CreateMap<SelfDeclaration, SelfDeclarationViewModel>()
                .ForMember(dest => dest.Answered, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.SortingNumber, opt => opt.MapFrom(src => src.SelfDeclarationType.SortingNumber));

            CreateMap<RemoteAccessSiteUpdateModel, RemoteAccessSite>();
            CreateMap<SelfDeclarationDocument, SelfDeclarationDocumentViewModel>();

            CreateMap<EnrolmentStatus, EnrolmentStatusViewModel>();
            CreateMap<EnrolmentStatus, EnrolmentStatusAdminViewModel>();
            CreateMap<Admin, AdminViewModel>();
            CreateMap<EnrolmentStatusReference, EnrolmentStatusReferenceViewModel>();
            CreateMap<EnrolleeNote, EnrolmentStatusReferenceNoteViewModel>();
            CreateMap<EnrolmentStatusReason, EnrolmentStatusReasonViewModel>();

            CreateMap<License, LicenseViewModel>()
                .IncludeMembers(l => l.CurrentLicenseDetail);
            CreateMap<LicenseDetail, LicenseViewModel>();

            CreateMap<HpdidLookup, EnrolleeLookup>();
            CreateMap<HpdidLookup, GpidDetailLookup>();

            CreateMap<DeviceProviderSite, DeviceProviderSiteViewModel>();

            CreateMap<UnlistedCertification, UnlistedCertificationViewModel>();
        }
    }
}
