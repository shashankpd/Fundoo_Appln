using Microsoft.Extensions.Configuration;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface Iregistration
    {
        public Task<IEnumerable<Registration>> Getregdetails();
        public Task<int> Addusers(Registration users);

        public Task<int> Deleteusers(string email);

        public Task<int> updateuser(string email, Registration reg);

        public Task<string> userLogin(string email, string password, IConfiguration configuration);


        public Task ForgotPassword(string email);

        public Task<Registration> GetUserByEmail(string email);

        public Task ResetPassword(string email, string otp, string newPassword);
       


        /*  public Task ResetPassword(string email, string token, string newPassword);*/





    }
}
