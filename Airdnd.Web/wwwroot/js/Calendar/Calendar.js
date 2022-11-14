//宣告
let state, calendarDateInfo, calendarTopInfo, roomSource, specifyDate, defaultPrice
let count = 0
const roomSourceUrl = '/api/CalendarApi/GetRoomSource/'
const calendarDateUrl = '/api/CalendarApi/GetDates/'
const migrationUrl = '/api/CalendarApi/MigrationCalendar'
const defaultPriceUrl = '/api/CalendarApi/GetDefaultPrice/'

//DOM
const calendar = document.querySelector('.calendar')
const yearMonth = document.querySelector('.year-month')
const setPrice = document.querySelector('.set-price input')
const setPriceBtn = document.querySelector('.set-price button')
const nonReverse = document.querySelector('.non-reverse')
const reverse = document.querySelector('.reverse')
const roomSelector = document.querySelector('#room')
const functionDate = document.querySelector('.function h2')
const warn = document.querySelector('.warn')

//calender.scroll
calendar.addEventListener('scroll', function () {
    calendarTopInfo = document.querySelector('.calendar').getBoundingClientRect().top
    calendarDateInfo = document.querySelectorAll('.month')
    calendarDateInfo.forEach(x => {
        if (x.getBoundingClientRect().bottom <= calendarTopInfo) {
            yearMonth.innerHTML = `<h2>${x.textContent}</h2>`
        }
    })
})
//window.onload
window.onload = async function () {
    await init()
    selector()

    if(roomSelector.value == ''){
        calendar.classList.add('d-none')
        warn.classList.remove('d-none')
    }
    else{
        warn.classList.add('d-none')
        }

    await fetch(calendarDateUrl + roomSelector.value)
        .then(async (res) => {
            specifyDate = await res.json()
        })
        .catch(ex => { console.log(ex) })

    await fetch(defaultPriceUrl + roomSelector.value)
        .then(async (res) => {
            defaultPrice = await res.json()
        })
        .catch(ex => console.log(ex))

    render()
    changFunctionDate()

    roomSelector.addEventListener('change', async function (event) {
        setPrice.value = ''
        functionDate.textContent = ''
        calendar.innerHTML = ''
        setPrice.setAttribute('disabled', 'disabled')
        //取得特定日子資料
        await fetch(calendarDateUrl + roomSelector.value)
            .then(async (res) => {
                specifyDate = await res.json()
            })
            .catch(ex => { console.log(ex) })
        //取得預設價格
        await fetch(defaultPriceUrl + roomSelector.value)
            .then(async (res) => {
                defaultPrice = await res.json()
                console.log(defaultPrice)
            })
            .catch(ex => console.log(ex))

        render()
    })
}


async function init() {
    state = new Date()

    // const res = await fetch(roomSourceUrl)
    // roomSource = await res.json();
    
    await fetch(roomSourceUrl)
        .then((res) => {
            return res.json()
        })
        .then((res) => {
            roomSource = res
        })
        .catch((ex) => {
            console.log(ex)
        })


}
//渲染下拉選單資料
function selector() {
    roomSource.forEach(x => roomSelector.innerHTML += `<option value="${x.listingId}">${x.listingName}</option>`)
}


function render() {
    while (count < 12) {
        //每個月第一天
        let firstDay = new Date(state.getFullYear(), state.getMonth() + count, 1)
        //本月第一天
        let date = new Date(firstDay.getFullYear(), firstDay.getMonth(), 1)

        //往前算到星期日
        date.setDate(date.getDate() - date.getDay())

        //每個月間格開
        const list = document.createElement('div')
        list.setAttribute('class', 'list')
        let month = document.createElement('p')
        month.setAttribute('class', 'month')
        month.textContent = `${firstDay.getFullYear()}-${firstDay.getMonth() + 1}`

        //判斷上個月的後幾天
        while (date < firstDay) {
            renderDate(date, list, firstDay)
        }

        //判斷這個月日期
        while (date.getMonth() === firstDay.getMonth()) {
            //劃出一天的格子
            renderDate(date, list, firstDay)
        }

        //判斷下個月日期
        while (date.getDay() > 0) {
            renderDate(date, list, firstDay)
        }
        count++
        calendar.append(month, list)

        yearMonth.innerHTML = `<h2>${state.getFullYear()}-${state.getMonth() + 1}</h2>`
    }
    count = 0
}

