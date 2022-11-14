let markers
let zoom = 8
if(searchData.location != 'Any' ){
    zoom = 12
}

function setMap() {
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
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 18,
        attribution: '© AirDnD'
    }).addTo(map)
    markers = L.markerClusterGroup({
        maxClusterRadius: 10,
        singleMarkerMode: true,
        chunkedLoading: true
    })
    L.control
        .zoom({
            position: 'topright'
        })
        .addTo(map)
}

function setMarker() {
    // 建立marker
    locGroup.forEach(item => {
        let displayName
        if(item.listingName.length>10){
            displayName = item.listingName.substring(0,10)+'...'
        }else{
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

window.onload = () => {
    if (locGroup.length == 0) {
        locGroup.push({ id: 0, location: { lat: 23.58, lng: 120.58 }, priceGet: '0' })
    }
    setMap()
    if (locGroup[0].priceGet != 0) {
        setMarker()
    }
}
