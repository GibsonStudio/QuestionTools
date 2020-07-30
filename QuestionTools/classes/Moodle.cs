using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace QuestionTools.classes
{



    static class Moodle
    {




        public static XmlNodeList GetQuestionNodes(string sourceFile)
        {

            XmlDocument doc = JwXML.Load(sourceFile);

            return doc.SelectNodes("/quiz/question");

        }




        public static string GetQuestionType(XmlNode myNode, string defaultValue = "")
        {

            return JwXML.GetNodeAttribute(myNode, "type", defaultValue);

        }





        public static Question GetQuestionMultichoice(XmlNode myNode)
        {

            Question q = new Question();

            q.name = JwString.CleanQuestionName(JwXML.GetNodeValue(myNode, "name/text"));
            q.text = JwString.Clean(JwXML.GetNodeValue(myNode, "questiontext/text"));
            q.type = JwXML.GetNodeAttribute(myNode, "type");

            // image?
            XmlNode imageNode = JwXML.GetSingleNode(myNode, "questiontext/file");

            q.image = JwXML.GetNodeAttribute(imageNode, "name");

            if (imageNode != null)
            {
                q.imageData = imageNode.InnerText;
            }

            XmlNodeList answers = JwXML.GetNodes(myNode, "answer");

            foreach (XmlNode answer in answers)
            {

                string optionText = JwString.Clean(JwXML.GetNodeValue(answer, "text"));
                string optionFeedback = JwString.Clean(JwXML.GetNodeValue(answer, "feedback"));
                float optionGrade = float.Parse(JwXML.GetNodeAttribute(answer, "fraction", "0"));

                q.AddOption(optionText, optionFeedback, optionGrade);

            }

            return q;

        }





        public static Question GetQuestionHotspot(XmlNode myNode)
        {

            Question q = new Question();

            q.name = JwXML.GetNodeValue(myNode, "name/text");
            q.text = JwXML.GetNodeValue(myNode, "questiontext/text");
            q.type = JwXML.GetNodeAttribute(myNode, "type");

            // get data
            string data = "{";

            // compile data from XML nodes
            data += "areashape:" + JwXML.GetNodeValue(myNode, "areashape") + ",";
            data += "rectangleleft:" + JwXML.GetNodeValue(myNode, "rectangleleft") + ",";
            data += "rectangletop:" + JwXML.GetNodeValue(myNode, "rectangletop") + ",";
            data += "rectangleright:" + JwXML.GetNodeValue(myNode, "rectangleright") + ",";
            data += "rectanglebottom:" + JwXML.GetNodeValue(myNode, "rectanglebottom") + ",";
            data += "imagewidth:" + JwXML.GetNodeValue(myNode, "imagewidth") + ",";
            data += "imageheight:" + JwXML.GetNodeValue(myNode, "imageheight") + ",";
            data += "areacorrect:" + JwXML.GetNodeValue(myNode, "areacorrect");
            data += "}";
            q.data = data;

            // image?
            XmlNode imageNode = JwXML.GetSingleNode(myNode, "file");

            q.image = JwXML.GetNodeAttribute(imageNode, "name");

            if (imageNode != null)
            {
                q.imageData = imageNode.InnerText;
            }

            return q;

        }





        public static string GetQuestionDescriptionNOTUSED(XmlNode myNode)
        {

            string name = JwXML.GetNodeValue(myNode, "name/text");
            string questionText = JwXML.GetNodeValue(myNode, "questiontext/text");

            string qData = "NAME: " + name + Environment.NewLine;
            qData += questionText;
            return qData;

        }






        public static string GetQuestionMatchingNOTUSED(XmlNode myNode)
        {

            string name = JwXML.GetNodeValue(myNode, "name/text");
            string questionText = JwXML.GetNodeValue(myNode, "questiontext/text");

            string qData = "NAME: " + name + Environment.NewLine;
            qData += "QUESTION: " + questionText + Environment.NewLine;

            XmlNodeList subquestions = JwXML.GetNodes(myNode, "subquestion");

            foreach (XmlNode sub in subquestions)
            {
                string sText = JwXML.GetNodeValue(sub, "text");
                string sAnswer = JwXML.GetNodeValue(sub, "answer/text");
                if (sText.Length > 0) { qData += "OPTION: " + sText + Environment.NewLine; }
                qData += "    --> " + sAnswer + Environment.NewLine;
            }

            return qData;

        }





        public static Question GetQuestionDragDropText(XmlNode myNode)
        {

            Question q = new Question();

            q.name = JwString.CleanQuestionName(JwXML.GetNodeValue(myNode, "name/text"));
            q.type = JwXML.GetNodeAttribute(myNode, "type");

            string qData = JwString.Clean(JwXML.GetNodeValue(myNode, "questiontext/text"));
            qData = qData.Replace("<![CDATA[", "");
            qData = qData.Replace("]]>", "");

            // get question text
            string pattern = @"[[\d+]]";
            string qText = Regex.Replace(qData, pattern, " ________ ");
            q.text = qText.Trim();

            q.xmlNode = myNode;

            return q;

        }


        public static Question GetQuestionCloze(XmlNode myNode)
        {

            Question q = new Question();

            q.name = JwString.CleanQuestionName(JwXML.GetNodeValue(myNode, "name/text"));            
            q.type = JwXML.GetNodeAttribute(myNode, "type");

            string qData = JwString.Clean(JwXML.GetNodeValue(myNode, "questiontext/text"));
            qData = qData.Replace("<![CDATA[", "");
            qData = qData.Replace("]]>", "");

            // get question text
            string pattern = @"{:MULTICHOICE(.*?)}";
            string qText = Regex.Replace(qData, pattern, " ________ ");
            q.text = qText.Trim();


            // get answer options
            MatchCollection blanks = Regex.Matches(qData, pattern);

            if (blanks.Count > 1) {
                q.type = "UNSUPPORTED: " + q.type;
                q.xmlNode = myNode;
            }

            for (int i = 0; i < blanks.Count; i++)
            {

                // get correct options
                string pattern2 = @"=([^~]+)[~}]";
                MatchCollection matches2 = Regex.Matches(blanks[i].Value, pattern2);

                for (int j = 0; j < matches2.Count; j++)
                {
                    string optionText = matches2[j].Groups[1].ToString().Trim();
                    string optionFeedback = "";
                    float optionGrade = 1.0f;
                    q.AddOption(optionText, optionFeedback, optionGrade);
                }


                // get incorrect options
                string pattern3 = @"%-?\d*%([^~}]+)";
                MatchCollection matches3 = Regex.Matches(blanks[i].Value, pattern3);

                for (int j = 0; j < matches3.Count; j++)
                {
                    string optionText = matches3[j].Groups[1].ToString().Trim();
                    string optionFeedback = "";
                    float optionGrade = 0.0f;
                    q.AddOption(optionText, optionFeedback, optionGrade);
                }

            }

            return q;

        }






        public static string GetQuestionNumericalNOTUSED(XmlNode myNode)
        {

            string name = JwXML.GetNodeValue(myNode, "name/text");
            string questionText = JwXML.GetNodeValue(myNode, "questiontext/text");
            string answer = JwXML.GetNodeValue(myNode, "answer/text");
            string tolerance = JwXML.GetNodeValue(myNode, "answer/tolerance");

            string qData = "NAME: " + name + Environment.NewLine;
            qData += "QUESTION: " + questionText + Environment.NewLine;
            qData += "ANSWER: " + answer + Environment.NewLine;
            qData += "TOLERANCE: " + tolerance + Environment.NewLine;

            return qData;

        }









        public static string GetQuestionShortAnswerNOTUSED(XmlNode myNode)
        {

            string name = JwXML.GetNodeValue(myNode, "name/text");
            string questionText = JwXML.GetNodeValue(myNode, "questiontext/text");
            string answer = JwXML.GetNodeValue(myNode, "answer/text");

            string qData = "NAME: " + name + Environment.NewLine;
            qData += "QUESTION: " + questionText + Environment.NewLine;
            qData += "ANSWER: " + answer + Environment.NewLine;

            return qData;

        }




        public static Question GetQuestionUnknown(XmlNode myNode)
        {

            Question q = new Question();

            q.name = JwString.CleanQuestionName(JwXML.GetNodeValue(myNode, "name/text"));
            q.text = JwString.Clean(JwXML.GetNodeValue(myNode, "questiontext/text"));
            q.type = JwXML.GetNodeAttribute(myNode, "type");
            q.xmlNode = myNode;

            XmlNodeList answers = JwXML.GetNodes(myNode, "answer");

            foreach (XmlNode answer in answers)
            {

                string optionText = JwString.Clean(JwXML.GetNodeValue(answer, "text"));
                string optionFeedback = JwString.Clean(JwXML.GetNodeValue(answer, "feedback"));
                float optionGrade = float.Parse(JwXML.GetNodeAttribute(answer, "fraction", "0"));

                q.AddOption(optionText, optionFeedback, optionGrade);

            }

            return q;

        }







        public static List<Question> ReadFile(string xmlFile)
        {

            List<Question> qList = new List<Question>();

            XmlDocument doc = JwXML.Load(xmlFile);

            string currentCategory = "";

            XmlNodeList questionNodes = JwXML.GetNodes(doc, "/quiz/question");

            for (int i = 0; i < questionNodes.Count; i++)
            {

                string type = JwXML.GetNodeAttribute(questionNodes[i], "type");

                if (type == "category")
                {
                    currentCategory = JwString.CleanCategory(questionNodes[i].InnerText);

                }
                else if (type == "multichoice" || type == "multichoiceset" || type == "truefalse")
                {

                    Question q = Moodle.GetQuestionMultichoice(questionNodes[i]);
                    q.category = currentCategory;
                    qList.Add(q);

                }
                else if (type == "cloze")
                {

                    Question q = Moodle.GetQuestionCloze(questionNodes[i]);
                    q.category = currentCategory;
                    qList.Add(q);

                }
                else if (type == "ddwtos")
                {

                    Question q = Moodle.GetQuestionDragDropText(questionNodes[i]);
                    q.category = currentCategory;
                    qList.Add(q);

                }
                else if (type == "hotspot")
                {

                    Question q = Moodle.GetQuestionHotspot(questionNodes[i]);
                    q.category = currentCategory;
                    qList.Add(q);

                }
                else
                {

                    // default option - don't know what to do with question
                    Question q = Moodle.GetQuestionUnknown(questionNodes[i]);
                    q.category = currentCategory;
                    qList.Add(q);

                }

            }

            return qList;

        }









    }



}
