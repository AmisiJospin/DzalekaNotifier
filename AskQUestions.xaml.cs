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
	public partial class AskQUestions : ContentPage
	{
		public AskQUestions ()
		{
			InitializeComponent ();
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            backgroungImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);
            icon.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.lock.png", assembly);

        }
        FireBaseDataManagement<Users> firebase = new FireBaseDataManagement<Users>();
        List<Users> loadValues = new List<Users>();
        List<String> loadKeys = new List<string>();
        Dictionary<String, Users> dictionary;

        public async void Answers()
        {

            dictionary = await firebase.GetAllUsers("DN_Users");
            if(!(dictionary== null))
            {
                loadKeys = dictionary.Select(K => K.Key).ToList();
                loadValues = dictionary.Select(V => V.Value).ToList();
                foreach (var item in loadValues)
                {


                    if (email.Text.Contains("@"))
                    {
                        if (DataConverter.ConvertToString( item.Email).Equals(email.Text))
                        {
                            if (DataConverter.ConvertToString(item.question1).Equals(question1.Text))
                            {
                                if (DataConverter.ConvertToString(item.question2).Equals(question2.Text))
                                {
                                    if (DataConverter.ConvertToString(item.question3).Equals(question3.Text))
                                    {
                                        ForgotPassword.email = DataConverter.ConvertToString(item.Email);
                                        ForgotPassword.PhoneNumber = DataConverter.ConvertToString(item.PhoneNumber);
                                        ForgotPassword.UserName = DataConverter.ConvertToString(item.UserName);
                                        ForgotPassword.imageUser = item.ImageUser;
                                        ForgotPassword.Name = DataConverter.ConvertToString(item.Name);
                                        ForgotPassword.question1 = DataConverter.ConvertToString(item.question1);
                                        ForgotPassword.question2 = DataConverter.ConvertToString(item.question2);
                                        ForgotPassword.question3 = DataConverter.ConvertToString(item.question3);
                                        ForgotPassword.Address = DataConverter.ConvertToString(item.Address);
                                        await Navigation.PushAsync(new ForgotPassword());
                                        break;
                                    }
                                    else
                                    {
                                        await DisplayAlert("Invalid", "Check your boxes", "Ok");
                                    }
                                }
                                else
                                {
                                    await DisplayAlert("Invalid", "Check your boxes", "Ok");
                                }
                            }
                            else
                            {
                                await DisplayAlert("Invalid", "Check your boxes", "Ok");
                            }

                        }
                        else
                        {
                            await DisplayAlert("incorrect Email", "Check Your Email Address Name", "Ok");
                        } 
                    }
                    else
                    {
                        await DisplayAlert("Invalid Email", "Check your Email Format", "Ok");
                    }
               }
            }
           

        }

        private void Confirm_Clicked(object sender, EventArgs e)
        {
            Answers();
        }
    }
}