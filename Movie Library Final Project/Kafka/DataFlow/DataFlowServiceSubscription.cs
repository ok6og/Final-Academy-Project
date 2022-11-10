using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;
using System.Threading.Tasks.Dataflow;
using MovieLibrary.Kafka.DataFlow;

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
        private readonly TransformBlock<Subscription, MonthlyProfit> _transformBlockMonthlyProfit;
        private readonly ActionBlock<MonthlyProfit> _actionBlockMonthlyProfit;

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
            _transformBlockMonthlyProfit = new TransformBlock<Subscription, MonthlyProfit>(async sub =>
            {
                var thisMonthSubs = await _subscriptionRepository.GetAllSubscriptionsForMonth();
                var listTask = new List<Task<int>>();
                foreach (var item in thisMonthSubs)
                {
                    listTask.Add(_planRepo.GetPlanPrice(item.PlanId));
                }
                var allPlanPrices = await Task.WhenAll(listTask);
                MonthlyProfit thisProfit = new MonthlyProfit()
                {
                    Profit = allPlanPrices.Sum(),
                    UserSubscriptionsForMonth = thisMonthSubs.Count(),
                    Month = DateTime.Now.ToString("MMMM"),
                    Year = DateTime.Now.Year
                };
                return thisProfit;
            });
            _actionBlockMonthlyProfit = new ActionBlock<MonthlyProfit>(async sub =>
            {
                if (await _profitRepo.IsThereReportAlready())
                {
                    await _profitRepo.UpdateProfit(sub);
                }
                else
                {
                    await _profitRepo.AddMonthlyProfit(sub);
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
