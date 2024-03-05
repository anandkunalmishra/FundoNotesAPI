using System;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Repository_Layer.Services
{
	public class CollabRepository:ICollabRepository
	{
		public readonly FundoContext context;
		public CollabRepository(FundoContext context)
		{
			this.context = context;
		}

		public CollabEntity AddCollab(string collabEmail,int userId,int NoteId)
		{
			if (context.UserTable.FirstOrDefault(x => x.userId == userId) != null)
			{
                if (context.NotesTable.FirstOrDefault(x => x.NoteId == NoteId) != null)
                {
                    if (context.CollabTable.FirstOrDefault(x => x.CollabEmail == collabEmail && x.NoteId==NoteId) == null)
                    {
                        CollabEntity entity = new CollabEntity();
                        entity.CollabEmail = collabEmail;
						context.CollabTable.Add(entity);
						context.SaveChanges();
                    }
                    throw new Exception("Already a collabarator");
                }
                throw new Exception("Notes doesn't exist");
            }
			throw new Exception("User is not authorized");
		}
	}
}

