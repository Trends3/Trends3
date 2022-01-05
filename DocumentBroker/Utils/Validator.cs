using System.Xml;
using System.Xml.Schema;

namespace DocumentBroker.Utils
{
    static class Validator
    {
        private static bool isValid = true;
        public static void validation(string validator, string input)
        {
            //XMLCollection
            XmlSchemaCollection collection = new XmlSchemaCollection();

            collection.Add("", validator);
            //collection.Add("", "StoreDocument_validator.xsd");
            //collection.Add("", "GenerateStoreRequest_validator.xsd");

            XmlTextReader r = new XmlTextReader(input);
            XmlValidatingReader v = new XmlValidatingReader(r);
            v.ValidationType = ValidationType.Schema;

            //add the XMLCollection.            
            v.Schemas.Add(collection);

            v.ValidationEventHandler += new ValidationEventHandler(MyValidationEventHandler);

            while (v.Read())
            {
                //process content
            }
            v.Close();

            // Check if document is valid or invalid.
            if (isValid)
            {
                Console.WriteLine("Document is valid");
                Program.IsValid = true;
            }
            else
            {
                Console.WriteLine("Document is invalid");
                Program.IsValid = false;
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

