﻿using MessagePack;

namespace MovieLibrary.Models.Models
{
    [MessagePackObject]
    public record User
    {
        [Key(0)]
        public int UserId { get; set; }
        [Key(1)]
        public string Name { get; set; }
        [Key(2)]
        public int Age { get; set; }
        [Key(3)]
        public string UserOnPlan { get; set; }
    }
}
