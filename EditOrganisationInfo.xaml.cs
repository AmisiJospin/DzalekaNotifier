using Plugin.FilePicker;
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
    public partial class EditOrganisationInfo : ContentPage
    {
        Assembly assembly = typeof(RegisterOrganizations).GetTypeInfo().Assembly;
        FireBaseDataManagement<OrganizationProperties> firebaserOrganization = new FireBaseDataManagement<OrganizationProperties>();

        public EditOrganisationInfo()
        {
            InitializeComponent();
            backgroundImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);
            PictureOfAdmBox.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.user.png", assembly);
            LogoOfOrgBox.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.user.png", assembly);
            LoadData();
        }
        Dictionary<Entry, Editor> ExistingServices = new Dictionary<Entry, Editor>();
        Dictionary<Entry, Editor> addedServices = new Dictionary<Entry, Editor>();
        public void LoadData()
        {
            
            ImageManagement management = new ImageManagement();
            foreach (var item in ListOfOffices.listOfValues)
            {
                if (OfficeDetails.OrgName.Equals(DataConverter.ConvertToString(item.OGName)))
                {
                    PictureOfAdmBox.Source = management.ByteArrayToImageSource(item.Image);
                    LogoOfOrgBox.Source = management.ByteArrayToImageSource(item.ImageID);
                    NameBox.Text = DataConverter.ConvertToString(item.OGName);
                    AdmNameBox.Text = DataConverter.ConvertToString(item.AdminName);
                    userBox.Text = DataConverter.ConvertToString(item.UserName);
                    EmailBox.Text = DataConverter.ConvertToString(item.Email);
                    PhoneNumnberBox.Text = DataConverter.ConvertToString(item.PhoneNumber);
                    Adress.Text = DataConverter.ConvertToString(item.Address);
                    PassWord.Text = DataConverter.ConvertToString(item.Password);
                    Summary.Text = DataConverter.ConvertToString(item.InfoSummary);
                    ServicesAccess.Text = DataConverter.ConvertToString(item.HowToAccess);
                    Contacts.Text = DataConverter.ConvertToString(item.ContactServices);

                }
            }

            LoadMoreServices();
         
        }

        private void AddMore_Clicked(object sender, EventArgs e)
        {
            Editor addContent;
            Entry addTitle;
            StackLayout moreInformationHolder = new StackLayout();

            moreInformationHolder.Margin = new Thickness(10, 0, 10, 0);
            moreInformationHolder.WidthRequest = double.MaxValue;
            moreInformationHolder.HorizontalOptions = LayoutOptions.CenterAndExpand;


            moreInformationHolder.Children.Add(
                addTitle = new Entry()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.White,
                    Placeholder = "Service Title ",
                    Margin = new Thickness(30, 10, 30, 10),
                });


            moreInformationHolder.Children.Add(
                addContent = new Editor()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HeightRequest = 200,
                    WidthRequest = 150,
                    Text = "Description ",
                    BackgroundColor = Color.White,
                    Margin = new Thickness(30, 10, 30, 10)

                });
            Button moreButton = new Button();
            moreButton.BackgroundColor = Color.FromHex("#4c466c");
            moreButton.Text = "Add More Bullets";
            moreButton.HorizontalOptions = LayoutOptions.CenterAndExpand;

            BigStack.Children.Add(moreInformationHolder);
            BigStack.Children.Add(moreButton);
            holderInput.Children.Add(BigStack);
            addedServices.Add(addTitle, addContent);
        }
        public static string imageAdm;
        public static string imageLogo;
        private async void adminPic_Clicked(object sender, EventArgs e)
        {
            var file = await CrossFilePicker.Current.PickFile();

            if (file != null)
            {
                byte[] data = file.DataArray;
                string myimageTobase64 = System.Convert.ToBase64String(data);

                ImageManagement Convert = new ImageManagement();
                PictureOfAdmBox.Source = Convert.ByteArrayToImageSource(myimageTobase64);
                imageAdm = myimageTobase64;

            }
        }

        private async void logoOrga_Clicked(object sender, EventArgs e)
        {
            var file = await CrossFilePicker.Current.PickFile();

            if (file != null)
            {
                byte[] data = file.DataArray;
                string myimageTobase64 = System.Convert.ToBase64String(data);

                ImageManagement Convert = new ImageManagement();
                LogoOfOrgBox.Source = Convert.ByteArrayToImageSource(myimageTobase64);
                imageLogo = myimageTobase64;

            }
        }
        List<Register_Org_TempClass> servicesList = new List<Register_Org_TempClass>();
        Register_Org_TempClass reg;
        int ServiceNumber = 0;

        StackLayout moreInformationHolder = new StackLayout();
        StackLayout BigStack = new StackLayout();
        public void LoadMoreServices()
        {
            servicesList.Add(reg);
            foreach (var item in ListOfOffices.listOfValues)
            {
                foreach (var item2 in item.orgServices)
                {
                    if (NameBox.Text.Equals(DataConverter.ConvertToString(item.OGName)))
                    {

                        reg = new Register_Org_TempClass();
                        reg.More = new List<Editor>();


                        ServiceNumber += 1;


                        moreInformationHolder.Margin = new Thickness(10, 0, 10, 0);
                        moreInformationHolder.WidthRequest = double.MaxValue;
                        moreInformationHolder.HorizontalOptions = LayoutOptions.CenterAndExpand;

                        BigStack.Margin = new Thickness(10, 0, 10, 0);
                        BigStack.WidthRequest = double.MaxValue;
                        BigStack.HorizontalOptions = LayoutOptions.CenterAndExpand;
                        


                         Button moreButton = new Button();
                        moreButton.BackgroundColor = Color.FromHex("#4c466c");
                        moreButton.Text = "Add More Bullets";
                        moreButton.HorizontalOptions = LayoutOptions.CenterAndExpand;


                        moreInformationHolder.Children.Add(
                            reg.Title = new Entry()
                            {
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                BackgroundColor = Color.White,
                                Text = item2.Title,
                                Margin = new Thickness(30, 10, 30, 10),
                            });


                        moreInformationHolder.Children.Add(
                            reg.Content = new Editor()
                            {
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                HeightRequest = 100,
                                WidthRequest = 150,
                                Text = item2.Content,
                                BackgroundColor = Color.White,
                                Margin = new Thickness(30, 10, 30, 10)

                            });
                        if (item2.More != null)
                        {
                            foreach (var item3 in item2.More)
                            {
                                Editor morebullets;
                                moreInformationHolder.Children.Add(morebullets = new Editor
                                {
                                    Text = item3,
                                    Margin = new Thickness(30, 10, 30, 10)

                                });
                            }
                        }
                     

                       

                        BigStack.Children.Add(moreInformationHolder);

                        moreButton.Clicked += moreBullets_Clicked;
                        
                        BigStack.Children.Add(moreButton);

                        holderInput.Children.Add(BigStack);
                    }
                }
              
                    
            }
        }
        Dictionary<String, OrganizationProperties> dictionary;
        List<String> keylist = new List<string>();
        List<OrganizationProperties> values = new List<OrganizationProperties>();
        private async void registerButton_Clicked(object sender, EventArgs e)
        {
            LoadMoreServices();
            dictionary = await firebaserOrganization.GetAllUsers("DN_Organizations");
            keylist = dictionary.Select(K => K.Key).ToList();
            values = dictionary.Select(K => K.Value).ToList();
            int selectedId = values.FindIndex(m => m.Email.Equals(DataConverter.ConvertToByteArray(EmailBox.Text)));
            foreach (var item in values)
            {
                if(DataConverter.ConvertToString(item.Email).Equals(EmailBox.Text))
                {
                    OrganizationProperties org = new OrganizationProperties();
                    if(imageAdm==""&& imageLogo=="")
                    {
                        org.Image = imageAdm;
                        org.ImageID = imageLogo;
                    }
                    else
                    {
                        org.Image = item.Image;
                        org.ImageID = item.ImageID;
                    }
                   
                    org.OGName = DataConverter.ConvertToByteArray(NameBox.Text);
                    org.AdminName = DataConverter.ConvertToByteArray(AdmNameBox.Text);
                    org.UserName = DataConverter.ConvertToByteArray(userBox.Text);
                    org.Email = DataConverter.ConvertToByteArray(EmailBox.Text);
                    org.PhoneNumber = DataConverter.ConvertToByteArray(PhoneNumnberBox.Text);
                    org.Address = DataConverter.ConvertToByteArray(Adress.Text);
                    org.Password = DataConverter.ConvertToByteArray(PassWord.Text);
                    org.InfoSummary = DataConverter.ConvertToByteArray(Summary.Text);
                    org.HowToAccess = DataConverter.ConvertToByteArray(ServicesAccess.Text);
                    org.ContactServices = DataConverter.ConvertToByteArray(Contacts.Text);
                    loadingImage.IsVisible = true;
                    loadingImage.IsRunning = true;
                    firebaserOrganization.ResetPassword(org, "DN_Organizations\\" + keylist[selectedId]);

                    loadingImage.IsRunning = false;
                    loadingImage.IsVisible = false;
                    
                    await Navigation.PushAsync(new ListOfOffices());
                    await  DisplayAlert("Done!", "Successfully registered!", "OK");
                    

                }
            }
           
           
        }
        private void moreBullets_Clicked(object sender, EventArgs e)
        {

            Editor morebullets;


            moreInformationHolder.Children.Add(morebullets = new Editor
            {
                Text = "More Info",
                Margin = new Thickness(30, 10, 30, 10)

            });
            reg.More.Add(morebullets);
        }
    }
}