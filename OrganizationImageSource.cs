using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DzalekaNotifierFinal
{
    public class OrganizationImageSource
    {
        public ImageSource ImageID { get; set; }
        public ImageSource Image { get; set; }
        public string OGName { get; set; }
        public string AdminName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }

        public OrganizationImageSource(ImageSource imageID, ImageSource image, string oGName, string adminName, string userName, string email, string phoneNumber, string address, string password)
        {
            ImageID = imageID;
            Image = image;
            OGName = oGName;
            AdminName = adminName;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Password = password;
        }

        public OrganizationImageSource()
        {
        }
    }
}
