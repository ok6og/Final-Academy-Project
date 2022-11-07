﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace MovieLibrary.Models.Models
{
    [MessagePackObject]
    public class MonthlyProfit
    {
        [Key(0)]
        public int MonthlyProfitId { get; set; }
        [Key(1)]
        public string Month { get; set; }
        [Key(2)]
        public int Profit { get; set; }
        [Key(3)]
        public int UserSubscriptions { get; set; }
        [Key(4)]
        public int Year { get; set; }
    }
}
