using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClarityAssignment.Console
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                string continueProgram = "y";
                while (continueProgram.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                {
                    System.Console.WriteLine("Type email recipient: ");
                    string to = System.Console.ReadLine();
                    System.Console.WriteLine("Type Subject: ");
                    string subject = System.Console.ReadLine();
                    System.Console.WriteLine("Type Body: ");
                    string body = System.Console.ReadLine();

                    //Default attachment for tests purpose
                    var bytes = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "Attachment", "Prueba.txt"));
                    var file = Convert.ToBase64String(bytes);

                    var request = new Email()
                    {
                        To = to,
                        Subject = subject,
                        Body = body,
                        AttachmentContent = file,
                        AttachmentName = "Prueba.txt"
                    };


                    var result = await CallService(request);
                    System.Console.WriteLine(result ? "Email sent Succesfully!" : "Email sent Failed!");
                    System.Console.WriteLine("Do you want to continue exuting the program? (Y/N)");
                    continueProgram = System.Console.ReadLine();
                }

            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Exception - {ex.Message}");
            }
        }
        static async Task<bool> CallService(Email request)
        {
            
            var httpClient = new HttpClient();
            var json = JsonSerializer.Serialize(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(@"https://localhost:44397/api/emailsender/sendemail", data);
            
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var result = JsonSerializer.Deserialize<Response>(response.Content.ReadAsStringAsync().Result, options);

            return result.Successful;
        }
    }
}
