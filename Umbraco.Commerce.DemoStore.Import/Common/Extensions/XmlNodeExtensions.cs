using System.Xml;

namespace Umbraco.Commerce.DemoStore.Import.Common.Extensions
{
    public static class XmlNodeExtensions
    {
        public static string SelectSingleNodeStringValue(this XmlNode xmlNode, string xPath)
        {
            string result = string.Empty;
            XmlNode xmlNode2 = xmlNode.SelectSingleNode(xPath);
            if (xmlNode2 != null)
            {
                result = xmlNode2.InnerText;
            }
            return result;
        }

        public static string SelectSingleNodeStringValue(this XmlNode xmlNode, string xPath, XmlNamespaceManager ns)
        {
            string result = string.Empty;
            XmlNode xmlNode2 = xmlNode.SelectSingleNode(xPath, ns);
            if (xmlNode2 != null)
            {
                result = xmlNode2.InnerText;
            }
            return result;
        }

        public static string SelectAttributeStringValue(this XmlNode xmlNode, string attributeName)
        {
            string result = string.Empty;
            if (xmlNode != null && xmlNode.Attributes[attributeName] != null)
            {
                result = xmlNode.Attributes[attributeName].InnerText;
            }
            return result;
        }

        public static void EnsureXMlNode(this XmlNode parent, string nodename, string defaultValue)
        {
            parent.EnsureXMlNode(nodename, defaultValue, prepend: false);
        }

        public static void EnsureXMlNode(this XmlNode parent, string nodename, string defaultValue, bool prepend)
        {
            if (parent.SelectSingleNode(nodename) == null)
            {
                XmlElement xmlElement = parent.OwnerDocument.CreateElement(nodename);
                xmlElement.InnerText = defaultValue;
                if (prepend)
                {
                    parent.PrependChild(xmlElement);
                }
                else
                {
                    parent.AppendChild(xmlElement);
                }
            }
        }

        public static string GetAttributeValueFromNode(this XmlNode node, string attributeName)
        {
            return node.GetAttributeValueFromNode<string>(attributeName, string.Empty);
        }

        public static string GetAttributeValueFromNode(this XmlNode node, string attributeName, string defaultValue)
        {
            return node.GetAttributeValueFromNode<string>(attributeName, defaultValue);
        }

        public static T GetAttributeValueFromNode<T>(this XmlNode node, string attributeName, T defaultValue)
        {
            if (node.Attributes[attributeName] != null)
            {
                string innerText = node.Attributes[attributeName].InnerText;
                if (string.IsNullOrEmpty(innerText))
                {
                    return defaultValue;
                }
                return (T)Convert.ChangeType(innerText, typeof(T));
            }
            return defaultValue;
        }
    }
}
