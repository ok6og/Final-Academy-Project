using MessagePack;
using MovieLibrary.Models.Models;

namespace MovieLibrary.Models.Responses
{
    [MessagePackObject]
    public record SubscriptionResponse
    {
        [Key(0)]
        public int SubscriptionId { get; set; }
        [Key(1)]
        public DateTime CreatedAt { get; set; }
        [Key(2)]
        public DateTime ValidTill { get; set; }
        [Key(3)]
        public Plan Plan { get; set; }
        [Key(4)]
        public User User { get; set; }
    }
}
