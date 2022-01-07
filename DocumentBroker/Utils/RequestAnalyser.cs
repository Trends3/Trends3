using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DocumentBroker.Utils
{
    class RequestAnalyser
    {

        private static bool Generate = false;
        private static bool Store = false;
        private static bool Generate_store1 = false;
        private static bool Generate_store2 = false;
       

        public  void SearchRequest(string input)
        {
               
            using (XmlReader reader = XmlReader.Create(new StringReader(input)))
            {


                Generate = false;
                Store = false;
                Generate_store1 = false;
                Generate_store2 = false;
                
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                      
                        //return only when you have START tag  
                        switch (reader.Name.ToString())
                        {
                            case "Generate":

                                if (string.IsNullOrEmpty(reader.ReadString()) == false)
                                {
                                    Generate = true;
                                    Store = false;
                                    Generate_store1 = true;

                                    //Console.WriteLine("Het is een generate request");
                                }

                                break;

                            case "Store":
                                
                                if(string.IsNullOrEmpty(reader.ReadString()) == false && Generate_store1 == true)
                                {
                                    Generate = false;
                                    Store = false;
                                    Generate_store2 = true;

                                    //Console.WriteLine("Het is een Generate_store request");
                                }
                                if(Generate_store1 == false)
                                {
                                    Generate = false;
                                    Store = true;
                                    Generate_store2 = false;

                                    //Console.WriteLine("Het is een store request");
                                }

                                break;
                        }
                    }
                }
            }
            //Console.ReadKey();
            if (Generate == true)
            {
                Console.WriteLine("Het is een Generate request");
                Console.WriteLine("");
            }
            if (Store == true)
            {
                Console.WriteLine("Het is een store request");
                Console.WriteLine("");
            }
            if (Generate_store2 == true)
            {
                Console.WriteLine("Het is een Generate_store request");
                Console.WriteLine("");
            }
        }
    }
}
