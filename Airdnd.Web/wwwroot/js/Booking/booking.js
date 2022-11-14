const vm = new Vue({
    el: '#app',
    data: {
        inputData: {
            paytype:''
        },
        inputDataCheck: {
            paytypeError: false,
            paytypeErrorMsg:''
        }
    },
    computed: {
        isVerify() {
            for (let prop in this.inputDataCheck) {
                if (this.inputDataCheck[prop] == true) {
                    return false
                }
            }
            return true
        }
    },
    watch: {
        'inputData.paytype': {
            handler: function () {
                if (this.inputData.paytype == '') {
                    this.inputDataCheck.paytypeError = true
                    this.inputDataCheck.paytypeErrorMsg = '請選擇付款方式'
                }
                else {
                    this.inputDataCheck.paytypeError = false
                    this.inputDataCheck.paytypeErrorMsg = ''
                }
            }
        }
    }
})