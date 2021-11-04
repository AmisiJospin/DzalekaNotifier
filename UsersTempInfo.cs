using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DzalekaNotifierFinal
{
    public class UsersTempInfo
    {
        public static String name;
        public static String userName;
        public static String Location;
        public static String email;
        public static String phoneNumber;
        public static String adress;
        public static String password;
        public static String image;
        public static String idUser;
        public static String quest1;
        public static String quest2;
        public static String quest3;



        public static string GenerateUserCode()
        {
            Random random = new Random();
            int number = random.Next(50, 60);
            int numberTwo = random.Next(20, 40);
            int numberThree = random.Next(10, 30);

            string codeGet = String.Concat(number, numberTwo, numberThree);

            return codeGet;



        }
    }
}
