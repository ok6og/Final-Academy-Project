using System.Net;
using AutoMapper;
using Kafka.KafkaConfig;
using Microsoft.Extensions.Options;
using Moq;
using Movie_Library_Final_Project.AutoMapper;
using MovieLibrary.BL.CommandHandlers.SubscriptionCommandHandlers;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.SubscriptionCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Requests.SubscriptionRequests;

namespace MovieLibrary.Test
{
    public class Subscriptionest
    {
        private readonly IList<Subscription> _subscriptions = new List<Subscription>()
        {
            new Subscription()
            {
                CreatedAt = DateTime.Now,
                ValidTill = DateTime.Now.AddMonths(5),
                PlanId = 1,
                SubscriptionId = 1,
                UserId = 1
            },
            new Subscription()
            {
                CreatedAt = DateTime.Now,
                ValidTill = DateTime.Now.AddMonths(10),
                PlanId = 2,
                SubscriptionId = 2,
                UserId = 2
            }
        };

        private readonly IMapper _mapper;
        private readonly Mock<ISubscriptionRepository> _subsRepoMock;
        private readonly Mock<IUserRepository> _userRepoMock;
        private readonly Mock<IPlanRepository> _planRepoMock;
        private readonly Mock<IOptionsMonitor<List<MyKafkaSettings>>> _config;

