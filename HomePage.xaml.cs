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
    public partial class HomePage : ContentPage
    {
        public static bool logedIn;
        Assembly assembly = typeof(HomePage).GetTypeInfo().Assembly;

        public HomePage()
        {
            InitializeComponent();

           
            //LoadOrganisationInfo();
            UserOrOrganization();
            LoadImages();
            ClickStack();

            //imageCurrentUser.Source = ImageSource.FromResource("DzalekaNotifierFinal.OtherImages.user.png", assembly);

            ImageManagement imageManagement = new ImageManagement();
            nameCurrentUser.Text = App.currentUser;
            imageCurrentUser.Source = imageManagement.ByteArrayToImageSource(App.currentImage);
        }

        public void LoadImages()
        {
            //backgroungImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.OtherImages.Back.png", assembly);
            officesImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.OtherImages.offices.png", assembly);
            newsImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.OtherImages.newz.png", assembly);
            ourNoticesImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.OtherImages.Globe.png", assembly);
            addNoticeImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.OtherImages.Addition.png", assembly);
            recentsImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.OtherImages.recents.png", assembly);
            helpImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.OtherImages.help.png", assembly);
            backgroungImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);
            //OrganisationsViewer.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.communicate.png", assembly);
        }
       
       

        public void ClickStack()
        {
            offices.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => Navigation.PushAsync(new ListOfOffices()))
            });

            news.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => Navigation.PushAsync(new ListOfNotices()))
            });

            ourNotices.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => Navigation.PushAsync(new SingleOfficeNotifications(App.currentUser)))
            });

            addNotice.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => Navigation.PushAsync(new AddNotifications()))
            });

            recents.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => Navigation.PushAsync(new Recent()))
            });

            help.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => Navigation.PushAsync(new HelpUser()))
            });
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new Login(), this);
            Navigation.PopAsync();
        }

        public void UserOrOrganization()
        {
            if (App.userOrOrganization.Equals("User"))
            {
                Grid stack = new Grid();

                stack.HorizontalOptions = LayoutOptions.CenterAndExpand;
                stack.VerticalOptions = LayoutOptions.CenterAndExpand;

                motherStack.Children.Remove(ourNotices);
                motherStack.Children.Remove(addNotice);

                motherStack.ColumnDefinitions.Remove(removableColumn);

                motherStack.RowDefinitions.Remove(removeRowDefinition);
                removeRowDefinition.Height = 1;

                Grid.SetRow(help, 1);
                Grid.SetColumn(help, 1);
                Grid.SetRow(recents, 1);
                Grid.SetColumn(recents, 0);

                motherStack.Children.Add(stack);



            }
        }


        private void profile_Clicked(object sender, EventArgs e)
        {
             Navigation.PushAsync(new Profile());
        }

        private void logout_Clicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new Login(),this);
            Navigation.PopAsync();
        }


        List<Organizations> orgvalueList = new List<Organizations>();
        ImageManagement Imagemanager = new ImageManagement();
        FireBaseDataManagement<Organizations> getfirebasemethods = new FireBaseDataManagement<Organizations>();
        
    }
}