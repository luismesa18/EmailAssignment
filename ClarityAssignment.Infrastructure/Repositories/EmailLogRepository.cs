using ClarityAssignment.Domain.Infraestructure;
using ClarityAssignment.Domain.Model;
using ClarityAssignment.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityAssignment.Infrastructure.Repositories
{
    public class EmailLogRepository : IEmailLogRepository
    {
        private readonly EmailDBContext _emailDBContext;
        public EmailLogRepository(EmailDBContext emailDBContext)
        {
            _emailDBContext = emailDBContext;
        }

        public async Task<int> AddAsync(EmailLog emailLog)
        {
             _emailDBContext.Add(emailLog);
            return await _emailDBContext.SaveChangesAsync();
        }

    }
}
