using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;


namespace QuestionTools.classes
{


    static class CFG
    {

        public static string cfgFile = @"config.cfg";
        public static string sourceDir = @"C:\"; // over-riden by value in config.cfg
        public static string outputDir = @"C:\"; // over-riden by value in config.cfg



        public static IDictionary<string, string> init ()
        {

            CFG.CheckCfgFileExists();

            IDictionary<string, string> result = new Dictionary<string, string>();

            string cfgSourceDir = CFG.GetVar("sourceDir");
            string cfgOutputDir = CFG.GetVar("outputDir");

            if (Directory.Exists(cfgSourceDir)) { sourceDir = cfgSourceDir; }
            if (Directory.Exists(cfgOutputDir)) { outputDir = cfgOutputDir; }

            result["sourceDir"] = sourceDir;
            result["outputDir"] = outputDir;

            return result;

        }






        public static string SetSourceDir()
        {

            FolderBrowserDialog fb = new FolderBrowserDialog();

            fb.SelectedPath = sourceDir;

            if (fb.ShowDialog() == DialogResult.OK)
            {
                sourceDir = fb.SelectedPath;
                if (!sourceDir.EndsWith(@"\"))
                {
                    sourceDir += @"\";
                }
                //Main._Main.txtSourceDir.Text = sourceDir;
                SetVar("sourceDir", sourceDir);
            }

            fb.Dispose();

            return sourceDir;

        }



        public static string SetOutputDir()
        {

            FolderBrowserDialog fb = new FolderBrowserDialog();

            fb.SelectedPath = outputDir;

            if (fb.ShowDialog() == DialogResult.OK)
            {
                outputDir = fb.SelectedPath;
                if (!outputDir.EndsWith(@"\"))
                {
                    outputDir += @"\";
                }
                //Main._Main.txtOutputDir.Text = outputDir;
                SetVar("outputDir", outputDir);
            }

            fb.Dispose();

            return outputDir;

        }




        public static void CheckCfgFileExists()
        {

            var myFile = cfgFile;

            if (!File.Exists(myFile))
            {

                FileStream fs = new FileStream(myFile, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

                string xml = "<?xml version=\"1.0\"?>" + Environment.NewLine;
                xml += "<config>" + Environment.NewLine;
                xml += "<variable><name>sourceDir</name><value>" + sourceDir + "</value></variable>" + Environment.NewLine;
                xml += "<variable><name>outputDir</name><value>" + outputDir + "</value></variable>" + Environment.NewLine;
                xml += "</config>";

                sw.Write(xml);
                sw.Close();

            }

        }






        public static string GetVar(string name, string defaultValue = "")
        {

            XmlDocument doc = JwXML.Load(cfgFile);
            XmlNodeList variables = JwXML.GetNodes(doc, "config/variable");

            foreach (XmlNode variable in variables)
            {

                string varName = JwXML.GetNodeValue(variable, "name");

                if (varName == name)
                {
                    return JwXML.GetNodeValue(variable, "value", defaultValue);
                }

            }

            return defaultValue;

        }





        public static void SetVar(string name, string value)
        {

            CheckCfgFileExists();

            XmlDocument doc = JwXML.Load(cfgFile);
            XmlNodeList variables = JwXML.GetNodes(doc, "config/variable");
            Boolean addNewNode = true;

            foreach (XmlNode variable in variables)
            {

                string varName = JwXML.GetNodeValue(variable, "name");

                if (varName == name)
                {

                    addNewNode = false;
                    JwXML.GetSingleNode(variable, "value").InnerText = value;

                }

            }

            // add new variable node?
            if (addNewNode)
            {

                XmlNode config = JwXML.GetSingleNode(doc, "config");
                XmlElement newVariable = doc.CreateElement("variable");
                XmlElement newName = doc.CreateElement("name");
                newName.InnerText = name;
                newVariable.AppendChild(newName);
                XmlElement newValue = doc.CreateElement("value");
                newValue.InnerText = value;
                newVariable.AppendChild(newValue);
                config.AppendChild(newVariable);

            }

            //write back to file
            JwXML.WriteToFile(doc, cfgFile);

        }










    }


}
