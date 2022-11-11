using System.Net;
using AutoMapper;
using Moq;
using Movie_Library_Final_Project.AutoMapper;
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
            //Setup
            var expectedCount = 2;
            _userRepoMock.Setup(x => x.GetAllUsers())
                .ReturnsAsync(_users);

            //Inject
            GetAllUsersCommand command = new GetAllUsersCommand();
            GetAllUsersCommandHandler handler = new GetAllUsersCommandHandler(_userRepoMock.Object);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.Equal(expectedCount, result.Value.Count());
            Assert.Equal(_users, result.Value);
            Assert.Equal(_users.LastOrDefault(), result.Value.LastOrDefault());
            Assert.Equal(_users.FirstOrDefault(), result.Value.FirstOrDefault());
        }

        [Fact]
        public async Task Users_GetById_Ok()
        {
            //Setup
            var userId = 2;

            _userRepoMock.Setup(x => x.GetUserById(userId))
                .ReturnsAsync(_users.First(x => x.UserId == userId));

            //Inject
            GetUserByIdCommand command = new GetUserByIdCommand(userId);
            GetUserByIdCommandHandler handler = new GetUserByIdCommandHandler(_userRepoMock.Object);

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.NotNull(result.Value);
            Assert.Equal(userId, result.Value.UserId);
            Assert.Equal("Successfully gotten an user", result.Message);
        }
        [Fact]
        public async Task User_NotFound_GetById()
        {
            //Setup 
            var userId = 78;
            _userRepoMock.Setup(x => x.GetUserById(userId))
                .ReturnsAsync(_users.FirstOrDefault(x => x.UserId == userId));

            //Inject
            var command = new GetUserByIdCommand(userId);
            var handler = new GetUserByIdCommandHandler(_userRepoMock.Object);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Value);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, result.StatusCode);
        }
        [Fact]
        public async Task Add_User_Ok()
        {
            //Setup 
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

            //Inject
            var command = new AddUserCommand(userAdd);
            var handler = new AddUsersCommandHandler(_userRepoMock.Object, _mapper);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.Value.UserId);
        }

        [Fact]
        public async Task Add_User_NotOk()
        {
            //Setup
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
            var command = new AddUserCommand(userAdd);
            var handler = new AddUsersCommandHandler(_userRepoMock.Object, _mapper);
            //act
            var result = await handler.Handle(command, new CancellationToken());
            //assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
        }
        [Fact]
        public async Task Add_User_NullError()
        {
            //Setup
            var userAdd = new AddUserRequest()
            {
                Age = 10,
                Name = "Hitar Petar"
            };
            _userRepoMock.Setup(x => x.AddUser(It.IsAny<User>()))
                .ReturnsAsync((User)null);

            //Inject
            var command = new AddUserCommand(userAdd);
            var handler = new AddUsersCommandHandler(_userRepoMock.Object, _mapper);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Null(result.Value);
        }
        [Fact]
        public async Task DoesUserHaveSubscriptions_OK()
        {
            //Setup
            int userId = 99;
            _userRepoMock.Setup(x => x.DoesUserHaveSubscription(userId)).ReturnsAsync(true);
            //Inject
            var command = new DoesUserHaveSubscriptionCommand(userId);
            var handler = new DoesUserHaveSubscriptionCommandHandler(_userRepoMock.Object);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
            Assert.True(result.Value);
        }
        [Fact]
        public async Task DoesUserHaveSubscriptions_Not_OK()
        {
            //Setup
            int userId = 99;
            _userRepoMock.Setup(x => x.DoesUserHaveSubscription(userId)).ReturnsAsync(false);
            //Inject
            var command = new DoesUserHaveSubscriptionCommand(userId);
            var handler = new DoesUserHaveSubscriptionCommandHandler(_userRepoMock.Object);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.StatusCode);
            Assert.False(result.Value);
        }
        [Fact]
        public async Task Update_User_OK()
        {
            //Setup
            var userId = 2;
            var newUser = new UpdateUserRequest()
            {
                UserId = userId,
                Age = 10,
                Name = "Gosho"
            };
            var user = _users.First();
            _userRepoMock.Setup(x => x.UpdateUser(It.IsAny<User>()))
                .Callback(() =>
                {
                    user.Name = "Petur Petela";
                }).ReturnsAsync(() => user);
            //Inject
            var command = new UpdateUserCommand(newUser);
            var handler = new UpdateUserCommandHandler(_userRepoMock.Object, _mapper);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.Equal("Petur Petela", user.Name);
            Assert.Equal("Successfully added an user", result.Message);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }
        [Fact]
        public async Task Update_User_Not_OK()
        {
            //Setup
            var userId = 2;
            var newUser = new UpdateUserRequest()
            {
                UserId = userId,
                Age = 10,
                Name = "Gosho"
            };
            _userRepoMock.Setup(x => x.UpdateUser(It.IsAny<User>()))
                .ReturnsAsync((User)null);
            //Inject
            var command = new UpdateUserCommand(newUser);
            var handler = new UpdateUserCommandHandler(_userRepoMock.Object, _mapper);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.Null(result.Value);
            Assert.Equal("There is not user with such Id", result.Message);
            Assert.Equal(System.Net.HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task Delete_User_OK()
        {
            //Setup
            var userId = 2;
            var user = _users.FirstOrDefault(x => x.UserId == userId);
            var newUser = new UpdateUserRequest()
            {
                UserId = userId,
                Age = 10,
                Name = "Gosho"
            };
            _userRepoMock.Setup(x => x.DeleteUser(userId))
                .ReturnsAsync(() => user);
            //Inject
            var command = new DeleteUserCommand(userId);
            var handler = new DeleteUserCommandHandler(_userRepoMock.Object);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.Equal(user, result.Value);
            Assert.Equal("Successfully deleted an user", result.Message);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }
        [Fact]
        public async Task Delete_User_Not_OK()
        {
            //Setup
            var userId = 2;
            _userRepoMock.Setup(x => x.DoesUserHaveSubscription(userId))
                .ReturnsAsync(true);
            //Inject
            var command = new DeleteUserCommand(userId);
            var handler = new DeleteUserCommandHandler(_userRepoMock.Object);
            //Act
            var result = await handler.Handle(command, new CancellationToken());
            //Assert
            Assert.Null(result.Value);
            Assert.Equal("An user with a subscription cannot be deleted", result.Message);
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}