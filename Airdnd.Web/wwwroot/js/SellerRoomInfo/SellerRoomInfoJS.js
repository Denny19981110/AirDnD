let swiper = new Swiper(".slide-content", {
	slidesPerView: 3,
	spaceBetween: 15,
	loop: true,
	centerSlide: 'true',
	fade: 'true',
	grabCursor: 'true',
	pagination: {
		el: ".swiper-pagination",
		clickable: true,
		dynamicBullets: true,
	},
	navigation: {
		nextEl: ".swiper-button-next",
		prevEl: ".swiper-button-prev",
	},

	breakpoints: {
		0: {
			slidesPerView: 1,
		},
		520: {
			slidesPerView: 2,
		},
		// 950: {
		//     slidesPerView: 3,
		// },
	},
});

let imgswiper = new Swiper(".slide-img-content", {
	slidesPerView: 3,
	spaceBetween: 15,
	loop: true,
	centerSlide: 'true',
	fade: 'true',
	grabCursor: 'true',
	pagination: {
		el: ".swiper-pagination",
		clickable: true,
		dynamicBullets: true,
	},
	// navigation: {
	//     nextEl: ".swiper-button-next",
	//     prevEl: ".swiper-button-prev",
	// },

	breakpoints: {
		0: {
			slidesPerView: 1,
		},
		520: {
			slidesPerView: 2,
		},
		1030: {
			slidesPerView: 4,
		},
	},
});

