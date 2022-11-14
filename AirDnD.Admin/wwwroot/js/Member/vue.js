var vm = new Vue({
    el: '#app',
    data: {
        fields: [
            { key: 'userAccountId', label: '會員編號', sortable: true },
            { key: 'name', label: '會員名稱' },
            { key: 'connect', label: '聯絡會員' },
            { key: 'Tag', label: '特殊標示' },
            { key: 'createDate', label: '入會時間', sortable: true },
            { key: 'Detail', label: '會員自介' },
        ],
        memberList: [],
        userList: [],
        filterOn: [],
        totalRows: 1,
        perPage: 10,
        currentPage: 1,
        filter: null,
        isFiltering: false, //是否在過濾
    },
    mounted() {
        const url = '/api/Member/GetMemberData'
        const token = Cookies.get('token')
        axios
            .get(url, {
                headers: {
                    'Content-type': 'application/json',
                    authorization: `Bearer ${token}`
                }
            })
            .then(res => {
                this.memberList = res.data
                this.totalRows = res.data.length
            })
            .catch(err => {
                console.log(err)
            })
    },
    methods: {
        onFiltered(filteredItems) {
            this.totalRows = filteredItems.length
            this.currentPage = 1
        },
        Exceler() {
            const worksheet = XLSX.utils.json_to_sheet(this.memberList);
            const workbook = XLSX.utils.book_new();
            XLSX.utils.book_append_sheet(workbook, worksheet, "Dates");
            XLSX.writeFile(workbook, "會員資料.xlsx", { compression: true });
        },
        birthdayNow(date){
            let a=new Date().getMonth()+1
            let b=date
            if(b!=null){
                b=date.split("-")[0]
            }
            if(a==b){
                return true
            }
            return false
        },
        newMember(date){
            let a=new Date().getFullYear()
            let b=date.split("-")[2]
            if(a==b){
                return true
            }
            return false
        }
    },
    watch: {
        filter: function () {
            if (this.filter != null) {
                this.isFiltering = true
            }
            else this.isFiltering = false
        },

        isFiltering: function () {
            if (this.isFiltering) {
                this.totalRows = this.filterOn.length
            }
            else {
                this.totalRows = this.userList.length
            }
        }
    }
})