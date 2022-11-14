let thisYear = new Date().getFullYear()
const apiUrl = "/api/ChartApi/GetChartData"
let apiData, totalMembers, thisMonthOrders, thisYearOrders, thisMonthRevenue, thisYearRevenue, everyMonthOrders, everyMonthRevenue, everySeasonMembers, everySeasonListing, membersGender
let ordersChart = [], revenueChart = [], membersChart = [], listingChart = [], genderChart = []
const ctx_income = document.getElementById("incomeChart")
const ctx_createRoom = document.getElementById("createRoomSourceChart")
const ctx_register = document.getElementById("registerChart")
const ctx_gender = document.getElementById("genderChart")



window.onload = async function () {
    await init()
    await data()
    
    document.querySelector('.loading').classList.add('d-none')
    document.getElementById('totalMembers').innerHTML = `共${totalMembers}位`
    document.getElementById('thisMonthOrders').innerHTML = `共${thisMonthOrders}筆`
    document.getElementById('thisYearOrders').innerHTML = `共${thisYearOrders}筆`
    document.getElementById('thisMonthRevenue').innerHTML = `$${thisMonthRevenue[0][""].toLocaleString()}`
    document.getElementById('thisYearRevenue').innerHTML = `$${ thisYearRevenue[0][""].toLocaleString() }`

    const chart1 = new Chart(ctx_income, {
        type: "bar",
        data: {
            labels: [
                "1月",
                "2月",
                "3月",
                "4月",
                "5月",
                "6月",
                "7月",
                "8月",
                "9月",
                "10月",
                "11月",
                "12月",
            ],
            datasets: [
                {
                    label: `營收`,
                    type: "bar",
                    data: revenueChart,
                    backgroundColor: "rgb(105, 189, 56, 0.7)",
                    yAxisID: "A",
                },
                {
                    label: `訂單數`,
                    type: "bar",
                    fill: false,
                    data: ordersChart,
                    backgroundColor: "rgb(54, 134, 125, 0.7)",
                    lineTension: 0,
                    pointRadius: 0,
                    yAxisID: "B",
                },
            ],
        },
        options: {
            title: {
                display: true,
                text: `${thisYear}年各月總營收及訂單筆數`,
                fontSize: "24",
            },
            scales: {
                yAxes: [
                    {
                        id: "A",
                        position: "left",
                        ticks: {
                            suggestedMin: 0,
                            suggestedMax: 500,
                            stepSize: 50,
                            callback: function (value, index, values) {
                                return value + "k";
                            },
                        },
                    },
                    {
                        id: "B",
                        position: "right",
                        ticks: {
                            suggestedMin: 0,
                            suggestedMax: 100,
                            stepSize: 10,
                            callback: function (value, index, values) {
                                return value + "筆";
                            },
                        },
                        gridLines: {
                            display: false,
                        }
                    },
                ],
            },
            responsive: true,
            maintainAspectRatio: false
        },
    });

    const chart2 = new Chart(ctx_createRoom, {
        type: "line",
        data: {
            labels: [
                "Q1",
                "Q2",
                "Q3",
                "Q4",
            ],
            datasets: [
                {
                    data: listingChart,
                    borderColor: "#f00",
                    borderWidth: 1,
                    fill: true,
                    backgroundColor: "#faa5",
                    lineTension: 0,
                    //pointStyle: 'star',
                    pointRadius: 1,
                },
            ],
        },
        options: {
            title: {
                display: true,
                text: `${thisYear}年房源數成長數據(每季)`,
                fontSize: "24",
            },
            legend: {
                display: false,
            },
            scales: {
                yAxes: [{
                    ticks: {
                        suggestedMax: 100,
                        stepSize: 10,
                        callback: function (value, index, values) {
                            return value + "";
                        },
                    },
                }],
            },
            responsive: true,
            maintainAspectRatio: false
        },
    });

    const chart3 = new Chart(ctx_register, {
        type: "line",
        data: {
            labels: [
                "Q1",
                "Q2",
                "Q3",
                "Q4",
            ],
            datasets: [
                {
                    data: membersChart,
                    borderColor: "#00f",
                    borderWidth: 1,
                    fill: true,
                    backgroundColor: "#aaf5",
                    /*lineTension: 0,*/
                    //pointStyle: 'star',
                    pointRadius: 1,
                },
            ],
        },
        options: {
            title: {
                display: true,
                text: `${thisYear}用戶數成長數據(每季)`,
                fontSize: "24",
            },
            legend: {
                display: false,
            },
            scales: {
                yAxes: [{
                    ticks: {
                        suggestedMax: 100,
                        stepSize: 10,
                        callback: function (value, index, values) {
                            return value + "人";
                        },
                    },
                }],
            },
            responsive: true,
            maintainAspectRatio: false
        },
    });

    const chart4 = new Chart(ctx_gender, {
        type: "pie",
        data: {
            labels: ["女性用戶", "男性用戶", "不透漏"],
            datasets: [
                {
                    data: genderChart,
                    backgroundColor: [
                        "rgba(255, 99, 132, 0.6)",
                        "rgba(54, 162, 235, 0.6)",
                        "rgba(255, 206, 86, 0.6)",
                    ],
                    borderColor: [
                        "rgba(255, 99, 132, 1)",
                        "rgba(54, 162, 235, 1)",
                        "rgba(255, 206, 86, 1)",
                    ],
                    borderWidth: 1,
                },
            ],
        },
        options: {
            title: {
                display: true,
                text: "用戶男女比例",
                fontSize: "24",
            },
            animation: {
                duration: 1000,
            },
            responsive: true,
            maintainAspectRatio: false
        },
    });
}


