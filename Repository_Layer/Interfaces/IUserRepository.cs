using System;
using Common_Layer.RequestModel;
using Repository_Layer.Entity;

namespace Repository_Layer.Interfaces
{
	public interface IUserRepository
	{
		public UserEntity UserRegisteration(RegisterModel model); 
		public string UserLogin(LoginModel model);
		public ForgetPasswordModel ForgetPassword(string Email);
		public bool ResetPassword(string Email, ResetPasswordModel reset);
        public bool checker(string Email);
    }
}

