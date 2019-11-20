using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text;

namespace ytBot
{
    public class YoutubeApiHelper
    {
        DirSet directory = new DirSet();

        string myDoc = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        string videoID;
        string descChannel;
        string subChannel;
        string countryChannel;


        public void transformLinkToID(List<string> UserName)
        {

            foreach (string Name in UserName)
            {
                string url = "https://www.googleapis.com/youtube/v3/channels?key=AIzaSyDGw2KE59_W_9RTba-ANH3uRhYJXKeFjEk&forUsername=" + Name + "&part=id";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                request.ContentType = "application/json; charset=UTF-8";
                request.Accept = "application/json, text/plain, */*";
                request.Headers.Add("Accept-Language: ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
                request.Host = "www.googleapis.com";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.146 Safari/537.36";


                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Encoding encode = System.Text.Encoding.UTF8;


                using (var reader = new StreamReader(response.GetResponseStream(), encode))
                {
                    var myStr = reader.ReadToEnd();
                    byte[] bytes = Encoding.UTF8.GetBytes(myStr);
                    string responseText = Encoding.Default.GetString(bytes);

                    dynamic json = JObject.Parse(responseText);

                    if (json.items.Count != 0)
                    {
                        string linkChannel = "https://www.youtube.com/channel/" + json.items[0].id;
                        File.AppendAllText(myDoc + @"/PRHunter/YouTube/getID/output.txt", linkChannel + Environment.NewLine);
                    }
                    else
                    {
                        File.AppendAllText(myDoc + @"/PRHunter/YouTube/getID/output.txt", "Неверный UserName" + Environment.NewLine);
                    }

                }
            }
        }
        
        public string getVideoID(string channelID)
        {
            string url = "https://www.googleapis.com/youtube/v3/activities?maxResults=1&channelId=" + channelID + "&part=contentDetails&key=AIzaSyDGw2KE59_W_9RTba-ANH3uRhYJXKeFjEk";//&maxResults=2&order=date&type=video&key=AIzaSyDGw2KE59_W_9RTba-ANH3uRhYJXKeFjE

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            request.ContentType = "application/json; charset=UTF-8";
            request.Accept = "application/json, text/plain, */*";
            request.Headers.Add("Accept-Language: ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            request.Headers.Add("X-Requested-With: XMLHttpRequest");
            request.Headers.Add("X-Referer: https://google-developers.appspot.com");
            request.Headers.Add("X-Origin: https://google-developers.appspot.com");
            request.Host = "content.googleapis.com";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.146 Safari/537.36";


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Encoding encode = Encoding.UTF8;


            using (var reader = new StreamReader(response.GetResponseStream(), encode))
            {
                var myStr = reader.ReadToEnd();
                byte[] bytes = Encoding.UTF8.GetBytes(myStr);
                string responseText = Encoding.UTF8.GetString(bytes);

                dynamic json = JObject.Parse(responseText);

                

                if (json.items.Count != 0 && json.items[0].contentDetails != null && json.items[0].contentDetails.upload != null)
                {
                    videoID = json.items[0].contentDetails.upload.videoId;
                }
                else if(json.items.Count != 0 && json.items[0].contentDetails != null && json.items[0].contentDetails.like != null)
                {
                    videoID = json.items[0].contentDetails.like.resourceId.videoId;
                }    

                return videoID;
            }
        }

        public string getDateVideo(string videoID)
        {
            string url = "https://www.googleapis.com/youtube/v3/videos?id=" + videoID + "&part=snippet&key=AIzaSyDGw2KE59_W_9RTba-ANH3uRhYJXKeFjEk";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            request.ContentType = "application/json; charset=UTF-8";
            request.Accept = "application/json, text/plain, */*";
            request.Headers.Add("Accept-Language: ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            request.Headers.Add("X-Requested-With: XMLHttpRequest");
            request.Headers.Add("X-Referer: https://google-developers.appspot.com");
            request.Headers.Add("X-Origin: https://google-developers.appspot.com");
            request.Host = "content.googleapis.com";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.146 Safari/537.36";


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Encoding encode = System.Text.Encoding.UTF8;


            using (var reader = new StreamReader(response.GetResponseStream(), encode))
            {
                var myStr = reader.ReadToEnd();
                byte[] bytes = Encoding.UTF8.GetBytes(myStr);
                string responseText = Encoding.Default.GetString(bytes);

                dynamic json = JObject.Parse(responseText);

                string dateVideo = json.items[0].snippet.publishedAt;
                dateVideo = dateVideo.Remove(10, dateVideo.Length - 10);

                return dateVideo;
            }
        }
        

        public List<string> channelSubCountry(string id) 
        {
            List<string> data = new List<string>();

            string url = "https://www.googleapis.com/youtube/v3/channels?id=" + id + "&part=snippet,statistics&key=AIzaSyDGw2KE59_W_9RTba-ANH3uRhYJXKeFjEk";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                request.ContentType = "application/json; charset=UTF-8";
                request.Accept = "*/*";
                request.Referer = "https://content.googleapis.com/static/proxy.html?usegapi=1&jsh=m%3B%2F_%2Fscs%2Fapps-static%2F_%2Fjs%2Fk%3Doz.gapi.ru.OJ73qm2Ow7U.O%2Fm%3D__features__%2Fam%3DAQE%2Frt%3Dj%2Fd%3D1%2Frs%3DAGLTcCMdmnobHNfiU8BYhAB27btjQPKtKg";
                request.Headers.Add("Accept-Language: ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
                request.Headers.Add("X-Requested-With: XMLHttpRequest");
                request.Headers.Add("X-Referer: https://google-developers.appspot.com");
                request.Headers.Add("X-Origin: https://google-developers.appspot.com");
                request.Host = "content.googleapis.com";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36";


                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Encoding encode = System.Text.Encoding.UTF8;


                using (var reader = new StreamReader(response.GetResponseStream(), encode))
                {
                    var myStr = reader.ReadToEnd();
                    byte[] bytes = Encoding.UTF8.GetBytes(myStr);
                    string responseText = Encoding.Default.GetString(bytes);

                    dynamic json = JObject.Parse(responseText);


                    if (json.items.Count != 0 && json.items[0].snippet.country != null)
                    {
                        countryChannel = json.items[0].snippet.country;
                    }
                    else
                    {
                        countryChannel = "No";
                    }

                    if (json.items.Count != 0 && json.items[0].statistics.subscriberCount != null)
                    {
                        subChannel = json.items[0].statistics.subscriberCount;                    }
                    else
                    {
                        subChannel = "No";
                    }

                    data.Add(subChannel);
                    data.Add(countryChannel);
                }

            return data;
            }

        public string EmailDescriptionGet(string ChannelID)
        {
            string url = "https://www.googleapis.com/youtube/v3/channels?id=" + ChannelID + "&part=snippet&key=AIzaSyDGw2KE59_W_9RTba-ANH3uRhYJXKeFjEk";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            request.ContentType = "application/json; charset=UTF-8";
            request.Accept = "*/*";
            request.Referer = "https://content.googleapis.com/static/proxy.html?usegapi=1&jsh=m%3B%2F_%2Fscs%2Fapps-static%2F_%2Fjs%2Fk%3Doz.gapi.ru.OJ73qm2Ow7U.O%2Fm%3D__features__%2Fam%3DAQE%2Frt%3Dj%2Fd%3D1%2Frs%3DAGLTcCMdmnobHNfiU8BYhAB27btjQPKtKg";
            request.Headers.Add("Accept-Language: ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
            request.Headers.Add("X-Requested-With: XMLHttpRequest");
            request.Headers.Add("X-Referer: https://google-developers.appspot.com");
            request.Headers.Add("X-Origin: https://google-developers.appspot.com");
            request.Host = "content.googleapis.com";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36";


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Encoding encode = Encoding.UTF8;


            using (var reader = new StreamReader(response.GetResponseStream(), encode))
            {
                var myStr = reader.ReadToEnd();
                byte[] bytes = Encoding.UTF8.GetBytes(myStr);
                string responseText = Encoding.Default.GetString(bytes);

                dynamic json = JObject.Parse(responseText);


                if(json.items.Count != 0)
                {
                    descChannel = json.items[0].snippet.description;
                }
                
                return descChannel;
            }
        }

        string pageToken;
        public void SearchData(string key)
        {
            for (int i = 0; i < 50; i++)
                {
                    string url = "https://www.googleapis.com/youtube/v3/search?part=snippet&maxResults=50&pageToken=" + pageToken + "&q=" + key + "&type=video&key=AIzaSyDGw2KE59_W_9RTba-ANH3uRhYJXKeFjEk";
                    

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";

                    request.Accept = "*/*";
                    request.Headers.Add("Accept-Language: ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
                    request.Headers.Add("X-Requested-With: XMLHttpRequest");
                    request.Headers.Add("X-Referer: https://google-developers.appspot.com");
                    request.Headers.Add("X-Origin: https://google-developers.appspot.com");
                    request.Host = "content.googleapis.com";
                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.146 Safari/537.36";


                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Encoding encode = System.Text.Encoding.UTF8;


                    using (var reader = new StreamReader(response.GetResponseStream(), encode))
                    {
                        var myStr = reader.ReadToEnd();
                        byte[] bytes = Encoding.UTF8.GetBytes(myStr);
                        string responseText = Encoding.Default.GetString(bytes);

                        dynamic json = JObject.Parse(responseText);

                        pageToken = json.nextPageToken;
                        
                        if(json.items.Count != 0)
                        {
                            for (int intKey = 0; intKey < json.items.Count; intKey++)
                            {
                                string curId = json.items[intKey].snippet.channelId;

                                using (StreamWriter sw = new StreamWriter(myDoc + @"/MailYT/Собранные ключи/" + key + ".txt", true, Encoding.UTF8))
                                {
                                    sw.WriteLine(curId); // Записываем ID каналов
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
        }
    }
}
