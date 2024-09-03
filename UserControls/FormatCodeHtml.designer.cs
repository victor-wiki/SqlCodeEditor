namespace SqlCodeEditor.UserControls
{
  partial class FormatCodeHtml
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
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
            btnClose = new Button();
            btnCopy = new Button();
            gbLanguage = new GroupBox();
            flowLayoutPanel2 = new FlowLayoutPanel();
            rbtsql = new RadioButton();
            gbSettings = new GroupBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            cbLineNumbers = new CheckBox();
            cbEmbedCss = new CheckBox();
            cbAlternate = new CheckBox();
            rbMySql = new RadioButton();
            rbPlSql = new RadioButton();
            rbPostgreSql = new RadioButton();
            rbSqliteSql = new RadioButton();
            gbLanguage.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            gbSettings.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.Location = new Point(304, 196);
            btnClose.Margin = new Padding(4);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(106, 30);
            btnClose.TabIndex = 0;
            btnClose.Text = "Copy && Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnCopy
            // 
            btnCopy.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCopy.Location = new Point(210, 196);
            btnCopy.Margin = new Padding(4);
            btnCopy.Name = "btnCopy";
            btnCopy.Size = new Size(88, 30);
            btnCopy.TabIndex = 1;
            btnCopy.Text = "Copy";
            btnCopy.UseVisualStyleBackColor = true;
            btnCopy.Click += btnCopy_Click;
            // 
            // gbLanguage
            // 
            gbLanguage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbLanguage.Controls.Add(flowLayoutPanel2);
            gbLanguage.Location = new Point(14, 16);
            gbLanguage.Margin = new Padding(4);
            gbLanguage.Name = "gbLanguage";
            gbLanguage.Padding = new Padding(4);
            gbLanguage.Size = new Size(397, 98);
            gbLanguage.TabIndex = 2;
            gbLanguage.TabStop = false;
            gbLanguage.Text = "Language";
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(rbtsql);
            flowLayoutPanel2.Controls.Add(rbMySql);
            flowLayoutPanel2.Controls.Add(rbPlSql);
            flowLayoutPanel2.Controls.Add(rbPostgreSql);
            flowLayoutPanel2.Controls.Add(rbSqliteSql);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.Location = new Point(4, 20);
            flowLayoutPanel2.Margin = new Padding(4);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Padding = new Padding(6, 7, 6, 7);
            flowLayoutPanel2.Size = new Size(389, 74);
            flowLayoutPanel2.TabIndex = 12;
            // 
            // rbtsql
            // 
            rbtsql.AutoSize = true;
            rbtsql.Location = new Point(10, 11);
            rbtsql.Margin = new Padding(4);
            rbtsql.Name = "rbtsql";
            rbtsql.Size = new Size(51, 21);
            rbtsql.TabIndex = 3;
            rbtsql.Text = "TSql";
            rbtsql.UseVisualStyleBackColor = true;
            // 
            // gbSettings
            // 
            gbSettings.Controls.Add(flowLayoutPanel1);
            gbSettings.Location = new Point(14, 124);
            gbSettings.Margin = new Padding(4);
            gbSettings.Name = "gbSettings";
            gbSettings.Padding = new Padding(4);
            gbSettings.Size = new Size(393, 67);
            gbSettings.TabIndex = 3;
            gbSettings.TabStop = false;
            gbSettings.Text = "Settings";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(cbLineNumbers);
            flowLayoutPanel1.Controls.Add(cbEmbedCss);
            flowLayoutPanel1.Controls.Add(cbAlternate);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(4, 20);
            flowLayoutPanel1.Margin = new Padding(4);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(6, 7, 6, 7);
            flowLayoutPanel1.Size = new Size(385, 43);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // cbLineNumbers
            // 
            cbLineNumbers.AutoSize = true;
            cbLineNumbers.Location = new Point(10, 11);
            cbLineNumbers.Margin = new Padding(4);
            cbLineNumbers.Name = "cbLineNumbers";
            cbLineNumbers.Size = new Size(108, 21);
            cbLineNumbers.TabIndex = 0;
            cbLineNumbers.Text = "Line Numbers";
            cbLineNumbers.UseVisualStyleBackColor = true;
            // 
            // cbEmbedCss
            // 
            cbEmbedCss.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            cbEmbedCss.AutoSize = true;
            cbEmbedCss.Location = new Point(126, 11);
            cbEmbedCss.Margin = new Padding(4);
            cbEmbedCss.Name = "cbEmbedCss";
            cbEmbedCss.Size = new Size(92, 21);
            cbEmbedCss.TabIndex = 1;
            cbEmbedCss.Text = "Embed Css";
            cbEmbedCss.UseVisualStyleBackColor = true;
            // 
            // cbAlternate
            // 
            cbAlternate.AutoSize = true;
            cbAlternate.Location = new Point(226, 11);
            cbAlternate.Margin = new Padding(4);
            cbAlternate.Name = "cbAlternate";
            cbAlternate.Size = new Size(148, 21);
            cbAlternate.TabIndex = 2;
            cbAlternate.Text = "Alternate Line Colors";
            cbAlternate.UseVisualStyleBackColor = true;
            // 
            // rbMySql
            // 
            rbMySql.AutoSize = true;
            rbMySql.Location = new Point(69, 11);
            rbMySql.Margin = new Padding(4);
            rbMySql.Name = "rbMySql";
            rbMySql.Size = new Size(62, 21);
            rbMySql.TabIndex = 4;
            rbMySql.Text = "MySql";
            rbMySql.UseVisualStyleBackColor = true;
            // 
            // rbPlSql
            // 
            rbPlSql.AutoSize = true;
            rbPlSql.Location = new Point(139, 11);
            rbPlSql.Margin = new Padding(4);
            rbPlSql.Name = "rbPlSql";
            rbPlSql.Size = new Size(54, 21);
            rbPlSql.TabIndex = 5;
            rbPlSql.Text = "PlSql";
            rbPlSql.UseVisualStyleBackColor = true;
            // 
            // rbPostgreSql
            // 
            rbPostgreSql.AutoSize = true;
            rbPostgreSql.Location = new Point(201, 11);
            rbPostgreSql.Margin = new Padding(4);
            rbPostgreSql.Name = "rbPostgreSql";
            rbPostgreSql.Size = new Size(89, 21);
            rbPostgreSql.TabIndex = 6;
            rbPostgreSql.Text = "PostgreSql";
            rbPostgreSql.UseVisualStyleBackColor = true;
            // 
            // rbSqliteSql
            // 
            rbSqliteSql.AutoSize = true;
            rbSqliteSql.Location = new Point(298, 11);
            rbSqliteSql.Margin = new Padding(4);
            rbSqliteSql.Name = "rbSqliteSql";
            rbSqliteSql.Size = new Size(76, 21);
            rbSqliteSql.TabIndex = 7;
            rbSqliteSql.Text = "SqliteSql";
            rbSqliteSql.UseVisualStyleBackColor = true;
            // 
            // FormatCodeHtml
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(425, 242);
            Controls.Add(gbSettings);
            Controls.Add(gbLanguage);
            Controls.Add(btnCopy);
            Controls.Add(btnClose);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormatCodeHtml";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Copy Code as Html";
            gbLanguage.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            gbSettings.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Button btnCopy;
    private System.Windows.Forms.GroupBox gbLanguage;
    private System.Windows.Forms.GroupBox gbSettings;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    private System.Windows.Forms.RadioButton rbtsql;
    private System.Windows.Forms.CheckBox cbLineNumbers;
    private System.Windows.Forms.CheckBox cbEmbedCss;
    private System.Windows.Forms.CheckBox cbAlternate;
        private RadioButton rbMySql;
        private RadioButton rbPlSql;
        private RadioButton rbPostgreSql;
        private RadioButton rbSqliteSql;
    }
}