using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;


using AE_Environment.TypesStruct;
using System.IO;
using AE_Environment.Model;
 

namespace AE_Environment.Forms
{


    
    public partial class ClssFrm : Form
    {


        private DataTable clssTable = null;//数据表
        public  int limitValue = 0;//
        public static ILayer pLayer = null;
        public static DataTable weightDt = null;


        Model.PatchClass classCaclute;
        public List<string> codeValues;//统计的分类
        public List<PatchClassIndex> patchClassIndexes;//计算的指标
        public List<InterfaceClassIndex> patchClassCac;//接口函数
        
        /// <summary>
        /// 构造函数初始化
        /// </summary>
        /// <param name="pHookHelper"></param>
        public ClssFrm()
        {
            InitializeComponent();
            

             
        }
        public ClssFrm(BaseData baseData)
        {
            InitializeComponent();
            codeValues = new List<string>();
            patchClassCac = new List<InterfaceClassIndex>();
            patchClassIndexes = new List<PatchClassIndex>();
            classCaclute = new Model.PatchClass();
            classCaclute.SetBaseData(baseData);

        }
        /// <summary>
        /// 窗体加载初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClssFrm_Load(object sender, EventArgs e)
        {
            
           
            //添加分类
            for (int i = 0; i < classCaclute.baseData.ccValue.Count;i++)
            {
                string code = classCaclute.baseData.ccValue[i];
                if (MainForm.pData.ContainsKey(code))
                {
                    comboValue.Items.Add(MainForm.pData[code]);
                }
                
            }
            comboValue.Text = "";

            //listview添加表头
            ColumnHeader ch = new ColumnHeader();
            ch.Text = "分类编码";
            ch.Width = 100;
            ch.TextAlign = HorizontalAlignment.Center;
            this.listView1.Columns.Add(ch);

            ColumnHeader chN = new ColumnHeader();
            chN.Text = "分类类型";
            chN.Width = 100;
            chN.TextAlign = HorizontalAlignment.Center;
            this.listView1.Columns.Add(chN);
            ////排序
            this.listView1.ListViewItemSorter = new Utilities.ListViewColumnSorter();
            listView1.ColumnClick += new ColumnClickEventHandler(Utilities.ListViewHelper.ListView_ColumnClick);
            //
            //btn_input.Enabled = false;
        }
       
       
        /// <summary>
        /// 指标类的选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboValue.SelectedIndex;
            string value = comboValue.Items[index].ToString();
            string key="";
            foreach (string k in MainForm.pData.Keys)
            {
                if (MainForm.pData[k]==value)
                {
                    key = k;
                    break;
                }
            }
            this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度 
            ListViewItem lvi = new ListViewItem();

