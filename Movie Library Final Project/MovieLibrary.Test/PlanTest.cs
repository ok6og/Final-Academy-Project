using System.Net;
using AutoMapper;
using Moq;
using Movie_Library_Final_Project.AutoMapper;
using MovieLibrary.BL.CommandHandlers.PlanCommandHandlers;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.PlanCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Requests.PlanRequests;

namespace MovieLibrary.Test
{
    public class PlanTest
    {
        private readonly IList<Plan> _plans = new List<Plan>()
        {
            new Plan()
            {
                PlanId = 10,
                PricePerMonth = 10,
                Type = "Premium"
            },
            new Plan()
            {
                PlanId = 20,
                PricePerMonth = 20,
                Type = "Extra Super Ultra HD Premium Plan"
            }
        };

        private readonly IMapper _mapper;
        private readonly Mock<IPlanRepository> _planRepoMock;

        public PlanTest()
        {
            var mockMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappings());
            });
            _mapper = mockMapperConfig.CreateMapper();
            _planRepoMock = new Mock<IPlanRepository>();
        }

        [Fact]
        public async Task Plan_GetAll_OK()
        {
            //Setup
            var expectedCount = 2;
            _planRepoMock.Setup(x => x.GetAllPlans())
                .ReturnsAsync(_plans);
            //Inject
            GetAllPlansCommand command = new GetAllPlansCommand();
            GetAllPlansCommandHandler handler = new GetAllPlansCommandHandler(_planRepoMock.Object);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.Equal(expectedCount, result.Value.Count());
            Assert.Equal(_plans, result.Value);
            Assert.Equal(_plans.FirstOrDefault(), result.Value.FirstOrDefault());
            Assert.Equal(_plans.LastOrDefault(), result.Value.LastOrDefault());
        }

        [Fact]
        public async Task Plans_GetById_OK()
        {
            //Setup
            var planId = 20;
            _planRepoMock.Setup(x => x.GetPlanById(planId))
                .ReturnsAsync(_plans.First(x => x.PlanId == planId));
            //Inject
            GetPlanByIdCommand command = new GetPlanByIdCommand(planId);
            GetPlanByIdCommandHandler handler = new GetPlanByIdCommandHandler(_planRepoMock.Object);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.Equal(planId, result.Value.PlanId);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
        [Fact]
        public async Task Plan_GetById_NotFound()
        {
            //Setup
            var planId = 2321443;
            _planRepoMock.Setup(x => x.GetPlanById(planId))
                .ReturnsAsync((Plan)null);
            //Inject
            GetPlanByIdCommand command = new GetPlanByIdCommand(planId);
            GetPlanByIdCommandHandler handler = new GetPlanByIdCommandHandler(_planRepoMock.Object);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Value);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
        [Fact]
        public async Task Add_Plan_Ok()
        {
            //Setup
            var planId = 10;
            var planToAdd = new AddPlanRequest()
            {
                PricePerMonth = 69,
                Type = "Extra Spicy"
            };
            _planRepoMock.Setup(x => x.AddPlan(It.IsAny<Plan>()))
                .ReturnsAsync(() => _plans.FirstOrDefault(x => x.PlanId == planId));
            //Inject
            AddPlanCommand command = new AddPlanCommand(planToAdd);
            AddPlanCommandHandler handler = new AddPlanCommandHandler(_planRepoMock.Object, _mapper);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(planId, result.Value.PlanId);
        }
        [Fact]
        public async Task Add_Plan_Not_Ok()
        {
            //Setup
            var planToAdd = new AddPlanRequest()
            {
                PricePerMonth = 69,
                Type = "Extra Spicy"
            };
            _planRepoMock.Setup(x => x.AddPlan(It.IsAny<Plan>()))
                .ReturnsAsync((Plan)null);
            //Inject
            AddPlanCommand command = new AddPlanCommand(planToAdd);
            AddPlanCommandHandler handler = new AddPlanCommandHandler(_planRepoMock.Object, _mapper);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Value);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task Update_Plan_OK()
        {
            //Setup
            var userId = 2;
            var newUser = new UpdatePlanRequest()
            {
                PlanId = 10,
                PricePerMonth = 10,
                Type = "10"
            };
            var user = _plans.First();
            _planRepoMock.Setup(x => x.UpdatPlan(It.IsAny<Plan>()))
                .ReturnsAsync(() => user);
            //Inject
            var command = new UpdatePlanCommand(newUser);
            var handler = new UpdatePlanCommandHandler(_planRepoMock.Object, _mapper);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.Equal("Successfully updated a plan", result.Message);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }
        [Fact]
        public async Task Update_Plan_Not_OK()
        {
            //Setup
            var planId = 2;
            var newPlan = new UpdatePlanRequest()
            {
                PlanId = 10,
                PricePerMonth = 10,
                Type = "10"
            };
            var user = _plans.First();
            _planRepoMock.Setup(x => x.UpdatPlan(It.IsAny<Plan>()))
                .ReturnsAsync((Plan)null);
            //Inject
            var command = new UpdatePlanCommand(newPlan);
            var handler = new UpdatePlanCommandHandler(_planRepoMock.Object, _mapper);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.Equal("There is no plan with such Id", result.Message);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, result.StatusCode);
        }
        [Fact]
        public async Task Delete_Plan_Ok()
        {
            //Setup
            var planId = 10;
            var plan = _plans.FirstOrDefault(x => x.PlanId == planId);
            var newPlan = new Plan()
            {
                PlanId = planId,
                PricePerMonth = 10,
                Type = "Some kind of type"
            };
            _planRepoMock.Setup(x => x.DeletePlan(planId))
                .ReturnsAsync(() => plan);
            //Inject
            DeletePlanCommand command = new DeletePlanCommand(planId);
            DeletePlanCommandHandler handler = new DeletePlanCommandHandler(_planRepoMock.Object);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.Equal(plan, result.Value);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
        [Fact]
        public async Task Delete_Plan_Not_Ok()
        {
            //Setup
            var planId = 2;
            _planRepoMock.Setup(x => x.DeletePlan(planId))
                .ReturnsAsync((Plan)null);
            //Inject
            DeletePlanCommand command = new DeletePlanCommand(planId);
            DeletePlanCommandHandler handler = new DeletePlanCommandHandler(_planRepoMock.Object);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.Null(result.Value);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
