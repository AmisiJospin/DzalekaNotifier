using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DzalekaNotifierFinal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HelpUser : ContentPage
    {
        Assembly assembly = typeof(HelpUser).GetTypeInfo().Assembly;

        public HelpUser()
        {
            InitializeComponent();

            backgroundImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);
        }
    }
}