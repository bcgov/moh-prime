using System.Threading.Tasks;
using Prime.Services.Razor;

namespace Prime.Services
{
    public interface IRazorConverterService
    {
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);

        Task<string> RenderViewToStringAsync<TModel>(RazorRenderingPackage<TModel> razorView);
    }
}
