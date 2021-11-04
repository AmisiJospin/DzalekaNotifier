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
	public partial class ForgotPassword : ContentPage
	{
		public ForgotPassword ()
		{
			InitializeComponent ();
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            backgroungImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);
            icon.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.lock.png", assembly);

        }
       public static string newPassword;
        public static string PhoneNumber;
        public static string email;
        public static string imageUser;
        public static string Name;
        public static string UserName;
        public static string Address;
        public static string question1;
        public static string question2;
        public static string question3;
        FireBaseDataManagement<Users> firebase = new FireBaseDataManagement<Users>();
        Dictionary<String, Users> dictionary;
        List<String> keylist = new List<string>();
        List<Users> values = new List<Users>();
        public async void Update()
        {
            dictionary = await firebase.GetAllUsers("DN_Users");
            keylist = dictionary.Select(K => K.Key).ToList();
            values = dictionary.Select(K => K.Value).ToList();

            int selectedId = values.FindIndex(m => m.Email.Equals(DataConverter.ConvertToByteArray(email)));
            foreach (var userData in values)
            {
                if (DataConverter.ConvertToString(userData.Email).Equals(email))
                {
                    Users user = new Users();
                    user.ImageUser = imageUser;
                    user.Name = DataConverter.ConvertToByteArray(Name);
                    user.UserName = DataConverter.ConvertToByteArray(UserName);
                    user.Email = DataConverter.ConvertToByteArray(email);
                    user.PhoneNumber = DataConverter.ConvertToByteArray(PhoneNumber);
                    user.Address = DataConverter.ConvertToByteArray(Address);
                    user.Password = DataConverter.ConvertToByteArray(password.Text);
                    user.question1 = DataConverter.ConvertToByteArray(question1);
                    user.question2 = DataConverter.ConvertToByteArray(question2);
                    user.question3 = DataConverter.ConvertToByteArray(question3);
                    firebase.ResetPassword(user, "DN_Users\\" + keylist[selectedId]);
                }
            }
        }

        private void Submit_Clicked(object sender, EventArgs e)
        {
            if (password.Text.Length>7&& confirmpassword.Text.Length>7)
            {
                if (confirmpassword.Text.Equals(password.Text))
                {
                    Update();
                    Navigation.PushAsync(new Login());
                }
                else
                {
                    DisplayAlert("Error", "Your Password And Confirm Password Are Different!","Ok");
                }
            }
            else
            {
                DisplayAlert("Error", "Your Password is Less Than 8 Characters!", "Ok");
            }
        }
    }
}