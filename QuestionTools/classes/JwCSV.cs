using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QuestionTools.classes
{




    static class JwCSV
    {




        static public string[] Load(string sourceFile)
        {

            string[] lines = File.ReadAllLines(sourceFile);
            return lines;

        }




        static public List<string> GetLineItems(string txt)
        {

            List<string> items = new List<string>();
            int startChar = 0;
            int quoteCount = 0;

            for (int i = 0; i < txt.Length; i++)
            {

                if (txt[i] == '"')
                {
                    quoteCount++;
                }

                if (txt[i] == ',' && quoteCount % 2 == 0)
                {
                    string item = txt.Substring(startChar, i - startChar);
                    items.Add(item);
                    startChar = i + 1;
                    quoteCount = 0;
                }

                if (i == txt.Length - 1)
                {
                    string item = txt.Substring(startChar);
                    items.Add(item);
                }

            }

            return items;

        }




        public static string GetFileName(string myFile)
        {

            myFile = Path.GetFileNameWithoutExtension(myFile);
            myFile += ".csv";
            return myFile;

        }





        public static bool WriteToFile(string myFile, string txtData)
        {

            if (File.Exists(myFile))
            {
                File.Delete(myFile);
            }

            // write this data to a file
            FileStream fs = new FileStream(myFile, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            sw.Write(txtData);
            sw.Close();

            //TODO - add checking
            return true;

        }






    }



}
