namespace QuestionTools
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.processFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveQuestionsAsTxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.getQuestionsInFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getQuestionTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getCategoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSourceDir = new System.Windows.Forms.TextBox();
            this.buSetSourceDir = new System.Windows.Forms.Button();
            this.txtOutputDir = new System.Windows.Forms.TextBox();
            this.buSetOutputDir = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtQuestions = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtErrors = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.menuStrip.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(137)))), ((int)(((byte)(236)))));
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1107, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox2,
            this.processFileToolStripMenuItem,
            this.saveQuestionsAsTxtToolStripMenuItem,
            this.toolStripSeparator2,
            this.toolStripTextBox1,
            this.getQuestionsInFileToolStripMenuItem,
            this.getQuestionTypesToolStripMenuItem,
            this.getCategoriesToolStripMenuItem,
            this.getImagesToolStripMenuItem,
            this.toolStripSeparator1,
            this.helpToolStripMenuItem,
            this.debugToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.BackColor = System.Drawing.SystemColors.Window;
            this.toolStripTextBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripTextBox2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.ReadOnly = true;
            this.toolStripTextBox2.Size = new System.Drawing.Size(100, 27);
            this.toolStripTextBox2.Text = "Files";
            // 
            // processFileToolStripMenuItem
            // 
            this.processFileToolStripMenuItem.Name = "processFileToolStripMenuItem";
            this.processFileToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.processFileToolStripMenuItem.Text = "Process";
            this.processFileToolStripMenuItem.Click += new System.EventHandler(this.processFileToolStripMenuItem_Click);
            // 
            // saveQuestionsAsTxtToolStripMenuItem
            // 
            this.saveQuestionsAsTxtToolStripMenuItem.Name = "saveQuestionsAsTxtToolStripMenuItem";
            this.saveQuestionsAsTxtToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.saveQuestionsAsTxtToolStripMenuItem.Text = "Save Questions as txt";
            this.saveQuestionsAsTxtToolStripMenuItem.Click += new System.EventHandler(this.saveQuestionsAsTxtToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(228, 6);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripTextBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.ReadOnly = true;
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 27);
            this.toolStripTextBox1.Text = "Info";
            // 
            // getQuestionsInFileToolStripMenuItem
            // 
            this.getQuestionsInFileToolStripMenuItem.Name = "getQuestionsInFileToolStripMenuItem";
            this.getQuestionsInFileToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.getQuestionsInFileToolStripMenuItem.Text = "Get Questions In File";
            this.getQuestionsInFileToolStripMenuItem.Click += new System.EventHandler(this.getQuestionsInFileToolStripMenuItem_Click);
            // 
            // getQuestionTypesToolStripMenuItem
            // 
            this.getQuestionTypesToolStripMenuItem.Name = "getQuestionTypesToolStripMenuItem";
            this.getQuestionTypesToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.getQuestionTypesToolStripMenuItem.Text = "Get Question Types";
            this.getQuestionTypesToolStripMenuItem.Click += new System.EventHandler(this.getQuestionTypesToolStripMenuItem_Click);
            // 
            // getCategoriesToolStripMenuItem
            // 
            this.getCategoriesToolStripMenuItem.Name = "getCategoriesToolStripMenuItem";
            this.getCategoriesToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.getCategoriesToolStripMenuItem.Text = "Get Categories";
            this.getCategoriesToolStripMenuItem.Click += new System.EventHandler(this.getCategoriesToolStripMenuItem_Click);
            // 
            // getImagesToolStripMenuItem
            // 
            this.getImagesToolStripMenuItem.Name = "getImagesToolStripMenuItem";
            this.getImagesToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.getImagesToolStripMenuItem.Text = "Get Images";
            this.getImagesToolStripMenuItem.Click += new System.EventHandler(this.getImagesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(228, 6);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.Click += new System.EventHandler(this.debugToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Open Sans SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(123, 74);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 27);
            this.label2.TabIndex = 19;
            this.label2.Text = "Output Dir:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Open Sans SemiBold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(125, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 27);
            this.label1.TabIndex = 18;
            this.label1.Text = "Source Dir:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSourceDir
            // 
            this.txtSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSourceDir.Location = new System.Drawing.Point(244, 47);
            this.txtSourceDir.Name = "txtSourceDir";
            this.txtSourceDir.Size = new System.Drawing.Size(803, 22);
            this.txtSourceDir.TabIndex = 20;
            // 
            // buSetSourceDir
            // 
            this.buSetSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buSetSourceDir.Location = new System.Drawing.Point(1053, 47);
            this.buSetSourceDir.Name = "buSetSourceDir";
            this.buSetSourceDir.Size = new System.Drawing.Size(42, 23);
            this.buSetSourceDir.TabIndex = 21;
            this.buSetSourceDir.Text = "...";
            this.buSetSourceDir.UseVisualStyleBackColor = true;
            this.buSetSourceDir.Click += new System.EventHandler(this.buSetSourceDir_Click);
            // 
            // txtOutputDir
            // 
            this.txtOutputDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputDir.Location = new System.Drawing.Point(244, 79);
            this.txtOutputDir.Name = "txtOutputDir";
            this.txtOutputDir.Size = new System.Drawing.Size(803, 22);
            this.txtOutputDir.TabIndex = 22;
            // 
            // buSetOutputDir
            // 
            this.buSetOutputDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buSetOutputDir.Location = new System.Drawing.Point(1053, 79);
            this.buSetOutputDir.Name = "buSetOutputDir";
            this.buSetOutputDir.Size = new System.Drawing.Size(42, 23);
            this.buSetOutputDir.TabIndex = 23;
            this.buSetOutputDir.Text = "...";
            this.buSetOutputDir.UseVisualStyleBackColor = true;
            this.buSetOutputDir.Click += new System.EventHandler(this.buSetOutputDir_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 138);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1083, 373);
            this.tabControl1.TabIndex = 25;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtOutput);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1075, 344);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Output";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.BackColor = System.Drawing.SystemColors.Window;
            this.txtOutput.Location = new System.Drawing.Point(3, 6);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOutput.Size = new System.Drawing.Size(1066, 332);
            this.txtOutput.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtQuestions);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1075, 344);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Questions";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtQuestions
            // 
            this.txtQuestions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQuestions.BackColor = System.Drawing.SystemColors.Window;
            this.txtQuestions.Location = new System.Drawing.Point(5, 6);
            this.txtQuestions.Multiline = true;
            this.txtQuestions.Name = "txtQuestions";
            this.txtQuestions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtQuestions.Size = new System.Drawing.Size(1064, 332);
            this.txtQuestions.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtErrors);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1075, 344);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Errors";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtErrors
            // 
            this.txtErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtErrors.BackColor = System.Drawing.SystemColors.Window;
            this.txtErrors.Location = new System.Drawing.Point(5, 6);
            this.txtErrors.Multiline = true;
            this.txtErrors.Name = "txtErrors";
            this.txtErrors.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtErrors.Size = new System.Drawing.Size(1067, 335);
            this.txtErrors.TabIndex = 1;
            // 
            // txtMessage
            // 
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage.BackColor = System.Drawing.SystemColors.Menu;
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessage.Font = new System.Drawing.Font("Open Sans", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.txtMessage.Location = new System.Drawing.Point(12, 108);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(1083, 24);
            this.txtMessage.TabIndex = 26;
            this.txtMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 523);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buSetOutputDir);
            this.Controls.Add(this.txtOutputDir);
            this.Controls.Add(this.buSetSourceDir);
            this.Controls.Add(this.txtSourceDir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Main";
            this.Text = "Question Tools v1.08";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processFileToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buSetSourceDir;
        public System.Windows.Forms.TextBox txtSourceDir;
        public System.Windows.Forms.TextBox txtOutputDir;
        private System.Windows.Forms.Button buSetOutputDir;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.TextBox txtQuestions;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txtErrors;
        private System.Windows.Forms.ToolStripMenuItem getQuestionsInFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem getQuestionTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getCategoriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem saveQuestionsAsTxtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
    }
}

