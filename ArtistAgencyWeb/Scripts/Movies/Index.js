$(document).ready(function () {
   
})

function LoadDetailsView(Id) {
    $.get('Movies/Details', {
        id: Id
    }).done(function (data) {
        $('#details').html(data);
    });
}

function LoadDeleteView(Id) {
    $.get('Movies/Delete', {
        id: Id
    }).done(function (data) {
        $('#details').html(data);
    });
}

function LoadEditView(Id) {
    $.get('Movies/Edit', {
        id: Id
    }).done(function (data) {
        $('#details').html(data);
    });
}

function LoadAssignView(Id) {
    $.get('Movies/Assign', {
        id: Id
    }).done(function (data) {
        $('#details').html(data);
    });
} 

function SearchMovie() {
    $.get('Movies/FilterByText', {
        text: $('#textoABuscar').val()
    }).done(function (data) {
        $('#Filtered').html(data);
    });

}