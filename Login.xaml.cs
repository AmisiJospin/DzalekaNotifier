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
    public partial class Login : ContentPage
    {
        FireBaseDataManagement<Users> firebaseUsers = new FireBaseDataManagement<Users>();
        FireBaseDataManagement<OrganizationProperties> firebaseOrganizationProperties = new FireBaseDataManagement<OrganizationProperties>();


        Dictionary<string, Users> usersDictionary;
        Dictionary<string, OrganizationProperties> OrganizationPropertiesDictionary;
        Dictionary<string, Users> usersTempDictionary;
        List<Users> usersCollection = new List<Users>();
        int selectedId;
        List<Users> usersTempCollection = new List<Users>();
        List<String> TempKeys = new List<string>();
        List<OrganizationProperties> organizationCollection = new List<OrganizationProperties>();

        Assembly assembly = typeof(Login).GetTypeInfo().Assembly;

        public Login()
        {
            InitializeComponent();
           BackImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);

        }

        public async void LoginMethod()
        {
            loadingImage.IsRunning = true;
            loadingImage.IsVisible = true;
            loginButton.Opacity = 0;
            invalidInputs.IsVisible = false;

            bool invalid = true;

            bool notFound = true;


            if (emailBox.Text == "Admin" && passwordBox.Text == "malawi2018")
            {
                App.currentUser = "Admin";

                //connect.RunInTransaction(() =>
                //{
                //    connect.Insert(new Admin("Admin", "malawi2018"));
                //});

                loadingImage.IsRunning = false;
                loadingImage.IsVisible = false;
                loginButton.Opacity = 1;

                Navigation.InsertPageBefore(new AdminHome(), this);
                await Navigation.PopAsync();
            }

            else
            {
                try
                {
                        usersDictionary = await firebaseUsers.GetAllUsers("DN_Users");
                        usersTempDictionary = await firebaseUsers.GetAllUsers("DN_Users_Temp");
                        OrganizationPropertiesDictionary = await firebaseOrganizationProperties.GetAllUsers("DN_Organizations");


                        organizationCollection = OrganizationPropertiesDictionary.Select(V => V.Value).ToList();
                       usersCollection = usersDictionary.Select(V => V.Value).ToList();
                        usersTempCollection = usersTempDictionary.Select(V => V.Value).ToList();
                        TempKeys = usersTempDictionary.Select(V => V.Key).ToList();
                        
                        selectedId = usersTempCollection.FindIndex(m => m.Email.Equals(DataConverter.ConvertToByteArray(emailBox.Text)));


                    try
                    {
                        foreach (var oneUser in usersCollection)
                        {
                            usersCollection.Add(new Users(oneUser.ImageUser, DataConverter.ConvertToString(oneUser.Name), DataConverter.ConvertToString(oneUser.UserName), DataConverter.ConvertToString(oneUser.Email), DataConverter.ConvertToString(oneUser.PhoneNumber), DataConverter.ConvertToString(oneUser.Address), DataConverter.ConvertToString(oneUser.Password),DataConverter.ConvertToString(oneUser.question1), DataConverter.ConvertToString(oneUser.question2), DataConverter.ConvertToString(oneUser.question3)));
                        }
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        foreach (var oneUserTemp in usersTempCollection)
                        {
                            usersTempCollection.Add(new Users(oneUserTemp.ImageUser, DataConverter.ConvertToString(oneUserTemp.Name), DataConverter.ConvertToString(oneUserTemp.UserName), DataConverter.ConvertToString(oneUserTemp.Email), DataConverter.ConvertToString(oneUserTemp.PhoneNumber), DataConverter.ConvertToString(oneUserTemp.Address), DataConverter.ConvertToString(oneUserTemp.Password),DataConverter.ConvertToString(oneUserTemp.question1), DataConverter.ConvertToString(oneUserTemp.question2), DataConverter.ConvertToString(oneUserTemp.question3)));
                        }
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        foreach (var oneOrganization in organizationCollection)
                        {
                            organizationCollection.Add(new OrganizationProperties(oneOrganization.ImageID, oneOrganization.Image, DataConverter.ConvertToString(oneOrganization.OGName),
                                DataConverter.ConvertToString(oneOrganization.AdminName), DataConverter.ConvertToString(oneOrganization.UserName),
                                DataConverter.ConvertToString(oneOrganization.Email), DataConverter.ConvertToString(oneOrganization.PhoneNumber),
                                DataConverter.ConvertToString(oneOrganization.Address), DataConverter.ConvertToString(oneOrganization.Password),
                                DataConverter.ConvertToString(oneOrganization.InfoSummary),oneOrganization.orgServices, DataConverter.ConvertToString(oneOrganization.HowToAccess),
                                DataConverter.ConvertToString(oneOrganization.ContactServices)

                               ));
                        }
                    }
                    catch (Exception)
                    {

                    }

                    if (usersDictionary != null)
                    {
                        foreach (var oneUser in usersCollection)
                        {
                            if ((DataConverter.ConvertToString(oneUser.Email).Equals(emailBox.Text) || DataConverter.ConvertToString(oneUser.PhoneNumber).Equals(emailBox.Text)) && DataConverter.ConvertToString(oneUser.Password).Equals(passwordBox.Text))
                            {

                                invalid = false;
                                notFound = false;

                                App.currentUser = oneUser.Name;
                                App.currentUserName = oneUser.UserName;
                                App.currentPhoneNumber = oneUser.PhoneNumber;
                                App.currentEmail = oneUser.Email;
                                App.currentImage = oneUser.ImageUser;
                                App.currentAddress = oneUser.Address;
                                App.userOrOrganization = "User";
                                HomePage.logedIn = true;

                                loadingImage.IsRunning = false;
                                loadingImage.IsVisible = false;
                                loginButton.Opacity = 1;

                                Navigation.InsertPageBefore(new HomePage(), this);
                                await Navigation.PopAsync();
                            }
                        }
                    }



                    if (notFound)
                    {
                        if (usersTempCollection != null)
                        {
                            foreach (var oneUser in usersTempCollection)
                            {
                                if ((DataConverter.ConvertToString(oneUser.Email).Equals(emailBox.Text) || DataConverter.ConvertToString(oneUser.PhoneNumber).Equals(emailBox.Text)) && DataConverter.ConvertToString(oneUser.Password).Equals(passwordBox.Text))
                                {
                                    invalid = false;
                                    notFound = false;

                                    UsersTempInfo.name = oneUser.Name;
                                    UsersTempInfo.phoneNumber = oneUser.PhoneNumber;
                                    UsersTempInfo.email = oneUser.Email;
                                    UsersTempInfo.image = oneUser.ImageUser;
                                    UsersTempInfo.adress = oneUser.Address;
                                    UsersTempInfo.password = oneUser.Password;
                                    UsersTempInfo.userName = oneUser.UserName;
                                    UsersTempInfo.idUser = TempKeys[selectedId];

                                    loadingImage.IsRunning = false;
                                    loadingImage.IsVisible = false;
                                    loginButton.Opacity = 1;

                                    Navigation.InsertPageBefore(new VerificationNumber(), this);
                                    await Navigation.PopAsync();
                                }
                            }
                        }


                    }

                }
                catch (Exception e)
                {
                    //await DisplayAlert("Error Check in User",  e.Message, "Close");
                }

                if (notFound)
                {
                    try
                    {
                        if (OrganizationPropertiesDictionary != null)
                       {
                            foreach (var oneOrganization in organizationCollection)
                            {
                                if ((DataConverter.ConvertToString(oneOrganization.Email).Equals(emailBox.Text) || DataConverter.ConvertToString(oneOrganization.PhoneNumber).Equals(emailBox.Text)) && DataConverter.ConvertToString(oneOrganization.Password).Equals(passwordBox.Text))
                                {

                                    invalid = false;
                                    notFound = false;

                                    App.currentUser =DataConverter.ConvertToString( oneOrganization.OGName);
                                    App.currentUserName = DataConverter.ConvertToString(oneOrganization.UserName);
                                    App.currentPhoneNumber = DataConverter.ConvertToString(oneOrganization.PhoneNumber);
                                    App.currentEmail = DataConverter.ConvertToString(oneOrganization.Email);
                                    App.currentImage = oneOrganization.Image;
                                    App.currentAddress = DataConverter.ConvertToString(oneOrganization.Address);
                                    App.userOrOrganization = "Organization";
                                    HomePage.logedIn = true;

                                    loadingImage.IsRunning = false;
                                    loadingImage.IsVisible = false;
                                    loginButton.Opacity = 1;

                                    Navigation.InsertPageBefore(new HomePage(), this);
                                    await Navigation.PopAsync();
                                }
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        //await DisplayAlert("Error Check in Organization", e.Message, "Close");
                    }
                }


                loadingImage.IsRunning = false;
                loadingImage.IsVisible = false;
                loginButton.Opacity = 1;

                if (notFound)
                {
                    invalidInputs.IsVisible = true;
                    forgotPass.IsVisible = false;
                }

                if (!App.isNetworkAvailable)
                {
                    invalidInputs.IsVisible = false;
                    forgotPass.IsVisible = true;
                    await DisplayAlert("Oops!", "Network Error!", "Close");
                }
            }
        }

        //public static void CreatingLoginDatabase()
        //{
        //    connect.CreateTable<Users>();
        //    connect.CreateTable<OrganizationProperties>();
        //    connect.CreateTable<Admin>();
        //}

        //public static void DeletingLoginDatabase()
        //{
        //    connect.DropTable<Users>();
        //    connect.DropTable<OrganizationProperties>();
        //    connect.DropTable<Admin>();
        //}

        //public static List<Users> GetLogedInUser()
        //{
        //    List<Users> logedIn = new List<Users>();
        //    logedIn = connect.Table<Users>().ToList();
        //    return logedIn;
        //}

        //public static List<OrganizationProperties> GetLogedOrganization()
        //{
        //    List<OrganizationProperties> logedOrg = new List<OrganizationProperties>();
        //    logedOrg = connect.Table<OrganizationProperties>().ToList();
        //    return logedOrg;
        //}

        //public static List<Admin> GetLogedAdmin()
        //{
        //    List<Admin> logedAdmin = new List<Admin>();
        //    logedAdmin = connect.Table<Admin>().ToList();
        //    return logedAdmin;
        //}



        private void loginButton_Clicked(object sender, EventArgs e)
        {
            if (emailBox.Text != null || passwordBox.Text != null)
            {
                LoginMethod();
            }
            else
            {
                DisplayAlert("Attention!", "Type in your email and password please!", "OK");

            }
        }

        private void register_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterUser());
        }

        private void Help_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HelpUser());
        }

        private void ForgotPassTapped_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AskQUestions());
        }
    }
}
