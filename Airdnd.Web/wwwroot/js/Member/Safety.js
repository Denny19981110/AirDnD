
var password = new Vue({
    el: '#passwordArea',
    data: {
        password: '',
        passwordError: false,
        passErrMsg: '',
        newPassword: '',
        newPasswordError: false,
        newPassErrMsg: '',
        checkPassword: '',
        checkPasswordError: false,
        checkPassErrMsg: '',
        updated: '',
        isEdit: false,
        isRead: true
    },
    created() {
        this.getPasswordModifyDate()
    },
    methods: {
        getPasswordModifyDate() {
            axios.get('/api/MemberApi/GetPasswordModifyDate')
                .then((res) => {
                    if (res.status == 200) {
                        this.updated = res.data
                    }
                })
                .catch((error) => { console.log(error) })
        },
        editPasswordChange() {
            this.isEdit = !this.isEdit
            this.isRead = !this.isRead
        },
        editPassword() {
            let data = {
                oldPassword: this.password,
                password: this.newPassword,
                checkPassword: this.checkPassword
            }
            axios.post('/api/MemberApi/EditPassword', data)
                .then((res) => {
                    if (res.data.status == 2000) {
                        this.editPasswordChange()
                        this.getPasswordModifyDate()
                    }
                    else {
                        console.log(res.data.msg)
                    }
                })
                .catch((error) => { console.log(error) })
        }
    },
    watch: {
        password: function () {
            var isText = /^[a-zA-Z0-9]+$/;
            var include = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,15}$/;
            console.log(this.password)
            if (!isText.test(this.password) && this.password != '') {
                this.passwordError = true;
                this.passErrMsg = '請勿包含特殊字元';
            }            
            else if (this.password.length < 6) {                
                this.passwordError = true;
                this.passErrMsg = '請勿少於6個字';
            }
            else if (this.password.length > 15) {
                this.passwordError = true;
                this.passErrMsg = '請勿超過15個字';
            }
            else if (!include.test(this.password)) {
                this.passwordError = true;
                this.passErrMsg = '至少包括一個大小寫字母或數字';
            }
            else {
                this.passwordError = false;
            }
        },
        newPassword: function () {
            var isText = /^[a-zA-Z0-9]+$/;
            var include = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,15}$/;
            if (!isText.test(this.newPassword) && this.newPassword != '') {
                this.newPasswordError = true;
                this.newPassErrMsg = '請勿包含特殊字元';
            }
            else if (this.newPassword.length < 6) {
                this.newPasswordError = true;
                this.newPassErrMsg = '請勿少於6個字';
            }
            else if (this.newPassword.length > 15) {
                this.newPasswordError = true;
                this.newPassErrMsg = '請勿超過15個字';
            }
            else if (!include.test(this.newPassword)) {
                this.newPasswordError = true;
                this.newPassErrMsg = '至少包括一個大小寫字母或數字';
            }
            else {
                this.newPasswordError = false;
            }
        },
        checkPassword: function () {
            var isText = /^[a-zA-Z0-9]+$/;
            var include = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,15}$/;
            if (!isText.test(this.checkPassword) && this.checkPassword != '') {
                this.checkPasswordError = true;
                this.checkPassErrMsg = '請勿包含特殊字元';
            }
            else if (this.checkPassword.length < 6) {
                this.checkPasswordError = true;
                this.checkPassErrMsg = '請勿少於6個字';
            }
            else if (this.checkPassword.length > 15) {
                this.checkPasswordError = true;
                this.checkPassErrMsg = '請勿超過15個字';
            }
            else if (!include.test(this.checkPassword)) {
                this.checkPasswordError = true;
                this.checkPassErrMsg = '至少包括一個大小寫字母或數字';
            }
            else if (this.checkPassword != this.newPassword) {
                this.checkPasswordError = true;
                this.checkPassErrMsg = '新密碼與確認密碼不相符';
            }
            else {
                this.checkPasswordError = false;
            }
        },
    }
});


let deactivate = document.querySelector("#deactivate")

window.addEventListener("load", function () {
    deactivate.addEventListener("click", function () {
        Deactivate()
    })
})

function Deactivate() {

}