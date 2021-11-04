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
    public partial class Profile : ContentPage
    {
        Assembly assembly = typeof(Profile).GetTypeInfo().Assembly;

        FireBaseDataManagement<Users> firebaseUser = new FireBaseDataManagement<Users>();
        ImageManagement imageManagement = new ImageManagement();

        public Profile()
        {
            InitializeComponent();

            backgroundImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);
            // fullImage.Source = ImageSource.FromResource("DzalekaNotifier.OtherImages.SplashNotice.png", assembly);

            profileImage.Source = imageManagement.ByteArrayToImageSource(App.currentImage);
            name.Text = App.currentUser;
            Location.Text = App.currentAddress;
            Email.Text = App.currentEmail;
            PhoneNumber.Text = App.currentPhoneNumber;

        }

     

        private async void savePasswordButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                Loader.IsVisible = true;
                Dictionary<String, Users> dictionary = await firebaseUser.GetAllUsers("DN_Users");

                List<string> keys = new List<string>();
                List<Users> values = new List<Users>();
                keys = dictionary.Select(m => m.Key).ToList();
                values = dictionary.Select(m => m.Value).ToList();
                int selectedId =  values.FindIndex(m => m.PhoneNumber.Equals(DataConverter.ConvertToByteArray(App.currentPhoneNumber)));

                foreach (var userData in values)
                {
                    if (DataConverter.ConvertToString(userData.Email).Equals(App.currentEmail))
                    {
                        if (DataConverter.ConvertToString(userData.Password).Equals(oldPasswordEntry.Text))
                        {
                            if (newPasswordEntry.Text.Equals(confirmPasswordEntry.Text))
                            {
                                Users user = new Users();
                                user.ImageUser = App.currentImage;
                                user.Name = DataConverter.ConvertToByteArray(App.currentUser);
                                user.UserName = DataConverter.ConvertToByteArray(App.currentUserName);
                                user.Email = DataConverter.ConvertToByteArray(App.currentEmail);
                                user.PhoneNumber = DataConverter.ConvertToByteArray(App.currentPhoneNumber);
                                user.Address = DataConverter.ConvertToByteArray(App.currentAddress);
                                user.Password = DataConverter.ConvertToByteArray(newPasswordEntry.Text);

                               
                                    firebaseUser.ResetPassword(user, "DN_Users\\"+keys[selectedId]);
                            }
                            else
                            {
                                await DisplayAlert("Error", "Your Password doesn't match", "Close");
                            }
                        }
                        else
                        {
                            await DisplayAlert("Error", "Your Password doesn't match with the old one", "Close");
                        }
                    }
                }
                Loader.IsVisible = false;
                resetLabel.IsVisible = true;
                resetPasswordPanel.IsVisible = false;
            }
            catch (Exception)
            {
                await DisplayAlert("Network Error", "Check Your internet Connection", "Retry");
            }
        }

        private void reset_Tapped(object sender, EventArgs e)
        {
            resetLabel.IsVisible = false;
            resetPasswordPanel.IsVisible = true;
        }
    }
}