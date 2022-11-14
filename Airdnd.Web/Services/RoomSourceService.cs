using Airdnd.Core.Entities;
using Airdnd.Core.enums;
using Airdnd.Core.Interfaces;
using Airdnd.Infrastructure.Data;
using Airdnd.Web.Models.DtoModels;
using Airdnd.Web.Models.DtoModels.RoomSource;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Airdnd.Web.Services
{
    public class RoomSourceService
    {
        private readonly IRepository<PropertyGroup> _efPropertyGroup;
        private readonly IRepository<PropertyType> _efPropertyType;
        private readonly IRepository<PrivacyType> _efPrivacyType;
        private readonly IRepository<ServiceType> _efServiceType;
        private readonly IRepository<Service> _efService;
        private readonly IRepository<Highlight> _efHighLight;
        private readonly IRepository<Legal> _efLegal;
        private readonly IRepository<UserAccount> _efUserAccount;
        private readonly IRepository<Listing> _efListing;
        private readonly IRepository<ServiceListing> _efServiceListing;
        private readonly IRepository<LegalListing> _efLegealListing;
        private readonly IRepository<ListingImage> _efListingImage;
        private readonly PhotoService _photoService;
        private readonly GoogleMapService _googleMapService;

        public RoomSourceService(
                IRepository<PropertyGroup> propertyGroup,
                IRepository<PropertyType> propertyType,
                IRepository<PrivacyType> privacyType,
                IRepository<ServiceType> serviceType,
                IRepository<Service> service,
                IRepository<Highlight> highLight,
                IRepository<Legal> legal,
                IRepository<UserAccount> userAccount,
                IRepository<Listing> listing,
                IRepository<ServiceListing> serviceListing,
                IRepository<LegalListing> legalListing,
                IRepository<ListingImage> listImage,
                PhotoService photoService,
                GoogleMapService googleMapService )
        {
            _efPropertyGroup = propertyGroup;
            _efPropertyType = propertyType;
            _efPrivacyType = privacyType;
            _efServiceType = serviceType;
            _efService = service;
            _efHighLight = highLight;
            _efLegal = legal;
            _efUserAccount = userAccount;
            _efListing = listing;
            _efServiceListing = serviceListing;
            _efLegealListing = legalListing;
            _efListingImage = listImage;
            _photoService = photoService;
            _googleMapService = googleMapService;
        }

        public IEnumerable<RoomSourceDto.Page1> GetPropertyGroups()
        {
            var options = _efPropertyGroup.GetAll().Select(x => new RoomSourceDto.Page1
            {
                PropertyGroupId = x.PropertyGroupId,
                PropertyGroupName = x.PropertyGroupName,
                PropertyGroupImgUrl = x.ImagePath
            }).ToList();
            return options;
        }
        public IEnumerable<RoomSourceDto.Page2> GetPropertyTypes()
        {
            var options = _efPropertyType.GetAll().Select(x => new RoomSourceDto.Page2
            {
                PropertyTypeId = x.PropertyId,
                PropertyTitle = x.PropertyName,
                PropertyContent = x.PropertyContent,
                PropertyGroupId = x.PropertyGroupId
            }).ToList();
            return options;
        }
        public IEnumerable<RoomSourceDto.Page2> GetPropertyTypes(int groupId)
        {
            var options = _efPropertyType.GetAll().Where(x => x.PropertyGroupId == groupId).Select(x => new RoomSourceDto.Page2
            {
                PropertyTypeId = x.PropertyId,
                PropertyTitle = x.PropertyName,
                PropertyContent = x.PropertyContent,
                PropertyGroupId = x.PropertyGroupId
            }).ToList();
            return options;
        }
        public IEnumerable<RoomSourceDto.Page3> GetPrivacyTypes()
        {
            var options = _efPrivacyType.GetAll().Select(x => new RoomSourceDto.Page3
            {
                PrivacyTypeId = x.PrivacyTypeId,
                PrivacyTypeName = x.PrivacyTypeName
            }).ToList();
            return options;
        }
        public IEnumerable<RoomSourceDto.Page6> GetServices()
        {
            var types = _efServiceType.GetAll().Select(x => new RoomSourceDto.Page6
            {
                ServiceTypeId = x.ServiceTypeId,
                ServiceTypeName = x.ServiceTypeName,

            }).ToList();
            var options = _efService.GetAll().Select(x => new RoomSourceDto.Page6.ServiceItem
            {
                ServiceId = x.ServiceId,
                ServiceTypeId = x.ServiceTypeId,
                Service = x.ServiceName,
                ServiceIconPath = x.IconPath,
                Sort = x.Sort
            }).ToList();

            var result = types.Select(type => new RoomSourceDto.Page6
            {
                ServiceTypeId = type.ServiceTypeId,
                ServiceTypeName = type.ServiceTypeName,
                ServiceItems = options.Where(option => option.ServiceTypeId == type.ServiceTypeId).Select(option => option).OrderBy(x => x.Sort)
            });
            return result;
        }
        public IEnumerable<RoomSourceDto.Page9> GetHighLights()
        {
            var options = _efHighLight.GetAll().Select(x => new RoomSourceDto.Page9
            {
                HighLightId = x.HighlightId,
                HighLightName = x.HighlightName,
                HighLightIconPath = x.IconPath
            }).ToList();
            return options;
        }
        public IEnumerable<RoomSourceDto.Page12> GetLegals()
        {
            var options = _efLegal.GetAll().Select(x => new RoomSourceDto.Page12
            {
                LegalId = x.LegalId,
                LegalName = x.LegalName
            }).ToList();
            return options;
        }
        public string GetPersonName(int id)
        {
            var user = _efUserAccount.GetAll().First(x => x.UserAccountId == id).Name;
            return user;
        }
        public void CreateRoom(CreateRoomDto query, string[] photos, int userId)
        {
            var location = _googleMapService.GetLatLngByAddress(query.Address);
            var listingEntity = new Listing
            {
                DefaultPrice = query.Price,
                Address = location.Address,
                Lng = location.Lng,
                Lat = location.Lat,
                ListingName = query.RoomName.Trim(),
                Description = query.Description,
                PropertyId = query.PropertyTypeID,
                CategoryId = query.PrivacyTypeID,
                Expected = query.People,
                UserAccountId = userId,
                Status = StatusType.HasUpload,
                Bed = query.Bed,
                BedRoom = query.Bedroom,
                BathRoom = query.BathRoom,
                Toilet = 1,
                CreateTime = DateTime.UtcNow,
                EditTime = null,
                IndieBathroom = query.InsideBathroom,
                HighlightId = query.HighLightID,
                
            };
            _efListing.Add(listingEntity);

            if (query.Services != null)
            {
                var serviceEntity = query.Services.Select(x => new ServiceListing
                {
                    ListingId = listingEntity.ListingId,
                    ServiceId = int.Parse(x),
                });
                _efServiceListing.AddRange(serviceEntity);
            }

            if(query.Legals != null)
            {
                var legalEntity = query.Legals.Select(x => new LegalListing
                {
                    ListingId = listingEntity.ListingId,
                    LegalId = int.Parse(x)
                });
                _efLegealListing.AddRange(legalEntity);
            }

            var photoEntity = photos.Select(x => new ListingImage
            {
                ListingImagePath = x,
                ListingId = listingEntity.ListingId
            });
            _efListingImage.AddRange(photoEntity);
        }

        //取得編輯房源頁面
        public GetRoomInfoDto GetRoom(int id)
        {
            
            var room = _efListing.GetAll().First(x=>x.ListingId == id);
            var groupName = _efPropertyGroup.GetAll().First(x => x.PropertyGroupId == (_efPropertyType.GetAll().First(x => x.PropertyId == room.PropertyId)).PropertyGroupId).PropertyGroupName;
            var propertyName = _efPropertyType.GetAll().First(x => x.PropertyId == room.PropertyId).PropertyName;
            var privacyName = _efPrivacyType.GetAll().First(x=>x.PrivacyTypeId == room.CategoryId).PrivacyTypeName;
            List<string> photos = _efListingImage.GetAll().Where(x => x.ListingId == room.ListingId).Select(x => x.ListingImagePath).ToList();
            return new GetRoomInfoDto
            {
                Id = room.ListingId,
                RoomName = room.ListingName,
                Address = room.Address,
                People = room.Expected,
                Bed = room.Bed,
                BedRoom = room.BedRoom,
                BathRoom = room.BathRoom,
                Description = room.Description,
                PropertyGroup = groupName,
                PropertyType = propertyName,
                PrivacyType = privacyName,
                Photos = photos
            };
        }
        //更新房源(不含照片)
        public void UpdateRoomInfo(UpdateRoomInfoDto input)
        {
            var room = _efListing.GetAll().First(x => x.ListingId == input.Id);

            if (input.Address != null) { room.Address = input.Address; }
            if (input.RoomDescription != null) { room.Description = input.RoomDescription; }
            if (input.RoomName != null) { room.ListingName = input.RoomName; }
            if (input.PeopleCount >= 0 && input.PeopleCount < 11) { room.Expected = input.PeopleCount; } 


            _efListing.Update(room);
        }

        

        //更新房源相片
        public void UpdateRoomImage(UpdateRoomInfoDto input)
        {
            var room = _efListing.GetAll().First(x => x.ListingId == input.Id);
            var photos = _efListingImage.GetAll().Where(x => x.ListingId == room.ListingId).Select(x => x.ListingImagePath);

            var photoEntity = input.Photos.Select(x => new ListingImage
            {
                ListingImagePath = x,
                ListingId = input.Id
            });
            _efListingImage.AddRange(photoEntity);

        }

        //刪除房源照片
        public void DeleteImage(string input)
        {
            var target = _efListingImage.GetAll().First(x=>x.ListingImagePath == input);
            _efListingImage.Delete(target);
        }

        public void ResetRoomImage(int id, List<string> photos)
        {
            
            var targets = _efListingImage.GetAll().Where(x=>x.ListingId == id).ToList();
            foreach (var target in targets)
            {
                _efListingImage.Delete(target);
            }

            var photoEntity = photos.Select(x => new ListingImage
            {
                ListingImagePath = x,
                ListingId = id
            });
            _efListingImage.AddRange(photoEntity);

        }
    }
}
