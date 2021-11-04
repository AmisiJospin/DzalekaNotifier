using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DzalekaNotifierFinal
{
    public class QuestionsProperties
    {
        public string Questions { get; set; }
        public string Date { get; set; }
        public string PhoneNumber { get; set; }


        public QuestionsProperties(string questions, string date, string phoneNumber)
        {
            Questions = questions;
            Date = date;
            PhoneNumber = phoneNumber;
        }

        public QuestionsProperties()
        {
        }
    }
}
