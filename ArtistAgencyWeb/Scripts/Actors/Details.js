$(document).ready(function () {

})


function LoadPerformView(movieId,actorId) {
    $.get('Perform', {
        movieId: movieId,
        actorId: actorId
    }).done(function (data) {
        $('#perform').html(data);
    });
}