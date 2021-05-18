using System.Threading.Tasks;
using Prime.Models;
using Prime.Services.Razor;

namespace Prime.Services
{
    public interface IRazorConverterService
    {
        Task<string> RenderTemplateToStringAsync<TModel>(RazorTemplate<TModel> template, TModel viewModel);
        string RenderStringTemplateToString<TModel>(string template, TModel viewModel);
        Task<string> RenderEmailTemplateToString<TModel>(EmailTemplateType type, TModel viewModel);
    }
}
