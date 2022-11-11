namespace MovieLibrary.Models.Requests.MovieRequests
{
    public class AddMovieRequest
    {
        public string Title { get; set; }
        public int LengthInMinutes { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
    }
}
