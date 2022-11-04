using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

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
    }
}
