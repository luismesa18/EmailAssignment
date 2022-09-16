using ClarityAssignment.Application.Commands.Classes;
using ClarityAssignment.Domain.Common;
using ClarityAssignment.Domain.Infraestructure;
using ClarityAssignment.Domain.Model;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClarityAssignment.Application.Commands.Handlers
{
    public class SendEmailHandler : IRequestHandler<SendEmailCommand, ApiResponse>
    {
        private readonly IEmailSender _emailSender;
        private readonly IEmailLogRepository _emailLogRepository;
        private readonly IConfiguration _configuration;
        public SendEmailHandler(IEmailSender emailSender, IEmailLogRepository emailLogRepository, IConfiguration configuration)
        {
            _emailSender = emailSender;
            _emailLogRepository = emailLogRepository;
            _configuration = configuration;
        }

        public async Task<ApiResponse> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                
                Email email = new(_configuration.GetSection("EmailSender").Value, request.To, request.Subject, request.Body);
                if (!string.IsNullOrEmpty(request.AttachmentContent))
                    email.AddAttachment(request.AttachmentName, request.AttachmentContent);
                var emailResult = await _emailSender.SendEmailAsync(email);

                var emailLog = new EmailLog()
                {
                    Attempts = 1,
                    Body = email.Body,
                    Created = DateTime.Now,
                    Sender = email.From,
                    Id = Guid.NewGuid(),
                    LastUpdate = DateTime.Now,
                    Successful = emailResult,
                    Subject = email.Subject,
                    Recipient = email.To
                };
                if(!request.Retry)
                    await _emailLogRepository.AddAsync(emailLog);

                apiResponse.Successful = emailResult;
                apiResponse.Message = emailResult ? Constants.EMAIL_SENT_SUCCESS_MSG : Constants.EMAIL_SENT_FAIL_MSG;
            }
            catch (Exception ex)
            {
                apiResponse.Successful = false;
                apiResponse.Message = Constants.EMAIL_SENT_FAIL_MSG;
            }

            return apiResponse;


        }
    }
}
