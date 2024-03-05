using System;
using Repository_Layer.Entity;

namespace Repository_Layer.Interfaces
{
	public interface ICollabRepository
	{
        public CollabEntity AddCollab(string collabEmail, int userId, int NoteId);

    }
}

