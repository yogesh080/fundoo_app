using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class LabelResponseModel
    {
        public long LabelId { get; set; }

        public string LabelName { get; set; }

        public long NotesId { get; set; }

        public long UserId { get; set; }
    }
}
