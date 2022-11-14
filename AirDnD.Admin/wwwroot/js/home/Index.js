(function IIFE() {
  const api = {
    getUserName: '/Auth/GetUserName',
    getClaims:'/Auth/GetClaims'
  }

  const apiCaller = {
    getUserName: () => httpGet(api.getUserName),
    getClaims: ()=> httpGet(api.getClaims)
  }

  const homeIndexVue = new Vue({
    el: '#home-index',
    data: {
      username: ''
    },
    methods: {
      clickMe() {
         const token = Cookies.get('token')
         axios.get('/api/Auth/GetUserName', {
             headers: {
                 authorization: `Bearer ${token}`
             }
         })
             .then((res) => {
                 console.log(res) 
                 this.username = res.data.body

             }).catch((err) => {
                 console.log(err.response.request.status === 401) 
                 window.location.href = '/login'
             })

        //apiCaller.getUserName()
        //    .then((res) => {
        //        this.username = res.data.body
        //        console.log(res)
        //    })
            //return apiCaller.getClaims()
            //.then(res => {
            //    console.log(res)
            //})
            //.catch(err => {
            //    console.log(err)
            //})
      }
    }
  })
})()