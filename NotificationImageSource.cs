using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DzalekaNotifierFinal
{
    public class NotificationImageSource
    {
        public int ID { get; set; }

        public string Organization { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
        public ImageSource Image { get; set; }
        public ImageSource OrgImage { get; set; }
        public ImageSource File { get; set; }

        public NotificationImageSource(string organization, string title, string category, string content, string date, ImageSource image, ImageSource orgImage)
        {
            Organization = organization;
            Title = title;
            Category = category;
            Content = content;
            Date = date;
            Image = image;
            OrgImage = orgImage;
        }

        public NotificationImageSource()
        {
        }
    }
}
