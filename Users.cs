using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DzalekaNotifierFinal
{
    public class Users
    {

        //[PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string ImageUser { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string CodeUser { get; set; }
        public string question1 { get; set; }
        public string question2 { get; set; }
        public string question3 { get; set; }

        public Users(string imageUser, string name, string userName, string email, string phoneNumber, string address, string password, string Quest1,string Quest2, string Quest3)
        {
            ImageUser = imageUser;
            Name = name;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Password = password;
            question1 = Quest1;
            question2 = Quest2;
            question3 = Quest3;
        }

        public Users(string imageUser, string name, string userName, string email, string phoneNumber, string address, string password, string code, string Quest1, string Quest2, string Quest3)
        {
            ImageUser = imageUser;
            Name = name;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Password = password;
            CodeUser = code;
            question1 = Quest1;
            question2 = Quest2;
            question3 = Quest3;
        }

        public Users()
        {
        }
    }
}
