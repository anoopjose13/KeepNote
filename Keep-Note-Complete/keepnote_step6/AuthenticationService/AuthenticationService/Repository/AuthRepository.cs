using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Models;

namespace AuthenticationService.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IAuthenticationContext authContext;
        /// <summary>
        /// Construction Injuction
        /// </summary>
        /// <param name="_authContext"></param>
        public AuthRepository(IAuthenticationContext _authContext)
        {
            authContext = _authContext;
        }
        /// <summary>
        /// Find user by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User FindUserById(string userId)
        {
           var user = authContext.Users.Where(s => s.UserId == userId).FirstOrDefault();
            return user;
        }
        /// <summary>
        /// Will return the User details if exist
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User LoginUser(string userId, string password)
        {
            var user = authContext.Users.Where(s => s.UserId == userId && s.Password == password).FirstOrDefault();
            return user;
        }
        /// <summary>
        /// Used to register new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool RegisterUser(User user)
        {
            authContext.Users.Add(user);
            var retVal = authContext.SaveChanges();
            return Convert.ToBoolean(retVal);
        }
    }
}
