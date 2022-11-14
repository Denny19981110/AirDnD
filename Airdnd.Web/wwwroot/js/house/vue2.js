
Vue.use(window.AirbnbStyleDatepicker, {
    colors: {
        selected: '#eb4c60',
        inRange: ' #FF9CB0',
        selectedText: '#fff',
        text: '#565a5c',
        inRangeBorder: '#D93240',
        disabled: '#d3d3d3',
        hoveredInRange: '#eb4c60'
    },
    sundayFirst: "true",
    keyboardShortcuts: [
        { symbol: '↵', label: 'Select the date in focus', symbolDescription: 'Enter key' },
    ],
})

let adult = {
    id: 1,
    titleE: "adult",
    title: "成人",
    description: "13 歲以上",
    count: 1
}
let child = {
    id: 2,
    titleE: "child",
    title: "兒童",
    description: "2 - 12歲",
    count: 0
}
let infant = {
    id: 3,
    titleE: "infant",
    title: "嬰幼兒",
    description: "2 歲以下",
    count: 0
}
let personCatergory = [
    adult, child, infant
]



var vm = new Vue({
    el: '#app',
    data: {
        houseId,
        commentCode,
        discount: 10,
        maxPerson,
        personCatergory,
        awardtemplate: [],
        perNight,
        date1: '',
        date2: '',
        disableDays,
        specialDays,
        cheapDate:[],
        expensiveDate: [],
        count
    },
    filters: {
        tips(money) {
            return money + Math.round(money * 0.1, 0) + Math.round(money * 0.1, 0)
        },
        moneyFomat(money) {
            return money
        },
        showpersent(money) {
            return money + "%"
        },
        dateFomat(day) {
            return day.toString("YYYY-MM-DD")
        }
    },
    methods: {
        apply() {
            console.log(this.date1);
        },
        subtraction(i) {
            if (i.title == "成人" && i.count > 1) {
                i.count--
            } else if (i.title != "成人" && i.count > 0) {
                i.count--
            }
        },
        add(i) {
            if (i.title == "嬰幼兒" && i.count < 5) {
                i.count++
            } else if (i.title != "嬰幼兒" && this.countperson < this.maxPerson) {
                i.count++
            }
        },
        translate(e, i) {
            fetch('https://api.cognitive.microsofttranslator.com/translate?api-version=3.0&to=zh-tw', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Ocp-Apim-Subscription-Key': 'cad4d172c71c47b6a7d6571d96ae8736',
                    'Ocp-Apim-Subscription-Region': 'eastasia'
                },
                body: JSON.stringify([{ "text": e.commentContent }])
            })
                .then(response => {
                    return response.json();
                })
                .then(data => {
                    console.log(data[0].translations[0].text.toString());
                    this.commentCode[i].commentContent = data[0].translations[0].text.toString();
                })
                .catch(error => console.error('Error:', error))
        },
        sendJudge() {

            if (this.date1 !== "" && this.date2 !== "" && userId != 0) {
                this.getPosts()
            } else {
                if (userId == 0) {
                    toastr["info"]("結帳前請先登入");
                };
                if (this.date1 === "" && this.date2 === "") {
                    toastr["info"]("請輸入日期");
                }
            }
        },
        getPosts() {
            fetch('/Booking/Index', {
                method: 'POST',
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json",
                },
                body: JSON.stringify({
                    houseId: this.houseId,
                    startDate: this.date1,
                    lastDate: this.date2,
                    total: this.total,
                    adult: this.personCatergory[0].count,
                    child: this.personCatergory[1].count,
                    infant: this.personCatergory[2].count,
                })
            })
                .then(response => {
                    console.log('Success:', this);
                    window.location.href = '/Booking/Index'
                })
                .catch(error => console.error('Error:', error))

        },
        cheapDateMaker() {
            if (this.specialDays != null) {

                if (this.specialDays.cheapDays != null) {
                    this.cheapDate = this.specialDays.cheapDays.map(x => x.datetime)
                }

            }
        },
        expensiveDateMaker() {
            if (this.specialDays != null) {
                if (this.specialDays.expensiveDays != null) {
                    this.expensiveDate = this.specialDays.expensiveDays.map(x => x.datetime)
                }
            }
        },
        addDays(date, i) {
            let day = new Date(date)
            dateTime = day.setDate(day.getDate() + i);
            dateTime = new Date(dateTime);
            dateTime = `${dateTime.getFullYear()}-${dateTime.getMonth()+1}-${dateTime.getDate()}`
            return dateTime
        }
    },
    async created() {
        this.cheapDateMaker();
        this.expensiveDateMaker();
        try {
            let res = await axios.get("https://raw.githubusercontent.com/KamiiLiu/FileStorage/main/reward.json")
            let rewardArr = []
            while (rewardArr.length < 3) {
                let num = Math.floor(Math.random() * 19)
                if (rewardArr.indexOf(res.data[num]) == -1) {
                    rewardArr.push(res.data[num])
                }
            }
            this.awardtemplate = rewardArr.sort(function (a, b) {
                return a.id - b.id;
            });
        } catch (e) {
            console.log(e)
        }
    },
    computed: {
        countperson() {
            return this.personCatergory[0].count + this.personCatergory[1].count;
        },
        dayDiff() {
            let dayduren = (new Date(this.date2.toString()) - new Date(this.date1.toString())) / (1000 * 3600 * 24);
            if (isNaN(dayduren)) {
                return 0
            } else {
                return dayduren
            }
        },
        total() {
            if (this.date1 != 0 && this.date2 != 0) {
                
                let sum = 0;
                for (let i = 0; i < this.dayDiff; i++) {
                    let day = vm.addDays(this.date1, i)
                    if(this.disableDays != null && this.disableDays.includes(day)) {
                        this.date1 = 0;
                        this.date2 = 0;
                        break;
                    } else if (this.cheapDate != null && this.cheapDate.includes(day)) {
                        let index = this.cheapDate.indexOf(day)
                        sum = sum + specialDays.cheapDays[index].price
                    } else if (this.expensiveDate != null && this.expensiveDate.includes(day)) {
                        let index = this.expensiveDate.indexOf(day)
                        sum = sum + specialDays.expensiveDays[index].price
                    } 
                    else {
                        sum = sum + this.perNight
                    }
                }
                return sum
            } else {
                return 0
            }
            
            },
        now() {
            return new Date().toDateString();
        },
        time() {
            return this.dayDiff == 0 ? "Select dates" : this.date1 + "~" + this.date2
        }
    }
})

