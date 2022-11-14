let stepCheckEmail = document.querySelector("#step-checkEmail")
let stepLogin = document.querySelector("#step-login")
let stepSignup = document.querySelector("#step-signup")
let stepForgetPassword = document.querySelector("#step-forgetPassword")
let stepSendResetPassword = document.querySelector("#step-sendResetPassword")
let stepPromise = document.querySelector("#step-promise")
let stepReject = document.querySelector("#reject")
let signupProfile = document.querySelector("#step-signupProfile")
let signupPhone = document.querySelector("#step-signup-phone")
let signupPhoto = document.querySelector("#step-signup-photo")
let closeBtn = document.querySelector("#closeBtn")


//Layout
let noLogging = document.querySelector("#no-logging") 
let logged = document.querySelector("#logged")

//帳號是否存在
let checkEmail = document.querySelector("#check-email")
let continueBtn = document.querySelector("#continuebtn")

//註冊
let signupBtn = document.querySelector("#signupbtn")
let firstName = document.querySelector("#first-name")
let lastName = document.querySelector("#last-name")
let birthday = document.querySelector("#birthday")
let email = document.querySelector("#email")
let password = document.querySelector("#password")

//密碼登入
let loginPassword = document.querySelector("#login-password")
let loginBtn = document.querySelector("#loginbtn")
let forgetPasswordLink = document.querySelector("#forget-password-link")
let errorPassword = document.querySelector("#errorPassword")
let loginModal = document.querySelector("#loginModal")
let errorMessage = document.querySelector('#errorMessage')
let headers = {
    "Content-Type": "application/json",
    "Accept": "application/json"
}

//忘記密碼
let forgetPasswordEmail = document.querySelector("#forget-password-email")
let forgetPasswordbtn = document.querySelector("#forgetPasswordbtn")


let promiseNextSteps = document.querySelector("#promise-next-steps")
let finishLaterPhone = document.querySelector("#finish-later-phone")
let finishLaterPhoto = document.querySelector("#finish-later-phone")


//建立個人資料

let accept = document.querySelector("#accept")
let cancelBtn = document.querySelector("#cancelBtn")
let backPromise = document.querySelector("#back-promise")
let signupCancel = document.querySelector("#signupCancel")

let signupContinueBtn = document.querySelector("#signup-continue-btn")
let phoneInputVerification = document.querySelector("#phoneInputVerification")
let signupPhoneBtn = document.querySelector("#signup-phone-btn")
//Photo
let fileUpload = document.querySelector("#file-upload")
let memberPhoto = document.querySelector("#memberPhoto")
let uploadPhoto = document.querySelector("#upload-photo")
let signupPhotoBtn = document.querySelector("#signup-photo-btn")

//前一頁
let prevLogin = document.querySelector("#prevLogin")
let prevSignup = document.querySelector("#prevSignup")
let prevForgetPassword = document.querySelector("#prevForgetPassword")
let prevBuildprofilePhone = document.querySelector("#prev-buildprofile-phone")
let prevBuildprofilePhoto = document.querySelector("#prev-buildprofile-phone")




