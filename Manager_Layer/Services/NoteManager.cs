using System;
using Common_Layer.RequestModel;
using Repository_Layer.Interfaces;
using Repository_Layer.Entity;
using Manager_Layer.Interfaces;

namespace Manager_Layer.Services
{
	public class NoteManager:INoteManager
	{
		public readonly INoteRepository noteRepository;

		public NoteManager(INoteRepository noteRepository)
		{
			this.noteRepository = noteRepository;
		}

        public NoteEntity NoteCreation(int userId,AddNotesModel addNotes)
		{
			return noteRepository.NoteCreation(userId,addNotes);
		}

        public bool DeleteNote(int UserId, int NoteId)
        {
			return noteRepository.DeleteNote(UserId,NoteId);
		}

        public List<NoteEntity> GetAllNotes(int UserId)
		{
			return noteRepository.GetAllNotes(UserId);
		}

        public bool UpdateNote(int UserId,int NoteId, UpdateNotesModel model)
		{
			return noteRepository.UpdateNote(UserId,NoteId, model);
		}

        public bool UpdatePin(int UserId,int NoteId)
		{
			return noteRepository.UpdatePin(UserId,NoteId);
		}

        public bool UpdateTrash(int UserId,int NoteId)
		{
			return noteRepository.UpdateTrash(UserId,NoteId);
		}

        public bool UpdateArchive(int UserId,int NoteId)
		{
			return noteRepository.UpdateArchive(UserId,NoteId);
		}

        public bool UpdateColor(int UserId, int NoteId, UpdateNoteModel model)
		{
			return noteRepository.UpdateColor(UserId, NoteId, model);
		}

        public bool UploadImage(string filePath, int NoteId, int UserId)
		{
			return noteRepository.UploadImage(filePath, NoteId, UserId);
		}
    }
}

