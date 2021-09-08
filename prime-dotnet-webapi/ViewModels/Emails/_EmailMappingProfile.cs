using AutoMapper;

using Prime.Models;
using Prime.ViewModels.Emails;

namespace Prime.ViewModels.Profiles
{
    public class EmailMappingProfile : Profile
    {
        public EmailMappingProfile()
        {
            CreateMap<EmailTemplate, EmailTemplateViewModel>();
            CreateMap<EmailTemplate, EmailTemplateListViewModel>();
        }
    }
}
