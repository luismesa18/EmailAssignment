using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityAssignment.Job.Model
{
    public class EmailLog
    {
        public Guid Id { get; set; }
        public string Sender { get; set; }
        public string Recipient { get;  set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool Successful { get; set; }
        public int Attempts { get ; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
