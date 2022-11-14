window.onload = () => {


    var map = L.map('map', {
        minZoom: 9,
        maxZoom: 16
    });


map.setView(new L.LatLng(address[0], address[1]), 15);

let Icon = new L.Icon({
    iconUrl: 'https://kamiiliu.github.io/house-icon.png',
    iconSize: [80, 80],
    iconAnchor: [40, 40],
    popupAnchor: [0, -50],
    shadowSize: [41, 41]
});

var circle = L.circle([address[0], address[1]],
     390,
      {
          color: '#eb4c60',
          fillColor: '#eb4c60',
          fillOpacity: 0.2
      }
    ).addTo(map);
    console.log(circle)

    // 設定圖資來源
    var osmUrl = 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png';
    var osm = new L.TileLayer(osmUrl);
    var marker = L.marker(address, { icon: Icon }).addTo(map).bindPopup("預定確認後，提供確切位置", {closeOnClick: false}).openPopup()
    map.addLayer(osm);

    let expensiveDay = document.querySelectorAll(".asd__day--expensive")
    expensiveDay.forEach((e,i) => {
        e.dataset.expensive = specialDays.expensiveDays[i].price
    })
    let cheapDay = document.querySelectorAll(".asd__day--cheap")
    cheapDay.forEach((e, i) => {
        e.dataset.cheap = specialDays.cheapDays[i].price
    })
    let c = document.querySelector(".asd__action-buttons")
    let div = document.createElement("div");
    c.prepend(div)
    div.innerHTML = '<div class="d-flex justify-content-end p-3"><div class="d-flex me-2"><div class="colerbox exbox"></div><p>高於平均價</p></div><div class="d-flex"><div class="colerbox chbox"></div><p>低於平均價</p></div></div>'
}

    
