﻿

using Pancake.Attribute;
using PancakeEditor.Attribute;

[assembly: RegisterAttributeValidator(typeof(ValidateInputValidator))]

namespace PancakeEditor.Attribute
{
    public class ValidateInputValidator : AttributeValidator<ValidateInputAttribute>
    {
        private ValueResolver<ValidationResult> _resolver;

        public override ExtensionInitializationResult Initialize(PropertyDefinition propertyDefinition)
        {
            base.Initialize(propertyDefinition);

            _resolver = ValueResolver.Resolve<ValidationResult>(propertyDefinition, Attribute.Method);

            if (_resolver.TryGetErrorString(out var error))
            {
                return error;
            }

            return ExtensionInitializationResult.Ok;
        }

        public override ValidationResult Validate(Property property)
        {
            if (_resolver.TryGetErrorString(out var error))
            {
                return ValidationResult.Error(error);
            }

            return _resolver.GetValue(property);
        }
    }
}