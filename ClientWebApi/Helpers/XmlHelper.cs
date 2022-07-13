using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ClientWebApi.Helpers
{
    public static class XmlHelper
    {
        public static string Serialize<T>(T dataToSerialize)
        {
            try
            {
                var stringwriter = new Utf8StringWriter();
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stringwriter, dataToSerialize);
                return stringwriter.ToString();
            }
            catch
            {
                throw;
            }
        }

        public static T Deserialize<T>(string xmlText)
        {
            try
            {
                var stringReader = new System.IO.StringReader(xmlText);
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
            catch
            {
                throw;
            }
        }

        public class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding
            {
                get { return new UTF8Encoding(false); }
            }
        }
    }
}
