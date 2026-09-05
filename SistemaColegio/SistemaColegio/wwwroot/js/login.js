document.getElementById("formLogin").addEventListener("submit", async function (event) {

    event.preventDefault();

    const nombreUsuario = document.getElementById("nombreUsuario").value;
    const password = document.getElementById("password").value;

    const mensajeError = document.getElementById("mensajeError");

    mensajeError.classList.add("d-none");
    mensajeError.textContent = "";

    try {

        const response = await fetch("/api/Auth/login", {

            method: "POST",

            headers: {
                "Content-Type": "application/json"
            },

            body: JSON.stringify({
                nombreUsuario: nombreUsuario,
                password: password
            })

        });

        if (response.ok) {

            const usuario = await response.json();

            console.log("Login exitoso:", usuario);

            // Por ahora solamente mostramos la información
            // para comprobar que la autenticación funciona.
            alert(`Bienvenido ${ usuario.nombres } ${ usuario.apellidos } `);

        }
        else if (response.status === 401) {

            const error = await response.json();

            mensajeError.textContent =
                error.mensaje || "Usuario o contraseña incorrectos.";

            mensajeError.classList.remove("d-none");

        }
        else {

            mensajeError.textContent =
                "Ocurrió un error al intentar iniciar sesión.";

            mensajeError.classList.remove("d-none");
        }

    }
    catch (error) {

        console.error("Error:", error);

        mensajeError.textContent = "No se pudo conectar con el servidor.";

        mensajeError.classList.remove("d-none");
    }

});

