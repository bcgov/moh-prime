using System.Threading.Tasks;

namespace Prime.Services
{
    public interface IRazorConverterService
    {
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}
