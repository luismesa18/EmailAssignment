using ClarityAssignment.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityAssignment.Infrastructure.Context
{
    public class EmailDBContext : DbContext
    {
        public EmailDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<EmailLog> EmailLogs { get; set; }

    }
}
