using ClarityAssignment.Job.Model;

namespace ClarityAssignment.Job.Infrastructure
{
    public interface IEmailLogRepository
    {
        Task<IEnumerable<EmailLog>> GetUnsentEmails();
        Task Update(EmailLog emailLog);
    }
}