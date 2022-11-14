using Airdnd.Core.Entities;
using Airdnd.Core.Helper;
using Airdnd.Core.Interfaces;
using Airdnd.Web.Models;
using Airdnd.Web.Models.DtoModels;
using Airdnd.Web.ViewModels;
using Airdnd.Web.WebApi;
using CloudinaryDotNet;
using System;
using System.Linq;
using static Airdnd.Web.Models.APIResult;

namespace Airdnd.Web.Services
{
    public class MemberService
    {
        private readonly IRepository<UserAccount> _accountRepo;
        public MemberService(IRepository<UserAccount> accountRepo)
        {
            _accountRepo = accountRepo;
        }

        public APIResult GetUserDataById(int id)
        {
            var entity = _accountRepo.GetAll().Single(x => x.UserAccountId == id);
            var memberData = new MemberData()
            {
                Name = entity.Name,
                Email = entity.Email,
                Gender = (int)entity.Gender,
                Phone = entity.Phone,
                Address = entity.Address
            };
            return new APIResult(APIStatus.Success, memberData);
        }
        public string GetUserNameById(int id)
        {
            return _accountRepo.GetAll().Single(x => x.UserAccountId == id).Name;
        }

        public APIResult EditMemberData(MemberDataDto dto, int userId)
        {
            var entity = _accountRepo.GetAll().Single(x => x.UserAccountId == userId);
            entity.Name = dto.Name;
            entity.Email = dto.Email;  
            entity.Gender = dto.Gender;
            entity.Phone = dto.Phone;
            entity.Address = dto.Address;

            try
            {
                _accountRepo.Update(entity);
            }
            catch(Exception ex)
            {
                return new APIResult(APIStatus.Fail,ex.ToString());
            }

            return new APIResult(APIStatus.Success);
        }
        public APIResult ModifyPassword(ResetPasswordDto dto, int userId)
        {
            if(dto.Password != dto.CheckPassword)
            {
                return new APIResult(APIStatus.Fail, "新密碼與確認密碼不相符");
            }
            var entity = _accountRepo.GetAll().Single(x => x.UserAccountId == userId);
            if(entity.Password != Encryption.SHA256Encrypt(dto.OldPassword))
            {
                return new APIResult(APIStatus.Fail, "輸入的密碼與舊密碼不相符");
            }
            entity.Password = Encryption.SHA256Encrypt(dto.Password);
            entity.PasswordModifyDate = DateTime.Now;

            try
            {
                _accountRepo.Update(entity);
            }
            catch (Exception ex)
            {
                return new APIResult(APIStatus.Fail, ex.ToString());
            }

            return new APIResult(APIStatus.Success);
        }
        public string GetPasswordModifyDate(int userId)
        {
            var modifyDate = DateTime.Now;
            var entity = _accountRepo.GetAll().Single(x => x.UserAccountId == userId);

            modifyDate = entity.PasswordModifyDate ?? entity.CreateDate;

            var diffDate = DateTime.Now.Subtract(modifyDate);
            if (diffDate.Days == 0)
            {
                if(diffDate.Hours == 0)
                {
                    return $"上次更新：{diffDate.Minutes}分鐘前";
                }
                return $"上次更新：{diffDate.Hours}小時前";
            }
            return $"上次更新：{diffDate.Days}天前";

        }
    }
}
