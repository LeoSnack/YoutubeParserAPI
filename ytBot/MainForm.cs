using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace ytBot
{
    public partial class MainForm : Form
    {
        string myDoc = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        List<string> transformChannelLink = new List<string>();
        List<string> listOfKey = new List<string>();
        
        /*DATA FOR FINAL LIST*/

        List<string> linkU = new List<string>();
        List<string> activeU = new List<string>();
        List<string> countSubU = new List<string>();
        List<string> emailU = new List<string>();
        List<string> countryU = new List<string>();

        /*! DATA FOR FINAL LIST !*/


        YoutubeApiHelper _youtubeApiHelper = new YoutubeApiHelper();
        ProcessDate taskDate = new ProcessDate();
        DirSet directory = new DirSet();
        MainFunction MainFunc = new MainFunction();
        


        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {/*
            List<Task> Tasks_List = new List<Task>();

            for (int i = 0; i < 2; i++)
            {
                Tasks_List.Add(
                      Task.Run(() =>
                        data.Add(MainFunc.channelEmail("UCeSR0h9NP5FX2hJQlgFEGHw"))
                      )
                 );
            }

            Task.WaitAll(Tasks_List.ToArray());

            int k = 0;*/

            directory.MakeDirectory("MailYT");
            directory.MakeDirectoryInPD("MailYT", "Собранные ключи");
            directory.MakeDirectoryInPD("MailYT", "Итоговые файлы");
            directory.MakeFileTXT("MailYT", "Ключи");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int countLine = File.ReadAllLines(myDoc + @"/MailYT/Ключи.txt").Length;


            for(int i = 0; i < countLine; i++)
            {
                string key = File.ReadLines(myDoc + @"/MailYT/Ключи.txt", Encoding.Default).Skip(i).First();
                directory.ResultFileHeadMake(key);
                MainFunc.dataKeyCollect(key);
                var idChannel = File.ReadAllLines(myDoc + @"/MailYT/Собранные ключи/" + key + ".txt").ToList();
                var uniqID = idChannel.Distinct().ToList<string>();

                foreach (string id in uniqID)
                {
                    List<string> channelInfo = _youtubeApiHelper.channelSubCountry(id);
                    List<string> data = new List<string>();
                    data.Add("https://www.youtube.com/channel/" + id);
                    data.Add(channelInfo[0]);
                    data.Add(MainFunc.channelActive(id));
                    data.Add(MainFunc.channelEmail(id));
                    data.Add(channelInfo[1]);

                    directory.ResultFileDataWrite(key, data);
                    data.Clear();
                }
                uniqID.Clear();
            }


        }
    }
}
