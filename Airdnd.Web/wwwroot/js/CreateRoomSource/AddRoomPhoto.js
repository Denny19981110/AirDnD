const ImgArea = document.querySelector('#gridDemo')
const btn = document.querySelector('.container7 .next')
let uploaded_images;
let filesInput = document.querySelector("#files")
const url = '/api/UploadPhotoApi/ConvertPhotoUrl'
const spanner = document.querySelector('.spinner-border')


new Sortable(ImgArea, {
    animation: 150,
    ghostClass: 'blue-background-class'
});


filesInput.addEventListener("change", (e) => { //CHANGE EVENT FOR UPLOADING PHOTOS
    if (window.File && window.FileReader && window.FileList && window.Blob) { //CHECK IF FILE API IS SUPPORTED
        spanner.classList.remove('d-none')
        const files = e.target.files; //FILE LIST OBJECT CONTAINING UPLOADED FILES
        for (let i = 0; i < files.length; i++) { // LOOP THROUGH THE FILE LIST OBJECT
            if (!files[i].type.match("image")) continue; // ONLY PHOTOS (SKIP CURRENT ITERATION IF NOT A PHOTO)
            const picReader = new FileReader(); // RETRIEVE DATA URI 
            picReader.addEventListener("load", async function (event) { // LOAD EVENT FOR DISPLAYING PHOTOS
                const picFile = event.target;
                let formdata = new FormData()
                let imgUrl

                formdata.append('file', files[i])

                //敲Api 圖片轉網址
                await fetch(url, {
                    method: 'POST',
                    body: formdata
                }).then(res => { return res.text() })
                    .then(res => {
                        imgUrl = res
                        console.log(imgUrl)
                    })
                    .catch(ex => consoel.log(ex))

                //渲染畫面 
                const div = document.createElement("div");
                div.setAttribute("class", "col-12 col-md-6 col-lg-4 img-box grid-square")
                div.innerHTML = `<img src="${picFile.result}" alt="">`
                div.innerHTML += `<div class="deleteBtn">X</div>`
                div.innerHTML += `<input type="text" id="files" name="File" class="d-none file" value="${imgUrl}">`
                await ImgArea.appendChild(div);
                spanner.classList.add('d-none')
                fileCount()
                $('.deleteBtn').click(function () {
                    $(this).parent('.img-box').remove();
                    if (document.querySelectorAll('.img-box').length < 6) {
                        btn.setAttribute('disabled', 'disabled')
                        btn.classList.add('fail')
                        btn.classList.remove('accept')
                    }
                    else {
                        btn.removeAttribute('disabled', 'disabled')
                        btn.classList.remove('fail')
                        btn.classList.add('accept')
                    }
                })
            });
            picReader.readAsDataURL(files[i]); //READ THE IMAGE

        }
    } else {
        alert("Your browser does not support File API");
    }
});


function fileCount() {
    if (document.querySelectorAll('.img-box').length < 6) {
        btn.setAttribute('disabled', 'disabled')
        btn.classList.add('fail')
        btn.classList.remove('accept')
    }
    else {
        btn.removeAttribute('disabled', 'disabled')
        btn.classList.remove('fail')
        btn.classList.add('accept')
    }
}

//const image_input = document.querySelector("#update-photo-input");
//const img_group = document.querySelector('.img-Group')

//image_input.addEventListener("change", function () {
//    img_group.innerHTML = ''
//    for (let i = 0; i < image_input.files.length; i++) {
//        let picInfo = this.files[i]
//        let binaryData = []
//        binaryData.push(picInfo)
//        let url = window.URL.createObjectURL(new Blob(binaryData))
//        img_group.innerHTML += `<div class="img-Item col-12 col-md-6 p-2"><img src="${url}"
//                    alt="..." class = "h-100 w-100"> </div>`

//        console.log(picInfo)
//        console.log(url)
//    }


//});