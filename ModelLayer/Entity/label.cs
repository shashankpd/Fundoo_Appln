using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entity
{
    public class label
    {
        public int LabelId { get; set; }
        public string LabelName { get; set; }

        public int userid { get; set; } //foreign key

        public int NoteId { get; set; } //foriegn key
    }
}
