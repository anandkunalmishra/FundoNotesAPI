﻿using System;
using Common_Layer.RequestModel;
using Repository_Layer.Context;
using Repository_Layer.Entity;

namespace Repository_Layer.Interfaces
{

	public interface INoteRepository
	{
        public NoteEntity NoteCreation(int userId, AddNotesModel addNotes);
        public bool DeleteNote(int NoteId);
        public List<NoteEntity> GetAllNotes(int UserId);
        public bool UpdateNote(int NoteId, UpdateNotesModel model);
    }
}

