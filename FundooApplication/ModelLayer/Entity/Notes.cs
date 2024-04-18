using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entity
{
    public class Notes
    {
        public int NoteId { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string BgColor { get; set; }

        public string ImagePath { get; set;}

        public DateTime? Remainder { get; set; }

        public bool IsArchive { get; set;}

        public bool Ispinned { get; set;}

        public bool IsTrash { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set;}

        //public List<int> CollabId { get; set; }

        public string? LabelName { get; set;}

        public int userid { get; set;}

        public override string ToString()
        {
            return $"NoteId: {NoteId}, Title: {Title}, Description: {Description}, " +
                $"BgColor: {BgColor}, ImagePath: {ImagePath}, Remainder: {Remainder}," +
                $" IsArchive: {IsArchive}, IsPinned: {Ispinned}, IsTrash: {IsTrash}, " +
                $"CreatedAt: {CreatedAt}, ModifiedAt: {ModifiedAt}, " +
                $" UserId: {userid}";
        }


    }
}
