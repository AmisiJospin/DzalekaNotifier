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
    public partial class Feedbacks : ContentPage
    {
        FireBaseDataManagement<Feedback> firebaseFeedback = new FireBaseDataManagement<Feedback>();
        Assembly assembly = typeof(Feedbacks).GetTypeInfo().Assembly;

        public Feedbacks()
        {
            InitializeComponent();

            backgroundImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.OtherImages.Back.png", assembly);
        }

        private void AddNoticeButton_Clicked(object sender, EventArgs e)
        {
            loadingImage.IsVisible = true;
            AddNewFeedBack();
            Navigation.RemovePage(this);
        }

        public async void AddNewFeedBack()
        {
            loadingImage.IsVisible = true;
            AddNoticeButton.IsVisible = false;


            if (messageBox.Text.Length > 10)
            {
                Feedback feedback = new Feedback(App.currentUser, App.currentAddress, App.currentPhoneNumber, messageBox.Text);
                Task task = Task.Run(() =>
                {
                    firebaseFeedback.AddNewUser(feedback, "DN_Feedbacks");
                });
                await Task.WhenAll();

                loadingImage.IsVisible = false;
                await DisplayAlert("Done!", "Feedback sent!", "Close");
            }
            else
            {
                await DisplayAlert("Error!", "The Lenght of your message is too short", "Close");
            }
        }
    }
}