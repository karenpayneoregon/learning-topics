using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FluentWebApplication.Classes;

#pragma warning disable CS8618
public static class Extensions
{
    public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState, string prefix)
    {

        if (result.IsValid) return;

        foreach (var error in result.Errors)
        {
            var key = string.IsNullOrEmpty(prefix)
                ? error.PropertyName
                : string.IsNullOrEmpty(error.PropertyName)
                    ? prefix
                    : $"{prefix}.{error.PropertyName}";

            modelState.AddModelError(key, error.ErrorMessage);
        }
    }
}