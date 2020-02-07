using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Prime.Models.Api
{
    public class ApiBadRequestResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; }

        public ApiBadRequestResponse(ModelStateDictionary modelState)
            : base(StatusCodes.Status400BadRequest)
        {
            if (modelState?.IsValid == true)
            {
                throw new ArgumentException("ModelState must be invalid", nameof(modelState));
            }

            Errors = modelState
                .SelectMany(x => x.Value.Errors)
                .Select(x => x.ErrorMessage);
        }
    }
}
