using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Airdnd.Web.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airdnd.Web.Services
{
    public class RankingService
    {
        private readonly IRepository<UserAccount> _hostrepo;
        private readonly IRepository<Listing> _listrepo;
        private readonly IRepository<Rating> _rankrepo;
        public RankingService(IRepository<UserAccount> hostrepo, IRepository<Listing> listrepo, IRepository<Rating> rankrepo)
        {
            _hostrepo = hostrepo;
            _listrepo = listrepo;
            _rankrepo = rankrepo;
        }
        /// <summary>
        /// 輸入一個房源ID回傳是否有評論
        /// </summary>
        public bool CommentExist(int houseid)
        {
            bool ans = _rankrepo.GetAll().Any(x => x.ListingId == houseid);
            return ans;
        }
        /// <summary>
        /// 輸入一個房源ID回傳有多少則評論
        /// </summary>
        public int CommentNum(int houseid)
        {
            int ans = _rankrepo.GetAll().Where(x => x.ListingId == houseid && x.CommentContent!=null).Count();
            return ans;
        }
        /// <summary>
        /// 輸入一個房源ID回傳一個房源總平均
        /// </summary>
        public double? CommentAverage(int houseid)
        {
            double? ans = _rankrepo.GetAll().Where(x => x.ListingId == houseid && x.RatingAvg!=null).Select(x=>x.RatingAvg).ToList().Average();
            return ans;
        }
        /// <summary>
        /// 六個項目的各個平均
        /// </summary>
        public RankDto CommentSix(int houseid)
        {
            List<RankDto> list = _rankrepo.GetAll().Where(x => x.ListingId == houseid).Select(x => new RankDto
            {
                ListingId = (int)x.ListingId,
                Clean = (int)x.Clean,
                Precise = (int)x.Precise,
                Communication = (int)x.Communication,
                Location = (int)x.Location,
                CheckIn = (int)x.CheckIn,
                CostPrice = (int)x.CostPrice
            }).ToList();
            RankDto ans = new RankDto
            {
                Clean = Math.Round(list.Where(x => x.Clean != 0).Select(x => x.Clean).Average(), 1),
                Precise = Math.Round(list.Where(x => x.Precise != 0).Select(x => x.Precise).Average(), 1),
                Communication = Math.Round(list.Where(x => x.Communication != 0).Select(x => x.Communication).Average(), 1),
                Location = Math.Round(list.Where(x => x.Location != 0).Select(x => x.Location).Average(),1),
                CheckIn = Math.Round(list.Where(x => x.CheckIn != 0).Select(x => x.CheckIn).Average(), 1),
                CostPrice = Math.Round(list.Where(x => x.CostPrice != 0).Select(x => x.CostPrice).Average(), 1),
            };
            return ans;
        }
        /// /// <summary>
        /// 使用者評論內容
        /// 檔案類型是List<RankPersonDto>
        /// </summary>
        public List<RankPersonDto> CommentUser(int houseid)
        {

            List<RankPersonDto> ans = _rankrepo.GetAll().Where(x => x.ListingId == houseid).Select(x => new RankPersonDto
            {
                userId= (int)x.UserId,
                CommentContent = x.CommentContent,
                UserName = _hostrepo.GetAll().First(y => y.UserAccountId == x.UserId).Name,
                Image = String.IsNullOrEmpty(_hostrepo.GetAll().First(y => y.UserAccountId == x.UserId).AvatarUrl) == true ? "/assert/common/fake.png" : _hostrepo.GetAll().First(y => y.UserAccountId == x.UserId).AvatarUrl,
                flag = false,
                time = x.CreatTime.ToString().Replace(" 12:00am", ""),
                timeflag= (DateTime)x.CreatTime,
                country= _hostrepo.GetAll().First(y => y.UserAccountId == x.UserId).Country,
            }).OrderBy(x=>x.timeflag).ToList();  
            return ans;
        }
    }
}
