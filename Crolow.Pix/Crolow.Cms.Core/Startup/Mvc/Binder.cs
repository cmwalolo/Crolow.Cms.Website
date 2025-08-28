using Microsoft.AspNetCore.Mvc.ModelBinding;


public class CsvArrayBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;

        if (string.IsNullOrWhiteSpace(value))
        {
            bindingContext.Result = ModelBindingResult.Success(Array.CreateInstance(
                bindingContext.ModelType.GetElementType() ?? typeof(string), 0));
            return Task.CompletedTask;
        }

        var elementType = bindingContext.ModelType.GetElementType() ?? typeof(string);
        var items = value.Split(',', StringSplitOptions.RemoveEmptyEntries)
                         .Select(v => ConvertValue(v, elementType))
                         .ToArray();

        var typedArray = Array.CreateInstance(elementType, items.Length);
        items.CopyTo(typedArray, 0);

        bindingContext.Result = ModelBindingResult.Success(typedArray);
        return Task.CompletedTask;
    }

    private object? ConvertValue(string value, Type targetType)
    {
        try
        {
            if (targetType == typeof(string)) return value;
            return Convert.ChangeType(value, targetType);
        }
        catch
        {
            return null;
        }
    }
}
