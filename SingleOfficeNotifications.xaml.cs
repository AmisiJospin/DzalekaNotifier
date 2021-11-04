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
    public partial class SingleOfficeNotifications : ContentPage
    {
        public static string orgName;
        public static string newsSuggestion = "";
        string titleText;

        FireBaseDataManagement<Notifications> firebaseNotifications = new FireBaseDataManagement<Notifications>();
        Assembly assembly = typeof(SingleOfficeNotifications).GetTypeInfo().Assembly;
        ObservableCollection<Notifications> notificationsCollection = new ObservableCollection<Notifications>();

        ObservableCollection<NotificationImageSource> notificationsWithImageCollection = new ObservableCollection<NotificationImageSource>();

        FireBaseDataManagement<UserViews> FireBaseData = new FireBaseDataManagement<UserViews>();

        public SingleOfficeNotifications(string organizationName)
        {
            InitializeComponent();

            orgName = organizationName;
            backgroundImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);
            LoadNotifications();
        }
        
        
        ImageManagement imageManagement = new ImageManagement();
        List<Notifications> valueList = new List<Notifications>();
        List<String> keyList = new List<string>();
        string localimage;
        public async void LoadNotifications()
        {
            Dictionary<String, Notifications> dictionary = await firebaseNotifications.GetAllUsers("DN_Notifications");
            App.isNetworkAvailable = false;
            loadingImage.IsRunning = true;
            loadingImage.IsVisible = true;
            valueList.Clear();
            try
            {
                if (dictionary != null)
                {
                    valueList = dictionary.Select(V => V.Value).ToList();
                    keyList = dictionary.Select(K => K.Key).ToList();
                   
                    foreach (var item in valueList)
                    {
                        if (orgName.Equals(DataConverter.ConvertToString(item.Organization)))
                        {
                            Frame frame = new Frame();
                            frame.OutlineColor = Color.White;
                            frame.CornerRadius = 10;
                            frame.Margin = new Thickness(5);

                            StackLayout stack = new StackLayout();
                            stack.Orientation = StackOrientation.Horizontal;

                            stack.Children.Add(new Image()
                            {
                                Source = imageManagement.ByteArrayToImageSource(item.OrgImage),

                                WidthRequest = 70,
                                HeightRequest = 70
                            });
                            
                            StackLayout stack2 = new StackLayout();

                            stack2.Children.Add(new Label()
                            {
                                Text = DataConverter.ConvertToString(item.Title),
                                TextColor = Color.FromHex("#4c466c"),
                                FontSize = 20
                            });

                            stack2.Children.Add(new Label()
                            {
                                Text = DataConverter.ConvertToString(item.Date),
                                TextColor = Color.FromHex("#4c466c"),
                                FontSize = 12
                            });

                            stack2.Children.Add(new Label()
                            {
                                Text = DataConverter.ConvertToString(item.Organization),
                                TextColor = Color.FromHex("#4c466c"),
                                FontSize = 0.000001,
                                Opacity = 0

                            });

                            stack2.Children.Add(new Label()
                            {
                                Text = DataConverter.ConvertToString(item.Category),
                                FontSize = 0.000001,
                                Opacity = 0
                            });

                            stack2.Children.Add(new Label()
                            {
                                Text = DataConverter.ConvertToString(item.Content),
                                FontSize = 0.000001,
                                Opacity = 0
                            });

                            stack2.Children.Add(new Image()
                            {
                                Source = imageManagement.ByteArrayToImageSource(item.Image),
                                WidthRequest = 0.000001,
                                HeightRequest = 0.000001,
                                Opacity = 0
                            });
                            localimage = item.Image;
                            stack.Children.Add(stack2);

                            frame.Content = stack;

                            containerView.Children.Add(frame);

                            frame.GestureRecognizers.Add(new TapGestureRecognizer()
                            {
                                Command = new Command(() =>
                                {
                                    ITEM_CLICKED(frame);
                                })

                            });
                        }
                    

                    }
                }
            }
            catch(Exception e)
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

        private void ITEM_CLICKED(Frame sender)
        {
            Frame motherHolder = sender;
            StackLayout stack = (StackLayout)motherHolder.Content;

            Image image = (Image)stack.Children[0];

            StackLayout stack1 = (StackLayout)stack.Children[1];
            Label meTitle = (Label)stack1.Children[0];
            Label meDate = (Label)stack1.Children[1];
            Label meOrganization = (Label)stack1.Children[2];
            Label meCategory = (Label)stack1.Children[3];
            Label meContent = (Label)stack1.Children[4];
            Image meImage = (Image)stack1.Children[5];

            NewDetails.title = meTitle.Text.ToString();
            NewDetails.date = meDate.Text.ToString();
            NewDetails.content = meContent.Text.ToString();
            NewDetails.stringImage = localimage;
            NewDetails.newsSuggestion = newsSuggestion;

            Navigation.PushAsync(new NewDetails());
        }
    }
}
