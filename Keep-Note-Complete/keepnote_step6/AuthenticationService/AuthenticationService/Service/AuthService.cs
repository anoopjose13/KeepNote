using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Exceptions;
using AuthenticationService.Models;
using AuthenticationService.Repository;

namespace AuthenticationService.Service
{
    
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository repository;
        public AuthService(IAuthRepository _repository)
        {
            this.repository = _repository;
        }
        /// <summary>
        /// Will return User details 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User LoginUser(string userId, string password)
        {
            try
            {
                var user = repository.LoginUser(userId, password);
                if(user ==null)
                {
                    throw new UserNotFoundException($"User with this id {userId} and password {password} does not exist");
                }
                else
                {
                    return user;
                }

            }
            catch(Exception Ex)
            {
                throw Ex;
            }
        }
        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool RegisterUser(User user)
        {
            try
            {
                var retVal = repository.RegisterUser(user);
                if(retVal)
                {
                    return retVal;
                }
                else
                {
                    throw new UserNotCreatedException($"User with this id {user.UserId} already exists");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
