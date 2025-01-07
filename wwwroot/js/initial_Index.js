// URL del controlador que proporciona la lista
const apiUrl = '/GetPokemon'; // Cambia esta URL por la de tu controlador

// Función para inicializar la tabla
async function loadBootstrapTable() {
    try {
        // Llamada a la API
        const response = await fetch(apiUrl);

        // Verificar si la respuesta fue exitosa
        if (!response.ok) {
            throw new Error(`Error al obtener los datos: ${response.status}`);
        }

        // Convertir la respuesta a JSON
        const result = await response.json();

        // Verifica si la tabla ya está inicializada
        if ($('#Pokemon-table').data('bootstrap.table')) {
            console.log("if")
            // Si ya está inicializada, usa 'load'
            $('#Pokemon-table').bootstrapTable('load', result);
        } else {
            console.log("else")
            // Inicializar Bootstrap Table con los datos
            $('#Pokemon-table').bootstrapTable({
                data: result
            });
        }

    } catch (error) {
        console.error('Error al cargar la tabla:', error);
    }
}

// Llamar a la función al cargar la página
document.addEventListener('DOMContentLoaded', loadBootstrapTable);