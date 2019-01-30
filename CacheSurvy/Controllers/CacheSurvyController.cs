namespace CacheSurvy.Controllers
{
    using System;
    using System.IO;
    using CacheSurvy.DataAccess;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Route("api/[controller]")]
    [ApiController]
    public class CacheSurvyController : ControllerBase
    {
        private readonly ILogger<CacheSurvyController> logger;
        private readonly IDataAccessService dataAccessService;

        public CacheSurvyController(
            ILogger<CacheSurvyController> logger,
            IDataAccessService dataAccessService)
        {
            this.logger = logger;
            this.dataAccessService = dataAccessService;
        }

        [HttpPost]
        public IActionResult MakeKeyValue()
        {
            /* Read a random file, making every 5 char as a Key and generate GUID as value.
             * This function will return OK Resule */

            IActionResult result = new OkResult();
            StreamReader reader = new StreamReader(@"OriginKeyPath");
            StreamWriter writer1 = new StreamWriter(@"KeyFilePath");
            StreamWriter writer2 = new StreamWriter(@"ValueFilePath");
            bool flag = false;
            while (true)
            {
                string key = string.Empty;
                for (var i = 0; i < 5; i++)
                {
                    if (reader.EndOfStream)
                    {
                        flag = true;
                    }

                    key += (char)reader.Read();
                }

                writer1.WriteLine(key);
                writer2.WriteLine(Guid.NewGuid());
                if (flag)
                {
                    break;
                }
            }

            reader.Close();
            reader.Dispose();
            writer1.Close();
            writer1.Dispose();
            writer2.Close();
            writer2.Dispose();
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Redis")]
        public IActionResult Redis()
        {
            /* Test Redis performance */

            IActionResult result = new OkResult();
            this.logger.LogInformation("10 round");
            this.logger.LogInformation("Start:");
            for (var i = 0; i < 10; i++)
            {
                this.dataAccessService.GetDataFromRedis();
            }

            this.logger.LogInformation("End============================================");
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Memory")]
        public IActionResult Memory()
        {
            /* Test Memory performance */

            IActionResult result = new OkResult();
            this.logger.LogInformation("10 round");
            this.logger.LogInformation("Start:");
            for (var i = 0; i < 10; i++)
            {
                this.dataAccessService.GetDataFromMemory();
            }

            this.logger.LogInformation("End============================================");
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("MemoryCache")]
        public IActionResult MemoryCache()
        {
            /* Test MemoryCache performance */

            IActionResult result = new OkResult();
            this.logger.LogInformation("10 round");
            this.logger.LogInformation("Start:");
            for (var i = 0; i < 10; i++)
            {
                this.dataAccessService.GetDataFromMemoryCache();
            }

            this.logger.LogInformation("End============================================");
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("MongoDB")]
        public IActionResult MongoDB()
        {
            /* Test MongoDB performance */

            IActionResult result = new OkResult();
            this.logger.LogInformation("10 round");
            this.logger.LogInformation("Start:");
            for (var i = 0; i < 10; i++)
            {
                this.dataAccessService.GetDataFromMongoDB();
            }

            this.logger.LogInformation("End============================================");
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("RedisTime")]
        public IActionResult RedisTime()
        {
            /* Test Redis performance with access same data */

            IActionResult result = new OkResult();
            this.logger.LogInformation("10 round Time test");
            this.logger.LogInformation("Start:");
            for (var i = 0; i < 10; i++)
            {
                this.dataAccessService.GetDataFromRedisTime();
            }

            this.logger.LogInformation("End============================================");
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("MemoryTime")]
        public IActionResult MemoryTime()
        {
            /* Test Memory performance with access same data */

            IActionResult result = new OkResult();
            this.logger.LogInformation("10 round Time test");
            this.logger.LogInformation("Start:");
            for (var i = 0; i < 10; i++)
            {
                this.dataAccessService.GetDataFromMemoryTime();
            }

            this.logger.LogInformation("End============================================");
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("MemoryCacheTime")]
        public IActionResult MemoryCacheTime()
        {
            /* Test MemoryCache performance with access same data */

            IActionResult result = new OkResult();
            this.logger.LogInformation("10 round Time test");
            this.logger.LogInformation("Start:");
            for (var i = 0; i < 10; i++)
            {
                this.dataAccessService.GetDataFromMemoryCacheTime();
            }

            this.logger.LogInformation("End============================================");
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("MongoDBTime")]
        public IActionResult MongoDBTime()
        {
            /* Test MongoDB performance with access same data */

            IActionResult result = new OkResult();
            this.logger.LogInformation("10 round Time test");
            this.logger.LogInformation("Start:");
            for (var i = 0; i < 10; i++)
            {
                this.dataAccessService.GetDataFromMongoDBTime();
            }

            this.logger.LogInformation("End============================================");
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetInitData")]
        public IActionResult GetInitData()
        {
            /* Test performance with access InitData */

            IActionResult result = new OkResult();
            this.dataAccessService.GetInitData();
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetInitTimeoutData")]
        public IActionResult GetInitTimeoutData()
        {
            /* Test performance with access InitData which should be timeout */

            IActionResult result = new OkResult();
            this.dataAccessService.GetInitTimeoutData();
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("CaculateMongoTime")]
        public IActionResult CaculateMongoTime()
        {
            /* Test performance MongoDB insert Data */

            IActionResult result = new OkResult();
            this.dataAccessService.CaculateTime();
            return result;
        }
    }
}
