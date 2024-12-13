using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Mon.Models
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var password = value as string;
            if (string.IsNullOrEmpty(password))
                return false;

            // Validar si tiene al menos una letra minúscula
            if (!Regex.IsMatch(password, @"[a-z]"))
                return false;

            // Validar si tiene al menos una letra mayúscula
            if (!Regex.IsMatch(password, @"[A-Z]"))
                return false;

            // Validar si tiene al menos un carácter no alfanumérico
            if (!Regex.IsMatch(password, @"[\W_]"))
                return false;

            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return "La contraseña debe contener al menos una letra mayúscula, una letra minúscula y un carácter especial.";
        }
    }
}
