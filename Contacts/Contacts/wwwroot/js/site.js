// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function () {
    $('#table').DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "lengthMenu": [5, 10, 25, 50, 100],
        "oLanguage": {
            "sEmptyTable": "Nenhum registro encontrado",
            "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrar 0 até 0 de 0 Registros",
            "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Mostrar _MENU_ registros por página",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Próximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Último"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            },
        }
    });

    $('.close-alert').click(function () {
        $('.alert').hide('hide')
    })

    $('.close-details').click(function () {
        $('.modal').modal('hide')
    })
});

// Método chamado na listagem para exibir os dados do contato numa modal
function ShowDetailsModal(id) {
    let request = new Request(`/Contacts/Details/${id}`)

    fetch(request)
        .then(response => response.json())
        .then(data => {
            let html = "<div>";
            html += "<span>"
            html += `<strong>Id</strong>: ${data.id}`
            html += "</span>"
            html += "<br />"
            html += "<span>"
            html += `<strong>Nome</strong>: ${data.name}`
            html += "</span>"
            html += "<br />"
            html += "<span>"
            html += `<strong>E-mail</strong>: ${data.email}`
            html += "</span>"
            html += "<br />"
            html += "<span>"
            html += `<strong>Telefone</strong>: ${data.phone}`
            html += "</span>"
            html += "</div>"
            document.getElementById('contact-content').innerHTML = html
            $('.modal').modal('show')
        })
        .catch((error) => console.log(error))
}