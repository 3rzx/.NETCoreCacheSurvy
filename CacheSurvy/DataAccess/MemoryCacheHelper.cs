namespace CacheSurvy.DataAccess
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using CacheSurvy.Models;
    using CacheSurvy.Utils;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Logging;

    public class MemoryCacheHelper : IMemoryCacheHelper
    {
        private readonly IMemoryCache cache;
        private readonly ILogger<MemoryCacheHelper> logger;
        private readonly MyMemoryCache myMemoryCache;

        public MemoryCacheHelper(
            IMemoryCache cache,
            MyMemoryCache myMemoryCache,
            ILogger<MemoryCacheHelper> logger)
        {
            this.cache = cache;
            this.myMemoryCache = myMemoryCache;
            this.logger = logger;
            this.ReadKeyValues();
        }

        public string GetServiceUrl(string serviceId)
        {
            var ans = this.cache.Get<string>(serviceId);
            this.logger.LogInformation($"Get from MemoryCache Key:{serviceId}, Value:{ans}");
            return ans;
        }

        public string GetInitData()
        {
            this.logger.LogInformation("Get Data: get from cache, get InitData, not timeout ");
            var ans = this.cache.Get<string>("InitData");
            return ans;
        }

        public string GetInitTimeoutData()
        {
            this.logger.LogInformation("Get Data: get from cache, get InitData1Sec, timeout ");
            var ans = this.cache.Get<string>("InitData1Sec");
            return ans;
        }

        public void SetServiceUrl(string serviceId, string url)
        {
            this.cache.Set(serviceId, url.ToByteArray());
        }

        private void ReadKeyValues()
        {
            this.cache.Set("InitData", "DataCreateWhenInitWithOutAnyConstrain");

            StreamReader reader1 = new StreamReader(@"KeyFilePath");
            StreamReader reader2 = new StreamReader(@"ValueFilePath");
            while (!reader2.EndOfStream)
            {
                var key = reader1.ReadLine();
                var value = reader2.ReadLine();
                this.cache.Set(key, value);
                this.myMemoryCache.SetServiceUrl(key, value);
            }

            reader1.Close();
            reader1.Dispose();
            reader2.Close();
            reader2.Dispose();

            MemoryCacheEntryOptions cacheExpirationOptions = new MemoryCacheEntryOptions();
            cacheExpirationOptions.SetSlidingExpiration(TimeSpan.FromSeconds(1));
            this.cache.Set("InitData1Sec", "DataCreateWhenInitWithConstrainOnly1Sec", cacheExpirationOptions);
        }
    }
}