window.addEventListener("load", function () {
    GetGoogleLoginUrl()
    //帳號是否存在
    loginModal.addEventListener('show.bs.modal', event => {
        console.log(event)
        ShowLoginModal()
    })
    continueBtn.addEventListener("click", function () {
        CheckEmail()
    })
    //註冊
    signupBtn.addEventListener("click", function () {
        Signup()
    })
    //密碼登入.
    loginBtn.addEventListener("click", function () {
        Login(checkEmail.value, loginPassword.value, true)
    })
    //忘記密碼
    forgetPasswordLink.addEventListener("click", function () {
        ShowForgetPasswordPage()
    })
    forgetPasswordbtn.addEventListener("click", function () {
        ForgetPassword()
    })
    //註冊後續
    closeBtn.addEventListener("click", function () {
        window.location.reload()
    })
    accept.addEventListener("click", function () {
        Accept()
    })
    cancelBtn.addEventListener("click", function () {
        Cancel()
    })
    backPromise.addEventListener("click", function () {
        BackPromise()
    })
    signupCancel.addEventListener("click", function () {
        SignupCancel()
    })
    promiseNextSteps.addEventListener('hide.bs.modal', event => {
        Login(email.value, password.value, true)
    })
    finishLaterPhone.addEventListener("click", function () {
        FinishLaterPhone()
    })
    finishLaterPhoto.addEventListener("click", function () {
        Login(email.value, password.value, true)
    })
    //建立個人資料
    signupContinueBtn.addEventListener("click", function () {
        SignupContinue()
    })
    signupPhoneBtn.addEventListener("click", function () {
        SignupPhone()
    })
    signupPhotoBtn.addEventListener("click", function () {
        PostPhoto()
    })
    //前一頁
    prevLogin.addEventListener("click", function () {
        PrevLogin()
    })
    prevSignup.addEventListener("click", function () {
        PrevSignup()
    })
    prevForgetPassword.addEventListener("click", function () {
        PrevForgetPassword()
    })
    prevBuildprofilePhone.addEventListener("click", function () {
        PrevBuildProfilePhone()
    })
    //prevBuildProfilePhoto.addEventListener("click", function () {
    //    PrevBuildProfilePhoto()
    //})
    
})


function ShowLoginModal() {
    stepCheckEmail.removeAttribute('hidden')
    continueBtn.removeAttribute('disabled')
    checkEmail.value = ''
    stepLogin.setAttribute('hidden', '')
    stepSignup.setAttribute('hidden', '')
    firstName.value = ''
    lastName.value = ''
    birthday.value = ''
    email.value = ''
    password.value = ''
}
function GetGoogleLoginUrl() {
    axios.get('Login/GetGoogleLoginUrl')
        .then((res) => {
            if (res.status == 200) {

            }
        })
}

function Google_Login() {
    axios.get('Login/GetGoogleLoginUrl')
        .then((res) => {
            if (res.status == 200) {
                window.location.href = res.data
            }
        })
        .catch((err) => {
            console.log(err)
        })
}

function CheckEmail() {
    continueBtn.setAttribute('disabled', '')
    axios.post(`/api/LoginApi/CheckEmail/${checkEmail.value}`)
        .then((res) => {
            if (res.status == 200) {
                stepCheckEmail.setAttribute('hidden', '')
                if (res.data) {
                    //登入
                    stepLogin.removeAttribute('hidden')
                }
                else {
                    //註冊
                    email.value = checkEmail.value
                    stepSignup.removeAttribute('hidden')
                }
            }
        })
        .catch((err) => {
            console.log(err)
            continueBtn.removeAttribute('disabled')
        })
}

function Signup() {
    let data = {
        name: lastName.value + firstName.value,
        birthday: birthday.value,
        email: email.value,
        password: password.value
    }
    signupBtn.setAttribute('disabled', '')
    axios({
        method: 'post',
        url: '/api/LoginApi/Signup',
        data: data,
        headers: headers
    })
        .then((res) => {
            if (res.data.status != 2000) {
                //註冊失敗
                console.log(res.data.msg)
            }
        })
        .catch((err) => {
            console.log(err)
            signupBtn.removeAttribute('disabled')
        })
}
function Login(email, password, isReload) {
    let data = {
        email: email,
        password: password
    }
    loginBtn.setAttribute('disabled', '')
    axios({
        method: 'post',
        url: '/api/LoginApi/Login',
        data: data,
        headers: headers
    })
        .then((res) => {
            if (res.data.status == 2000) {
                if (isReload)
                {
                    loginModal.hidden = true
                    window.location.reload();
                }
            }
            else {
                errorPassword.innerText = res.data.msg
                loginBtn.removeAttribute('disabled')
                //alert("密碼錯誤 請重新操作")
                //window.location.reload();
            }
        })
}

function ShowForgetPasswordPage() {
    stepLogin.setAttribute('hidden', '')
    stepForgetPassword.removeAttribute('hidden')
}

