using Airdnd.Core.Entities;
using Airdnd.Web.Models.DtoModels;
using AutoMapper;
namespace Airdnd.Web.AutoMapperFile
{
    //繼承AutoMapper對應的屬性
    public class UswrWishListFile:Profile
    {
        public UswrWishListFile()
        {
            CreateMap<WishList, UserWishListDto>();
        }
    }
}
