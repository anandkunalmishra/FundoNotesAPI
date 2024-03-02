using Common_Layer.RequestModel;
using Repository_Layer.Entity;

namespace Repository_Layer.Interfaces
{

    public interface INoteRepository
	{
        public NoteEntity NoteCreation(int userId, AddNotesModel addNotes);
        public bool DeleteNote(int UserId, int NoteId);
        public List<NoteEntity> GetAllNotes(int UserId);
        public bool UpdateNote(int UserId,int NoteId, UpdateNotesModel model);
        public bool UpdatePin(int UserId,int NoteId);
        public bool UpdateTrash(int UserId,int NoteId);
        public bool UpdateArchive(int UserId,int NoteId);
        public bool UpdateColor(int UserId, int NoteId, UpdateNoteModel model);
        public bool UploadImage(string filePath, int NoteId, int UserId);
    }
}

