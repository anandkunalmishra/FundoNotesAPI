using System;
using Common_Layer.RequestModel;
using Repository_Layer.Entity;

namespace Manager_Layer.Interfaces
{
	public interface ILabelManager
	{
        public LabelEntity AddLabel(int UserId, int NoteId, AddLabelModel model);
        public LabelEntity UpdateLabel(int UserId, int NoteId, int LabelId, AddLabelModel model);

    }
}

