using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Prime.Controllers
{
    public class PrimeControllerBase : ControllerBase
    {
        /// <summary>
        /// Sends a StatusCodes.Status400BadRequest response with an ApiMessageResponse body.
        /// </summary>
        /// <param name="message"></param>
        [NonAction]
        public BadRequestObjectResult BadRequest(string message)
        {
            return base.BadRequest(new ApiMessageResponse(message));
        }

        /// <summary>
        /// Sends a StatusCodes.Status409Conflict response with an ApiMessageResponse body.
        /// </summary>
        /// <param name="message"></param>
        [NonAction]
        public ConflictObjectResult Conflict(string message)
        {
            return base.Conflict(new ApiMessageResponse(message));
        }

        /// <summary>
        /// Sends a StatusCodes.Status201Created response with an ApiResultResponse body wrapping the value.
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="routeValues"></param>
        /// <param name="value"></param>
        [NonAction]
        public CreatedAtActionResult CreatedAtAction<T>(string actionName, object routeValues, [ActionResultObjectValue] T value)
        {
            return base.CreatedAtAction(actionName, routeValues, new ApiResultResponse<T>(value));
        }

        /// <summary>
        /// Sends a StatusCodes.Status404NotFound response with an ApiMessageResponse body.
        /// </summary>
        /// <param name="message"></param>
        [NonAction]
        public NotFoundObjectResult NotFound(string message)
        {
            return base.NotFound(new ApiMessageResponse(message));
        }

        /// <summary>
        /// Sends a StatusCodes.Status200OK response with an ApiResultResponse body wrapping the value.
        /// </summary>
        /// <param name="value"></param>
        [NonAction]
        public OkObjectResult Ok<T>([ActionResultObjectValue] T value)
        {
            return base.Ok(new ApiResultResponse<T>(value));
        }
    }

    public class ApiResultResponse<T>
    {
        public T Result { get; }

        public ApiResultResponse(T result)
        {
            Result = result;
        }
    }

    public class ApiMessageResponse
    {
        public string Message { get; }

        public ApiMessageResponse(string message)
        {
            Message = message;
        }
    }
}
