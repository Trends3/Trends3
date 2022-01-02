using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DocumentBroker.Utils
{
    internal static class RequestAnalyser
    {
        



        public static void SearchRequest(string input , string target)
        {
            /*
            XElement root = XElement.Load(input);
            XNamespace aw = "http://www.w3.org/2001/XMLSchema";

            IEnumerable<XElement> result =
            from el in root.Elements(aw + "GenerateDocumentRequest")
            where (string)el.Element(aw + "Generate") == target
            select el;
            /*
            var xDoc = XDocument.Load("your XSD path");
            var ns = XNamespace.Get(@"http://www.w3.org/2001/XMLSchema");

            var length = (from sType in xDoc.Element(ns + "schema").Elements(ns + "simpleType")
            where sType.Attribute("name").Value == "Amount_Type"
            from r in sType.Elements(ns + "restriction")
            select r.Element(ns + "maxLength").Attribute("value")
                    .Value).FirstOrDefault();
            // */
            //var xDoc = XDocument.Load(input);
            //var ns = XNamespace.Get(@"http://www.w3.org/2001/XMLSchema");

            //var length = from el in xDoc.Elements(ns + "GenerateDocumentRequest")
            //              where el.Attribute("Generate").Value == target
            //              select el;

            //foreach (XElement el in length)
            //{
            //    Console.WriteLine(el);
            //    Console.WriteLine("het doet iets");

            //    if (string.IsNullOrEmpty(el.ToString()))
            //    {
            //        Console.WriteLine("test");
            //    }

            //}
            using (XmlReader reader = XmlReader.Create(@".\GenerateDocumentRequest.xml"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        //return only when you have START tag  
                        switch (reader.Name.ToString())
                        {
                            case "RequestId":
                                Console.WriteLine("The Name of the Student is " + reader.ReadString());
                                break;
                        }
                    }
                    Console.WriteLine("");
                }
            }
            Console.ReadKey();
        }
    }
}
