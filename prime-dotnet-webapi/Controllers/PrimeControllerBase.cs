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
        /// Sends a StatusCodes.Status201Created response with an ApiResultResponse body wrapping the value.
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="routeValues"></param>
        /// <param name="value"></param>
        public CreatedAtActionResult CreatedAtAction<T>(string actionName, object routeValues, [ActionResultObjectValue] T value)
        {
            return base.CreatedAtAction(actionName, routeValues, ApiResponse.Result(value));
        }

        /// <summary>
        /// Sends a StatusCodes.404NotFound response with an ApiMessageResponse body.
        /// </summary>
        /// <param name="message"></param>
        public NotFoundObjectResult NotFound(string message)
        {
            return base.NotFound(ApiResponse.Message(message));
        }

        /// <summary>
        /// Sends a StatusCodes.200OK response with an ApiResultResponse body wrapping the value.
        /// </summary>
        /// <param name="value"></param>
        public OkObjectResult Ok<T>([ActionResultObjectValue] T value)
        {
            return base.Ok(ApiResponse.Result(value));
        }
    }
}
