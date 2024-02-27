using System;
using System.IdentityModel.Tokens.Jwt;
using Repository_Layer.Interfaces;
using Repository_Layer.Context;
using Common_Layer.RequestModel;
using Repository_Layer.Services;
using Repository_Layer.Entity;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Repository_Layer.Services
{
	public class UserRepository:IUserRepository
	{
		public readonly FundoContext context;
		public readonly IConfiguration config;

        Encryption objEncrypt = new Encryption();

        public UserRepository(FundoContext context, IConfiguration config)
		{
			this.context = context;
			this.config = config;
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

		public string GenerateToken(UserEntity user)
		{
			if (config == null) throw new Exception("Configuration is not inititalized");
			else
			{
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.userId.ToString()),
                new Claim(ClaimTypes.Email, user.userEmail)
            };

                var token = new JwtSecurityToken(
                    config["Jwt:Issuer"],
                    config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(5),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            
		}

    }
}

