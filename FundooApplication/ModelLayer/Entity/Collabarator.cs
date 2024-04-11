using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entity
{
    public class Collabarator
    {
        public int collabid { get; set; }
        public int NoteId { get; set; }   //foriegn key
        public int userid { get; set; }   //foriegn key
        public string collabEmail { get; set; }    
    }
}
