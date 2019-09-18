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
using SaintSender.Core.Services;

namespace SaintSender.DesktopUI.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            string email = emailBox.Text;
            string password = passwordBox.Password;
            EmailServices emailService = new EmailServices();
            LoginDataHandler loginService = new LoginDataHandler();
            if (loginService.IsValidEmail(email))
            {
                emailService.ConnectToIMAPService(email, password);
                if (emailService.IsConnected())
                {
                    loginService.SaveLoginData(email, password);
                    MessagesMainWindow mainWindow = new MessagesMainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Email or Password is incorrect, please try again.",
                        "Incorrect login",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

            }
            else
            {
                MessageBox.Show("Please enter a valid email!",
                    "Email Invalid",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
