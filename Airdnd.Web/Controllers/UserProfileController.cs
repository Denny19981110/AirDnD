using Airbnb.ViewData;
using Airbnb.ViewModel;
using Airdnd.Web.Models.DtoModels;
using Airdnd.Web.Services;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Airbnb.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly UserAccountService _userAccountService;
        private readonly PhotoService _photoService;
        public UserProfileController(UserAccountService userAccountService, PhotoService photoService)
        {
            _userAccountService = userAccountService;
            _photoService = photoService;
        }

        //使用者帳號頁面
        [Authorize]
        public IActionResult UserProfileIndex()
        {
            int userId = 0;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Identity.Name);
            };
            var user = _userAccountService.GetUser(userId);

            user.Image ??= "https://res.cloudinary.com/dbp76raxc/image/upload/v1664589756/defaultIMG_rm314m.jpg";

            var result = new UserProfileViewModel
            {
                
                User = new UserProfileViewModel.UserProfileData
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    UserEmail = user.Email,
                    AboutMe = user.AboutMe,
                    Address = user.Address,
                    Image = user.Image,
                }
            };

            return View(result);
        }

        //更新使用者資訊(不含照片)
        [HttpPost]
        [Authorize]
        public IActionResult Update(UpdateUserDto input)
        {
            int userId = 0;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Identity.Name);
            };
            input.Id = userId;
            _userAccountService.UpdateUser(input);

            return RedirectToAction("UserProfileIndex");
        }

        //編輯使用者照片
        [HttpPost]
        [Authorize]
        public IActionResult UpdatePhoto(List<IFormFile> file)
        {
            int userId = 0;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Identity.Name);
            };
            var result = _photoService.UploadPhotos(file);

            if (result.IsSuccess == false)
            {
                return View("EditPhoto", new UserProfileViewModel
                {
                    Message = result.Message,
                    IsSuccess = false,
                    User = new UserProfileViewModel.UserProfileData
                    {
                        Id = userId,
                        Image = _userAccountService.GetUser(userId).Image
                    }
                });
            }else
            {
                if (result.IsSuccess == true)
                {
                    _userAccountService.UpdatePhoto(new UpdateUserDto
                    {
                        Id = userId,
                        Image = result.PhotoList.First()
                    });
                }
                return RedirectToAction("EditPhoto");
            }
        }

        //使用者照片
        [HttpGet]
        [Authorize]
        public IActionResult EditPhoto()
        {
            int userId = 0;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.Identity.Name);
            };
            var user = _userAccountService.GetUser(userId);
            if (user.Image == null || user.Image == String.Empty)
            {
                user.Image = "https://res.cloudinary.com/dbp76raxc/image/upload/v1664589756/defaultIMG_rm314m.jpg";
            }
            else
            {
                user.Image = user.Image;
            }
            var result = new UserProfileViewModel
            {
                User = new UserProfileViewModel.UserProfileData
                {
                    Id = user.Id,
                    Image = user.Image,
                }
            };
            return View(result);
        }

        //刪除照片
        [HttpPost]
        [Authorize]
        public IActionResult RemoveUserPhoto(int id)
        {
            try
            {
                _userAccountService.DeleteUserPhoto(id);
                return RedirectToAction("EditPhoto");
            }
            catch
            {
                return View("ErrorPage");
            }
        }
    }
}
