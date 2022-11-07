using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Movie_Library_Final_Project.AutoMapper;
using Movie_Library_Final_Project.Controllers;
using MovieLibrary.BL.CommandHandlers.UserCommandHandlers;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Mediatr.UserCommands;
using MovieLibrary.Models.Models;
using MovieLibrary.Models.Requests.UserRequests;

namespace MovieLibrary.Test
{
    public class UserTest
    {
        private readonly IList<User> _users = new List<User>()
        {
            new User()
            {
                UserId=1,
                Age=18,
                Name="Georgi",
                UserOnPlan = null
            },
            new User()
            {
                UserId = 2,
                Age = 20,
                Name="Mr.WorldWide",
                UserOnPlan = "somePlan"
            }
        };

        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _userRepoMock;

        public UserTest()
        {
            var mockMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappings());
            });
            _mapper = mockMapperConfig.CreateMapper();
            _userRepoMock = new Mock<IUserRepository>();
            
        }
        [Fact]
        public async Task User_Get_All_Check()
        {
            //setup
            var expectedCount = 2;
            _userRepoMock.Setup(x => x.GetAllUsers())
                .ReturnsAsync(_users);

            //inject
            var mediatr = new Mock<IMediator>();

            GetAllUsersCommand command = new GetAllUsersCommand();
            GetAllUsersCommandHandler handler = new GetAllUsersCommandHandler(_userRepoMock.Object);

            //Act
            var x = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.Equal(expectedCount, x.Count());
            Assert.Equal(_users, x);
        }

        [Fact]
        public async Task Users_Are_Same_GetAll()
        {
            //setup
            var expectedCount = 2;
            _userRepoMock.Setup(x => x.GetAllUsers())
                .ReturnsAsync(_users);

            //inject
            var mediatr = new Mock<IMediator>();

            GetAllUsersCommand command = new GetAllUsersCommand();
            GetAllUsersCommandHandler handler = new GetAllUsersCommandHandler(_userRepoMock.Object);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.Equal(_users.LastOrDefault(), result.LastOrDefault());
            Assert.Equal(_users.FirstOrDefault(), result.FirstOrDefault());
        }

        [Fact]
        public async Task Users_GetById_Ok()
        {
            //setup
            var userId = 2;
            var expectedUser = _users.First(x=> x.UserId == userId);

            _userRepoMock.Setup(x => x.GetUserById(userId))
                .ReturnsAsync(_users.First(x=> x.UserId == userId));

            //inject
            var mediatr = new Mock<IMediator>();
            GetUserByIdCommand command = new GetUserByIdCommand(userId);
            GetUserByIdCommandHandler handler = new GetUserByIdCommandHandler(_userRepoMock.Object);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
        }
        [Fact]
        public async Task User_NotFound_GetById()
        {
            //setup 
            var userId = 78;
            _userRepoMock.Setup(x => x.GetUserById(userId))
                .ReturnsAsync(_users.FirstOrDefault(x => x.UserId == userId));

            //inject
            var mediatr = new Mock<IMediator>();
            var command = new GetUserByIdCommand(userId);
            var handler = new GetUserByIdCommandHandler(_userRepoMock.Object);
            //act
            var result = await handler.Handle(command, new CancellationToken());
            //assert
            Assert.Null(result);
        }
        [Fact]
        public async Task Add_User_Ok()
        {
            //setup 
            var userId = 3;
            var userAdd = new AddUserRequest()
            {
                Age = 10,
                Name = "Hitar Petar"
            };
            _userRepoMock.Setup(x => x.AddUser(It.IsAny<User>()))
                .Callback(() =>
                {
                    var user1 = new User()
                    {
                        UserId = userId,
                        Name = userAdd.Name,
                        Age = userAdd.Age,
                    };
                    _users.Add(user1);
                }).ReturnsAsync(() => _users.FirstOrDefault(x => x.UserId == userId));

            //inject
            var mediatr = new Mock<IMediator>();
            var command = new AddUserCommand(userAdd);
            var handler = new AddUsersCommandHandler(_userRepoMock.Object,_mapper);
            //act
            var result = await handler.Handle(command, new CancellationToken());
            //assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
        }

        [Fact]
        public async Task Add_User_NotOk()
        {
            //setup  SHOULD MAKE HTTPRESPONSE MODEL FOR THIS TO WORK OR INJECT CONTROLLER
            var userId = -1;
            var userAdd = new AddUserRequest()
            {
                Age = 10,
                Name = "Hitar Petar"
            };
            _userRepoMock.Setup(x => x.AddUser(It.IsAny<User>()))
                .Callback(() =>
                {
                    var user1 = new User()
                    {
                        UserId = userId,
                        Name = userAdd.Name,
                        Age = userAdd.Age,
                    };
                    _users.Add(user1);
                }).ReturnsAsync(() => _users.FirstOrDefault(x => x.UserId == userId));

            //inject
            var mediatr = new Mock<IMediator>();
            var command = new AddUserCommand(userAdd);
            var handler = new AddUsersCommandHandler(_userRepoMock.Object, _mapper);
            //act
            var result = await handler.Handle(command, new CancellationToken());
            //assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
        }
    }
}