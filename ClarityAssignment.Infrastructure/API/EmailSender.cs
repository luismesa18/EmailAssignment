using ClarityAssignment.Domain.Infraestructure;
using ClarityAssignment.Domain.Model;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ClarityAssignment.Infrastructure.API
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration Configuration;
        public EmailSender (IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async Task<bool> SendEmailAsync(Email email)
        {
            SendGridClient client = new(Configuration.GetSection("SendGridapiKey").Value);
            EmailAddress from = new EmailAddress(email.From); 
            EmailAddress to = new EmailAddress(email.To);
            SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
            
            if(!string.IsNullOrEmpty(email.Attachment?.Base64Content))
                msg.AddAttachment(email.Attachment.Name, email.Attachment.Base64Content);

           var response =  await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode;
        }
    }
}
