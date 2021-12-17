//  #define DEBUG


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExamNP
{
    class Checker
    {
        public Checker()
        {
            IsChecked = new List<UrlInfo>();
            WaitChecking = new List<string>();
        }

        public struct UrlInfo
        {
            public string url;
            public HttpStatusCode httpStatusCode;
        }
        public List<string> WaitChecking { get; set; }
        public List<UrlInfo> IsChecked { get; set; }


        public void CheckUrl()
        {
            IsChecked.Clear();
            foreach (var item in WaitChecking)
            {

                Console.WriteLine($"Please wait, started check: {item}");

                UrlInfo urlInfo = new UrlInfo();
                urlInfo.url = item;

                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(item);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    //    Console.WriteLine($"StatusCode:{(int)response.StatusCode} --> ({response.StatusCode})");

                    urlInfo.httpStatusCode = response.StatusCode;

                 //   response.Close();
                }
                catch (WebException e)
                {
                    //      Console.WriteLine($"This program is expected to throw WebException on successful run." +
                    //          $"\n\nException Message :{ e.Message}");
                  //  Console.WriteLine($"{ e.Message}");

                    if (e.Status == WebExceptionStatus.ProtocolError)
                    {

                         //     Console.WriteLine($"Status Code : {((HttpWebResponse)e.Response).StatusCode}");
                        //      Console.WriteLine($"Status Description : {((HttpWebResponse)e.Response).StatusDescription}");

                        urlInfo.httpStatusCode = ((HttpWebResponse)e.Response).StatusCode;
                    }
                  //  urlInfo.httpStatusCode = HttpStatusCode.BadRequest;
                }
                catch (Exception e)
                {
                   //      Console.WriteLine(e.Message);

                 //   urlInfo.httpStatusCode = HttpStatusCode.BadRequest;
                }

                IsChecked.Add(urlInfo);
                // WaitChecking.Remove(item);
            }
            WaitChecking.Clear();
        }

        public List<UrlInfo> GetListStatusCode(HttpStatusCode code)
        {
            List<UrlInfo> urlInfos = new();
            foreach (var item in IsChecked)
            {
                if (item.httpStatusCode == code)
                {
                    urlInfos.Add(item);
                }
            }
            return urlInfos;
        }
    }
}
