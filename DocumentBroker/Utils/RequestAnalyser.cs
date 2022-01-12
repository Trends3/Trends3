using System.Xml;

namespace DocumentBroker.Utils
{
    class RequestAnalyser
    {

        private static bool Generate = false;
        private static bool Store = false;
        private static bool Generate_store1 = false;
        private static bool Generate_store2 = false;


        public char SearchRequest(string input)
        {
            try
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

                                    if (string.IsNullOrEmpty(reader.ReadString()) == false && Generate_store1 == true)
                                    {
                                        Generate = false;
                                        Store = false;
                                        Generate_store2 = true;

                                        //Console.WriteLine("Het is een Generate_store request");
                                    }
                                    if (Generate_store1 == false)
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            try
            {
                using (XmlReader reader = XmlReader.Create(input))
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

                                    if (string.IsNullOrEmpty(reader.ReadString()) == false && Generate_store1 == true)
                                    {
                                        Generate = false;
                                        Store = false;
                                        Generate_store2 = true;

                                        //Console.WriteLine("Het is een Generate_store request");
                                    }
                                    if (Generate_store1 == false)
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //Console.ReadKey();
            if (Generate == true)
            {
                Console.WriteLine("Het is een Generate request");
                Console.WriteLine("");
                return 'g';
            }
            if (Store == true)
            {
                Console.WriteLine("Het is een store request");
                Console.WriteLine("");
                return 's';
            }
            if (Generate_store2 == true)
            {
                Console.WriteLine("Het is een Generate_store request");
                Console.WriteLine("");
                return '0';
            }
            return '9';
        }
    }
}
