using CommonLayer.RequestModels;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interfaces
{
    public interface IUserManager
    {
        public UserEntity UserRegistration(RegisterModel registerModel);
        public string UserLogin(LoginModel loginModel);
        public string ResetPassword(string email, ResetPasswordModel model);
        public ForgotPasswordModel ForgotPassword(string email);
        public bool CheckEmail(string email);
    }
}
