using System;
using Repository_Layer.Interfaces;
using Repository_Layer.Context;
using Common_Layer.RequestModel;
using Repository_Layer.Services;
using Repository_Layer.Entity;

namespace Repository_Layer.Services
{
	public class UserRepository:IUserRepository
	{
		public readonly FundoContext context;
        Encryption objEncrypt = new Encryption();

        public UserRepository(FundoContext context)
		{
			this.context = context;
		}

        public UserEntity UserRegisteration(RegisterModel model)
		{
            if (context.UserTable.Any(x => x.userEmail == model.userEmail))
            {
                throw new Exception("User Already Exists");
            }

            //User info is being stored to entity for registeration
            UserEntity entity = new UserEntity();
			entity.fName = model.fName;
			entity.lName = model.lName;
			entity.userEmail = model.userEmail;


			//Making the Password secure using Hashing or Any HashAlgorithm
			entity.userPassword = objEncrypt.encrypt(model.userPassword);
			
            //entity.userPassword = model.userPassword;

            //add the entity to userTable database
            try
			{
                context.UserTable.Add(entity);
                context.SaveChanges();
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				throw new Exception("Failed to add User");
			}
			
			return entity;
		}
        public UserEntity UserLogin(LoginModel model)
		{
			UserEntity user = context.UserTable.FirstOrDefault(x => x.userEmail == model.userEmail);
			if (user != null)
			{
				if (objEncrypt.matchPassword(model.userPassword, user.userPassword)){
					return user;
				}
				else
				{
					throw new Exception("Wrong Credentials");
				}
			}
			else
			{
				throw new Exception("User doesn't exists");
			}
		}

    }
}

