﻿using MovieLibrary.Models.Models;

namespace MovieLibrary.Kafka.DataFlow
{
    public interface IDataFlowMonthlyProfitService
    {
        public void HandleSubscriptions(Subscription subscription);
    }
}
