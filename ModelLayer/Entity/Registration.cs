using CommonLayer.Model.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Entity
{
    public class Registration
    {
        [Required]
        [UserRequestValidation]
        public string userid { get; set; }
        public string firstName { set; get; }
        public string lastName { set;get; }

        public string emailId { set;get; }

        public string password { set;get; }

       
    }
}
