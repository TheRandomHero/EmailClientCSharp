using MimeKit;
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

namespace SaintSender.DesktopUI.Views.Message_Modal
{
    /// <summary>
    /// Interaction logic for EmailMessage.xaml
    /// </summary>
    public partial class EmailMessage : Window
    {
        public EmailMessage()
        {
            InitializeComponent();
        }

        public EmailMessage(MimeMessage mail)
        {
            InitializeComponent();
            SenderBox.Text = mail.From.ToString();
            SubjectBox.Text = mail.Subject;
            BodyText.Text = mail.GetTextBody(MimeKit.Text.TextFormat.Plain);
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
