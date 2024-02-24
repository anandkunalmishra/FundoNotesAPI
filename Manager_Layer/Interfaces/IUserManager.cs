using System;
using Repository_Layer.Entity;
using Common_Layer.RequestModel;

namespace Manager_Layer.Interfaces
{
	public interface IUserManager
	{
		public UserEntity UserRegisteration(RegisterModel model);
	}
}

