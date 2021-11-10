using AutoMapper;
using System.Linq;

using Prime.DTOs.AgreementEngine;
using Prime.Models;

namespace Prime.ViewModels.Profiles
{
    public class EnrolleeMappingProfile : Profile
    {
        public EnrolleeMappingProfile()
        {
            CreateMap<EnrolleeCreateModel, Enrollee>();

            IQueryable<int> newestAgreementIds = null;
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
                .ForMember(dest => dest.Confirmed, opt => opt.MapFrom(src => src.Submissions.OrderByDescending(s => s.CreatedDate).FirstOrDefault().Confirmed == true));

            CreateMap<Enrollee, EnrolleeDTO>()
                .ForMember(dest => dest.Confirmed, opt => opt.MapFrom(src => src.Submissions.OrderByDescending(s => s.CreatedDate).FirstOrDefault().Confirmed == true))
                .ForMember(dest => dest.HasNewestAgreement, opt => opt.MapFrom(src => newestAgreementIds.Any(id => id == src.CurrentAgreementId)));

            CreateMap<EnrolleeDTO, EnrolleeViewModel>();

            CreateMap<EnrolleeNote, EnrolleeNoteViewModel>();
            CreateMap<EnrolleeAbsence, EnrolleeAbsenceViewModel>();

            CreateMap<Enrollee, AgreementEngineDto>()
                .ForMember(dest => dest.CareSettingCodes, opt => opt.MapFrom(src => src.EnrolleeCareSettings.Select(ecs => ecs.CareSettingCode)));

            // ------- Children -------
            CreateMap<AccessAgreementNote, AccessAgreementNoteViewModel>();
            CreateMap<Certification, CertificationDto>();
            CreateMap<Certification, CertificationViewModel>();
            CreateMap<EnrolleeNote, EnrolleeNoteViewModel>();
            CreateMap<EnrolleeRemoteUser, EnrolleeRemoteUserViewModel>();
            CreateMap<OboSite, OboSiteViewModel>();
            CreateMap<RemoteAccessLocation, RemoteAccessLocationViewModel>();
            CreateMap<RemoteAccessSite, RemoteAccessSiteViewModel>()
                .ForMember(dest => dest.Site, opt => opt.MapFrom(src => src.Site as CommunitySite));
            CreateMap<CommunitySite, RemoteAccessSiteViewModel.SiteViewModel>();
            CreateMap<SiteVendor, RemoteAccessSiteViewModel.VendorViewModel>();
            CreateMap<SelfDeclaration, SelfDeclarationViewModel>()
                .ForMember(dest => dest.Answered, opt => opt.MapFrom(src => true));
            CreateMap<SelfDeclarationDocument, SelfDeclarationDocumentViewModel>();

            CreateMap<EnrolmentStatus, EnrolmentStatusViewModel>();
            CreateMap<EnrolmentStatus, EnrolmentStatusAdminViewModel>();
            CreateMap<Admin, AdminViewModel>();
            CreateMap<EnrolmentStatusReference, EnrolmentStatusReferenceViewModel>();
            CreateMap<EnrolleeNote, EnrolmentStatusReferenceNoteViewModel>();
            CreateMap<EnrolmentStatusReason, EnrolmentStatusReasonViewModel>();
        }
    }
}
