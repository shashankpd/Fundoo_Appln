using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Request_Body
{
    public class NotesBody
    {
        public int NoteId { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string BgColor { get; set; }

        public string ImagePath { get; set; }

        public DateTime? Remainder { get; set; }

        public bool IsArchive { get; set; }

        public bool Ispinned { get; set; }

        public bool IsTrash { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }

        public string LabelName { get; set; }

    }
}
