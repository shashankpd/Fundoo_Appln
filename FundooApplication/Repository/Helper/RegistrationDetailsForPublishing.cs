using ModelLayer.Entity;
using ModelLayer.Request_Body;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Helper
{
    public class RegistrationDetailsForPublishing
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
       

        // Add any other fields you want to include for publishing

        public RegistrationDetailsForPublishing(ResgistrationBody userRegModel)
        {
            FirstName = userRegModel.firstName;
            LastName = userRegModel.lastName;
            Email = userRegModel.emailId;

        }

       
    }
}
