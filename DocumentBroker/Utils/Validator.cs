using System.Xml;
using System.Xml.Schema;

namespace DocumentBroker.Utils
{
    class Validator
    {
        public static bool ValidForQ;
        private static bool isValid;
        private static bool isValidDoc;

        public void validation(string validator, string input)
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
            if (isValid == true || isValidDoc == true)
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

    }
}

