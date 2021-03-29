using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;


namespace QuestionTools.classes
{
    static class QuestionLib
    {



        public static List<Question> GetQuestions(string sourceFile)
        {

            string fileFormat = GetFileFormat(sourceFile);

            // make questions (List<Question>) from xml file
            List<Question> qList = new List<Question>();

            if (fileFormat == "qantasXML") { qList = Qantas.ReadFile(sourceFile); }
            if (fileFormat == "moodleXML") { qList = Moodle.ReadFile(sourceFile); }
            if (fileFormat == "lplusExcel") { qList = LPlus.ReadFile(sourceFile); }
            if (fileFormat == "pelesysXML") { qList = PelesysXML.ReadFile(sourceFile); }

            return qList;


        }




        public static SortedList<string, int> GetCategories(string sourceFile)
        {

            string fileFormat = GetFileFormat(sourceFile);

            SortedList<string, int> categories = new SortedList<string, int>();

            List<Question> qList = GetQuestions(sourceFile);

            foreach (Question q in qList)
            {

                if (categories.ContainsKey(q.category))
                {
                    categories[q.category]++;
                }
                else
                {
                    categories.Add(q.category, 1);
                }

            }

            return categories;

        }





        public static SortedList<string, int> GetImages(string sourceFile)
        {

            string fileFormat = GetFileFormat(sourceFile);

            SortedList<string, int> images = new SortedList<string, int>();

            List<Question> qList = GetQuestions(sourceFile);

            foreach (Question q in qList)
            {

                if (q.images.Count > 0)
                {

                    for (int i = 0; i < q.images.Count; i++)
                    {

                        string im = q.images[i].name.Trim();

                        if (im != String.Empty)
                        {

                            if (images.ContainsKey(im))
                            {
                                images[im]++;
                            }
                            else
                            {
                                images.Add(im, 1);
                            }

                        }

                    }

                }
                

                /*
                string im = q.image.Trim();

                if (q.imageData != String.Empty) { im += " [DATA]";  }

                if (im != String.Empty)
                {

                    if (images.ContainsKey(im))
                    {
                        images[im]++;
                    }
                    else
                    {
                        images.Add(im, 1);
                    }

                }
                */
            }

            return images;

        }







        public static SortedList<string, int> GetTypes(string sourceFile)
        {

            string fileFormat = GetFileFormat(sourceFile);

            SortedList<string, int> qtypes = new SortedList<string, int>();

            List<Question> qList = GetQuestions(sourceFile);

            foreach (Question q in qList)
            {

                if (qtypes.ContainsKey(q.type))
                {
                    qtypes[q.type]++;
                }
                else
                {
                    qtypes.Add(q.type, 1);
                }

            }

            return qtypes;

        }





        public static string GetFileFormat(string sourceFile)
        {

            string fileFormat = "unknown";

            // check file extension
            string ext = Path.GetExtension(sourceFile);
            if (ext == ".xlsx")
            {
                return "lplusExcel";
            }


            XmlDocument doc = new XmlDocument();
            doc.Load(sourceFile);

            XmlNodeList items = doc.SelectNodes("/questestinterop/item");
            if (items.Count > 0) { fileFormat = "qantasXML"; }

            XmlNodeList questions = doc.SelectNodes("/quiz/question");
            if (questions.Count > 0) { fileFormat = "moodleXML"; }

            XmlNodeList questions2 = doc.SelectNodes("/questions/question");
            if (questions2.Count > 0) { fileFormat = "pelesysXML"; }

            return fileFormat;

        }








        public static List<string> CheckQuestions(List<Question> qList)
        {

            List<string> errorList = new List<string>();

            foreach (Question q in qList)
            {

                List<string> errors = CheckQuestion(q);

                if (errors.Count > 0)
                {

                    foreach (string error in errors)
                    {
                        errorList.Add(q.name + ": " + error);
                    }

                }

            }

            return errorList;

        }








        public static List<string> CheckQuestion(Question q)
        {

            List<string> errorsFound = new List<string>();

            // check name
            if (q.name.Length < 1)
            {
                errorsFound.Add("Question has no name.");
            }

            // check category
            if (q.category.Length < 1)
            {
                errorsFound.Add("Question has no category.");
            }

            // check number of answer options
            //if (q.type == "multichoice" && q.options.Count != 4)
            //{
            //    errorsFound.Add(q.options.Count.ToString() + " answer options found, should be 4.");
            //}

            if (q.type == "truefalse" && q.options.Count != 2)
            {
                errorsFound.Add(q.options.Count.ToString() + " answer options found, should be 2.");
            }


            // check option grades
            int scoreOptions = 0;

            for (int n = 0; n < q.options.Count; n++)
            {
                if (q.options[n].grade != 0) { scoreOptions++; }
            }

            if (scoreOptions == 0)
            {
                errorsFound.Add("Question has no correct answer specified.");
            }

            return errorsFound;

        }





        public static List<string> CheckQuestionMResponse(Question q)
        {

            List<string> errorsFound = new List<string>();

            // check name
            if (q.name.Length < 1)
            {
                errorsFound.Add("Question has no name.");
            }

            // check category
            if (q.category.Length < 1)
            {
                errorsFound.Add("Question has no category.");
            }            

            return errorsFound;

        }





        public static List<Question> GetQuestionListWithImages(List<Question> qList)
        {

            List<Question> qListImages = new List<Question>();

            foreach (Question q in qList)
            {

                if (q.text.Contains("<img"))
                {
                    qListImages.Add(q);
                }

            }

            return qListImages;

        }




        public static List<Question> GetQuestionsWithImageData(List<Question> questions)
        {

            List<Question> questionsWithImageData = new List<Question>();

            for (int i = 0; i < questions.Count; i++)
            {

                if (questions[i].images.Count > 0)
                {
                    questionsWithImageData.Add(questions[i]);
                }

            }

            return questionsWithImageData;

        }




        public static String[] ParseFeedbacks(Question q)
        {

            String[] fb = new String[2];

            String cFB = q.correctfeedback;
            String iFB = q.incorrectfeedback;
            String gFB = q.generalfeedback;
            String pFB = q.partiallycorrectfeedback;

            fb[0] = "";
            fb[1] = "";

            if (cFB != String.Empty && iFB != String.Empty)
            {
                fb[0] = cFB;
                fb[1] = iFB;
            }
            else if (cFB != String.Empty && iFB == String.Empty)
            {
                fb[0] = cFB;
                fb[1] = cFB;
            }
            else if (cFB == String.Empty && iFB != String.Empty)
            {
                fb[0] = iFB;
                fb[1] = iFB;
            }
            else if (cFB == String.Empty && iFB == String.Empty && gFB != String.Empty)
            {
                fb[0] = gFB;
                fb[1] = gFB;
            }
            else if (cFB == String.Empty && iFB == String.Empty && pFB != String.Empty)
            {
                fb[0] = pFB;
                fb[1] = pFB;
            }

            return fb;

        }


        public static List<Question> GetQuestionsWithNewFeedback (List<Question> questions)
        {

            List<Question> result = new List<Question>();

            foreach (Question q in questions)
            {
                
                if (q.HasNewFeedback())
                {
                    result.Add(q);
                }                

            }

            return result;

        }




    }
}
