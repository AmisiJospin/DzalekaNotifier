using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DzalekaNotifierFinal
{
    public class Organizations
    {

        // [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string ImageID { get; set; }
        public string Image { get; set; }
        public string OGName { get; set; }
        public string AdminName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Information { get; set; }
        public string Activity { get; set; }
        public string Location { get; set; }

        public Organizations(string imageID, string image, string oGName, string adminName, string userName, string email, string phoneNumber, string address, string password, string information, string activity, string location)
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
            Information = information;
            Activity = activity;
            Location = location;
        }

        public Organizations()
        {
        }
    }
}
