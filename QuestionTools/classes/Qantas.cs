using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QuestionTools.classes
{
    static class Qantas
    {

        public static List<Question> ReadFile(string xmlFile)
        {

            List<Question> qList = new List<Question>();

            XmlDocument doc = JwXML.Load(xmlFile);

            XmlNodeList itemNodes = JwXML.GetNodes(doc, "/questestinterop/item");

            for (int i = 0; i < itemNodes.Count; i++)
            {

                Question q = new Question();

                q.name = JwXML.GetNodeAttribute(itemNodes[i], "ident");

                //XmlNodeList children = itemNodes[i].ChildNodes;

                // get item data
                XmlNode metaData = JwXML.GetSingleNode(itemNodes[i], "itemmetadata");
                q.type = JwXML.GetNodeValue(metaData, "qmd_itemtype");
                q.category = JwXML.GetNodeValue(metaData, "qmd_topic");

                // get question text
                q.text = JwXML.GetNodeValue(itemNodes[i], "presentation/material/mattext");

                // get options
                XmlNodeList options = JwXML.GetNodes(itemNodes[i], "presentation/response_lid/render_choice/response_label");

                for (int index = 0; index < options.Count; index++)
                {
                    string optionText = JwString.Clean(options[index].InnerText);
                    q.AddOption(optionText, "", 0);
                }


                // find correct answer
                XmlNodeList responses = JwXML.GetNodes(itemNodes[i], "resprocessing/respcondition");

                for (int index = 0; index < responses.Count; index++)
                {
                    string setVar = JwXML.GetNodeValue(responses[index], "setvar", "0");
                    if (index < q.options.Count) { q.options[index].grade = float.Parse(setVar) * 100; }
                }

                // get feedback
                XmlNodeList feedbacks = JwXML.GetNodes(itemNodes[i], "itemfeedback");

                for (int index = 0; index < feedbacks.Count; index++)
                {
                    string feedback = JwXML.GetNodeValue(feedbacks[index], "material/mattext");
                    if (index < q.options.Count) { q.options[index].feedback = feedback; }
                }


                // add question to qList
                qList.Add(q);

            }

            return qList;

        }


    }
}
