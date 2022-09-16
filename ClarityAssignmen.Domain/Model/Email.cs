using System;
using System.Collections.Generic;
using System.Text;

namespace ClarityAssignment.Domain.Model
{
    public class Email
    {
        public string From { get; private set; } 
        public string To { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
        public Attachment Attachment { get; private set; }
        public Email(string from, string to, string subject, string body)
        {
            From = from;
            To = to;
            Subject = subject;
            Body = body;
        }
        public void AddAttachment(string FileName, string base64ContentFile)
        {
            Attachment = new Attachment(FileName, base64ContentFile);
        }
       
    }
}
