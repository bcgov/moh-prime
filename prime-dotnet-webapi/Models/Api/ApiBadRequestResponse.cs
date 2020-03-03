using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Prime.Models.Api
{
    public class ApiBadRequestResponse
    {
        public IEnumerable<string> Errors { get; }

        public ApiBadRequestResponse(ModelStateDictionary modelState)
        {
            if (modelState == null || modelState.IsValid)
            {
                throw new ArgumentException("ModelState must have errors", nameof(modelState));
            }

            Errors = modelState
                .SelectMany(x => x.Value.Errors)
                .Select(x => x.ErrorMessage);
        }
    }
}
