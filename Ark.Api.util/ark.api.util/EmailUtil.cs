﻿using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Linq;

namespace ark.net.util
{
    public class EmailUtil
    {
        string _email; string _pw; string _from; string _display; string _subject; string _smtp; int _port;
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
            email.From.Add(new MailboxAddress(display, _from));
            (to ?? new string[] { }).Where(t => !string.IsNullOrEmpty(t)).Select(t => t.Trim()).ToList().ForEach(t =>
            {
                email.To.Add(MailboxAddress.Parse(t));
            });
            (cc ?? new string[] { }).Where(t => !string.IsNullOrEmpty(t)).Select(t => t.Trim()).ToList().ForEach(t =>
            {
                try { var tt = MailboxAddress.Parse(t); email.Cc.Add(tt); } catch { }
            });
            (bcc ?? new string[] { }).Where(t => !string.IsNullOrEmpty(t)).Select(t => t.Trim()).ToList().ForEach(t =>
            {
                try { var tt = MailboxAddress.Parse(t); email.Bcc.Add(tt); } catch { }
            });
            email.Subject = subject;
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
        /// <summary>
        /// Simple Method validates the email format
        /// </summary>
        /// <param name="email">ex: raj@immanuel.co, invalid_email </param>
        /// <returns>returns true/false</returns>
        public static bool IsValidFormat(string email)
        {
            try
            {
                var tt = new System.Net.Mail.MailAddress(email);
                return tt.Address == email;
            }
            catch (System.Exception exx)
            {
                return false;
            }
        }
    }
}
