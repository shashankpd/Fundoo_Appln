using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Request_Body
{
    public class CollaboratorBody
    {
       
        public int NoteId { get; set; }   //foriegn key
        public string collabEmail { get; set; }
    }
}
