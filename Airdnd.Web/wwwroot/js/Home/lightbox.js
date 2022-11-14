let btnLike = document.querySelectorAll('.like-btn')
let btnClose = document.querySelectorAll('.btn-close')
const btnCreate = document.querySelector('.btn-create')

const inputName = document.querySelector('#new-input')

let overlayGroup = document.querySelectorAll('.lightbox')

let lightboxAdd = document.querySelector('.new-wishlist')
let clickAdd = document.querySelector('.add-new-list')
let wishlistAll = document.querySelectorAll('.per-wishlist')

const addWishUrl = `https://${location.host}/api/WishlistApi/AddListing`
const deleteUrl = `https://${location.host}/api/WishlistApi/DeleteListing`
const addNewListUrl = `https://${location.host}/api/WishlistApi/NewWishlist`

window.onload = () => {
    document.body.style.overflow = 'scroll'
}

let listingId = 0
let wishlistId = 0
btnLike.forEach(btn => {
    btn.addEventListener('click', event => {
        if (userId == 0) {
            var loginModal = new bootstrap.Modal(document.getElementById('loginModal'), {
                keyboard: false
            })
            loginModal.toggle()
        } else {
            if (!btn.classList.contains('like-check')) {
                $('.wishlist').show()
                let listingId = event.currentTarget.dataset.id

                wishlistAll.forEach(list => {
                    list.onclick = function (event) {
                        let wishlistId = event.currentTarget.dataset.id
                        let body = {
                            ListingId: listingId,
                            WishlistId: wishlistId
                        }

                        fetch(addWishUrl, {
                            method: 'POST',
                            headers: { 'Content-Type': 'application/json' },
                            body: JSON.stringify(body)
                        })
                            .then(res => {
                                if (res.status === 200) {
                                    btn.classList.add('like-check')
                                }
                            })
                            .catch(err => {
                                console.warn(err)
                            })
                        $('.lightbox').hide()
                    }
                })
                btnCreate.onclick = () => {
                    let groupName = inputName.value
                    let body = {
                        ListingId: listingId,
                        GroupName: groupName
                    }
                    fetch(addNewListUrl, {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(body)
                    }).then(res => {
                        if (res.status === 200) {
                            btn.classList.add('like-check')
                            location.reload()
                        }
                    })
                    inputName.value = ''
                }
                event.stopPropagation()
            } else {
                let listingId = event.currentTarget.dataset.id
                let body = {
                    ListingId: listingId,
                    WishlistId: wishlistId
                }
                fetch(deleteUrl, {
                    method: 'DELETE',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(body)
                })
                    .then(res => {
                        if (res.status === 200) {
                            btn.classList.remove('like-check')
                        }
                    })
                    .catch(err => console.error(err))
                event.stopPropagation()
            }
        }
    })
})

overlayGroup.forEach(overlay => {
    overlay.onclick = e => {
        if ($(e.target).has('.lightbox-container').length) {
            $('.lightbox').hide()

            e.stopPropagation()
            document.body.style.overflow = 'overlay'
        }
        if ($(e.target).has('.area').length) {
            $('.search-bar-container').addClass('before')
            $('.lightbox').hide()
            e.stopPropagation()
            if (window.innerWidth <= 768) {
                $('.md-btn-container').show()
                e.stopPropagation()
            }
            document.body.style.overflow = 'overlay'
        }
    }
})

btnClose.forEach(item => {
    item.onclick = event => {
        $('.lightbox').hide()
    }
})

clickAdd.onclick = () => {
    $('.lightbox').hide()
    $('.new-wishlist').show()
}
// 建立新心願單

inputName.onkeyup = () => {
    let words = inputName.value.length
    if (words > 0 && words <= 50) {
        btnCreate.classList.remove('disabled')
    } else {
        btnCreate.classList = 'btn btn-dark btn-create disabled'
    }
}

lightboxAdd.onclick = event => {
    if ($(event.target).has('.lightbox-container').length) {
        $('.lightbox').hide()
    }
}
btnCreate.addEventListener('click', function () {
    $('.lightbox').hide()
})
