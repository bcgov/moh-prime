using System.Text.RegularExpressions;
using AutoMapper;

using Prime.Models;
using Prime.ViewModels.Emails;

namespace Prime.ViewModels.Profiles
{
    public class EmailMappingProfile : Profile
    {
        public EmailMappingProfile()
        {
            CreateMap<EmailTemplate, EmailTemplateViewModel>()
                .ForMember(dest => dest.TemplateName, opt => opt.MapFrom(src => Regex.Replace(src.EmailType.ToString(), "(\\B([A-Z])[a-z])", " $1")));
            CreateMap<EmailTemplate, EmailTemplateListViewModel>()
                .ForMember(dest => dest.TemplateName, opt => opt.MapFrom(src => Regex.Replace(src.EmailType.ToString(), "(\\B([A-Z])[a-z])", " $1")));
        }
    }
}
