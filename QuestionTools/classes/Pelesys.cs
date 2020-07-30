using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QuestionTools.classes
{
    static class Pelesys
    {






        public static List<string> CreateOutput(string filename, List<Question> questions)
        {

            List<string> result = new List<string>();

            string filenameMResponse = Path.Combine(CFG.outputDir, Path.GetFileNameWithoutExtension(filename) + "-mresponse.csv");
            string filenameHotspot = Path.Combine(CFG.outputDir, Path.GetFileNameWithoutExtension(filename) + "-hotspot.csv");
            string filenameXML = Path.Combine(CFG.outputDir, Path.GetFileNameWithoutExtension(filename) + "-UNSUPPORTED.xml");

            // sort questions into output types
            List<Question> qDefault = new List<Question>();
            List<Question> qMResponse = new List<Question>();
            List<Question> qHotspot = new List<Question>();
            List<Question> qXML = new List<Question>();

            for (int i = 0; i < questions.Count; i++)
            {

                Question q = questions[i];
                if (q.type == "hotspot")
                {
                    qHotspot.Add(q);
                }
                else if (q.type == "gapselect" || q.type == "ddwtos" || q.type.StartsWith("UNSUPPORTED"))
                {
                    qXML.Add(q);
                }
                else if (q.HasMultipleCorrect())
                {
                    qMResponse.Add(q);
                }
                else
                {
                    qDefault.Add(q);
                }

            }

            result.Add(qDefault.Count.ToString() + " default qtypes found.");
            result.Add(qMResponse.Count.ToString() + " MResponse found.");
            result.Add(qHotspot.Count.ToString() + " hotspot qtype found.");
            result.Add(qXML.Count.ToString() + " unsupported qtype found - will output to xml file.");

            if (qDefault.Count > 0) { result.AddRange(CreateDefaultCSV(filename, qDefault)); }
            if (qMResponse.Count > 0) { result.AddRange(CreateMResponseCSV(filenameMResponse, qMResponse)); }
            if (qHotspot.Count > 0) { result.AddRange(CreateHotspotCSV(filenameHotspot, qHotspot)); }
            if (qXML.Count > 0) { result.AddRange(CreateXML(filenameXML, qXML)); }

            // done
            return result;

        }





        public static List<string> CreateDefaultCSV(string filename, List<Question> questions)
        {

            int questionsSkipped = 0;
            List<string> result = new List<string>();
            result.Add("Processing default question types....");

            // build CSV text
            string csvText = "code,body,correct_choice,";
            csvText += "choice_1,choice_2,choice_3,choice_4,choice_5,choice_6,choice_7,choice_8,choice_9,choice_10,";
            csvText += "status,difficulty,question_category_code,keyword,can_fail_exam,feedback_correct,feedback_incorrect";

            for (int i = 0; i < questions.Count; i++)
            {

                Question q = questions[i];

                List<string> errorsFound = QuestionLib.CheckQuestion(q);

                if (errorsFound.Count == 0)
                {

                    string csv = GetQuestionAsCSV(q);

                    if (csv.Length > 0)
                    {
                        csvText += Environment.NewLine + csv;
                    }

                }
                else
                {
                    questionsSkipped++;
                }

            }

            result.Add("\tCSV text created; questions skipped: " + questionsSkipped.ToString());


            // write csv file
            JwCSV.WriteToFile(filename, csvText);
            result.Add("\tCSV file created.");

            // create image folder
            List<Question> questionsWithImageData = QuestionLib.GetQuestionsWithImageData(questions);
            string imageFolder = CFG.outputDir + Path.GetFileNameWithoutExtension(filename) + "-img";

            if (questionsWithImageData.Count > 0)
            {

                System.IO.Directory.CreateDirectory(imageFolder);
                result.Add("\timage dir created.");


                // create images
                int imageCount = 0;
                for (int i = 0; i < questionsWithImageData.Count; i++)
                {

                    Question q = questionsWithImageData[i];
                    if (q.image != String.Empty && q.imageData != String.Empty)
                    {
                        string imageFile = Path.Combine(imageFolder, q.image);
                        File.WriteAllBytes(imageFile, Convert.FromBase64String(q.imageData));
                        imageCount++;
                    }
                }

                result.Add("\t" + imageCount.ToString() + " images created.");

            }

            // return result
            return result;

        }





        public static List<string> CreateMResponseCSV(string filename, List<Question> questions)
        {

            int questionsSkipped = 0;
            List<string> result = new List<string>();
            result.Add("Processing MResponse question types....");

            // build CSV text
            string csvText = "code,body,";
            csvText += "choice_1,answer_1,choice_2,answer_2,choice_3,answer_3,choice_4,answer_4,choice_5,answer_5,";
            csvText += "choice_6,answer_6,choice_7,answer_7,choice_8,answer_8,choice_9,answer_9,choice_10,answer_10,";
            csvText += "status,difficulty,question_category_code,keyword,can_fail_exam";

            for (int i = 0; i < questions.Count; i++)
            {

                Question q = questions[i];

                List<string> errorsFound = QuestionLib.CheckQuestionMResponse(q);

                if (errorsFound.Count == 0)
                {

                    string csv = GetQuestionMResponseAsCSV(q);

                    if (csv.Length > 0)
                    {
                        csvText += Environment.NewLine + csv;
                    }

                }
                else
                {
                    questionsSkipped++;
                }

            }

            result.Add("\tCSV text created; questions skipped: " + questionsSkipped.ToString());

            // write csv file
            JwCSV.WriteToFile(filename, csvText);
            result.Add("\tCSV file created.");

            List<Question> questionsWithImageData = QuestionLib.GetQuestionsWithImageData(questions);
            string imageFolder = CFG.outputDir + Path.GetFileNameWithoutExtension(filename) + "-img";

            if (questionsWithImageData.Count > 0)
            {

                // create image folder
                System.IO.Directory.CreateDirectory(imageFolder);
                result.Add("\timage dir created.");

                // create images
                int imageCount = 0;
                for (int i = 0; i < questionsWithImageData.Count; i++)
                {

                    Question q = questionsWithImageData[i];
                    if (q.image != String.Empty && q.imageData != String.Empty)
                    {
                        string imageFile = Path.Combine(imageFolder, q.image);
                        File.WriteAllBytes(imageFile, Convert.FromBase64String(q.imageData));
                        imageCount++;
                    }
                }

                result.Add("\t" + imageCount.ToString() + " images created.");

            }


            // return result
            return result;

        }








        public static List<string> CreateHotspotCSV(string filename, List<Question> questions)
        {

            //int questionsSkipped = 0;
            List<string> result = new List<string>();
            result.Add("Processing hotspot question types....");

            // build CSV text
            string csvText = "code,body,correct_choice,";
            csvText += "choice_1,choice_2,choice_3,choice_4,choice_5,choice_6,choice_7,choice_8,choice_9,choice_10,";
            csvText += "status,difficulty,question_category_code,keyword,can_fail_exam,feedback_correct,feedback_incorrect";

            for (int i = 0; i < questions.Count; i++)
            {

                Question q = questions[i];

                //List<string> errorsFound = QuestionLib.CheckQuestion(q);

                //if (errorsFound.Count == 0)
                //{

                    string csv = GetQuestionHotspotAsCSV(q);

                    if (csv.Length > 0)
                    {
                        csvText += Environment.NewLine + csv;
                    }

                //}
                ////else
                //{
                //    questionsSkipped++;
                //}

            }

            result.Add("\tCSV text created.");


            // write csv file
            JwCSV.WriteToFile(filename, csvText);
            result.Add("\tCSV file created.");

            // create image folder
            List<Question> questionsWithImageData = QuestionLib.GetQuestionsWithImageData(questions);
            string imageFolder = CFG.outputDir + Path.GetFileNameWithoutExtension(filename) + "-img";

            if (questionsWithImageData.Count > 0)
            {

                System.IO.Directory.CreateDirectory(imageFolder);
                result.Add("\timage dir created.");


                // create images
                int imageCount = 0;
                for (int i = 0; i < questionsWithImageData.Count; i++)
                {

                    Question q = questionsWithImageData[i];
                    if (q.image != String.Empty && q.imageData != String.Empty)
                    {
                        string imageFile = Path.Combine(imageFolder, q.image);
                        File.WriteAllBytes(imageFile, Convert.FromBase64String(q.imageData));
                        imageCount++;
                    }
                }

                result.Add("\t" + imageCount.ToString() + " images created.");

            }

            // return result
            return result;

        }






        public static string GetQuestionAsCSV(Question q)
        {

            string csv = "";

            if ((q.type == "multichoice" || q.type == "truefalse" || q.type == "Multiple Choice" || q.type == "cloze") == false) { return ""; }

            //TODO - this is duplicated in GetCSVText
            List<string> errors = QuestionLib.CheckQuestion(q);
            if (errors.Count > 0) { return ""; }

            // name - code
            csv += "\"" + q.name + "\"" + ",";

            // questionText - body
            csv += "\"" + q.text + "\"" + ",";

            // correct_choice
            int correct_choice = 1;
            for (int i = 0; i < q.options.Count; i++)
            {
                if (q.options[i].grade == 100) { correct_choice = i + 1; }
            }
            csv += "choice_" + correct_choice.ToString() + ",";

            // answer options
            for (int i = 0; i < 10; i++)
            {
                if (i < q.options.Count)
                {
                    csv += "\"" + q.options[i].text + "\"" + ",";
                }
                else
                {
                    csv += ",";
                }
            }

            csv += "Open,L1,";

            // category - question_category_code
            csv += "\"" + q.category + "\"" + ",";

            string feedback_right = "";
            string feedback_wrong = "";

            for (int i = 0; i < q.options.Count; i++)
            {
                if (q.options[i].grade > 0)
                {
                    feedback_right = q.options[i].feedback;
                }
                else
                {
                    feedback_wrong = q.options[i].feedback;
                }
            }

            csv += "\"" + q.image + "\"" + ",n," + "\"" + feedback_right + "\"" + "," + "\"" + feedback_wrong + "\"";

            return csv;

        }





        public static string GetQuestionMResponseAsCSV(Question q)
        {

            string csv = "";

            if ((q.type == "multichoice" || q.type == "Multiple Choice" || q.type == "multichoiceset") == false) { return ""; }

            // name - code
            csv += "\"" + q.name + "\"" + ",";

            // questionText - body
            csv += "\"" + q.text + "\"" + ",";

            // answer options
            for (int i = 0; i < 10; i++)
            {

                if (i < q.options.Count)
                {
                    csv += "\"" + q.options[i].text + "\"" + ",";
                    if (q.options[i].grade <= 0)
                    {
                        csv += "FALSE,";
                    } else
                    {
                        csv += "TRUE,";
                    }
                } else
                {
                    csv += "\"\",\"\",";
                }

            }

            csv += "Open,L1,";

            // category - question_category_code
            csv += "\"" + q.category + "\"" + ",";

            csv += "\"\",\"\"";

            return csv;

        }





        public static string GetQuestionHotspotAsCSV(Question q)
        {

            string csv = "";

            // name - code
            csv += "\"" + q.name + "\"" + ",";

            // questionText - body
            csv += "\"" + q.text + "\"" + ",";

            csv += "\"" + q.data + "\"" + ",";

            // answer options
            for (int i = 0; i < 10; i++)
            {
                csv += ",";
            }

            csv += "Open,L1,";

            // category - question_category_code
            csv += "\"" + q.category + "\"" + ",";

            string feedback_right = "";
            string feedback_wrong = "";

            csv += "\"" + q.image + "\"" + ",n," + "\"" + feedback_right + "\"" + "," + "\"" + feedback_wrong + "\"";

            return csv;

        }





        public static List<string> CreateXML(string filename, List<Question> questions)
        {

            int questionsSkipped = 0;
            List<string> result = new List<string>();
            result.Add("Processing " +questions.Count.ToString() + " unsupported question types to XML....");

            // make new XML doc
            XmlDocument xmlDoc = new XmlDocument();

            XmlNode quizNode = xmlDoc.CreateElement("quiz");
            xmlDoc.AppendChild(quizNode);

            // add nodes
            for (int i = 0; i < questions.Count; i++)
            {
                XmlNode qNode = questions[i].xmlNode;
                if (qNode != null)
                {

                    XmlNode question = quizNode.OwnerDocument.ImportNode(qNode, true);
                    quizNode.AppendChild(question);

                }
            }

            

            // write to file
            JwXML.WriteToFile(xmlDoc, filename);

            result.Add("\tOK. " + questionsSkipped.ToString() + " questions skipped (no XML data)");

            // return result
            return result;

        }








        public static List<string> CheckFile(string csvFile)
        {

            List<string> errors = new List<string>();

            // default file
            errors.AddRange(CheckFileDefault(csvFile));

            // MResponse file
            string filenameMResponse = Path.Combine(CFG.outputDir, Path.GetFileNameWithoutExtension(csvFile) + "-mresponse.csv");
            errors.AddRange(CheckFileMResponse(filenameMResponse));

            return errors;

        }



        public static List<string> CheckFileDefault(string csvFile)
        {

            List<string> errors = new List<string>();

            if (!File.Exists(csvFile)) { return errors; }

            string[] lines = File.ReadAllLines(csvFile);

            

            for (int i = 0; i < lines.Length; i++)
            {

                List<string> items = JwCSV.GetLineItems(lines[i]);

                if (items.Count < 20)
                {
                    errors.Add("Not enought items in line " + i.ToString());
                    continue;
                }

                // skip 1st line
                if (items[0] == "code") { continue; }

                // check code (0) is not blank
                if (items[0] == String.Empty) { errors.Add("line " + i.ToString() + ": code is blank"); }

                // check correct_choice (2) starts with "choice_"
                if (!items[2].StartsWith("choice_")) { errors.Add("line " + i.ToString() + ": cell 3 should start with 'choice_'"); }

                // check 13 = Open
                if (items[13] != "Open") { errors.Add("line " + i.ToString() + ": cell 14 should be 'Open'"); }

                // check 14 = L1
                if (items[14] != "L1") { errors.Add("line " + i.ToString() + ": cell 15 should be 'L1'"); }

            }

            return errors;

        }




        public static List<string> CheckFileMResponse(string csvFile)
        {

            List<string> errors = new List<string>();

            if (!File.Exists(csvFile)) { return errors; }

            string[] lines = File.ReadAllLines(csvFile);

            for (int i = 0; i < lines.Length; i++)
            {

                List<string> items = JwCSV.GetLineItems(lines[i]);

                if (items.Count < 27)
                {
                    errors.Add("Not enought items in line " + i.ToString());
                    continue;
                }

                // skip 1st line
                if (items[0] == "code") { continue; }

                // check code (0) is not blank
                if (items[0] == String.Empty) { errors.Add("line " + i.ToString() + ": code is blank"); }

                // check 22 = Open
                if (items[22] != "Open") { errors.Add("line " + i.ToString() + ": cell 23 should be 'Open'"); }

                // check 23 = L1
                if (items[23] != "L1") { errors.Add("line " + i.ToString() + ": cell 24 should be 'L1'"); }

            }

            return errors;

        }












    }
}
