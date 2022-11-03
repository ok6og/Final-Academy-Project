﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Models.Requests.SubscriptionRequests
{
    public class AddSubscriptionRequest
    {
        public int PlanId { get; set; }
        public int UserId { get; set; }
        public DateTime SubscribedAtDate { get; set; }
        public DateTime SubscriptionValidTill { get; set; }
    }
}