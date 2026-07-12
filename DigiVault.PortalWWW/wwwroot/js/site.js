function scrollCarouselLeft(id) {
    document.getElementById(id).scrollBy({ left: -600, behavior: 'smooth' });
}

function scrollCarouselRight(id) {
    document.getElementById(id).scrollBy({ left: 600, behavior: 'smooth' });
}
