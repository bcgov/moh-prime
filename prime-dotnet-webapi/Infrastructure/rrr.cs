
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Prime.Infrastructure
{
    public class SubmissionActionEntityBinder : IModelBinder
    {
        // private readonly ApiDbContext _context;

        // public SubmissionActionEntityBinder(ApiDbContext context)
        // {
        //     _context = context;
        // }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;

            // Try to fetch the value of the argument by name
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;

            // Check if the argument value is null or empty
            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            if (!int.TryParse(value, out var id))
            {
                // Non-integer arguments result in model state errors
                bindingContext.ModelState.TryAddModelError(
                    modelName, "Author Id must be an integer.");

                return Task.CompletedTask;
            }




            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}
