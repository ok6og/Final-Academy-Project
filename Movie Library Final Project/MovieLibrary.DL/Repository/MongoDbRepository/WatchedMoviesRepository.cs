using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.MongoDbModels;

namespace MovieLibrary.DL.Repository.MongoDbRepository
{
    public class WatchedMoviesRepository : IWatchedMoviesRepository
    {
        private readonly IOptions<MongoDbModel> _mongoDbOptions;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<WatchedList> _collection;

        public WatchedMoviesRepository(IOptions<MongoDbModel> mongoDbOptions)
        {
            _mongoDbOptions = mongoDbOptions;
            MongoClient dbClient = new MongoClient(_mongoDbOptions.Value.ConnectionString);
            _database = dbClient.GetDatabase(_mongoDbOptions.Value.DatabaseName);
            _collection = _database.GetCollection<WatchedList>(_mongoDbOptions.Value.CollectionNameWatchedList);
        }
        public async Task DeleteWatchedMovie(int userId)
        {
            await _collection.DeleteOneAsync(x => x.UserId == userId);
            return;
        }

        public async Task<IEnumerable<WatchedList>> GetWatchedMovies(int userId)
        {
            var collection = await _collection.FindAsync(x => x.UserId == userId);
            return await collection.ToListAsync();
        }

        public async Task<WatchedList?> SaveWatchedMovies(WatchedList watch)
        {
            await _collection.InsertOneAsync(watch);
            return watch;
        }
        public async Task<WatchedList> UpdateWatchedMovies(WatchedList watchedList)
        {
            await _collection.ReplaceOneAsync(x => x.UserId == watchedList.UserId, watchedList);
            return watchedList;
        }
    }
}
