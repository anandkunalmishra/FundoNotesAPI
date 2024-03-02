using System;
using Common_Layer.RequestModel;
using Manager_Layer.Interfaces;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Manager_Layer.Services
{
	public class LabelManager:ILabelManager
	{
        public readonly ILabelRepository repository;

        public LabelManager(ILabelRepository repository)
        {
            this.repository = repository;
        }

        public LabelEntity AddLabel(int UserId, int NoteId, AddLabelModel model)
        {
            return repository.AddLabel(UserId, NoteId, model);
        }

        public LabelEntity UpdateLabel(int UserId, int NoteId, int LabelId, AddLabelModel model)
        {
            return repository.UpdateLabel(UserId,NoteId,LabelId, model);
        }

    }
}

