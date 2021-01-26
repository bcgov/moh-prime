using System.Threading.Tasks;
using Prime.Services.Razor;

namespace Prime.Services
{
    public interface IRazorConverterService
    {
        Task<string> RenderTemplateToStringAsync<TModel>(RazorTemplate<TModel> template, TModel viewModel);
    }
}
