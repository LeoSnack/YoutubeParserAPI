using System;

namespace ytBot
{
    class ProcessDate
    {
        public int differenceBetweenDates(string YouTubeVideoDate)
        {
            int[] dateCur = processCurDateToINT();
            int[] dateGet = processGetDateToINT(YouTubeVideoDate);

            DateTime get = new DateTime(dateGet[2], dateGet[1], dateGet[0]);
            DateTime cur = new DateTime(dateCur[2], dateCur[1], dateCur[0]);
            int diffMonths = (cur.Month + cur.Year * 12) - (get.Month + get.Year * 12);

            return diffMonths;
        }

        public int[] processCurDateToINT() // Элементы текущей даты 
        {
            int[] dateArray = new int[3];
            string curDate = DateTime.Today.ToString("d");

            dateArray[0] = Int32.Parse(curDate.Substring(0, 2)); // Day of Date
            dateArray[1] = Int32.Parse(curDate.Substring(3, 2)); // Month of Date
            dateArray[2] = Int32.Parse(curDate.Substring(6, 4)); // Year of Date

            return dateArray;
        }

        public int[] processGetDateToINT(string videoID) // Элементы даты, которую получаем
        {
            int[] dateArray = new int[3];

            dateArray[0] = Int32.Parse(videoID.Substring(3, 2)); // Day of Date
            dateArray[1] = Int32.Parse(videoID.Substring(0, 2)); // Month of Date
            dateArray[2] = Int32.Parse(videoID.Substring(6, 4)); // Year of Date

            return dateArray;
        }
    }
}
