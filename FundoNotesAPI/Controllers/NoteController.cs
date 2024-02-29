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
	[Route("note/[controller]")]
	public class NoteController:ControllerBase
	{
		public readonly INoteManager noteManager;

		public NoteController(INoteManager noteManager)
		{
			this.noteManager = noteManager;
		}

		[Authorize]
		[HttpPost]
		[Route("addNote")]
		public ActionResult AddNote(AddNotesModel addNotes)
		{
			try
			{
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);

                var response = noteManager.NoteCreation(userId,addNotes);

				if (response != null)
				{
					return Ok(new ResModel<NoteEntity> { Success = true, Message = "Succesfully added the note", Data = response });
				}
				else
				{
					return BadRequest(new ResModel<NoteEntity> { Success = false, Message = "Note Addition unsuccessful", Data = null });
				}
			}
			catch(Exception ex)
			{
                return BadRequest(new ResModel<NoteEntity> { Success = false, Message = ex.Message, Data = null });
            }
		}
		
        [Authorize]
        [HttpDelete]
        [Route("deleteNote")]
        public ActionResult DeleteNote(int NoteId)
		{
			try
			{   int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
				if (userId!=null)
				{
					var response = noteManager.DeleteNote(NoteId);
					return BadRequest(new ResModel<bool> { Success = true, Message = "Delete Successful", Data = true });
				}
				else
				{
					return BadRequest(new ResModel<bool> { Success = true, Message = "Delete Unsuccessful", Data = false });
				}
            }
			catch(Exception ex)
			{
                return BadRequest(new ResModel<bool> { Success = true, Message = ex.Message, Data = false });
            }
		}



    }
}

