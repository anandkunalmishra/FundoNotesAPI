using System;
using Common_Layer.RequestModel;
using Repository_Layer.Entity;

namespace Repository_Layer.Interfaces
{
	public interface IUserRepository
	{
		public UserEntity UserRegisteration(RegisterModel model);
        public UserEntity UserLogin(LoginModel model);
    }
}

