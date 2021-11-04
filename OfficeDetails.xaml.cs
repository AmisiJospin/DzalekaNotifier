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
    public partial class OfficeDetails : ContentPage
    {

        public static string OrgName, ImageId,
            InfoSummary, ServiceTitle, 
            ServiceDescription, ContactServices,
            HowToAccess;
          public static   string title;
        Dictionary<String, string> ContactDetails = new Dictionary<string, string>();
             

        private void viewNotice_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SingleOfficeNotifications(OrgName));
        }

        public static string image;


        Assembly assembly = typeof(OfficeDetails).GetTypeInfo().Assembly;

        public OfficeDetails()
        {
            InitializeComponent();
            if(App.currentUserName!="")
            {
                editorg.IsVisible = false;
            }
            backgroundImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);
            LoadContent();
        }

        public static Dictionary<String, String> PushDataToEdit = new Dictionary<string, string>();
        private void editorg_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditOrganisationInfo());
        }

      
        
       

        //public void LoadingSplittedInfo()
        //{
        //    foreach (var item in splitedData)
        //    {
        //        if(item.Value != " " && item.Key != " ")
        //        {
        //            StackLayout stack = new StackLayout();
                   
        //            stack.Orientation = StackOrientation.Vertical;

        //            stack.Children.Add(new Label()
        //            {
        //                Text = item.Key,
        //                TextColor = Color.FromHex("#4c466c"),
        //                VerticalOptions = LayoutOptions.StartAndExpand,
        //                FontSize = 20
        //            });

        //           string  details = item.Value;
        //            details = details.Substring(0, details.Length > 150 ? 150 : details.Length);
        //            stack.Children.Add(new Label()
        //            {
                        
        //                Text = details+"...",
        //                TextColor = Color.FromHex("#4c466c"),
        //                FontSize = 12,
                       

        //            });
        //            Label morebt = new Label();
        //            morebt.HorizontalOptions = LayoutOptions.EndAndExpand;
        //            morebt.TextColor = Color.FromHex("#4c466c");
        //            morebt.BackgroundColor = Color.Transparent;
        //            morebt.Text = "More";
                    
        //            stack.Children.Add(morebt);

        //            BoxView line = new BoxView();
        //            line.Color = Color.FromHex("#4c466c");
        //            line.HeightRequest = 1;

        //            stack.Children.Add(line);

        //            stack.GestureRecognizers.Add(new TapGestureRecognizer()
        //            {
        //                Command = new Command(() =>
        //                {
        //                    ITEM_CLICKED(stack);
        //                })

        //            });

        //            servicesHolder.Children.Add(stack);
        //        }
               
        //    }
        //}

        private void ITEM_CLICKED(StackLayout sender)
        {
            StackLayout Stackrecogniser = (StackLayout)sender;
            Label title = (Label)Stackrecogniser.Children[0];
            Label details = (Label)Stackrecogniser.Children[1];
            
            Navigation.PushAsync(new MoreOfficeDetails(title.Text, details.Text));
        }

        private void ServicesPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            servicesHolder.Children.Clear();

            Picker picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {

                foreach (var item in ListOfOffices.listOfValues)
                {

                    StackLayout bigstack = new StackLayout();

                    foreach (var Services in item.orgServices)
                    {

                        if (ServicesPicker.SelectedItem.Equals(Services.Title.Trim()))
                        {
                            bigstack.Children.Add(new Label { Text = Services.Title.Trim(), FontSize = 16, FontAttributes = FontAttributes.Bold, TextColor = Color.FromHex("#4c466c") });
                            bigstack.Children.Add(new Label { Text = Services.Content.Trim(), FontSize = 13, TextColor = Color.FromHex("#4c466c") });
                            List<String> morelist = new List<string>();
                            if (Services.More != null)
                            {
                                foreach (var item3 in Services.More)
                                {
                                    bigstack.Children.Add(new Label { Text = "\u2022 " + item3.Trim(), FontSize = 13, TextColor = Color.FromHex("#4c466c") });
                                }
                                break;
                            }
                            
                        }
                        else
                        {
                            foreach (var Contact in ContactDetails)
                            {
                                if (ServicesPicker.SelectedItem.Equals(Contact.Key))
                                {
                                    servicesHolder.Children.Clear();
                                    servicesHolder.Children.Add(new Label { Text = Contact.Key, TextColor = Color.FromHex("#4c466c"), FontSize = 16 });
                                    servicesHolder.Children.Add(new Label { Text = DataConverter.ConvertToString(Contact.Value), TextColor = Color.FromHex("#4c466c"), FontSize = 13 });
                                    break;
                                }

                            }
                        }


                        servicesHolder.Children.Add(bigstack);

                    }
                }
            }
        }
        

        public static List<String> values;
        string summaryDetails;
       
        List<String> servicesTitles = new List<string>();

        public void LoadContent()
        {

            foreach (var item in ListOfOffices.listOfValues)
            {
                StackLayout bigstack = new StackLayout();
                if (OrgName.Equals(DataConverter.ConvertToString( item.OGName)))
                {
                    image = item.ImageID;
                    foreach (var Services in item.orgServices)
                    {
                        
                        bigstack.Children.Add(new Label { Text = Services.Title.Trim(), FontSize=16, FontAttributes = FontAttributes.Bold,TextColor= Color.FromHex("#4c466c") });
                        bigstack.Children.Add(new Label { Text = Services.Content.Trim(), FontSize = 13 , TextColor = Color.FromHex("#4c466c") });
                        List<String> morelist = new List<string>();
                        if(Services.More!=null)
                        {
                            foreach (var item3 in Services.More)
                            {
                                bigstack.Children.Add(new Label { Text = "\u2022 " + item3.Trim(), FontSize = 13 , TextColor = Color.FromHex("#4c466c") });
                            }
                        }
                        servicesTitles.Add(Services.Title.Trim());
                        
                    }
                   
                    ContactDetails.Add("Contact Services", item.ContactServices);
                    ContactDetails.Add("How To Access Their Services", item.HowToAccess);
                    servicesHolder.Children.Add(bigstack);
                }

               
            }
            ImageManagement management = new ImageManagement();
            PictureOffice.Source = management.ByteArrayToImageSource(image);
           
          
            OrgNameText.Text = OrgName;
            summaryDetails = InfoSummary;
            summaryDetails = summaryDetails.Substring(0, summaryDetails.Length > 150 ? 150 : summaryDetails.Length);
            InformationSummary.Text = summaryDetails+"...";

            servicesTitles.Add("Contact Services");
            servicesTitles.Add("How To Access Their Services");
            ServicesPicker.ItemsSource = servicesTitles;

        }
        private void moreTapped_Tapped(object sender, EventArgs e)
        {
            InformationSummary.Text = InfoSummary;
            More.IsVisible = false;
            Hide.IsVisible = true;
        }

        private void hideTapped_Tapped(object sender, EventArgs e)
        {
            InformationSummary.Text = summaryDetails+"...";
            More.IsVisible = true;
            Hide.IsVisible = false;
        }

       
    }
}