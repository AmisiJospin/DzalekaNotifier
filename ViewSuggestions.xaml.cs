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
    public partial class ViewSuggestions : ContentPage
    {
        FireBaseDataManagement<Suggestion> firebaserSuggestions = new FireBaseDataManagement<Suggestion>();
        ObservableCollection<string> suggectionsCollection = new ObservableCollection<string>();

        Assembly assembly = typeof(ViewSuggestions).GetTypeInfo().Assembly;

        public static string title, content;

        public ViewSuggestions()
        {
            InitializeComponent();

            backgroundImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);

            LoadingAllSuggestions();
        }

        //private void suggestionsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    contentText.Text = suggestionsList.SelectedItem.ToString();
        //}

        public async void LoadingAllSuggestions()
        {
            App.isNetworkAvailable = false;
            loadingImage.IsRunning = true;

            try
            {
                Task task = Task.Run(async () =>
                {
                    foreach (var oneSuggestion in await firebaserSuggestions.GetAllUsers("DN_Suggestions"))
                    {
                        string titleSuggestion = DataConverter.ConvertToString(oneSuggestion.Value.Title);
                        string contentSuggestion = DataConverter.ConvertToString(oneSuggestion.Value.NoticeContent);

                        if (title.Equals(titleSuggestion) && (content.Equals(contentSuggestion)))
                        {
                            //suggectionsCollection.Add(DataConverter.ConvertToString(oneSuggestion.Value.Content));
                            suggectionsCollection.Add(oneSuggestion.Value.Content);
                        }
                    }
                });
                await Task.WhenAll(task);


                foreach (var oneSuggestion in suggectionsCollection)
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
                        Text = DataConverter.ConvertToString(oneSuggestion),
                        TextColor = Color.FromHex("#4c466c"),
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 14
                    });

                    stack.Children.Add(stack2);
                    frame.Content = stack;
                    containerView.Children.Add(frame);

                    frame.GestureRecognizers.Add(new TapGestureRecognizer()
                    {
                        Command = new Command(() =>
                        {
                            ITEM_CLICKED(frame);

                        })

                    });
                }

            }
            catch (Exception)
            {
            }

            loadingImage.IsRunning = false;

            if (!App.isNetworkAvailable)
            {
                await DisplayAlert("Oops!", "Network Error!", "Close");
                Navigation.RemovePage(this);
            }

        }

        private void ITEM_CLICKED(Frame sender)
        {
            Frame motherHolder = sender;
            StackLayout stack = (StackLayout)motherHolder.Content;

            BoxView box = (BoxView)stack.Children[0];

            StackLayout stack1 = (StackLayout)stack.Children[1];
            Label meContent = (Label)stack1.Children[0];

            contentText.Text = meContent.Text.ToString();
        }
    }
}