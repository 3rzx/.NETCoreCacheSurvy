namespace CacheSurvy.DataAccess
{
    using System.IO;
    using System.Threading;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Logging;
    using static CacheSurvy.Utils.Utils;

    public class RedisCacheHelper : IRedisCacheHelper
    {
        private readonly IDistributedCache cache;
        private readonly ILogger<RedisCacheHelper> logger;

        public RedisCacheHelper(
            IDistributedCache cache,
            ILogger<RedisCacheHelper> logger)
        {
            this.cache = cache;
            this.logger = logger;
            this.ReadInitKeyValues();
        }

        public string GetServiceUrl(string serviceId)
        {
            var ans = this.cache.Get(serviceId).ByteArrayToObject<string>();
            this.logger.LogInformation($"Get from Redis Key:{serviceId}, Value:{ans}");
            return ans;
        }

        public void SetServiceUrl(string serviceId, string url)
        {
            this.cache.Set(serviceId, url.ToByteArray());
        }

        private void ReadInitKeyValues()
        {
            StreamReader reader1 = new StreamReader(@"KeyFilePath");
            StreamReader reader2 = new StreamReader(@"ValueFilePath");
            while (!reader2.EndOfStream)
            {
                var key = reader1.ReadLine();
                var value = reader2.ReadLine();
                this.cache.Set(key, value.ToByteArray());
            }

            reader1.Close();
            reader1.Dispose();
            reader2.Close();
            reader2.Dispose();
        }
    }
}
