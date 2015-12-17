using AE_Environment.Model;
using AE_Environment.Model.EcosystemIndex;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace AE_Environment.Forms
{
    public partial class EcoSystemIndexFrm : Form
    {
        PatchEcosystem ecoIndex;
        List<List<string>> indexName;
        private DataTable dt_Result = new DataTable();

        public EcoSystemIndexFrm(BaseData baseData)
        {
            InitializeComponent();
            ecoIndex = new PatchEcosystem();
            ecoIndex.SetBaseData(baseData);
        }

        private void bt_Calculator_Click(object sender, EventArgs e)
        {
            indexName = new List<List<string>>();
            List<string> comIndexName = new List<string>();
            List<string> speIndexName = new List<string>();
            List<string> natIndexName = new List<string>();
            List<string> YiJiLeiName = new List<string>();

            if (tabControl_EcoSystemIndex.SelectedTab == tbPg_ComIndex)
            {
                comIndexName = GetCheckedItem_COM(tbPg_ComIndex);
            }
            if (tabControl_EcoSystemIndex.SelectedTab == tbPg_EcoFctIndex)
            {
                speIndexName = GetCheckedItem_SPE(tbPg_EcoFctIndex);
            }
            if (tabControl_EcoSystemIndex.SelectedTab == tbPg_NatProIndex)
            {
                natIndexName = GetCheckedItem_SPE(tbPg_NatProIndex);
            }
            if (tabControl_EcoSystemIndex.SelectedTab == tbPg_Yiji_tongji)
            {
                for (int i = 0; i < tbPg_Yiji_tongji.Controls.Count; i++)
                {
                    // 识别GroupBox控件;
                    Type type1 = tbPg_Yiji_tongji.Controls[i].GetType();
                    // 识别CheckBox控件;
                    CheckBox checkbox = new CheckBox();
                    if (type1 == checkbox.GetType())
                    {
                        checkbox = (CheckBox)tbPg_Yiji_tongji.Controls[i];
                        if (checkbox.Checked)
                        {
                            YiJiLeiName.Add(checkbox.Text.ToString());
                        }
                    }
                }
            }
            indexName.Add(comIndexName);
            indexName.Add(speIndexName);
            indexName.Add(natIndexName);
            indexName.Add(YiJiLeiName);
            ecoIndex.Initialize(indexName);

            if (comIndexName.Count <= 0 && speIndexName.Count <= 0 && natIndexName.Count <= 0 && YiJiLeiName.Count <= 0)
            {
                MessageBox.Show("请选择所要计算的指标", "提示");
                return;
            }

            if (ecoIndex.CalculateIndex(progressBar))
            {
                if (ecoIndex.BuildResultTable())
                {
                    dt_Result = ecoIndex.result_dt;
                    dgv_Result.DataSource = dt_Result;
                    MessageBox.Show("计算完毕！");
                }
            }
        }

        private void loadPopulation_btn_Click(object sender, EventArgs e)
        {
            cbox_Arable.Enabled = true;
            cbox_forest.Enabled = true;
            cbox_Leisure.Enabled = true;

            string filePath = "";
            openFileDialog1.Filter = "*.xls|*.XLS";
            DialogResult result = openFileDialog1.ShowDialog();
            //如果用户选择"确定"
            if (result == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
            }
            if (filePath == "")
            {
                MessageBox.Show("请选择正确的路径");
                return ;
            }

            MainForm.dt_Population = ToDataTable(filePath).Tables[0];
        }

        private void bt_Save_Click(object sender, EventArgs e)
        {
            MainForm.dt_EcosystemIndex = dt_Result;
            MessageBox.Show("数据保存完毕");
            this.Close();
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

        private void EcoSystemIndexFrm_FormClosing(object sender, FormClosingEventArgs e)
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
                    MainForm.dt_EcosystemIndex = dt_Result;
                    MessageBox.Show("数据保存完毕");
                }
                else
                {
                    e.Cancel = false;
                }
            }
            e.Cancel = false;
        }


        #region 生态功能区选择

        private void rbt_FFGS_gnq_CheckedChanged(object sender, EventArgs e)
        {
            SetItemOperate(rbt_FFGS_gnq, gb_FFGS_gnq);
        }

        private void rbt_STBC_gnq_CheckedChanged(object sender, EventArgs e)
        {
            SetItemOperate(rbt_STBC_gnq, gb_STBC_gnq);
        }

        private void rbt_SYHY_gnq_CheckedChanged(object sender, EventArgs e)
        {
            SetItemOperate(rbt_SYHY_gnq, gb_SYHY_gnq);
        }

        private void rbt_SWDYXWH_gnq_CheckedChanged(object sender, EventArgs e)
        {
            SetItemOperate(rbt_SWDYXWH_gnq, gb_SWDYXWH_gnq);
        }
        #endregion

        #region 生态系统选择
        private void rtb_SL_STXT_CheckedChanged(object sender, EventArgs e)
        {
            SetItemOperate(rtb_SL_STXT, gb_SL_STXT);
        }

        private void rtb_CYCD_STXT_CheckedChanged(object sender, EventArgs e)
        {
            SetItemOperate(rtb_CYCD_STXT, gb_CYCD_STXT);
        }

        private void rtb_HM_STXT_CheckedChanged(object sender, EventArgs e)
        {
            SetItemOperate(rtb_HM_STXT, gb_HM_STXT);
        }

        private void rtb_SYSD_STXT_CheckedChanged(object sender, EventArgs e)
        {
            SetItemOperate(rtb_SYSD_STXT, gb_SYSD_STXT);
        }
        #endregion

        #region 自定义函数

        #region 获取选中的控件(普通)
        private List<string> GetCheckedItem_COM(TabPage tp_IndexName)
        {
            List<string> IndexName = new List<string>();
            for (int i = 0; i < tp_IndexName.Controls.Count; i++)
            {
                // 识别GroupBox控件;
                Type type1 = tp_IndexName.Controls[i].GetType();
                GroupBox groupBox = new GroupBox();
                if (type1 == groupBox.GetType())
                {
                    groupBox = (GroupBox)tp_IndexName.Controls[i];
                    for (int j = 0; j < groupBox.Controls.Count; j++)
                    {
                        // 识别CheckBox控件;
                        CheckBox checkbox = new CheckBox();
                        string str = "";
                        Type type2 = groupBox.Controls[j].GetType();
                        if (type2 == checkbox.GetType())
                        {
                            checkbox = (CheckBox)groupBox.Controls[j];
                            if (checkbox.Checked)
                            {
                                str = checkbox.Text;
                                str = str.Substring(0, str.IndexOf("("));
                                IndexName.Add(str);
                            }
                        }
                    }
                }
            }
            return IndexName;
        }
        #endregion

        #region 获取选中的控件(专题)
        private List<string> GetCheckedItem_SPE(TabPage tabPage)
        {
            List<string> IndexName = new List<string>();
            string str1 = "";

            for (int i = 0; i < tabPage.Controls.Count; i++)
            {
                Type typ1 = tabPage.Controls[i].GetType();
                RadioButton radioButton = new RadioButton();
                if (typ1 == radioButton.GetType())
                {
                    radioButton = (RadioButton)tabPage.Controls[i];
                    if (radioButton.Checked)
                    {
                        str1 = radioButton.Text;
                        str1 = str1.Substring(0, str1.IndexOf("("));
                    }
                }
            }

            #region 识别选中的CheckBox
            for (int i = 0; i < tabPage.Controls.Count; i++)
            {
                Type typ1 = tabPage.Controls[i].GetType();
                GroupBox groupBox = new GroupBox();
                if (typ1 == groupBox.GetType())
                {
                    groupBox = (GroupBox)tabPage.Controls[i];
                    for (int j = 0; j < groupBox.Controls.Count; j++)
                    {
                        // 识别CheckBox控件;
                        CheckBox checkBox = new CheckBox();
                        Type type2 = groupBox.Controls[j].GetType();
                        if (type2 == checkBox.GetType())
                        {
                            string str2 = "";
                            checkBox = (CheckBox)groupBox.Controls[j];
                            if (checkBox.Checked)
                            {
                                str2 = checkBox.Text;
                                str2 = str2.Substring(0, str2.IndexOf("("));
                                IndexName.Add(str1 + "_" + str2);
                            }
                        }
                    }
                }
                
            }
            #endregion
            return IndexName;
        }
        #endregion

        #region 设置CheckBox的操作状态
        private void SetItemOperate(RadioButton radioButton, GroupBox groupBox)
        {
            if (radioButton.Checked == false)
            {
                //groupBox.Enabled = false;
                for (int i = 0; i < groupBox.Controls.Count; i++)
                {
                    CheckBox checkbox = new CheckBox();
                    Type type = groupBox.Controls[i].GetType();
                    if (checkbox.GetType() == type)
                    {
                        checkbox = (CheckBox)groupBox.Controls[i];
                        checkbox.CheckState = CheckState.Unchecked;
                        checkbox.Enabled = false;
                    }
                }
            }
            else if (radioButton.Checked == true)
            {
                //groupBox.Enabled = true;
                for (int i = 0; i < groupBox.Controls.Count; i++)
                {
                    CheckBox checkbox = new CheckBox();
                    Type type = groupBox.Controls[i].GetType();
                    if (checkbox.GetType() == type)
                    {
                        checkbox = (CheckBox)groupBox.Controls[i];
                        checkbox.Enabled = true;
                    }
                }
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

        #region 打开Excel数据表
        private DataSet ToDataTable(string filePath)
        {
            string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filePath + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
            string sql_F = "Select * FROM [{0}]";

            OleDbConnection conn = null;
            OleDbDataAdapter da = null;
            DataTable dtSheetName = null;

            DataSet ds = new DataSet();
            try
            {
                // 初始化连接，并打开
                conn = new OleDbConnection(connStr);
                conn.Open();

                // 获取数据源的表定义元数据                       
                string SheetName = "";
                dtSheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                // 初始化适配器
                da = new OleDbDataAdapter();
                for (int i = 0; i < dtSheetName.Rows.Count; i++)
                {
                    SheetName = (string)dtSheetName.Rows[i]["TABLE_NAME"];

                    if (SheetName.Contains("$") && !SheetName.Replace("'", "").EndsWith("$"))
                    {
                        continue;
                    }

                    da.SelectCommand = new OleDbCommand(String.Format(sql_F, SheetName), conn);
                    DataSet dsItem = new DataSet();
                    da.Fill(dsItem, "tblName");

                    ds.Tables.Add(dsItem.Tables[0].Copy());
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    da.Dispose();
                    conn.Dispose();
                }
            }
            return ds;
        }
        #endregion

        #endregion



    }
}
