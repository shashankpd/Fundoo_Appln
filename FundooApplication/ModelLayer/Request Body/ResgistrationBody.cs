using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Request_Body
{
    public class ResgistrationBody
    {
        public string firstName { set; get; }
        public string lastName { set; get; }

        public string emailId { set; get; }

        public string password { set; get; }
    }
}
