using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL
{
    public class DrawStatistic
    {
        public List<DateTime> DSDt { get; set; }
        public List<int> DSVal1 { get; set; }
        public List<int> DSVal2 { get; set; }

        public static DrawStatistic BuildStatistic(LF_ServerInfo info)
        {
            List<DateTime> tempDSDt = new List<DateTime>();
            List<int> tempDSVal1 = new List<int>();
            List<int> tempDSVal2 = new List<int>();

            string temp = info.QC_StatUri.ToString();
            temp = temp.Contains("labels=") ? RemoveTillWord(temp, "labels=", 7) : null;

            string labels = null;
            if (temp != null)
                labels = RemoveAfterWord(temp, "&data1=", 0);

            string data1 = null;
            if (temp != null)
            {
                data1 = RemoveTillWord(temp, "&data1=", 7);
                data1 = RemoveAfterWord(data1, "&data2=", 0);
            }

            string data2 = null;
            if (temp != null && temp.Contains("&data2="))
                data2 = RemoveTillWord(temp, "&data2=", 7);

            string[] slabels = null;
            if (labels != null)
            {
                slabels = labels.Split(',');
                foreach (var item in slabels)
                {
                    string[] dtstemp = item.Split('-');
                    DateTime dt = new DateTime(Convert.ToInt32(dtstemp[2]), Convert.ToInt32(dtstemp[1]), Convert.ToInt32(dtstemp[0]));
                    tempDSDt.Add(dt);
                }
            }

            string[] sDSVal1 = null;
            if (data1 != null)
            {
                sDSVal1 = data1.Split(',');
                foreach (var item in sDSVal1)
                {
                    int inttemp = Convert.ToInt32(item);
                    tempDSVal1.Add(inttemp);
                }
            }

            string[] sDSVal2 = null;
            if (data2 != null)
            {
                sDSVal2 = data2.Split(',');
                foreach (var item in sDSVal2)
                {
                    int inttemp2 = Convert.ToInt32(item);
                    tempDSVal2.Add(inttemp2);
                }
            }

            DrawStatistic dss = new DrawStatistic
            {
                DSDt = tempDSDt,
                DSVal1 = tempDSVal1,
                DSVal2 = tempDSVal2
            };

            return dss;
        }
        public static string RemoveTillWord(string input, string word, int removewordint)
        {
            return input.Substring(input.IndexOf(word) + removewordint);
        }
        public static string RemoveAfterWord(string input, string word, int keepwordint)
        {
            int index = input.LastIndexOf(word);
            if (index > 0)
                input = input.Substring(0, index + keepwordint);

            return input;
        }
    }
}
