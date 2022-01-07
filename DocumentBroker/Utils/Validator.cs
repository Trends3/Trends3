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
            XmlSchemaCollection collection = new XmlSchemaCollection();
          

            collection.Add("", validator);
            

   
            XmlTextReader r = new XmlTextReader(new StringReader(input));

            XmlValidatingReader v = new XmlValidatingReader(r);
            v.ValidationType = ValidationType.Schema;

            //add the XMLCollection.            
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

