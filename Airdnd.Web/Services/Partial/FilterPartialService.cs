

using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Airdnd.Web.Interfaces;
using Airdnd.Web.ViewModels;
using Airdnd.Web.ViewModels.Partial;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airdnd.Web.Services.Partial
{
    public class FilterPartialService :IFilterPartialService
    {
        private readonly IRepository<PrivacyType> _privacy;
        private readonly IRepository<PropertyGroup> _property;
        private readonly IRepository<Service> _service;
        private readonly IRepository<ServiceType> _serviceType;
        private readonly IRepository<Listing> _listing;
        private readonly IProductService _productService;
        private readonly HomeService _homeService;
        public FilterPartialService( IRepository<PrivacyType> privacy, IRepository<PropertyGroup> property, IRepository<Core.Entities.Service> service, IRepository<ServiceType> serviceType, IRepository<Listing> listing, IProductService productService, HomeService homeService )
        {
            _privacy = privacy;
            _property = property;
            _service = service;
            _serviceType = serviceType;
            _listing = listing;
            _productService = productService;
            _homeService = homeService;
        }
        public FilterPartialVM GetAllFilter()
        {
            var privacy = _privacy.GetAll().Select(p => new Privacy
            { Id = p.PrivacyTypeId, IdName = $"privacy{p.PrivacyTypeId}", Title = p.PrivacyTypeName, Content = p.PrivacyTypeContent }).ToList();
            var propertyGroup = _property.GetAll().Select(p => new Property
            { Id = p.PropertyGroupId, IdName = $"property{p.PropertyGroupId}", Title = p.PropertyGroupName, Icon = p.IconPath }).ToList();
            var serviceType = _serviceType.GetAll().ToList();
            var services = _service.GetAll().ToList()
                .Select(s => new ServiceFilter
                {
                    Id = s.ServiceId,
                    IdName = $"services{s.ServiceId}",
                    TypeId = s.ServiceTypeId,
                    Key = serviceType.Where(st => st.ServiceTypeId == s.ServiceTypeId)
                                        .Select(st => st.ServiceTypeName).First(),
                    Title = s.ServiceName,
                    Group = $"service{s.ServiceTypeId}"
                }).ToList();
            var serviceGp = services.GroupBy(s => s.Key).Select(s => new ServiceGroup { Key = s.Key, Service = s.ToList() });
            //補空陣列給vfor
            var is4Th = propertyGroup.Count % 4 == 0 ? true : false;
            int take4 = 0;
            if( !is4Th ) { take4 = 4 - propertyGroup.Count % 4; }
            var emptyGroup = new List<Property>();
            for( int i = 0;i < take4;i++ )
            {
                var temp = new Property();
                emptyGroup.Add(temp);
            }

            var listing = _listing.GetAllReadOnly().AsEnumerable();
            var price = new FilterPrice
            {
                MaxPrice = listing.Max(l => l.DefaultPrice),
                MinPrice = listing.Min(l => l.DefaultPrice),
                PriceAvg = Math.Round(listing.Average(l => l.DefaultPrice), 0),
            };
            price.CurrentMax = price.MaxPrice;
            price.CurrentMin = price.MinPrice;

            var result = new FilterPartialVM()
            {
                Privacy = privacy,
                Property = propertyGroup,
                Service = serviceGp,
                FillFour = emptyGroup,
                Price = price,
            };

            return result;
        }
        public FilterPrice GetCurrentPrice( IEnumerable<ProductDto> listings )
        {
            if( listings.Count() == 0 )
            {
                var nullResult = new FilterPrice
                {
                    MaxPrice = 0,
                    MinPrice = 0,
                    PriceAvg = 0
                }; return nullResult;
            }
            var max = (int)listings.Max(l => l.Price);
            var min = (int)listings.Min(l => l.Price);
            var avg = (int)listings.Average(l => l.Price);
            var result = new FilterPrice
            {
                CurrentMax = max,
                CurrentMin = min,
                PriceAvg = avg
            };
            return result;
        }
        public FilterPrice SetPriceByResult( IEnumerable<ProductDto> listings )
        {
            if( listings.Count() == 0 )
            {
                var nullResult = new FilterPrice
                {
                    MaxPrice = 0,
                    MinPrice = 0,
                    PriceAvg = 0
                };
                return nullResult;
            }
            var max = (int)listings.Max(l => l.Price);
            var min = (int)listings.Min(l => l.Price);
            var avg = (int)listings.Average(l => l.Price);
            var result = new FilterPrice
            {
                MaxPrice = max,
                MinPrice = min,
                CurrentMax = max,
                CurrentMin = min,
                PriceAvg = avg
            };
            return result;
        }
        public IEnumerable<ProductDto> FilterListings( FilterPartialDto filter, IEnumerable<ProductDto> listings, int page )
        {
            var max = filter.Price.CurrentMax;
            var min = filter.Price.CurrentMin;
            var result = listings.Where(r => r.Price >= min && r.Price <= max).ToList();

            var properties = filter.Properties.ToList();
            result = WhereIf(result, r => properties.ToList().Contains(r.PropertyId), properties.Count() > 1).ToList();

            var privacies = filter.Privacies.ToList();
            result = WhereIf(result, r => privacies.Contains(r.PrivacyId), result.Any() && privacies.Count() > 1).ToList();

            var services = filter.Services.ToList();
            result = WhereIf(result, r => services.Contains(r.PrivacyId), result.Any() && services.Count() > 1).ToList();

            if( result.Any() )
            {
                result = filter.Rooms.Beds switch
                {
                    8 => result.Where(r => r.Rooms.Beds > 8).ToList(),
                    0 => result,
                    _ => result.Where(r => r.Rooms.Beds == filter.Rooms.Beds).ToList()
                };
            }
            if( result.Any() )
            {
                result = filter.Rooms.Bedrooms switch
                {
                    8 => result.Where(r => r.Rooms.Bedrooms > 8).ToList(),
                    0 => result,
                    _ => result.Where(r => r.Rooms.Bedrooms == filter.Rooms.Bedrooms).ToList()
                };
            }
            if( result.Any() )
            {
                result = filter.Rooms.Bathrooms switch
                {
                    8 => result.Where(r => r.Rooms.Bathrooms > 8).ToList(),
                    0 => result,
                    _ => result.Where(r => r.Rooms.Bathrooms == filter.Rooms.Bathrooms).ToList()
                };
            }

            return result;
        }
        public HomeViewModel GetListingByFilter( FilterPartialDto dto, int userId, int page )
        {

            var listings = _productService.GetAllListings(userId).ToList();

            listings = FilterListings(dto, listings, page).ToList();

            var result = new HomeViewModel
            {
                Listings = listings.ToList(),
                Properties = _productService.GetHomeProperties().ToList(),
                UserInfo = _homeService.GetUserInfo(userId),
                FilterPartialVM = GetAllFilter()
            };
            result.PageCount = CountFilterPages(result);

            if( result.Listings.Count() > 16 )
            {
                result.Listings = result.Listings.Skip(16 * (page - 1)).Take(16);
            }
            result.CurrentPage = page;


            result.FilterPartialVM.Price.CurrentMax = dto.Price.CurrentMax;
            result.FilterPartialVM.Price.CurrentMin = dto.Price.CurrentMin;
            return result;
        }
        public int CountFilterPages( HomeViewModel home )
        {
            int products = home.Listings.ToList().Count;
            if( products % 16 == 0 )
            {
                return products / 16;
            }
            else
            {
                return (products / 16) + 1;
            }
        }

        public IEnumerable<T> WhereIf<T>( IEnumerable<T> source, Func<T, bool> predicate, bool condition )
        {
            return condition ? source.Where(predicate) : source;
        }
    }
}
