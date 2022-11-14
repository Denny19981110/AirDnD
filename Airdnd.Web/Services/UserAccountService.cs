using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Airdnd.Infrastructure.Data;
using Airdnd.Web.Models.DtoModels;
using System.Linq;

namespace Airdnd.Web.Services
{
    public class UserAccountService
    {
        private readonly IRepository<UserAccount> _efRepository;
        public UserAccountService(IRepository<UserAccount> efRepository)
        {
            _efRepository = efRepository;
        }

        public UserAccountDto GetUser(int id)
        {
            var user = _efRepository.GetAll().First(x => x.UserAccountId == id);
            return new UserAccountDto
            {
                Id = user.UserAccountId,
                UserName = user.Name,
                Email = user.Email,

                AboutMe = user.AboutMe,
                Address = user.Address,
                Image = user.AvatarUrl
            };
        }
        public void UpdateUser(UpdateUserDto input)
        {

            var user = _efRepository.GetAll().First(x => x.UserAccountId == input.Id);
            user.AboutMe = input.AboutMe;
            user.Address = input.Address;

            _efRepository.Update(user);

        }
        public void UpdatePhoto(UpdateUserDto input)
        {
            var user = _efRepository.GetAll().First(x => x.UserAccountId == input.Id);
            user.AvatarUrl = input.Image;
            _efRepository.Update(user);
        }

        //刪除用戶頭像
        public void DeleteUserPhoto(int id)
        {
            var target = _efRepository.GetAll().First(x => x.UserAccountId == id);
            target.AvatarUrl = null;
            _efRepository.Update(target);
        }
    }
}