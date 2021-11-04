using ImageCircle.Forms.Plugin.Abstractions;
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
    public partial class NewsChoice : ContentPage
    {
        Assembly assembly = typeof(NewsChoice).GetTypeInfo().Assembly;

        public NewsChoice()
        {
            InitializeComponent();
            LoadingRecent.IsVisible = true;
            LoadingRecent.IsRunning = true;
            NameWeb.Source = "https://www.nytimes.com/section/world/africa#stream-panel";
            ImagesLoading();
            LoadingRecent.IsVisible = false;
            LoadingRecent.IsRunning = false;
            // LoadRecentNotification();

        }
        FireBaseDataManagement<Notifications> fireBaseDataManagement = new FireBaseDataManagement<Notifications>();
        List<Notifications> valuelist = new List<Notifications>();
        List<String> keyList = new List<string>();
        //public async void LoadRecentNotification()
        //{
        //    LoadingRecent.IsVisible = true;
        //    LoadingRecent.IsRunning = true;
        //    try
        //    {
        //        Dictionary<String, Notifications> dictionary = await fireBaseDataManagement.GetAllUsers("DN_Notifications");
        //        if (dictionary != null)
        //        {
        //            valuelist = dictionary.Select(V => V.Value).ToList();
        //            keyList = dictionary.Select(K => K.Key).ToList();
        //            foreach (var item in valuelist)
        //            {
        //                TimeSpan time = DateTime.Now.Subtract(DateTime.Parse(DataConverter.ConvertToString(item.Date)));
        //                if (time.TotalDays <= 2)
        //                {
        //                    Frame frame = new Frame();
        //                    frame.OutlineColor = Color.FromHex("#4c466c");
        //                    frame.CornerRadius = 10;
        //                    frame.Margin = new Thickness(5);

        //                    StackLayout stack = new StackLayout();
        //                    stack.Orientation = StackOrientation.Horizontal;

        //                    ImageManagement image = new ImageManagement();

        //                    stack.Children.Add(new CircleImage()
        //                    {
        //                        Source = image.ByteArrayToImageSource(item.OrgImage),
        //                        HeightRequest = 70,
        //                        WidthRequest = 70,


        //                    });

        //                    StackLayout stack2 = new StackLayout();

        //                    stack2.Children.Add(new Label()
        //                    {
        //                        Text = DataConverter.ConvertToString(item.Title),
        //                        TextColor = Color.FromHex("#4c466c"),
        //                        FontSize = 20
        //                    });
        //                    stack2.Children.Add(new Label()
        //                    {
        //                        Text = DataConverter.ConvertToString(item.Content),
        //                        FontSize = 12,
        //                        TextColor = Color.FromHex("#4c466c"),

        //                    });

        //                    stack2.Children.Add(new Label()
        //                    {
        //                        Text = DataConverter.ConvertToString(item.Date),
        //                        TextColor = Color.FromHex("#4c466c"),
        //                        FontSize = 12
        //                    });

        //                    stack2.Children.Add(new Label()
        //                    {
        //                        Text = DataConverter.ConvertToString(item.Organization),
        //                        TextColor = Color.FromHex("#4c466c"),
        //                        FontSize = 0.000001,
        //                        Opacity = 0

        //                    });

        //                    stack2.Children.Add(new Label()
        //                    {
        //                        Text = DataConverter.ConvertToString(item.Category),
        //                        FontSize = 0.000001,
        //                        Opacity = 0
        //                    });

                          

        //                    stack2.Children.Add(new Image()
        //                    {
        //                        Source = item.Image,
        //                        WidthRequest = 0.000001,
        //                        HeightRequest = 0.000001,
        //                        Opacity = 0
        //                    });
                           
        //                    stack.Children.Add(stack2);

        //                    frame.Content = stack;

        //                    stackHolder.Children.Add(frame);

                            
        //                }


        //            }

        //        }
        //        else
        //        {
        //            LoadingRecent.IsVisible = false;
        //            await DisplayAlert("No Data", "No Recent News Has Been Published", "Colse");
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        await DisplayAlert("Network", "Check Your internet Connection", "Close");
        //    }



        //    LoadingRecent.IsVisible = false;

        //}

        public void ImagesLoading()
        {
            backgroundImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);
            educationImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.OtherImages.Education.png", assembly);
            healthImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.OtherImages.Health.png", assembly);
            artImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.OtherImages.Art.png", assembly);
            securityImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.OtherImages.Security.png", assembly);
            igaImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.OtherImages.IGA.png", assembly);
            communicationImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.OtherImages.Communication.png", assembly);
        }

        private async void education_Clicked(object sender, EventArgs e)
        {
            App.newsType = "Education";
            await Navigation.PushAsync(new NewsClicked("Education"));
        }

        private async void health_Clicked(object sender, EventArgs e)
        {
            App.newsType = "Health And Sanitation";
            NewsClicked.activityType = "Health And Sanitation";
            await Navigation.PushAsync(new NewsClicked("Health And Sanitation"));
        }

        private async void art_Clicked(object sender, EventArgs e)
        {
            App.newsType = "Art and Entertainment";
            NewsClicked.activityType = "Art and Entertainment";
            await Navigation.PushAsync(new NewsClicked("Art and Entertainment"));
        }

        private async void security_Clicked(object sender, EventArgs e)
        {
            App.newsType = "Security";
            NewsClicked.activityType = "Security";
            await Navigation.PushAsync(new NewsClicked("Security"));
        }

        private async void iga_Clicked(object sender, EventArgs e)
        {
            App.newsType = "Income Generating Activities";
            NewsClicked.activityType = "Income Generating Activities";
            await Navigation.PushAsync(new NewsClicked("Income Generating Activities"));
        }

        private async void cwc_Clicked(object sender, EventArgs e)
        {
            App.newsType = "Communication With Communities";
            NewsClicked.activityType = "Communication With Communities";
            await Navigation.PushAsync(new NewsClicked("Communication With Communities"));
        }
    }
}
