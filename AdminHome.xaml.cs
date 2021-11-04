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
    public partial class AdminHome : ContentPage
    {
        public AdminHome()
        {
            InitializeComponent();
            ImageClick();
            LoadImages();
        }

        public void LoadImages()
        {
            var assembly = typeof(AdminHome).GetTypeInfo().Assembly;
            addorganizationsIcon.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Addition.png", assembly);
            // addorgBack.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);
            allOrganizationsIcon.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.offices.png", assembly);
            allUsersIcon.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.user.png", assembly);
            messageIcon.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.message.png", assembly);
            suggestionIcon.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.suggestion.png", assembly);
            //  allusersback.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);
            // questionback.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);
            // sugesstionBack.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);
            // allorgback.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);
            main.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);

            //mybackgroundImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.OtherImages.Back.png", assembly);

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {


        }

        private void ImageClick()
        {
            AddOrganization.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => Navigation.PushAsync(new Add_Organisations()))
            });

            AllOrganizations.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => Navigation.PushAsync(new ListOfOffices()))
            });

            AllUsers.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => Navigation.PushAsync(new AllUsers()))
            });

            Question.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => Navigation.PushAsync(new PageMessages()))
            });

            suggestionBox.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => Navigation.PushAsync(new AllSuggestions()))
            });

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new Login(), this);
            Navigation.PopAsync();
        }

        private void logout_Clicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new Login(),this);
            Navigation.PopAsync();
        }
    }
}
