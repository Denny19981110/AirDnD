window.onload = () => {
    getPosition()
    setMap()
    if (noDis.length > 0 && $.cookie('location') != null) {
        getDistanceByCookie()
    }
}

function toggleMenu() {
    document.querySelectorAll('.toggle-menu').forEach(e => {
        e.classList.toggle('toggle-menu-active')
    })
}
function openSearchBar() {
    $('.search-bar-container').removeClass('before')
    if (window.innerWidth <= 768) {
        $('.md-btn-container').hide()
        $('.search-bar-floatbox.loc').show()
    }
    document.body.style.overflow = 'hidden'
}

const loc = { lat: 0, lng: 0 }
function successHandler(position) {
    loc.lat = position.coords.latitude
    loc.lng = position.coords.longitude
    $.cookie('location', `${JSON.stringify(loc)}`, { expires: 7 })
    getDistanceByCookie()
}

function errorHandler(err) {
    console.log(err);
}

function getPosition() {
    navigator.geolocation.getCurrentPosition(successHandler, errorHandler)
}

let noDis = document.querySelectorAll('.no-distance')
function getDistanceByCookie() {
    noDis.forEach(item => {
            let dis = Math.round(getUserToListing(item))
            item.innerText = `距離${dis} 公里`    
    })
}
function getUserToListing(item) {
    let cookie = $.cookie('location')
    let userJson = JSON.parse(cookie)
    let itemLat = item.dataset.lat
    let itemLng = item.dataset.lng

    let result = getDistanceFromLatLonInKm(itemLat, itemLng, userJson.lat, userJson.lng)
    return result
}

function getDistanceFromLatLonInKm(lat1, lng1, lat2, lng2) {
    var R = 6371 // Radius of the earth in km
    var dLat = deg2rad(lat2 - lat1) // deg2rad below
    var dLng = deg2rad(lng2 - lng1)
    var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) + Math.cos(deg2rad(lat1)) * Math.cos(deg2rad(lat2)) * Math.sin(dLng / 2) * Math.sin(dLng / 2)

    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a))
    var d = R * c // Distance in km
    return d
}

function deg2rad(deg) {
    return deg * (Math.PI / 180)
}

let propBtn = document.querySelectorAll('.category-btn')
propBtn.forEach(btn => {
    btn.onclick = function (e) {
        let propId = btn.dataset.id
        location.assign(`/prop/${propId}`)
    }
})
