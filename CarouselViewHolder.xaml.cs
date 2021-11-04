using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DzalekaNotifierFinal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarouselViewHolder : ContentPage
    {
        public CarouselViewHolder()
        {
            InitializeComponent();
            LoadOrganisationInfo();
        }
        List<Organizations> orgvalueList = new List<Organizations>();
        List<CarouselViewClass> ConvertedList = new List<CarouselViewClass>();
        ImageManagement Imagemanager = new ImageManagement();
        FireBaseDataManagement<Organizations> getfirebasemethods = new FireBaseDataManagement<Organizations>();
        public async void LoadOrganisationInfo()
        {
            Dictionary<String, Organizations> dictionary = await getfirebasemethods.GetAllUsers("DN_Organizations");
            if (dictionary != null)
            {
                orgvalueList = dictionary.Select(V => V.Value).ToList();
                foreach (var item in orgvalueList)
                {
                    ConvertedList.Add(new CarouselViewClass
                    {
                        Activity = DataConverter.ConvertToString(item.Activity),
                        Information = DataConverter.ConvertToString(item.Information),
                        Location = DataConverter.ConvertToString(item.Location)

                    });

                    //CarouselViewPage.ItemsSource = ConvertedList;
                }
            }
            else
            {

            }

        }
    }
}