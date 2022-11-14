var map, geocoder;
const submit = document.querySelector('.addressSubmit')
const addressInput = document.querySelector('.addressInput')
const nextBtn = document.querySelector('.container4 .next')
const errorMsg = document.querySelector('.container4 .warn')


function initMap() {
    geocoder = new google.maps.Geocoder();
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 17
    });

    var address = '總統府';

    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == 'OK') {
            map.setCenter(results[0].geometry.location);
            var marker = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location
            });
        } else {
            console.log(status);
        }
    });
}

submit.addEventListener('click', function () {
    console.log(addressInput.value);
    geocoder.geocode({
        'address': addressInput.value
    }, function (results, status) {
        if (status != 'OK') {
            console.log(status);
            nextBtn.setAttribute('disabled', 'disabled')
            nextBtn.classList.add('fail')
            nextBtn.classList.remove('accept')
            errorMsg.classList.remove('d-none')
        } else {
            map.setCenter(results[0].geometry.location);
            var marker = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location
            });
            nextBtn.removeAttribute('disabled', 'disabled')
            nextBtn.classList.remove('fail')
            nextBtn.classList.add('accept')
            errorMsg.classList.add('d-none')
        }
    });
})


