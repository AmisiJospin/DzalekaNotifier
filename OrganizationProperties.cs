using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DzalekaNotifierFinal
{
    public class OrganizationProperties
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

        public string InfoSummary { get; set; }

        public List<Temp_String_Services_Class> orgServices { get; set; }

        public string MoreBullets { get; set; }

        public string HowToAccess { get; set; }
       
        public string ContactServices { get; set; }
       

        public OrganizationProperties(string imageID, string image, string oGName, string adminName, string userName, string email, string phoneNumber, string address, string password,string infoSummary,List<Temp_String_Services_Class> orgservice,string howToAccess,string contactServises )
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
            InfoSummary = infoSummary;
            orgServices = orgservice;
            HowToAccess = howToAccess;
            ContactServices = contactServises;

        }

        public OrganizationProperties(string imageID, string image, string oGName, string adminName, string userName, string email, string phoneNumber, string address, string password, string infoSummary, List<Temp_String_Services_Class> orgservice,string morebullets, string howToAccess, string contactServises)
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
            InfoSummary = infoSummary;
            orgServices = orgservice;
            MoreBullets = morebullets;
            HowToAccess = howToAccess;
            ContactServices = contactServises;

        }

        public OrganizationProperties()
        {

        }
    }
}
