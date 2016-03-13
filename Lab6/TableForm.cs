using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6
{
    public partial class TableForm : Form
    {
        public TableForm()
        {
            InitializeComponent();            
        }
        public TableForm(string[] columns, IEnumerable<string[]> data)
        {
            InitializeComponent();

            for(var i = 0; i < columns.Length; ++i)
            {
                DataGridViewTextBoxColumn newColumn = new DataGridViewTextBoxColumn();
                newColumn.Name = "Column" + i;
                newColumn.HeaderText = columns[i];
                newColumn.ReadOnly = true;

                dataTable.Columns.Add(newColumn);
            }

            foreach(var row in data)
            {
                dataTable.Rows.Add(row);
            }
        }

        internal void BuildRelationsTable()
        {
            //var time = System.Diagnostics.Stopwatch.StartNew();            
            var rel = GrammarRelations.Relations.GetInstance();
            //System.Threading.Thread.Sleep(3500);
            int head = 0;
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Font = new Font("Calibri", 16);
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (var header in rel.Headers)
            {
                dataTable.Columns.Add("column" + head, header);
                dataTable.Columns[head].DefaultCellStyle = style;
                head++;
            }
            dataTable.RowCount = rel.Headers.Count;
            head = 0;
            foreach (var header in rel.Headers)
            {
                dataTable.Rows[head++].HeaderCell.Value = header;
            }
            for (int i = 0; i < rel.Table.GetLength(0); i++)
            {
                for (int j = 0; j < rel.Table.GetLength(1); j++)
                {
                    dataTable[j, i].Value = rel.Table[i, j];
                }
            }
            //time.Stop();
        }
    }
}
