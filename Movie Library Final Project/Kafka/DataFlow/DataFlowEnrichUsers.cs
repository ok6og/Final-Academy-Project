using System.Threading.Tasks.Dataflow;
using AutoMapper;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Responses;

namespace MovieLibrary.Kafka.DataFlow
{
    public class DataFlowEnrichUsers : IDataFlowEnrichUsersService
    {
        private readonly TransformBlock<Subscription, SubscriptionResponse> _transformBlock;
        private readonly ActionBlock<SubscriptionResponse> _actionBlockEnrichUsers;
        private readonly IMapper _mapper;
        private readonly IPlanRepository _planRepo;
        private readonly IUserRepository _userRepo;
        public DataFlowEnrichUsers(IMapper mapper, IPlanRepository planRepo, IUserRepository userRepo)
        {
            _mapper = mapper;
            _planRepo = planRepo;
            _userRepo = userRepo;
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
            _transformBlock.LinkTo(_actionBlockEnrichUsers);
        }

        public void HandleSubscriptions(Subscription subscription)
        {
            _transformBlock.Post(subscription);
        }
    }
}
