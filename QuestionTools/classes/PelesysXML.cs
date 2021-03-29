using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace QuestionTools.classes
{
    class PelesysXML
    {





        public static List<Question> ReadFile(string xmlFile)
        {

            List<Question> qList = new List<Question>();

            XmlDocument doc = JwXML.Load(xmlFile);

            string currentTopic = "Topic";
            string currentSubTopic = "Subtopic";


            //XmlNodeList myNodes = JwXML.GetNodes(doc, "/questions");
            XmlNode rootNode = JwXML.GetSingleNode(doc, "questions");
            XmlNodeList myNodes = rootNode.ChildNodes;

            for (int i = 0; i < myNodes.Count; i++)
            {

                XmlNode node = myNodes[i];
                //Question q = new Question();

                if (node.Name == "topic")
                {
                    currentTopic = node.InnerText;
                }
                else if (node.Name == "subtopic")
                {
                    currentSubTopic = node.InnerText;
                }
                else if (node.Name == "question")
                {

                    string type = JwXML.GetNodeAttribute(node, "type");

                    if (type == "Multiple Choice") {
                        Question q = PelesysXML.GetQuestionMultichoice(node);
                        q.category = currentTopic + " " + currentSubTopic;
                        qList.Add(q);
                    }

                }
                


            }

            return qList;

        }




        

        public static Question GetQuestionMultichoice(XmlNode myNode)
        {

            Question q = new Question();
            q.name = JwString.CleanQuestionName(JwXML.GetNodeAttribute(myNode, "identifier"));
            q.text = JwString.Clean(JwXML.GetNodeValue(myNode, "title"));
            q.type = JwXML.GetNodeAttribute(myNode, "type");


            // answer options
            XmlNodeList answers = JwXML.GetSingleNode(myNode, "choiceset").ChildNodes;
            float optionGrade = float.Parse(JwXML.GetNodeValue(myNode, "answers"));

            for (int i = 0; i < answers.Count; i++)
            {

                string optionText = JwString.Clean(answers[i].InnerText);                

                if ((i + 1) == optionGrade)
                {
                    q.AddOption(optionText, "", 100);
                } else {
                    q.AddOption(optionText, "", 0);
                }

            }


            // feedback
            q.correctfeedback = JwString.Clean(JwXML.GetNodeValue(myNode, "feedback/correct"));
            q.incorrectfeedback = JwString.Clean(JwXML.GetNodeValue(myNode, "feedback/incorrect"));


            // graphic
            string graphic = JwString.Clean(JwXML.GetNodeValue(myNode, "graphic"));
            if (graphic != String.Empty) {
                q.AddImage(graphic, "");
            }

            return q;

        }








            public static List<Question> ReadFileOLD(string xmlFile)
        {

            List<Question> qList = new List<Question>();

            XmlDocument doc = JwXML.Load(xmlFile);

            string currentCategory = "";

            XmlNodeList questionNodes = JwXML.GetNodes(doc, "/questions/question");

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
