using System;
using System.Net.Mail;

namespace eCart.API.Data.Services.Mail
{
	public class MailService: IMailService
	{

        public async Task SendEmailAsync(string toEmail, string subject, string content, string Name)
        {
            var apiKey = "XYZ";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("haseebahmed02@gmail.com", Name);
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
        }
	}
}