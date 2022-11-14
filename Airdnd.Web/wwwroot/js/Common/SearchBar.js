Vue.use(window.AirbnbStyleDatepicker, {
    colors: {
        selected: '#eb4c60',
        inRange: ' #FF9CB0',
        selectedText: '#fff',
        text: '#565a5c',
        inRangeBorder: '#D93240',
        hoveredInRange: '#eb4c60'
    },
    sundayFirst: 'true'
})

const searchBar = new Vue({
    el: '#searchbar-vue',
    data: {
        location: searchData.location != 'Any' ? searchData.location : '',
        guest: {
            adult: searchData.guest.adult,
            child: searchData.guest.child,
            baby: searchData.guest.baby,
            max: 10
        },
        startOpen: true,
        date1: searchData.date1 != 'Any' ? searchData.date1 : '',
        date2: searchData.date2 != 'Any' ? searchData.date2 : '',
        history: []
    },
    created() {
        if (localStorage.getItem('searchHistory') != null) {
            histories = JSON.parse(localStorage.getItem('searchHistory'))
            this.history = histories
        }
    },
    mounted() {
        this.$refs.monthWidth.width = 380

        $('input').on('keypress', e => {
            let code = e.keycode || e.witch
            if (code == 13) {
                e.preventDefault()
                return false
            }
        })
    },
    methods: {
        lightboxClose(e) {
            if ($(e.target).has('.lightbox-container').length) {
                $('.lightbox').hide()
                if (window.innerWidth >= 768) {
                    $('header .main').show()
                } else {
                    $('.md-btn-container').show()
                }
                document.body.style.overflow = 'overlay'
            }
            if ($(e.target).has('.area').length) {
                $('.search-bar-container').addClass('before')
                $('.lightbox').hide()
                if (window.innerWidth >= 768) {
                    $('header .main').show()
                } else {
                    $('.md-btn-container').show()
                }
                document.body.style.overflow = 'overlay'
            }
        },
        openSearchBar() {
            $('.search-bar-container').removeClass('before')
            $('header .main').hide()
            if (window.innerWidth <= 768) {
                $('.md-btn-container').hide()
                $('.search-bar-floatbox.loc').show()
            }
            document.body.style.overflow = 'hidden'
        },
        searchLoc() {
            $('.search-bar-floatbox').hide()
            $('.search-bar-floatbox.loc').show()
        },
        searchDate() {
            $('.search-bar-floatbox').hide()
            $('.search-bar-floatbox.calendar').show()
        },
        searchGuest() {
            $('.search-bar-floatbox').hide()
            $('.search-bar-floatbox.guest').show()
        },
        search() {
            if (this.location != '' || (this.date1 != '' && this.date2 != '')) {
                let loc = this.location == '' ? 'Any' : this.location
                let checkIn = this.checkInSelect == '新增日期' ? 'Any' : this.checkInSelect
                let checkOut = this.checkOutSelect == '新增日期' ? 'Any' : this.checkOutSelect

                let historyBody = {
                    Location: (location = this.location != '' ? this.location : 'Any'),
                    Date: {
                        checkIn: (d1 = this.date1 != '' ? this.date1 : 'Any'),
                        checkOut: (d2 = this.date2 != '' ? this.date2 : 'Any')
                    },
                    Guests: `${this.guest.adult}-${this.guest.child}-${this.guest.baby}`
                }
                let histories = []
                if (localStorage.getItem('searchHistory') != null) {
                    histories = JSON.parse(localStorage.getItem('searchHistory'))
                    if (histories.length == 12) {
                        histories.splice(11, 1)
                    }
                    histories.unshift(historyBody)
                } else {
                    histories.push(historyBody)
                }
                localStorage.setItem('searchHistory', JSON.stringify(histories))
                let domain = location.host
                location.assign(`https://${domain}/search/${loc}/${checkIn}_${checkOut}/${this.getGuests}`)
            } else {
                $('.lightbox').hide()
                $('.search-bar-container').addClass('before')
                $('header .main').show()
                document.body.style.overflow = 'overlay'
            }
        },
        searchHistory(item) {
            let domain = location.host
            location.assign(`https://${domain}/search/${item.Location}/${item.Date.checkIn}_${item.Date.checkOut}/${item.Guests}`)
        }
    },
    computed: {
        adultMinusDisabled() {
            return this.guest.adult === 1
        },
        adultAddDisabled() {
            return this.guest.adult === this.guest.max
        },
        childMinusDisabled() {
            return this.guest.child === 0
        },
        childAddDisabled() {
            return this.guest.child === this.guest.max
        },
        babyMinusDisabled() {
            return this.guest.baby === 0
        },
        babyAddDisabled() {
            return this.guest.baby === this.guest.max
        },
        getGuests() {
            return `${this.guest.adult}-${this.guest.child}-${this.guest.baby}`
        },
        guestCount() {
            let result
            if (this.guest.baby == 0) {
                result = `${this.guest.adult + this.guest.child}位`
            } else {
                result = `${this.guest.adult + this.guest.child}位,${this.guest.baby}名嬰幼兒`
            }
            return result
        },
        now() {
            return new Date().toDateString()
        },
        checkInSelect() {
            return this.date1 !== '' ? this.date1 : '新增日期'
        },
        checkOutSelect() {
            return this.date2 !== '' ? this.date2 : '新增日期'
        },
        currentLoc() {
            let result = this.location != '' ? this.location : '任何地點'
            return result
        },
        currentTime() {
            let d1 = this.date1 != '' ? new Date(this.date1) : 0
            let d2 = this.date2 != '' ? new Date(this.date2) : 0
            let result
            if (d1 != 0 && d2 != 0)
                result =
                    d1.getMonth() == d2.getMonth()
                        ? `${d1.getMonth() + 1}/${d1.getDate()} - ${d2.getDate()}`
                        : `${d1.getMonth() + 1}/${d1.getDate()}-${d2.getMonth() + 1}/${d2.getDate()}`
            else result = '任何時間'

            return result
        },
        currentGuest() {
            let result
            let mainCount = this.guest.adult + this.guest.child
            if (mainCount == 1) return '新增人數'
            if (this.guest.baby == 0) result = `${mainCount} 位`
            else result = `${mainCount}位,${this.guest.baby}名嬰幼兒`

            return result
        }
    },
    filters: {
        toLocation(loc) {
            if (loc == 'Any') {
                return '任何地點'
            }
            return loc
        },
        toDateRange(date) {
            let dIn = new Date(date.checkIn)
            let dOut = new Date(date.checkOut)
            if (isNaN(dIn.getMonth()) || isNaN(dOut.getMonth())) {
                return '任何時間'
            }
            if (dIn.getMonth() + 1 == dOut.getMonth() + 1) {
                return `${dIn.getMonth() + 1}月${dIn.getDate()}日至${dOut.getDate()}日`
            } else {
                return `${dIn.getMonth() + 1}月${dIn.getDate()}日至${dOut.getMonth() + 1}月${dOut.getDate()}日`
            }
        },
        toGuest(value) {
            var guestArr = value.split('-')
            if (guestArr[2] == 0) {
                return `${parseInt(guestArr[0]) + parseInt(guestArr[1])}位`
            } else {
                return `${parseInt(guestArr[0]) + parseInt(guestArr[1])}位,${guestArr[2]}名嬰幼兒`
            }
        }
    }
})
