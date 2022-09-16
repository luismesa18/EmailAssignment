using ClarityAssignment.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityAssignment.Domain.Infraestructure
{
    public interface IEmailLogRepository
    {
        Task<int> AddAsync(EmailLog emailLog);
    }
}
