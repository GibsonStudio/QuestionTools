using QuestionTools.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;



//TODO check code still works with other input formats



namespace QuestionTools
{
    public partial class Main : Form
    {

        private Help help = new Help();
        public static Main myThis;
        public List<Question> g_questions = new List<Question>();
        public string g_filename = "";


  
        public Main()
        {

            InitializeComponent();
            IDictionary<string, string> configResult = CFG.init();
            txtSourceDir.Text = configResult["sourceDir"];
            txtOutputDir.Text = configResult["outputDir"];

            myThis = this;

        }






        private void ClearText()
        {

            txtMessage.Text = "";
            txtOutput.Text = "";
            txtQuestions.Text = "";
            txtErrors.Text = "";

            tabControl1.SelectedIndex = 0;

        }





        private string getFileDialog()
        {

            string result = "";

            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "All Files|*.*|XML Files|*.xml|L-plus Excel|*.xlsx";
            f.InitialDirectory = CFG.sourceDir;
            f.Title = "Select file to process:";
            f.RestoreDirectory = false;
            f.ShowHelp = true;

            if (f.ShowDialog() == DialogResult.OK)
            {
                result = f.FileName;
            }

            f.Dispose();

            return result;

        }








        private void processFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string myFile = getFileDialog();

            if (myFile != string.Empty)
            {
                ProcessFile(myFile);
            }

        }







        private void ProcessFile(string sourceFile)
        {

            ClearText();

            ShowOutput("Processing file " + sourceFile);

            // detect file format
            ShowOutput("Detecting file format....", false);
            string fileFormat = QuestionLib.GetFileFormat(sourceFile);
            ShowOutput(fileFormat);

            // get questions
            ShowOutput("Getting questions....", false);

            List<Question> questions = QuestionLib.GetQuestions(sourceFile);
            ShowOutput(questions.Count.ToString() + " questions found.");

            // look for questions with new feedback, cloze and multichoiceset
            List<Question> qNewFeedback = QuestionLib.GetQuestionsWithNewFeedback(questions);
            ShowOutput("Questions with new feedback: " + qNewFeedback.Count.ToString());


            // show questions in tab
            foreach (Question q in questions)
            {
                txtQuestions.AppendText(q.GetDebug() + Environment.NewLine + Environment.NewLine);
            }

            // check questions
            ShowOutput("Checking for errors....", false);
            List<string> errorList = QuestionLib.CheckQuestions(questions);
            ShowOutput(errorList.Count.ToString() + " errors found.");

            txtErrors.Text = "";

            if (errorList.Count > 0)
            {
                txtErrors.AppendText(Environment.NewLine + errorList.Count.ToString() + " errors in " + sourceFile + Environment.NewLine);
            }

            foreach (string e in errorList)
            {
                txtErrors.AppendText(e + Environment.NewLine);
            }



            // create output (Pelesys CSV)
            string csvFilename = Path.Combine(CFG.outputDir, JwCSV.GetFileName(sourceFile));
            List<string> result = Pelesys.CreateOutput(csvFilename, questions);

            for (int i = 0; i < result.Count; i++)
            {
                ShowOutput(result[i]);
            }


            // check output files
            ShowOutput("Checking CSV file....", false);
            List<string> errors = Pelesys.CheckFile(csvFilename);

            if (errors.Count == 0)
            {
                ShowOutput("OK");
            }
            else
            {
                ShowOutput(errors.Count.ToString() + " errors found. See Errors tab.");
                txtErrors.AppendText(Environment.NewLine + errors.Count.ToString() + " errors in " + csvFilename + Environment.NewLine);

                foreach (string e in errors)
                {
                    txtErrors.AppendText(e + Environment.NewLine);
                }

            }

            ShowMessage("Done. " + errors.Count.ToString() + " errors in output file.");


        }



        private void getQuestionsInFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string myFile = getFileDialog();

