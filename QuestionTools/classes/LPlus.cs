using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace QuestionTools.classes
{

    static class LPlus
    {





        public static List<Question> ReadFile(string excelFile)
        {

            int headersRow = 2;

            List<Question> qList = new List<Question>();

            // open excel file
            Excel.Application excel = new Excel.Application();
            Excel.Workbook workbook = excel.Workbooks.Open(excelFile);
            Excel._Worksheet worksheet = workbook.Sheets[1];

            int rowCount = worksheet.UsedRange.Rows.Count;
            int colCount = worksheet.UsedRange.Columns.Count;

            // find data indexes
            //main.ShowOutput("Finding data indexes....", false);
            int questionTextIndex = 0;
            int nameIndex = 0;
            int categoryIndex = 0;
            int answerIndex = 0;
            int imageIndex = 0;
            int[] optionIndexes = new int[] { 0, 0, 0, 0 };

            for (int col = 1; col <= colCount; col++)
            {
                
                string v = worksheet.Cells[headersRow, col].Value.ToString();

                if (v == "Question Stem") { questionTextIndex = col; }
                if (v == "Syllabus") { categoryIndex = col; }
                if (v == "Correct Answer") { answerIndex = col; }
                if (v == "Question ID") { nameIndex = col; }
                if (v == "Annex") { imageIndex = col; }

                string[] answerFields = new string[] { "A", "B", "C", "D" };

                for (int i = 0; i < 4; i++)
                {
                    if (v == answerFields[i]) { optionIndexes[i] = col; }
                }

            }

            // check data indexes
            if (questionTextIndex > 0
                && optionIndexes[0] > 0
                && optionIndexes[1] > 0
                && optionIndexes[2] > 0
                && optionIndexes[3] > 0
                && answerIndex > 0
                && imageIndex > 0
                && categoryIndex > 0
                && nameIndex > 0)
            {
                //main.ShowOutput("OK");
            }
            else
            {
                
                Main.myThis.ShowOutput("INDEX ERROR!!");
                if (questionTextIndex == 0) { Main.myThis.ShowOutput("questionTextIndex not found. Should be\"Question Stem\""); }
                if (optionIndexes[0] == 0) { Main.myThis.ShowOutput("optionIndexes(A) not found. Should be\"A\""); }
                if (optionIndexes[0] == 0) { Main.myThis.ShowOutput("optionIndexes(B) not found. Should be\"B\""); }
                if (optionIndexes[0] == 0) { Main.myThis.ShowOutput("optionIndexes(C) not found. Should be\"C\""); }
                if (optionIndexes[0] == 0) { Main.myThis.ShowOutput("optionIndexes(D) not found. Should be\"D\""); }
                if (answerIndex == 0) { Main.myThis.ShowOutput("answerIndex not found. Should be\"Correct Answer\""); }
                if (imageIndex == 0) { Main.myThis.ShowOutput("imageIndex not found. Should be\"Annex\""); }
                if (categoryIndex == 0) { Main.myThis.ShowOutput("categoryIndex not found. Should be\"Syllabus\""); }
                if (nameIndex == 0) { Main.myThis.ShowOutput("nameIndex not found. Should be\"Question ID\""); }

                workbook.Close();
                excel.Quit();
                return qList;
            }

            // get question data
            for (int row = (headersRow + 1); row <= rowCount; row++)
            {

                Question q = new Question();
                q.name = JwString.Clean(worksheet.Cells[row, nameIndex].Value.ToString());
                q.category = CleanCategory(JwString.Clean(worksheet.Cells[row, categoryIndex].Value.ToString()));
                q.text = JwString.Clean(worksheet.Cells[row, questionTextIndex].Value.ToString());

                if (worksheet.Cells[row, imageIndex].Value != null)
                { 
                    string imageName = CleanImage(JwString.Clean(worksheet.Cells[row, imageIndex].Value.ToString()));
                    q.AddImage(imageName, "");
                }

                q.type = "multichoice";

                string answer = worksheet.Cells[row, answerIndex].Value.Trim();
                string[] answerValues = new string[] { "A", "B", "C", "D"};

                for (int i = 1; i <= 4; i++)
                {

                    Option o = new Option();
                    o.text = JwString.Clean(worksheet.Cells[row, optionIndexes[i - 1]].Value.ToString());
                    o.feedback = "";
                    o.grade = 0;
                    if (answer == answerValues[i - 1]) { o.grade = 100; }
                    q.options.Add(o);

                }

                qList.Add(q);

            }

            // close excel and return questions
            workbook.Close();
            excel.Quit();

            return qList;

        }




        private static string CleanCategory(string myCat)
        {

            string[] parts = myCat.Split('.');

            if (parts.Length < 2) { return myCat; }

            myCat = AddLeadingZero(parts[0]) + "." + AddLeadingZero(parts[1]);

            return myCat;

        }



        public static string CleanImage(string myImage)
        {

            myImage = myImage.Trim();

            string[] parts = myImage.Split(',');

            if (parts.Length < 2) { return myImage; }

            if (parts[0] == parts[1])
            {
                return parts[0].Trim();
            }

            return myImage;

        }



        private static string AddLeadingZero(string num)
        {

            num = num.Trim();

            if (num.Length == 1) { num = "0" + num; }

            return num;

        }





        public static List<Question> ReadFileGerman(string excelFile)
        {

            List<Question> qList = new List<Question>();

            // open excel file
            Excel.Application excel = new Excel.Application();
            Excel.Workbook workbook = excel.Workbooks.Open(excelFile);
            Excel._Worksheet worksheet = workbook.Sheets[1];

            int rowCount = worksheet.UsedRange.Rows.Count;
            int colCount = worksheet.UsedRange.Columns.Count;

            // find data indexes
            Main main = new Main();
            main.ShowOutput("Finding data indexes....", false);
            int questionTextIndex = 0;
            int nameIndex = 0;
            int categoryIndex = 0;
            int answerIndex = 0;
            int[] optionIndexes = new int[] { 0, 0, 0, 0 };

            for (int col = 1; col <= colCount; col++)
            {

                string v = worksheet.Cells[1, col].Value.ToString();

                if (v == "Fragetext") { questionTextIndex = col; }
                if (v == "Themenname") { categoryIndex = col; }
                if (v == "Korrekte Antwort(en)") { answerIndex = col; }
                if (v == "ID") { nameIndex = col; }

                for (int i = 1; i <= 4; i++)
                {
                    if (v == "Antwort " + i.ToString()) { optionIndexes[i - 1] = col; }
                }

            }

            // check data indexes
            if (questionTextIndex > 0
                && optionIndexes[0] > 0
                && optionIndexes[1] > 0
                && optionIndexes[2] > 0
                && optionIndexes[3] > 0
                && answerIndex > 0
                && categoryIndex > 0
                && nameIndex > 0)
            {
                main.ShowOutput("OK");
            }
            else
            {
                main.ShowOutput("Error. Indexes not found.");
                workbook.Close();
                excel.Quit();
                return qList;
            }

            // get question data
            for (int row = 2; row <= rowCount; row++)
            {

                Question q = new Question();
                q.name = JwString.Clean(worksheet.Cells[row, nameIndex].Value.ToString());
                q.category = JwString.Clean(worksheet.Cells[row, categoryIndex].Value.ToString());
                q.text = JwString.Clean(worksheet.Cells[row, questionTextIndex].Value.ToString());
                q.type = "multichoice";

                int answer = Int32.Parse(worksheet.Cells[row, answerIndex].Value);

                for (int i = 1; i <= 4; i++)
                {

                    Option o = new Option();
                    o.text = JwString.Clean(worksheet.Cells[row, optionIndexes[i - 1]].Value.ToString());
                    o.feedback = "";
                    o.grade = 0;
                    if (answer == i) { o.grade = 100; }
                    q.options.Add(o);

                }

                qList.Add(q);

            }

            // close excel and return questions
            workbook.Close();
            excel.Quit();

            return qList;

        }



    }

}
