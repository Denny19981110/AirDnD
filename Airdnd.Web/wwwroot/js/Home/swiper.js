var swiper1 = new Swiper(".cate-swiper", {
    slidesPerView: 1,
    spaceBetween: 10,
    watchSlidesProgress: true,
    scrollbar: {
        el: ".swiper-scrollbar",
        hide: true,
    },
    breakpoints: {
        "@0.00": {
            slidesPerView: 5,
            spaceBetween: 0,
        },
        "@0.75": {
            slidesPerView: 8,
            spaceBetween: 0,
        },
        "@1.00": {
            slidesPerView: 10,
            spaceBetween: 5,
        },
        "@1.50": {
            slidesPerView: 16,
            spaceBetween: 10,
        },
    },
    navigation: {
        nextEl: "#cateR.swiper-button-next",
        prevEl: "#cateL.swiper-button-prev",
    }
});
var swiper2 = new Swiper(".img-swiper", {
    preloadImages: false,
    lazy: {
        enabled: true,
        loadPrevNext: true,
        loadPrevNextAmount: 1,
    },
    loop: false,
    spaceBetween: 30,
    centeredSlides: true,
    pagination: {
        el: ".swiper-pagination",
        clickable: true,
    },
    navigation: {
        nextEl: ".item-section .swiper-button-next",
        prevEl: ".item-section .swiper-button-prev",
    },
});
