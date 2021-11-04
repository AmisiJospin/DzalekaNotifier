using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DzalekaNotifierFinal
{
    public class UserLikes
    {
        public string Username { get; set; }
        public string NoticeTitle { get; set; }

        public UserLikes(string username, string notice)
        {
            Username = username;
            NoticeTitle = notice;
        }

        public UserLikes()
        {
        }
    }
}
