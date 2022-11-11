namespace MovieLibrary.Models.Models
{
    public record Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int LengthInMinutes { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
    }
}
