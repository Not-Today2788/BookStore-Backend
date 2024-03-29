using CommonLayer.RequestModels;
using ManagerLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Services
{
    public class UserManager: IUserManager
    {
        private readonly IUserRepository iuserRepository;

        public UserManager(IUserRepository iuserRepository)
        {
            this.iuserRepository = iuserRepository;
        }

        public UserEntity UserRegistration(RegisterModel registerModel)
        {
            return iuserRepository.UserRegistration(registerModel);
        }
        public string UserLogin(LoginModel loginModel)
        {
            return iuserRepository.UserLogin(loginModel); 
        }

        public string ResetPassword(string email, ResetPasswordModel model)
        {
            return iuserRepository.ResetPassword(email, model);
        }
        public ForgotPasswordModel ForgotPassword(string email)
        {
            return iuserRepository.ForgotPassword(email);
        }
        public bool CheckEmail(string email)
        {
            return iuserRepository.CheckEmail(email);
        }
    }
}
