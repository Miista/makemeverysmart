namespace VerySmart_UI
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
            this.makeMeVerySmartBtn = new System.Windows.Forms.Button();
            this.inputTextBox = new System.Windows.Forms.RichTextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.synonymSelectionBox = new System.Windows.Forms.GroupBox();
            this.selectRandom = new System.Windows.Forms.RadioButton();
            this.selectLongest = new System.Windows.Forms.RadioButton();
            this.speakItBtn = new System.Windows.Forms.Button();
            this.repeatLastBtn = new System.Windows.Forms.Button();
            this.outputTextBox = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.selectComplex = new System.Windows.Forms.RadioButton();
            this.panel3.SuspendLayout();
            this.synonymSelectionBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // makeMeVerySmartBtn
            // 
            this.makeMeVerySmartBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.makeMeVerySmartBtn.Location = new System.Drawing.Point(427, 345);
            this.makeMeVerySmartBtn.Name = "makeMeVerySmartBtn";
            this.makeMeVerySmartBtn.Size = new System.Drawing.Size(136, 23);
            this.makeMeVerySmartBtn.TabIndex = 2;
            this.makeMeVerySmartBtn.Text = "Make Me Very Smart";
            this.makeMeVerySmartBtn.UseVisualStyleBackColor = true;
            this.makeMeVerySmartBtn.Click += new System.EventHandler(this.makeMeVerySmartBtn_Click);
            // 
            // inputTextBox
            // 
            this.inputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputTextBox.Location = new System.Drawing.Point(0, 0);
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(409, 165);
            this.inputTextBox.TabIndex = 1;
            this.inputTextBox.Text = "i wanted to write an essay";
            this.inputTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.inputTextBox_KeyPress);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 345);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(409, 23);
            this.progressBar.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.synonymSelectionBox);
            this.panel3.Location = new System.Drawing.Point(427, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(136, 269);
            this.panel3.TabIndex = 8;
            // 
            // synonymSelectionBox
            // 
            this.synonymSelectionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.synonymSelectionBox.Controls.Add(this.selectComplex);
            this.synonymSelectionBox.Controls.Add(this.selectRandom);
            this.synonymSelectionBox.Controls.Add(this.selectLongest);
            this.synonymSelectionBox.Location = new System.Drawing.Point(3, 3);
            this.synonymSelectionBox.Name = "synonymSelectionBox";
            this.synonymSelectionBox.Size = new System.Drawing.Size(130, 93);
            this.synonymSelectionBox.TabIndex = 5;
            this.synonymSelectionBox.TabStop = false;
            this.synonymSelectionBox.Text = "Synonym Selection";
            // 
            // selectRandom
            // 
            this.selectRandom.AutoSize = true;
            this.selectRandom.Location = new System.Drawing.Point(7, 44);
            this.selectRandom.Name = "selectRandom";
            this.selectRandom.Size = new System.Drawing.Size(65, 17);
            this.selectRandom.TabIndex = 1;
            this.selectRandom.Text = "Random";
            this.selectRandom.UseVisualStyleBackColor = true;
            // 
            // selectLongest
            // 
            this.selectLongest.AutoSize = true;
            this.selectLongest.Checked = true;
            this.selectLongest.Location = new System.Drawing.Point(7, 20);
            this.selectLongest.Name = "selectLongest";
            this.selectLongest.Size = new System.Drawing.Size(63, 17);
            this.selectLongest.TabIndex = 0;
            this.selectLongest.TabStop = true;
            this.selectLongest.Text = "Longest";
            this.selectLongest.UseVisualStyleBackColor = true;
            // 
            // speakItBtn
            // 
            this.speakItBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.speakItBtn.Location = new System.Drawing.Point(427, 287);
            this.speakItBtn.Name = "speakItBtn";
            this.speakItBtn.Size = new System.Drawing.Size(136, 22);
            this.speakItBtn.TabIndex = 7;
            this.speakItBtn.Text = "Read It Aloud!";
            this.speakItBtn.UseVisualStyleBackColor = true;
            this.speakItBtn.Click += new System.EventHandler(this.speakItBtn_Click);
            // 
            // repeatLastBtn
            // 
            this.repeatLastBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.repeatLastBtn.Enabled = false;
            this.repeatLastBtn.Location = new System.Drawing.Point(427, 315);
            this.repeatLastBtn.Name = "repeatLastBtn";
            this.repeatLastBtn.Size = new System.Drawing.Size(136, 24);
            this.repeatLastBtn.TabIndex = 6;
            this.repeatLastBtn.Text = "Again!!!";
            this.repeatLastBtn.UseVisualStyleBackColor = true;
            this.repeatLastBtn.Click += new System.EventHandler(this.repeatLastBtn_Click);
            // 
            // outputTextBox
            // 
            this.outputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputTextBox.Location = new System.Drawing.Point(0, 0);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(409, 158);
            this.outputTextBox.TabIndex = 3;
            this.outputTextBox.Text = "";
            this.outputTextBox.TextChanged += new System.EventHandler(this.outputTextBox_TextChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.inputTextBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.outputTextBox);
            this.splitContainer1.Size = new System.Drawing.Size(409, 327);
            this.splitContainer1.SplitterDistance = 165;
            this.splitContainer1.TabIndex = 10;
            // 
            // selectComplex
            // 
            this.selectComplex.AutoSize = true;
            this.selectComplex.Location = new System.Drawing.Point(7, 67);
            this.selectComplex.Name = "selectComplex";
            this.selectComplex.Size = new System.Drawing.Size(75, 17);
            this.selectComplex.TabIndex = 6;
            this.selectComplex.TabStop = true;
            this.selectComplex.Text = "Complexity";
            this.selectComplex.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 379);
            this.Controls.Add(this.repeatLastBtn);
            this.Controls.Add(this.speakItBtn);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.makeMeVerySmartBtn);
            this.Name = "Main";
            this.Text = "Form1";
            this.panel3.ResumeLayout(false);
            this.synonymSelectionBox.ResumeLayout(false);
            this.synonymSelectionBox.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button makeMeVerySmartBtn;
        private System.Windows.Forms.RichTextBox inputTextBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox synonymSelectionBox;
        private System.Windows.Forms.RadioButton selectRandom;
        private System.Windows.Forms.RadioButton selectLongest;
        private System.Windows.Forms.RichTextBox outputTextBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button repeatLastBtn;
        private System.Windows.Forms.Button speakItBtn;
        private System.Windows.Forms.RadioButton selectComplex;
    }
}

