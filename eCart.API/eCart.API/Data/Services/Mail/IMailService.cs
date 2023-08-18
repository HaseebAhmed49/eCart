using System;
namespace eCart.API.Data.Services.Mail
{
	public interface IMailService
	{
        Task SendEmailAsync(string toEmail, string subject, string content, string Name);

        Task SendEmailAsyncBrevo(string toEmail, string toName, string subject, string content, string Name);

        Task SendSMSAsyncBrevo(string recipientNumber, string senderName, string message);
    }
}

