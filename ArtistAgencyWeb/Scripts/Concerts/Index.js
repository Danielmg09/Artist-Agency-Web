$(document).ready(function () {

})

function LoadDetailsView(Id) {
    $.get('Concerts/Details', {
        id: Id
    }).done(function (data) {
        $('#details').html(data);
    });
}

function LoadDeleteView(Id) {
    $.get('Concerts/Delete', {
        id: Id
    }).done(function (data) {
        $('#details').html(data);
    });
}

function LoadEditView(Id) {
    $.get('Concerts/Edit', {
        id: Id
    }).done(function (data) {
        $('#details').html(data);
    });
}

function LoadAssignView(Id) {
    $.get('Concerts/Assign', {
        id: Id
    }).done(function (data) {
        $('#details').html(data);
    });
}

function SearchConcert() {
    $.get('Concerts/FilterByText', {
        text: $('#textoABuscar').val()
    }).done(function (data) {
        $('#Filtered').html(data);
    });

}