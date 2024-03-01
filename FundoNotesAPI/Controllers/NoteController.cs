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
					var response = noteManager.DeleteNote(userId,NoteId);
					return Ok(new ResModel<bool> { Success = true, Message = "Delete Successful", Data = true });
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

        [Authorize]
        [HttpGet]
        [Route("getNotes")]
        public ActionResult GetAllNotes()
		{
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                if (userId != null)
                {
                    var response = noteManager.GetAllNotes(userId);
                    return Ok(new ResModel<List<NoteEntity>> { Success = true, Message = "Display Successful", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<List<NoteEntity>> { Success = true, Message = "Display Unsuccessful", Data = null });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<List<NoteEntity>> { Success = true, Message = ex.Message, Data = null });
            }
        }


        [Authorize]
        [HttpPatch]
        [Route("UpdateNote")]
        public ActionResult UpdateNote(int NoteId,UpdateNotesModel model)
		{
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                if (userId != null)
                {
                    var response = noteManager.UpdateNote(userId,NoteId,model);
                    if (response)
                    {
                        return Ok(new ResModel<bool> { Success = true, Message = "Update Successful", Data = response });
                    }
                    else
                    {
                        return  BadRequest(new ResModel<bool> { Success = true, Message = "Update Unsuccessful", Data = response });
                    }
                }
                else
                {
                    return BadRequest(new ResModel<NoteEntity> { Success = true, Message = "User Doesn't exist", Data = null });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<NoteEntity> { Success = true, Message = ex.Message, Data = null });
            }
        }



        [Authorize]
        [HttpPut]
        [Route("updatePin")]
        public ActionResult UpdatePin(int NoteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                if (userId != null)
                {
                    var response = noteManager.UpdatePin(userId,NoteId);
                    if (response)
                    {
                        return Ok(new ResModel<bool> { Success = true, Message = "Update Successful", Data = response });
                    }
                    else
                    {
                        return BadRequest(new ResModel<bool> { Success = true, Message = "Update Unsuccessful", Data = response });
                    }
                }
                else
                {
                    return BadRequest(new ResModel<NoteEntity> { Success = true, Message = "User Doesn't exist", Data = null });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<NoteEntity> { Success = true, Message = ex.Message, Data = null });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("updateTrash")]
        public ActionResult UpdateTrash(int NoteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                if (userId != null)
                {
                    var response = noteManager.UpdateTrash(userId,NoteId);
                    if (response)
                    {
                        return Ok(new ResModel<bool> { Success = true, Message = "Update Successful", Data = response });
                    }
                    else
                    {
                        return BadRequest(new ResModel<bool> { Success = true, Message = "Update Unsuccessful", Data = response });
                    }
                }
                else
                {
                    return BadRequest(new ResModel<NoteEntity> { Success = true, Message = "User Doesn't exist", Data = null });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<NoteEntity> { Success = true, Message = ex.Message, Data = null });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("updateArchive")]
        public ActionResult UpdateArchive(int NoteId)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                if (userId != null)
                {
                    var response = noteManager.UpdateArchive(userId,NoteId);
                    if (response)
                    {
                        return Ok(new ResModel<bool> { Success = true, Message = "Update Successful", Data = response });
                    }
                    else
                    {
                        return BadRequest(new ResModel<bool> { Success = true, Message = "Update Unsuccessful", Data = response });
                    }
                }
                else
                {
                    return BadRequest(new ResModel<NoteEntity> { Success = true, Message = "User Doesn't exist", Data = null });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<NoteEntity> { Success = true, Message = ex.Message, Data = null });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("updateColour")]
        public ActionResult UpdateColour(int UserId,int NoteId,UpdateNoteModel model)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                if (userId != null)
                {
                    var response = noteManager.UpdateArchive(userId, NoteId);
                    if (response)
                    {
                        return Ok(new ResModel<bool> { Success = true, Message = "Update Successful", Data = response });
                    }
                    else
                    {
                        return BadRequest(new ResModel<bool> { Success = true, Message = "Update Unsuccessful", Data = response });
                    }
                }
                else
                {
                    return BadRequest(new ResModel<NoteEntity> { Success = true, Message = "User Doesn't exist", Data = null });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<NoteEntity> { Success = true, Message = ex.Message, Data = null });
            }
        }

    }
}

