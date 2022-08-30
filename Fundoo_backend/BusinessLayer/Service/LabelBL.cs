using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
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

        public NoteLabel AddLabel(long notesId, string labelname)
        {

            try
            {
                return labelRL.AddLabel(notesId, labelname);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
