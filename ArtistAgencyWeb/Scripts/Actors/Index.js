$(document).ready(function () {

})

function LoadDetailsView(Id) {
    $.get('Actors/Details', {
        id: Id
    }).done(function (data) {
        $('#details').html(data);
    });
}

function LoadDeleteView(Id) {
    $.get('Actors/Delete', {
        id: Id
    }).done(function (data) {
        $('#details').html(data);
    });
}

function LoadEditView(Id) {
    $.get('Actors/Edit', {
        id: Id
    }).done(function (data) {
        $('#details').html(data);
    });
}



function SearchActors() {
    $.get('Actors/FilterByText', {
        text: $('#textoABuscar').val()
    }).done(function (data) {
        $('#Filtered').html(data);
    });

}

function LoadPerformView(movieId, actorId) {
    $.get('Actors/Perform', {
        movieId: movieId,
        actorId: actorId
    }).done(function (data) {
        $('#perform').html(data);
    });
}

function LoadQuitView(movieId, actorId) {
    $.get('Actors/Quit', {
        movieId: movieId,
        actorId: actorId
    }).done(function (data) {
        $('#perform').html(data);
    });
}


