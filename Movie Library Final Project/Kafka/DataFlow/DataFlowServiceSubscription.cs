using System.Threading.Tasks.Dataflow;
using AutoMapper;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Kafka.DataFlow;
using MovieLibrary.Models.Models;

namespace MovieLibrary.BL.Services
{
    public class DataFlowServiceSubscription : IDataFlowMonthlyProfitService
    {
        private readonly IPlanRepository _planRepo;
        private readonly IMonthlyProfitRepository _profitRepo;
        private readonly IMapper _mapper;
        private readonly TransformBlock<Subscription, List<MonthlyProfit>> _transformBlockMonthlyProfit;
        private readonly ActionBlock<List<MonthlyProfit>> _actionBlockMonthlyProfit;

        public DataFlowServiceSubscription(IUserRepository userRepo, IPlanRepository planRepo, ISubscriptionRepository subscriptionRepository, IMonthlyProfitRepository profitRepo, IMapper mapper)
        {
            _planRepo = planRepo;
            _profitRepo = profitRepo;
            _mapper = mapper;
            _transformBlockMonthlyProfit = new TransformBlock<Subscription, List<MonthlyProfit>>(async sub =>
            {
                int hasSubsmonths = Math.Abs(12 * (DateTime.Now.Year - sub.ValidTill.Year) + DateTime.Now.Month - sub.ValidTill.Month);
                var planPrice = await _planRepo.GetPlanPrice(sub.PlanId);
                var listOfTasks = new List<MonthlyProfit>();

                for (int i = 0; i < hasSubsmonths; i++)
                {
                    listOfTasks.Add(new MonthlyProfit
                    {
                        Profit = planPrice,
                        UserSubscriptionsForMonth = 1,
                        Month = DateTime.Now.AddMonths(i).ToString("MMMM"),
                        Year = DateTime.Now.AddMonths(i).Year
                    });
                }
                return listOfTasks;
            });
            _actionBlockMonthlyProfit = new ActionBlock<List<MonthlyProfit>>(async sub =>
            {
                foreach (var report in sub)
                {
                    if (await _profitRepo.IsThereReportAlready(report.Month, report.Year))
                    {
                        await _profitRepo.IncreaseMonthlyProfit(report);
                    }
                    else
                    {
                        await _profitRepo.AddMonthlyProfit(report);
                    }
                }
            });
            _transformBlockMonthlyProfit.LinkTo(_actionBlockMonthlyProfit);
        }
        public void HandleSubscriptions(Subscription subscription)
        {
            _transformBlockMonthlyProfit.Post(subscription);
        }
    }
}
