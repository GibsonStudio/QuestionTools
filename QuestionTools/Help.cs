using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuestionTools
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

        private void Help_Load(object sender, EventArgs e)
        {

            rtb_help.LoadFile("data\\help.rtf");
            rtb_qantas.LoadFile("data\\qantas_xml_help.rtf");
            rtb_moodle.LoadFile("data\\moodle_xml_help.rtf");
            rtb_lplus.LoadFile("data\\l-plus_help.rtf");
            rtb_pelesys.LoadFile("data\\pelesys_help.rtf");

        }



        private void HelpClosing (object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true; // this cancels the close event.
        }



    }
}
