using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DzalekaNotifierFinal
{
    public class SendSmsToUsers
    {

        public static string codeSaved;

        public async static void SendSMSMessage(String phoneNumber, String messageBody)
        {
            phoneNumber = phoneNumber.Trim().Replace(" ", "");
            string url = "https://api.twilio.com/2010-04-01/Accounts/AC6ffb66b3cd44ee6382966349e7f1cf20/Messages.json";

            try
            {
                using (var handle = new HttpClientHandler())
                {
                    handle.Credentials = new System.Net.NetworkCredential("AC6ffb66b3cd44ee6382966349e7f1cf20", "5f69fbff09e38ebc5687b88c75070633");
                    using (var client = new HttpClient(handle))
                    {
                        var requestContent = new FormUrlEncodedContent(new[]
                        {
                                new KeyValuePair<string, string>("To", phoneNumber.Trim()),
                                new KeyValuePair<string, string>("From", "+14155239121"),
                                new KeyValuePair<string, string>("Body", messageBody.Trim())
                            });

                        HttpResponseMessage msm = await client.PostAsync(url, requestContent);
                        //await context.DisplayAlert("Notice", msm.ReasonPhrase, "Close");
                    }
                }
            }
            catch (Exception e)
            {

            }


        }
    }
}
