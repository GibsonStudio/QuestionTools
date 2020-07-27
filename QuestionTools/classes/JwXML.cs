using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace QuestionTools.classes
{


    static class JwXML
    {



        public static XmlDocument Load(string sourceFile)
        {

            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(sourceFile);
                return doc;
            }
            catch
            {
                return doc;
            }

        }




        public static void WriteToFile(XmlDocument doc, string xmlFile)
        {

            XmlTextWriter xtw = new XmlTextWriter(xmlFile, null);
            xtw.Formatting = Formatting.Indented;
            doc.Save(xtw);
            xtw.Close();

        }




        public static string GetNodeAttribute(XmlNode myNode, string attributeName, string defaultValue = "")
        {
            try
            {
                string attributeValue = myNode.Attributes.GetNamedItem(attributeName).Value;
                return attributeValue;
            }
            catch
            {
                return defaultValue;
            }
        }




        public static string GetNodeValue(XmlNode parentNode, string nodeName, string defaultValue = "")
        {
            try
            {
                string nodeValue = JwString.Clean(parentNode.SelectSingleNode(nodeName).InnerText);
                return nodeValue;
            }
            catch
            {
                return defaultValue;
            }
        }




        public static XmlNodeList GetNodes(XmlNode myNode, string nodePath)
        {
            XmlNodeList myNodes = myNode.SelectNodes(nodePath);
            return myNodes;
        }




        public static XmlNode GetSingleNode(XmlNode myNode, string nodeName)
        {
            XmlNode selectedNode = myNode.SelectSingleNode(nodeName);
            return selectedNode;
        }




    }


}
