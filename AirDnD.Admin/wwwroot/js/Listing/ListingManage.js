const mapping = [
    { key: 'id', value: '房源ID' },
    { key: 'listingName', value: '房名' },
    { key: 'host', value: '房東' },
    { key: 'region', value: '地區' },
    { key: 'bedRooms', value: '臥房數' },
    { key: 'beds', value: '床位' },
    { key: 'bathRooms', value: '衛浴' },
    { key: 'createTime', value: '建立時間' },
    { key: 'propertyName', value: '類型' },
    { key: 'status', value: '上架狀態' },
    { key: 'price', value: '價格' }
]
var allListing = []
const appVue = new Vue({
    el: '#listings',
    created() {
        this.$_loadingTimeInterval = null

    },
    mounted() {
        this.startLoading()
        const url = '/api/ListingApi/ListingManage'
        const token = Cookies.get('token')
        function clearLoadingTimeInterval() {
            clearInterval(this.$_loadingTimeInterval)
            this.$_loadingTimeInterval = null
        }
        axios
            .get(url, {
                headers: {
                    'Content-type': 'application/json',
                    authorization: `Bearer ${token}`
                }
            })
            .then(res => {
                this.listingItems = res.data
                this.totalRows = res.data.length
                this.loading = false
                clearLoadingTimeInterval()
            })
            .catch(err => {
                console.log(err)
            })
    },
    data: {
        fields: [
            { key: 'listingId', label: '房源ID', sortable: true },
            { key: 'listingName', label: '房名' },
            { key: 'host', label: '房東' },
            { key: 'region', label: '地區', sortable: true },
            { key: 'price', label: '基本價錢' },
            { key: 'createTime', label: '創建時間' },
            {
                key: 'status',
                label: '狀態',
                sortable: true,
                formatter(value) {
                    let result
                    switch (value) {
                        case 0:
                            result = '編輯中'
                            break
                        case 1:
                            result = '上架中'
                            break
                        case 2:
                            result = '已下架'
                            break
                    }
                    return result
                }
            },
            { key: 'detail', label: '' },
            { key: 'update', label: '' }
        ],
        isBusy: true,
        listingItems: [],
        totalRows: 1,
        filter: '',
        token: Cookies.get('token'),
        currentPage: 1,
        perPage: 9,
        filterOn: ['host', 'region', 'listingName'],
        isLaunch: 1,
        loading: false,
        loadingTime: 0,
        maxLoadingTime:3
    },
    filters: {
        mappingFilter(val) {
            return mapping.find(x => x.key === val)?.value ?? val
        }
    },
    methods: {
        takeDown(item, url) {
            axios
                .put(
                    url,
                    { ListingId: item.listingId, Status: item.status },
                    {
                        headers: { authorization: `Bearer ${this.token}` }
                    }
                )
                .then(res => {
                    console.log(res)
                    if (res.status === 200) {
                        item.status = 2
                    }
                })
                .catch(err => console.log(err))
        },
        launch(item, url) {
            axios
                .put(
                    url,
                    { ListingId: item.listingId, Status: item.status },
                    {
                        headers: { authorization: `Bearer ${this.token}` }
                    }
                )
                .then(res => {
                    if (res.status === 200) {
                        item.status = 1
                    }
                })
                .catch(err => console.log(err))
        },
        changeStatus(item) {
            const url = '../api/ListingApi/ChangeStatus'

            if (item.status == 1) {
                this.$bvModal
                    .msgBoxConfirm('確定要下架房源', {
                        title: '警告',
                        size: 'sm',
                        okVariant: 'danger',
                        okTitle: '確認',
                        cancelTitle: '取消',
                        hideHeaderClose: true,
                        centered: true
                    })
                    .then(value => {
                        if (value === true) {
                            this.takeDown(item, url)
                        }
                    })
                    .catch(err => {
                        console.warn(err)
                    })
            } else {
                this.$bvModal
                    .msgBoxConfirm('確定要重新上架房源', {
                        title: '警告',
                        size: 'sm',
                        okVariant: 'success',
                        okTitle: '確認',
                        cancelTitle: '取消',
                        hideHeaderClose: true,
                        centered: true
                    })
                    .then(value => {
                        if (value === true) {
                            this.launch(item, url)
                        }
                    })
                    .catch(err => {
                        console.warn(err)
                    })
            }
        },
        startLoading() {
            this.loading = true
            this.loadingTime = 0
        },
        onFiltered(items){
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
                this.totalRows = this.listingItems.length
            }
        }
    }
})
