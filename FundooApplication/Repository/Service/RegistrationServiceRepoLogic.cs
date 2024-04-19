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
using Confluent.Kafka;
using RepositoryLayer.Helper;
using ModelLayer.Request_Body;

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


        public async Task<int> Addusers(ResgistrationBody user)
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
                    //-----------------------
                    var registrationDetailsForPublishing = new RegistrationDetailsForPublishing(user);


                    Helper help = new Helper();
                    help.producer(registrationDetailsForPublishing);

                    //-----------------------

                    return affectedRows;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error occurred while adding user");
                throw;
            }
        }
        // Hash the password using a secure hashing algorithm like SHA256
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public async Task<int> Deleteusers(int userid)
        {
            var query = "DELETE FROM register WHERE userid = @userid";
            using (var connection = _context.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(query, new { userid });
                return affectedRows;
            }
        }


        public async Task<int> updateuser(int userid, Registration reg)
        {
            var query = $"update register set firstName=@FirstName,lastName=@lastName,emailId=@emailId,password=@password where userid=@UserId";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.ExecuteAsync(query,new { FirstName=reg.firstName,lastname=reg.lastName,emailId=reg.emailId,Password=reg.password, UserId = userid});
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


        // Generating JWT Tokens    
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
                new Claim("UserId", user.userid.ToString())
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
        

        public async Task<Registration> GetUserByEmail(string email)
        {
            var query = "SELECT * FROM register WHERE emailId = @Email";
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Registration>(query, new { Email = email });
            }
        }

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
