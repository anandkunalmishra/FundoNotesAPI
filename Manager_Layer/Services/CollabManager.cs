using System;
using Manager_Layer.Interfaces;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Manager_Layer.Services
{
	public class CollabManager:ICollabManager
	{
        public readonly ICollabRepository repository;

        public CollabManager(ICollabRepository repository)
        {
            this.repository = repository;
        }

        public CollabEntity AddCollab(string collabEmail, int userId, int NoteId)
        {
            return repository.AddCollab(collabEmail, userId, NoteId);
        }
    }
}

