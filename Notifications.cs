using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DzalekaNotifierFinal
{
    public class Notifications
    {
        // [PrimaryKey, AutoIncrement]
        //Create properties for sending notification

        public int ID { get; set; }

        public string Organization { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
        public string Image { get; set; }
        public string OrgImage { get; set; }
        public string File { get; set; }

        public Notifications(string organization, string title, string category, string content, string date, string image, string orgImage)
        {
            Organization = organization;
            Title = title;
            Category = category;
            Content = content;
            Date = date;
            Image = image;
            OrgImage = orgImage;
        }



        public Notifications(string organization, string title, string category, string content, string date, string image, string orgImage,string files)
        {
            Organization = organization;
            Title = title;
            Category = category;
            Content = content;
            Date = date;
            Image = image;
            OrgImage = orgImage;
        }

        public Notifications()
        {

        }
    }
}
