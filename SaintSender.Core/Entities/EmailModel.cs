using System;

namespace SaintSender.Core.Services
{
    public class EmailModel
    {
        private string From { get; set; }
        private string Subject { get; set; }
        private DateTime DateTime { get; set; }
        private string Status { get; set; }
        private string Text { get; set; }

        public EmailModel(string from, string subject, DateTime dateTime, string status, string text)
        {
            this.From = from;
            this.Subject = subject;
            this.DateTime = dateTime;
            this.Status = status;
            this.Text = text;
        }
    }
}