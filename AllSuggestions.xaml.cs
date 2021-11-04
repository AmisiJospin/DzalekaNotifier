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
    public partial class AllSuggestions : ContentPage
    {
        FireBaseDataManagement<Suggestion> fireBaseData = new FireBaseDataManagement<Suggestion>();
        ObservableCollection<Suggestion> suggestionsCollection = new ObservableCollection<Suggestion>();
        Assembly assembly = typeof(AllSuggestions).GetTypeInfo().Assembly;

        public AllSuggestions()
        {
            InitializeComponent();

            backgroundImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);
            LoadAllSuggestions();
        }

        private void allSuggestions_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        //public async void LoadAllSuggestions()
        //{
        //    App.isNetworkAvailable = false;


        //    loadingImage.IsRunning = true;
        //    loadingImage.IsVisible = true;

        //    try
        //    {
        //        Task task = Task.Run(async () =>
        //        {

        //            foreach (var OneSuggestion in LoadDataOffline.AllSuggestionsDictionnary)
        //            {
        //                suggestionsCollection.Add(new Suggestion(DataConverter.ConvertToString(OneSuggestion.Value.Sender), DataConverter.ConvertToString(OneSuggestion.Value.PhoneNumber), DataConverter.ConvertToString(OneSuggestion.Value.Address), DataConverter.ConvertToString(OneSuggestion.Value.Content), DataConverter.ConvertToString(OneSuggestion.Value.Title), DataConverter.ConvertToString(OneSuggestion.Value.NoticeContent), DataConverter.ConvertToString(OneSuggestion.Value.Date)));
        //            }
        //        });
        //        await Task.WhenAll(task);
        //    }
        //    catch (Exception)
        //    {
        //    }

        //    loadingImage.IsRunning = false;
        //    loadingImage.IsVisible = false;

        //    if (!App.isNetworkAvailable)
        //    {
        //        await DisplayAlert("Oops!", "Network Error!", "Close");
        //        Navigation.RemovePage(this);
        //    }

        //    allSuggestions.ItemsSource = suggestionsCollection;

        //}

        public async void LoadAllSuggestions()
        {
            App.isNetworkAvailable = false;
            loadingImage.IsRunning = true;

            try
            {
                Task task = Task.Run(async () =>
                {
                    foreach (var oneSuggestion in await fireBaseData.GetAllUsers("DN_Suggestions"))
                    {
                        suggestionsCollection.Add(oneSuggestion.Value);
                    }
                });

                await Task.WhenAll(task);

             

                foreach (var oneUser in suggestionsCollection)
                {
                    Frame frame = new Frame();
                    frame.OutlineColor = Color.White;
                    frame.CornerRadius = 10;
                    frame.Margin = new Thickness(2);

                    StackLayout stack = new StackLayout();
                    stack.Orientation = StackOrientation.Horizontal;

                    stack.Children.Add(new BoxView()
                    {
                        WidthRequest = 5,
                        BackgroundColor = Color.FromHex("#4c466c")
                    });

                    StackLayout stack2 = new StackLayout();

                    stack2.Children.Add(new Label()
                    {
                        Text = DataConverter.ConvertToString(oneUser.Sender),
                        TextColor = Color.FromHex("#4c466c"),
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 20
                    });

                    stack2.Children.Add(new Label()
                    {
                        Text = DataConverter.ConvertToString(oneUser.Title),
                        TextColor = Color.FromHex("#4c466c"),
                        FontSize = 17
                    });

                    stack2.Children.Add(new Label()
                    {
                        Text = DataConverter.ConvertToString(oneUser.Date),
                        TextColor = Color.FromHex("#4c466c"),
                        FontSize = 14
                    });

                    stack.Children.Add(stack2);
                    frame.Content = stack;
                    contentHolder.Children.Add(frame);

                }

          

            }
            catch (Exception)
            {
                if (!App.isNetworkAvailable)
                {
                    await DisplayAlert("Oops!", "Network Error!", "Close");
                    Navigation.RemovePage(this);
                }
            }

            loadingImage.IsRunning = false;



        }
    }
}