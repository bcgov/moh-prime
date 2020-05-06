using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;
using FakeItEasy;

using Prime;
using Prime.Models;
using Prime.Services;
using PrimeTests.Utils;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;

namespace PrimeTests.Services
{
    public class RazorConverterServiceTests
    {
        public static RazorConverterService CreateService(
            IRazorViewEngine viewEngine = null,
            ITempDataProvider tempDataProvider = null,
            IServiceProvider serviceProvider = null,
            IHttpContextAccessor contextAccessor = null)
        {
            return new RazorConverterService(
                viewEngine ?? A.Fake<IRazorViewEngine>(),
                tempDataProvider ?? A.Fake<ITempDataProvider>(),
                serviceProvider ?? A.Fake<IServiceProvider>(),
                contextAccessor ?? A.Fake<IHttpContextAccessor>()
            );
        }
    }
}

// namespace Prime.Services
// {
//     public interface IRazorConverterService
//     {
//         Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
//     }
// }
