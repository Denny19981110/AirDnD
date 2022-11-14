
let caheckEmail = new Vue({
    el: '#checkEmailArea',
    data: {
        email: '',
        emailCheck: {
            emailError: false,
            emailErrMsg: ''
        }
    },
    computed: {
        isVerify() {
            for (let prop in this.emailCheck) {
                if (this.emailCheck[prop] == true) {
                    return false
                }
            }
            return true
        }
    },
    watch: {
        email: function () {
            var isMail = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(“.+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if (!isMail.test(this.email) && this.email != '') {
                this.emailCheck.emailError = true;
                console.log(this.emailCheck.emailError)
                this.emailCheck.emailErrMsg = 'email格式錯誤';
            }
            else {
                this.emailCheck.emailError = false;
            }
        }
    }
})

let Password = new Vue({
    el: '#PasswordArea',
    data: {
        password: '',
        passwordError: false,
        passErrMsg: ''
    },
    watch: {
        password: function () {
            var isText = /^[a-zA-z0-9]+$/;
            var include = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,15}$/;

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
        }
    }
})

let Signin = new Vue({
    el: '#SigninArea',
    data: {
        firstName: '',
        firstNameError: false,
        firstNameErrMsg: '',
        lastName: '',
        lastNameError: false,
        lastNameErrMsg: '',
        birthDay: '',
        birthDayError: false,
        birthDayErrMsg: '',
        password: '',
        SigninCheck: {
            passwordError: false,
            passErrMsg: ''
        }
    },
    computed: {
        isVerify() {
            for (let prop in this.SigninCheck) {
                if (this.SigninCheck[prop] == true) {
                    return false
                }
            }
            return true
        }
    },
    watch: {
        firstName: function () {
            if (this.firstName == '') {
                this.firstNameError = true;
                this.firstNameErrMsg = '名字不得為空';
            }
            else {
                this.firstNameError = false;
            }
        },
        lastName: function () {
            if (this.lastName == '') {
                this.lastNameError = true;
                this.lastNameErrMsg = '姓氏不得為空';
            }
            else {
                this.lastNameError = false;
            }
        },
        birthDay: function () {
            const ageDifMs = Date.now() - new Date(this.birthDay).getTime();
            const ageDate = new Date(ageDifMs);
            const age = Math.abs(ageDate.getUTCFullYear() - 1970);

            if (this.birthDay == '') {
                this.birthDayError = true;
                this.birthDayErrMsg = '生日不得為空';
            }
            else if (age < 18) {
                this.birthDayError = true;
                this.birthDayErrMsg = '年齡不能小於18歲';
            }
            else {
                this.birthDayError = false;
            }
        },
        password: function () {
            var isText = /^[a-zA-z0-9]+$/;
            var include = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,15}$/;

            if (!isText.test(this.password) && this.password != '') {
                this.SigninCheck.passwordError = true;
                this.SigninCheck.passErrMsg = '請勿包含特殊字元';
            }
            else if (this.password.length < 6) {
                this.SigninCheck.passwordError = true;
                this.SigninCheck.passErrMsg = '請勿少於6個字';
            }
            else if (this.password.length > 15) {
                this.SigninCheck.passwordError = true;
                this.SigninCheck.passErrMsg = '請勿超過15個字';
            }
            else if (!include.test(this.password)) {
                this.SigninCheck.passwordError = true;
                this.SigninCheck.passErrMsg = '至少包括一個大小寫字母或數字';
            }
            else {
                this.SigninCheck.passwordError = false;
            }
        }
    }
})

let forgetPassword = new Vue({
    el: '#forgetPasswordArea',
    data: {
        email: '',
        emailError: false,
        emailErrMsg: ''
    },
    watch: {
        email: function () {
            var isMail = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(“.+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            if (!isMail.test(this.email) && this.email != '') {
                this.emailError = true;
                this.emailErrMsg = 'email格式錯誤';
            }
            else {
                this.emailError = false;
            }
        }
    }
})

let phoneVerification = new Vue({
    el: '#phoneVerification',
    data: {
        phone: '',
        phoneError: false,
        phoneErrMsg: ''
    },
    watch: {
        phone: function () {
            var isPhone = /^(09)[0-9]{8}$/;
            if (!isPhone.test(this.phone) && this.phone != '') {
                this.phoneError = true;
                this.phoneErrMsg = '電話號碼格式錯誤';
            }
            else {
                this.phoneError = false;
            }
        }
    }
})