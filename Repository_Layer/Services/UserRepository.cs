using System.IdentityModel.Tokens.Jwt;
using Repository_Layer.Interfaces;
using Repository_Layer.Context;
using Common_Layer.RequestModel;
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

		public bool checker(string Email)
		{
			if(context.UserTable.ToList().Find(x=>x.userEmail == Email)!=null)
			{
				return true;
			}
			return false;
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
        public string UserLogin(LoginModel model)
		{
			UserEntity user = context.UserTable.FirstOrDefault(x => x.userEmail == model.userEmail);
			if (user != null)
			{
				if (objEncrypt.matchPassword(model.userPassword, user.userPassword)){
					var token = GenerateToken(user.userEmail,user.userId);
					return token;
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

        private string GenerateToken(string Email, int UserId)
        {
            //Defining a Security Key 
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",Email),
                new Claim("UserId", UserId.ToString())
            };
            var token = new JwtSecurityToken(
                config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Token expiration time
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;

        }
        public ForgetPasswordModel ForgetPassword(string Email)
		{
			UserEntity User = context.UserTable.FirstOrDefault(x => x.userEmail == Email);
			ForgetPasswordModel forgetPassword = new ForgetPasswordModel();
			forgetPassword.userEmail = User.userEmail;
			forgetPassword.userId = User.userId;
			forgetPassword.token = GenerateToken(User.userEmail,User.userId);

			return forgetPassword;
		}

		public bool ResetPassword(string Email,ResetPasswordModel reset)
		{
			UserEntity User = context.UserTable.FirstOrDefault(x => x.userEmail == Email);

			if (User != null)
			{
				User.userPassword = objEncrypt.encrypt(reset.userPassword);
				context.SaveChanges();
				return true;
			}
			return false;
		}
	
    }
}

