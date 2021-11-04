using ImageCircle.Forms.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DzalekaNotifierFinal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllUsers : ContentPage
    {
        FireBaseDataManagement<Users> fireBaseData = new FireBaseDataManagement<Users>();
        Assembly assembly = typeof(AllUsers).GetTypeInfo().Assembly;
        ObservableCollection<Users> usersCollection = new ObservableCollection<Users>();
        ObservableCollection<UserImageSource> usersImageCollection = new ObservableCollection<UserImageSource>();

        public AllUsers()
        {
            InitializeComponent();

            backgroundImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);
            LoadAllUsersMethod();
        }
        

        List<Users> valuesList = new List<Users>();
        List<String> keyslist = new List<string>();
        ImageManagement imageManagement = new ImageManagement();

        public async void LoadAllUsersMethod()
        {
            App.isNetworkAvailable = false;
            loadingImage.IsVisible = true;
            loadingImage.IsRunning = true;

            try
            {
                
                Dictionary<String, Users> dictionary = await fireBaseData.GetAllUsers("DN_Users");
                valuesList = dictionary.Select(V => V.Value).ToList();
                keyslist = dictionary.Select(K => K.Key).ToList();

                foreach (var item in valuesList)
                {
                    Frame frame = new Frame();
                    frame.OutlineColor = Color.White;
                    frame.CornerRadius = 10;
                    frame.Margin = new Thickness(2);

                    StackLayout stack = new StackLayout();
                    stack.Orientation = StackOrientation.Horizontal;

                    stack.Children.Add(new CircleImage()
                    {
                        Source = imageManagement.ByteArrayToImageSource(item.ImageUser),
                        WidthRequest = 70,
                        HeightRequest = 70
                    });

                    StackLayout stack2 = new StackLayout();

                    stack2.Children.Add(new Label()
                    {
                        Text = DataConverter.ConvertToString(item.Name),
                        TextColor = Color.FromHex("#4c466c"),
                        FontAttributes = FontAttributes.Bold,
                        Margin = new Thickness(0, 15, 0, 0),
                        FontSize = 20
                    });


                    stack.Children.Add(stack2);
                    frame.Content = stack;
                    contentHolder.Children.Add(frame);


                }
            }
            catch (Exception)
            {
                

                if (!App.isNetworkAvailable)
                {
                    await DisplayAlert("Oops!", "Network Error!", "Close");
                    Navigation.RemovePage(this);
                }

            }
            loadingImage.IsRunning = false;
            loadingImage.IsVisible = false;
        }

        private void allUsersGrid_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}