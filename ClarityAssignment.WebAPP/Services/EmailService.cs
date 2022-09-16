using ClarityAssignment.WebAPP.Models;
using System.Text;
using System.Text.Json;

namespace ClarityAssignment.WebAPP.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;
        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<bool> SendEmail(EmailViewModel email)
        {
            var httpClient = new HttpClient();
            var json = JsonSerializer.Serialize(email);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(configuration.GetValue<string>("UrlEmailService"), data);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = JsonSerializer.Deserialize<Response>(response.Content.ReadAsStringAsync().Result, options);
            return result.Successful;
        }
    }
}
