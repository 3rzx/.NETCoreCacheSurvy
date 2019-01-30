using System.IO;
using System.Threading;
using CacheSurvy.Models;
using Microsoft.Extensions.Logging;

namespace CacheSurvy.DataAccess
{
    public class DataAccessService : IDataAccessService
    {
        private readonly IRedisCacheHelper redisCacheHelper;
        private readonly IMemoryCacheHelper memoryCacheHelper;
        private readonly MyMemoryCache myMemoryCache;
        private readonly IMongoDBHelper mongoDBHelper;
        private readonly ILogger<DataAccessService> logger;
        private readonly MemoryHelper memoryHelper;

        public DataAccessService(
            IRedisCacheHelper redisCacheHelper,
            IMemoryCacheHelper memoryCacheHelper,
            MyMemoryCache myMemoryCache,
            IMongoDBHelper mongoDBHelper,
            ILogger<DataAccessService> logger,
            MemoryHelper memoryHelper)
        {
            this.redisCacheHelper = redisCacheHelper;
            this.memoryCacheHelper = memoryCacheHelper;
            this.myMemoryCache = myMemoryCache;
            this.mongoDBHelper = mongoDBHelper;
            this.logger = logger;
            this.memoryHelper = memoryHelper;
        }

        public void GetDataFromMemoryCache()
        {
            this.logger.LogInformation("Use 20 thread to GetDataFromMemoryCache");
            this.logger.LogInformation("Start:");
            StreamReader reader = new StreamReader(@"KeyFilePath");
            var serviceId = string.Empty;
            for (var i = 0; i < 20; i++)
            {
                serviceId = reader.ReadLine();
                Thread t = new Thread(() => this.MultiThreadToGetDataFromMemoryCache(serviceId));
                t.Start();
            }

            this.logger.LogInformation("End");
        }

        public void GetDataFromRedis()
        {
            this.logger.LogInformation("Use 20 thread to GetDataFromRedis");
            this.logger.LogInformation("Start:");
            StreamReader reader = new StreamReader(@"KeyFilePath");
            var serviceId = string.Empty;
            for (var i = 0; i < 20; i++)
            {
                serviceId = reader.ReadLine();
                Thread t = new Thread(() => this.MultiThreadToGetDataFromRedis(serviceId));
                t.Start();
            }

            this.logger.LogInformation("End");
        }

        public void GetDataFromMemory()
        {
            this.logger.LogInformation("Use 20 thread to GetDataFromMemory");
            this.logger.LogInformation("Start:");
            StreamReader reader = new StreamReader(@"KeyFilePath");
            var serviceId = string.Empty;
            for (var i = 0; i < 20; i++)
            {
                serviceId = reader.ReadLine();
                Thread t = new Thread(() => this.MultiThreadToGetDataFromMemory(serviceId));
                t.Start();
            }

            this.logger.LogInformation("End");
        }

        public void GetDataFromMongoDB()
        {
            this.logger.LogInformation("Use 20 thread to GetDataFromMongoDB");
            this.logger.LogInformation("Start:");
            StreamReader reader = new StreamReader(@"KeyFilePath");
            var serviceId = string.Empty;
            for (var i = 0; i < 20; i++)
            {
                serviceId = reader.ReadLine();
                Thread t = new Thread(() => this.MultiThreadToGetDataFromMongoDB(serviceId));
                t.Start();
            }

            this.logger.LogInformation("End");
        }

        public void GetDataFromRedisTime()
        {
            StreamReader reader = new StreamReader(@"KeyFilePath");
            var serviceId1 = reader.ReadLine();
            var serviceId2 = reader.ReadLine();
            var serviceId3 = reader.ReadLine();
            var serviceId4 = reader.ReadLine();
            reader.Close();
            reader.Dispose();
            this.logger.LogInformation("Use 20 thread to GetDataFromRedis");
            for (var i = 0; i < 20; i++)
            {
                Thread t = new Thread(() => this.MultiThreadToGetDataFromRedis(serviceId1));
                t.Start();
                Thread t2 = new Thread(() => this.MultiThreadToGetDataFromRedis(serviceId2));
                t2.Start();
                Thread t3 = new Thread(() => this.MultiThreadToGetDataFromRedis(serviceId3));
                t3.Start();
                Thread t4 = new Thread(() => this.MultiThreadToGetDataFromRedis(serviceId4));
                t4.Start();
            }

            this.logger.LogInformation("End");
        }

