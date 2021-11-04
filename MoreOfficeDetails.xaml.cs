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
    public partial class MoreOfficeDetails : ContentPage
    {
        String title, details;
        Assembly assembly = typeof(OfficeDetails).GetTypeInfo().Assembly;
        public MoreOfficeDetails()
        {
            InitializeComponent();
            backgroundImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);
            LoadDataFromDetails();

        }

        public MoreOfficeDetails( String title, String details)
        {
            InitializeComponent();
            backgroundImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);

            this.title = title;
            this.details  = details;
            LoadDataFromDetails();
        }


        public static Dictionary<String, String> data = new Dictionary<string, string>();
        ImageManagement management = new ImageManagement();
        public void LoadDataFromDetails()
        {
            OrgNameText.Text = OfficeDetails.OrgName;
            foreach (var item in ListOfOffices.listOfValues)
            {
                if (OrgNameText.Text.Equals(DataConverter.ConvertToString(item.OGName)))
                {
                    PictureOffice.Source = management.ByteArrayToImageSource(item.ImageID);
                    break;
                }
            }

            foreach (var item in data)
            {
                if (item.Key.Equals(title))
                {
                    detailsLB.Text = item.Value;
                    titleLB.Text = item.Key;
                }
               
            }
            
        }
    }
}