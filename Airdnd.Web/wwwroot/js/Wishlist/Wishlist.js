let inputNameOld = document.querySelector('#old-input')
const btnSave = document.querySelector('.btn-save')

const wishId = document.querySelector('.wishlist-name').dataset.id
const changeNameUrl = '../api/WishlistApi/UpdateName'
const deleteWishUrl = '../api/WishlistApi/DeleteWishList'
function closeLightBox() {
  $('#set-wishlist').hide()
}
$('.btn-set-wishlist').click(function () {
  $('#set-wishlist').show()
  let listName = document.querySelector('.wishlist-name')
  inputNameOld.value = listName.innerText
})

$('.cancel-change').click(closeLightBox)
$('.btn-save').click(() => {
  let newName = inputNameOld.value
  body = {
    WishlistId: wishId,
    GroupName: newName
  }
  fetch(changeNameUrl, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(body)
  }).then(res => {
    if (res.status === 200) {
      location.reload()
    }
  })

  closeLightBox()
})
function openConfirm() {
  closeLightBox()
  $('.delete-confirm').show()
}
function rollback() {
  closeLightBox()
  $('#set-wishlist').show()
}
function deleteWishlist() {
  body = {
    WishlistId: wishId
  }
  fetch(deleteWishUrl, {
    method: 'Delete',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(body)
  }).then(res => {
    if (res.status === 200) {
      window.location = '../UserWishList/UserWishList'
    }
  })
}

inputNameOld.onkeyup = () => {
  let words = inputNameOld.value.length
  if (words > 0 && words <= 50) {
    btnSave.classList.remove('disabled')
  } else {
    btnSave.classList.add = 'btn btn-dark btn-save disabled'
  }
}
