namespace MiniWordPad
{
    partial class Form1
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
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новыйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.инструментsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.шрифтToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выравниваниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поЛевомуКраюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поПравомуКраюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поСерединеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.скопироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вырезатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.поискToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заменаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.cursorPoisitionLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.charCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.buttonPrev = new System.Windows.Forms.Button();
            this.panelReplace = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonAllReplace = new System.Windows.Forms.Button();
            this.buttonOnceReplace = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonNextReplace = new System.Windows.Forms.Button();
            this.textBoxOn = new System.Windows.Forms.TextBox();
            this.buttonPrevReplace = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxWhat = new System.Windows.Forms.TextBox();
            this.contentRichTextBox = new System.Windows.Forms.RichTextBox();
            this.mainMenuStrip.SuspendLayout();
            this.mainStatusStrip.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelReplace.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.файлToolStripMenuItem,
            this.инструментsToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.mainMenuStrip.Size = new System.Drawing.Size(1045, 28);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(48, 24);
            this.toolStripMenuItem3.Text = "<—";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(48, 24);
            this.toolStripMenuItem4.Text = "—>";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новыйToolStripMenuItem,
            this.открытьToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.сохранитьКакToolStripMenuItem,
            this.выходToolStripMenuItem,
            this.выходToolStripMenuItem1});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // новыйToolStripMenuItem
            // 
            this.новыйToolStripMenuItem.Name = "новыйToolStripMenuItem";
            this.новыйToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.новыйToolStripMenuItem.Text = "Новый";
            this.новыйToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // сохранитьКакToolStripMenuItem
            // 
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.сохранитьКакToolStripMenuItem.Text = "Сохранить как";
            this.сохранитьКакToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(189, 6);
            // 
            // выходToolStripMenuItem1
            // 
            this.выходToolStripMenuItem1.Name = "выходToolStripMenuItem1";
            this.выходToolStripMenuItem1.Size = new System.Drawing.Size(192, 26);
            this.выходToolStripMenuItem1.Text = "Выход";
            this.выходToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // инструментsToolStripMenuItem
            // 
            this.инструментsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.шрифтToolStripMenuItem,
            this.выравниваниеToolStripMenuItem,
            this.toolStripMenuItem1,
            this.скопироватьToolStripMenuItem,
            this.вырезатьToolStripMenuItem,
            this.вставитьToolStripMenuItem,
            this.toolStripMenuItem2,
            this.поискToolStripMenuItem,
            this.заменаToolStripMenuItem});
            this.инструментsToolStripMenuItem.Name = "инструментsToolStripMenuItem";
            this.инструментsToolStripMenuItem.Size = new System.Drawing.Size(112, 24);
            this.инструментsToolStripMenuItem.Text = "Инструментs";
            // 
            // шрифтToolStripMenuItem
            // 
            this.шрифтToolStripMenuItem.Name = "шрифтToolStripMenuItem";
            this.шрифтToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.шрифтToolStripMenuItem.Text = "Шрифт";
            this.шрифтToolStripMenuItem.Click += new System.EventHandler(this.fontDefaultToolStripButton_Click);
            // 
            // выравниваниеToolStripMenuItem
            // 
            this.выравниваниеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.поЛевомуКраюToolStripMenuItem,
            this.поПравомуКраюToolStripMenuItem,
            this.поСерединеToolStripMenuItem});
            this.выравниваниеToolStripMenuItem.Name = "выравниваниеToolStripMenuItem";
            this.выравниваниеToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.выравниваниеToolStripMenuItem.Text = "Выравнивание";
            // 
            // поЛевомуКраюToolStripMenuItem
            // 
            this.поЛевомуКраюToolStripMenuItem.Name = "поЛевомуКраюToolStripMenuItem";
            this.поЛевомуКраюToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            this.поЛевомуКраюToolStripMenuItem.Text = "По левому краю";
            this.поЛевомуКраюToolStripMenuItem.Click += new System.EventHandler(this.alignLeftToolStripButton_Click);
            // 
            // поПравомуКраюToolStripMenuItem
            // 
            this.поПравомуКраюToolStripMenuItem.Name = "поПравомуКраюToolStripMenuItem";
            this.поПравомуКраюToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            this.поПравомуКраюToolStripMenuItem.Text = "По правому краю";
            this.поПравомуКраюToolStripMenuItem.Click += new System.EventHandler(this.alignRightToolStripButton_Click);
            // 
            // поСерединеToolStripMenuItem
            // 
            this.поСерединеToolStripMenuItem.Name = "поСерединеToolStripMenuItem";
            this.поСерединеToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            this.поСерединеToolStripMenuItem.Text = "По середине";
            this.поСерединеToolStripMenuItem.Click += new System.EventHandler(this.alignCenterToolStripButton_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(194, 6);
            // 
            // скопироватьToolStripMenuItem
            // 
            this.скопироватьToolStripMenuItem.Name = "скопироватьToolStripMenuItem";
            this.скопироватьToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.скопироватьToolStripMenuItem.Text = "Скопировать";
            this.скопироватьToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripButton_Click);
            // 
            // вырезатьToolStripMenuItem
            // 
            this.вырезатьToolStripMenuItem.Name = "вырезатьToolStripMenuItem";
            this.вырезатьToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.вырезатьToolStripMenuItem.Text = "Вырезать";
            this.вырезатьToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripButton_Click);
            // 
            // вставитьToolStripMenuItem
            // 
            this.вставитьToolStripMenuItem.Name = "вставитьToolStripMenuItem";
            this.вставитьToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.вставитьToolStripMenuItem.Text = "Вставить";
            this.вставитьToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripButton_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(194, 6);
            // 
            // поискToolStripMenuItem
            // 
            this.поискToolStripMenuItem.Name = "поискToolStripMenuItem";
            this.поискToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.поискToolStripMenuItem.Text = "Поиск";
            this.поискToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripButton_Click);
            // 
            // заменаToolStripMenuItem
            // 
            this.заменаToolStripMenuItem.Name = "заменаToolStripMenuItem";
            this.заменаToolStripMenuItem.Size = new System.Drawing.Size(197, 26);
            this.заменаToolStripMenuItem.Text = "Замена";
            this.заменаToolStripMenuItem.Click += new System.EventHandler(this.ReplaceToolStripButton_Click);
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cursorPoisitionLabel,
            this.charCountLabel,
            this.statusLabel});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 664);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.mainStatusStrip.Size = new System.Drawing.Size(1045, 26);
            this.mainStatusStrip.TabIndex = 3;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // cursorPoisitionLabel
            // 
            this.cursorPoisitionLabel.Name = "cursorPoisitionLabel";
            this.cursorPoisitionLabel.Size = new System.Drawing.Size(121, 20);
            this.cursorPoisitionLabel.Text = "Строка/Столбец";
            // 
            // charCountLabel
            // 
            this.charCountLabel.Name = "charCountLabel";
            this.charCountLabel.Size = new System.Drawing.Size(74, 20);
            this.charCountLabel.Text = "Символы";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(74, 20);
            this.statusLabel.Text = "Действие";
            // 
            // panelSearch
            // 
            this.panelSearch.Controls.Add(this.groupBox1);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSearch.Location = new System.Drawing.Point(845, 28);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(200, 636);
            this.panelSearch.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonNext);
            this.groupBox1.Controls.Add(this.searchBox);
            this.groupBox1.Controls.Add(this.buttonPrev);
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(197, 183);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Поиск вхождений";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Введите значение";
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(96, 83);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(83, 27);
            this.buttonNext.TabIndex = 2;
            this.buttonNext.Text = "->\r\n\r\n";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(9, 50);
            this.searchBox.Name = "searchBox";
            this.searchBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.searchBox.Size = new System.Drawing.Size(170, 22);
            this.searchBox.TabIndex = 0;
            // 
            // buttonPrev
            // 
            this.buttonPrev.Location = new System.Drawing.Point(9, 83);
            this.buttonPrev.Name = "buttonPrev";
            this.buttonPrev.Size = new System.Drawing.Size(83, 27);
            this.buttonPrev.TabIndex = 1;
            this.buttonPrev.Text = "<-\r\n";
            this.buttonPrev.UseVisualStyleBackColor = true;
            this.buttonPrev.Click += new System.EventHandler(this.buttonPrev_Click);
            // 
            // panelReplace
            // 
            this.panelReplace.Controls.Add(this.groupBox2);
            this.panelReplace.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelReplace.Location = new System.Drawing.Point(645, 28);
            this.panelReplace.Name = "panelReplace";
            this.panelReplace.Size = new System.Drawing.Size(200, 636);
            this.panelReplace.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonAllReplace);
            this.groupBox2.Controls.Add(this.buttonOnceReplace);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.buttonNextReplace);
            this.groupBox2.Controls.Add(this.textBoxOn);
            this.groupBox2.Controls.Add(this.buttonPrevReplace);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBoxWhat);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(197, 285);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Замена вхождений";
            // 
            // buttonAllReplace
            // 
            this.buttonAllReplace.Location = new System.Drawing.Point(9, 195);
            this.buttonAllReplace.Name = "buttonAllReplace";
            this.buttonAllReplace.Size = new System.Drawing.Size(170, 27);
            this.buttonAllReplace.TabIndex = 7;
            this.buttonAllReplace.Text = "Заменить всё";
            this.buttonAllReplace.UseVisualStyleBackColor = true;
            this.buttonAllReplace.Click += new System.EventHandler(this.buttonReplaceAll_Click);
            // 
            // buttonOnceReplace
            // 
            this.buttonOnceReplace.Location = new System.Drawing.Point(9, 165);
            this.buttonOnceReplace.Margin = new System.Windows.Forms.Padding(0);
            this.buttonOnceReplace.Name = "buttonOnceReplace";
            this.buttonOnceReplace.Size = new System.Drawing.Size(170, 27);
            this.buttonOnceReplace.TabIndex = 6;
            this.buttonOnceReplace.Text = "Заменить вхождение";
            this.buttonOnceReplace.UseVisualStyleBackColor = true;
            this.buttonOnceReplace.Click += new System.EventHandler(this.buttonReplace_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "На";
            // 
            // buttonNextReplace
            // 
            this.buttonNextReplace.Location = new System.Drawing.Point(96, 135);
            this.buttonNextReplace.Name = "buttonNextReplace";
            this.buttonNextReplace.Size = new System.Drawing.Size(83, 27);
            this.buttonNextReplace.TabIndex = 2;
            this.buttonNextReplace.Text = "->\r\n\r\n";
            this.buttonNextReplace.UseVisualStyleBackColor = true;
            this.buttonNextReplace.Click += new System.EventHandler(this.buttonNextReplace_Click);
            // 
            // textBoxOn
            // 
            this.textBoxOn.Location = new System.Drawing.Point(9, 102);
            this.textBoxOn.Name = "textBoxOn";
            this.textBoxOn.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxOn.Size = new System.Drawing.Size(170, 22);
            this.textBoxOn.TabIndex = 4;
            // 
            // buttonPrevReplace
            // 
            this.buttonPrevReplace.Location = new System.Drawing.Point(9, 135);
            this.buttonPrevReplace.Name = "buttonPrevReplace";
            this.buttonPrevReplace.Size = new System.Drawing.Size(83, 27);
            this.buttonPrevReplace.TabIndex = 1;
            this.buttonPrevReplace.Text = "<-\r\n";
            this.buttonPrevReplace.UseVisualStyleBackColor = true;
            this.buttonPrevReplace.Click += new System.EventHandler(this.buttonPrevReplace_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Заменить";
            // 
            // textBoxWhat
            // 
            this.textBoxWhat.Location = new System.Drawing.Point(9, 50);
            this.textBoxWhat.Name = "textBoxWhat";
            this.textBoxWhat.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxWhat.Size = new System.Drawing.Size(170, 22);
            this.textBoxWhat.TabIndex = 0;
            // 
            // contentRichTextBox
            // 
            this.contentRichTextBox.AcceptsTab = true;
            this.contentRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentRichTextBox.Location = new System.Drawing.Point(0, 28);
            this.contentRichTextBox.Name = "contentRichTextBox";
            this.contentRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.contentRichTextBox.Size = new System.Drawing.Size(645, 636);
            this.contentRichTextBox.TabIndex = 8;
            this.contentRichTextBox.Text = "";
            this.contentRichTextBox.SelectionChanged += new System.EventHandler(this.contentRichTextBox_SelectionChanged);
            this.contentRichTextBox.TextChanged += new System.EventHandler(this.contentRichTextBox_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 690);
            this.Controls.Add(this.contentRichTextBox);
            this.Controls.Add(this.panelReplace);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.panelSearch.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelReplace.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новыйToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem1;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPrev;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panelReplace;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonNextReplace;
        private System.Windows.Forms.TextBox textBoxWhat;
        private System.Windows.Forms.Button buttonPrevReplace;
        private System.Windows.Forms.RichTextBox contentRichTextBox;
        private System.Windows.Forms.TextBox textBoxOn;
        private System.Windows.Forms.Button buttonAllReplace;
        private System.Windows.Forms.Button buttonOnceReplace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem инструментsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem шрифтToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выравниваниеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поЛевомуКраюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поПравомуКраюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поСерединеToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem скопироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вставитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem поискToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заменаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripStatusLabel cursorPoisitionLabel;
        private System.Windows.Forms.ToolStripStatusLabel charCountLabel;
        private System.Windows.Forms.ToolStripMenuItem вырезатьToolStripMenuItem;
    }
}

