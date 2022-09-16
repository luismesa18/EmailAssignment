using ClarityAssignment.Application.Commands.Classes;
using ClarityAssignment.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ClarityAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSenderController : ControllerBase
    {
        private readonly IMediator mediatr;
        public EmailSenderController(IMediator mediatr)
        {
            this.mediatr = mediatr;
        }
        [HttpPost("SendEmail")]
        public async Task<ActionResult<ApiResponse>> SendEmailAsync(SendEmailCommand request)
        {
            try
            {
                return Ok(await mediatr.Send(request));
            }catch(Exception ex)
            {
                return StatusCode(500, new ApiResponse() { Successful = false, Message =Constants.EMAIL_SENT_EXC_MSG });
            }
        }
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(Constants.SERVICE_RUNNING_MSG);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Exception");
            }
        }
    }
}
