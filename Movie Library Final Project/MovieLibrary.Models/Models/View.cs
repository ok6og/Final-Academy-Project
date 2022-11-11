namespace MovieLibrary.Models.Models
{
    public record View
    {
        public int ViewId { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public int WatchedTillMinute { get; set; }
    }
}
