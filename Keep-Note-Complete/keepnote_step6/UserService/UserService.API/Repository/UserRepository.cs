using System.Linq;
using MongoDB.Driver;
using UserService.API.Models;

namespace UserService.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserContext _context;
        public UserRepository(IUserContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Used to delete user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteUser(string userId)
        {
            var filter = Builders<User>.Filter.Eq(u => u.UserId, userId);

            var result = _context.Users.DeleteOneAsync(filter).Result;
            return result.IsAcknowledged; throw new System.NotImplementedException();
        }
        /// <summary>
        /// Get user details by id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUserById(string userId)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(u => u.UserId, userId);
            return _context.Users.Find(filter).FirstOrDefault();
        }
        /// <summary>
        /// Used to register the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User RegisterUser(User user)
        {
            _context.Users.InsertOne(user);
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(u => u.UserId, user.UserId);
            return _context.Users.Find(filter).FirstOrDefault();
        }
        /// <summary>
        /// Used to update the user 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUser(string userId, User user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.UserId, userId);
            var update = Builders<User>.Update
                .Set("Name", user.Name).Set("Password", user.Password).Set("Contact", user.Contact).Set("AddedDate", user.AddedDate);
            var result = _context.Users.UpdateOneAsync(filter, update).Result;
            return result.IsAcknowledged;
        }
    }
}
