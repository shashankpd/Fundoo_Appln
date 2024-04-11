using Business.Interface;
using Microsoft.Extensions.Configuration;
using ModelLayer.Entity;
using Repository.Interface;
using Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service
{
    public class RegistrationServiceBusinessLogic : IRegistrationBusiness
    {
        private static string otp;
        private static string email;
        private static Registration entity;

        private readonly IRegistrationRepository Registration;

        public RegistrationServiceBusinessLogic(IRegistrationRepository Registration)
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

        public async Task ForgotPassword(string email)
        {

            var user = await GetUserByEmail(email);
            if (user != null)
            {
                var Otp = GenerateOneTimePassword();
                // Store the token along with the user's email in your database or cache
                // Send an email to the user with a link containing the token for password reset
                RegistrationServiceRepoLogic.Otp = Otp;
                RegistrationServiceRepoLogic.Email = email;
                SendPasswordResetEmail(email, Otp);
            }
            else
            {
                // Handle case where email does not exist in the database
                throw new ArgumentException("User with provided email does not exist.");
            }
        }

        public Task<Registration> GetUserByEmail(string email)
        {
            return Registration.GetUserByEmail(email);
        }


        //----------------
        private string GenerateOneTimePassword()
        {
            // Generate a random six-digit OTP
            Random random = new Random();
            int otp = random.Next(100000, 999999);

            return otp.ToString();

        }

        //-----------------------------------

        public async Task SendPasswordResetEmail(string email, string Otp)
        {
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            try
            {
                mailMessage.From = new System.Net.Mail.MailAddress("pdshashank8@outlook.com", "FUNDOO NOTES");
                mailMessage.To.Add(email);
                mailMessage.Subject = "Change password for Fundoo Notes";
                mailMessage.Body = "This is your otp please enter to change password " + Otp;
                mailMessage.IsBodyHtml = true;
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient("smtp-mail.outlook.com");

                // Specifies how email messages are delivered. Here Email is sent through the network to an SMTP server.
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                // Set the port for Outlook's SMTP server
                smtpClient.Port = 587; // Outlook SMTP port for TLS/STARTTLS

                // Enable SSL/TLS
                smtpClient.EnableSsl = true;

                string loginName = "pdshashank8@outlook.com";
                string loginPassword = "PDshashank@123";

                System.Net.NetworkCredential networkCredential = new System.Net.NetworkCredential(loginName, loginPassword);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;

                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: " + ex.Message);
            }
            finally
            {
                mailMessage.Dispose();
            }
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
