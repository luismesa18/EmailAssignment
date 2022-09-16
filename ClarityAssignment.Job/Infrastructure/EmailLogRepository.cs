using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using ClarityAssignment.Job.Model;

namespace ClarityAssignment.Job.Infrastructure
{
    public class EmailLogRepository : IEmailLogRepository
    {
        private readonly IConfiguration _configuration;


        public EmailLogRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<EmailLog>> GetUnsentEmails()
        { 
            using var connection = new SqlConnection(_configuration.GetConnectionString("EmailDB"));
            var dictionary = new Dictionary<string, object> { { "@Successful", false }, { "@MaxAttempts", Convert.ToInt32(_configuration.GetSection("MaxAttempts").Value) } };


            return await connection.QueryAsync<EmailLog>(@"SELECT [Id], [Sender], [Recipient], [Subject], [Body], [Successful], [Attempts], [Created], [LastUpdate]
                FROM [dbo].[EmailLogs] 
                WHERE [Successful] = @Successful AND [Attempts] < @MaxAttempts
                ORDER BY Created", dictionary);

        }

        public async Task Update(EmailLog emailLog)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("EmailDB"));

             await connection.ExecuteAsync(@"UPDATE [dbo].[EmailLogs]
	            SET [Successful] = @Successful,
		            [Attempts] = @Attempts,
		            [LastUpdate] = @LastUpdate
                WHERE Id = @Id", emailLog);

        }
    }
}
