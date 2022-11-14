
const ImgArea = document.querySelector('#gridDemo')
let uploaded_images;
const image_input = document.querySelector("#update-photo-input");
let filesInput = document.querySelector("#files")
//let url = '/SellerRoomInfo/EditRoomImage'
const url = '/api/UploadPhotoApi/ConvertPhotoUrl'
let deleteBtn = document.querySelectorAll('.deleteBtn')
let submitBtn = document.querySelector('.submitBtn')



//CHANGE EVENT FOR UPLOADING PHOTOS
filesInput.addEventListener("change", (e) => { 
    //CHECK IF FILE API IS SUPPORTED
    if (window.File && window.FileReader && window.FileList && window.Blob) { 

        const files = e.target.files; 
        for (let i = 0; i < files.length; i++) { 
            //only the type is photos
            if (!files[i].type.match("image")) continue;
            // read URL
            const picReader = new FileReader();  
            let formdata = new FormData()
            let imgUrl
            formdata.append('file',files[i])
            
            // show photos
            picReader.addEventListener("load", async function (event) { 
                const picFile = event.target;
                const div = document.createElement("div");

                await fetch(url, {
                    method: 'POST',
                    body: formdata
                })
                    .then(res => { return res.text() })
                    .then(res => {
                        imgUrl = res
                        console.log(imgUrl);
                    })

                div.setAttribute("class", "col-12 col-md-6 col-lg-4 img-box grid-square")
                div.innerHTML = `<img src="${imgUrl}" alt="">`;
                div.innerHTML += `<div  class="deleteBtn">X</div>`
                div.innerHTML += `<input type="text" id="file${i}" value=${imgUrl} name="photos" class="d-none uploadImg">`
                ImgArea.appendChild(div);
                $(`.deleteBtn`).click(function () {
                    $(this).parent('.img-box').remove();``
                })
            });
            
            //READ THE IMAGE
            picReader.readAsDataURL(files[i]); 
            

        }
    } else {
        alert("Your browser does not support File API");
    }
});
/*let deleteUrl = '/SellerRoomInfo/DeleteImage'*/
let deleteUrl = '/api/SellerRoomInfoApi/DeleteImage'
deleteBtn.forEach(x=>{
    x.addEventListener('click',function(){
        //get image url
        let src = $(this).parent('.img-box').children('img')[0].src;
        let data = {src: src}
        
        console.log(src)
        console.log(data)
        fetch(deleteUrl,{
            method:'POST',
            headers: {
                'Content-Type': 'application/json'
              },
            //body:JSON.stringify(src)
            body: JSON.stringify(src)
        })
        .then(res => { return res.text() })
        .then(res => {
            let img = res
            console.log(img)
            window.location.reload();
         })

    })
})

let inputs = document.querySelectorAll('.uploadfile')
let uploadData = new FormData()
submitBtn.addEventListener('click',function(){
    inputs.forEach((file)=>{
        console.log(file.value)
        uploadData.append('file',file)
        console.log(uploadData)
    })
})

