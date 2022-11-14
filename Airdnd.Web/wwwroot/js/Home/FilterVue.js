const btnFilter = document.querySelector('.filter-button')
let overlayFilter = document.querySelector('.filter')


overlayFilter.onclick = event => {
    if ($(event.target).has('.lightbox-container').length) {
        $('.lightbox').hide()
        document.body.style.overflow = 'overlay'
    }
}
btnFilter.onclick = () => {
    $('.filter').show()
    document.body.style.overflow = 'hidden'
}

//slider
const sliderOne = document.querySelector('#slider-1')
const sliderTwo = document.querySelector('#slider-2')
const min = document.querySelector('.min-price')
const max = document.querySelector('.max-price')
const track = document.querySelector('.slider-track')
let maxVal = sliderOne.max
let minVal = sliderOne.min
let minGap = 0
const filterUrl = '../'

let filter = new Vue({
    el: '.lightbox.filter',
    data: {
        minPrice: dataObj.price.minPrice,
        maxPrice: dataObj.price.maxPrice,
        currentMin: dataObj.price.currentMin,
        currentMax: dataObj.price.currentMax,
        priceAvg: dataObj.price.priceAvg,
        trackColor: 'background:linear-gradient(to right, #d5d5d5 0% , #000 0% , #000 100%, #d5d5d5 100%)',
        privacyList: dataObj.privacy,
        privacyArr: [0],
        bedrooms: 0,
        beds: 0,
        bathrooms: 0,
        propertyList: dataObj.property,
        propertyArr: [0],
        fillFour: dataObj.fillFour,
        serviceList: dataObj.service,
        serviceArr: [0],
        lightbox: true,
        show: 'display:none'
    },
    mounted: function () {
        this.fillLine()
    },
    methods: {
        closeLightbox(event) {
            if ($(event.target).has('.lightbox-container').length) {
                $('.lightbox').hide()
                document.body.style.overflow = 'overlay'
            }
        },
		closeFilter(){
			 $('.filter').hide()
                document.body.style.overflow = 'overlay'
		},
        clearCheck() {
            let checks = document.querySelectorAll('input[type=checkbox]')
            checks.forEach(item => {
                item.checked = false
            })
            this.currentMin = this.minPrice
            this.currentMax = this.maxPrice
            this.bedrooms = 0
            this.beds = 0
            this.bathrooms = 0
            this.privacyArr = [0]
            this.propertyArr = [0]
            this.serviceArr = [0]
            this.fillLine()
        },
        passData() {
            let body = {
                Price: {
                    MinPrice: this.minPrice,
                    MaxPrice: this.maxPrice,
                    CurrentMax: this.currentMax,
                    CurrentMin: this.currentMin
                },
                Privacies: this.privacyArr,
                Rooms: {
                    Bedrooms: this.bedrooms,
                    Beds: this.beds,
                    Bathrooms: this.bathrooms
                },
                Properties: this.propertyArr,
                Services: this.serviceArr
            }
            let url = new URL(document.URL)
            let param = $.param(body)
            url.search = new URLSearchParams(param)  
            window.location = url
        },
        getBedrooms(event) {
            this.bedrooms = event.target.value
        },
		
        getBeds(event) {
            this.beds = event.target.value
        },
		
        getBathrooms(event) {
            this.bathrooms = event.target.value
        },
        slideOneDisplay() {
            if (parseInt(sliderTwo.value) - parseInt(sliderOne.value) <= minGap) {
                sliderOne.value = parseInt(sliderTwo.value) - minGap
            }
            this.minPrice = sliderOne.value
            fillLine()
        },
        slideTwoDisplay() {
            if (parseInt(sliderTwo.value) - parseInt(sliderOne.value) <= minGap) {
                sliderTwo.value = parseInt(sliderOne.value) + minGap
            }
            this.maxPrice = sliderTwo.value
            fillLine()
        },
        fillLine() {
            let p1 = Math.round(((this.currentMin - this.minPrice) / (this.maxPrice - this.minPrice)) * 100)
            let p2 = Math.round(((this.currentMax - this.minPrice) / (this.maxPrice - this.minPrice)) * 100)
            this.trackColor = `background:linear-gradient(to right, #d5d5d5 ${p1}% , #000 ${p1}% , #000 ${p2}%, #d5d5d5 ${p2}%)`
        },
        setMax() {
            if (this.currentMax > this.maxPrice) {
                this.currentMax = this.maxPrice
            }
        },
        setMin() {
            if (this.currentMin < this.minPrice) {
                this.currentMin = this.minPrice
            }
        },
    },
    computed: {
        fillLineOnchange() {
            let p1 = Math.round(((this.currentMin - this.minPrice) / (this.maxPrice - this.minPrice)) * 100)
            let p2 = Math.round(((this.currentMax - this.minPrice) / (this.maxPrice - this.minPrice)) * 100)
            return `background-image:linear-gradient(to right, #d5d5d5 ${p1}% , #000 ${p1}% , #000 ${p2}%, #d5d5d5 ${p2}%)`
        }
    }
})
