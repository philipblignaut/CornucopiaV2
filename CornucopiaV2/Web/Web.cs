using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CornucopiaV2
{
    public static class Web
    {
        public static string GetWebPageHTML
            (string URL
            )
        {
            string html = C.es;
            // Create a new HttpWebRequest object.Make sure that 
            // a default proxy is set if you are behind a firewall.
            HttpWebRequest myHttpWebRequest1 =
              (HttpWebRequest)WebRequest.Create(URL);

            myHttpWebRequest1.KeepAlive = false;
            // Assign the response object of HttpWebRequest to a HttpWebResponse variable.
            HttpWebResponse myHttpWebResponse1 =
              (HttpWebResponse)myHttpWebRequest1.GetResponse();

            Console.WriteLine("\nThe HTTP request Headers for the first request are: \n{0}", myHttpWebRequest1.Headers);

            Stream streamResponse = myHttpWebResponse1.GetResponseStream();
            StreamReader streamRead = new StreamReader(streamResponse);
            Char[] readBuff = new Char[256];
            int count = streamRead.Read(readBuff, 0, 256);
            Console.WriteLine("The contents of the Html page are.......\n");
            string page = C.es;
            while (count > 0)
            {
                String outputData = new String(readBuff, 0, count);
                page += outputData;
                //Console.Write(outputData);
                count = streamRead.Read(readBuff, 0, 256);
            }
            //int ithe = page.IndexOf("2.7182818284590452353");
            //page = page.Substring(ithe, page.Length - ithe);
            html += page;
            //Console.WriteLine();
            // Close the Stream object.
            streamResponse.Close();
            streamRead.Close();
            // Release the resources held by response object.
            myHttpWebResponse1.Close();
            return html;

        }
    }
}
