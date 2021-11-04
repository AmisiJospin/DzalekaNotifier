using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DzalekaNotifierFinal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewDetails : ContentPage
    {
        public static string title, date, content;
        public static ImageSource image;
        public static string stringImage;
        public static string newsSuggestion = "";
        Button postOrCancelButton;
        Button cancelButton;
        Editor suggestionInput;
        TranslateHelper mainTrans = new TranslateHelper();
        string langSource = "";
        bool isSuggestionSent = true;

        FireBaseDataManagement<Suggestion> firebaseSuggestion = new FireBaseDataManagement<Suggestion>();
        FireBaseDataManagement<UserLikes> fireBaseData = new FireBaseDataManagement<UserLikes>();
        FireBaseDataManagement<UserViews> fireBaseDataView = new FireBaseDataManagement<UserViews>();
        FireBaseDataManagement<Notifications> loadImage = new FireBaseDataManagement<Notifications>();

        Assembly assembly = typeof(NewDetails).GetTypeInfo().Assembly;

        public NewDetails()
        {
            InitializeComponent();
            backgroundImage.Source = ImageSource.FromResource("DzalekaNotifierFinal.Images.Back.png", assembly);
            
            LoadingDetails();
          



            //ShowLikes();
            //ShowView();
            mainTrans.title = title;
            mainTrans.content = content;
            



            //checkLanguage(contentText.Text.ToString());
            checkLanguage(content);


            if (App.userOrOrganization.Equals("Organization"))
            {
                suggestionStack.Children.Remove(suggestionButton);
                //likeBtn.IsVisible = false;
                //likeNumbers.IsVisible = true;
                //viewNumbers.IsVisible = true;

            }
            else
            {
                suggestionStack.Children.Remove(viewSuggestionButton);
                //likeNumbers.IsVisible = true;
                //viewNumbers.IsVisible = true;
                //likeAndViewNumber.IsVisible = false;
            }

            if (newsSuggestion.Equals("news"))
            {
                suggestionStack.Children.Remove(viewSuggestionButton);
                //likeAndViewNumber.IsVisible = false;
            }


        }

        private void suggestionButton_Clicked(object sender, EventArgs e)
        {
            StackLayout suggestionHolder = new StackLayout();
            StackLayout suggestionButton = new StackLayout();

            suggestionHolder.Margin = new Thickness(10, 0, 10, 0);
            suggestionHolder.WidthRequest = double.MaxValue;
            suggestionHolder.HorizontalOptions = LayoutOptions.CenterAndExpand;
            suggestionButton.Orientation = StackOrientation.Horizontal;
            suggestionButton.HorizontalOptions = LayoutOptions.FillAndExpand;

            suggestionHolder.Children.Add(
                suggestionInput = new Editor()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HeightRequest = 100,
                    Margin = new Thickness(5,0,5,5)
                    
                });

            suggestionButton.Children.Add(
                cancelButton = new Button()
                {
                    HorizontalOptions = LayoutOptions.Start,
                    BackgroundColor = Color.FromHex("#4c466c"),
                    TextColor = Color.White,
                    Margin = new Thickness(0, 5, 5, 5),
                    Text = "Cancel"
                });

            suggestionButton.Children.Add(
              postOrCancelButton = new Button()
              {
                  HorizontalOptions = LayoutOptions.End,
                  BackgroundColor = Color.FromHex("#4c466c"),
                  TextColor = Color.White,
                  Margin = new Thickness(0, 5, 5, 5),
                  Text = "Post"
              });

            postOrCancelButton.Clicked += PostOrCancelButton_Clicked;
            cancelButton.Clicked += CancelButton_Clicked;
          //  suggestionStack.Children.Clear();

            suggestionHolder.Children.Add(suggestionButton);

            inputStack.Children.Add(suggestionHolder);
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {

            //theInputStackHolder.Children.Remove(inputStack);
            inputStack.Children.Clear();
        }

        private void PostOrCancelButton_Clicked(object sender, EventArgs e)
        {
            if (postOrCancelButton.Text.Equals("Post"))
            {
                SendingSuggestion();

                if (isSuggestionSent)
                {
                    ScrollView scrollView = new ScrollView();

                    StackLayout finalHolder = new StackLayout();

                    finalHolder.Children.Add(
                        new Label()
                        {
                            Text = "You",
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromHex("#4c466c")

                        });

                    finalHolder.Children.Add(
                        new Label()
                        {
                            Text = suggestionInput.Text,
                            HorizontalTextAlignment = TextAlignment.Center,
                            TextColor = Color.FromHex("#4c466c")
                        });

                    finalHolder.Children.Add(
                        new Label()
                        {
                            Text = "Sent",
                            FontAttributes = FontAttributes.Italic,
                            HorizontalOptions = LayoutOptions.End,
                            TextColor = Color.FromHex("#4c466c")
                        });

                    scrollView.Content = finalHolder;

                    inputStack.Children.Clear();

                    inputStack.Children.Add(scrollView);
                }
            }
            else
            {
                Navigation.RemovePage(this);
            }
        }

        public async Task<TranslateHelper> getTranslation(string sourceLanguage, string toLanguage, string text)
        {
            HttpClient goTrans = new HttpClient();

            //await DisplayAlert("Alert", text, "Retry");

            var my = await goTrans.GetStringAsync("https://translate.googleapis.com/translate_a/single?client=gtx&sl=" + sourceLanguage + "&tl=" + toLanguage + "&dt=t&q=" + text + "");


            my = my.Replace("[", "");
            my = my.Replace("[", "");
            my = my.Replace("null,", "");

            object[] ba = my.Split(',');

            TranslateHelper converter = new TranslateHelper();

            converter.translated = ba[0].ToString().Replace('"', '\0');

            converter.source = ba[1].ToString().Replace('"', '\0');


            return converter;
        }

        private async void engTranslator_Clicked(object sender, EventArgs e)
        {
            langLoader.IsVisible = true;
            langLoader.IsRunning = true;
            engTranslator.IsVisible = false;
            frTranslator.IsVisible = false;
            swTranslator.IsVisible = false;
            if (!langSource.Equals("en"))
            {
                TranslateHelper titleTranslate = await getTranslation(langSource, "en", convertSourceWords(mainTrans.title));
                TranslateHelper detailTranslate = await getTranslation(langSource, "en", convertSourceWords(mainTrans.content));

                titleText.Text = titleTranslate.translated;
                contentText.Text = detailTranslate.translated.Replace("pp90", "\n").Replace(" 4u0K ", ",").Replace("4u0K ", ",");
            }

            else
            {
                titleText.Text = mainTrans.title;
                contentText.Text = mainTrans.content;
            }
            engTranslator.IsVisible = true;
            frTranslator.IsVisible = true;
            swTranslator.IsVisible = true;
            langLoader.IsVisible = false;
            langLoader.IsRunning = false;

            frTranslator.IsEnabled = true;
            engTranslator.IsEnabled = false;
            swTranslator.IsEnabled = true;

        }

        private async void swTranslator_Clicked(object sender, EventArgs e)
        {
            langLoader.IsVisible = true;
            langLoader.IsRunning = true;
            engTranslator.IsVisible = false;
            frTranslator.IsVisible = false;
            swTranslator.IsVisible = false;
            if (!langSource.Equals("sw"))
            {
                TranslateHelper titleTranslate = await getTranslation(langSource, "sw", convertSourceWords(mainTrans.title));
                TranslateHelper detailTranslate = await getTranslation(langSource, "sw", convertSourceWords(mainTrans.content));

                titleText.Text = titleTranslate.translated;
                contentText.Text = detailTranslate.translated.Replace("pp90", "\n").Replace(" 4u0K ", ",").Replace("4u0K ", ",");
            }

            else
            {
                titleText.Text = mainTrans.title;
                contentText.Text = mainTrans.content;
            }
            engTranslator.IsVisible = true;
            frTranslator.IsVisible = true;
            swTranslator.IsVisible = true;
            langLoader.IsVisible = false;
            langLoader.IsRunning = false;

            frTranslator.IsEnabled = true;
            engTranslator.IsEnabled = true;
            swTranslator.IsEnabled = false;
        }

        private async void frTranslator_Clicked(object sender, EventArgs e)
        {
            langLoader.IsVisible = true;
            langLoader.IsRunning = true;
            engTranslator.IsVisible = false;
            frTranslator.IsVisible = false;
            swTranslator.IsVisible = false;
            if (!langSource.Equals("fr"))
            {
                TranslateHelper titleTranslate = await getTranslation(langSource, "fr", convertSourceWords(mainTrans.title));
                TranslateHelper detailTranslate = await getTranslation(langSource, "fr", convertSourceWords(mainTrans.content));

                titleText.Text = titleTranslate.translated;
                contentText.Text = detailTranslate.translated.Replace("pp90", "\n").Replace(" 4u0K ", ",").Replace("4u0K ", ",");
            }
            else
            {
                titleText.Text = mainTrans.title;
                contentText.Text = mainTrans.content;
            }
            langLoader.IsVisible = false;
            langLoader.IsRunning = false;
            engTranslator.IsVisible = true;
            frTranslator.IsVisible = true;
            swTranslator.IsVisible = true;

            frTranslator.IsEnabled = false;
            engTranslator.IsEnabled = true;
            swTranslator.IsEnabled = true;

        }

        public string convertSourceWords(string text)
        {

            text = text.Replace(",", " 4u0K ");
            text = text.Replace("\r", " pp90 ");
            text = text.Replace(" ", "+");

            return text;
        }

        public async void checkLanguage(string content)
        {
            try
            {
                string text = await detectLanguage(content);
                langSource = text;
            }
            catch (Exception error)
            {
                await DisplayAlert("Connection alert!", error.Message, "Retry");
            }



            if (langSource.Equals("en"))
            {
                engTranslator.IsEnabled = false;
                frTranslator.IsEnabled = true;
                swTranslator.IsEnabled = true;
                ////likeBtn.IsEnabled = true;
            }
            if (langSource.Equals("fr"))
            {
                frTranslator.IsEnabled = false;
                engTranslator.IsEnabled = true;
                swTranslator.IsEnabled = true;
                ////likeBtn.IsEnabled = true;
            }
            if (langSource.Equals("sw"))
            {
                swTranslator.IsEnabled = false;
                engTranslator.IsEnabled = true;
                frTranslator.IsEnabled = true;
                ////likeBtn.IsEnabled = true;

            }
            //await DisplayAlert("Wat language is the text in", text, "Close");
        }

        public async Task<string> detectLanguage(string content)
        {
            HttpClient client = new HttpClient();

            var res = await client.GetStringAsync("http://apilayer.net/api/detect?access_key=5682b2de4f4af737e043ebcd7b36542c& query=" + content + ".");
            //We Changed The Percentage Property From Int To Double.
            var results = Newtonsoft.Json.JsonConvert.DeserializeObject<DetectorLanguageControler.RootObject>(res);
            return results.results[0].language_code;
        }
     

        public string detectLanguageConverter(String text)
        {
            text = text.Replace(" ", "%20");

            return text;
        }

        public async void LoadingDetails()
        {
            bigStack.IsVisible = false;
            loadingimageforpage.IsVisible = true;
            loadingimageforpage.IsRunning = true;
           
            ImageManagement management = new ImageManagement();
            foreach (var item in ListOfNotices.valueList)
            {
                if(title.Equals(DataConverter.ConvertToString(item.Title)))
                {
                    stringImage = item.Image;
                }
            }
            imageNews.Source = management.ByteArrayToImageSource(stringImage);

            titleText.Text = title;
            dateText.Text = date;
            contentText.Text = content;

            loadingimageforpage.IsVisible = false;
            loadingimageforpage.IsRunning = false;
            bigStack.IsVisible = true;
        }

        List<string> LikesKeys = new List<string>();
        List<UserLikes> LikesValues = new List<UserLikes>();

        //public void ShowLikes()
        //{
        //    Dictionary<String, UserLikes> dictionary = LoadDataOffline.AllUserLikesDictionnary;

        //    if (dictionary != null)
        //    {
        //        LikesKeys = dictionary.Select(k => k.Key).ToList();
        //        LikesValues = dictionary.Select(v => v.Value).ToList();

        //        if (LikesValues == null && LikesKeys == null)
        //        {
        //            likeNumbers.Text = "0" + " Likes";
        //        }
        //        else
        //        {
        //            var TempArray = LikesValues.FindAll(m => DataConverter.ConvertToString(m.NoticeTitle).Contains(title));
        //            likeNumbers.Text = TempArray.Count.ToString() + " Likes";

        //        }
        //    }

        //}


        List<string> ViewKeys = new List<string>();
        List<UserViews> ViewValues = new List<UserViews>();

        //public async void ShowView()
        //{
        //    Dictionary<String, UserViews> dictionary = LoadDataOffline.AllUserViewsDictionnary;

        //    if (dictionary != null)
        //    {
        //        ViewKeys = dictionary.Select(k => k.Key).ToList();
        //        ViewValues = dictionary.Select(v => v.Value).ToList();

        //        if (ViewKeys == null && ViewValues == null)
        //        {
        //            viewNumbers.Text = "0" + " Views";
        //        }
        //        else
        //        {
        //            var TempArray = LikesValues.FindAll(m => DataConverter.ConvertToString(m.NoticeTitle).Contains(title));
        //            viewNumbers.Text = TempArray.Count.ToString() + " Views";

        //        }
        //    }

        //}



        public async void SendingSuggestion()
        {
            if (suggestionInput.Text.Length > 10)
            {
                Suggestion suggestion = new Suggestion(DataConverter.ConvertToByteArray(App.currentUser), DataConverter.ConvertToByteArray(App.currentPhoneNumber), DataConverter.ConvertToByteArray(App.currentAddress), DataConverter.ConvertToByteArray(suggestionInput.Text), DataConverter.ConvertToByteArray(title), DataConverter.ConvertToByteArray(content), DataConverter.ConvertToByteArray(DateTime.Now.ToString()));

                Task task = Task.Run(() =>
                {
                    firebaseSuggestion.AddNewUser(suggestion, "DN_Suggestions");
                    isSuggestionSent = true;
                });
                await Task.WhenAll(task);

           
            }
            else
            {
                isSuggestionSent = false;
                await DisplayAlert("Error!", "Your suggestion is too short!", "Close");
            }
        }

        private void viewSuggestionButton_Clicked(object sender, EventArgs e)
        {
            ViewSuggestions.title = title;
            ViewSuggestions.content = content;
            Navigation.PushAsync(new ViewSuggestions());
        }


        //private async void //likeBtn_Clicked(object sender, EventArgs e)
        //{
        //    if (App.currentUser != null)
        //    {
        //        await AddUserLikes();
        //    }
        //}

        public async Task AddUserLikes()
        {
            try
            {
                if (LoadDataOffline.AllUserLikesDictionnary == null)
                {
                    UserLikes user = new UserLikes(DataConverter.ConvertToByteArray(App.currentUser), DataConverter.ConvertToByteArray(title));

                    Task task = Task.Run(() =>
                    {
                        fireBaseData.AddNewUser(user, "DN_UserLikes");
                    });
                    await Task.WhenAll(task);

                    //likeBtn.Text = "Unlike";
                }

                else
                {
                    foreach (var oneLike in LoadDataOffline.AllUserLikesDictionnary)
                    {
                        if (!(DataConverter.ConvertToString(oneLike.Value.Username).Equals(App.currentUser)))
                        {
                            UserLikes user = new UserLikes(DataConverter.ConvertToByteArray(App.currentUser), DataConverter.ConvertToByteArray(title));

                            Task task = Task.Run(() =>
                            {
                                fireBaseData.AddNewUser(user, "DN_UserLikes");
                            });
                            await Task.WhenAll(task);

                            //likeBtn.Text = "Unlike";

                        }


                        else
                        {
                            await DisplayAlert("Liked", "This is already liked", "Close");
                        }
                    }
                }


            }

            catch (Exception)
            {

            }

        }

    }
}