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
    public partial class UserQuestions : ContentPage
    {
        FireBaseDataManagement<QuestionsProperties> fireBaseData = new FireBaseDataManagement<QuestionsProperties>();
        ObservableCollection<QuestionsProperties> observableCollection = new ObservableCollection<QuestionsProperties>();

        public UserQuestions()
        {
            InitializeComponent();
            MyListView.ItemsSource = observableCollection;

        }

        private void SubmitButton_Clicked(object sender, EventArgs e)
        {
            CheckData();
        }

        //please create a method for checking the data

        public async void CheckData()
        {
            //Create the object of the class of QuestionProperties and start using the key to access its members

            QuestionsProperties question;

            if (QuestionInput.Text != null)
            {
                if (QuestionInput.Text.Length > 8)
                {
                    question = new QuestionsProperties(DataConverter.ConvertToByteArray(QuestionInput.Text), DataConverter.ConvertToByteArray(DateTime.Now.ToString()), DataConverter.ConvertToByteArray(App.currentPhoneNumber));

                    Task task = Task.Run(() =>
                    {
                        fireBaseData.AddNewUser(question, "DN_Questions");
                    });
                    await Task.WhenAll(task);

                    await DisplayAlert("Done!", "Question Sent!", "Close");

                    QuestionInput.Text = "";

                }
                else
                {
                    await DisplayAlert("Error", "Please, the length of your comment is too short!", "Cancel");
                }
            }
            else
            {
                await DisplayAlert("Error", "Provide a question, please!", "Close");
            }
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}
