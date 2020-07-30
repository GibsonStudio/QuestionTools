using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QuestionTools.classes
{






    public class Question
    {

        public string name = "";
        public string category = "";
        public string text = "";
        public string type = "";
        public string data = "";
        public string image = "";
        public string imageData = "";
        public List<Option> options = new List<Option>();
        public XmlNode xmlNode;


        public void AddOption(string optionText, string optionFeedback, float optionGrade)
        {

            Option o = new Option();
            o.text = optionText;
            o.feedback = optionFeedback;
            o.grade = optionGrade;
            options.Add(o);

        }


        public string GetDebug()
        {
            string txt = "NAME: " + name + Environment.NewLine;
            txt += "CATEGORY: " + category + Environment.NewLine;
            if (type != String.Empty) { txt += "TYPE: " + type + Environment.NewLine; }
            txt += text + Environment.NewLine;

            // answer options
            string[] optionPrefixes = { "a) ", "b) ", "c) ", "d) ", "e) ", "f) ", "g) ", "h) ", "i) " };

            for (int i = 0; i < options.Count; i++)
            {
                int optionIndex = Math.Min(i, optionPrefixes.Length - 1);
                txt += optionPrefixes[optionIndex] + options[i].text;
                if (options[i].grade > 0) { txt += " (" + options[i].grade.ToString() + ")"; }
                if (options[i].feedback != String.Empty) { txt += " [" + options[i].feedback + "]"; }
                txt += Environment.NewLine;
            }

            if (image != String.Empty) { txt += "Image: " + image + Environment.NewLine; }
            if (imageData != String.Empty) { txt += "ImageData: " + "YES" + Environment.NewLine; }
            if (data != String.Empty) { txt += "Data: " + data + Environment.NewLine; }
            if (xmlNode != null) { txt += "XML:" + Environment.NewLine + xmlNode.OuterXml.ToString() + Environment.NewLine; }

            return txt;
        }



        public Boolean HasMultipleCorrect ()
        {

            int correctCount = 0;

            for (int i = 0; i < options.Count; i++)
            {
                if (options[i].grade > 0) { correctCount++; }
            }

            if (correctCount > 1) { return true; }

            return false;

        }



    }




    public class Option
    {
        public string text = "";
        public float grade = 0;
        public string feedback = "";
    }



    public class Image
    {
        public string name = "";
        public string imageData = "";
    }



}
