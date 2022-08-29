using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class CollabResponseModel
    {

        public long CollaboratorID { get; set; }

        public string CollaboratedEmail { get; set; }

        public long NotesId { get; set; }

        public long UserId { get; set; }

    }
}
