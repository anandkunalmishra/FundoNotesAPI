using Manager_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Common_Layer.RequestModel;
using Common_Layer.ResponseModel;
using Repository_Layer.Entity;

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
			var response = userManager.UserRegisteration(model);

			if (response != null)
			{
				return Ok(new ResModel<UserEntity> { Success = true, Message = "Registered Successfully", Data = response });
			}
			else
			{
				return BadRequest(new ResModel<UserEntity> { Success = false,Message="Registration Failed",Data = response});
			}
		}

    }
}

