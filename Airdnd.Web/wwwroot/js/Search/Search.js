const containerProducts = document.querySelector('.container-set')
const btnHideL = document.querySelector('.btn-hide-l')
const btnHideM = document.querySelector('.btn-hide-m')

const btnPull = document.querySelector('.pull-icon')
const secMap = document.querySelector('.map-section')

btnHideL.onclick = () => {
        btnHideL.classList.toggle('close')
        containerProducts.classList.toggle('close')
        secMap.classList.toggle('full-width')
        setTimeout(function () {
            map.invalidateSize()
        }, 500) 
}

btnHideM.onclick = () => { 
      secMap.classList.toggle('close')
      btnHideM.classList.toggle('close')
   
}
