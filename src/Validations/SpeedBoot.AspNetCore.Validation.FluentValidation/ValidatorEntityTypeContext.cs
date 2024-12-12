// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See LICENSE.txt in the project root for license information.

namespace SpeedBoot.AspNetCore.Validation.FluentValidation;

public class ValidatorEntityTypeContext
{
    private readonly CustomConcurrentDictionary<Type, Type> _validatorTypes;

    public ValidatorEntityTypeContext()
    {
        var validatorType = typeof(IValidator);
        var validatorTypes = App.Instance.Services
            .Where(service => service.ServiceType.IsAssignableTo(validatorType))
            .SelectMany(service => service.ServiceType.GenericTypeArguments)
            .ToDictionary(t => t, t => typeof(IValidator<>).MakeGenericType(t));
        _validatorTypes = new CustomConcurrentDictionary<Type, Type>(validatorTypes);
    }

    public bool TryGet(Type type, [NotNullWhen(true)] out Type? validatorType)
        => _validatorTypes.TryGet(type, out validatorType);
}
