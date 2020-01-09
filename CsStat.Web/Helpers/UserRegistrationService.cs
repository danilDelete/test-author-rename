using System;
using System.Collections.Generic;
using System.Linq;
using BusinessFacade.Repositories;
using CsStat.Domain.Definitions;
using CsStat.Domain.Entities;
using CsStat.Web.Models;
using DataService;
using DataService.Extensions;
using ErrorLogger;

namespace CsStat.Web.Helpers
{
    public enum SignUp
    {
        Succes,
        Fail
    }
    public class UserRegistrationService
    {
        private static IUserRepository _userRepository;
        private static ILogger _logger;

        public UserRegistrationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            var connectionString = new ConnectionStringFactory();
            var mongoRepository = new MongoRepositoryFactory(connectionString);
            _logger = new Logger(mongoRepository);
        }
        public Dictionary<SignUp, string> SignUp(SignInViewModel userModel)
        {
            var users = _userRepository.GetAll();

            if (users.Any(x => x.Name.ToLower() == userModel.Name.ToLower()))
            {
                return new Dictionary<SignUp, string>{{Helpers.SignUp.Fail, "User already exist"}};
            }
            
            var user = new User
            {
                Name = userModel.Name.ToLower(),
                PasswordHash = GetPasswordHash(userModel.Password),
            };

            try
            {
                _userRepository.Add(user);
            }
            catch(Exception ex)
            {
                _logger.Error(ex, "User has not been added");
                return new Dictionary<SignUp, string> {{Helpers.SignUp.Fail, "User has not been added"}} ;
            }

            return new Dictionary<SignUp, string> {{Helpers.SignUp.Succes, "User has been added"}};
        }

        public bool SignIn(SignInViewModel userModel)
        {
            var user = _userRepository.GetByName(userModel.Name);
            return user != null && user.VerifyPassword(userModel.Password);
        }
        
        private static string GetPasswordHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}