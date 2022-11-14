let zoom = 8
let markers
let map
const taiwanCenter = [23.97565, 120.9738819]

function setMap() {
    if (locGroup.length != 0) {
        map = L.map('map', {
            center: [
                locGroup.reduce((prev, calc) => {
                    return prev + calc.location.lat
                }, 0) / locGroup.length,
                locGroup.reduce((prev, calc) => {
                    return prev + calc.location.lng
                }, 0) / locGroup.length
            ],
            zoom: zoom,
            zoomControl: false,
            closePopupOnClick: false
        })
    } else {
        map = L.map('map', {
            center: taiwanCenter,
            zoom: zoom,
            zoomControl: false,
            closePopupOnClick: false
        })
    }
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 18,
        minZoom: 6,
        attribution: '© AirDnd'
    }).addTo(map)
    markers = L.markerClusterGroup({
        maxClusterRadius: 10,
        singleMarkerMode: true,
        chunkedLoading: true
    })
    if (document.querySelector('.btn-map') == null) {
        setMarker()
    }
    $('.btn-map').one('click', setMarker)
}

const secMap = document.querySelector('.map-section')
$('.btn-map').click(function () {
    secMap.classList.toggle('show')
    if (secMap.classList.contains('show')) {
        $('.btn-map').html(`<p class="map-display-title">顯示列表</p>
                        <i class="fa-regular fa-list-ul"></i>`)
        document.body.style.overflowY = 'hidden'
    } else {
        $('.btn-map').html(`<p class="map-display-title">顯示地圖</p>
        <i class="fa-solid fa-map-location"></i>`)
        document.body.style.overflowY = 'overlay'
    }
})
window.onresize = () => map.invalidateSize

//初始化marker

function setMarker() {
    // 建立marker

    locGroup.forEach(item => {
        let displayName
        if (item.listingName.length > 10) {
            displayName = item.listingName.substring(0, 10) + '...'
        } else {
            displayName = item.listingName
        }
        let popup = L.popup()
            .setLatLng([item.location.lat, item.location.lng])
            .setContent(`<a href="/House/${item.listingName}" class="product-link">${displayName}<br/>\$${item.priceGet}&nbsp;TWD</a>`)
            .openOn(map)

        markers.addLayer(popup)
    })
    map.addLayer(markers)
}
