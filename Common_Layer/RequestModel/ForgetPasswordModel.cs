using System;
namespace Common_Layer.RequestModel
{
	public class ForgetPasswordModel
	{
		public int userId { get; set; }
		public string userEmail { get; set; }
		public string token { get; set; }
	}
}

