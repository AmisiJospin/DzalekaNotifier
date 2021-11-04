using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DzalekaNotifierFinal
{
    public class UserImageSource
    {
        public ImageSource ImageUser { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }

        public UserImageSource(ImageSource imageUser, string name, string userName, string email, string phoneNumber, string address, string password)
        {
            ImageUser = imageUser;
            Name = name;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Password = password;
        }

        public UserImageSource()
        {
        }
    }
}
