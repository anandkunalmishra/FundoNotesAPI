using System;
using Common_Layer.RequestModel;
using Manager_Layer.Interfaces;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;
namespace Manager_Layer.Services
{
	public class UserManager:IUserManager
	{
		public readonly IUserRepository repository;
		public UserManager(IUserRepository repository)
		{
			this.repository = repository;
		}

        public UserEntity UserRegisteration(RegisterModel model)
		{
			try
			{
				return repository.UserRegisteration(model);
			}
			catch
			{
				throw new Exception("Could not Register");
			}
		}
        public UserEntity UserLogin(LoginModel model)
        {
			try
			{
				return repository.UserLogin(model);
			}
			catch
			{
				throw new Exception("Could not Login");
			}
		}
    }
}

