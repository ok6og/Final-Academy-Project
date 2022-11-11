using MessagePack;

namespace MovieLibrary.Models.Models
{
    [MessagePackObject]
    public record Plan
    {
        [Key(0)]
        public int PlanId { get; set; }
        [Key(1)]
        public string Type { get; set; }
        [Key(2)]
        public int PricePerMonth { get; set; }
    }
}
