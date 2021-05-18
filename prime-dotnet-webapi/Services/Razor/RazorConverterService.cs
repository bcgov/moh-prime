using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using RazorEngine;
using RazorEngine.Templating;

using Prime.Services.Razor;
using Prime.Models;

namespace Prime.Services
{
    public class RazorConverterService : IRazorConverterService
    {
        private readonly IRazorViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IEmailTemplateService _emailTemplateService;

        public RazorConverterService(
            IRazorViewEngine viewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider,
            IEmailTemplateService emailTemplateService,
            IHttpContextAccessor contextAccessor)
        {
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
            _contextAccessor = contextAccessor;
            _emailTemplateService = emailTemplateService;
        }

        public async Task<string> RenderTemplateToStringAsync<TModel>(RazorTemplate<TModel> template, TModel viewModel)
        {
            var actionContext = GetActionContext();
            var view = GetView(actionContext, template.ViewPath);

            using var output = new StringWriter();
            var viewContext = new ViewContext(
                actionContext,
                view,
                new ViewDataDictionary<TModel>(
                    metadataProvider: new EmptyModelMetadataProvider(),
                    modelState: new ModelStateDictionary())
                {
                    Model = viewModel
                },
                new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                output,
                new HtmlHelperOptions());

            await view.RenderAsync(viewContext);

            return output.ToString();
        }

        public string RenderStringTemplateToString<TModel>(string template, TModel model)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                return Engine.Razor.RunCompile(template, guid.ToString(), typeof(TModel), model);
            }
            catch (TemplateCompilationException ex)
            {
                // TODO: This is the error thrown then @Model.X and X doesn't exist in the model
                throw ex;
            }

        }

        public async Task<string> RenderEmailTemplateToString<TModel>(EmailTemplateType type, TModel viewModel)
        {
            var emailTemplate = await _emailTemplateService.GetEmailTemplateByTypeAsync(type);
            return RenderStringTemplateToString(emailTemplate.Template, viewModel);
        }

        private IView GetView(ActionContext actionContext, string viewName)
        {
            var getViewResult = _viewEngine.GetView(executingFilePath: null, viewPath: viewName, isMainPage: true);
            if (getViewResult.Success)
            {
                return getViewResult.View;
            }

            var findViewResult = _viewEngine.FindView(actionContext, viewName, isMainPage: true);
            if (findViewResult.Success)
            {
                return findViewResult.View;
            }

            var searchedLocations = getViewResult.SearchedLocations.Concat(findViewResult.SearchedLocations);
            var errorMessage = string.Join(
                Environment.NewLine,
                new[] { $"Unable to find view '{viewName}'. The following locations were searched:" }.Concat(searchedLocations));

            throw new InvalidOperationException(errorMessage);
        }

        private ActionContext GetActionContext()
        {
            var httpContext = new DefaultHttpContext
            {
                RequestServices = _serviceProvider
            };

            return new ActionContext(httpContext, _contextAccessor.HttpContext.GetRouteData(), new ActionDescriptor());
        }
    }
}
