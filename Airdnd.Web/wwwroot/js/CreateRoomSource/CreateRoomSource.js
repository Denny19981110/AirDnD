//宣告
let curr = 1;
//DOM
const groupArea = document.querySelector('#group-area')
const typeArea = document.querySelector('#type-area')
const next = document.querySelectorAll('.next')
const last = document.querySelectorAll('.return')
const plus = document.querySelector('.container11 .fa-circle-plus')
const minus = document.querySelector('.container11 .fa-circle-minus')
const price = document.querySelector('.container11 .price input')
const item5 = document.querySelectorAll('.container5 .item')
const warn = document.querySelector('.container11 .warn')

//頁面跳轉
next.forEach(btn => {
    if (!btn.getAttribute('disabled')) {
        btn.classList.remove('fail')
        btn.classList.add('accept')
    }
    btn.onclick = function () {
        document.querySelector(`.container${curr}`).classList.add('d-none')
        curr++
        document.querySelector(`.container${curr}`).classList.remove('d-none')
    }

});

last.forEach(btn => {
    btn.onclick = function () {
        document.querySelector(`.container${curr}`).classList.add('d-none')
        curr--
        document.querySelector(`.container${curr}`).classList.remove('d-none')
    }
})



//window.onload
window.onload = function () {

    groupArea.innerHTML = ''

    propGroups.forEach(x => {
        let html = `
            <input type="radio" class="d-none" id="${"group" + x.PropertyGroupId}" name="PropertyGroupID" value="${x.PropertyGroupId}">
            <label for="${"group" + x.PropertyGroupId}" class="item-btn d-flex justify-content-between p-3 my-3" onclick="changeType(${x.PropertyGroupId})">
                <span style="font-family:Noto Serif TC;">${x.PropertyGroupName}</span>
                <div class="pic"><img src="${x.PropertyGroupImgUrl}" alt="範例圖"></div>
            </label>
        `
        groupArea.innerHTML += html
    })
    //document.querySelector('.addressInput').onkeypress = function (event) {
    //    if (event.keyCode === 13) {
    //        document.querySelector('.addressSubmit').click
    //    }
    //}
    //container5
    item5.forEach(x => {
        let plus = x.querySelector('.fa-plus')
        let minus = x.querySelector('.fa-minus')
        let number = x.querySelector('input')

        plus.onclick = function () {
            if ((Number)(number.value) < 50) {
                number.value = ((Number)(number.value) + 1).toString()
            }
        }
        minus.onclick = function () {
            if ((Number)(number.value) > 1) {
                number.value = ((Number)(number.value) - 1).toString()
            }
        }
    })
    //container11
    price.value = 100
    plus.onclick = function () {
        if ((Number)(price.value) < 99995) {
            price.value = (Number)(price.value) + 5
        }
        else {
            price.value = 99999
        }
    }
    minus.onclick = function () {
        if ((Number)(price.value) > 100) {
            price.value = (Number)(price.value) - 5
        }
        else {
            price.value = 100
        }
    }

    
    
    //事件委派 => 防呆
    document.querySelector('.container1 .content').addEventListener('click', function (e) {
        if (e.target.tagName.toLowerCase() == 'input') {
            let btn = document.querySelector('.container1 .next')
            btn.removeAttribute('disabled', 'disabled')
            btn.classList.remove('fail')
            btn.classList.add('accept')
        }
    })
    document.querySelector('.container2 .content').addEventListener('click', function (e) {
        if (e.target.tagName.toLowerCase() == 'input') {
            let btn = document.querySelector('.container2 .next')
            btn.removeAttribute('disabled', 'disabled')
            btn.classList.remove('fail')
            btn.classList.add('accept')
        }
    })
    document.querySelector('.container3 .content').addEventListener('click', function (e) {
        if (e.target.tagName.toLowerCase() == 'input') {
            let btn = document.querySelector('.container3 .next')
            btn.removeAttribute('disabled', 'disabled')
            btn.classList.remove('fail')
            btn.classList.add('accept')
        }
    })
    document.querySelector('.container5 .content').addEventListener('click', function (e) {
        if (e.target.tagName.toLowerCase() == 'input') {
            let btn = document.querySelector('.container5 .next')
            btn.removeAttribute('disabled', 'disabled')
            btn.classList.remove('fail')
            btn.classList.add('accept')
        }
    })
    document.querySelector('.container6 .content').addEventListener('click', function (e) {
        if (e.target.tagName.toLowerCase() == 'input') {
            let btn = document.querySelector('.container6 .next')
            btn.removeAttribute('disabled', 'disabled')
            btn.classList.remove('fail')
            btn.classList.add('accept')
        }
    })
    document.querySelector('.container9 .content').addEventListener('click', function (e) {
        if (e.target.tagName.toLowerCase() == 'input') {
            let btn = document.querySelector('.container9 .next')
            btn.removeAttribute('disabled', 'disabled')
            btn.classList.remove('fail')
            btn.classList.add('accept')
        }
    })
}

//function
function changeType(groupId) {
    typeArea.innerHTML = ''

    var group = propGroups.find(x => x.PropertyGroupId == groupId)

    group.TypeList.forEach(x => {
        let html = `
            <input type="radio" class="d-none" id="${"type" + x.PropertyTypeId}" name="PropertyTypeID" value="${x.PropertyTypeId}">
            <label for="${"type"+x.PropertyTypeId}" class="item-btn d-flex flex-column p-3 my-3">
                <p style="font-family:Noto Serif TC;">${x.PropertyTitle}</p>
                <span style="font-family:Noto Serif TC;">${x.PropertyContent}</span>
            </label>
        `
        typeArea.innerHTML += html
    })
}
function checkInput(input) {
    let btn = document.querySelector(`.container${curr} .next`)
    if (input.value != null && input.value != '') {
        btn.removeAttribute('disabled', 'disabled')
        btn.classList.remove('fail')
        btn.classList.add('accept')
    }
    else {
        btn.setAttribute('disabled', 'disabled')
        btn.classList.add('fail')
        btn.classList.remove('accept')
    }
}
function checkPrice(input) {
    if ((Number)(input.value) > 100 || (Number)(input.value) < 99999) {
        btn.removeAttribute('disabled', 'disabled')
        btn.classList.remove('fail')
        btn.classList.add('accept')
    }
    else {
        btn.setAttribute('disabled', 'disabled')
        btn.classList.add('fail')
        btn.classList.remove('accept')
    }
}
function checkQuantity(input) {
    if ((Number)(input.value) < 1) {
        input.value = '1'
    }
    if ((Number)(input.value) > 50) {
        input.value = '50'
    }
}

