using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityAssignment.Domain.Model
{
    public class Attachment
    {
        public string Name { get; private set; }
        public string Base64Content { get; private set; }
        public Attachment(string name, string base64Content)
        {
            Name = name;
            Base64Content = base64Content;
        }
    }
}