function renderDate(date, list, firstDay) {
    let cell = document.createElement('div')
    cell.classList.add('date')

    const price = document.createElement('p')
    price.setAttribute('class', 'price')

    date < state || date.getMonth() > firstDay.getMonth() || date.getMonth() < firstDay.getMonth() ? cell.classList.add('fadeout') : ''

    date.getDate() === state.getDate() && date.getMonth() === state.getMonth() && date.getFullYear() === state.getFullYear() && date.getMonth() === firstDay.getMonth() ? cell.classList.add('today') : cell.classList.remove('today')

    cell.innerHTML = `<h4>${date.getDate()}</h4>`
    price.innerHTML = `$<span>${defaultPrice}</span>`
    
    //渲染資料異動過的日期
    if (specifyDate != null) {
        specifyDate.forEach(x => {
            new Date(x.calendarDate).toISOString() == date.toISOString() && !x.available ? cell.classList.add('no-book') : ''
            if (new Date(x.calendarDate).toISOString() == date.toISOString()) {
                price.innerHTML = `$<span>${x.price}</span>`
            }
        })
    }

    cell.appendChild(price)
    list.appendChild(cell)

    //日期+1
    date.setDate(date.getDate() + 1)
    cell.scroll(100, cell.scrollHeight)
}
//資料異動
function changFunctionDate() {
    calendar.addEventListener('click', function (e) {
        if (e.target.tagName.toLowerCase() == 'div') {
            functionDate.textContent = `${yearMonth.textContent}-${(e.target.querySelector('h4').textContent).padStart(2, 0)}`
            setPrice.value = ''
            if (!e.target.classList.contains('fadeout') && !e.target.classList.contains('no-book')) {
                //符合條件可以更改價格
                setPrice.removeAttribute('disabled')
                setPrice.value = Number(e.target.querySelector('span').textContent)
                setPriceBtn.onclick = function () {
                    e.target.querySelector('span').textContent = setPrice.value
                    let data = {
                        Price: parseInt(setPrice.value),
                        listingId: roomSelector.value,
                        available: true,
                        calendarDate: functionDate.textContent
                    }
                    fetch(migrationUrl, {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(data)
                    }).then(res => console.log(res.status))
                        .catch(ex => console.log(ex))
                }
                ReverseFunction(e.target)
            }
            else {
                ReverseFunction(e.target)
                setPrice.setAttribute('disabled', 'disablbed')
            }
        }
    })
}
//更改是否能預定
function ReverseFunction(x) {
    reverse.onclick = function () {
        setPrice.value = x.querySelector('span').textContent
        if (!x.classList.contains('fadeout')) {
            x.setAttribute('class', 'date')
            setPrice.removeAttribute('disabled')
        }
        let data = {
            Price: parseInt(setPrice.value),
            listingId: roomSelector.value,
            available: true,
            calendarDate: new Date(functionDate.textContent)
        }
        fetch(migrationUrl, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        }).then(res => console.log(res.status))
            .catch(ex => console.log(ex))

    }
    nonReverse.onclick = function () {
        if (!x.classList.contains('fadeout')) {
            x.setAttribute('class', 'date no-book')
            setPrice.setAttribute('disabled', 'disablbed')
        }
        let data = {
            Price: parseInt(setPrice.value),
            listingId: roomSelector.value,
            available: false,
            calendarDate: new Date(functionDate.textContent)
        }
        fetch(migrationUrl, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        }).then(res => console.log(res.status))
            .catch(ex => console.log(ex))
    }
}