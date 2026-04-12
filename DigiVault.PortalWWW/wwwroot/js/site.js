// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function scrollCarouselLeft(id) {
    document.getElementById(id).scrollBy({ left: -600, behavior: 'smooth' });
}

function scrollCarouselRight(id) {
    document.getElementById(id).scrollBy({ left: 600, behavior: 'smooth' });
}