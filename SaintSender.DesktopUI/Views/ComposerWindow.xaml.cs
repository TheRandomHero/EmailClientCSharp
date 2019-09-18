using SaintSender.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for ComposeWindow.xaml
    /// </summary>
    public partial class ComposerWindow : Window
    {
        LoginData loginData;
        public ComposerWindow(LoginData loginData)
        {
            this.loginData = loginData;
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to discard this email?", "Discard compose", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string to = toTextBox.Text;
            string subject = subjectTextBox.Text;
            string message = messageTextBox.Text;
            EmailServices emailServices = new EmailServices();
            emailServices.SendEmail(loginData, to, subject, message);
            MessageBox.Show("Email sent to: " + to, "Mail sent", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
