using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Linq;

namespace ark.net.util
{
    public class EmailUtil
    {
        string _email; string _pw; string _from; string _display; string _subject; string _smtp;int _port;
        public EmailUtil(string email, string pw, string from, string display, string subject, string smtp, int port)
        {
            _email = email;
            _pw = pw;
            _from = from;
            _display = display;
            _subject = subject;
            _smtp = smtp;
            _port = port;
        }
        public void SendEmail(string to, string htmlString)
        {
            SendEmail(to, htmlString, _subject, _display);
        }
        public void SendEmail(string to, string htmlString, string subject)
        {
            SendEmail(to, htmlString, subject, _display);
        }
        public void SendEmail(string to, string htmlString, string subject, string display)
        {
            SendEmail(new string[] { to }, new string[] { }, new string[] { }, htmlString, subject, display);
        }
        public void SendEmail(string[] to, string[] cc, string[] bcc, string htmlString, string subject, string display)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_email);
            email.From.Add(new MailboxAddress(_display, _from));
            (to ?? new string[] { }).ToList().ForEach(t =>
            {
                email.To.Add(MailboxAddress.Parse(t));
            });
            (cc ?? new string[] { }).ToList().ForEach(t =>
            {
                email.Cc.Add(MailboxAddress.Parse(t));
            });
            (bcc ?? new string[] { }).ToList().ForEach(t =>
            {
                email.Bcc.Add(MailboxAddress.Parse(t));
            });
            email.Subject = _subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = htmlString;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_smtp, _port, SecureSocketOptions.Auto);
            smtp.Authenticate(_email, _pw);
            smtp.Send(email);
            smtp.Disconnect(true);
            //EmailUtil u = new EmailUtil("sticky.notes@immanuel.co", "<pw>", "sticky.notes.alias@immanuel.co", "Sticky-Notes (ARK)", "Stick Notes: OTP", "mail.immanuel.co", 2525)
        }

    }
}