async function init() {
    const token = Cookies.get('token')
    await axios.get(apiUrl,{
        headers: {
            authorization: `Bearer ${ token }`
        }
    })
        .then(res => {
            apiData = res.data
            totalMembers = apiData.totalMembers
            thisMonthOrders = apiData.thisMonthOrders
            thisYearOrders = apiData.thisYearOrders
            thisMonthRevenue = apiData.thisMonthRevenue
            thisYearRevenue = apiData.thisYearRevenue
            everyMonthOrders = apiData.everyMonthOrders
            everyMonthRevenue = apiData.everyMonthRevenue
            everySeasonListing = apiData.everySeasonListing
            everySeasonMembers = apiData.everySeasonMembers
            membersGender = apiData.membersGender
        }).catch(ex => {
            console.log(ex)
        })
}
async function data() {
    console.log(everyMonthOrders)

    const getData = (x) =>  x[0][''] ?? 0
    const getData2 = (x) =>  ((x[0][''] ?? 0)/1000)
    
    ordersChart = everyMonthOrders.map(getData)
    revenueChart = everyMonthRevenue.map(getData2)
    listingChart = everySeasonListing.map(getData)
    membersChart = everySeasonMembers.map(getData)
    genderChart = membersGender.map(getData)
    
    // everyMonthOrders.forEach(x => {
    //     let data = x[0][""] != null ? x[0][""] : 0 
    //     ordersChart.push(data)
    // })

    // everyMonthRevenue.forEach(x => {
    //     let data = x[0][""] != null ? x[0][""] : 0
    //     revenueChart.push(data/1000)
    // })
    // everySeasonListing.forEach(x => {
    //     let data = x[0][""] != null ? x[0][""] : 0
    //     listingChart.push(data)
    // })
    // everySeasonMembers.forEach(x => {
    //     let data = x[0][""] != null ? x[0][""] : 0
    //     membersChart.push(data)
    // })
    // membersGender.forEach(x => {
    //     let data = x[0][""] != null ? x[0][""] : 0
    //     genderChart.push(data)
    // })
}


