using ClarityAssignment.Domain.Infraestructure;
using ClarityAssignment.Infrastructure.API;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using ClarityAssignment.Application.Commands.Classes;
using ClarityAssignment.Infrastructure.Repositories;
using ClarityAssignment.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ClarityAssignment
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddControllers();
            services.AddDbContext<EmailDBContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("EmailDB")));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IEmailLogRepository, EmailLogRepository>();
            services.AddMediatR(typeof(SendEmailCommand).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
