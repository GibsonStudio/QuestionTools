using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuestionTools.classes
{


    static class JwString
    {

        public static string Clean(string myString)
        {

            // remove any odd characters
            myString = myString.Replace("Â", "");
            myString = myString.Replace("â€¦", "");

            myString = myString.Trim();

            myString = Regex.Replace(myString, @"\r\n?|\n", " ");
            myString = myString.Replace("\"", "'");

            List<string> trimItems = new List<string> { "<BR>", "<BR/>", "<BR />", "<br>", "<br/>", "<br />", "Default for" };

            foreach (string item in trimItems)
            {
                myString = TrimFromString(myString, item);
            }

            return myString;

        }



        public static string TrimFromString(string myString, string removeString)
        {
            if (myString == String.Empty) { return myString; }

            while (myString.EndsWith(removeString))
            {
                myString = myString.Substring(0, myString.Length - removeString.Length).Trim();
            }

            while (myString.StartsWith(removeString))
            {
                myString = myString.Substring(removeString.Length).Trim();
            }

            return myString;
        }




        public static string CleanCategory(string txt)
        {
            int index = txt.LastIndexOf("/");

            if (index > 0)
            {
                txt = txt.Substring(index + 1);
            }

            txt = txt.Trim();

            if (txt.EndsWith("]"))
            {

                int lPos = txt.LastIndexOf("[");
                txt = txt.Substring(0, lPos).Trim();

            }

            return Clean(txt);

        }





        public static string CleanQuestionName(string txt)
        {
            int index = txt.IndexOf(";");

            if (index > 0)
            {
                txt = txt.Substring(0, index);
            }

            return Clean(txt);
        }





    }




}
