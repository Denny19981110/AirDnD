using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Airdnd.Web.Models;
using Airdnd.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text;
using static Airdnd.Web.Models.APIResult;
using Airdnd.Web.Models.DtoModels;
using Airdnd.Core.Helper;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Airdnd.Core.enums;

namespace Airdnd.Web.Services
{
    public class LoginService
    {        
        private readonly IRepository<UserAccount> _accountRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MailService _mailService;

        public LoginService(IRepository<UserAccount> accountRepo, IHttpContextAccessor httpContextAccessor, MailService mailService)
        {
            _accountRepo = accountRepo;
            _httpContextAccessor = httpContextAccessor;
            _mailService = mailService;
        }
        public bool IsAccountExist(string email, LoginType loginType)
        {
            //找符合的email & loginType
            return _accountRepo.GetAll().Any(x => x.Email == email && x.LoginType == loginType);
        }

        public APIResult Signup(SignupDto dto)
        {
            var encryptPwd = Encryption.SHA256Encrypt(dto.Password);
            var entity = new UserAccount()
            {
                Name = dto.Name,
                Email = dto.Email,
                Birthday = dto.Birthday,
                Password = encryptPwd,
                CreateDate = DateTime.UtcNow,
                Gender = GenderType.Other,
                LoginType = LoginType.Email
            };

            try
            {
                _accountRepo.Add(entity);
            }
            catch (Exception ex) 
            {
                return new APIResult(APIStatus.Fail, ex.ToString());
            }

            return new APIResult(APIStatus.Success);

        }

        public APIResult AcceptPromise(string email, string name)
        {
            //Email驗證信
            _mailService.SendVerifyMail(email, name);
            return new APIResult(APIStatus.Success);
        }

        public APIResult DelectAccount(string email)
        {
            //刪除帳號
            var entity = _accountRepo.GetAll().Single(x => x.Email == email && x.LoginType == 0);
            try
            {
                _accountRepo.Delete(entity);
            }
            catch(Exception ex)
            {
                return new APIResult(APIStatus.Fail, ex.ToString());
            }
            return new APIResult(APIStatus.Success);
        }

        public APIResult SignupPhone(SignupDto dto)
        {
            var entity = _accountRepo.GetAll().Single(x => x.Email == dto.Email && x.LoginType == 0);
            entity.Phone = dto.Phone;
            entity.AvatarUrl = dto.AvatarUrl;
            try
            {
                _accountRepo.Update(entity);
            }
            catch(Exception ex)
            {
                return new APIResult(APIStatus.Fail, ex.ToString());
            }

            return new APIResult(APIStatus.Success);
        }

        public APIResult Login(LoginDto dto)
        {
            if (dto.Email == null) return new APIResult(APIStatus.Fail, "email不可為null");

            var currentUser = _accountRepo.GetAll().Single(x => x.Email == dto.Email && x.LoginType == dto.LoginType);

            if (currentUser.LoginType == LoginType.Email)
            {
                if (Encryption.SHA256Encrypt(dto.Password) != currentUser.Password)
                {
                    return new APIResult(APIStatus.Fail, "密碼錯誤!");
                }
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, currentUser.UserAccountId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

            _httpContextAccessor.HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

            return new APIResult(APIStatus.Success);
        }

        public int GetCurrentUserId()
        {
            int userId = 0;
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                userId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            }
            return userId;
        }

        public void Logout()
        {
            _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public APIResult ForgetPassword(string email)
        {   
            //沒通過檢核
            if (!this.IsAccountExist(email, 0))
            {
                return new APIResult(APIStatus.Fail, "使用者帳號不存在!");
            }

            var userName = _accountRepo.GetAll().Single(x => x.Email == email && x.LoginType == 0).Name;

            //重設密碼信
            _mailService.SendResetPassword(email, userName);
            return new APIResult(APIStatus.Success);
        }

        public ResetPasswordOutputDto ResetPassword(ResetPasswordDto model)
        {
            ResetPasswordOutputDto result = new ResetPasswordOutputDto();
            result.IsSuccess = false;

            //檢核是否與舊密碼重複
            var currentUser = _accountRepo.GetAll().First(x => x.Email == model.Email && x.LoginType == 0);

            if (Encryption.SHA256Encrypt(model.Password) == currentUser.Password)
            {
                result.ErrorMessage = "與舊密碼重複";
                //result.IsSuccess = false;
                return result;
            }
            if(model.Password != model.CheckPassword)
            {
                result.ErrorMessage = "密碼與確認密碼不一致";
                //result.IsSuccess = false;
                return result;
            }

            currentUser.Password = Encryption.SHA256Encrypt(model.Password);
            _accountRepo.Update(currentUser);
            result.IsSuccess = true;
            return result;
        }
        
        public APIResult SocialCreateAccount(SocialLoginInputDto input)
        {
            var entity = new UserAccount
            {
                Name = input.Name,
                Gender = GenderType.Other,
                Email = input.Email,
                CreateDate = DateTime.UtcNow,
                LoginType = input.LoginType,
                AvatarUrl = input.Photo
            };
            try
            {
                _accountRepo.Add(entity);
            }
            catch (Exception ex)
            {
                return new APIResult(APIStatus.Fail, ex.ToString());
            }

            return new APIResult(APIStatus.Success);
        }
    }
}
