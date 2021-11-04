using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DzalekaNotifierFinal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMessages : ContentPage
    {
        FireBaseDataManagement<QuestionsProperties> fireBase = new FireBaseDataManagement<QuestionsProperties>();
        ObservableCollection<QuestionsProperties> observableCollection = new ObservableCollection<QuestionsProperties>();

        public PageMessages()
        {
            InitializeComponent();
            MyListView.ItemsSource = observableCollection;
        }

        private void MyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}