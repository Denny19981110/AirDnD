using Airdnd.Web.Interfaces;
using Airdnd.Web.Services.Partial;
using Airdnd.Web.ViewModels;
using Airdnd.Web.ViewModels.Partial;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Airdnd.Web.Services.Cache
{
    public class RedisFilterService :IFilterPartialService
    {
        private readonly IDistributedCache _cache;
        private readonly FilterPartialService _filterService;
        private static readonly TimeSpan _defaultCacheDuration = TimeSpan.FromMinutes(1);
        private static readonly TimeSpan _defaultMaxCacheDuration = TimeSpan.FromMinutes(10);
        public RedisFilterService( FilterPartialService filterService, IDistributedCache cache )
        {
            _filterService = filterService;
            _cache = cache;
        }
        public FilterPartialVM GetAllFilter()
        {
            string cacheKey = $"filter";
            var cacheFilter = ByteArrayToObj<FilterPartialVM>(_cache.Get(cacheKey));
            if( cacheFilter == null )
            {
                var filters = _filterService.GetAllFilter();
                var byteArr = ObjectToByteArray(filters);
                _cache.Set(cacheKey, byteArr, new DistributedCacheEntryOptions
                {
                    //相對消滅時間
                    SlidingExpiration = _defaultCacheDuration,
                    //絕對相滅時間
                    AbsoluteExpirationRelativeToNow = _defaultMaxCacheDuration
                });
                return filters;
            }
            return cacheFilter;
        }

        private byte[] ObjectToByteArray( object obj )
        {
            return JsonSerializer.SerializeToUtf8Bytes(obj);
        }

        private T ByteArrayToObj<T>( byte[] byteArr ) where T : class
        {
            return byteArr is null ? null : JsonSerializer.Deserialize<T>(byteArr);
        }
    }
}


