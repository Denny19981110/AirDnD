using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Airdnd.Web.Models.DtoModels;
using System.Collections.Generic;
using System.Linq;

namespace Airdnd.Web.Services
{
    public class UserWishListService
    {
        private readonly IRepository<WishList> _efRepository;
        public UserWishListService(IRepository<WishList> repository)
        {
            _efRepository = repository;
        }

        public IEnumerable<UserWishListDto> GetAllList()
        {
            var list = _efRepository.GetAll().ToList();
            return list.Select(x=> new UserWishListDto
            {
                GroupId = x.WishlistId,
                GroupName = x.WishGroupName,
                UserId = x.UserAccountId
            });
        }
    }
}
