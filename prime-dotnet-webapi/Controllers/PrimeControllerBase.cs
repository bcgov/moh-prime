using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Prime.Models.Api;

namespace Prime.Controllers
{
    public class PrimeControllerBase : ControllerBase
    {
        /// <summary>
        /// The default ControllerBase.Ok(value) method. Please use OkResult(value) instead.
        /// </summary>
        public override OkObjectResult Ok([ActionResultObjectValue] object value)
        {
            return base.Ok(value);
        }

        /// <summary>
        /// Wraps the value in an ApiResultResponse object and returns a 200OK.
        /// </summary>
        /// <param name="value"></param>
        public OkObjectResult OkResponse([ActionResultObjectValue] object value)
        {
            return base.Ok(value);
        }
    }
}
