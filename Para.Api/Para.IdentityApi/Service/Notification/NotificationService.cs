using System.Net.Mail;
using Para.IdentityApi.Schema;

namespace Para.IdentityApi.Service;

public class NotificationService  : INotificationService
{
    public void SendEmail(NotificationTemplate template)
    {
        SmtpClient mySmtpClient = new SmtpClient("my.smtp.exampleserver.net");

        mySmtpClient.UseDefaultCredentials = false;
        System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("username", "password");
        mySmtpClient.Credentials = basicAuthenticationInfo;

        MailAddress from = new MailAddress("test@example.com", "TestFromName");
        MailAddress to = new MailAddress(template.Email, "TestToName");
        MailMessage myMail = new System.Net.Mail.MailMessage(from, to);
        MailAddress replyTo = new MailAddress("reply@example.com");
        myMail.ReplyToList.Add(replyTo);

        myMail.Subject = template.Subject;
        myMail.SubjectEncoding = System.Text.Encoding.UTF8;

        myMail.Body = "<b>Test Mail</b><br>using <b>HTML</b>." + template.Content;
        myMail.BodyEncoding = System.Text.Encoding.UTF8;
        myMail.IsBodyHtml = true;

        mySmtpClient.Send(myMail);
    }
}