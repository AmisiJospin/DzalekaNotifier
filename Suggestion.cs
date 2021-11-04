using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DzalekaNotifierFinal
{
    public class Suggestion
    {
        // [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Sender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string NoticeContent { get; set; }
        public string Date { get; set; }

        public Suggestion(string sender, string phoneNumber, string address, string content, string title, string noticeContent, string date)
        {
            Sender = sender;
            PhoneNumber = phoneNumber;
            Address = address;
            Content = content;
            Title = title;
            NoticeContent = noticeContent;
            Date = date;
        }

        public Suggestion()
        {
        }
    }
}
