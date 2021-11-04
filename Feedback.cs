using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DzalekaNotifierFinal
{
    public class Feedback
    {

        public int ID { get; set; }

        public string Sender { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Message { get; set; }

        public Feedback(string sender, string address, string phone, string message)
        {
            Sender = sender;
            Address = address;
            PhoneNumber = phone;
            Message = message;
        }

        public Feedback()
        {

        }
    }
}
