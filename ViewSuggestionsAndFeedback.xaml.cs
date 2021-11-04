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
    public partial class ViewSuggestionsAndFeedback : ContentPage
    {
        public static string senderName, senderPhoneNumber, senderAddress, senderMessage;

        Assembly assembly = typeof(ViewSuggestionsAndFeedback).GetTypeInfo().Assembly;

        public ViewSuggestionsAndFeedback()
        {
            InitializeComponent();

            backgroundImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.OtherImages.Back.png", assembly);

            Viewsuggestiondetials();
        }

        private void Viewsuggestiondetials()
        {
            UserName.Text = senderName;
            PhoneNumber.Text = senderPhoneNumber;
            UserAddress.Text = senderAddress;
            SenderMessage.Text = senderMessage;
        }
    }
}