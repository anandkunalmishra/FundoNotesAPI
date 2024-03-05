using System;
using Repository_Layer.Entity;

namespace Manager_Layer.Interfaces
{
	public interface ICollabManager
	{
        public CollabEntity AddCollab(string collabEmail, int userId, int NoteId);

    }
}

