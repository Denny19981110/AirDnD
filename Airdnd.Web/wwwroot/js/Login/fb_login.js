
    //載入和初始化FB SDK
    window.fbAsyncInit = function() {
        FB.init({
            appId: '791166918783048',
            cookie: true,
            xfbml: true,
            version: 'v13.0'
        });

    FB.AppEvents.logPageView();   
      
    };

    (function(d, s, id){
         var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) {return;}
        js = d.createElement(s); js.id = id;
        js.src = "https://connect.facebook.net/en_US/sdk.js";
        fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));

//登入FB登入按鈕呼叫
function checkLoginState() {
    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });
}

function statusChangeCallback(response) {
    if (response.status === 'connected') {
        //已登入
        getUserData()
    }
    else {
        loginFB()
    }
}

function getUserData() {
    FB.api('/me', 'GET', { "fields": "id, name, email" }, function (response) {

        let userData = {
            Name: response.name,
            Email: response.email
        }
        fetch(`/api/LoginApi/FacebookLogin`, {
            headers: {
                "Accept": "application/json, text/plain",
                "Content-Type": "application/json;charset=UTF-8"
            },
            method: "Post",
            body: JSON.stringify(userData)
        })
            .then(function (res) {
                console.log(res)
                if (res.status == 200) {
                    window.location.href = '/Home/Index'
                }
            })
    })
}

function loginFB() {
    FB.login(function (response) {
        getUserData()
    },
    { scope: 'public_profile, email' })
}
