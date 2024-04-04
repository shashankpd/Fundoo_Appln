using Business.Interface;
using Microsoft.Extensions.Configuration;
using Repository.Entity;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service
{
    public class RegistrationService_bl : Iregistration_bl
    {

        private readonly Iregistration Registration;

        public RegistrationService_bl(Iregistration Registration)
        {
            this.Registration = Registration;
        }

        public Task<IEnumerable<Registration>> Getregdetails()
        {
            return Registration.Getregdetails();
        }

        public Task<int> Addusers(Registration users)
        {
            return Registration.Addusers(users);
        }

        public Task<int> Deleteusers(string email)
        {
            return Registration.Deleteusers(email);
        }

        public Task<int> updateuser(string email, Registration reg)
        {
            return Registration.updateuser(email,reg);
        }

        public Task<string> userLogin(string email, string password, IConfiguration configuration)
        {
            return Registration.userLogin(email,  password,  configuration);
        }

        public Task ForgotPassword(string email)
        {
            return Registration.ForgotPassword(email);
        }

        public Task<Registration> GetUserByEmail(string email)
        {
            return Registration.GetUserByEmail(email);
        }

        public Task ResetPassword(string email, string otp, string newPassword)
        {
            return Registration.ResetPassword(email, otp, newPassword);
        }



        /*  public Task ResetPassword(string email, string token, string newPassword)
          {
              return Registration.ResetPassword(email,  token, newPassword);
          }*/


    }
}
