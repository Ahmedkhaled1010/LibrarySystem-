using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace PaymentMicroServices.InfraStructure.Data.Context
{
    public class MongoDb
    {
        private readonly IConfiguration configuration;
        private readonly IMongoDatabase mongoDatabase;

        public MongoDb(IConfiguration configuration)
        {
            this.configuration = configuration;
            var connectionString = configuration["MongoDB:ConnectionString"];
            var databaseName = configuration["MongoDB:DatabaseName"];

            var mongoUri = MongoUrl.Create(connectionString);
            var client = new MongoClient(mongoUri);
            mongoDatabase = client.GetDatabase(databaseName);



        }
        public IMongoDatabase? Database => mongoDatabase;
    }
}
