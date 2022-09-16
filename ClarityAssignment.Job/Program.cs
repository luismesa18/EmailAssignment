
using ClarityAssignment.Job.Infrastructure;
using ClarityAssignment.Job.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClarityAssignment.Job
{
    internal class Program
    {
        private static IConfiguration Configuration { get; set;  }
        private static EmailLogRepository emailLogRepository { get; set; }
        static async Task Main(string[] args)
        {
            try
            {
                //This sleep is just to wait for the API to run when we launch everything inside of Visual Studio. For test purposes only
                Thread.Sleep(8000);
                
                InitializeConfiguration();

                IEnumerable<EmailLog> emailLogs = await GetUnsentEmails();

                var succession = Convert.ToBoolean(Configuration.GetSection("Succession").Value);
                var maxAttempts = Convert.ToInt32(Configuration.GetSection("MaxAttempts").Value);
                ParallelOptions parallelOptions = new ParallelOptions() { MaxDegreeOfParallelism = Convert.ToInt32(Configuration.GetSection("MaxThreads").Value) };
                Parallel.ForEach(emailLogs, parallelOptions, (emaillog) =>
                {
                    for (int i = emaillog.Attempts; i < maxAttempts; i++)
                    {
                        var result = CallService(emaillog);
                        emaillog.Attempts += 1;
                        emaillog.LastUpdate = DateTime.Now;
                        emaillog.Successful = result;
                        Update(emaillog);
                        if (result || !succession)
                            break;
                    }
                });
                Console.WriteLine("Job finished!!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception - {ex.Message}");
            }
        }
        private static void InitializeConfiguration()
        {
            Configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json")
                   .Build();
            emailLogRepository = new EmailLogRepository(Configuration);
        }
       
        static bool CallService(EmailLog input)
        {

            var httpClient = new HttpClient();
            var request = new Email()
            {
                To = input.Recipient,
                Subject = input.Subject,
                Body = input.Body,
                Retry = true
            };

            var json = JsonSerializer.Serialize(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(Configuration.GetSection("UrlEmailService").Value, data).Result;
       
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = JsonSerializer.Deserialize<Response>(response.Content.ReadAsStringAsync().Result, options);

            return result.Successful;
        }
        private static void Update(EmailLog emailLog)
        {
            //EmailLogRepository emailLogRepository = new EmailLogRepository(Configuration);
            emailLogRepository.Update(emailLog).Wait();
        }
        private async static Task<IEnumerable<EmailLog>> GetUnsentEmails()
        {
            //EmailLogRepository emailLogRepository = new EmailLogRepository(Configuration);
            return await emailLogRepository.GetUnsentEmails();
        }
    }
}

