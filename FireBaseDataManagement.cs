using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DzalekaNotifierFinal
{
    public class FireBaseDataManagement<T>
    {
        HttpClient httpclient = new HttpClient();
        private static string url = "https://dzaleka-notifier-firebase.firebaseio.com/Dzaleka Notifier/";
        public static List<T> dataList = new List<T>();
        public static string json = ".json";


        public async Task<Dictionary<string, T>> GetAllUsers(string dataName)
        {
            Dictionary<String, T> res = new Dictionary<string, T>();

            try
            {
                var result = await httpclient.GetStringAsync(url + dataName + json);
                res = JsonConvert.DeserializeObject<Dictionary<string, T>>(result);
                App.isNetworkAvailable = true;
            }
            catch (Exception)
            {
                App.isNetworkAvailable = false;
            }

            return res;
        }

        public async void AddNewUser(T User, string dataName)
        {
            try
            {
                var infoRef = JsonConvert.SerializeObject(User);
                var content = new StringContent(infoRef);
                App.isNetworkAvailable = true;
                var sendData = await httpclient.PostAsync(url + dataName + json, content);

            }

            catch (Exception e)
            {
                string name = (e.Message);
                App.isNetworkAvailable = false;
            }
        }

        public async Task<String> AddNewUserWithID(T User, string dataName)
        {
            try
            {
                var infoRef = JsonConvert.SerializeObject(User);
                var content = new StringContent(infoRef);
                App.isNetworkAvailable = true;
                var sendData = await httpclient.PostAsync(url + dataName + json, content);
                var result = sendData.Content.ReadAsStringAsync();
                var temp = result.Result.ToString().Split('=');
                var tempTwo = temp[0].ToString().Split(':');
                var tempResult = tempTwo[1].Replace("\"", "").Replace("}", "").Replace('"', '\0');
                var tem = tempResult;
                return tempResult;


            }
            catch (Exception)
            {
                App.isNetworkAvailable = false;
                return null;
            }
        }

        public async void UpdateUser(string databaseName, string userID, T Refugeeusers)
        {
            try
            {
                var datajson = JsonConvert.SerializeObject(Refugeeusers);
                var content = new StringContent(datajson);
                var sendData = await httpclient.PutAsync(url + databaseName + "/" + userID + json, content);
                App.isNetworkAvailable = true;
            }
            catch (Exception)
            {
                App.isNetworkAvailable = false;
            }
        }

        public async void DeleteNotifier(string userId, string tableName)
        {
            try
            {
                var res = await httpclient.DeleteAsync(url + tableName + "/" + userId + json);
                App.isNetworkAvailable = true;
            }
            catch (Exception)
            {
                App.isNetworkAvailable = false;
            }

        }

        public async void ResetPassword(T card, string tableName)
        {
            var datajson = JsonConvert.SerializeObject(card);
            var content = new StringContent(datajson);
            var sendData = await httpclient.PutAsync(url + tableName + "/" + json, content);
        }
    }
}


