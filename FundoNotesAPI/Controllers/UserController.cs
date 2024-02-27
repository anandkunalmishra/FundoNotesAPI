using Manager_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Common_Layer.RequestModel;
using Common_Layer.ResponseModel;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
using Azure;

namespace FundoNotesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
	public class UserController:ControllerBase
	{
		private readonly IUserManager userManager;

		public UserController(IUserManager userManager)
		{
			this.userManager = userManager;
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
					return Ok(new ResModel<String> { Success = true, Message = "Login Successful", Data =  userManager.GenerateToken(response)});
				}
				else
				{
					return BadRequest(new ResModel<UserEntity> { Success = false, Message = "Login Unsuccessful", Data = response });
				}
            }
			catch(Exception ex)
			{
				return BadRequest(new ResModel<UserEntity> { Success = false, Message = ex.Message, Data = null });
			}
		}

    }
}

