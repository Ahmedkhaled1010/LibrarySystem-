using EmailService.Domain.Contracts;
using EmailService.Domain.Models;
using EmailService.Infrastructure.Data.Context;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EmailService.Infrastructure.Data.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IMongoCollection<Email> _collection;
        public EmailRepository(MongoDb mongoDb)
        {
            var database = mongoDb.Database;
            _collection = database.GetCollection<Email>("Emails");
        }
        public async Task<IEnumerable<Email>> GetAllEmailsAsync() => await _collection.Find(_ => true).ToListAsync();
        public async Task<Email> GetEmailByIdAsync(string id) => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task AddEmailAsync(Email entity) => await _collection.InsertOneAsync(entity);

        public async Task DeleteEmailsync(string id) =>
        await _collection.DeleteOneAsync(x => x.Id == id);





        public async Task UpdateEmailsync(string id, Email entity)
        {
            var filter = Builders<Email>.Filter.Eq("_id", new ObjectId(id));

            var updateDefinitions = new List<UpdateDefinition<Email>>();
            foreach (var prop in typeof(Email).GetProperties())
            {
                var value = prop.GetValue(entity);
                if (prop.Name != "Id" && value != null)
                    updateDefinitions.Add(Builders<Email>.Update.Set(prop.Name, value));
            }
            if (updateDefinitions.Count > 0)
            {
                var update = Builders<Email>.Update.Combine(updateDefinitions);
                await _collection.UpdateOneAsync(filter, update);
            }
        }
    }
}
