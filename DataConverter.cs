using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DzalekaNotifierFinal
{
    public class DataConverter
    {
        public static string ConvertToString(string byteArray)
        {
            var Data = Convert.FromBase64String(byteArray);
            byteArray = Encoding.UTF8.GetString(Data, 0, Data.Length);

            return byteArray;
        }

        public static string ConvertToByteArray(string inputString)
        {

            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            byte[] bytes = encoding.GetBytes(inputString);
            inputString = Convert.ToBase64String(bytes, 0, bytes.Length);


            return inputString;
        }

    }
}
