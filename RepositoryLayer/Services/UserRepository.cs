using CommonLayer.RequestModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RepositoryLayer.Services
{
    public class UserRepository:IUserRepository
    {
        private readonly BookStoreContext context;
        private readonly IConfiguration iconfig;
        private readonly BcryptEncryption bcrypt;

        public UserRepository(BookStoreContext context, IConfiguration iconfig)
        {
            this.context = context;
            this.iconfig = iconfig;
            this.bcrypt = new BcryptEncryption();
        }

        public UserEntity UserRegistration(RegisterModel registerModel)
        {
            var user= context.UserTable.FirstOrDefault(x=>x.UserEmail == registerModel.UserEmail);
            if(user != null)
            {
                throw new Exception("user already exists");
            }

            UserEntity userEntity = new UserEntity
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                UserEmail = registerModel.UserEmail,
                UserPassword = bcrypt.HashPassGenerator(registerModel.UserPassword)                
            };

            context.UserTable.Add(userEntity);
            context.SaveChanges();

            return userEntity;
        }

        public string UserLogin(LoginModel loginModel)
        {
            UserEntity user = context.UserTable.FirstOrDefault(x => x.UserEmail == loginModel.useremail);

            if (user != null)
            {
                if (bcrypt.MatchPass(loginModel.userpassword, user.UserPassword))
                {
                    //user.LastLoginTime = DateTime.Now;
                    string token = GenerateToken(user.UserEmail, user.UserId);
                    return token;
                }
                else
                {
                    throw new ArgumentException("Incorrect password");
                }
            }
            else
            {
                throw new ArgumentException("Incorrect email");
            }
        }


        private string GenerateToken(string Email, int userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(iconfig["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("UserEmail", Email),
                new Claim("UserId", userId.ToString())
            };

            var token = new JwtSecurityToken(
                iconfig["Jwt:Issuer"],
                iconfig["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public ForgotPasswordModel ForgotPassword(string email)
        {
            var entity = context.UserTable.FirstOrDefault(user => user.UserEmail == email);

            if (entity != null)
            {
                ForgotPasswordModel model = new ForgotPasswordModel
                {
                    UserId = entity.UserId,
                    UserEmail = entity.UserEmail,
                    Token = GenerateToken(email, entity.UserId)
                };

                return model;
            }
            throw new ArgumentException("User with the specified email does not exist");
        }

        public string ResetPassword(string email, ResetPasswordModel model)
        {
            if (model.new_password == model.confirm_password)
            {
                if (CheckEmail(email))
                {
                    var entity = context.UserTable.SingleOrDefault(user => user.UserEmail == email);
                    entity.UserPassword = bcrypt.HashPassGenerator(model.new_password);
                    context.SaveChanges();
                    return "true";
                }
                throw new ArgumentException("User with the specified email does not exist");
            }
            throw new ArgumentException("Password does not match");
        }

        public bool CheckEmail(string email)
        {
            var user = context.UserTable.SingleOrDefault(user => user.UserEmail == email);
            return user != null;
        }
    }
}
