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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SaintSender.Core.Services;
using SaintSender.DesktopUI.Views;

namespace SaintSender.DesktopUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LoginData loginData;
        public MainWindow()
        {
            LoginDataHandler loginDataHandler = new LoginDataHandler();
            loginData = loginDataHandler.LoadLoginDataFromHardDrive();
            InitializeComponent();
        }

        private void GreetBtn_Click(object sender, RoutedEventArgs e)
        {
            var service = new GreetService();
            var name = NameTxt.Text;
            var greeting = service.Greet(name);
            ResultTxt.Text = greeting;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            if (loginData == null)
            {
                loginWindow.Show();
            }
            else
            {
                MessagesMainWindow messageBoxWindow = new MessagesMainWindow();
                messageBoxWindow.Show();
            }
            this.Close();
        }

        
    }
}
