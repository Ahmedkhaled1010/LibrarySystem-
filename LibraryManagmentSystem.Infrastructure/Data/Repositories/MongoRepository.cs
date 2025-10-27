using LibraryManagmentSystem.Domain.Contracts;
using LibraryManagmentSystem.Domain.Entity;
using LibraryManagmentSystem.Infrastructure.Data.MongoContext;
using MongoDB.Driver;

namespace LibraryManagmentSystem.Infrastructure.Data.Repositories
{
    public class MongoRepository<TEntity> : IMongoRepository<TEntity> where TEntity : BaseMongoEntity
    {
        private readonly IMongoCollection<TEntity> _collection;
        public MongoRepository(MongoDb mongoDb)
        {
            var database = mongoDb.Database;
            _collection = database.GetCollection<TEntity>(typeof(TEntity).Name + "s");


        }
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _collection.Find(_ => true).ToListAsync();
        public async Task<TEntity> GetByIdAsync(Guid id) =>
        await _collection.Find(x => EqualityComparer<Guid>.Default.Equals(x.Id, id)).FirstOrDefaultAsync();


        public async Task AddAsync(TEntity entity) => await _collection.InsertOneAsync(entity);

        public async Task DeleteAsync(Guid id) =>
       await _collection.DeleteOneAsync(x => EqualityComparer<Guid>.Default.Equals(x.Id, id));





        public async Task UpdateAsync(Guid id, TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Where(x => EqualityComparer<Guid>.Default.Equals(x.Id, id));

            var updateDefinitions = new List<UpdateDefinition<TEntity>>();
            foreach (var prop in typeof(TEntity).GetProperties())
            {
                var value = prop.GetValue(entity);
                if (prop.Name != "Id" && value != null)
                    updateDefinitions.Add(Builders<TEntity>.Update.Set(prop.Name, value));
            }
            if (updateDefinitions.Count > 0)
            {
                var update = Builders<TEntity>.Update.Combine(updateDefinitions);
                await _collection.UpdateOneAsync(filter, update);
            }
        }
    }
}
