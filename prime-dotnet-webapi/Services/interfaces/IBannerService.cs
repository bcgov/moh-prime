using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface IBannerService
    {
        Task<Banner> GetBannerAsync(int bannerId);
        Task<IEnumerable<Banner>> GetBannersAsync();
        Task<Banner> GetActiveBannerByLocationAsync(BannerLocationCode location);
        Task<Banner> CreateBannerAsync(Banner banner);
        Task RemoveBannerAsync(int bannerId);
        Task<Banner> UpdateBannerAsync(int bannerId, BannerUpdateViewModel updateModel);

    }
}
