// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    $(window).on('scroll', function () {
        if ($(window).scrollTop() > 10) {
            $('.navbar').addClass('active');
        } else {
            $('.navbar').removeClass('active');
        }
    });
});

try {

    var modal = document.getElementById("myModal");
    var span = document.getElementsByClassName("closeModel")[0];

    span.onclick = function () {
        modal.style.display = "none";
    }

    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }
} catch { }

// Initialize and add the map
function initMap() {
    // The location of Uluru
    const redwizards = { lat: 51.92780685424805, lng: 4.476498603820801 };
    // The map, centered at Uluru
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 18,
        center: redwizards,
        mapId: '8aca3da93b28cd03',
    });
    // The marker, positioned at Uluru
    const marker = new google.maps.Marker({
        position: redwizards,
        map: map,
    });
}

window.initMap = initMap;