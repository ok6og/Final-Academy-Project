using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MovieLibrary.DL.Interfaces;
using MovieLibrary.Models.Models;

namespace MovieLibrary.DL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ILogger<UserRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<User?> AddUser(User user)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstAsync<User>("INSERT INTO [Users]  (Name, Age) output INSERTED.* VALUES (@Name, @Age)",
                        new {Name = user.Name, Age = user.Age});
                    _logger.LogInformation("Successfully added a user");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddUser)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<User?> DeleteUser(int userId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var deletedBook = await GetUserById(userId);
                    var result = await conn.ExecuteAsync("DELETE FROM USERS WHERE UserId = @Id", new { Id = userId });
                    _logger.LogInformation("Successfully deleted a movie");
                    return deletedBook;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(DeleteUser)}: {ex.Message}", ex);
            }
            return null;
        }

        public async Task<IEnumerable<User?>> GetAllUsers()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var query = "SELECT * FROM USERS WITH(NOLOCK)";
                    await conn.OpenAsync();
                    _logger.LogInformation("Successfully got all users");
                    return await conn.QueryAsync<User>(query);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllUsers)}: {ex.Message}", ex);
            }
            return Enumerable.Empty<User>();
        }

        public async Task<User?> GetUserById(int userId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstOrDefaultAsync<User>("SELECT * FROM USERS WITH(NOLOCK) WHERE UserId = @Id", new { Id = userId });
                    _logger.LogInformation("Successfully found a user");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetUserById)}: {ex.Message}", ex);
            }
            return null;
        }
        public async Task<User?> UpdateUser(User user)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryFirstAsync<User>("UPDATE USERS SET NAME = @Name, AGE = @Age output INSERTED.* WHERE USERID = @Id",
                        new { Name = user.Name, Age = user.Age});
                    _logger.LogInformation("Successfully updated a user");
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateUser)}: {ex.Message}", ex);
            }
            return null;
        }
        public async Task<bool> DoesUserHaveSubscription(int userId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var result = await conn.QueryAsync<User>("SELECT * FROM SUBSCRIPTIONS WITH(NOLOCK) WHERE UserId = @Id", new { Id = userId });
                    return result.Any();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetUserById)}: {ex.Message}", ex);
            }
            return false;
        }

    }
}
