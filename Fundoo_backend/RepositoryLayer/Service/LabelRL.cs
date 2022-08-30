using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        private readonly FundooContext fundooContext;

        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public NoteLabel AddLabel(long userId,long notesId, string labelname)
        {
            try
            {
                var noteResult = fundooContext.NotesTable.Where(x => x.NotesId == notesId && x.UserId == userId).FirstOrDefault();
                if (noteResult != null)
                {
                    NoteLabel LabelEntity = new NoteLabel();
                    LabelEntity.NotesId = noteResult.NotesId;
                    LabelEntity.UserId = noteResult.UserId;
                    LabelEntity.LabelName = labelname;
                    fundooContext.Add(LabelEntity);
                    fundooContext.SaveChanges();
                    return LabelEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
