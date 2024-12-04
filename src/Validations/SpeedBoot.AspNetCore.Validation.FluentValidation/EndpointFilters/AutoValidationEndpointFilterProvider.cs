// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore.Validation.FluentValidation.EndpointFilters;

public class AutoValidationEndpointFilterProvider : EndpointFilterProviderBase
{
    private readonly IOptions<JsonOptions> _jsonOptions;
    private readonly ValidatorEntityTypeContext _validatorEntityTypeContext;

    public AutoValidationEndpointFilterProvider(ValidatorEntityTypeContext validatorEntityTypeContext, IOptions<JsonOptions> jsonOptions)
    {
        _jsonOptions = jsonOptions;
        _validatorEntityTypeContext = validatorEntityTypeContext;
    }

    public override async ValueTask<object?> HandlerAsync(EndpointFilterInvocationContext invocationContext, EndpointFilterDelegate next)
    {
        for (var i = 0; i < invocationContext.Arguments.Count; i++)
        {
            var argument = invocationContext.Arguments[i];

            var type = argument?.GetType();
            if (type is null || !_validatorEntityTypeContext.TryGet(type, out var validatorType))
                continue;

            if (invocationContext.HttpContext.RequestServices.GetService(validatorType) is not IValidator validator)
                continue;

            var validationResult =
                await validator.ValidateAsync(new ValidationContext<object?>(argument), invocationContext.HttpContext.RequestAborted);

            if (validationResult.IsValid)
                continue;

            var data = validationResult.ToDictionary();

            if (_jsonOptions.Value.SerializerOptions.PropertyNamingPolicy != null)
            {
                data = data.ToDictionary(x => _jsonOptions.Value.SerializerOptions.PropertyNamingPolicy.ConvertName(x.Key), x => x.Value);
            }

            return Results.ValidationProblem(data);
        }

        return await next(invocationContext);
    }
}
