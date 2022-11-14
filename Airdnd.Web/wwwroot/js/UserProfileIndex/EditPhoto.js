const image_input = document.querySelector("#update-photo-input");
const btn = document.querySelector('.submitBtn')

//test
image_input.addEventListener("change", function () {
    const reader = new FileReader();
    btn.removeAttribute('disabled')
    reader.addEventListener("load", () => {
        const uploaded_image = reader.result;
        document.querySelectorAll(".card-img-middle").forEach(img => {
            img.src = `${uploaded_image}`
        })
    });
    reader.readAsDataURL(this.files[0]);
});