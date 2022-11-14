using Airdnd.Core.Entities;
using Airdnd.Core.enums;
using Airdnd.Core.Interfaces;
using Airdnd.Web.Models;
using Airdnd.Web.Models.DtoModels.RoomSource;
using Airdnd.Web.ViewModels.House;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Airdnd.Web.Services
{
    public class HouseService
    {
        private readonly IRepository<UserAccount> _hostrepo;
        private readonly IRepository<Listing> _listrepo;
        private readonly IRepository<ListingImage> _listIMGrepo;
        private readonly IRepository<LegalListing> _legalrepo;
        private readonly IRepository<ServiceListing> _servicerepo;
        private readonly IRepository<PropertyType> _propertyType;
        private readonly IRepository<PrivacyType> _privacyType;
        private readonly IRepository<Highlight> _highlight;
        private readonly IRepository<Service> _service;
        private readonly IRepository<ServiceListing> _serviceListing;
        private readonly IRepository<ServiceType> _serviceType;
        public HouseService(
            IRepository<UserAccount> hostrepo,
            IRepository<Listing> listrepo,
            IRepository<ListingImage> listIMGrepo,
            IRepository<LegalListing> legalrepo,
            IRepository<ServiceListing> servicerepo,
            IRepository<PropertyType> propertyType,
            IRepository<PrivacyType> privacyType,
            IRepository<Highlight> highlight,
            IRepository<Service> service,
            IRepository<ServiceListing> serviceListing,
            IRepository<ServiceType> serviceType
            )
        {
            _hostrepo = hostrepo;
            _listrepo = listrepo;
            _listIMGrepo = listIMGrepo;
            _legalrepo = legalrepo;
            _servicerepo = servicerepo;
            _propertyType = propertyType;
            _privacyType = privacyType;
            _highlight = highlight;
            _service = service;
            _serviceListing = serviceListing;
            _serviceType = serviceType;
        }
        public int SearchHouseID(string name)
        {
            int ans = _listrepo.GetAll().Single(x => x.ListingName == name).ListingId;
            return ans;
        }
        public bool HouseExist(string name)
        {
            bool ans = _listrepo.GetAll().Any(x => x.ListingName == name && x.Status == StatusType.HasUpload);
            return ans;
        }
        public HouseDto GetHouse(int id)
        {
            //public StatusType Status { get; set; }
            var listing = _listrepo.GetAll().Single(x => x.ListingId == id);
            List<ListIMGDto> ListingIMG = _listIMGrepo.GetAll().Where(x => x.ListingId == id).Select(x => new ListIMGDto { AvatarUrl = x.ListingImagePath }).ToList();
            UserAccount Host = _hostrepo.GetAll().First(x => x.UserAccountId == listing.UserAccountId);
            return new HouseDto
            {
                Bed = listing.Bed,
                IndieBathroom = listing.IndieBathroom,
                HouseID = listing.ListingId,
                HouseName = listing.ListingName,
                HousePic = ListingIMG,
                DefaultPrice = listing.DefaultPrice,
                Address = listing.Address,
                BathRoom = listing.BathRoom,
                BedRoom = listing.BedRoom,
                Description = listing.Description,
                Lat = listing.Lat,
                Lng = listing.Lng,
                Property = _propertyType.GetAll().First(x => x.PropertyId == listing.PropertyId).PropertyName,
                Category = _privacyType.GetAll().First(x => x.PrivacyTypeId == listing.CategoryId).PrivacyTypeName,
                Expected = listing.Expected,
                Toilet = listing.Toilet,
                Highlight = _highlight.GetAll().First(x => x.HighlightId == listing.HighlightId).HighlightName,
                Host = new HostDto
                {
                    HostID = Host.UserAccountId,
                    HostName = Host.Name,
                    AvatarUrl = String.IsNullOrEmpty(Host.AvatarUrl) == true ? "~/assert/common/fake.png" : Host.AvatarUrl,
                    HostAboutMe = String.IsNullOrEmpty(Host.AboutMe) == true ? "「挖金憨慢講話，但是挖金實在」.這位房東目前沒有自我介紹，但圖片說明一切" : Host.AboutMe,
                },
                Status = listing.Status,
            };

        }
        public CService GetService(int id)
        {
            var SL = _serviceListing.GetAll().Where(x => x.ListingId == id).Select(x => x.ServiceId);
            List<IService> S = SL.Select(y => _service.GetAll().First(x => x.ServiceId == y)).Select(x => new IService
            {
                ServiceTypeID = x.ServiceTypeId,
                ServiceId = x.ServiceId,
                ServiceName = x.ServiceName,
                Sort = x.Sort,
                IconPath = x.IconPath
            }).OrderBy(x => x.ServiceTypeID).ToList();
            var ST = _serviceType.GetAll().Select(x => x).ToList();
            List<LService> STR = ST.Select(type => new LService
            {
                ServiceTypeID = type.ServiceTypeId,
                ServiceTypeName = type.ServiceTypeName,
                ServiceIcon = S.Where(option => option.ServiceTypeID == type.ServiceTypeId).Select(option => option).OrderBy(x => x.Sort).ToList()
            }).OrderBy(x => x.ServiceTypeID).ToList();

            CService A = new CService()
            {
                count = S.Count(),
                ServiceList = STR
            };
            return A;
        }
    }
}
