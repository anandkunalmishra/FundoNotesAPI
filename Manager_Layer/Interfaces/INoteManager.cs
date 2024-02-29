using System;
using Common_Layer.RequestModel;
using Repository_Layer.Entity;

namespace Manager_Layer.Interfaces
{
	public interface INoteManager
	{
        public NoteEntity NoteCreation(int userId,AddNotesModel addNotes);
        public bool DeleteNote(int NoteId);
        public List<NoteEntity> GetAllNotes(int UserId);
        public bool UpdateNote(int NoteId, UpdateNotesModel model);

    }
}

