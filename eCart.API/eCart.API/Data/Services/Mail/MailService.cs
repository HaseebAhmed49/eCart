using System;
using System.Configuration;
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
            // Api Instance
            var apiInstance = new TransactionalEmailsApi();
            string tag = null;
            string senderName = "Haseeb Ahmed";

            // Sender Details
            string senderEmail = "haseebahmed02@gmail.com";
            string templateName = "Example Template";

            // Html Content
            string htmlContent = content;
            string htmlUrl = null;

            // Subject
            string subject = subject;
            //string replyTo = "replyto@domain.com";

            // ToEmail
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