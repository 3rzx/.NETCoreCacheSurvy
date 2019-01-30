namespace CacheSurvy.DataAccess
{
    using System;
    using System.IO;
    using System.Linq;
    using CacheSurvy.Models;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    public class MongoDBHelper : IMongoDBHelper
    {
        private readonly ILogger<MongoDBHelper> logger;
        private readonly string tableName;
        private readonly string collectionName;
        private MongoClient mongoClient;

        public MongoDBHelper(IOptions<AppSettings> option, ILogger<MongoDBHelper> logger)
        {
            this.logger = logger;
            this.mongoClient = new MongoClient(option.Value.ConnectionStrings.DB);
            this.tableName = option.Value.ConnectionStrings.Table;
            this.collectionName = option.Value.ConnectionStrings.Collection;
            this.ReadKeyValue();
        }

        public string GetServiceUrl(string destinationId)
        {
            var session = this.mongoClient.StartSession();
            var db = session.Client.GetDatabase(this.tableName).GetCollection<MongoDBCache>(this.collectionName);

            session.StartTransaction();
            var result = db.Find<MongoDBCache>(mongoDBCache => mongoDBCache.DestinationId == destinationId).ToList();
            session.CommitTransaction();

            var url = result.OrderByDescending(mongoDBCache => mongoDBCache.CreateDateTime).First().Url;
            this.logger.LogInformation($"Get from MongoDB Key:{destinationId}, Value:{url}");
            return url;
        }

        public void SetServiceUrl(string destinationId, string url)
        {
            var session = this.mongoClient.StartSession();
            var db = session.Client.GetDatabase(this.tableName).GetCollection<MongoDBCache>(this.collectionName);

            var mongoDBCache = new MongoDBCache()
            {
                DestinationId = destinationId,
                Url = url,
                CreateDateTime = DateTime.Now
            };

            session.StartTransaction();
            db.InsertOne(mongoDBCache);
            session.CommitTransaction();
        }

        public void GetInitData()
        {
            throw new NotImplementedException();
        }

        public void GetInitTimeoutData()
        {
            this.SetServiceUrl("KeyString", "127.0.0.1");
            this.logger.LogInformation("Get Init Data Id = KeyString Url = " + this.GetServiceUrl("KeyString"));
            this.logger.LogInformation("Add New record: Id = KeyString, Url = localhost");
            this.SetServiceUrl("KeyString", "localhost");
            this.logger.LogInformation("Get Data Id = KeyString Url = " + this.GetServiceUrl("KeyString"));
        }

        public void CaculateTime()
        {
            this.ReadKeyValue();
        }

        private void ReadKeyValue()
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
