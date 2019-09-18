using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SaintSender.Core.Services
{
    //cblursaintsender@gmail.com
    //saintsender
    [Serializable()]
    public class LoginData : ISerializable
    {
        public string Email;
        public string Password;

        public LoginData (string email, string password)
        {
            Email = email;
            Password = password;
        }

        public LoginData (SerializationInfo info, StreamingContext context)
        {
            Email = (string)info.GetValue("Email", typeof(string));
            Password = (string)info.GetValue("Password", typeof(string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Email", Email);
            info.AddValue("Password", Password);
        }

        

    }
}
