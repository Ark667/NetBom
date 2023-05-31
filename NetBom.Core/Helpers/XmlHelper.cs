using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NetBom.Core.Helpers
{
    public static class XmlHelper
    {
        /// <summary>
        /// The DeserializeXml.
        /// </summary>
        /// <param name="filePath"></param>
        public static T DeserializeXml<T>(string filePath)
        {
            using TextReader reader = new StringReader(File.ReadAllText(filePath));
            return (T)
                new XmlSerializer(typeof(T)).Deserialize(new IgnoreNamespaceXmlTextReader(reader));
        }

        private sealed class IgnoreNamespaceXmlTextReader : XmlTextReader
        {
            public IgnoreNamespaceXmlTextReader(TextReader reader) : base(reader) { }

            public override string NamespaceURI => string.Empty;
        }
    }
}
