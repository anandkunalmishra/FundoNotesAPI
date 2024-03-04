using Common_Layer.RequestModel;
using Repository_Layer.Entity;

namespace Repository_Layer.Interfaces
{
    public interface ILabelRepository
	{
        public LabelEntity AddLabel(int UserId, int NoteId, AddLabelModel model);
        public LabelEntity UpdateLabel(int UserId,int NoteId, int LabelId,AddLabelModel model);
        public List<string> GetAllLabel(int UserId);
    }
}

