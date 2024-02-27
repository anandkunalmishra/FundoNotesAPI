using System;
using Repository_Layer.Entity;
using Common_Layer.RequestModel;

namespace Manager_Layer.Interfaces
{
	public interface IUserManager
	{
		public UserEntity UserRegisteration(RegisterModel model);
		public string UserLogin(LoginModel model);
		public ForgetPasswordModel ForgetPassword(string Email);
        public bool ResetPassword(string Email, ResetPasswordModel reset);
        public bool checker(string Email);
    }
}

