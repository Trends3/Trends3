using System.Xml;
using System.Xml.Schema;

namespace DocumentBroker.Utils
{
     class Validator
    {
        public static char ValidForQ;
        private static bool isValid;
        private static bool isValidDoc;

        public void validation(string validator , string input)
        {
            //XMLCollection
            XmlSchemaCollection collection = new XmlSchemaCollection();

            XmlSchemaSet collection2 = new XmlSchemaSet();

            collection.Add("", validator);



            XmlTextReader r = new XmlTextReader(new StringReader(input));

            XmlValidatingReader v = new XmlValidatingReader(r)
            {
                ValidationType = ValidationType.Schema
            };

            XmlTextReader txtReader = new XmlTextReader(input);
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add("", validator);
            settings.ValidationType = ValidationType.Schema;
            XmlReader v2 = XmlReader.Create(txtReader, settings);



            //v.Schemas.Add(collection);


            //v.ValidationEventHandler += new ValidationEventHandler(MyValidationEventHandler);


            //while (v.Read())
            //{
            //    //process content
            //}

            try
            {
                v.Read();
                v.Close();
                isValid = true;
            }
            catch (Exception e)
            {
                isValid = false;
                Console.WriteLine(e.Message);
            }

            try
            {
                v2.Read();
                v2.Close();
                isValidDoc = true;
            }
            catch (Exception e)
            {
                isValidDoc = false;
                Console.WriteLine(e.Message);
            }

            // Check if document is valid or invalid.
            if (isValid == true)
            {
                Console.WriteLine("Document is valid");
                ValidForQ = '1';
            }else if (isValidDoc == true)
            {
                Console.WriteLine("Document is valid");
                ValidForQ = '2';
            }
            else
            {
                Console.WriteLine("Document is invalid");
                ValidForQ = '0';
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

