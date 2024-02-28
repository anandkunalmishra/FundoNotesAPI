using System;
using Common_Layer.RequestModel;
using Repository_Layer.Entity;

namespace Manager_Layer.Interfaces
{
	public interface INoteManager
	{
        public NoteEntity NoteCreation(int userId,AddNotesModel addNotes);

    }
}

