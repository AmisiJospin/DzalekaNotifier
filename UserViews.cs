using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DzalekaNotifierFinal
{
    public class UserViews
    {
        public string Username { get; set; }
        public string NoticeTitle { get; set; }

        public UserViews(string username, string notice)
        {
            Username = username;
            NoticeTitle = notice;
        }

        public UserViews()
        {
        }
    }
}
