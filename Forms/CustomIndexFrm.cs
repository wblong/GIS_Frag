using AE_Environment.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AE_Environment.Model.CustomIndex;

namespace AE_Environment.Forms
{
    public partial class CustomIndexFrm : Form
    {
        PatchCustom customIndex;
        DataTable dt_Result = null;
        BaseData baseData = null;
        public CustomIndexFrm(BaseData _basedata)
        {
            InitializeComponent();
            baseData = _basedata;
            customIndex = new PatchCustom();
            customIndex.SetBaseData(baseData);
            
        }

        private void CustomIndexFrm_Load(object sender, EventArgs e)
        {
            dt_Result = new DataTable();
            for (int i = 0; i < baseData.ccValue.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = "'" + baseData.ccValue[i] + "'";
                lvi.SubItems.Add(MainForm.pData[baseData.ccValue[i]]);
                lv_UniqueValue.Items.Add(lvi);
            }
            ListViewItem items = new ListViewItem();
            items.Text = "'0000'";
            items.SubItems.Add("总面积/周长");
            lv_UniqueValue.Items.Insert(0, items);
            
        }

        private void bt_Add_Click(object sender, EventArgs e)
        {
            InsertString(" " + ((Button)sender).Text + " ", rtb_Math);
        }

        private void bt_Minus_Click(object sender, EventArgs e)
        {
            InsertString(" " + ((Button)sender).Text + " ", rtb_Math);
        }

        private void bt_Multiplicative_Click(object sender, EventArgs e)
        {
            InsertString(" " + ((Button)sender).Text + " ", rtb_Math);
        }

        private void bt_Division_Click(object sender, EventArgs e)
        {
            InsertString(" " + ((Button)sender).Text + " ", rtb_Math);
        }

        private void bt_LBracket_Click(object sender, EventArgs e)
        {
            InsertString(((Button)sender).Text, rtb_Math);
        }

        private void bt_RBracket_Click(object sender, EventArgs e)
        {
            InsertString(((Button)sender).Text, rtb_Math);
        }

        private void bt_LoadFormula_Click(object sender, EventArgs e)
        {
            LoadFormula(lv_Formula);
        }

        private void bt_AddToList_Click(object sender, EventArgs e)
        {
            AddToListView(rtb_Math, tb_IndexName, lv_Formula);
        }

        private void bt_Clear_Click(object sender, EventArgs e)
        {
            rtb_Math.Clear();
        }

        private void bt_Calculate_Click(object sender, EventArgs e)
        {
            customIndex.Initialize(tb_IndexName.Text, rtb_Math.Text);
            if (customIndex.CalculateIndex(progressBar1))
            {
                if (customIndex.BuildResultTable())
                {
                    dgv_Result.DataSource = customIndex.result_dt;
                    MessageBox.Show("计算完毕！");
                }
            }
            else
            {
                MessageBox.Show("请检查自定义指标名称和计算公式是否为空", "提示");
            }
        }

        private void bt_Save_Click(object sender, EventArgs e)
        {
            MainForm.dt_custom = customIndex.result_dt;
            MessageBox.Show("数据保存完毕");
            this.Close();
        }

        private void lv_UniqueValue_DoubleClick(object sender, EventArgs e)
        {
            if (rbt_Area.Checked)
            {
                InsertString("Area(" + lv_UniqueValue.SelectedItems[0].SubItems[0].Text + ")", rtb_Math);
            }
            else if (rbt_Perimeter.Checked)
            {
                InsertString("Perimeter(" + lv_UniqueValue.SelectedItems[0].SubItems[0].Text + ")", rtb_Math);
            }
        }

        private void 删除该指标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            while (lv_Formula.SelectedItems.Count != 0)
            {
                lv_Formula.Items.Remove(lv_Formula.SelectedItems[0]);
            }
        }

