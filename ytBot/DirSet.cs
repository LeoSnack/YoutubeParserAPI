using System;
using System.IO;
using System.Collections.Generic;
using System.Text;


namespace ytBot
{
    class DirSet
    {
        string myDoc = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        string content;

        public void ResultFileDataWrite(string key, List<string> dataContent)
        {
            string newFileName = myDoc + @"/MailYT/Итоговые файлы/" + key + ".csv";
            content = dataContent[0] + ";" + dataContent[1] + ";" + dataContent[2] + ";" + dataContent[3] + ";" + dataContent[4] + Environment.NewLine;
            File.AppendAllText(newFileName, content);
        }

        public void ResultFileHeadMake(string key)
        {
            string newFileName = myDoc + @"/MailYT/Итоговые файлы/" + key + ".csv";

            if (!File.Exists(newFileName))
            {
                string clientHeader = "Link" + ";" + "Sub" + ";" + "Activity" + ";" + "E-mail" + ";" + "Country" + Environment.NewLine;

                File.WriteAllText(newFileName, clientHeader);
            }
        }

        public void MakeDirectory(string projectName)
        {
            bool visibleFolder = Directory.Exists(myDoc + @"/" + projectName);

            if (!visibleFolder)
            {
                Directory.CreateDirectory(myDoc + @"/" + projectName);
            }
        }

        public void MakeDirectoryInPD(string projectName, string nameFolder)
        {
            bool visibleFolder = Directory.Exists(myDoc + @"/" + projectName + "/" + nameFolder);

            if (!visibleFolder)
            {
                Directory.CreateDirectory(myDoc + @"/" + projectName + "/" + nameFolder);
            }
        }

        public void MakeFileTXT(string projectName, string nameFile)
        {
            bool visibleFile = File.Exists(myDoc + @"/" + projectName + "/" + nameFile + ".txt");

            if (!visibleFile)
            {
                using (var stream = new FileStream(myDoc + @"/" + projectName + "/" + nameFile + ".txt", FileMode.Create)) ;
            }
        }

        public void makeFolder(string projectName, string nameFolder)
        {
            Directory.CreateDirectory(myDoc + @"/" + projectName + "/" + nameFolder);
        }

        
        public void EmailDescAdd(string Mail)
        {
            using (StreamWriter sw = new StreamWriter(myDoc + @"/ytBot/Main/Email/EmailDescription.txt", true, System.Text.Encoding.UTF8))
            {
                sw.WriteLine(Mail);
            }
        }
        

        public void EmailListAdd(string Mail)
        {
            using (StreamWriter sw = new StreamWriter(myDoc + @"/ytBot/Main/browserList.txt", true, System.Text.Encoding.UTF8))
            {
                sw.WriteLine(Mail);
            }
        }

        public void FileClear(string path)
        {
            File.WriteAllText(path, String.Empty);
        }
    }
}
