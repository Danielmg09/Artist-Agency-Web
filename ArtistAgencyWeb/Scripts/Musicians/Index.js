$(document).ready(function () {

})

function LoadDetailsView(Id) {
    $.get('Musicians/Details', {
        id: Id
    }).done(function (data) {
        $('#details').html(data);
    });
}

function LoadDeleteView(Id) {
    $.get('Musicians/Delete', {
        id: Id
    }).done(function (data) {
        $('#details').html(data);
    });
}

function LoadEditView(Id) {
    $.get('Musicians/Edit', {
        id: Id
    }).done(function (data) {
        $('#details').html(data);
    });
}

function LoadPerformView(concertId, musicianId) {
    $.get('Musicians/Perform', {
        concertId: concertId,
        musicianId: musicianId
    }).done(function (data) {
        $('#perform').html(data);
    });
}

function LoadQuitView(concertId, musicianId) {
    $.get('Musicians/Quit', {
        concertId: concertId,
        musicianId: musicianId
    }).done(function (data) {
        $('#perform').html(data);
    });
}




function SearchMusicians() {
    $.get('Musicians/FilterByText', {
        text: $('#textoABuscar').val()
    }).done(function (data) {
        $('#Filtered').html(data);
    });

}