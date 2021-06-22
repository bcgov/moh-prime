using System;
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface IBannerService
    {
        Task<BannerViewModel> GetBannerAsync(BannerLocationCode locationCode);
        Task<BannerDisplayViewModel> GetActiveBannerAsync(BannerLocationCode locationCode, DateTime atTime);
        Task<BannerViewModel> SetBannerAsync(BannerLocationCode locationCode, BannerViewModel updateModel);
        Task DeleteBannerAsync(BannerLocationCode locationCode);
    }
}
