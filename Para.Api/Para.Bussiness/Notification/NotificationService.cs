using System.Net.Mail;

namespace Para.Bussiness.Notification;

public class NotificationService  : INotificationService
{
    public void SendEmail(string subject, string email, string content)
    {
        
        SmtpClient mySmtpClient = new SmtpClient("my.smtp.exampleserver.net");

        mySmtpClient.UseDefaultCredentials = false;
        System.Net.NetworkCredential basicAuthenticationInfo = new
            System.Net.NetworkCredential("username", "password");
        mySmtpClient.Credentials = basicAuthenticationInfo;

        MailAddress from = new MailAddress("test@example.com", "TestFromName");
        MailAddress to = new MailAddress(email, "TestToName");
        MailMessage myMail = new System.Net.Mail.MailMessage(from, to);
        MailAddress replyTo = new MailAddress("reply@example.com");
        myMail.ReplyToList.Add(replyTo);

        myMail.Subject = subject;
        myMail.SubjectEncoding = System.Text.Encoding.UTF8;

        myMail.Body = "<b>Test Mail</b><br>using <b>HTML</b>." + content;
        myMail.BodyEncoding = System.Text.Encoding.UTF8;
        myMail.IsBodyHtml = true;

        mySmtpClient.Send(myMail);
    }
}