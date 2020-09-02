using AngleSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace XML_Parser
{
    class Program
    {        
        static string GetCode(string url)
        {
            string code;
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.GetEncoding("windows-1251");
                code = client.DownloadString(url);
            }
            return code;
        }

        static void Main(string[] args)
        {
            Handler();
        }

        static async void Handler()
        {
            var url = @"https://yastatic.net/market-export/_/partner/help/YML.xml";
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);

            var source = GetCode(url);

            var document = await context.OpenAsync(req => req.Content(source));
            var offersList = document.All.Where(elem => elem.LocalName == "offer");
            List<Model> offersAsModels = new List<Model>();
            foreach (var item in offersList)
            {
                // Прямое обозначение переменных, на мой взгляд, выглядит более удобочитаемее, чем через конструктор
                offersAsModels.Add(new Model() { 
                    id = int.Parse(item.GetAttribute("id")),
                    bid = int.Parse(item.GetAttribute("bid")),
                    Cbid = item.GetAttribute("cbid"),
                    type = item.GetAttribute("type"),
                    available = bool.Parse(item.GetAttribute("available"))
                });
            }

            // ../XML_Parser/bin/Debug/netcoreapp3.1
            using (var txt = new StreamWriter("output.txt"))
            {
                foreach (var item in offersAsModels)
                {
                    txt.WriteLine(item.id);
                }
            }

        }
    }
}
