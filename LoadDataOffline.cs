using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DzalekaNotifierFinal
{
    public class LoadDataOffline
    {

        public static List<String> allUsersKeys = new List<string>();
        public static List<Users> allUsersValues = new List<Users>();

        public static List<String> allUsersImagesKeys = new List<string>();
        public static List<UserImageSource> allUsersImagesValues = new List<UserImageSource>();

        public static List<String> allOrgsKeys = new List<string>();
        public static List<OrganizationProperties> allOrgsValues = new List<OrganizationProperties>();

        public static List<String> allOrgsImagesKeys = new List<string>();
        public static List<OrganizationImageSource> allOrgsImagesValues = new List<OrganizationImageSource>();

        public static List<String> allFedKeys = new List<string>();
        public static List<Feedback> allFedValues = new List<Feedback>();

        public static List<String> allNotKeys = new List<string>();
        public static List<Notifications> allNotValues = new List<Notifications>();

        public static List<String> allNotImagesKeys = new List<string>();
        public static List<NotificationImageSource> allNotImagesValues = new List<NotificationImageSource>();

        public static List<String> allQueKeys = new List<string>();
        public static List<QuestionsProperties> allQueValues = new List<QuestionsProperties>();

        public static List<String> allSugKeys = new List<string>();
        public static List<Suggestion> allSugValues = new List<Suggestion>();

        public static List<String> allLikKeys = new List<string>();
        public static List<UserLikes> allLikValues = new List<UserLikes>();

        public static List<String> allVieKeys = new List<string>();
        public static List<UserViews> allVieValues = new List<UserViews>();



        /* ALL USERS */
        public static Dictionary<String, Users> AllUsersDictionary;

        public static async void LoadAllUsers()
        {
            FireBaseDataManagement<Users> UserDatas = new FireBaseDataManagement<Users>();
            AllUsersDictionary = await UserDatas.GetAllUsers("DN_Users");
            if (AllUsersDictionary != null)
            {
                allUsersKeys = AllUsersDictionary.Select(m => m.Key).ToList();
                allUsersValues = AllUsersDictionary.Select(k => k.Value).ToList();
            }
        }

        /* ALL USERS IN TEMPO DATABASE */
        public static Dictionary<String, Users> AllUsersTempDictionary;
        public static async void LoadAllUsersTemp()
        {
            FireBaseDataManagement<Users> UserDatas = new FireBaseDataManagement<Users>();
            AllUsersTempDictionary = await UserDatas.GetAllUsers("DN_Users_Temp");
            if (AllUsersTempDictionary != null)
            {
                allUsersKeys = AllUsersTempDictionary.Select(m => m.Key).ToList();
                allUsersValues = AllUsersTempDictionary.Select(k => k.Value).ToList();
            }
        }

        /* ALL USERS IMAGESOURCE */
        public static Dictionary<String, UserImageSource> AllUsersImageSourceDictionary;

        public static async void LoadAllUsersImageSource()
        {
            FireBaseDataManagement<UserImageSource> UserDatas = new FireBaseDataManagement<UserImageSource>();
            AllUsersImageSourceDictionary = await UserDatas.GetAllUsers("DN_Users");
            if (AllUsersImageSourceDictionary != null)
            {
                allUsersImagesKeys = AllUsersImageSourceDictionary.Select(m => m.Key).ToList();
                allUsersImagesValues = AllUsersImageSourceDictionary.Select(k => k.Value).ToList();
            }

        }
        /* ALL OrganizationProperties */
        public static Dictionary<String, OrganizationProperties> AllOrganizationPropertiesDictionnary;
        public static async void LoadAllOrganizationProperties()
        {
            FireBaseDataManagement<OrganizationProperties> UserDatas = new FireBaseDataManagement<OrganizationProperties>();
            AllOrganizationPropertiesDictionnary = await UserDatas.GetAllUsers("DN_OrganizationProperties");
            if (AllOrganizationPropertiesDictionnary != null)
            {
                allOrgsKeys = AllOrganizationPropertiesDictionnary.Select(m => m.Key).ToList();
                allOrgsValues = AllOrganizationPropertiesDictionnary.Select(k => k.Value).ToList();
            }

        }

        /* ALL OrganizationProperties WITH IMAGE */
        public static Dictionary<String, OrganizationImageSource> AllOrganizationImageDictionnary;
        public static async void LoadAllOrganizationImage()
        {
            FireBaseDataManagement<OrganizationImageSource> UserDatas = new FireBaseDataManagement<OrganizationImageSource>();
            AllOrganizationImageDictionnary = await UserDatas.GetAllUsers("DN_OrganizationProperties");

            if (AllOrganizationImageDictionnary != null)
            {
                allOrgsImagesKeys = AllOrganizationImageDictionnary.Select(m => m.Key).ToList();
                allOrgsImagesValues = AllOrganizationImageDictionnary.Select(k => k.Value).ToList();
            }
        }

        /* ALL FEEDBACKS */
        public static Dictionary<String, Feedback> AllFeedBacksDictionnary;
        public static async void LoadAllFeedBacks()
        {
            FireBaseDataManagement<Feedback> UserDatas = new FireBaseDataManagement<Feedback>();
            AllFeedBacksDictionnary = await UserDatas.GetAllUsers("DN_FeedBacks");
            if (AllFeedBacksDictionnary != null)
            {
                allFedKeys = AllFeedBacksDictionnary.Select(m => m.Key).ToList();
                allFedValues = AllFeedBacksDictionnary.Select(k => k.Value).ToList();
            }
        }

        /* ALL NOTIFICATIONS */
        public static Dictionary<String, Notifications> AllNotificationsDictionnary;
        public static async void LoadAllNotifications()
        {
            FireBaseDataManagement<Notifications> UserDatas = new FireBaseDataManagement<Notifications>();
            AllNotificationsDictionnary = await UserDatas.GetAllUsers("DN_Notifications");
            if (AllNotificationsDictionnary != null)
            {
                allNotKeys = AllNotificationsDictionnary.Select(m => m.Key).ToList();
                allNotValues = AllNotificationsDictionnary.Select(k => k.Value).ToList();
            }

        }

        /* ALL NOTIFICATIONS WITH IMAGE */
        public static Dictionary<String, NotificationImageSource> AllNotificationImageSourceDictionnary;
        public static async void LoadAllNotificationImageSource()
        {
            FireBaseDataManagement<NotificationImageSource> UserDatas = new FireBaseDataManagement<NotificationImageSource>();
            AllNotificationImageSourceDictionnary = await UserDatas.GetAllUsers("DN_Notifications");
            if (AllNotificationImageSourceDictionnary != null)
            {
                allNotImagesKeys = AllNotificationImageSourceDictionnary.Select(m => m.Key).ToList();
                allNotImagesValues = AllNotificationImageSourceDictionnary.Select(k => k.Value).ToList();
            }
        }

        /* ALL QUESTIONS AND ANSWERS */
        public static Dictionary<String, QuestionsProperties> AllQuestionsDictionnary;
        public static async void LoadAllQuestions()
        {
            FireBaseDataManagement<QuestionsProperties> UserDatas = new FireBaseDataManagement<QuestionsProperties>();
            AllQuestionsDictionnary = await UserDatas.GetAllUsers("DN_Questions");
            if (AllQuestionsDictionnary != null)
            {
                allQueKeys = AllQuestionsDictionnary.Select(m => m.Key).ToList();
                allQueValues = AllQuestionsDictionnary.Select(k => k.Value).ToList();
            }
        }

        /* ALL SUGGESTIONS */
        public static Dictionary<String, Suggestion> AllSuggestionsDictionnary;
        public static async void LoadAllSuggestions()
        {
            FireBaseDataManagement<Suggestion> UserDatas = new FireBaseDataManagement<Suggestion>();
            AllSuggestionsDictionnary = await UserDatas.GetAllUsers("DN_Suggestions");
            if (AllSuggestionsDictionnary != null)
            {
                allSugKeys = AllSuggestionsDictionnary.Select(m => m.Key).ToList();
                allSugValues = AllSuggestionsDictionnary.Select(k => k.Value).ToList();
            }
        }

        /* ALL USERLIKES */
        public static Dictionary<String, UserLikes> AllUserLikesDictionnary;
        public static async void LoadAllLikes()
        {
            FireBaseDataManagement<UserLikes> UserDatas = new FireBaseDataManagement<UserLikes>();
            AllUserLikesDictionnary = await UserDatas.GetAllUsers("DN_UserLikes");
            if (AllUserLikesDictionnary != null)
            {
                allLikKeys = AllUserLikesDictionnary.Select(m => m.Key).ToList();
                allLikValues = AllUserLikesDictionnary.Select(k => k.Value).ToList();
            }
        }

        /* ALL USERVIEWS */
        public static Dictionary<String, UserViews> AllUserViewsDictionnary;
        public static async void LoadAllViews()
        {
            FireBaseDataManagement<UserViews> UserDatas = new FireBaseDataManagement<UserViews>();
            AllUserViewsDictionnary = await UserDatas.GetAllUsers("DN_UserViews");
            if (AllUserViewsDictionnary != null)
            {
                allVieKeys = AllUserViewsDictionnary.Select(m => m.Key).ToList();
                allVieValues = AllUserViewsDictionnary.Select(k => k.Value).ToList();
            }

        }

    }
}
