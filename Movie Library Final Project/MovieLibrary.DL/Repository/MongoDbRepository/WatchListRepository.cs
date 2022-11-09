using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.MongoDbModels;

namespace MovieLibrary.DL.Repository.MongoDbRepository
{
    public class WatchListRepository : IWatchListRepository
    {
        private readonly IOptions<MongoDbModel> _mongoDbOptions;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Watchlist> _collection;
        private readonly IWatchedMoviesRepository _watchedRepo;
        public WatchListRepository(IOptions<MongoDbModel> mongoDbOptions, IWatchedMoviesRepository watchedRepo)
        {
            _mongoDbOptions = mongoDbOptions;
            _watchedRepo = watchedRepo;
            MongoClient dbClient = new MongoClient(_mongoDbOptions.Value.ConnectionString);
            _database = dbClient.GetDatabase(_mongoDbOptions.Value.DatabaseName);
            _collection = _database.GetCollection<Watchlist>(_mongoDbOptions.Value.CollectionNameWatchList);
        }
        public async Task<Watchlist?> AddWatchList(Watchlist watchlist)
        {
            await _collection.InsertOneAsync(watchlist);
            return watchlist;
        }

        public async Task EmptyWatchList(int userId)
        {
            await _collection.DeleteManyAsync(x => x.UserId == userId);
        }

        public async Task FinishWatchList(WatchedList list)
        {
            await _watchedRepo.SaveWatchedMovies(list);
        }

        public async Task<IEnumerable<Watchlist>> GetContent(int userId)
        {
            var collection = await _collection.FindAsync(x => x.UserId == userId);
            return await collection.ToListAsync();
        }

        public async Task<Watchlist> GetWatchList(int userId)
        {
            var collection = await _collection.FindAsync(x=> x.UserId == userId);
            return collection.FirstOrDefault();
        }

        public async Task<Movie?> RemoveFromWatchList(int userId, int movieId)
        {
            var watchlist = await GetWatchList(userId);
            var movieToRemove = watchlist.WatchList.FirstOrDefault(x => x.MovieId == movieId);
            watchlist.WatchList.Remove(movieToRemove);
            await _collection.ReplaceOneAsync(x => x.UserId == userId, watchlist);
            return movieToRemove;
        }

        public async Task RemoveWatchList(int userId)
        {
            await _collection.DeleteOneAsync(x => x.UserId == userId);
        }
    }
}
