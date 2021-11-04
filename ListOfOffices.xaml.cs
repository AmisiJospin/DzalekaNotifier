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
    public partial class ListOfOffices : ContentPage
    {
        FireBaseDataManagement<OrganizationProperties> firebaseOrganizations = new FireBaseDataManagement<OrganizationProperties>();
        ImageManagement imageManagement = new ImageManagement();
        public static   List<OrganizationProperties> listOfValues = new List<OrganizationProperties>();
        List<String> listOfKeys = new List<string>();

        Assembly assembly = typeof(Offices).GetTypeInfo().Assembly;

        string getImage;
        public ListOfOffices()
        {
            InitializeComponent();
            backgroundImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);
            LoadOrganisationInfo();
        }

        public async void LoadOrganisationInfo()
        {
            loadingImage.IsVisible = true;
            loadingImage.IsRunning = true;
            try
            {
                Dictionary<String, OrganizationProperties> dictionary = await firebaseOrganizations.GetAllUsers("DN_Organizations");

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

                            getImage = item.ImageID;

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
                                Text = DataConverter.ConvertToString(item.InfoSummary),
                                FontSize = 0.000001,
                                Opacity = 0
                            });
                            
                            stack2.Children.Add(new Label()
                            {
                                Text = DataConverter.ConvertToString(item.ContactServices),
                                TextColor = Color.FromHex("#4c466c"),
                                FontAttributes = FontAttributes.None,
                                Margin = new Thickness(0, 5, 0, 0),
                                FontSize = 0.000001,
                                Opacity = 0
                            });

                      

                        

                            stack2.Children.Add(new Label()
                            {
                                Text = DataConverter.ConvertToString(item.HowToAccess),
                                FontSize = 0.000001,
                                Opacity = 0
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
            loadingImage.IsRunning = false;

        }

        private void ITEM_CLICKED(Frame sender)
        {
            Frame motherHolder = sender;
            StackLayout stack = (StackLayout)motherHolder.Content;

            Image image = (Image)stack.Children[0];
           
            StackLayout stack1 = (StackLayout)stack.Children[1];
            Label meOrgName = (Label)stack1.Children[0];
            Label meInfoSummary = (Label)stack1.Children[1];
            Label meHowToAccess = (Label)stack1.Children[3];
            Label Contacts = (Label)stack1.Children[2];


            OfficeDetails.OrgName = meOrgName.Text.ToString();
            OfficeDetails.InfoSummary = meInfoSummary.Text.ToString();
            OfficeDetails.ContactServices = Contacts.Text.ToString();
            OfficeDetails.HowToAccess = meHowToAccess.Text.ToString();
           

            Navigation.PushAsync(new OfficeDetails());

        }

    }
}