        private void 删除行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = dgv_Result.CurrentCell.RowIndex;
            if (index > 0)
            {
                dgv_Result.Rows.RemoveAt(index);
            }
        }

        private void 删除列ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = dgv_Result.CurrentCell.ColumnIndex;
            if (index > 0)
            {
                dgv_Result.Columns.RemoveAt(index);
            }
        }

        private void lv_Formula_DoubleClick(object sender, EventArgs e)
        {
            rtb_Math.Text = "";
            tb_IndexName.Text = "";

            tb_IndexName.Text = lv_Formula.SelectedItems[0].SubItems[0].Text;
            rtb_Math.Text = lv_Formula.SelectedItems[0].SubItems[1].Text;
        }

        private void bt_SaveFormula_Click(object sender, EventArgs e)
        {
            SaveFormula(lv_Formula);
        }

        private void CustomIndexFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dgv_Result.Rows.Count <= 0 )
            {
                e.Cancel = false;
            }
            else
            {
                DialogResult dr = MessageBox.Show("是否保存计算数据", "提示", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    MainForm.dt_custom = dt_Result;
                    MessageBox.Show("数据保存完毕");
                }
                else
                {
                    e.Cancel = false;
                }
            }
            e.Cancel = false;
        }

        #region 自定义函数

        #region 插入字符
        private void InsertString(string strInset, RichTextBox textBox)
        {
            string sql = textBox.Text;
            int start = textBox.SelectionStart;
            int length = textBox.SelectionLength;
            string str1 = "";
            string str2 = "";
            if (start == sql.Length)
            {
                textBox.Text = textBox.Text + strInset;
            }
            else
            {
                str1 = sql.Substring(0, start);
                str2 = sql.Substring(start + length);
                textBox.Text = str1 + strInset + str2;
            }
            textBox.Focus();
            textBox.SelectionStart = start + strInset.Length;
            textBox.SelectionLength = 0;
        }
        #endregion

        #region 添加公式模板到ListView
        private void AddToListView(RichTextBox formula, TextBox indexName, ListView listView)
        {
            if (indexName.Text == "" || formula.Text == "")
            {
                MessageBox.Show("请检查指标名称或公式是否为空！", "提示");
                return;
            }

            for (int i = 0; i < listView.Items.Count; i++)
            {
                if (indexName.Text == listView.Items[i].Text)
                {
                    MessageBox.Show("该名称已经存在！", "提示");
                    return;
                }
            }

            ListViewItem item = new ListViewItem();
            item.Text = indexName.Text;
            item.SubItems.Add(formula.Text);
            listView.Items.Add(item);
        }
        #endregion

        #region 加载公式
        private void LoadFormula(ListView listview)
        {
            listview.Items.Clear();
            string filePath = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "加载公式模板";
            ofd.Filter = "公式模板(*.formula)|*.formula";
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                filePath = ofd.FileName;
                StreamReader fileRead = new StreamReader(filePath, System.Text.Encoding.UTF8);
                while (!fileRead.EndOfStream)
                {
                    ListViewItem item = new ListViewItem();
                    string str = fileRead.ReadLine();
                    item.Text = str.Substring(0, str.IndexOf(","));
                    item.SubItems.Add(str.Substring(str.IndexOf(",") + 1));
                    listview.Items.Add(item);
                }
            }
        }
        #endregion

        #region 保存公式
        private void SaveFormula(ListView listview)
        {
            string filePath = string.Empty;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "保存公式模板";
            sfd.Filter = "公式模板(*.formula)|*.formula";
            DialogResult drt = sfd.ShowDialog();
            if (drt == DialogResult.OK)
            {
                filePath = sfd.FileName;
            }
            if (filePath != null)
            {
                StreamWriter FileWriter = new StreamWriter(filePath);
                for (int i = 0; i < listview.Items.Count; i++)
                {
                    string str = listview.Items[i].SubItems[0].Text + "," + listview.Items[i].SubItems[1].Text;
                    FileWriter.Write(str);
                    FileWriter.WriteLine();
                }

                FileWriter.Flush();
                FileWriter.Close();
                MessageBox.Show("文件已经保存！");
            }
            else
            {
                MessageBox.Show("请输入文件名称");
                return;
            }
        }
        #endregion

        #region 方法实现把dgv里的数据完整的复制到一张DataTable
        public static DataTable GetDgvToTable(DataGridView dgv)
        {
            DataTable dt = new DataTable();
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = dgv.Rows[count].Cells[countsub].Value.ToString();
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion

        #region 实现dataTable的追加 
        private  DataTable DataTableAppend(DataTable toDataTab, DataTable fromDataTab)
        {
            if (toDataTab.Rows.Count == 0 && toDataTab != null)
            {
                toDataTab = fromDataTab;
                return fromDataTab;
            }
            else
            {
                for (int i = 0; i < toDataTab.Rows.Count;i++ )
                {
                    for (int j = 1; j < fromDataTab.Columns.Count;j++ )
                    {
                        //添加列标题;
                        DataColumn dataColumn = new DataColumn();
                        dataColumn.ColumnName = fromDataTab.Columns[j].ColumnName;
                        dataColumn.DataType = System.Type.GetType("System.String");
                        toDataTab.Columns.Add(dataColumn);

                        //添加数据;
                        DataRow dataRowTo = toDataTab.Rows[i];
                        int count = toDataTab.Columns.Count;
                        dataRowTo[count - 1] = fromDataTab.Rows[i][j];
                    }
                }
            }
            return dt_Result;
        }

        #endregion

        

        #endregion


    }
}
