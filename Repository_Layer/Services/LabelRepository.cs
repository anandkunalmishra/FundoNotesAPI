using System;
using Common_Layer.RequestModel;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Repository_Layer.Services
{
	public class LabelRepository:ILabelRepository
	{
        public readonly FundoContext context;
        public LabelRepository(FundoContext context)
        {
            this.context = context;
        }

        public LabelEntity AddLabel(int UserId,int NoteId,AddLabelModel model)
        {
            LabelEntity entity = new LabelEntity();
            if (context.UserTable.FirstOrDefault(x => x.userId == UserId) != null)
            {
                if (context.NotesTable.FirstOrDefault(x => x.NoteId == NoteId) != null)
                {
                    entity.UserId = UserId;
                    entity.LabelName = model.LabelName;
                    entity.NoteId = NoteId;

                    context.LabelTable.Add(entity);
                    context.SaveChanges();
                    return entity;
                }
                else
                {
                    throw new Exception("Note Id does not exist");
                }
            }
            else
            {
                throw new Exception("User doesn't Exist");
            }
        }

        public LabelEntity UpdateLabel(int UserId, int NoteId, int LabelId, AddLabelModel model)
        {
            var Label = context.LabelTable.FirstOrDefault(x => x.LabelId == LabelId);
            if (Label != null)
            {
                if (context.UserTable.FirstOrDefault(x => x.userId == UserId) != null)
                {
                    if (context.NotesTable.FirstOrDefault(x => x.NoteId == NoteId) != null)
                    {
                        if (context.LabelTable.FirstOrDefault(x => x.LabelId == LabelId) != null)
                        {
                            Label.LabelName = model.LabelName;

                            context.SaveChanges();
                            return Label;
                        }
                        throw new Exception("Label Id does not exist");
                    }
                    throw new Exception("Note Id does not exist");
                }
                throw new Exception("User doesn't Exist");
            }
            throw new Exception("label doesn't exist");
        }

        //public LabelEntity GetAllLabel(int UserId)
        //{
        //    if (context.UserTable.FirstOrDefault(x => x.userId == UserId) != null)
        //    {
        //        var LabelsByUser = context.LabelTable.Where(x=>x.UserId==UserId).GroupBy(l=>l)
        //    }
        //}
    }
}

