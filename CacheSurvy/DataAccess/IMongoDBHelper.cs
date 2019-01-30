using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CacheSurvy.DataAccess
{
    public interface IMongoDBHelper
    {
        string GetServiceUrl(string destinationId);

        void SetServiceUrl(string destinationId, string url);

        void GetInitData();

        void GetInitTimeoutData();

        void CaculateTime();
    }
}
