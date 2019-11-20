using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ytBot
{
    class MainFunction
    {

        YoutubeApiHelper _youtubeApiHelper = new YoutubeApiHelper();
        ProcessDate taskDate = new ProcessDate();
        DirSet directory = new DirSet();


        List<string> listOfKey = new List<string>();
        string myDoc = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        List<string> linkU = new List<string>();
        string mailResponse;

        public void dataKeyCollect(string key)
        {
            using (var stream = new FileStream(myDoc + @"/MailYT/Собранные ключи/" + key + ".txt", FileMode.Create));
            _youtubeApiHelper.SearchData(key);
        }

        public string channelActive(string id)
        {
                string videoid = _youtubeApiHelper.getVideoID(id); // получаем id последнего видео на канале

                if (string.IsNullOrWhiteSpace(videoid))
                {
                    return "noActive";
                }
                else
                {
                    string ytdate = _youtubeApiHelper.getDateVideo(videoid);

                    if (taskDate.differenceBetweenDates(ytdate) < 3)
                    {
                        return "Active";
                    }
                    else
                    {
                        return "noActive";
                    }
                }
        }

        public string channelEmail(string id)
        {
            string EmailDesc = ExtractEmail(_youtubeApiHelper.EmailDescriptionGet(id));

             if (string.IsNullOrWhiteSpace(EmailDesc))
            {
                return "Null";
            }
            else
            {
                return EmailDesc;
            }
        }

        

        public string ExtractEmail(string MailString)
        {
            if (string.IsNullOrWhiteSpace(MailString))
            {
                return null;
            }
            else
            {
                Regex regex = new Regex(@"([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)");
                Match match = regex.Match(MailString);
                if (!match.Success)
                    return string.Empty;
                return match.Groups[0].Value;
            }
        }
    }
}
