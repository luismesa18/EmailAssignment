using ClarityAssignment.WebAPP.Models;

namespace ClarityAssignment.WebAPP.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailViewModel email);
    }
}