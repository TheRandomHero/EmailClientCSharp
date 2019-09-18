using MailKit;
using MailKit.Security;
using MailKit.Net.Imap;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using System.Collections.ObjectModel;
using MimeKit;

namespace SaintSender.Core.Services
{
    public class EmailServices
    {
        private ImapClient _client;


        public void ConnectToIMAPService(string username, string password)
        {
            this._client = new ImapClient();

            try
            {
                _client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                _client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                _client.Authenticate(username, password);

            }
            catch (Exception e)
            {
                throw new ArgumentException($"Failed to connect to IMAP service: {e}");
            }
        }

        public ObservableCollection<MimeMessage> GetInboxEmails()
        {
            var emails = new ObservableCollection<MimeMessage>();

            if (IsConnected())
            {
                var inbox = _client.Inbox;
                inbox.Open(MailKit.FolderAccess.ReadOnly);

                var emailSummaries = inbox.Fetch(0, -1, MessageSummaryItems.UniqueId | MessageSummaryItems.Size | MessageSummaryItems.Flags);

                foreach (var summary in emailSummaries)
                {
                    IList<IMessageSummary> info = inbox.Fetch(new[] { summary.UniqueId }, MessageSummaryItems.Flags | MessageSummaryItems.GMailLabels);

                    var email = inbox.GetMessage(summary.UniqueId);

                    emails.Add(_client.Inbox.GetMessage(summary.UniqueId));
                }
            }
            return emails;
        }
        private string GetEmailStatus(IList<IMessageSummary> info)
        {
            return info[0].Flags.Value.HasFlag(MessageFlags.Seen) ? "seen" : "unseen";
            string status;

            if (info[0].Flags.Value.HasFlag(MessageFlags.Seen))
            {
                status = "seen";
            }
            else
            {
                status = "unseen";
            }

            return status;
        }

        public bool IsConnected()
        {
            if (_client != null)
            {
                return _client.IsAuthenticated;
            }
            else
            {
                return false;
            }
        }

        public void DisconnectFromIMAPService()
        {
            _client.Disconnect(true);
        }

        public void SendEmail(LoginData loginData, string to, string subject, string message)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress(loginData.Email));
            email.To.Add(new MailboxAddress(to));
            email.Subject = subject;
            email.Body = new TextPart("html") { Text = message };

            using (var client = new SmtpClient(new ProtocolLogger("smtp.log")))
            {
                client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                client.Authenticate(loginData.Email, loginData.Password);
                client.Send(email);
                client.Disconnect(true);
            }
        }
    }
}
