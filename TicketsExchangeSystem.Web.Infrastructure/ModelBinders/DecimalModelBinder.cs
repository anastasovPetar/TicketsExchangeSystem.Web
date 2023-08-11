namespace TicketsExchangeSystem.Web.Infrastructure.ModelBinders
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.Globalization;
    using System.Threading.Tasks;

    public class DecimalModelBinder : IModelBinder

    {
        public Task BindModelAsync(ModelBindingContext? bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            ValueProviderResult result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (result != ValueProviderResult.None && !string.IsNullOrWhiteSpace(result.FirstValue))
            {
                decimal parsedValue = 0m;
                bool parseSuccess = false;

                try
                {
                    string valueFromForm = result.FirstValue;
                    valueFromForm = valueFromForm.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    valueFromForm = valueFromForm.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

                    parsedValue = Convert.ToDecimal(valueFromForm);
                    parseSuccess = true;
                }
                catch (FormatException e)
                {

                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);

                    if (parseSuccess)
                    {
                        bindingContext.Result = ModelBindingResult.Success(parsedValue);
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
