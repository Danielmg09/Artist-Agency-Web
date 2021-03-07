$(document).ready(function () {

})


function LoadPerformView(concertId, musicianId) {
    $.get('Musicians/Perform', {
        concertId: concertId,
        musicianId: musicianId
    }).done(function (data) {
        $('#perform').html(data);
    });
}