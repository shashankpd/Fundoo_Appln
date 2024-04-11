using Dapper;
using Microsoft.IdentityModel.Tokens;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using ModelLayer.Entity;
using NLog;

namespace Repository.Service
{
    public class RegistrationServiceRepoLogic : IRegistrationRepository
    {


        public static String Otp;
        public static string Email;

        public readonly DapperContext _context;

        private readonly ILogger _logger;
        public RegistrationServiceRepoLogic(DapperContext context, ILogger logger)
        {
            _context = context;
            _logger = logger; // Initialize NLog logger
        }

        public async Task<IEnumerable<Registration>> Getregdetails()
        {
            try
            {
                var query = "SELECT * FROM register";
                using (var connection = _context.CreateConnection())
                {
                    var users = await connection.QueryAsync<Registration>(query);
                    return users.AsList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while getting registration details");
                throw;
            }
        }


        public async Task<int> Addusers(Registration user)
        {
            try
            {
                // Hash the password
                string hashedPassword = HashPassword(user.password);

                // Prepare the query
                var query = $"INSERT INTO register (firstName, lastName, emailId, password) VALUES (@FirstName, @LastName, @EmailId, @Password)";

                // Execute the query
                using (var connection = _context.CreateConnection())
                {
                    var affectedRows = await connection.ExecuteAsync(query, new
                    {
                        user.firstName,
                        user.lastName,
                        user.emailId,
                        Password = hashedPassword // Pass the hashed password to the query parameter
                    });

                    return affectedRows;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while adding user");
                throw;
            }
        }

        private string HashPassword(string password)
        {
            // Hash the password using a secure hashing algorithm like SHA256
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }




        public async Task<int> Deleteusers(string email)
        {
            var query = $"Delete from register where emailId={email}";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.ExecuteAsync(query, email);
                return user;
            }
        }

        public async Task<int> updateuser(string email, Registration reg)
        {
            var query = $"update register set firstName=@FirstName,lastName=@lastName,emailId=@emailId,password=@password where emailId=@Email";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.ExecuteAsync(query,new { FirstName=reg.firstName,lastname=reg.lastName,emailId=reg.emailId,Password=reg.password,Email=email});
                return user;
            }
        }

        public async Task<string> userLogin(string email, string password, IConfiguration configuration)
        {
            try
            {
                _logger.Info($"Attempting login for user with email: {email}");

                var query = "SELECT * FROM register WHERE emailId = @Email";
                using (var connection = _context.CreateConnection())
                {
                    var user = await connection.QueryFirstOrDefaultAsync<Registration>(query, new { Email = email });

                    // Check if user exists
                    if (user != null)
                    {
                        // Hash the provided password
                        string hashedPassword = HashPassword(password);

                        // Compare the hashed password with the stored hashed password
                        if (hashedPassword == user.password)
                        {
                            _logger.Info($"User login successful for user with email: {email}");

                            // Generate JWT token
                            var token = GenerateJwtToken(user, configuration);
                            return token;
                        }
                    }

                    // Authentication failed
                    _logger.Warn($"User login failed for user with email: {email}. Invalid email or password.");
                    throw new UnauthorizedAccessException("Invalid email or password.");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"An error occurred while attempting login for user with email: {email}");
                throw;
            }
        }

        // Modify the GenerateJwtToken method to accept IConfiguration as a parameter
       
        private string GenerateJwtToken(Registration user, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]);
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.emailId),
                    // Add more claims as needed
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = issuer,
                Audience = audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        // code for forgot and reset password

        /*public async Task ForgotPassword(string email)
        {

            var user = await GetUserByEmail(email);
            if (user != null)
            {
                var Otp = GenerateOneTimePassword();
                // Store the token along with the user's email in your database or cache
                // Send an email to the user with a link containing the token for password reset
                RegistrationServiceRepoLogic.Otp= Otp;
                RegistrationServiceRepoLogic.Email = email;
                SendPasswordResetEmail(email, Otp);
            }
            else
            {
                // Handle case where email does not exist in the database
                throw new ArgumentException("User with provided email does not exist.");
            }
        }*/

        public async Task<Registration> GetUserByEmail(string email)
        {
            var query = "SELECT * FROM register WHERE emailId = @Email";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Registration>(query, new { Email = email });
            }
        }


        /* private string GenerateOneTimePassword()
         {
             // Generate a random six-digit OTP
             Random random = new Random();
             int otp = random.Next(100000, 999999);

             return otp.ToString();
         }*/





        /* public async Task SendPasswordResetEmail(string email, string Otp)
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
         }*/


        public async Task ResetPassword(string email, string otp, string newPassword)
        {
            // Check if the provided OTP matches the stored OTP for the user's email
            if (otp != Otp || email != Email)
            {
                throw new ArgumentException("Invalid OTP or email.");
            }

            // Hash the new password
            string hashedPassword = HashPassword(newPassword);

            // Update the user's password in the database
            var query = "UPDATE register SET password = @Password WHERE emailId = @Email";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Password = hashedPassword, Email = email });
            }
        }


    }
}