        public void GetDataFromMemoryCacheTime()
        {
            StreamReader reader = new StreamReader(@"KeyFilePath");
            var serviceId1 = reader.ReadLine();
            var serviceId2 = reader.ReadLine();
            var serviceId3 = reader.ReadLine();
            var serviceId4 = reader.ReadLine();
            reader.Close();
            reader.Dispose();
            this.logger.LogInformation("Use 20 thread to GetDataFromMemoryCache");
            for (var i = 0; i < 20; i++)
            {
                Thread t = new Thread(() => this.MultiThreadToGetDataFromMemoryCache(serviceId1));
                t.Start();
                Thread t2 = new Thread(() => this.MultiThreadToGetDataFromMemoryCache(serviceId2));
                t2.Start();
                Thread t3 = new Thread(() => this.MultiThreadToGetDataFromMemoryCache(serviceId3));
                t3.Start();
                Thread t4 = new Thread(() => this.MultiThreadToGetDataFromMemoryCache(serviceId4));
                t4.Start();
            }

            this.logger.LogInformation("End");
        }

        public void GetDataFromMemoryTime()
        {
            StreamReader reader = new StreamReader(@"KeyFilePath");
            var serviceId1 = reader.ReadLine();
            var serviceId2 = reader.ReadLine();
            var serviceId3 = reader.ReadLine();
            var serviceId4 = reader.ReadLine();
            reader.Close();
            reader.Dispose();
            this.logger.LogInformation("Use 20 thread to GetDataFromMemory");
            for (var i = 0; i < 20; i++)
            {
                Thread t = new Thread(() => this.MultiThreadToGetDataFromMemory(serviceId1));
                t.Start();
                Thread t2 = new Thread(() => this.MultiThreadToGetDataFromMemory(serviceId2));
                t2.Start();
                Thread t3 = new Thread(() => this.MultiThreadToGetDataFromMemory(serviceId3));
                t3.Start();
                Thread t4 = new Thread(() => this.MultiThreadToGetDataFromMemory(serviceId4));
                t4.Start();
            }

            this.logger.LogInformation("End");
        }

        public void GetDataFromMongoDBTime()
        {
            StreamReader reader = new StreamReader(@"KeyFilePath");
            var serviceId1 = reader.ReadLine();
            var serviceId2 = reader.ReadLine();
            var serviceId3 = reader.ReadLine();
            var serviceId4 = reader.ReadLine();
            reader.Close();
            reader.Dispose();
            this.logger.LogInformation("Use 20 thread to GetDataFromMemory");
            for (var i = 0; i < 20; i++)
            {
                Thread t = new Thread(() => this.MultiThreadToGetDataFromMongoDB(serviceId1));
                t.Start();
                Thread t2 = new Thread(() => this.MultiThreadToGetDataFromMongoDB(serviceId2));
                t2.Start();
                Thread t3 = new Thread(() => this.MultiThreadToGetDataFromMongoDB(serviceId3));
                t3.Start();
                Thread t4 = new Thread(() => this.MultiThreadToGetDataFromMongoDB(serviceId4));
                t4.Start();
            }

            this.logger.LogInformation("End");
        }

        public void MultiThreadToGetDataFromMemoryCache(string serviceId)
        {
            this.memoryCacheHelper.GetServiceUrl(serviceId);
        }

        public void MultiThreadToGetDataFromRedis(string serviceId)
        {
            this.redisCacheHelper.GetServiceUrl(serviceId);
        }

        public void MultiThreadToGetDataFromMemory(string key)
        {
            this.memoryHelper.GetServiceUrl(key);
        }

        public void MultiThreadToGetDataFromMongoDB(string key)
        {
            this.mongoDBHelper.GetServiceUrl(key);
        }

        public void GetInitData()
        {
            this.memoryCacheHelper.GetInitData();
            this.myMemoryCache.GetInitData();
        }

        public void GetInitTimeoutData()
        {
            this.memoryCacheHelper.GetInitTimeoutData();
            this.myMemoryCache.GetInitTimeoutData();
            this.mongoDBHelper.GetInitTimeoutData();
        }

        public void CaculateTime()
        {
            this.mongoDBHelper.CaculateTime();
        }
    }
}
