const app = new Vue({
    el: '#app',
    data: {
        fields: [
            { key: 'orderId', label:'訂單編號',sortable:true},
            { key: 'userAccountId', label: '會員編號', sortable: true },
            { key: 'listingName', label: '房源名稱' },
            { key: 'endDate', label:'完成訂單時間',sortable:true},
            { key: 'price', label:'訂單價格'},
            {
                key: 'tranStatus', label: '撥款狀態', sortable: true,
                formatter(sta) {
                    let tranStatus
                    switch (sta) {
                        case 1:
                            tranStatus = '未完成'
                            break
                        case 2:
                            tranStatus = '未撥款'
                            break
                        case 3:
                            tranStatus = '已撥款'
                            break
                    }
                    return tranStatus
                }
            },

            { key: 'detail', label: '' },
            { key: 'grant', label: ''},
            
        ],
        orderList: [],
        orderfilterList: [],
        filter: null,
        filterOn: [],
        totalRows: 1,
        perPage: 10,
        currentPage: 1,
        isFiltering: false,
    },
    mounted() {
        const url = '/api/TransactionApi/GetOrder'
        const token = Cookies.get('token')
       
        axios
            .get(url, {
                headers: {
                    'Content-type': 'application/json',
                    authorization: `Bearer ${token}`
                }
            })
            .then(res => {
                this.orderList = res.data
                this.totalRows = res.data.length
            })
            .catch(err => {
                console.log(err)
            })
    },
    methods: {
        grant(item) {
            const token = Cookies.get('token')
            const url = '/api/TransactionApi/ChangeTranStatus'
            axios
                .put(
                    url,
                    { OrderId : item.orderId } ,
                    {
                        headers: { authorization: `Bearer ${token}` }
                    }
                )
                .then(res => {
                    if (res.status === 200) {
                        item.tranStatus = 3
                    }
                })
                .catch(err => {
                    console.log(err)
                })
        },
        onFiltered(items) {
            this.totalRows = items.length
            this.currentPage = 1
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
                this.totalRows = this.orderfilterList.length
            }
        }
    }
})