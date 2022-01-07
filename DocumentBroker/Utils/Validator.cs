using System.Xml;
using System.Xml.Schema;

namespace DocumentBroker.Utils
{
     class Validator
    {
        public static bool ValidForQ;
        private static bool isValid;

        public void validation(string validator , string input)
        {
            //XMLCollection
            //XmlSchemaCollection collection = new XmlSchemaCollection();

            // nieuwere code

            XmlSchemaSet collection = new XmlSchemaSet();

            // nieuwere code 


            collection.Add("", validator);
            

   
            XmlTextReader r = new XmlTextReader(new StringReader(input));

            XmlValidatingReader v = new XmlValidatingReader(r)
            {
                ValidationType = ValidationType.Schema
            };


            // voorbeeld code van de documentatie 

            //XmlReaderSettings settings = new XmlReaderSettings();
            //settings.ValidationType = ValidationType.DTD;
            //XmlReader inner = XmlReader.Create("book.xml", settings); // DTD Validation
            //settings.Schemas.Add("urn:book-schema", "book.xsd");
            //settings.ValidationType = ValidationType.Schema;
            //XmlReader outer = XmlReader.Create(inner, settings);


            //add the XMLCollection.
            
            
            // verwacht een schema geen set.. ? 

            v.Schemas.Add(collection);

            v.ValidationEventHandler += new ValidationEventHandler(MyValidationEventHandler);

            //while (v.Read())
            //{
            //    //process content
            //}
            try
            {
                v.Read();
                v.Close();

            }

            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            

            // Check if document is valid or invalid.
            if (isValid == true)
            {
                Console.WriteLine("Document is valid");
                ValidForQ = true;
            }
            else
            {
                Console.WriteLine("Document is invalid");
                ValidForQ = false;
            }
        }
        
        public static void MyValidationEventHandler(object sender,
                                           ValidationEventArgs args)
        {
            isValid = false;
            Console.WriteLine("Validation event\n" + args.Message);
        }



    }
}

