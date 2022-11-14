let member = new Vue({
    el: '#app',
    data: {
        memberData: {
            name: '',
            gender: '',
            email: '',
            phone: '',
            address: ''
        },
        displayGender: '',
        isEdit: {
            name: false,
            gender: false,
            email: false,
            phone: false
        },
        isRead: {
            name: true,
            gender: true,
            email: true,
            phone: true
        }
    },
    created() {
        this.getAllData()
    },
    methods: {
        getAllData() {
             axios.get('/api/MemberApi/GetMemberData')
                 .then((res) => {
                     if(res.data.status == 2000){
                         this.memberData = res.data.result
                         this.switchGender(this.memberData.gender)
                         this.switchData()
                     }
                     else{
                         console.log(res.data.msg)
                     }
                 })
        },
        switchGender(gender) {
            switch (gender) {
                case 0:
                    this.displayGender = "女性"
                    break
                case 1:
                    this.displayGender = "男性"
                    break
                case 2:
                    this.displayGender = "未指定"
                    break
            }
        },
        switchData() {
            if (this.memberData.phone == null) {
                this.memberData.phone = "新增電話號碼，以便旅客和 Airbnb 能夠與你聯絡。你可以新增其他號碼並選擇其使用方式。"
            }
            if (this.memberData.address == null) {
                this.memberData.address = "未指定"
            }
        },
        editMemberName() {
             axios.post('/api/MemberApi/EditMemberData', this.memberData)
                 .then((res) => {
                     if (res.data.status == 2000) {
                         this.editNameChange()
                         this.getAllData()
                     }
                     else {
                         console.log(res.data.msg)
                     }
                 })
                 .catch((error) => {console.log(error)} )
        },
        editMemberEmail() {
            axios.post('/api/MemberApi/EditMemberData', this.memberData)
                .then((res) => {
                    if (res.data.status == 2000) {
                        this.editEmailChange()
                        this.getAllData()
                        console.log(this.memberData)
                    }
                })
                .catch((error) => { console.log(error) })
        },
        editMemberGender() {
            axios.post('/api/MemberApi/EditMemberData', this.memberData)
                .then((res) => {
                    if (res.data.status == 2000) {
                        this.editGenderChange()
                        this.getAllData()
                    }
                })
                .catch((error) => { console.log(error) })
        },
        editMemberPhone() {
            axios.post('/api/MemberApi/EditMemberData', this.memberData)
                .then((res) => {
                    if (res.data.status == 2000) {
                        this.editPhoneChange()
                        this.getAllData()
                    }
                })
                .catch((error) => { console.log(error) })
        },
        editNameChange() {
            this.isEdit.name = !this.isEdit.name
            this.isRead.name = !this.isRead.name
        },        
        editGenderChange() {
            this.isEdit.gender = !this.isEdit.gender
            this.isRead.gender = !this.isRead.gender
        },        
        editEmailChange() {
            this.isEdit.email = !this.isEdit.email
            this.isRead.email = !this.isRead.email
        },
        editPhoneChange() {
            this.isEdit.phone = !this.isEdit.phone
            this.isRead.phone = !this.isRead.phone
        }
    }
})
