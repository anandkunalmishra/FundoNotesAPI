using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common_Layer.ResponseModel;
using Manager_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;

namespace FundoNotesAPI.Controllers
{
    [Route("api/[controller]")]
    public class CollabController : ControllerBase
    {
        public readonly ICollabManager collabManager;

        public CollabController(ICollabManager collabManager)
        {
            this.collabManager = collabManager;
        }

        [Authorize]
        [HttpPost]
        [Route("AddCollab")]
        public ActionResult AddCollaborator(int NoteId,string CollabEmail)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                if (userId != null)
                {
                    var response = collabManager.AddCollab(CollabEmail, userId, NoteId);
                    if (response == null)
                    {
                        return BadRequest(new ResModel<CollabEntity> { Success = false, Message = "not able to add", Data = null });
                    }
                    return Ok(new ResModel<CollabEntity> { Success = true, Message = "collaborator added successfully", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<CollabEntity> { Success = false, Message = "User Not Authorized", Data = null });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new ResModel<CollabEntity> { Success = false, Message = ex.Message, Data = null });
            }
            
        }

    }
}

