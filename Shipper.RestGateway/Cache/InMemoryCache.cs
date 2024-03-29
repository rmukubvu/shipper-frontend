﻿using System;
using System.Runtime.Caching;
using Shipper.RestGateway.BaseClient;

namespace Shipper.RestGateway.Cache
{
    public class InMemoryCache: ICacheService
    {
        public T Get<T>(string cacheKey) where T : class
        {
            return MemoryCache.Default.Get(cacheKey) as T;
        }

        public void Set(string cacheKey, object item, int minutes = 30)
        {
            if (item != null)
            {
                MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(minutes));
            }
        }
    }
}
