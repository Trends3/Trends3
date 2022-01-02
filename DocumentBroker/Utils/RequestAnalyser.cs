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
            using (XmlReader reader = XmlReader.Create(@"GenerateDocumentRequest.xml"))
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
            //Console.ReadKey();
        }
    }
}
