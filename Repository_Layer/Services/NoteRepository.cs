using Common_Layer.RequestModel;
using Repository_Layer.Entity;
using Repository_Layer.Context;
using Repository_Layer.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

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
        public bool DeleteNote(int UserId,int NoteId)
        {
            try
            {
                var note = context.NotesTable.FirstOrDefault(x => x.NoteId == NoteId);
                if (note != null)
                {
                    if (note.userId == UserId)
                    {
                        context.NotesTable.Remove(note);
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("You don't have access");
                    }
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

        public bool UpdateNote(int UserId,int NoteId,UpdateNotesModel model)
        {
            try
            {
                var note = context.NotesTable.FirstOrDefault(x => x.NoteId == NoteId);
                if (note != null)
                {
                    if (note.userId==UserId)
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
                        throw new Exception($"You don't have the access");
                    }
                }
                else
                {
                    throw new Exception("Note doesn't exist");
                }
            }
            catch
            {
                throw;
            }
        }

        public bool UpdatePin(int UserId,int NoteId)
        {
            try
            {
                var note = context.NotesTable.FirstOrDefault(x => x.NoteId == NoteId);
                if (note != null)
                {
                    if (note.userId == UserId)
                    {
                        if (note.IsPin)
                        {
                            note.IsPin = false;
                        }
                        else
                        {
                            note.IsPin = true;
                        }
                        note.UpdatedAt = DateTime.UtcNow;
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("You don't have access");
                    }
                }
                else
                {
                    throw new Exception($"Note with note id {NoteId} doesn't exist");
                }
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateTrash(int UserId,int NoteId)
        {
            try
            {
                var note = context.NotesTable.FirstOrDefault(x => x.NoteId == NoteId);
                if (note != null)
                {
                    if (note.userId == UserId)
                    {
                        if (note.IsTrash)
                        {
                            note.IsTrash = false;
                        }
                        else
                        {
                            note.IsTrash = true;
                        }
                        note.UpdatedAt = DateTime.UtcNow;
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("You don't have access");
                    }
                }
                else
                {
                    throw new Exception($"Note with note id {NoteId} doesn't exist");
                }
            }
            catch
            {
                throw;
            }
        }


        public bool UpdateArchive(int UserId,int NoteId)
        {
            try
            {
                var note = context.NotesTable.FirstOrDefault(x => x.NoteId == NoteId);
                if (note != null)
                {
                    if (note.userId == UserId)
                    {
                        if (note.IsArchive)
                        {
                            note.IsArchive = false;
                        }
                        else
                        {
                            note.IsArchive = true;
                        }
                        note.UpdatedAt = DateTime.UtcNow;
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("You don't have access");
                    }
                }
                else
                {
                    throw new Exception($"Note with note id {NoteId} doesn't exist");
                }
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateColor(int UserId,int NoteId,UpdateNoteModel model)
        {
            try
            {
                var note = context.NotesTable.FirstOrDefault(x => x.NoteId == NoteId);
                if (note != null)
                {
                    if (note.userId == UserId)
                    {
                        note.colour = model.colour;
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("You don't have access");
                    }
                }
                else
                {
                    throw new Exception($"Note with note id {NoteId} doesn't exist");
                }
            }
            catch
            {
                throw;
            }
        }

        public bool UploadImage(string filePath,int NoteId,int UserId)
        {
            try
            {
                var note = context.NotesTable.FirstOrDefault(x => x.NoteId == NoteId);
                if (note != null)
                {
                    if (note.userId == UserId)
                    {
                        Account account = new Account("diu0dzuph", "151566961183183", "kPNIAx62USDiH2zqIdQBmEt54t0");
                        Cloudinary cloudinary = new Cloudinary(account);
                        ImageUploadParams uploadParams = new ImageUploadParams
                        {
                            File = new FileDescription(filePath),
                            PublicId = note.NoteTitle
                        };
                        ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

                        note.UpdatedAt = DateTime.UtcNow;
                        note.background = uploadResult.Url.ToString();
                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        throw new Exception("You don't have access");
                    }
                }
                else
                {
                    throw new Exception($"Note with note id {NoteId} doesn't exist");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}

