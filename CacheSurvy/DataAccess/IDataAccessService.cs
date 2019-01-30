using System.Collections.Generic;
using CacheSurvy.Models;

namespace CacheSurvy.DataAccess
{
    public interface IDataAccessService
    {
        void GetDataFromRedis();

        void GetDataFromMemoryCache();

        void GetDataFromMemory();

        void GetDataFromMongoDB();

        void GetDataFromRedisTime();

        void GetDataFromMemoryCacheTime();

        void GetDataFromMemoryTime();

        void GetDataFromMongoDBTime();

        void GetInitData();

        void GetInitTimeoutData();

        void CaculateTime();
    }
}
