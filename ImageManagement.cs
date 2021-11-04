using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DzalekaNotifierFinal
{
    class ImageManagement
    {
        public byte[] convertBackToByte(string ImageinByte)
        {
            return System.Convert.FromBase64String(ImageinByte);
        }
        public ImageSource ByteArrayToImageSource(string RealImage)
        {
            try
            {
                MemoryStream ms = new MemoryStream(convertBackToByte(RealImage));
                StreamReader msa = new StreamReader(ms);
                Stream go = msa.BaseStream;
                return ImageSource.FromStream(() => go);
            }

            catch (Exception)
            {
                return null;
            }


        }
    }
}
