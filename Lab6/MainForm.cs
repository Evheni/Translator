using GrammarRelations.Lexems;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6
{
    public partial class MainForm : Form
    {
        List<string[]> lexemeTable;
        List<string> idsTable = new List<string>();

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Show output lexemes table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lexemeTableBtn_Click(object sender, EventArgs e)
        {
            StartWorking();
            string[] columns = new string[]
            {
                "Номер", "Рядок", "Лексема", "Код", "Код ід/кон"
            };
            
            try
            {
                CheckLexemeTable();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
                MessageBox.Show("Помилка лексичного аналізатора. Перевірте код.");
            }
            var tableF = new TableForm(columns, lexemeTable);
                tableF.ShowDialog();
            
            FinishWorking();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            programTextBox.Text =
                @"var , a, b, c, i : real
begin
ReadLine(, a)
a = (a + 1.6E-37 + 52.4 * (5. - (4 + 2 / 3)))
if [(a) > 0] or b == c then goto start
do i = 1 to a
do i = 1 to a
b = b + i
b = b + 1
next
& start
WriteLine(, b)
next
end";
        }

        /// <summary>
        /// Show lexemes relation table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void showTableBtn_Click(object sender, EventArgs e)
        {
            //var tableF = new TableForm();
            StartWorking();
            var table = await Task.Run(() =>
            {
                var tableF = new TableForm();
                tableF.BuildRelationsTable();
                return tableF;              
            });
            table.Show();
            FinishWorking();
        }

        private void ShowMessage(string message)
        {
            resultTextBox.Text = message;
        }
        private void StartWorking()
        {
            progressStatus.Text = "Працюємо";
            progressBar.Visible = true;
        }
        private void FinishWorking()
        {            
            progressBar.Visible = false;
            progressStatus.Text = "Готово";
        }
        private void CheckLexemeTable()
        {
            if (lexemeTable == null)
            {
                var lexController = new LexemeController(
                programTextBox.Text.Split(
                    new string[] { Environment.NewLine },
                    StringSplitOptions.RemoveEmptyEntries));
                var table = lexController.LexicalAnalysis();
                lexemeTable = new List<string[]>();
                for (var i = 0; i < table.Count; ++i)
                {
                    lexemeTable.Add(new string[]
                    {
                    (i + 1).ToString(), // Lexeme number
                    table[i].StringNumber.ToString(), // String number
                    table[i].Name, // Lexeme name
                    table[i].ID.ToString(), // Lexeme ID
                    table[i].Index.ToString() // If lexeme is con or id or label, it's ID
                    });
                }
                foreach(var v in lexController.Ids)
                    idsTable.Add(v.Name);
            }
        }
        private void CleaneTables()
        {
            analysisResultTable.Rows.Clear();
        }

        private void verifyBtn_Click(object sender, EventArgs e)
        {
            StartWorking();
            CleaneTables();
            try
            {
                CheckLexemeTable();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
                MessageBox.Show("Помилка лексичного аналізатора. Перевірте код.");
            }
            var controller = new Analysis.AnalysisController(lexemeTable);
            try { 
                controller.Parse(this.idsTable.ToArray());
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
                MessageBox.Show("Помилка синтаксичного аналізатора. Перевірте код.");
            }            
            var result = controller.ResultTable;
            if (result != null)
            {
                foreach (var row in result)
                {
                    analysisResultTable.Rows.Add(row);
                }
            }
            FinishWorking();
        }

        private void programTextBox_TextChanged(object sender, EventArgs e)
        {
            this.lexemeTable = null;
        }
    }
}