        public Subscriptionest()
        {
            var mockMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappings());
            });
            _mapper = mockMapperConfig.CreateMapper();
            _subsRepoMock = new Mock<ISubscriptionRepository>();
            _planRepoMock = new Mock<IPlanRepository>();
            _userRepoMock = new Mock<IUserRepository>();
            _config = new Mock<IOptionsMonitor<List<MyKafkaSettings>>>();
        }

        [Fact]
        public async Task Subscription_Get_All_Check()
        {
            //Setup
            var expectedCount = 2;
            _subsRepoMock.Setup(x => x.GetAllSubscriptions())
                .ReturnsAsync(_subscriptions);
            //Inject
            GetAllSubscriptionsCommand command = new GetAllSubscriptionsCommand();
            GetAllSubscriptionsCommandHandler handler = new GetAllSubscriptionsCommandHandler(_subsRepoMock.Object);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.Equal(expectedCount, result.Value.Count());
            Assert.Equal(_subscriptions, result.Value);
            Assert.Equal(_subscriptions.FirstOrDefault(), result.Value.FirstOrDefault());
            Assert.Equal(_subscriptions.LastOrDefault(), result.Value.LastOrDefault());
        }
        [Fact]
        public async Task Subscription_GetById_Ok()
        {
            //Setup
            var subId = 2;
            _subsRepoMock.Setup(x => x.GetSubscriptionById(subId))
                .ReturnsAsync(_subscriptions.First(x => x.SubscriptionId == subId));
            //Inject
            GetSubscriptionByIdCommand command = new GetSubscriptionByIdCommand(subId);
            GetSubscriptionByIdCommandHandler handler = new GetSubscriptionByIdCommandHandler(_subsRepoMock.Object);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.NotNull(result.Value);
            Assert.Equal(subId, result.Value.SubscriptionId);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
        [Fact]
        public async Task Subscription_NotFound_GetById()
        {
            //Setup 
            var subId = 78;
            _subsRepoMock.Setup(x => x.GetSubscriptionById(subId))
                .ReturnsAsync(_subscriptions.FirstOrDefault(x => x.SubscriptionId == subId));

            //Inject
            GetSubscriptionByIdCommand command = new GetSubscriptionByIdCommand(subId);
            GetSubscriptionByIdCommandHandler handler = new GetSubscriptionByIdCommandHandler(_subsRepoMock.Object);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Value);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, result.StatusCode);
        }
        [Fact]
        public async Task Add_Subscription_Ok()
        {
            //Setup 
            var subId = 2;
            var planId = 2;
            var userId = 2;
            var months = 10;
            var subAdd = new AddSubscriptionRequest()
            {
                PlanId = planId,
                UserId = userId
            };
            _subsRepoMock.Setup(x => x.AddSubscription(It.IsAny<Subscription>(), It.IsAny<int>()))
                .ReturnsAsync(() => _subscriptions.FirstOrDefault(x => x.SubscriptionId == subId));
            _userRepoMock.Setup(x => x.GetUserById(userId))
                .ReturnsAsync(new User
                {
                    Age = 10,
                    Name = "Gosho",
                    UserId = userId,
                    UserOnPlan = null
                });
            _planRepoMock.Setup(x => x.GetPlanById(planId))
                .ReturnsAsync(new Plan
                {
                    PlanId = planId,
                    PricePerMonth = 10,
                    Type = "SomethingSomething"
                });
            _config.Setup(x => x.CurrentValue)
                .Returns(new List<MyKafkaSettings>() { new MyKafkaSettings() { objectType = typeof(Subscription).Name, BootstrapServers = "" } });
            //Inject
            AddSubscriptionCommand command = new AddSubscriptionCommand(subAdd, months);
            AddSubscriptionCommandHandler handler = new AddSubscriptionCommandHandler(_subsRepoMock.Object, _mapper, _planRepoMock.Object, _userRepoMock.Object, _config.Object);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.NotNull(result.Value);
            Assert.Equal(subId, result.Value.SubscriptionId);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Update_Subscription_OK()
        {
            //Setup
            var subscriptionId = 2;
            var newSubscription = new UpdateSubscriptionRequest()
            {
                CreatedAt = DateTime.Now,
                PlanId = subscriptionId,
                SubscriptionId = subscriptionId,
                UserId = subscriptionId,
                ValidTill = DateTime.Now.AddMonths(5)
            };
            var subs = _subscriptions.First();
            _subsRepoMock.Setup(x => x.UpdatSubscription(It.IsAny<Subscription>()))
                .ReturnsAsync(() => subs);
            //Inject
            UpdateSubscriptionCommand command = new UpdateSubscriptionCommand(newSubscription);
            UpdateSubscriptionCommandHandler handler = new UpdateSubscriptionCommandHandler(_subsRepoMock.Object, _mapper);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.Equal("Successfully updated a subscrption", result.Message);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task Update_Subscription_Not_OK()
        {
            //Setup
            var subsId = 2;
            var newSubs = new UpdateSubscriptionRequest()
            {
                CreatedAt = DateTime.Now,
                PlanId = subsId,
                SubscriptionId = subsId,
                UserId = subsId,
                ValidTill = DateTime.Now.AddMonths(subsId)
            };
            var subs = _subscriptions.First();
            _subsRepoMock.Setup(x => x.UpdatSubscription(It.IsAny<Subscription>()))
                .ReturnsAsync((Subscription)null);
            //Inject
            UpdateSubscriptionCommand command = new UpdateSubscriptionCommand(newSubs);
            UpdateSubscriptionCommandHandler handler = new UpdateSubscriptionCommandHandler(_subsRepoMock.Object, _mapper);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.Equal("There is not subscription with such Id", result.Message);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, result.StatusCode);
        }
        [Fact]
        public async Task Delete_Subscriptions_Ok()
        {
            //Setup
            var subsId = 1;
            var subscription = _subscriptions.FirstOrDefault(x => x.SubscriptionId == subsId);
            var newSubscription = new Subscription()
            {
                CreatedAt = DateTime.Now,
                PlanId = subsId,
                SubscriptionId = subsId,
                UserId = subsId,
                ValidTill = DateTime.Now.AddMonths(subsId),
            };
            _subsRepoMock.Setup(x => x.DeleteSubscription(subsId))
                .ReturnsAsync(() => subscription);
            //Inject
            DeleteSubscriptionCommand command = new DeleteSubscriptionCommand(subsId);
            DeleteSubscriptionCommandHandler handler = new DeleteSubscriptionCommandHandler(_subsRepoMock.Object);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.Equal(subscription, result.Value);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
        [Fact]
        public async Task Delete_Subscription_Not_Ok()
        {
            //Setup
            var subscriptionId = 2;
            _subsRepoMock.Setup(x => x.DeleteSubscription(subscriptionId))
                .ReturnsAsync((Subscription)null);
            //Inject
            DeleteSubscriptionCommand command = new DeleteSubscriptionCommand(subscriptionId);
            DeleteSubscriptionCommandHandler handler = new DeleteSubscriptionCommandHandler(_subsRepoMock.Object);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.Null(result.Value);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
