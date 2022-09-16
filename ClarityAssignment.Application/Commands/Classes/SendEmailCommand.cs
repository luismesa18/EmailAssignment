using ClarityAssignment.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityAssignment.Application.Commands.Classes
{
    public class SendEmailCommand : IRequest<ApiResponse>
    {
        public string To { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
        public string AttachmentContent { get; private set; }
        public string AttachmentName { get; private set; }
        public bool Retry { get; private set; }
        public SendEmailCommand(string to, string subject, string body, string attachmentContent, string attachmentName, bool retry)
        {
            To = to;
            Subject = subject;
            Body = body;
            AttachmentContent = attachmentContent;
            AttachmentName = attachmentName;
            Retry = retry;
        }
    }
}
