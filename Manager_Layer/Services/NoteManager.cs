﻿using System;
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

        public bool DeleteNote(int NoteId)
		{
			return noteRepository.DeleteNote(NoteId);
		}

    }
}

