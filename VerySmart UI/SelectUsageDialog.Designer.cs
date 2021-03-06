﻿namespace VerySmart_UI
{
    partial class SelectUsageDialog
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
            this.infoLabel = new System.Windows.Forms.Label();
            this.usagesListView = new System.Windows.Forms.ListView();
            this.textHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.typeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dontUseASynonymBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(12, 9);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(213, 13);
            this.infoLabel.TabIndex = 0;
            this.infoLabel.Text = "Multiple usages have been found for {term}.";
            // 
            // usagesListView
            // 
            this.usagesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.usagesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.textHeader,
            this.typeHeader});
            this.usagesListView.FullRowSelect = true;
            this.usagesListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.usagesListView.Location = new System.Drawing.Point(12, 38);
            this.usagesListView.Name = "usagesListView";
            this.usagesListView.Size = new System.Drawing.Size(315, 186);
            this.usagesListView.TabIndex = 2;
            this.usagesListView.UseCompatibleStateImageBehavior = false;
            this.usagesListView.View = System.Windows.Forms.View.Details;
            this.usagesListView.ItemActivate += new System.EventHandler(this.usagesListView_ItemActivate);
            this.usagesListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.usagesListView_MouseDoubleClick);
            // 
            // textHeader
            // 
            this.textHeader.Text = "Text";
            // 
            // typeHeader
            // 
            this.typeHeader.Text = "Type";
            // 
            // dontUseASynonymBtn
            // 
            this.dontUseASynonymBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dontUseASynonymBtn.Location = new System.Drawing.Point(12, 230);
            this.dontUseASynonymBtn.Name = "dontUseASynonymBtn";
            this.dontUseASynonymBtn.Size = new System.Drawing.Size(123, 23);
            this.dontUseASynonymBtn.TabIndex = 3;
            this.dontUseASynonymBtn.Text = "Don\'t Use A Synonym";
            this.dontUseASynonymBtn.UseVisualStyleBackColor = true;
            this.dontUseASynonymBtn.Click += new System.EventHandler(this.dontUseASynonymBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(317, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Press [Escape] if you don\'t want to select a synonym for this word.";
            // 
            // SelectUsageDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 265);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dontUseASynonymBtn);
            this.Controls.Add(this.usagesListView);
            this.Controls.Add(this.infoLabel);
            this.KeyPreview = true;
            this.Name = "SelectUsageDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SelectUsageDialog";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SelectUsageDialog_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.ListView usagesListView;
        private System.Windows.Forms.ColumnHeader textHeader;
        private System.Windows.Forms.ColumnHeader typeHeader;
        private System.Windows.Forms.Button dontUseASynonymBtn;
        private System.Windows.Forms.Label label1;
    }
}