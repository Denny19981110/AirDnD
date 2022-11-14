using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Airdnd.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airdnd.Web.Services
{
    public class ManaginRoomService
    {
        private readonly IRepository<Listing> _listing;
        private readonly IRepository<ListingImage> _listingimage;

        public ManaginRoomService(IRepository<Listing> listing, IRepository<ListingImage> listingimage)
        {
            _listing = listing;
            _listingimage = listingimage;
        }


        public IEnumerable<ManagingRoomViewModel> GetById(int userId)
        {
            var listings = _listing.GetAll().Where(l => l.UserAccountId == userId).Select(l => new ManagingRoomViewModel
            {
                RoomId = l.ListingId,
                RoomName = l.ListingName,
                statusType = l.Status,
                Bed = l.Bed,
                BedRoom = l.BedRoom,
                BathRoom = l.BathRoom,
                Createtime = l.CreateTime,
                Address = l.Address

            }).ToList();

            var imageurl = _listingimage.GetAll().Where(i => listings.Select(t => t.HostId).Contains(i.ListingId)).ToList();
            foreach(var listing in listings)
            {
                if(imageurl.Any(i => i.ListingId == listing.RoomId))
                {
                    listing.Roomphoto = imageurl.First().ListingImagePath;
                }
                yield return listing;
            }

        }
    }
}
