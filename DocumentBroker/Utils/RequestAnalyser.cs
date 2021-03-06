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


                            switch (reader.Name.ToString())
                            {
                                case "Generate":

                                    if (string.IsNullOrEmpty(reader.ReadString()) == false)
                                    {
                                        Generate = true;
                                        Store = false;
                                        Generate_store1 = true;


                                    }

                                    break;

                                case "Store":

                                    if (string.IsNullOrEmpty(reader.ReadString()) == false && Generate_store1 == true)
                                    {
                                        Generate = false;
                                        Store = false;
                                        Generate_store2 = true;


                                    }
                                    if (Generate_store1 == false)
                                    {
                                        Generate = false;
                                        Store = true;
                                        Generate_store2 = false;


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


            if (Generate == true)
            {
                Console.WriteLine("Het is een Generate request");
                Console.WriteLine("");
                return 'g';
            }
            else if (Store == true)
            {
                Console.WriteLine("Het is een store request");
                Console.WriteLine("");
                return 's';
            }
            else if (Generate_store2 == true)
            {
                Console.WriteLine("Het is een Generate_store request");
                Console.WriteLine("");
                return '0';
            }
            else
            {
                return '9';
            }
        }
    }
}
