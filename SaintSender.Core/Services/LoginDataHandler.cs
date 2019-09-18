using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace SaintSender.Core.Services
{
    public class LoginDataHandler
    {
        public void SaveLoginData(string email, string password)
        {
            LoginData loginData = new LoginData(email, password);
            Stream stream = File.Open("Login.dat", FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, loginData);
            stream.Close();
        }

        public LoginData LoadLoginDataFromHardDrive()
        {
            try
            {
                Stream stream = File.Open("Login.dat", FileMode.Open);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                LoginData loginData = (LoginData)binaryFormatter.Deserialize(stream);
                stream.Close();
                return loginData;

            }
            catch
            {
                Console.WriteLine("Couldn't Load LoginData from Login.dat");
            }
            return null;
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        
        

    }
}
