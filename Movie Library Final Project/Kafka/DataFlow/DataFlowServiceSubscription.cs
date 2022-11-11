using System.Threading.Tasks.Dataflow;
using AutoMapper;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Kafka.DataFlow;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.BL.Services
{
    public class DataFlowServiceSubscription : IDataFlowServiceSubscriptions
    {
        private readonly IUserRepository _userRepo;
        private readonly IPlanRepository _planRepo;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMonthlyProfitRepository _profitRepo;
        private readonly IMapper _mapper;
        private readonly TransformBlock<Subscription, SubscriptionResponse> _transformBlock;
        private readonly ActionBlock<SubscriptionResponse> _actionBlockEnrichUsers;
        private readonly TransformBlock<Subscription, List<MonthlyProfit>> _transformBlockMonthlyProfit;
        private readonly ActionBlock<List<MonthlyProfit>> _actionBlockMonthlyProfit;

        public DataFlowServiceSubscription(IUserRepository userRepo, IPlanRepository planRepo, ISubscriptionRepository subscriptionRepository, IMonthlyProfitRepository profitRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _planRepo = planRepo;
            _subscriptionRepository = subscriptionRepository;
            _profitRepo = profitRepo;
            _mapper = mapper;
            _transformBlock = new TransformBlock<Subscription, SubscriptionResponse>(async sub =>
            {
                var subResponse = _mapper.Map<SubscriptionResponse>(sub);
                subResponse.Plan = await _planRepo.GetPlanById(sub.PlanId);
                subResponse.User = await _userRepo.GetUserById(sub.UserId);
                return subResponse;
            });
            _actionBlockEnrichUsers = new ActionBlock<SubscriptionResponse>(async value =>
            {
                var updateUser = await _userRepo.GetUserById(value.User.UserId);
                int hasSubsmonths = 12 * (DateTime.Now.Year - value.ValidTill.Year) + DateTime.Now.Month - value.ValidTill.Month;
                updateUser.UserOnPlan = $"This user has a valid plan for {Math.Abs(hasSubsmonths)} months.";
                await _userRepo.UpdateUser(updateUser);
            });
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
                    if (await _profitRepo.IsThereReportAlready(report.Month,report.Year))
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
            _transformBlock.LinkTo(_actionBlockEnrichUsers);
        }
        public void HandleSubscriptions(Subscription subscription)
        {
            _transformBlock.Post(subscription);
            _transformBlockMonthlyProfit.Post(subscription);
        }
    }
}
