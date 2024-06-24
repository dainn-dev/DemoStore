using System.Xml.Linq;

namespace Umbraco.Commerce.DemoStore.Import.Common.Extensions
{
    public static class XDocumentExtensions
    {
        public static string SelectSingleValue(this XDocument doc, string name)
        {
            XElement xElement = doc.Descendants(name).FirstOrDefault();
            if (xElement != null)
            {
                return xElement.Value;
            }
            return string.Empty;
        }

        public static string SelectSingleValue(this XElement element, string name)
        {
            XElement xElement = element.Descendants(name).FirstOrDefault();
            if (xElement != null)
            {
                return xElement.Value;
            }
            return string.Empty;
        }

        public static XElement SelectSingleNode(this XDocument doc, string name)
        {
            return doc.Descendants(name).FirstOrDefault();
        }

        public static XElement SelectSingleNode(this XElement element, string name)
        {
            return element.Descendants(name).FirstOrDefault();
        }

        public static XElement CreateNode(this XDocument doc, string name)
        {
            XElement xElement = new XElement("emailConfiguration");
            doc.Root.Add(xElement);
            return xElement;
        }

        public static string GetAttributeValue(this XElement element, string name)
        {
            XAttribute xAttribute = element.Attribute(name);
            if (xAttribute != null)
            {
                return xAttribute.Value;
            }
            return string.Empty;
        }

        private static bool AttributeExists(this XElement element, string name)
        {
            return element.Attribute(name) != null;
        }

        public static bool GetBooleanAttributeValue(this XElement element, string name)
        {
            return GetAttributeValue(element, name).Equals("true", StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool GetBooleanAttributeValue(this XElement element, string name, bool defaultValue)
        {
            if (!AttributeExists(element, name))
            {
                return defaultValue;
            }
            return GetAttributeValue(element, name).Equals("true", StringComparison.CurrentCultureIgnoreCase);
        }

        public static string WriteStringWithDeclaration(this XDocument doc)
        {
            return $"<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n{doc}";
        }
    }
}
