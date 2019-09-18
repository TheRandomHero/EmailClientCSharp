using SaintSender.Core.Entities;
using SaintSender.Core.Services;
using MimeKit;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaintSender.DesktopUI.ViewModels
{
    class MessagesMainWindowViewModel
    {
        public EmailServices emailServices;
        public LoginData loginData;
        public List<EmailModel> Emails { get; set; }
            

        public MessagesMainWindowViewModel()
        {
            emailServices = new EmailServices();
            LoginDataHandler loginDataHandler = new LoginDataHandler();
            loginData = loginDataHandler.LoadLoginDataFromHardDrive();
        }

        public ObservableCollection<MimeMessage> GetAllEmailsForUser()
        {
            try
            {
                emailServices.ConnectToIMAPService(loginData.Email, loginData.Password);
                return emailServices.GetInboxEmails();
            }
            catch(MailKit.Security.AuthenticationException e)
            {
                System.Console.WriteLine(e);
                return null;
            }
        }
    }
}
