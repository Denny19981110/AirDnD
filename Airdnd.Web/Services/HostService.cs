using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Airdnd.Web.Models;
using Airdnd.Web.Models.DtoModels;
using Airdnd.Web.ViewModels.Hoster;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airdnd.Web.Services
{
    public class HostService
    {
        private readonly IRepository<UserAccount> _hostrepo;
        private readonly RankingService _rankService;
        private readonly IRepository<Listing> _listrepo;
        private readonly IRepository<ListingImage> _listIMGrepo;
        public HostService(IRepository<UserAccount> hostrepo, RankingService rankService, IRepository<Listing> listrepo, IRepository<ListingImage> listIMGrepo)
        {
            _hostrepo = hostrepo;
            _listrepo = listrepo;
            _listIMGrepo = listIMGrepo;
            _rankService = rankService;

        }
        public bool HouseExist(int id)
        {
            bool ans = _listrepo.GetAll().Any(x => x.UserAccountId == id);
            return ans;
        }
        public HostDto GetHost(int id)
        {
            var user = _hostrepo.GetAll().First(x => x.UserAccountId == id);
            var house = GetHouse(id);
            return new HostDto
            {
                HostID=user.UserAccountId,
                HostAboutMe = String.IsNullOrEmpty(user.AboutMe) == true ? "「挖金憨慢講話，但是挖金實在」.這位房東目前沒有自我介紹，但圖片說明一切" : user.AboutMe,
                HostName=user.Name,
                AvatarUrl= String.IsNullOrEmpty(user.AvatarUrl) == true ? "~/assert/common/fake.png" : user.AvatarUrl,
                email = String.IsNullOrEmpty(user.Email) == true ? "Close" : "Done",
                phone = String.IsNullOrEmpty(user.Phone) == true ? "Close" : "Done",
                House =house
            };
        }
        public List<ListDto> GetHouse(int id)
       {

        List<ListDto> user = _listrepo.GetAll().Where(x => x.UserAccountId == id).Select(x=>new ListDto{
            HostID=x.ListingId,
            ListingName=x.ListingName,
            Address=x.Address,
            DefaultPrice=x.DefaultPrice,
            star= _rankService.CommentAverage(id)
        }).ToList();
        GetHostIMG(user);
        return user;
        }
        public void GetHostIMG(List<ListDto> house)
        {
            foreach (var h in house)
            {
                List<ListIMGDto> ListingIMG = _listIMGrepo.GetAll().Where(x => x.ListingId == h.HostID).Select(x => new ListIMGDto { AvatarUrl = String.IsNullOrEmpty(x.ListingImagePath) == true ? "~/assert/common/fake.png" : x.ListingImagePath }).ToList();
                h.ListIMG = ListingIMG;
            }
        }
    }
}
