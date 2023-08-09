using System;
using System.Diagnostics;
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

        public Task SendEmailAsyncBrevo(string toEmail, string subject, string content, string Name)
        {
            Configuration.Default.ApiKey.Add("api-key", "YOUR API KEY");

            var apiInstance = new TransactionalEmailsApi();
            string tag = null;
            string senderName = "John Doe";
            string senderEmail = "example@example.com";
            string templateName = "Example Template";
            string htmlContent = content;
            string htmlUrl = null;
            string subject = subject;
            //string replyTo = "replyto@domain.com";
            string toField = toEmail;
            bool? isActive = true;
            string attachmentUrl = "https://example.net/upload-file";
            CreateSmtpTemplateSender sender = new CreateSmtpTemplateSender(senderName, senderEmail);
            try
            {
                var smtpTemplate = new CreateSmtpTemplate(tag, sender, templateName, htmlContent, htmlUrl, subject, replyTo, toField, attachmentUrl, isActive);
                CreateModel result = apiInstance.CreateSmtpTemplate(smtpTemplate); Debug.WriteLine(result.ToJson());
                Console.WriteLine(result.ToJson());
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}