function ForgetPassword() {
    axios.post(`/api/LoginApi/ForgetPassword/${forgetPasswordEmail.value}`)
        .then((res) => {
            if (res.status == 200) {
                if (res.data.status == 2000) {
                    //信箱正確
                    stepForgetPassword.setAttribute('hidden', '')
                    stepSendResetPassword.removeAttribute('hidden')
                }
                else {
                    errorMessage.innerText = res.data.msg
                }
            }
        })
}

function Accept() {
    Login(email.value, password.value, false)
}
function Cancel() {
    stepPromise.setAttribute('hidden', '')
    stepReject.removeAttribute('hidden')
}

function BackPromise(){
    stepReject.setAttribute('hidden', '')
    stepPromise.removeAttribute('hidden')
}

function SignupContinue() {
    signupProfile.setAttribute('hidden', '')
    signupPhone.removeAttribute('hidden')
}

function SignupCancel() {
    axios.post(`/api/LoginApi/DeleteAccount/${email.value}`)
        .then((res) => {
            if (res.data.status == 2000) {
                window.location.reload();
            }
            else {
                console.log(res.data.msg)
            }
        })
}

function SignupPhone() {
    signupPhoneBtn.setAttribute('disabled', '')
    
    let data = {
        email: email.value,
        phone: phoneInputVerification.value
    }
    axios({
        method: 'post',
        url: '/api/LoginApi/SignupPhone',
        data: data,
        headers: headers
    })
        .then((res) => {
            if (res.data.status == 2000) {
                signupPhone.setAttribute('hidden', '')
                signupPhoto.removeAttribute('hidden')
            }
            else {
                console.log(res.data.msg)
            }
        })
}
function SignupPhoto() {
    signupPhotoBtn.setAttribute('disabled', '')
    const form = new FormData()
    form.append('file', fileUpload.files[0])

    axios.post('api/UploadPhotoApi/ConvertPhotoUrl', form)
        .then((res) => {
            uploadPhoto.setAttribute('disabled','')
            console.log(res)
            if (res.status == 200) {
                memberPhoto.setAttribute('src', res.data)
                uploadPhoto.setAttribute('hidden', '')
                signupPhotoBtn.removeAttribute('hidden')
            }
            else {
                console.log(res.data.msg)
            }
        })
}
function PostPhoto() {
    let data = {
        email: email.value,
        phone: phoneInputVerification.value,
        avatarUrl: memberPhoto.src
    }
    axios({
        method: 'post',
        url: '/api/LoginApi/SignupPhone',
        data: data,
        headers: headers
    })
        .then((res) => {
            signupPhotoBtn.setAttribute('disabled', '')
            if (res.data.status == 2000) {
                signupProfilePhone.setAttribute('hidden', '')
                signupProfilePhoto.removeAttribute('hidden')
                window.location.reload()
            }
            else {
                console.log(res.data.msg)
            }
        })
}

function FinishLaterPhone() {
    signupPhone.setAttribute('hidden', '')
    signupPhoto.removeAttribute('hidden')
}

//上一頁
function PrevLogin() {
    stepLogin.setAttribute('hidden', '')
    stepCheckEmail.removeAttribute('hidden')
    continueBtn.removeAttribute('disabled')
    checkEmail.value = ''

}

function PrevSignup() {
    stepSignup.setAttribute('hidden', '')
    stepCheckEmail.removeAttribute('hidden')
    continueBtn.removeAttribute('disabled')
    firstName.value = ''
    lastName.value = ''
    birthday.value = ''
    email.value = ''
    password.value = ''
    checkEmail.value = ''
}


function PrevBuildProfilePhone() {
    signupPhone.setAttribute('hidden', '')
    signupProfile.removeAttribute('hidden')
    signupContinueBtn.removeAttribute('disabled')
}

function PrevBuildProfilePhoto() {
    signupPhoto.setAttribute('hidden', '')
    signupPhone.removeAttribute('hidden')
    signupPhoneBtn.removeAttribute('disabled')
}

function PrevForgetPassword() {
    stepForgetPassword.setAttribute('hidden', '')
    stepLogin.removeAttribute('hidden')
    loginBtn.removeAttribute('disabled')
}