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
    public partial class NewsClicked : ContentPage
    {
        FireBaseDataManagement<Notifications> firebaseNotifications = new FireBaseDataManagement<Notifications>();
        Assembly assembly = typeof(NewsClicked).GetTypeInfo().Assembly;

        ObservableCollection<Notifications> educationCollection = new ObservableCollection<Notifications>();
        ObservableCollection<Notifications> healthCollection = new ObservableCollection<Notifications>();
        ObservableCollection<Notifications> securityCollection = new ObservableCollection<Notifications>();
        ObservableCollection<Notifications> incomeCollection = new ObservableCollection<Notifications>();
        ObservableCollection<Notifications> artCollection = new ObservableCollection<Notifications>();
        ObservableCollection<Notifications> communicationCollection = new ObservableCollection<Notifications>();

        ObservableCollection<NotificationImageSource> educationImageCollection = new ObservableCollection<NotificationImageSource>();
        ObservableCollection<NotificationImageSource> healthImageCollection = new ObservableCollection<NotificationImageSource>();
        ObservableCollection<NotificationImageSource> securityImageCollection = new ObservableCollection<NotificationImageSource>();
        ObservableCollection<NotificationImageSource> incomeImageCollection = new ObservableCollection<NotificationImageSource>();
        ObservableCollection<NotificationImageSource> artImageCollection = new ObservableCollection<NotificationImageSource>();
        ObservableCollection<NotificationImageSource> communicationImageCollection = new ObservableCollection<NotificationImageSource>();

        public static string activityType = "";


        public static string newsSuggestions;

        public NewsClicked(string newsOrSuggestions)
        {
            var assembly = typeof(NewsClicked).GetTypeInfo().Assembly;
            this.Title = App.newsType;

            InitializeComponent();


            newsSuggestions = newsOrSuggestions;
            backgroundImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);

            LoadingNews();
        }




        public async void LoadingNews()
        {
            App.isNetworkAvailable = false;
            loadingImage.IsRunning = true;
            loadingImage.IsVisible = true;

            contentHolder.Children.Clear();

            educationImageCollection.Clear();
            artImageCollection.Clear();
            healthImageCollection.Clear();
            communicationImageCollection.Clear();
            securityImageCollection.Clear();
            incomeImageCollection.Clear();


            educationCollection.Clear();
            artCollection.Clear();
            healthCollection.Clear();
            communicationCollection.Clear();
            incomeCollection.Clear();
            securityCollection.Clear();



            try
            {
                Task task = Task.Run(async () =>
                {
                    foreach (var oneNews in await firebaseNotifications.GetAllUsers("DN_Notifications"))
                    {

                        if (DataConverter.ConvertToString(oneNews.Value.Category).Equals("Education"))
                        {
                            educationCollection.Insert(0, oneNews.Value);
                        }
                        else if (DataConverter.ConvertToString(oneNews.Value.Category).Equals("Health And Sanitation"))
                        {
                            healthCollection.Insert(0, oneNews.Value);
                        }
                        else if (DataConverter.ConvertToString(oneNews.Value.Category).Equals("Security"))
                        {
                            securityCollection.Insert(0, oneNews.Value);

                        }
                        else if (DataConverter.ConvertToString(oneNews.Value.Category).Equals("Income Generating Activities"))
                        {
                            incomeCollection.Insert(0, oneNews.Value);

                        }
                        else if (DataConverter.ConvertToString(oneNews.Value.Category).Equals("Art And Entertainment"))
                        {
                            artCollection.Insert(0, oneNews.Value);

                        }
                        else if (DataConverter.ConvertToString(oneNews.Value.Category).Equals("Communication With Communities"))
                        {
                            communicationCollection.Insert(0, oneNews.Value);

                        }
                    }
                });
                await Task.WhenAll(task);


                foreach (var oneNews in educationCollection)
                {
                    ImageManagement imageManagement = new ImageManagement();
                    educationImageCollection.Insert(0, new NotificationImageSource(DataConverter.ConvertToString(oneNews.Organization), DataConverter.ConvertToString(oneNews.Title), DataConverter.ConvertToString(oneNews.Category), DataConverter.ConvertToString(oneNews.Content), DataConverter.ConvertToString(oneNews.Date), imageManagement.ByteArrayToImageSource(oneNews.Image), imageManagement.ByteArrayToImageSource(oneNews.OrgImage)));
                }

                foreach (var oneNews in healthCollection)
                {
                    ImageManagement imageManagement = new ImageManagement();
                    healthImageCollection.Insert(0, new NotificationImageSource(DataConverter.ConvertToString(oneNews.Organization), DataConverter.ConvertToString(oneNews.Title), DataConverter.ConvertToString(oneNews.Category), DataConverter.ConvertToString(oneNews.Content), DataConverter.ConvertToString(oneNews.Date), imageManagement.ByteArrayToImageSource(oneNews.Image), imageManagement.ByteArrayToImageSource(oneNews.OrgImage)));
                }

                foreach (var oneNews in securityCollection)
                {
                    ImageManagement imageManagement = new ImageManagement();
                    securityImageCollection.Insert(0, new NotificationImageSource(DataConverter.ConvertToString(oneNews.Organization), DataConverter.ConvertToString(oneNews.Title), DataConverter.ConvertToString(oneNews.Category), DataConverter.ConvertToString(oneNews.Content), DataConverter.ConvertToString(oneNews.Date), imageManagement.ByteArrayToImageSource(oneNews.Image), imageManagement.ByteArrayToImageSource(oneNews.OrgImage)));
                }

                foreach (var oneNews in incomeCollection)
                {
                    ImageManagement imageManagement = new ImageManagement();
                    incomeImageCollection.Insert(0, new NotificationImageSource(DataConverter.ConvertToString(oneNews.Organization), DataConverter.ConvertToString(oneNews.Title), DataConverter.ConvertToString(oneNews.Category), DataConverter.ConvertToString(oneNews.Content), DataConverter.ConvertToString(oneNews.Date), imageManagement.ByteArrayToImageSource(oneNews.Image), imageManagement.ByteArrayToImageSource(oneNews.OrgImage)));
                }

                foreach (var oneNews in artCollection)
                {
                    ImageManagement imageManagement = new ImageManagement();
                    artImageCollection.Insert(0, new NotificationImageSource(DataConverter.ConvertToString(oneNews.Organization), DataConverter.ConvertToString(oneNews.Title), DataConverter.ConvertToString(oneNews.Category), DataConverter.ConvertToString(oneNews.Content), DataConverter.ConvertToString(oneNews.Date), imageManagement.ByteArrayToImageSource(oneNews.Image), imageManagement.ByteArrayToImageSource(oneNews.OrgImage)));
                }

                foreach (var oneNews in communicationCollection)
                {
                    ImageManagement imageManagement = new ImageManagement();
                    communicationImageCollection.Insert(0, new NotificationImageSource(DataConverter.ConvertToString(oneNews.Organization), DataConverter.ConvertToString(oneNews.Title), DataConverter.ConvertToString(oneNews.Category), DataConverter.ConvertToString(oneNews.Content), DataConverter.ConvertToString(oneNews.Date), imageManagement.ByteArrayToImageSource(oneNews.Image), imageManagement.ByteArrayToImageSource(oneNews.OrgImage)));
                }

                if (App.newsType.Equals("Communication With Communities"))
                {

                    foreach (var oneNotification in communicationCollection)
                    {
                        ImageManagement imageManagement = new ImageManagement();
                        NotificationImageSource communicationImage = new NotificationImageSource(DataConverter.ConvertToString(oneNotification.Organization), DataConverter.ConvertToString(oneNotification.Title), DataConverter.ConvertToString(oneNotification.Category), DataConverter.ConvertToString(oneNotification.Content), DataConverter.ConvertToString(oneNotification.Date), imageManagement.ByteArrayToImageSource(oneNotification.Image), imageManagement.ByteArrayToImageSource(oneNotification.OrgImage));
                        communicationImageCollection.Add(communicationImage);
                    }

                    foreach (var oneNotification in communicationImageCollection)
                    {
                        Frame frame = new Frame();
                        frame.OutlineColor = Color.White;
                        frame.CornerRadius = 10;
                        frame.Margin = new Thickness(5);

                        StackLayout stack = new StackLayout();
                        stack.Orientation = StackOrientation.Horizontal;

                        stack.Children.Add(new Image()
                        {
                            Source = oneNotification.OrgImage,
                            WidthRequest = 70,
                            HeightRequest = 70
                        });

                        StackLayout stack2 = new StackLayout();

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Title,
                            TextColor = Color.FromHex("#4c466c"),
                            FontSize = 20
                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Date,
                            TextColor = Color.FromHex("#4c466c"),
                            FontSize = 12
                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Organization,
                            TextColor = Color.FromHex("#4c466c"),
                            FontSize = 0.000001,
                            Opacity = 0

                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Category,
                            FontSize = 0.000001,
                            Opacity = 0
                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Content,
                            FontSize = 0.000001,
                            Opacity = 0
                        });

                        stack2.Children.Add(new Image()
                        {
                            Source = oneNotification.Image,
                            WidthRequest = 0.000001,
                            HeightRequest = 0.000001,
                            Opacity = 0
                        });

                        stack.Children.Add(stack2);

                        frame.Content = stack;

                        contentHolder.Children.Add(frame);

                        frame.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            Command = new Command(() =>
                            {
                                ITEM_CLICKED(frame);
                            })

                        });

                    }

                    //loadingImage.IsRunning = false;

                }

                if (App.newsType.Equals("Health And Sanitation"))
                {

                    foreach (var oneNotification in healthCollection)
                    {
                        ImageManagement imageManagement = new ImageManagement();
                        NotificationImageSource healthImage = new NotificationImageSource(DataConverter.ConvertToString(oneNotification.Organization), DataConverter.ConvertToString(oneNotification.Title), DataConverter.ConvertToString(oneNotification.Category), DataConverter.ConvertToString(oneNotification.Content), DataConverter.ConvertToString(oneNotification.Date), imageManagement.ByteArrayToImageSource(oneNotification.Image), imageManagement.ByteArrayToImageSource(oneNotification.OrgImage));
                        healthImageCollection.Add(healthImage);
                    }

                    foreach (var oneNotification in healthImageCollection)
                    {
                        Frame frame = new Frame();
                        frame.OutlineColor = Color.White;
                        frame.CornerRadius = 10;
                        frame.Margin = new Thickness(5);

                        StackLayout stack = new StackLayout();
                        stack.Orientation = StackOrientation.Horizontal;

                        stack.Children.Add(new Image()
                        {
                            Source = oneNotification.OrgImage,
                            WidthRequest = 70,
                            HeightRequest = 70
                        });

                        StackLayout stack2 = new StackLayout();

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Title,
                            TextColor = Color.FromHex("#4c466c"),
                            FontSize = 20
                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Date,
                            TextColor = Color.FromHex("#4c466c"),
                            FontSize = 12
                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Organization,
                            TextColor = Color.FromHex("#4c466c"),
                            FontSize = 0.000001,
                            Opacity = 0

                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Category,
                            FontSize = 0.000001,
                            Opacity = 0
                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Content,
                            FontSize = 0.000001,
                            Opacity = 0
                        });

                        stack2.Children.Add(new Image()
                        {
                            Source = oneNotification.Image,
                            WidthRequest = 0.000001,
                            HeightRequest = 0.000001,
                            Opacity = 0
                        });

                        stack.Children.Add(stack2);

                        frame.Content = stack;

                        contentHolder.Children.Add(frame);

                        frame.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            Command = new Command(() =>
                            {
                                ITEM_CLICKED(frame);
                            })

                        });

                    }

                    //loadingImage.IsRunning = false;

                }

                if (App.newsType.Equals("Security"))
                {
                    foreach (var oneNotification in securityCollection)
                    {
                        ImageManagement imageManagement = new ImageManagement();
                        NotificationImageSource securityImage = new NotificationImageSource(DataConverter.ConvertToString(oneNotification.Organization), DataConverter.ConvertToString(oneNotification.Title), DataConverter.ConvertToString(oneNotification.Category), DataConverter.ConvertToString(oneNotification.Content), DataConverter.ConvertToString(oneNotification.Date), imageManagement.ByteArrayToImageSource(oneNotification.Image), imageManagement.ByteArrayToImageSource(oneNotification.OrgImage));
                        securityImageCollection.Add(securityImage);
                    }

                    foreach (var oneNotification in securityImageCollection)
                    {
                        Frame frame = new Frame();
                        frame.OutlineColor = Color.White;
                        frame.CornerRadius = 10;
                        frame.Margin = new Thickness(5);

                        StackLayout stack = new StackLayout();
                        stack.Orientation = StackOrientation.Horizontal;

                        stack.Children.Add(new Image()
                        {
                            Source = oneNotification.OrgImage,
                            WidthRequest = 70,
                            HeightRequest = 70
                        });

                        StackLayout stack2 = new StackLayout();

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Title,
                            TextColor = Color.FromHex("#4c466c"),
                            FontSize = 20
                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Date,
                            TextColor = Color.FromHex("#4c466c"),
                            FontSize = 12
                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Organization,
                            TextColor = Color.FromHex("#4c466c"),
                            FontSize = 0.000001,
                            Opacity = 0

                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Category,
                            FontSize = 0.000001,
                            Opacity = 0
                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Content,
                            FontSize = 0.000001,
                            Opacity = 0
                        });

                        stack2.Children.Add(new Image()
                        {
                            Source = oneNotification.Image,
                            WidthRequest = 0.000001,
                            HeightRequest = 0.000001,
                            Opacity = 0
                        });

                        stack.Children.Add(stack2);

                        frame.Content = stack;

                        contentHolder.Children.Add(frame);

                        frame.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            Command = new Command(() =>
                            {
                                ITEM_CLICKED(frame);
                            })

                        });

                    }

                    //loadingImage.IsRunning = false;
                }

                if (App.newsType.Equals("Income Generating Activities"))
                {
                    foreach (var oneNotification in incomeCollection)
                    {
                        ImageManagement imageManagement = new ImageManagement();
                        NotificationImageSource incomeImage = new NotificationImageSource(DataConverter.ConvertToString(oneNotification.Organization), DataConverter.ConvertToString(oneNotification.Title), DataConverter.ConvertToString(oneNotification.Category), DataConverter.ConvertToString(oneNotification.Content), DataConverter.ConvertToString(oneNotification.Date), imageManagement.ByteArrayToImageSource(oneNotification.Image), imageManagement.ByteArrayToImageSource(oneNotification.OrgImage));
                        incomeImageCollection.Add(incomeImage);
                    }

                    foreach (var oneNotification in incomeImageCollection)
                    {
                        Frame frame = new Frame();
                        frame.OutlineColor = Color.White;
                        frame.CornerRadius = 10;
                        frame.Margin = new Thickness(5);

                        StackLayout stack = new StackLayout();
                        stack.Orientation = StackOrientation.Horizontal;

                        stack.Children.Add(new Image()
                        {
                            Source = oneNotification.OrgImage,
                            WidthRequest = 70,
                            HeightRequest = 70
                        });

                        StackLayout stack2 = new StackLayout();

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Title,
                            TextColor = Color.FromHex("#4c466c"),
                            FontSize = 20
                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Date,
                            TextColor = Color.FromHex("#4c466c"),
                            FontSize = 12
                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Organization,
                            TextColor = Color.FromHex("#4c466c"),
                            FontSize = 0.000001,
                            Opacity = 0

                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Category,
                            FontSize = 0.000001,
                            Opacity = 0
                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Content,
                            FontSize = 0.000001,
                            Opacity = 0
                        });

                        stack2.Children.Add(new Image()
                        {
                            Source = oneNotification.Image,
                            WidthRequest = 0.000001,
                            HeightRequest = 0.000001,
                            Opacity = 0
                        });

                        stack.Children.Add(stack2);

                        frame.Content = stack;

                        contentHolder.Children.Add(frame);

                        frame.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            Command = new Command(() =>
                            {
                                ITEM_CLICKED(frame);
                            })

                        });

                    }

                    //loadingImage.IsRunning = false;

                }

                if (App.newsType.Equals("Art And Entertainment"))
                {
                    foreach (var oneNotification in artCollection)
                    {
                        ImageManagement imageManagement = new ImageManagement();
                        NotificationImageSource artImage = new NotificationImageSource(DataConverter.ConvertToString(oneNotification.Organization), DataConverter.ConvertToString(oneNotification.Title), DataConverter.ConvertToString(oneNotification.Category), DataConverter.ConvertToString(oneNotification.Content), DataConverter.ConvertToString(oneNotification.Date), imageManagement.ByteArrayToImageSource(oneNotification.Image), imageManagement.ByteArrayToImageSource(oneNotification.OrgImage));
                        artImageCollection.Add(artImage);
                    }

                    foreach (var oneNotification in artImageCollection)
                    {
                        Frame frame = new Frame();
                        frame.OutlineColor = Color.White;
                        frame.CornerRadius = 10;
                        frame.Margin = new Thickness(5);

                        StackLayout stack = new StackLayout();
                        stack.Orientation = StackOrientation.Horizontal;

                        stack.Children.Add(new Image()
                        {
                            Source = oneNotification.OrgImage,
                            WidthRequest = 70,
                            HeightRequest = 70
                        });

                        StackLayout stack2 = new StackLayout();

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Title,
                            TextColor = Color.FromHex("#4c466c"),
                            FontSize = 20
                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Date,
                            TextColor = Color.FromHex("#4c466c"),
                            FontSize = 12
                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Organization,
                            TextColor = Color.FromHex("#4c466c"),
                            FontSize = 0.000001,
                            Opacity = 0

                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Category,
                            FontSize = 0.000001,
                            Opacity = 0
                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Content,
                            FontSize = 0.000001,
                            Opacity = 0
                        });

                        stack2.Children.Add(new Image()
                        {
                            Source = oneNotification.Image,
                            WidthRequest = 0.000001,
                            HeightRequest = 0.000001,
                            Opacity = 0
                        });

                        stack.Children.Add(stack2);

                        frame.Content = stack;

                        contentHolder.Children.Add(frame);

                        frame.GestureRecognizers.Add(new TapGestureRecognizer()
                        {
                            Command = new Command(() =>
                            {
                                ITEM_CLICKED(frame);
                            })

                        });

                    }

                    //loadingImage.IsRunning = false;
                }

                if (App.newsType.Equals("Education"))
                {
                    foreach (var oneNotification in communicationCollection)
                    {
                        ImageManagement imageManagement = new ImageManagement();
                        NotificationImageSource educationImage = new NotificationImageSource(DataConverter.ConvertToString(oneNotification.Organization), DataConverter.ConvertToString(oneNotification.Title), DataConverter.ConvertToString(oneNotification.Category), DataConverter.ConvertToString(oneNotification.Content), DataConverter.ConvertToString(oneNotification.Date), imageManagement.ByteArrayToImageSource(oneNotification.Image), imageManagement.ByteArrayToImageSource(oneNotification.OrgImage));
                        educationImageCollection.Add(educationImage);
                    }

                    foreach (var oneNotification in educationImageCollection)
                    {
                        Frame frame = new Frame();
                        frame.OutlineColor = Color.White;
                        frame.CornerRadius = 10;
                        frame.Margin = new Thickness(5);

                        StackLayout stack = new StackLayout();
                        stack.Orientation = StackOrientation.Horizontal;

                        stack.Children.Add(new Image()
                        {
                            Source = oneNotification.OrgImage,
                            WidthRequest = 70,
                            HeightRequest = 70
                        });

                        StackLayout stack2 = new StackLayout();

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Title,
                            TextColor = Color.FromHex("#4c466c"),
                            FontSize = 20
                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Date,
                            TextColor = Color.FromHex("#4c466c"),
                            FontSize = 12
                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Organization,
                            TextColor = Color.FromHex("#4c466c"),
                            FontSize = 0.000001,
                            Opacity = 0

                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Category,
                            FontSize = 0.000001,
                            Opacity = 0
                        });

                        stack2.Children.Add(new Label()
                        {
                            Text = oneNotification.Content,
                            FontSize = 0.000001,
                            Opacity = 0
                        });

                        stack2.Children.Add(new Image()
                        {
                            Source = oneNotification.Image,
                            WidthRequest = 0.000001,
                            HeightRequest = 0.000001,
                            Opacity = 0
                        });

                        stack.Children.Add(stack2);

                        frame.Content = stack;

                        contentHolder.Children.Add(frame);

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

            catch (Exception)
            {

            }

            loadingImage.IsRunning = false;
            loadingImage.IsVisible = false;

            if (!App.isNetworkAvailable)
            {
                await DisplayAlert("Oops!", "Network Error!", "Close");
                Navigation.RemovePage(this);
            }

        }

        ImageManagement manageImages = new ImageManagement();
        private void ITEM_CLICKED(Frame frame)
        {
            Frame motherHolder = frame;
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
            NewDetails.image =meImage.Source;
            NewDetails.newsSuggestion = newsSuggestions;

            Navigation.PushAsync(new NewDetails());
        }
    }
}
