using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Parser;
using AngleSharp.Dom;
using System.Xml;

namespace testworknot
{
    class Program
    { 

        static void Main(string[] args)
        {
            var document = GetDoc().Result;
            XmlDocument xmlDoc = new XmlDocument();
            HtmlParser a = new HtmlParser();
            //Xml
            var xDoc = a.ParseDocument(document.DocumentElement.OuterHtml);
            xmlDoc.LoadXml(xDoc.ToHtml());
            var c = xmlDoc.DocumentElement.SelectNodes("body//offers/offer");
            using (StreamWriter sw = new StreamWriter("offeridsxml.txt", false, System.Text.Encoding.Default))
            {
                foreach (XmlNode cell in c)
                {
                    sw.WriteLine(cell.SelectSingleNode("@id").Value);
                }
            }
            //

            //HtmlDoc
            var cells = document.QuerySelectorAll("offer");
            using (StreamWriter sw = new StreamWriter("offeridsdoc.txt", false, System.Text.Encoding.Default))
            {
                foreach (var cell in cells)
                {

                    sw.WriteLine(cell.Id);
                }
            }
            //
        }

        static async Task<IDocument> GetDoc()
        {
            var src = Url.Create("https://yastatic.net/market-export/_/partner/help/YML.xml");
            var config = Configuration.Default
                         .WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(src);
            return document;
        }
    }
}
