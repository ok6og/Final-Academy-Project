namespace MovieLibrary.Models.Requests.UserRequests
{
    public class UpdateUserRequest
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
