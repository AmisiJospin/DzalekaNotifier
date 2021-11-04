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
    public partial class Offices : ContentPage
    {
        FireBaseDataManagement<Organizations> firebaseOrganizations = new FireBaseDataManagement<Organizations>();
        Assembly assembly = typeof(Offices).GetTypeInfo().Assembly;

        String d = "";

        public Offices()
        {
            InitializeComponent();
           

            backgroundImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);
            //LoadOfficesMethod();
            LoadOrganisationInfo();
        }
        ImageManagement imageManagement = new ImageManagement();

        List<Organizations> listOfValues = new List<Organizations>();
        List<String> listOfKeys = new List<string>();

        public async void LoadOfficesMethod()
        {
            App.isNetworkAvailable = false;
            loadingImage.IsRunning = true;
            try
            {
                Dictionary<String, Organizations> dictionary = await firebaseOrganizations.GetAllUsers("DN_Organizations");
                if (dictionary != null)
                {
                    listOfKeys = dictionary.Select(K => K.Key).ToList();
                    listOfValues = dictionary.Select(V => V.Value).ToList();

                    foreach (var item in listOfValues)
                    {
                        string name = App.currentUser;
                        if (!App.currentUser.Equals(DataConverter.ConvertToString(item.OGName)))
                        {
                            Frame frame = new Frame();
                            frame.OutlineColor = Color.White;
                            frame.CornerRadius = 10;
                            frame.Margin = new Thickness(2);

                            StackLayout stack = new StackLayout();
                            stack.Orientation = StackOrientation.Horizontal;

                            stack.Children.Add(new CircleImage()
                            {
                                Source = imageManagement.ByteArrayToImageSource(item.ImageID),
                                WidthRequest = 70,
                                HeightRequest = 70
                            });

                            StackLayout stack2 = new StackLayout();

                            stack2.Children.Add(new Label()
                            {
                                Text = DataConverter.ConvertToString(item.OGName),
                                TextColor = Color.FromHex("#4c466c"),
                                FontAttributes = FontAttributes.Bold,
                                Margin = new Thickness(0, 15, 0, 0),
                                FontSize = 20
                            });


                            stack.Children.Add(stack2);
                            frame.Content = stack;
                            scrollContent.Content = frame;
                            stackContainer.Children.Add(scrollContent);

                            frame.GestureRecognizers.Add(new TapGestureRecognizer()
                            {
                                Command = new Command(() =>
                                {
                                    ITEM_CLICKED(frame);
                                })

                            });

                        }
                        else
                        {
                           await DisplayAlert("Problem", "Prob", "Ok");
                        }
                    }


                }
            }
            catch(Exception e)
            {
              await  DisplayAlert("Error", e.Message, "Ok");
            }
            loadingImage.IsRunning = false;

            if (!App.isNetworkAvailable)
            {
                await DisplayAlert("Oops!", "Network Error!", "Close");
                Navigation.RemovePage(this);
            }
        }

        public async void LoadOrganisationInfo()
        {
            loadingImage.IsVisible = true;
            loadingImage.IsRunning = true;
            try
            {
                Dictionary<String, Organizations> dictionary = await firebaseOrganizations.GetAllUsers("DN_Organizations");
               
                if (dictionary != null)
                {

                    listOfValues = dictionary.Select(V => V.Value).ToList();
                    foreach (var item in listOfValues)
                    {

                        if (!App.currentUser.Equals(item.OGName))
                        {
                            Frame frame = new Frame();
                            frame.OutlineColor = Color.FromHex("#4c466c");
                            frame.CornerRadius = 10;
                            frame.Margin = new Thickness(2);

                            StackLayout stack = new StackLayout();
                            stack.Orientation = StackOrientation.Horizontal;

                            stack.Children.Add(new CircleImage()
                            {
                                Source = imageManagement.ByteArrayToImageSource(item.ImageID),
                                WidthRequest = 70,
                                HeightRequest = 70,
                                VerticalOptions = LayoutOptions.StartAndExpand
                            });

                            StackLayout stack2 = new StackLayout();

                            stack2.Children.Add(new Label()
                            {
                                Text = DataConverter.ConvertToString(item.OGName),
                                TextColor = Color.FromHex("#4c466c"),
                                FontAttributes = FontAttributes.Bold,
                                Margin = new Thickness(0, 15, 0, 0),
                                FontSize = 20
                            });



                            stack2.Children.Add(new Label()
                            {
                                Text = DataConverter.ConvertToString(item.Activity),
                                TextColor = Color.FromHex("#4c466c"),
                                FontAttributes = FontAttributes.None,
                                Margin = new Thickness(0, 5, 0, 0),
                                FontSize = 12
                            });


                            stack.Children.Add(stack2);
                            frame.Content = stack;
                            stackContainer.Children.Add(frame);

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
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "Ok");
                loadingImage.IsVisible = false;
            }

            loadingImage.IsVisible = false;


        }

        private void ITEM_CLICKED(Frame sender)
        {
            Frame motherHolder = sender;
            StackLayout stack = (StackLayout)motherHolder.Content;

            Image image = (Image)stack.Children[0];

            StackLayout stack1 = (StackLayout)stack.Children[1];
            Label meOrganization = (Label)stack1.Children[0];

            string officeName = meOrganization.Text.ToString();
            SingleOfficeNotifications.orgName = officeName;

            Navigation.PushAsync(new SingleOfficeNotifications(officeName));

        }
    }
}