            lvi.Text = key;
            lvi.SubItems.Add(value);
            this.listView1.Items.Add(lvi);

            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。 
        }
        /// <summary>
        /// 删除选择项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView1.SelectedItems)  //选中项遍历  
            {
                listView1.Items.RemoveAt(lvi.Index); // 按索引移除  
   
            }     
        }

        private bool CheckingInput()
        {
            if (checkBox_MMI.Checked && pLayer == null)
            {

                MessageBox.Show("请选择输入图层.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 确定--将参数保存到paramInfo中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (!CheckingInput())
            {
                return;
            }
            //添加分类
            if (listView1.Items.Count>0)
            {
                codeValues.Clear();
                foreach (ListViewItem item in this.listView1.Items)
                {
                    codeValues.Add(item.Text);
                   
                } 

            }
            //添加指标
            patchClassCac.Clear();
            for (int i = 0; i < patchClassIndexes.Count; i++)
            {
                switch (patchClassIndexes[i])
                {

                    case PatchClassIndex.TotalArea:
                        patchClassCac.Add(new Model.FunctionIndexes.CTotalArea(codeValues));
                        break;
                    case PatchClassIndex.AreaRatio:
                        patchClassCac.Add(new Model.FunctionIndexes.CAreaRatio(codeValues));
                        break;
                    case PatchClassIndex.MaxAreaIndex:
                        patchClassCac.Add(new Model.FunctionIndexes.CMaxAreaIndex(codeValues));
                        break;
                    case PatchClassIndex.TotalEgde:
                        patchClassCac.Add(new Model.FunctionIndexes.CTotalEdge(codeValues));
                        break;
                    case PatchClassIndex.EdgeDensity:
                        patchClassCac.Add(new Model.FunctionIndexes.CEdgeDensity(codeValues));
                        break;
                    case PatchClassIndex.MeshIndex:
                        patchClassCac.Add(new Model.FunctionIndexes.CMeshIndex(codeValues));
                        break;
                    case PatchClassIndex.UMeshIndex:
                        patchClassCac.Add(new Model.FunctionIndexes.CUMeshIndex(codeValues, limitValue));
                        break;
                    case PatchClassIndex.PAFRACIndex:
                        patchClassCac.Add(new Model.FunctionIndexes.CPAFACIndex(codeValues));
                        break;
                    case PatchClassIndex.NumberOfPatch:
                        patchClassCac.Add(new Model.FunctionIndexes.CNumberOfPatch(codeValues));
                        break;
                    case PatchClassIndex.PatchDensity:
                        patchClassCac.Add(new Model.FunctionIndexes.CPatchDensity(codeValues));
                        break;
                    case PatchClassIndex.MeanPatchSize:
                        patchClassCac.Add(new Model.FunctionIndexes.CMeanPatchSize(codeValues));
                        break;

                }
            }
           
            classCaclute.InitData(codeValues, patchClassIndexes, patchClassCac);
            DateTime beforDT = System.DateTime.Now;
            bool isSuccessed = classCaclute.CalculateIndex(this.progressBar1);
            DateTime afterDT = System.DateTime.Now;
            TimeSpan ts = afterDT.Subtract(beforDT);
            string timer1 = string.Format("计算完成,共耗时{0:0.##}分。", ts.TotalMilliseconds * 0.001 / 60);
            if (isSuccessed)
            {
                classCaclute.BuildResultTable();
                this.dataGridView1.DataSource = classCaclute.result_dt;
                MessageBox.Show(timer1);
            }
            else
            {

                MessageBox.Show("计算失败!");
            }
            this.progressBar1.Value = 0;
        }
        

      

        
        /// <summary>
        /// 总面积
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_TA.Checked)
            {
                patchClassIndexes.Add(PatchClassIndex.TotalArea);
                
            }
            else
            {
                patchClassIndexes.Remove(PatchClassIndex.TotalArea);
                 
            }
        }
        /// <summary>
        /// 面积比
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_AR.Checked)
            {
                patchClassIndexes.Add(PatchClassIndex.AreaRatio);

            }
            else
            {
                patchClassIndexes.Remove(PatchClassIndex.AreaRatio);

            }
        }
        /// <summary>
        /// 最大面积
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_MAI.Checked)
            {
                patchClassIndexes.Add(PatchClassIndex.MaxAreaIndex);
            }
            else
            {
                patchClassIndexes.Remove(PatchClassIndex.MaxAreaIndex);

            }
        }
        /// <summary>
        /// 总周长
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_TE.Checked)
            {
                patchClassIndexes.Add(PatchClassIndex.TotalEgde);
            }
            else
            {
                patchClassIndexes.Remove(PatchClassIndex.TotalEgde);

            }
        }
        /// <summary>
        /// 周长/面积
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_ED.Checked)
            {
                patchClassIndexes.Add(PatchClassIndex.EdgeDensity);
            }
            else
            {
                patchClassIndexes.Remove(PatchClassIndex.EdgeDensity);
                 
            }
        }
       /// <summary>
       /// 破碎度
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_MI.Checked)
            {
                patchClassIndexes.Add(PatchClassIndex.MeshIndex);
                
            }
            else
            {
                patchClassIndexes.Remove(PatchClassIndex.MeshIndex);

            }
        }

        private void btnALL_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < comboValue.Items.Count;i++ )
            {

                string value = comboValue.Items[i].ToString();
                string key = "";
                foreach (string k in MainForm.pData.Keys)
                {
                    if (MainForm.pData[k] == value)
                    {
                        key = k;
                        break;
                    }
                }
                this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度 
                ListViewItem lvi = new ListViewItem();

                lvi.Text = key;
                lvi.SubItems.Add(value);
                this.listView1.Items.Add(lvi);

                this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。 
            }
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }
        /// <summary>
        /// 创建指标数据表
        /// </summary>
        private void CreateDataTable()
        {

            List<ClassIndex> clssIndex = MainForm.clssParamInfo.clssIndex;
            if (clssIndex.Count>0)
            {
                
                clssTable = MainForm.clssParamInfo.resultTable;
                clssTable.Rows.Clear();
                for (int i = 3; i < clssTable.Columns.Count;i++ )
                {
                    clssTable.Columns.RemoveAt(i);
                }
                //创建必要的属性列
                
                if (!clssTable.Columns.Contains("OID"))
                {

                    DataColumn pDataColumn0 = new DataColumn();
                    pDataColumn0.ColumnName = "OID";
                    pDataColumn0.Unique = true;
                    pDataColumn0.DataType = System.Type.GetType("System.Int32");
                    pDataColumn0.AllowDBNull = false;
                    pDataColumn0.AutoIncrement = true;
                    clssTable.Columns.Add(pDataColumn0);
                }


                if (!clssTable.Columns.Contains("ZID"))
                {
                    DataColumn pDataColumn1 = new DataColumn();
                    pDataColumn1.ColumnName = "ZID";
                    pDataColumn1.DataType = System.Type.GetType("System.Int64");
                    pDataColumn1.AllowDBNull = false;
                    clssTable.Columns.Add(pDataColumn1);
                }
                if (!clssTable.Columns.Contains("Type"))
                {
                    DataColumn pDataColumn2 = new DataColumn();
                    pDataColumn2.ColumnName = "Type";
                    pDataColumn2.DataType = System.Type.GetType("System.String");
                    pDataColumn2.MaxLength = 20;
                    pDataColumn2.AllowDBNull = false;
                    clssTable.Columns.Add(pDataColumn2);
                }
                

                DataColumn pDataColumn = null;

                for (int i = 0; i < clssIndex.Count;i++ )
                {
                    pDataColumn = new DataColumn();
                    switch (clssIndex[i])
                    {

                        case ClassIndex.AreaRatio:
                            pDataColumn.ColumnName = "AR(%)";
                            break;
                        case ClassIndex.EdgeDensity:
                            pDataColumn.ColumnName = "ED(m/hectare)";
                            break;
                        case ClassIndex.MaxAreaIndex:
                            pDataColumn.ColumnName = "LPI(%)";
                            break;
                        case ClassIndex.TotalArea:
                            pDataColumn.ColumnName = "TA(m2)";
                            break;
                        case  ClassIndex.TotalEgde:
                            pDataColumn.ColumnName = "TE(m)";
                            break;
                        case ClassIndex.MeshIndex:
                            pDataColumn.ColumnName = "MI(km2)";
                            break;
                        case ClassIndex.UMeshIndex:
                            pDataColumn.ColumnName = "UMI(km2)";
                            break;
                        case ClassIndex.MMeshIndex:
                            pDataColumn.ColumnName = "MMI(km2)";
                            break;
                        case ClassIndex.PAFRACIndex:
                            pDataColumn.ColumnName = "PAFRACIndex(None)";
                            break;
                        case ClassIndex.CWEDIndex:
                            pDataColumn.ColumnName = "CWED(m/hectare)";
                            break;
                        case ClassIndex.TECIIndex:
                            pDataColumn.ColumnName = "TECI(%)";
                            break;
                    }

                    
                    pDataColumn.DataType = System.Type.GetType("System.Double");
                   // pDataColumn.AllowDBNull = false;
                    if (!clssTable.Columns.Contains(pDataColumn.ColumnName))
                    {
                        clssTable.Columns.Add(pDataColumn);
                         
                    }
                     
                }
            }
        }
        /// <summary>
        /// 修正后的破碎度指数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_UMI_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_UMI.Checked)
            {
                patchClassIndexes.Add(PatchClassIndex.UMeshIndex);
            }
            else
            {

                patchClassIndexes.Remove(PatchClassIndex.UMeshIndex);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            limitValue = Convert.ToInt32(textBox1.Text);
        }

        private void checkBox_MMI_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_MMI.Checked)
            {

            }
            else
            {

            }
        }

        private void btn_Config_Click(object sender, EventArgs e)
        {
            
            
        }

        private void checkBox_PAFRAC_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_PAFRAC.Checked)
            {
                patchClassIndexes.Add(PatchClassIndex.PAFRACIndex);
            }
            else
            {
                patchClassIndexes.Remove(PatchClassIndex.PAFRACIndex);
                
            }
        }

        private void btn_input_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofg = new OpenFileDialog();
            ofg.Filter = "权重文件|*.csv";
            
            if (ofg.ShowDialog()==DialogResult.OK)
            {
                string filename = ofg.FileName;
                try
                {
                    weightDt = FragStats.Stats.OpenCSV(filename);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("读取权重系数失败！");
                }
                
            }
            //weightDt = FragStats.Stats.OpenCSV(System.Environment.CurrentDirectory+"\\contrast.csv");
            
        }
        public static double GetWeightFromDt(DataTable dt,string type1,string type2)
        {
            double value = 0.0;
            string express = "Type=" + type1;
            if (dt.Select(express).Length>0)
            {
                DataRow dr = dt.Select(express)[0];
                DataColumn dc = FragStats.Stats.GetTableColumnByName(dt,type2);
                object obj=dr[dc];
                value=Convert.ToDouble(obj);

            }
            else
            {

                return 0;
            }
            return value;
        }

        private void checkBox_CWED_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_CWED.Checked)
            {
                
            }
            else
            {
                 

            }
        }

        private void checkBox_TECI_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_TECI.Checked)
            {
                
            }
            else
            {
                 

            }
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage3)
            {
                btn_input.Enabled = true;
            }
            else
            {

                btn_input.Enabled = false;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            MainForm.dt_class= classCaclute.result_dt;
            MessageBox.Show("保存成功！!");
            this.Close();
        }

        private void checkBox_NP_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_NP.Checked)
            {
                patchClassIndexes.Add(PatchClassIndex.NumberOfPatch);
            }
            else
            {

                patchClassIndexes.Remove(PatchClassIndex.NumberOfPatch);

            }
        }

        private void checkBox_PD_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_PD.Checked)
            {
                patchClassIndexes.Add(PatchClassIndex.PatchDensity);
            }
            else
            {

                patchClassIndexes.Remove(PatchClassIndex.PatchDensity);
            }
        }

        private void checkBox_MPS_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_MPS.Checked)
            {
                patchClassIndexes.Add(PatchClassIndex.MeanPatchSize);

            }
            else
            {

                patchClassIndexes.Remove(PatchClassIndex.MeanPatchSize);

            }
        }    
        
    }
}
