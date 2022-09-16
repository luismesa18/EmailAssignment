using System;
using System.Collections.Generic;

namespace ClarityAssignment.Job.Model
{
    public class Email
    {
        public string To { get;  set; }
        public string Subject { get;  set; }
        public string Body { get;  set; }
        public string AttachmentContent { get; set; }
        public string AttachmentName { get; set; }
        public bool Retry { get; set; }

    }
}
