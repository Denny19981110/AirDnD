using Airdnd.Web.Interfaces;
using Airdnd.Web.ViewModels;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Airdnd.Web.Services.Cache
{
    public class RedisProductService : IProductService
    {
        private readonly IDistributedCache _cache;
        private readonly ProductService _productService;
        private static readonly TimeSpan _defaultCacheDuration = TimeSpan.FromMinutes(1);
        private static readonly TimeSpan _defaultMaxCacheDuration = TimeSpan.FromMinutes(10);
        public RedisProductService(IDistributedCache cache, ProductService productService)
        {
            _cache = cache;
            _productService = productService;
        }
        public IEnumerable<ProductDto> Get16Listings(int? loginUserId, int page)
        {
            string cacheKey = $"listings-page{page}";
            var cacheListings = ByteArrayToObj<IEnumerable<ProductDto>>(_cache.Get(cacheKey));
            //如果快取是空的
            if (cacheListings == null)
            {
                var cachePageItems = _productService.Get16Listings(loginUserId, page);
                var byteArr = ObjectToByteArray(cachePageItems);
                _cache.Set(cacheKey, byteArr, new DistributedCacheEntryOptions
                {
                    //相對消滅時間
                    SlidingExpiration = _defaultCacheDuration,
                    //絕對相滅時間
                    AbsoluteExpirationRelativeToNow = _defaultMaxCacheDuration
                });
                return cachePageItems;
            }
            return cacheListings;
        }
        public IEnumerable<ProductDto> GetAllListings(int? loginUserId)
        {
            string cacheKey = $"allListings";
            var cacheListings = ByteArrayToObj<IEnumerable<ProductDto>>(_cache.Get(cacheKey));
           
            if (cacheListings == null)
            {
                var cachePageItems = _productService.GetAllListings(loginUserId);
                var byteArr = ObjectToByteArray(cachePageItems);
                _cache.Set(cacheKey, byteArr, new DistributedCacheEntryOptions
                {
                    
                    SlidingExpiration = _defaultCacheDuration,
                   
                    AbsoluteExpirationRelativeToNow = _defaultMaxCacheDuration
                });
                return cachePageItems;
            }
            return cacheListings;
        }
        public IEnumerable<HomePropertyDto> GetHomeProperties()
        {
            string cacheKey = "properties";
            var cacheProperties = ByteArrayToObj<IEnumerable<HomePropertyDto>>(_cache.Get(cacheKey));

            if (cacheProperties == null)
            {
                var cachePropertyItems = _productService.GetHomeProperties();
                var byteArr = ObjectToByteArray(cachePropertyItems);
                _cache.Set(cacheKey, byteArr, new DistributedCacheEntryOptions
                {
                    SlidingExpiration = _defaultCacheDuration,
                    AbsoluteExpirationRelativeToNow = _defaultMaxCacheDuration
                });
                return cachePropertyItems;
            }
            return cacheProperties;
        }
        private byte[] ObjectToByteArray(object obj)
        {
            return JsonSerializer.SerializeToUtf8Bytes(obj);
        }

        private T ByteArrayToObj<T>(byte[] byteArr) where T : class
        {
            return byteArr is null ? null : JsonSerializer.Deserialize<T>(byteArr);
        }
    }
}
