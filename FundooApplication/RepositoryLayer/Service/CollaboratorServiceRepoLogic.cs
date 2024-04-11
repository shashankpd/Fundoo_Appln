using Dapper;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Entity;

namespace Repository.Service
{
    public class CollaboratorServiceRepoLogic : IcollaboratorRepository
    {

        private readonly DapperContext _context;

        public CollaboratorServiceRepoLogic(DapperContext context)
        {
            _context = context;
        }
        //start

        public async Task<int> Addcollaborator(Collabarator collab)
        {
            var query = @"
        INSERT INTO collabarator (NoteId, userid, collabEmail)
        VALUES (@noteid, @userid, @collabemail)";

            // Execute the query
            using (var connection = _context.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(query, new { collab.NoteId, collab.userid, collab.collabEmail });

                // Send email to collaborator
                await SendEmail(collab.collabEmail, "You have been added as a collaborator", "You have been added as a collaborator to a note.");

                return affectedRows;
            }
        }

        private async Task SendEmail(string toEmail, string subject, string body)
        {
            // Configure SMTP client for Outlook
            var smtpClient = new SmtpClient("smtp-mail.outlook.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("pdshashank8@outlook.com", "PDshashank@123"),
                EnableSsl = true,
            };

            // Create mail message
            var mailMessage = new MailMessage
            {
                From = new MailAddress("pdshashank8@outlook.com", "Added As Collaborator"),
                Subject = subject,
                Body = body,
                IsBodyHtml = false, // Change to true if sending HTML emails
            };
            mailMessage.To.Add(toEmail);

            // Send email
            await smtpClient.SendMailAsync(mailMessage);
        }

        public async Task<object> Getbycollabid(int collabid)
        {
            var query = @"
        SELECT c.*, n.* 
        FROM collabarator c 
        INNER JOIN Notes n ON c.NoteId = n.NoteId 
        WHERE c.collabid = @collabid";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync(query, new { collabid });

                // Return the dynamic result
                return result;
            }
        }

        public async Task<int> Deletebycollabid(int colid)
        {
            var query = $"Delete from collabarator where collabid={colid}";
            using (var connection = _context.CreateConnection())
            {
                var user = await connection.ExecuteAsync(query, colid);
                return user;
            }
        }



    }
}
