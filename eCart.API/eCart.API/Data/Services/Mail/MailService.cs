using System;
using System.Diagnostics;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using Task = System.Threading.Tasks.Task;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using eCart.API.Data.Services.Basket;
using eCart.API.Data.Services.UoW;
using Stripe;

namespace eCart.API.Data.Services.Mail
{
	public class MailService: IMailService
	{
        private readonly IConfiguration _config;

        public MailService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string content, string Name)
        {
            var apiKey = _config["SendGrid:api-key"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("haseebahmed02@googlemail.com", Name);
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
        }

        public async Task SendEmailAsyncBrevo(string toEmail,string toName,  string subjectEmail, string content, string Name)
        {
            #region SMTP Template
            //// Api Instance
            //var apiInstance = new TransactionalEmailsApi();
            //var apiKey = _config["SendInBlue:api-key"];
            //apiInstance.Configuration.AddApiKey("api-key", apiKey);
            //string tag = null;
            //string senderName = "Powersoft19 Pvt Ltd";

            //// Sender Details
            //string senderEmail = "haseebahmed02@googlemail.com";
            //string templateName = "Example Template";

            //// Html Content
            //string htmlContent = content;
            //string htmlUrl = null;

            //// Subject
            //string subject = subjectEmail;
            //string replyTo = "haseebahmed02@googlemail.com";

            //// ToEmail
            //string toField = toEmail;
            //bool? isActive = true;
            //string attachmentUrl = null;
            //CreateSmtpTemplateSender sender = new CreateSmtpTemplateSender(senderName, senderEmail);
            //var smtpTemplate = new CreateSmtpTemplate(tag, sender, templateName, htmlContent, htmlUrl, subject, replyTo, toField, attachmentUrl, isActive);
            //var result = await apiInstance.SendTransacEmailAsync(smtpTemplate); Debug.WriteLine(result.ToJson());

            #endregion

            var apiInstance = new TransactionalEmailsApi();
            var apiKey = _config["SendInBlue:api-key"];            
            apiInstance.Configuration.AddApiKey("api-key", apiKey);

            string SenderName = "Powersoft19 Pvt Ltd";
            string SenderEmail = "haseebahmed02@googlemail.com";
            SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);

            string ToEmail = toEmail;
            string ToName = toName;
            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(ToEmail, ToName);
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(smtpEmailTo);

            //string BccName = "Janice Doe";
            //string BccEmail = "example2@example2.com";
            //SendSmtpEmailBcc BccData = new SendSmtpEmailBcc(BccEmail, BccName);
            //List<SendSmtpEmailBcc> Bcc = new List<SendSmtpEmailBcc>();
            //Bcc.Add(BccData);
            //string CcName = "John Doe";
            //string CcEmail = "example3@example2.com";
            //SendSmtpEmailCc CcData = new SendSmtpEmailCc(CcEmail, CcName);
            //List<SendSmtpEmailCc> Cc = new List<SendSmtpEmailCc>();
            //Cc.Add(CcData);

            string HtmlContent = content;
            string TextContent = null;

            string Subject = subjectEmail;

            string ReplyToName = "Powersoft19 Pvt Ltd";
            string ReplyToEmail = "haseebahmed02@googlemail.com";
            SendSmtpEmailReplyTo ReplyTo = new SendSmtpEmailReplyTo(ReplyToEmail, ReplyToName);

            //string AttachmentUrl = null;
            //string stringInBase64 = "aGVsbG8gdGhpcyBpcyB0ZXN0";
            //byte[] Content = System.Convert.FromBase64String(stringInBase64);
            //string AttachmentName = "test.txt";
            //SendSmtpEmailAttachment AttachmentContent = new SendSmtpEmailAttachment(AttachmentUrl, Content, AttachmentName);
            //List<SendSmtpEmailAttachment> Attachment = new List<SendSmtpEmailAttachment>();
            //Attachment.Add(AttachmentContent);

            //JObject Headers = new JObject();
            //Headers.Add("Some-Custom-Name", "unique-id-1234");
            //long? TemplateId = null;
            //JObject Params = new JObject();
            //Params.Add("parameter", "My param value");
            //Params.Add("subject", "New Subject");
            //List<string> Tags = new List<string>();
            //Tags.Add("mytag");
            SendSmtpEmailTo1 smtpEmailTo1 = new SendSmtpEmailTo1(ToEmail, ToName);
            List<SendSmtpEmailTo1> To1 = new List<SendSmtpEmailTo1>();
            To1.Add(smtpEmailTo1);
            //Dictionary<string, object> _parmas = new Dictionary<string, object>();
            //_parmas.Add("params", Params);
            SendSmtpEmailReplyTo1 ReplyTo1 = new SendSmtpEmailReplyTo1(ReplyToEmail, ReplyToName);
            SendSmtpEmailMessageVersions messageVersion = new SendSmtpEmailMessageVersions(To1, null, null, null, ReplyTo1, Subject);
            List<SendSmtpEmailMessageVersions> messageVersiopns = new List<SendSmtpEmailMessageVersions>();
            messageVersiopns.Add(messageVersion);
            var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, HtmlContent, TextContent, Subject, ReplyTo, null, null, null, null, messageVersiopns, null);
            CreateSmtpEmail result = await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
        }

        public async Task SendSMSAsyncBrevo(string recipientNumber, string senderName, string message)
        {
            var apiInstance = new TransactionalSMSApi();
            var apiKey = _config["SendInBlue:api-key"];
            apiInstance.Configuration.AddApiKey("api-key", apiKey);

            string sender = "Haseeb";
            string recipient = recipientNumber;
            string content = message;
            SendTransacSms.TypeEnum type = SendTransacSms.TypeEnum.Transactional;
            string tag = "testTag";
            string webUrl = null;
            var sendTransacSms = new SendTransacSms(sender, recipient, content, type, tag, webUrl);
            SendSms result = await apiInstance.SendTransacSmsAsync(sendTransacSms);
        }
    }
}