            if (myFile != string.Empty)
            {
                getQuestionsInFile(myFile);
            }

        }



        private void getQuestionsInFile(string sourceFile)
        {

            ClearText();

            ShowOutput("Processing file " + sourceFile);

            // detect file format
            ShowOutput("Detecting file format....", false);
            string fileFormat = QuestionLib.GetFileFormat(sourceFile);
            ShowOutput(fileFormat);

            // get questions
            ShowOutput("Getting questions....", false);
            List<Question> questions = QuestionLib.GetQuestions(sourceFile);
            ShowOutput(questions.Count.ToString() + " questions found.");

            foreach (Question q in questions)
            {
                txtQuestions.AppendText(q.GetDebug() + Environment.NewLine + Environment.NewLine);
            }


            // check questions
            ShowOutput("Checking for errors....", false);
            List<string> errorList = QuestionLib.CheckQuestions(questions);
            ShowOutput(errorList.Count.ToString() + " errors found.");

            txtErrors.Text = "";

            if (errorList.Count > 0)
            {
                txtErrors.AppendText(Environment.NewLine + errorList.Count.ToString() + " errors in " + sourceFile + Environment.NewLine);
            }

            foreach (string e in errorList)
            {
                txtErrors.AppendText(e + Environment.NewLine);
            }


            // look for questions with embedded images
            ShowOutput("Looking for questions with images....", false);
            List<Question> withImages = QuestionLib.GetQuestionListWithImages(questions);
            ShowOutput(withImages.Count.ToString() + " found.");



            // look for questions with new feedback, cloze and multichoiceset
            List<Question> qNewFeedback = QuestionLib.GetQuestionsWithNewFeedback(questions);
            ShowOutput("Questions with new feedback: " + qNewFeedback.Count.ToString());




            // finished
            ShowMessage("Done. " + questions.Count.ToString() + " questions found.");


        }









        public void ShowMessage(string message)
        {
            txtMessage.Text = message;
            txtMessage.Update();
        }






        public void ShowOutput(string message, bool newLine = true)
        {
            txtOutput.AppendText(message);
            if (newLine) { txtOutput.AppendText(Environment.NewLine); }
            txtOutput.Update();
        }




        private void buSetSourceDir_Click(object sender, EventArgs e)
        {
            txtSourceDir.Text = CFG.SetSourceDir();
        }





        private void buSetOutputDir_Click(object sender, EventArgs e)
        {
            txtOutputDir.Text = CFG.SetOutputDir();
        }



        




        private void getCategoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string myFile = getFileDialog();

            if (myFile != string.Empty)
            {

                ClearText();
                ShowOutput("Getting question categories in " + myFile + "....");

                SortedList<string, int> categories = QuestionLib.GetCategories(myFile);

                foreach (KeyValuePair<string, int> entry in categories)
                {
                    ShowOutput(entry.Key + "  (" + entry.Value.ToString() + ")");
                }

                ShowMessage("Done. " + categories.Count.ToString() + " categories found.");

            }

            
        }




        private void getQuestionTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string myFile = getFileDialog();

            if (myFile != string.Empty)
            {

                ClearText();
                ShowOutput("Getting question types in " + myFile + "....");

                SortedList<string, int> qtypes = QuestionLib.GetTypes(myFile);

                foreach (KeyValuePair<string, int> entry in qtypes)
                {
                    ShowOutput(entry.Key + "  (" + entry.Value.ToString() + ")");
                }

                ShowMessage("Done. " + qtypes.Count.ToString() + " question types found.");

            }

        }






        private void saveQuestionsAsTxtToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "Text Files|*.txt";
            s.InitialDirectory = CFG.outputDir;
            s.Title = "Save Questions as text:";

            if (s.ShowDialog() == DialogResult.OK)
            {
                WriteToFile(s.FileName, txtQuestions.Text);

                if (File.Exists(s.FileName))
                {
                    ShowMessage("File saved OK.");
                }
                else
                {
                    ShowMessage("ERROR: File not saved.");
                }
            }

            s.Dispose();

        }




        private void WriteToFile(string myFile, string txtData)
        {

            ShowOutput("Writing to file " + myFile + "....", false);

            if (File.Exists(myFile))
            {
                File.Delete(myFile);
            }

            // write this data to a file
            FileStream fs = new FileStream(myFile, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.Write(txtData);
            sw.Close();

            ShowOutput("OK");

        }




        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            help.Show();
        }






        private void getImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string myFile = getFileDialog();

            if (myFile != string.Empty)
            {

                ClearText();
                ShowOutput("Getting question images in " + myFile + "....", false);

                SortedList<string, int> images = QuestionLib.GetImages(myFile);

                ShowOutput("Done.");

                foreach (KeyValuePair<string, int> entry in images)
                {
                    ShowOutput(entry.Key + "  (" + entry.Value.ToString() + ")");
                }

                ShowMessage("Done. " + images.Count.ToString() + " images found.");

            }

        }





        // menu: Debug
        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string myFile = @"C:\jon\data-to-process\bomardier\source\2-images.xml";

            ProcessFile(myFile);

        }






        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }






    }
}
