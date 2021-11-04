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
    public partial class VerificationNumber : ContentPage
    {
        FireBaseDataManagement<Users> fireBaseUsers = new FireBaseDataManagement<Users>();

        public VerificationNumber()
        {
            InitializeComponent();
            LoadAllUsersTemp();

            var assembly = typeof(VerificationNumber).GetTypeInfo().Assembly;
            backgroundImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);
            messageLockImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.OtherImages.verification.png", assembly);

            //if(LoadDataOffline.AllUsersTempDictionary != null)
            //{
            //    foreach (var userTemp in LoadDataOffline.AllUsersTempDictionary)
            //    {
            //        
            //    }
            //}
            VerifyNumber.Text = SendSmsToUsers.codeSaved;

        }

        /* ALL USERS IN TEMPO DATABASE */
        public static Dictionary<String, Users> AllUsersTempDictionary;
        public static async void LoadAllUsersTemp()
        {
            FireBaseDataManagement<Users> UserDatas = new FireBaseDataManagement<Users>();
            List<Users> allUsersValues = new List<Users>();
            List<String> allUsersKeys = new List<string>();
            AllUsersTempDictionary = await UserDatas.GetAllUsers("DN_Users_Temp");
            if (AllUsersTempDictionary != null)
            {
                allUsersKeys = AllUsersTempDictionary.Select(m => m.Key).ToList();
                allUsersValues = AllUsersTempDictionary.Select(k => k.Value).ToList();
            }
        }

        private async void submitBtn_Clicked(object sender, EventArgs e)
        {
            submitBtn.IsVisible = false;
            resendCode.IsVisible = false;
            loadingImage.IsVisible = true;
            loadingImage.IsRunning = true;

            try
            {
                foreach (var userTemp in AllUsersTempDictionary)
                {
                    if (VerifyNumber.Text != null)
                    {
                        if (VerifyNumber.Text.ToString().Trim().Equals(DataConverter.ConvertToString(userTemp.Value.CodeUser)))
                        {
                            App.currentUser = DataConverter.ConvertToString(UsersTempInfo.name);
                            App.currentPhoneNumber = DataConverter.ConvertToString(UsersTempInfo.phoneNumber);
                            App.currentEmail = DataConverter.ConvertToString(UsersTempInfo.email);
                            App.currentImage = UsersTempInfo.image;
                            App.currentAddress = DataConverter.ConvertToString(UsersTempInfo.adress);
                            App.userOrOrganization = "User";
                            HomePage.logedIn = true;

                            Users users = new Users(UsersTempInfo.image, UsersTempInfo.name, UsersTempInfo.userName, UsersTempInfo.email, UsersTempInfo.phoneNumber, UsersTempInfo.adress, UsersTempInfo.password, UsersTempInfo.quest1, UsersTempInfo.quest2, UsersTempInfo.quest3);

                            fireBaseUsers.AddNewUser(users, "DN_Users");

                            fireBaseUsers.DeleteNotifier(UsersTempInfo.idUser, "DN_Users_Temp");

                            await Navigation.PushAsync(new HomePage());

                            break;
                        }
                       
                    }
                    else
                    {
                        await DisplayAlert("Error!", "Wrong Code", "Close");
                    }


                }

                SendSmsToUsers.SendSMSMessage(App.currentPhoneNumber, "Hello " + App.currentUser + ", Congratulations! You are now registered to Dzaleka Notifier.");
                await DisplayAlert("Done!", App.currentUser + " is registered!", "OK");
            }
            catch (Exception)
            {
              
            }

           

            submitBtn.IsVisible = true;
            resendCode.IsVisible = true;
            loadingImage.IsVisible = false;
            loadingImage.IsRunning = false;

        }

        private void resendCode_Clicked(object sender, EventArgs e)
        {
            submitBtn.IsVisible = false;
            resendCode.IsVisible = false;
            loadingImage.IsVisible = true;
            loadingImage.IsRunning = true;

            try
            {
                string code = UsersTempInfo.GenerateUserCode();
                SendSmsToUsers.codeSaved = code;

                fireBaseUsers.UpdateUser("DN_Users_Temp", UsersTempInfo.idUser,
                    new Users()
                    {
                        Email = DataConverter.ConvertToByteArray(UsersTempInfo.email),
                        Name = DataConverter.ConvertToByteArray(UsersTempInfo.name),
                        ImageUser = UsersTempInfo.image,
                        Password = DataConverter.ConvertToByteArray(UsersTempInfo.password),
                        PhoneNumber = DataConverter.ConvertToByteArray(UsersTempInfo.phoneNumber),
                        Address = DataConverter.ConvertToByteArray(UsersTempInfo.adress),
                        UserName = DataConverter.ConvertToByteArray(UsersTempInfo.userName),
                        question1 = DataConverter.ConvertToByteArray(UsersTempInfo.quest1),
                        question2 = DataConverter.ConvertToByteArray(UsersTempInfo.quest2),
                        question3 = DataConverter.ConvertToByteArray(UsersTempInfo.quest3),

                        CodeUser = DataConverter.ConvertToByteArray(code)
                    });

                SendSmsToUsers.SendSMSMessage(UsersTempInfo.phoneNumber, "Your confirmation code for Dzaleka Notifier is " + code);
            }
            catch (Exception)
            {

            }

            submitBtn.IsVisible = true;
            resendCode.IsVisible = true;
            loadingImage.IsVisible = false;
            loadingImage.IsRunning = false;

        }
    }
}
