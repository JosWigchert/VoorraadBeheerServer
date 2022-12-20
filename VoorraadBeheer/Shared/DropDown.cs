using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Reflection;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace VoorraadBeheer.Shared
{
    public class DropDown<TValue> : InputSelect<TValue>
    {

        [Parameter] public EventCallback<Object> SelectChanged { get; set; }

        protected override bool TryParseValueFromString(string value, out TValue result,
            out string validationErrorMessage)
        {
            if (typeof(TValue) == typeof(int) || typeof(TValue) == typeof(int?))
            {
                if (int.TryParse(value, out var resultInt))
                {
                    result = (TValue)(object)resultInt;
                    //object model = FieldIdentifier.Model;
                    //if (model.GetType().FullName.StartsWith("MatchAssDB"))
                    //{
                    //    Type modeltype = model.GetType();
                    //    System.Reflection.PropertyInfo property = modeltype.GetProperty(this.FieldIdentifier.FieldName);
                    //    property.SetValue(model, resultInt);
                    //}

                    validationErrorMessage = null;

                    //if (model.GetType().Name == "Profile")
                    //{
                    //    ValidationMessageStore validationMessageStore = new ValidationMessageStore(this.EditContext);
                    //    ValidateField(this.EditContext, validationMessageStore);

                    //}
                    SelectChanged.InvokeAsync(resultInt);
                    return true;
                }
                else
                {
                    result = default;
                    validationErrorMessage =
                        $"The selected value {value} is not a valid number.";
                    return false;
                }
            }
            else if (typeof(TValue) == typeof(decimal) || typeof(TValue) == typeof(decimal?))
            {
                if (decimal.TryParse(value, out var resultDec))
                {
                    result = (TValue)(object)resultDec;
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    result = default;
                    validationErrorMessage =
                        $"The selected value {value} is not a valid number.";
                    return false;
                }

            }
            else if (typeof(TValue) == typeof(bool) || typeof(TValue) == typeof(bool?))
            {
                if (bool.TryParse(value, out var resultBool))
                {
                    result = (TValue)(object)resultBool;
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    result = default;
                    validationErrorMessage =
                        $"The selected value {value} is not a valid number.";
                    return false;
                }

            }
            else
            {
                return base.TryParseValueFromString(value, out result,
                    out validationErrorMessage);
            }
        }


        //private IValidator GetValidatorForModel(object model)
        //{
        //    var abstractValidatorType = typeof(AbstractValidator<>).MakeGenericType(model.GetType());
        //    var modelValidatorType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.IsSubclassOf(abstractValidatorType));
        //    var modelValidatorInstance = (IValidator)Activator.CreateInstance(modelValidatorType);

        //    return modelValidatorInstance;
        //}

        //private void ValidateField(EditContext editContext, ValidationMessageStore messages)
        //{
        //    var context = new ValidationContext<Profile>((Profile)editContext.Model);

        //    var validator = GetValidatorForModel(editContext.Model);
        //    var validationResults = validator.Validate(context);

        //    messages.Clear(this.FieldIdentifier);
        //    IEnumerable<string> errors = validationResults.Errors.Where(p => p.PropertyName == this.FieldIdentifier.FieldName).Select(error => error.ErrorMessage);
        //    foreach (var errmsg in errors)
        //    {
        //        messages.Add(FieldIdentifier, errmsg);
        //    }

        //    editContext.NotifyValidationStateChanged();
        //}
    }

}