using SaintSender.DesktopUI.ViewModels;
using System.Windows;
using SaintSender.Core.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MailKit;
using MimeKit;
using System.Windows.Controls;
using SaintSender.DesktopUI.Views.Message_Modal;
using System;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for MessagesMainWindow.xaml
    /// </summary>
    public partial class MessagesMainWindow : Window
    {
        private MessagesMainWindowViewModel _mmvm;
        public ObservableCollection<MimeMessage> ObsEmails { get; set; }

        public MessagesMainWindow()
        {
            InitializeComponent();
            _mmvm = new MessagesMainWindowViewModel();
            ObsEmails = _mmvm.GetAllEmailsForUser();

            mailbox.DataContext = ObsEmails;


            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 5000;
            timer.Elapsed += UpdateEmailList;
            timer.AutoReset = true;
            timer.Start();


        }


        private void Mailbox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                MimeMessage email;
                email = (MimeMessage)mailbox.SelectedItem;
                var emailMessageWindow = new EmailMessage(email);
                emailMessageWindow.Show();
            }
            catch (NullReferenceException c)
            {
                Console.WriteLine(c);
            }

           
        }
        private void UpdateEmailList(object sender, System.Timers.ElapsedEventArgs e)
        {
            ObsEmails = _mmvm.GetAllEmailsForUser();
            this.Dispatcher.Invoke(() =>
            {
                mailbox.DataContext = ObsEmails;
            });
            Console.WriteLine("Emails updated");

        }

        private void ComposeButton_Click(object sender, RoutedEventArgs e)
        {
            LoginDataHandler loginDataHandler = new LoginDataHandler();
            LoginData loginData = loginDataHandler.LoadLoginDataFromHardDrive();
            ComposerWindow composeWindow = new ComposerWindow(loginData);
            composeWindow.Show();
        }
    }
}
