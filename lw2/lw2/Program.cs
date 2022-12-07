using System;
using System.Net;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace lw2
{
    class Program
    {
        static void GetHtmlAndInitChecking(string address, ref List<string> validLinks, ref List<string> invalidLinks, StreamWriter validFile, StreamWriter invalidFile)
        {
            var baseUrl = new Uri(address);
            var host = baseUrl.Host;
            HtmlWeb client = new HtmlWeb();
            HtmlDocument doc = client.Load(address);
            FindAndCheckLinks(ref validLinks, ref invalidLinks, host, validFile, invalidFile, baseUrl, doc, "link", "href");
            FindAndCheckLinks(ref validLinks, ref invalidLinks, host, validFile, invalidFile, baseUrl, doc, "a", "href");
        }

        static void FindAndCheckLinks(ref List<string> validLinks, ref List<string> invalidLinks, string host, StreamWriter validFile, StreamWriter invalidFile, Uri baseUrl, HtmlDocument doc, string tag, string attribute)
        {
            if (doc.DocumentNode.SelectNodes("//" + tag + "[@" + attribute + "]") != null)
            {
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//" + tag + "[@" + attribute + "]"))
                {
                    string newValue = link.Attributes[attribute].Value;
                    var url = new Uri(baseUrl, newValue).AbsoluteUri;
                    Console.WriteLine(url);
                    if ((!validLinks.Contains(url)) && (!invalidLinks.Contains(url)) && (url.IndexOf(host) != -1) && (url.Length > 0) && (newValue != "#"))
                    {
                        try
                        {
                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                            if ((response.StatusCode == HttpStatusCode.OK))
                            {
                                validLinks.Add(url);
                                validFile.WriteLine(url);
                                validFile.WriteLine((int)response.StatusCode);
                                response.Close();
                            }
                        }
                        catch (WebException e)
                        {
                            HttpWebResponse response = (HttpWebResponse)e.Response;
                            invalidLinks.Add(url);
                            invalidFile.WriteLine(url);
                            invalidFile.WriteLine((int)response.StatusCode);
                            response.Close();
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            StreamWriter validFile = new StreamWriter("../../valid.txt");
            StreamWriter invalidFile = new StreamWriter("../../invalid.txt");
            /*string url = "http://links.qatl.ru/";*/
            string url = "http://www.edu.ru/";
            /*string url = "http://11licey.ru/";*/

            List<string> validLinks = new List<string>();
            List<string> invalidLinks = new List<string>();
            GetHtmlAndInitChecking(url, ref validLinks, ref invalidLinks, validFile, invalidFile);

            foreach (var link in validLinks)
            {
                if (link.Contains(".html"))
                {
                    GetHtmlAndInitChecking(link, ref validLinks, ref invalidLinks, validFile, invalidFile);
                }
            }

            validFile.WriteLine(validLinks.Count());
            invalidFile.WriteLine(invalidLinks.Count());
            validFile.WriteLine(DateTime.Now.ToString());
            invalidFile.WriteLine(DateTime.Now.ToString());
            validFile.Close();
            invalidFile.Close();
        }
    }
}