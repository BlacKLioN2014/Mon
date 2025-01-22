// URL del controlador que proporciona la lista
const apiUrl = '/GetPokemon'; // Cambia esta URL por la de tu controlador

var $table = $('#Pokemon-table')

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
                data: result,
                //columns: [{
                //    field: 'id',
                //    title: 'ID',
                //    sortable: true
                //}, {
                //    field: 'nombre',
                //    title: 'Nombre',
                //    sortable: true
                //}, {
                //    field: 'url',
                //    title: 'Url'
                //}, {
                //    field: 'operate',
                //    title: 'Eventos',
                //    formatter: operateFormatter, // Se asigna el formatter
                //    events: operateEvents // Se asignan los eventos
                //}]
            });
        }

    } catch (error) {
        console.error('Error al cargar la tabla:', error);
    }
}

// Llamar a la función al cargar la página
document.addEventListener('DOMContentLoaded', loadBootstrapTable);

function operateFormatter(value, row, index) {
    return [
        '<a class="like" href="javascript:void(0)" title="Ver informacion">',
        '<i class="fas fa-eye" style="color:mediumblue;"></i>',
        '</a>  ',
        '<a class="remove" href="javascript:void(0)" title="Remover">',
        '<i class="fas fa-eye-slash" style="color:mediumblue;"></i>',
        '</a>'
    ].join('')
}

window.operateEvents = {
    'click .like': function (e, value, row, index) {
        alert('You click like action, row: ' + JSON.stringify(row))
    },
    'click .remove': function (e, value, row, index) {
        $table.bootstrapTable('remove', {
            field: 'id',
            values: [row.id]
        })
    }
}