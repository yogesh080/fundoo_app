using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBL : ILabelBL
    {
        public ILabelRL labelRL;

        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        public NoteLabel AddLabel(long userId,long notesId, string labelname)
        {

            try
            {
                return labelRL.AddLabel(userId,notesId, labelname);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public string DeleteLabel(long labelId)
        {
            try
            {

                return labelRL.DeleteLabel(labelId);

            }
            catch (Exception)
            {

                throw;
            }
        }


        

        public IEnumerable<NoteLabel> ReadLabel(long labelId)
        {
            try
            {
                return labelRL.ReadLabel(labelId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
