//DOM
const coming = document.querySelector('.coming')
const checkIn = document.querySelector('.checkIn')
const checkOut = document.querySelector('.checkOut')
const comingApi = '/api/OrderApi/GetComing'
const checkInApi = '/api/OrderApi/GetCheckIn'
const checkOutApi = '/api/OrderApi/GetCheckOut'

const tab = new Vue({
    el: '#tab',
    data: {
        //即將入住
        coming: [],
        //入住中
        checkIn: [],
        //即將退房
        checkOut: []
    },
    created() {
        this.getComing()
        this.getCheckIn()
        this.getCheckOut()
    },
    filters: {
        dateFormat(date) {
            if (date != undefined) {
                return date.slice(0, 10)
            }
        },
        moneyFormat(price) {
            if (price != undefined) {
                return price.toLocaleString()
            }
        }
    },
    methods: {
        getComing() {
            axios.get(comingApi)
                .then((res) => {
                    if (res.status == 200) {
                        this.coming = res.data
                        console.log(this.coming)
                    }
                })

        },
        getCheckIn() {
            axios.get(checkInApi)
                .then((res) => {
                    if (res.status == 200) {
                        this.checkIn = res.data
                        console.log(this.checkIn)
                    }
                })
        },
        getCheckOut() {
            axios.get(checkOutApi)
                .then((res) => {
                    if (res.status == 200) {
                        this.checkOut = res.data
                        console.log(this.checkOut)
                    }
                })
        },
    }
})




