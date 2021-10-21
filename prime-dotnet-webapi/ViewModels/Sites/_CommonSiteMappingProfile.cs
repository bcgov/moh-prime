using System.Linq;
using AutoMapper;

using Prime.Models;
using Prime.ViewModels.Sites;

namespace Prime.ViewModels.Profiles
{
    public class CommonSiteMappingProfile : Profile
    {
        public CommonSiteMappingProfile()
        {
            CreateMap<BusinessDay, BusinessDayViewModel>();
            CreateMap<RemoteUser, RemoteUserViewModel>();
        }
    }
}
