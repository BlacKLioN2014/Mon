

document.addEventListener("DOMContentLoaded", () => {
    const loginForm = document.getElementById("loginform");

    loginForm.addEventListener("submit", async (event) => {
        // Prevenir el comportamiento por defecto del formulario

        event.preventDefault();

        // Recoger los datos del formulario
        const formData = new FormData(loginForm);
        const data = {
            NombreUsuario: formData.get("NombreUsuario"),
            Password: formData.get("Password")
        };

        var a = JSON.stringify(data)
        console.log(a)

        try {
            // Enviar los datos al servidor
            const response = await fetch("/Login/Validate", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: a
            });


            const data = await response.json();

            console.log(data)

            // Manejo de la respuesta
            if (data.role == null) {

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
                    title: '"Usuario o contraseña incorrectos"',
                    text: ""
                })

                //alert(result.message || "Usuario o contraseña incorrectos");

            }
            else {

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
                    title: 'Usuario Correcto'
                })
                setTimeout(() => window.location.href = '@Url.Action("ValidaRol","Home")', 2000)

                //alert("Inicio de sesión exitoso");
                //window.location.href = "/Home"; // Redirige al usuario

            }
        }catch (error) {
            console.error("Error:", error);
            alert("Ocurrió un error al intentar iniciar sesión");
        }
    });
});

