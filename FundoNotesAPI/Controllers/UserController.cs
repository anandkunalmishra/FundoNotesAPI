using Manager_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Common_Layer.RequestModel;
using Common_Layer.ResponseModel;
using Repository_Layer.Entity;
using Common_Layer.Utility;
using MassTransit;

namespace FundoNotesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
	public class UserController:ControllerBase
	{
		private readonly IUserManager userManager;
		private readonly IBus bus;
		private readonly ILogger<UserController> logger;

		public UserController(IUserManager userManager,IBus bus, ILogger<UserController> logger)
		{
			this.userManager = userManager;
			this.bus = bus;
			this.logger = logger;
		}

		[HttpPost]
		[Route("reg")]
		public ActionResult Register(RegisterModel model)
		{
			try
			{
                var response = userManager.UserRegisteration(model);

                if (response != null)
                {
					logger.LogInformation("Register Successful");
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
		[Route("login")]
		public ActionResult Login(LoginModel model)
		{
			try
			{
                var response = userManager.UserLogin(model);


				if (response != null)
				{
					return Ok(new ResModel<string> { Success = true, Message = "Login Successful", Data = response });
				}
				else
				{
					return BadRequest(new ResModel<string> { Success = false, Message = "Login Unsuccessful", Data = null });
				}
            }
			catch(Exception ex)
			{
				return BadRequest(new ResModel<string> { Success = false, Message = ex.Message, Data = null });
			}
		}

		[HttpPost]
		[Route("forgetPass")]
		public async Task<ActionResult> ForgetPassword(string Email)
		{
			try
			{
                if (userManager.checker(Email))
                {
                    Send send = new Send();
                    ForgetPasswordModel model = userManager.ForgetPassword(Email);
                    string str = send.SendMail(model.userEmail, model.token);
                    Uri uri = new Uri("rabbitmq://localhost/FundooNotesEmailQueue");
                    var endpoint = await bus.GetSendEndpoint(uri);
                    return Ok(new ResModel<string> { Success = true, Message = "Forget password successful", Data = model.token });
                }
                else
                {
                    throw new Exception("Failed to send email");
                }
            }
			catch(Exception er)
			{
				return BadRequest(new ResModel<string> { Success = true, Message = er.Message, Data = null });
			}
			
		}

		[Authorize]
		[HttpPost]
		[Route("resetPass")]
		public ActionResult ResetPassword(ResetPasswordModel reset) 
		{
			try
			{
				string Email = User.FindFirst("Email").Value;
                if (userManager.ResetPassword(Email,reset))
                {
					return Ok(new ResModel<bool> { Success = true,Message="Password Reset successful",Data=true});
                }
                else
                {
					return BadRequest(new ResModel<bool> { Success = false, Message = "Failed to reset", Data = false }); ;
                }
            }
			catch(Exception ex)
			{
				return BadRequest(new ResModel<bool> {Success=false,Message=ex.Message,Data=false });
			}
		}

	}
}

