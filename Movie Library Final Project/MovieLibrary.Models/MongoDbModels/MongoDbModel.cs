namespace MovieLibrary.Models.MongoDbModels
{
    public class MongoDbModel
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionNameWatchList { get; set; }
        public string CollectionNameWatchedList { get; set; }
    }
}
