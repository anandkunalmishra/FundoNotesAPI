using System;
using Repository_Layer.Interfaces;
using Repository_Layer.Context;
using Common_Layer.RequestModel;
using Repository_Layer.Entity;

namespace Repository_Layer.Services
{
	public class UserRepository:IUserRepository
	{
		public readonly FundoContext context;

		public UserRepository(FundoContext context)
		{
			this.context = context;
		}

        public UserEntity UserRegisteration(RegisterModel model)
		{
			//User info send is being stored to entity for register
			UserEntity entity = new UserEntity();
			entity.fName = model.fName;
			entity.lName = model.lName;
			entity.userEmail = model.userEmail;
			entity.userPassword = model.userPassword;
			return entity;
		}

    }
}

