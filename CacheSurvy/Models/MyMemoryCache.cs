namespace CacheSurvy.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using CacheSurvy.DataAccess;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;

    public class MyMemoryCache : IMemoryCacheHelper
    {
        private readonly ILogger<MyMemoryCache> logger;

        public MyMemoryCache(ILogger<MyMemoryCache> logger)
        {
            this.Cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 10
            });
            this.logger = logger;
            this.ReadInitKeyValue();
        }

        private MemoryCache Cache { get; set; }

        public string GetServiceUrl(string destinationId)
        {
            return this.Cache.Get<string>(destinationId);
        }

        public void SetServiceUrl(string destinationId, string url)
        {
            MemoryCacheEntryOptions cacheExpirationOptions = new MemoryCacheEntryOptions();
            cacheExpirationOptions.SetSlidingExpiration(TimeSpan.FromMinutes(10));
            cacheExpirationOptions.SetSize(1);
            cacheExpirationOptions.Priority = CacheItemPriority.Normal;
            this.Cache.Set(destinationId, url, cacheExpirationOptions);
        }

        public string TryGet(string destinationId)
        {
            string url;
            this.Cache.TryGetValue(destinationId, out url);
            return url;
        }

        public string GetInitData()
        {
            this.SetServiceUrl("InitData10Min", "DataCreateWhenInitWithConstrainOnly10Min");
            this.logger.LogInformation("Get Data: get from MyMemoryCache, get InitData10Min, not timeout ");
            var ans = this.GetServiceUrl("InitData10Min");
            return ans;
        }

        public string GetInitTimeoutData()
        {
            this.logger.LogInformation("Get Data: get from cacheInHelper, get InitData, not in cache ");
            var ans = this.GetServiceUrl("InitData");
            return ans;
        }

        private void ReadInitKeyValue()
        {
            this.SetServiceUrl("InitData", "DataCreateWhenInit");

            StreamReader reader1 = new StreamReader(@"KeyFilePath");
            StreamReader reader2 = new StreamReader(@"ValueFilePath");
            while (!reader2.EndOfStream)
            {
                var key = reader1.ReadLine();
                var value = reader2.ReadLine();
                this.SetServiceUrl(key, value);
            }

            reader1.Close();
            reader1.Dispose();
            reader2.Close();
            reader2.Dispose();
        }
    }
}
