
using System.Net.Mail;
using System.Text;
namespace BlogyBackend.Shared;
public class Smtp
{
    public const string From = "blogy.technical@gmail.com";

    public static void SendMessage(string toEmail, string subject, string body)
    {

        MailMessage message = new MailMessage(From, toEmail);
        message.Subject = subject;
        message.Body = body;
        message.BodyEncoding = Encoding.UTF8;
        message.IsBodyHtml = true;
        SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
        System.Net.NetworkCredential basicCredential1 = new
        System.Net.NetworkCredential(Smtp.From, "2510203121");
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = basicCredential1;
        client.Send(message);

    }
}