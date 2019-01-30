namespace CacheSurvy.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class MemoryHelper
    {
        private readonly ILogger<MemoryHelper> logger;
        private Dictionary<string, string> memoryDic;

        public MemoryHelper(ILogger<MemoryHelper> logger)
        {
            this.logger = logger;
            this.memoryDic = new Dictionary<string, string>();
            this.ReadInitKeyValue();
        }

        public string GetServiceUrl(string destinationId)
        {
            var ans = this.memoryDic[destinationId];
            this.logger.LogInformation($"Get from Memory Key:{destinationId}, Value:{ans}");
            return ans;
        }

        public void SetServiceUrl(string destinationId, string url)
        {
            this.memoryDic.Add(destinationId, url);
        }

        private void ReadInitKeyValue()
        {
            this.memoryDic.Add("InitData", "DataCreateWhenInitWithOutAnyConstrain");

            StreamReader reader1 = new StreamReader(@"KeyFilePath");
            StreamReader reader2 = new StreamReader(@"ValueFilePath");
            while (!reader2.EndOfStream)
            {
                var key = reader1.ReadLine();
                var value = reader2.ReadLine();
                this.memoryDic.Add(key, value);
            }

            reader1.Close();
            reader1.Dispose();
            reader2.Close();
            reader2.Dispose();

            this.memoryDic.Add("InitData1Sec", "DataCreateWhenInitWithConstrainOnly1Sec");
        }
    }
}
