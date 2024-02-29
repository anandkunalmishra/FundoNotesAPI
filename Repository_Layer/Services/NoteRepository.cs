
using System;
using Common_Layer.RequestModel;
using Repository_Layer.Entity;
using Repository_Layer.Context;
using Repository_Layer.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Drawing;
using System.IO.Compression;

namespace Repository_Layer.Services
{
	public class NoteRepository:INoteRepository
	{
        public readonly FundoContext context;
        public NoteRepository(FundoContext context)
        {
            this.context = context;
        }
        public NoteEntity NoteCreation(int userId,AddNotesModel addNotes)
        {
            var user = context.UserTable.FirstOrDefault(x => x.userId == userId);
            if (user != null)
            {
                NoteEntity note = new NoteEntity
                {
                    userId = user.userId,
                    NoteTitle = addNotes.NoteTitle,
                    NoteDescription = addNotes.NoteDescription,
                    background = "",
                    IsPin = false,
                    IsTrash = false,
                    colour = "#FFFFFF",
                    IsArchive = false,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                };
                try
                {
                    context.NotesTable.Add(note);
                    context.SaveChanges();
                    return note;
                }
                catch
                {
                    throw new Exception("Not able to add to DBNotes");
                }
            }
            else
            {
                throw new Exception("User is not registered");
            }
            
        }
        public bool DeleteNote(int NoteId)
        {
            try
            {
                var note = context.NotesTable.FirstOrDefault(x => x.NoteId == NoteId);
                if (note != null)
                {
                    context.NotesTable.Remove(note);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                throw;
            }
        }

        public List<NoteEntity> GetAllNotes(int UserId)
        {
            try
            {
                var notes = context.NotesTable.Where(x => x.userId == UserId).ToList();
                if (notes != null) return notes;
                else
                {
                    return null;
                }
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateNote(int NoteId,UpdateNotesModel model)
        {
            try
            {
                var note = context.NotesTable.FirstOrDefault(x => x.NoteId == NoteId);
                if (note != null)
                {
                    if (model.NoteDescription != null)
                    {
                        note.NoteDescription = model.NoteDescription;
                    }
                    if (model.NoteTitle != null)
                    {
                        note.NoteTitle = model.NoteTitle;
                    }
                    note.UpdatedAt = DateTime.UtcNow;

                    context.NotesTable.Update(note);
                    context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}

