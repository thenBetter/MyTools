/***************
NameSpace：UniversalTools
Name： XmlTools
Author： Better
Timer：2020/2/1 21:55:28
Introduce:  xml tools
***************/
using System.Xml;

namespace UniversalTools
{
    public static class XmlTools
    {
        public static XmlNode GetRootNode(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            return doc.LastChild;
        }
        public static bool GetInnerText(ref string text, XmlNode node)
        {
            if (node == null) return false;
            text = node.InnerText;
            return true;
        }
        public static bool GetInnerTextToInteger(ref int number, XmlNode node)
        {
            string text = string.Empty;
            if (GetInnerText(ref text, node))
            {
                if (int.TryParse(text, out number))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool GetInnerTextToFloat(ref float number, XmlNode node)
        {
            string text = string.Empty;
            if (GetInnerText(ref text, node))
            {
                if (float.TryParse(text, out number))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool GetInnerTextToBool(ref bool value, XmlNode node)
        {
            string text = string.Empty;
            if (GetInnerText(ref text, node))
            {
                if (bool.TryParse(text, out value))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool GetAttributeValue(ref string value, string name, XmlNode node)
        {
            if (node == null) return false;
            if (node.Attributes == null) return false;
            XmlAttribute attribute = node.Attributes[name];
            if (attribute == null) return false;
            value = attribute.Value;
            return true;
        }
        public static bool GetAttributeValueToInteger(ref int number, string name, XmlNode node)
        {
            string text = string.Empty;
            if (GetAttributeValue(ref text, name, node))
            {
                if (int.TryParse(text, out number))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool GetAttributeValueToFloat(ref float number, string name, XmlNode node)
        {
            string text = string.Empty;
            if (GetAttributeValue(ref text, name, node))
            {
                if (float.TryParse(text, out number))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool GetAttributeValueToBool(ref bool value, string name, XmlNode node)
        {
            string text = string.Empty;
            if (GetAttributeValue(ref text, name, node))
            {
                if (bool.TryParse(text, out value))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// get xmlnode by attribute
        /// </summary>
        /// <param name="list"></param>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static XmlNode GetXmlNodeByAttribute(XmlNodeList list, string attributeName, string value)
        {
            if (list == null || list.Count <= 0) { return null; }

            for (int i = 0; i < list.Count; i++)
            {
                XmlAttributeCollection attributes = list[i].Attributes;
                if (attributes == null) { return null;}

                XmlAttribute tmp = attributes[attributeName];
                if (tmp == null) { return null;}

                if (string.Equals(tmp.Value, value)) { return list[i]; }
            }
            return null;
        }

    }
}
