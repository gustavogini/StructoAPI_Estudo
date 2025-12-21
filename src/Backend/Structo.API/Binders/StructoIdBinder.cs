using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sqids;

namespace Structo.API.Binders
{
    public class StructoIdBinder : IModelBinder
    {
        private readonly SqidsEncoder<long> _idEncoder;

        public StructoIdBinder(SqidsEncoder<long> idEncoder)
        {
            _idEncoder = idEncoder;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var modelName = bindingContext.ModelName;

            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var valueAsString = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(valueAsString))
            {
                return Task.CompletedTask;
            }

            var id = _idEncoder.Decode(valueAsString).Single();

            bindingContext.Result = ModelBindingResult.Success(id);

            return Task.CompletedTask;

        }
    }
}
