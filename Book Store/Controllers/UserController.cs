using CommonLayer;
using CommonLayer.RequestModels;
using CommonLayer.ResponseModel;
using ManagerLayer.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System;
using System.Threading.Tasks;

namespace Book_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:ControllerBase
    {
        private readonly IUserManager iuserManager;
        private readonly IBus ibus;
        //logger

        public UserController(IUserManager iuserManager, IBus ibus)
        {
            this.iuserManager = iuserManager;
            this.ibus = ibus;
        }

        [HttpPost]
        [Route("Reg")]

        public ActionResult Register(RegisterModel registerModel)
        {
            try
            {
                var response= iuserManager.UserRegistration(registerModel);
                if(response != null)
                {
                    return Ok(new ResModel<UserEntity> { Success = true, Message = "Registered Successfully", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<UserEntity> { Success = false, Message = "Registration Failed", Data = response });
                }

            }
            catch(Exception ex)
            {
                return BadRequest(new ResModel<UserEntity> { Success = false, Message = ex.Message, Data = null });
            }
            
        }

        [HttpPost]
        [Route("Log")]
        public ActionResult Login(LoginModel loginModel)
        {
            try
            {
                string response = iuserManager.UserLogin(loginModel);
                if (response != null)
                {
                    return Ok(new ResModel<String> { Success = true, Message = "Login Successful :)", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<String> { Success = false, Message = "Login failed", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<String> { Success = false, Message = ex.Message, Data = null });
            }
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword(string Email)
        {
            try
            {
                if (iuserManager.CheckEmail(Email))
                {
                    SendMail mail = new SendMail();
                    ForgotPasswordModel model = iuserManager.ForgotPassword(Email);
                    string str = mail.SendMailtoUser(model.UserEmail, model.Token);
                    Uri uri = new Uri("rabbitmq://localhost/FunfooNotesEmailQueue");
                    var endPoint = await ibus.GetSendEndpoint(uri);
                    await endPoint.Send(model);
                    return Ok(new ResModel<bool> { Success = true, Message = str, Data = true });
                }
                else
                {
                    throw new Exception("Failed to send email");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
            }
        }


        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]
        public ActionResult Reset(ResetPasswordModel model)
        {
            try
            {
                string email = User.FindFirst("UserEmail").Value;
                return Ok(new ResModel<string>
                {
                    Success = true,
                    Message = "Password Reset Successful",
                    Data = iuserManager.ResetPassword(email, model)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<string>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = "Password reset unsuccessful"
                });
            }
        }
    }
}
