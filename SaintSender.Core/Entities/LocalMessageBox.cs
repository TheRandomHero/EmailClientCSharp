using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using MailKit;
using MailKit.Search;
using MailKit.Net.Imap;

namespace SaintSender.Core.Entities
{
    public class LocalMessageBox
    {
        private string _emailAddress;
        private string _password;


        public LocalMessageBox(string address, string password)
        {
            _emailAddress = address;
            _password = password;
        }
            
        public LocalMessageBox()
        {

        }

        public ObservableCollection<MimeMessage> GetAllMessages()
        {
            ObservableCollection<MimeMessage> messages = new ObservableCollection<MimeMessage>();

            using(var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, MailKit.Security.SecureSocketOptions.SslOnConnect);
                try
                {
                    client.Authenticate(_emailAddress, _password);

                    client.Inbox.Open(FolderAccess.ReadOnly);

                    var messagesIds = client.Inbox.Search(SearchQuery.All);

                    foreach (var messageId in messagesIds)
                    {
                        messages.Add(client.Inbox.GetMessage(messageId));
                    }
                }
                catch(ArgumentNullException e)
                {
                    Console.WriteLine("Getting messages failed");
                }
                
            }
            return messages;
        }
    }
}
