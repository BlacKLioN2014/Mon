document.addEventListener("DOMContentLoaded", () => {
    const RegisterForm = document.getElementById("Registerform");

    RegisterForm.addEventListener("submit", async (event) => {
        // Prevenir el comportamiento por defecto del formulario

        event.preventDefault();

        // Ejecutar la validación del formulario de jQuery Unobtrusive
        if (!$("#Registerform").valid()) {
            console.log("validaciones asp")
            return; // Si la validación falla, no proceder
        }

        // Recoger los datos del formulario
        const formData = new FormData(RegisterForm);
        const data = {
            NombreUsuario: formData.get("NombreUsuario"),
            Nombre: formData.get("Nombre"),
            Passsword: formData.get("Passsword"),
            ConfirmarPasssword: formData.get("ConfirmarPasssword"),
            Correo: formData.get("Correo"),
            Role: formData.get("Role")
        };

        var a = JSON.stringify(data)
        console.log(data)

        try {

            // Enviar los datos al servidor
            const response = await fetch("/Login/Registrar_Usuario", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: a
            });

            const data = await response.json();

            console.log(data)

            if (!data.success)
            {
                const Toast = Swal.mixin({
                    toast: true,
                    position: 'top-end',
                    showConfirmButton: false,
                    timer: 3000,
                    timerProgressBar: true,
                    didOpen: (toast) => {
                        toast.addEventListener('mouseenter', Swal.stopTimer)
                        toast.addEventListener('mouseleave', Swal.resumeTimer)
                    }
                })
                Toast.fire({
                    icon: 'error',
                    title: 'Ocurrio un error',
                    text: data.message
                })
            }
            else
            {

                const Toast = Swal.mixin({
                    toast: true,
                    position: 'top-end',
                    showConfirmButton: false,
                    timer: 3000,
                    timerProgressBar: true,
                    didOpen: (toast) => {
                        toast.addEventListener('mouseenter', Swal.stopTimer)
                        toast.addEventListener('mouseleave', Swal.resumeTimer)
                    }
                })
                Toast.fire({
                    icon: 'success',
                    title: 'Registro correcto',
                    text: data.message
                })
                setTimeout(() => window.location.href = '@Url.Action("Login","Validate")', 2000)

                //alert("Inicio de sesión exitoso");
                //window.location.href = "/Home"; // Redirige al usuario

            }

        } catch (error) {
            console.error("Error:", error);
            alert("Ocurrio un error intenta de nuevo mas tarde");
        }
    });
});