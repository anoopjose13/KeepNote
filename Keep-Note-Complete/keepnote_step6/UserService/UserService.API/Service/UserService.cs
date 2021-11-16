using System;
using UserService.API.Exceptions;
using UserService.API.Models;
using UserService.API.Repository;

namespace UserService.API.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        /// <summary>
        /// Used to delete the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteUser(string userId)
        {
            try
            {
                if (!_userRepository.DeleteUser(userId))
                {
                    throw new UserNotFoundException("This user id does not exist");
                }
                else
                { return true; }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        /// <summary>
        /// Get user y id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUserById(string userId)
        {
            try
            {
                var result = _userRepository.GetUserById(userId);
                if (result == null)
                    throw new UserNotFoundException("This user id does not exist");
                else return result;
            }

            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        /// <summary>
        /// Used to register the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User RegisterUser(User user)
        {
            try
            {
                var result = _userRepository.RegisterUser(user);
                if (result == null)
                {
                    throw new UserNotCreatedException("This user id already exists");
                }
                else
                    return result;

            }

            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        /// <summary>
        /// Used to update the user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUser(string userId, User user)
        {
            try
            {
                var result = _userRepository.UpdateUser(userId, user);
                if (!result)
                {
                    throw new UserNotFoundException("This user id does not exist");
                }
                else
                    return result;
            }

            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
