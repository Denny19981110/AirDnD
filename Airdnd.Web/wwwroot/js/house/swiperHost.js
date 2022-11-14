var swiper2 = new Swiper(".img-swiper", {
    loop: true,
    preloadImages: false,
    lazy: true,
    spaceBetween: 30,
    centeredSlides: true,
    watchSlidesProgress: true,
    pagination: {
        el: ".swiper-pagination",
        clickable: true,
    },
    navigation: {
        nextEl: ".swiper-button-next",
        prevEl: ".swiper-button-prev",
    },
});