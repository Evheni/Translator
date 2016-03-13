namespace Lab6
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.showTableBtn = new System.Windows.Forms.Button();
            this.programTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.verifyBtn = new System.Windows.Forms.Button();
            this.resultTextBox = new System.Windows.Forms.TextBox();
            this.analysisResultTable = new System.Windows.Forms.DataGridView();
            this.stepColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stackColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ratioColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inputColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.polizColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lexemeTableBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.button1 = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.progressStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.analysisResultTable)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // showTableBtn
            // 
            this.showTableBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showTableBtn.Location = new System.Drawing.Point(1105, 27);
            this.showTableBtn.Name = "showTableBtn";
            this.showTableBtn.Size = new System.Drawing.Size(129, 39);
            this.showTableBtn.TabIndex = 8;
            this.showTableBtn.Text = "Показати таблицю відношень";
            this.showTableBtn.UseVisualStyleBackColor = true;
            this.showTableBtn.Click += new System.EventHandler(this.showTableBtn_Click);
            // 
            // programTextBox
            // 
            this.programTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.programTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.programTextBox.Location = new System.Drawing.Point(3, 16);
            this.programTextBox.MaxLength = 900000;
            this.programTextBox.Multiline = true;
            this.programTextBox.Name = "programTextBox";
            this.programTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.programTextBox.Size = new System.Drawing.Size(259, 341);
            this.programTextBox.TabIndex = 1;
            this.programTextBox.TextChanged += new System.EventHandler(this.programTextBox_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.programTextBox);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 360);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Текст програми";
            // 
            // verifyBtn
            // 
            this.verifyBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.verifyBtn.Location = new System.Drawing.Point(1105, 72);
            this.verifyBtn.Name = "verifyBtn";
            this.verifyBtn.Size = new System.Drawing.Size(129, 23);
            this.verifyBtn.TabIndex = 10;
            this.verifyBtn.Text = "Перевірити код";
            this.verifyBtn.UseVisualStyleBackColor = true;
            this.verifyBtn.Click += new System.EventHandler(this.verifyBtn_Click);
            // 
            // resultTextBox
            // 
            this.resultTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resultTextBox.Location = new System.Drawing.Point(12, 399);
            this.resultTextBox.Multiline = true;
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.ReadOnly = true;
            this.resultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resultTextBox.Size = new System.Drawing.Size(1222, 120);
            this.resultTextBox.TabIndex = 9;
            // 
            // analysisResultTable
            // 
            this.analysisResultTable.AllowUserToAddRows = false;
            this.analysisResultTable.AllowUserToDeleteRows = false;
            this.analysisResultTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.analysisResultTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.analysisResultTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.analysisResultTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stepColumn,
            this.stackColumn,
            this.ratioColumn,
            this.inputColumn,
            this.polizColumn,
            this.valueColumn});
            this.analysisResultTable.Location = new System.Drawing.Point(274, 3);
            this.analysisResultTable.Name = "analysisResultTable";
            this.analysisResultTable.ReadOnly = true;
            this.analysisResultTable.Size = new System.Drawing.Size(810, 360);
            this.analysisResultTable.TabIndex = 5;
            // 
            // stepColumn
            // 
            this.stepColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.stepColumn.FillWeight = 30F;
            this.stepColumn.HeaderText = "Крок";
            this.stepColumn.Name = "stepColumn";
            this.stepColumn.ReadOnly = true;
            // 
            // stackColumn
            // 
            this.stackColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.stackColumn.HeaderText = "Стек";
            this.stackColumn.Name = "stackColumn";
            this.stackColumn.ReadOnly = true;
            // 
            // ratioColumn
            // 
            this.ratioColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ratioColumn.FillWeight = 30F;
            this.ratioColumn.HeaderText = "Відношення";
            this.ratioColumn.Name = "ratioColumn";
            this.ratioColumn.ReadOnly = true;
            // 
            // inputColumn
            // 
            this.inputColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.inputColumn.HeaderText = "Вхідний ланцюжок";
            this.inputColumn.Name = "inputColumn";
            this.inputColumn.ReadOnly = true;
            // 
            // polizColumn
            // 
            this.polizColumn.HeaderText = "ПОЛІЗ";
            this.polizColumn.Name = "polizColumn";
            this.polizColumn.ReadOnly = true;
            // 
            // valueColumn
            // 
            this.valueColumn.HeaderText = "Значення";
            this.valueColumn.Name = "valueColumn";
            this.valueColumn.ReadOnly = true;
            // 
            // lexemeTableBtn
            // 
            this.lexemeTableBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lexemeTableBtn.Location = new System.Drawing.Point(1105, 351);
            this.lexemeTableBtn.Name = "lexemeTableBtn";
            this.lexemeTableBtn.Size = new System.Drawing.Size(129, 36);
            this.lexemeTableBtn.TabIndex = 12;
            this.lexemeTableBtn.Text = "Показати таблицю лексем";
            this.lexemeTableBtn.UseVisualStyleBackColor = true;
            this.lexemeTableBtn.Click += new System.EventHandler(this.lexemeTableBtn_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.analysisResultTable, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1087, 366);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressStatus,
            this.progressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 522);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(1246, 22);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1246, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1105, 101);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Значення та змінні";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.Visible = false;
            // 
            // progressStatus
            // 
            this.progressStatus.Name = "progressStatus";
            this.progressStatus.Size = new System.Drawing.Size(45, 17);
            this.progressStatus.Text = "Готово";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1246, 544);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.showTableBtn);
            this.Controls.Add(this.verifyBtn);
            this.Controls.Add(this.resultTextBox);
            this.Controls.Add(this.lexemeTableBtn);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.analysisResultTable)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button showTableBtn;
        private System.Windows.Forms.TextBox programTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button verifyBtn;
        private System.Windows.Forms.TextBox resultTextBox;
        private System.Windows.Forms.DataGridView analysisResultTable;
        private System.Windows.Forms.Button lexemeTableBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn stepColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stackColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ratioColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn inputColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn polizColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueColumn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripStatusLabel progressStatus;
    }
}

