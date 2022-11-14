using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Airdnd.Web.Models;
using Airdnd.Core.Interfaces;
using System.Collections.Generic;
using Airdnd.Web.Models.DtoModels;
using CloudinaryDotNet.Actions;
using Airdnd.Core.Entities;
using System.Linq;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace Airdnd.Web.Services
{
    public class PhotoService
    {
        private readonly IConfiguration configuration;
        private readonly CloudinarySettings _cloudinarySettings;
        private Cloudinary _cloudinary;
        public PhotoService(IConfiguration _configuration)
        {
            configuration = _configuration;
            _cloudinarySettings = configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
            Account account = new Account(
                _cloudinarySettings.Cloudname,
                _cloudinarySettings.ApiKey,
                _cloudinarySettings.ApiSecret
               
                );
            _cloudinary = new Cloudinary(account);

        }
        //Get ImageURL from Cloudinary
        public IEnumerable<string> GetURLCloudinary(List<IFormFile> files)
        {
            List<string> PhotoURLList = new List<string>();
            foreach (var file in files)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                };
                var result = _cloudinary.Upload(uploadParams).SecureUrl.OriginalString.ToString();
                PhotoURLList.Add(result);
            };
            return PhotoURLList;
        }
        public UpdatePhotoResponseDto UploadPhotos(List<IFormFile> photos)
        {
            List<string> photoUrl = new List<string>();
            if (IsValidType(photos) == false)
            {
                return new UpdatePhotoResponseDto
                {
                    IsSuccess = false,
                    Message = "請選擇符合'jpg','png','jpeg'格式的圖片",
                };
            }
            else if (photos.Count == 0)
            {
                return new UpdatePhotoResponseDto
                {
                    IsSuccess = false,
                    Message = "請選擇圖片",
                };
            }
            else
            {
                foreach (var file in photos)
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, file.OpenReadStream()),
                    };
                    var result = _cloudinary.Upload(uploadParams).SecureUrl.OriginalString.ToString();
                    photoUrl.Add(result);
                };
                return new UpdatePhotoResponseDto
                {
                    IsSuccess = true,
                    Message = string.Empty,
                    PhotoList = photoUrl
                };
            }
        }
        public bool IsValidType(List<IFormFile> files)
        {
            var list = files.Select(x => Path.GetExtension(x.FileName).ToLowerInvariant()).ToList();
            List<string> permittedExtensions = new List<string> { ".png", ".jpg", ".jpeg" };


            var result = list.Where(x => permittedExtensions.Contains(x)).Any();

            return result;


        }
        public string UploadPhoto(IFormFile photo)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(photo.FileName, photo.OpenReadStream()),
            };
            var result = _cloudinary.Upload(uploadParams).Url.OriginalString.ToString();

            return result;
        }
    }
}

