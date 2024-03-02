using System;
using Common_Layer.RequestModel;
using Common_Layer.ResponseModel;
using Manager_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;

namespace FundoNotesAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class LabelController:ControllerBase
	{
		public readonly ILabelManager labelManager;

		public LabelController(ILabelManager labelManager)
		{
			this.labelManager = labelManager;
		}

		[HttpPost]
		[Route("addLabel")]
		public ActionResult AddLabel(int NoteId,AddLabelModel model)
		{
			try
			{
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
				var response = labelManager.AddLabel(userId, NoteId, model);
				if (response != null)
				{
					return Ok(new ResModel<LabelEntity> { Success = true, Message = "Label addition successful", Data = response });
				}
				return BadRequest(new ResModel<LabelEntity> { Success = true, Message = "Label addition unsucessful", Data = null });

            }
			catch(Exception ex)
			{
				return BadRequest(new ResModel<LabelEntity> { Success = false, Message = ex.Message, Data = null });
			}
		}

		[HttpPut]
		[Route("updateLabel")]
     
        public ActionResult UpdateLabel(int NoteId, int LabelId,AddLabelModel model)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var response = labelManager.UpdateLabel(userId, NoteId, LabelId,model);
                if (response != null)
                {
                    return Ok(new ResModel<LabelEntity> { Success = true, Message = "Label addition successful", Data = response });
                }
                return BadRequest(new ResModel<LabelEntity> { Success = true, Message = "Label addition unsucessful", Data = null });

            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<LabelEntity> { Success = false, Message = ex.Message, Data = null });
            }
        }

		[HttpGet]
		[Route("getAllLabels")]
		public ActionResult GetAllLabel()
		{

		}
	}
}

