using Microsoft.Extensions.Configuration;
using ModelLayer.Entity;
using ModelLayer.Request_Body;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IRegistrationRepository
    {
        public Task<IEnumerable<Registration>> Getregdetails();
        public Task<int> Addusers(ResgistrationBody users);

        public Task<int> Deleteusers(int userid);

        public Task<int> updateuser(int userid, Registration reg);

        public Task<string> userLogin(string email, string password, IConfiguration configuration);


        //public Task ForgotPassword(string email);

        public Task<Registration> GetUserByEmail(string email);

        public Task ResetPassword(string email, string otp, string newPassword);
       


        /*  public Task ResetPassword(string email, string token, string newPassword);*/





    }
}
