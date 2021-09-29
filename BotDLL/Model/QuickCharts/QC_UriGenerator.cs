using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotDLL
{
    /// <summary>
    /// The quickchart uri generator.
    /// </summary>
    public class QC_UriGenerator
    {
        private static string token = "";
        private static int virgin = 0;
        /// <summary>
        /// Gets or sets the quickchart stat uri.
        /// </summary>
        public string QC_StatUri { get; set; }
        /// <summary>
        /// Creates the object.
        /// </summary>
        /// <param name="rawcontent">The rawcontent.</param>
        /// <returns>A QC_UriGenerator.</returns>
        public static QC_UriGenerator CreateObj(string rawcontent)
        {
            if (virgin == 0)
            {
                Connections connections = Connections.GetConnections();
                token = connections.QuickChartApi;
#if DEBUG
                token = connections.QuickChartApiDebug;
#endif
                virgin = 69;
            }
            QC_UriGenerator obj = new QC_UriGenerator();

            string content = RemoveTillWord(rawcontent, "var data", 0);

            string title = RemoveTillWord(content, "title", 39);
            title = RemoveAfterWord(title, "fontFamily", -7);
            title = title.Replace("\"", " ");
            content = RemoveAfterWord(content, "var ctx", 0);

            string label1 = RemoveTillWord(content, "label:", 7);
            string label2 = RemoveTillWord(label1, "label:", 7);

            label1 = RemoveAfterWord(label1, "fill", -6);
            label1 = RemoveAfterWord(label1, "fill", -6);
            label2 = RemoveAfterWord(label2, "fill", -6);

            label1 = label1.Replace('"', ' ').Trim();
            label2 = label2.Replace('"', ' ').Trim();

            string labels = RemoveTillWord(content, "labels", 9);
            labels = RemoveAfterWord(labels, "dataset", -5);
            labels = labels.Replace('"', ' ').Replace("\n", "").Replace("\t", "").Trim(',');

            string dataset1 = RemoveTillWord(content, "dataset", 11);
            dataset1 = RemoveAfterWord(dataset1, "label", -83);
            dataset1 = RemoveTillWord(dataset1, "data", 8);
            dataset1 = dataset1.Replace("\n", "").Replace("\t", "").Trim(',');

            string dataset2 = RemoveTillWord(content, "dataset", 11);
            dataset2 = RemoveTillWord(dataset2, "data", 8);
            dataset2 = RemoveTillWord(dataset2, "data", 8);
            dataset2 = RemoveAfterWord(dataset2, "borderColor", -7);
            dataset2 = dataset2.Replace("\n", "").Replace("\t", "").Trim(',');

            string[] arrlables1 = labels.Split(',');
            string[] arrlabels2 = new string[arrlables1.Length];
            if (arrlables1[0] == "")
            {
                string url = "https://quickchart.io/chart/render/" + token + "?title=" + title;
                url = url.Replace(" ", "%20");
                obj.QC_StatUri = url;
                return obj;
            }
            int i1 = 0;
            foreach (var item in arrlables1)
            {
                string[] splited = item.Split('-');
                string year = splited[0];
                string month = splited[1];
                string day = splited[2];
                arrlabels2[i1] = day.Trim() + '-' + month.Trim() + '-' + year.Trim();
                i1++;
            }

            string[] arrdataset1 = dataset1.Split(',');
            string[] arrdataset2 = dataset2.Split(',');

            string struri = "https://quickchart.io/chart/render/" + token + "?title=" + title + "&labels=";

            int i = 1;
            foreach (var item in arrlabels2)
            {
                struri += item;
                if (arrlabels2.Length != i)
                    struri += ',';
                i++;
            }
            struri += "&data1=";
            i = 1;
            foreach (var item in arrdataset1)
            {
                struri += item;
                if (arrdataset1.Length != i)
                    struri += ',';
                i++;
            }
            i = 1;
            if(arrdataset2.Length > 1)
            {
                struri += "&data2=";
                foreach (var item in arrdataset2)
                {
                    struri += item;
                    if (arrdataset2.Length > i)
                        struri += ',';
                    i++;
                }
            }

            obj.QC_StatUri = struri.Replace(" ", "%20");
            return obj;
        }
        /// <summary>
        /// Removes till word.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="word">The word.</param>
        /// <param name="removewordint">The removewordint.</param>
        /// <returns>A string.</returns>
        public static string RemoveTillWord(string input, string word, int removewordint)
        {
            return input.Substring(input.IndexOf(word) + removewordint);
        }
        /// <summary>
        /// Removes after word.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="word">The word.</param>
        /// <param name="keepwordint">The keepwordint.</param>
        /// <returns>A string.</returns>
        public static string RemoveAfterWord(string input, string word, int keepwordint)
        {
            int index = input.LastIndexOf(word);
            if (index > 0)
                input = input.Substring(0, index + keepwordint);

            return input;
        }
    }
}
