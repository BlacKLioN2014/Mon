using System;
using System.ComponentModel.DataAnnotations;

namespace Mon.Models.Dtos
{
    public class UserRegistroDto
    {
        [Required(ErrorMessage = "El campo Usuario es obligatorio")]
        [MinLength(5, ErrorMessage = "El campo Usuario debe tener al menos {1} caracteres")]
        [MaxLength(10, ErrorMessage = "El campo Usuario no debe exceder los {1} caracteres")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [MaxLength(25, ErrorMessage = "El campo Nombre no debe exceder los {1} caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es obligatorio")]
        [MinLength(5, ErrorMessage = "El campo Contraseña debe tener al menos {1} caracteres")]
        [MaxLength(15, ErrorMessage = "El campo Contraseña no debe exceder los {1} caracteres")]
        //[RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "El campo Contraseña solo puede contener letras y números")]
        [PasswordValidation(ErrorMessage = "La contraseña debe contener al menos una letra mayúscula, una letra minúscula y un carácter especial.")]
        public string Passsword { get; set; }

        [Required(ErrorMessage = "El campo confirmar contraseña es obligatorio")]
        [Compare("Passsword", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmarPasssword { get; set; }

        [Required(ErrorMessage = "El campo Rol es obligatorio")]
        public string Role { get; set; }

        [Required(ErrorMessage = "El campo Correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El campo Correo no es una dirección de correo válida")]
        public string Correo { get; set; }
    }
}


#region utilidades

//Lista de Validaciones Comunes
//[Required]

//Hace que el campo sea obligatorio.
//csharp
//Copiar código
//[Required(ErrorMessage = "El campo {0} es obligatorio")]
//public string NombreUsuario { get; set; }
//[StringLength]

//Define la longitud mínima y máxima de una cadena.
//csharp
//Copiar código
//[StringLength(50, MinimumLength = 3, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
//public string NombreUsuario { get; set; }
//[MinLength] y[MaxLength]

//Define solo la longitud mínima o máxima.
//csharp
//Copiar código
//[MinLength(5, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres")]
//[MaxLength(100, ErrorMessage = "El campo {0} no debe exceder los {1} caracteres")]
//public string Descripcion { get; set; }
//[RegularExpression]

//Valida contra una expresión regular.
//csharp
//Copiar código
//[RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "El campo {0} solo puede contener letras y números")]
//public string NombreUsuario { get; set; }
//[Range]

//Valida que un número esté dentro de un rango específico.
//csharp
//Copiar código
//[Range(1, 100, ErrorMessage = "El campo {0} debe estar entre {1} y {2}")]
//public int Edad { get; set; }
//[EmailAddress]

//Valida que el valor sea una dirección de correo electrónico válida.
//csharp
//Copiar código
//[EmailAddress(ErrorMessage = "El campo {0} no es una dirección de correo válida")]
//public string Correo { get; set; }
//[Phone]

//Valida que el valor sea un número de teléfono válido.
//csharp
//Copiar código
//[Phone(ErrorMessage = "El campo {0} no es un número de teléfono válido")]
//public string Telefono { get; set; }
//[Url]

//Valida que el valor sea una URL válida.
//csharp
//Copiar código
//[Url(ErrorMessage = "El campo {0} no es una URL válida")]
//public string SitioWeb { get; set; }
//[CreditCard]

//Valida que el valor sea un número de tarjeta de crédito válido.
//csharp
//Copiar código
//[CreditCard(ErrorMessage = "El campo {0} no es un número de tarjeta de crédito válido")]
//public string TarjetaCredito { get; set; }
//[Compare]

//Compara dos propiedades del modelo para asegurarse de que sean iguales (por ejemplo, contraseñas).
//csharp
//Copiar código
//[Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
//public string ConfirmarPassword { get; set; }
//[DataType]

//Especifica un tipo de datos para una representación específica (por ejemplo, fecha, moneda).
//csharp
//Copiar código
//[DataType(DataType.Date)]
//public DateTime FechaNacimiento { get; set; }
//[CustomValidation]

//Permite crear validaciones personalizadas.
//csharp
//Copiar código
//[CustomValidation(typeof(MiClaseDeValidacion), "MetodoDeValidacion")]
//public string MiCampo { get; set; }
//[Range] para fechas

//Usada para validar rangos de fechas:
//csharp
//Copiar código
//[Range(typeof(DateTime), "2023-01-01", "2024-01-01", ErrorMessage = "La fecha debe estar entre {1} y {2}")]
//public DateTime Fecha { get; set; }
#endregion