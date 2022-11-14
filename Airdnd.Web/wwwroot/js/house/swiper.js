
     var swiper = new Swiper(".snailSwiper", {
         loop: true,
         preloadImages: false,
         lazy: true,
         spaceBetween: 10,
         slidesPerView: 4,
         freeMode: true,
         watchSlidesProgress: true,
     });
     var swiper2 = new Swiper(".top-swiper", {
         loop: true,
         preloadImages: false,
         lazy: true,
         spaceBetween: 10,
         navigation: {
             nextEl: ".swiper-button-next",
             prevEl: ".swiper-button-prev",
         },
         thumbs: {
             swiper: swiper,
         },

     });
        //Denny lightbox js
        const lightbox = document.createElement('div')
        lightbox.id = 'lightbox'
        document.body.appendChild(lightbox)
let imgs = document.querySelectorAll('.swiperImg')
        imgs.forEach(getimg => {
            getimg.addEventListener('click', clickimg => {
                lightbox.classList.add('active')
                let img = document.createElement('img')
                img.src = getimg.src
                while (lightbox.firstChild) {
                    lightbox.removeChild(lightbox.firstChild)
                }
                lightbox.appendChild(img)
                console.log(lightbox)
            })
        })
        lightbox.addEventListener('click', removelightbox => {
            lightbox.classList.remove('active')
        })
