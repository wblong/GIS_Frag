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
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using AE_Environment.TypesStruct;
using AE_Environment.Model;





namespace AE_Environment.Forms
{
    public partial class LandFrm : Form
    {

        private DataTable landTable = null;//数据表

        public  int limitValue = 0;//
        public static ILayer pLayer = null;
        public static DataTable weightDt = null;

        Model.PatchLand landCaclute;
        //public List<string> codeValues;//统计的分类
        public List<PatchLandIndex> patchLandIndexes;//计算的指标
        public List<InterfaceLandIndex> patchLandCac;//接口函数
        public LandFrm(IMapControl3 mapControl,BaseData baseData)
        {
            InitializeComponent();
       
             
            patchLandCac = new List<InterfaceLandIndex>();
            patchLandIndexes = new List<PatchLandIndex>();
            landCaclute = new Model.PatchLand();
            landCaclute.SetBaseData(baseData);
            if (baseData.zone_FC==null)
            {
                checkBox_ED.Enabled = false;
                checkBox_TE.Enabled = false;
            }
        }
        private void LandFrm_Load(object sender, EventArgs e)
        {
            if (MainForm.dataInputInfo.layerDataCover == null)
            {
                return;
            }
             
            if (MainForm.landscapeParamInfo.landIndex.Count>0)
            {
                for (int i = 0; i < MainForm.landscapeParamInfo.landIndex.Count;i++ )
                {
                    switch (MainForm.landscapeParamInfo.landIndex[i])
                    {
                        case LandscapeIndex.TotalEgde:
                            checkBox_TE.Checked = true;
                            break;
                        case LandscapeIndex.TotalArea:
                            checkBox_TA.Checked = true;
                            break;
                        case LandscapeIndex.EdgeDensity:
                            checkBox_ED.Checked = true;
                            break;
                        case LandscapeIndex.MaxAreaIndex:
                            checkBox_MAI.Checked = true;
                            break;
                        case LandscapeIndex.PAFRACIndex:
                            checkBox_PAFRAC.Checked = true;
                            break;
                        case LandscapeIndex.MMeshIndex:
                            checkBox_MMI.Checked = true;
                            break;
                        case LandscapeIndex.UMeshIndex:
                            checkBox_UMI.Checked = true;
                            break;
                        case LandscapeIndex.MeshIndex:
                            checkBox_MI.Checked = true;
                            break;
                        case LandscapeIndex.CWEDIndex:
                            //checkBox_CWED.Checked = true;
                            break;
                        case LandscapeIndex.TECIIndex:
                            //checkBox_TECI.Checked = true;
                            break;

                    }
                }
            }
        }
        private bool CheckingInput()
        {
           
//             if ((checkBox_TECI.Checked || checkBox_CWED.Checked) && weightDt == null)
//             {
//                 MessageBox.Show("请配置权重参数！.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
//             }


            return true;
        }
        private void btn_OK_Click(object sender, EventArgs e)
        {
            
            if (!CheckingInput())
            {
                return;
            }
            try
            {
                patchLandCac.Clear();
                for (int i = 0; i < patchLandIndexes.Count;i++ )
                {

                    switch (patchLandIndexes[i])
                    {
                        case PatchLandIndex.TotalArea:
                            patchLandCac.Add(new Model.FunctionIndexes.LTotalArea());

                            break;
                        case PatchLandIndex.TotalEgde:
                            patchLandCac.Add(new Model.FunctionIndexes.LTotalEdge());

                            break;
                        case PatchLandIndex.MaxAreaIndex:
                            patchLandCac.Add(new Model.FunctionIndexes.LMaxAreaIndex());

                            break;
                        case PatchLandIndex.EdgeDensity:
                            patchLandCac.Add(new Model.FunctionIndexes.LEdgeDensity());

                            break;
                        case PatchLandIndex.PAFRACIndex:
                            patchLandCac.Add(new Model.FunctionIndexes.LPAFACIndex());
                            break;
                        case PatchLandIndex.MeshIndex:
                            patchLandCac.Add(new Model.FunctionIndexes.LMeshIndex());
                            break;
                        case PatchLandIndex.UMeshIndex:
                            patchLandCac.Add(new Model.FunctionIndexes.LUMeshIndex(limitValue));
                            break;
                        

                    }
                }
                landCaclute.InitData(patchLandIndexes, patchLandCac);
                DateTime beforDT = System.DateTime.Now;
                bool isSuccessed = landCaclute.CalculateIndex(this.progressBar1);
                DateTime afterDT = System.DateTime.Now;
                TimeSpan ts = afterDT.Subtract(beforDT);
                string timer1 = string.Format("计算完成,共耗时{0:0.##}分。", ts.TotalMilliseconds * 0.001 / 60);

                if (isSuccessed)
                {
                    landCaclute.BuildResultTable();
                    this.dataGridView1.DataSource = landCaclute.result_dt;
                    MessageBox.Show(timer1);

                }
                else
                {

                    MessageBox.Show("计算失败 !");
                }
                this.progressBar1.Value = 0;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("创建指标数据表失败！");
            }
             
        }



     
        /// <summary>
        /// 创建指标数据表
        /// </summary>
        private void CreateDataTable()
        {

            List<LandscapeIndex> landIndex = MainForm.landscapeParamInfo.landIndex;
            if (landIndex.Count > 0)
            {

                landTable = MainForm.landscapeParamInfo.resultTable;
                for (int i = 2; i < landTable.Columns.Count; i++)
                {
                   landTable.Columns.RemoveAt(i);
                }
                //创建必要的属性列

                if (!landTable.Columns.Contains("OID"))
                {

                    DataColumn pDataColumn0 = new DataColumn();
                    pDataColumn0.ColumnName = "OID";
                    pDataColumn0.Unique = true;
                    pDataColumn0.DataType = System.Type.GetType("System.Int32");
                    pDataColumn0.AllowDBNull = false;
                    pDataColumn0.AutoIncrement = true;
                    landTable.Columns.Add(pDataColumn0);
                }


                if (!landTable.Columns.Contains("ZID"))
                {
                    DataColumn pDataColumn1 = new DataColumn();
                    pDataColumn1.ColumnName = "ZID";
                    pDataColumn1.DataType = System.Type.GetType("System.Int64");
                    pDataColumn1.AllowDBNull = false;
                    landTable.Columns.Add(pDataColumn1);
                }
                 


                DataColumn pDataColumn = null;

                for (int i = 0; i < landIndex.Count; i++)
                {
                    pDataColumn = new DataColumn();
                    switch (landIndex[i])
                    {

                        case LandscapeIndex.EdgeDensity:
                            pDataColumn.ColumnName = "ED(m/hectare)";
                            break;
                        case LandscapeIndex.MaxAreaIndex:
                            pDataColumn.ColumnName = "LPI(%)";
                            break;
                        case LandscapeIndex.TotalArea:
                            pDataColumn.ColumnName = "TA(m2)";
                            break;
                        case LandscapeIndex.TotalEgde:
                            pDataColumn.ColumnName = "TE(m)";
                            break;
                        case LandscapeIndex.PAFRACIndex:
                            pDataColumn.ColumnName = "LPAFRACIndex(None)";
                            break;
                        case LandscapeIndex.MeshIndex:
                            pDataColumn.ColumnName = "MI(km2)";
                            break;
                        case LandscapeIndex.UMeshIndex:
                            pDataColumn.ColumnName = "UMI(km2)";
                            break;
                        case LandscapeIndex.MMeshIndex:
                            pDataColumn.ColumnName = "MMI(km2)";
                            break;
                        case LandscapeIndex.CWEDIndex:
                            pDataColumn.ColumnName = "CWED(m/hectare)";
                            break;
                        case LandscapeIndex.TECIIndex:
                            pDataColumn.ColumnName = "TECI(%)";
                            break;
                         
                    }


                    pDataColumn.DataType = System.Type.GetType("System.Double");
                   // pDataColumn.AllowDBNull = false;
                    if (!landTable.Columns.Contains(pDataColumn.ColumnName))
                    {
                        landTable.Columns.Add(pDataColumn);

                    }

                }
            }
        }

        private void checkBox_TA_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_TA.Checked)
            {
                patchLandIndexes.Add(PatchLandIndex.TotalArea);

            }
            else
            {

                patchLandIndexes.Remove(PatchLandIndex.TotalArea);

            }
        }

        private void checkBox_ED_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_ED.Checked)
            {
                patchLandIndexes.Add(PatchLandIndex.EdgeDensity);

            }
            else
            {

                patchLandIndexes.Remove(PatchLandIndex.EdgeDensity);

            }
        }

        private void checkBox_MAI_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_MAI.Checked)
            {
                patchLandIndexes.Add(PatchLandIndex.MaxAreaIndex);

            }
            else
            {

                patchLandIndexes.Remove(PatchLandIndex.MaxAreaIndex);

            }
        }

        private void checkBox_TE_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_TE.Checked)
            {

                patchLandIndexes.Add(PatchLandIndex.TotalEgde);
            }
            else
            {

                patchLandIndexes.Remove(PatchLandIndex.TotalEgde);     

            }
        }

        private void checkBox_PAFRAC_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_PAFRAC.Checked)
            {
                patchLandIndexes.Add(PatchLandIndex.PAFRACIndex);
            }
            else
            {
                patchLandIndexes.Remove(PatchLandIndex.PAFRACIndex);
                 
            }
        }

        private void btn_Config_Click(object sender, EventArgs e)
        {
            Forms.LayerFrm lf = new Forms.LayerFrm(MainForm.m_mapControl);
            lf.ShowDialog();
            pLayer = lf.PLayer;
        }

        private void checkBox_MMI_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_MMI.Checked)
            {
                btn_Config.Enabled = true;
                if (!MainForm.landscapeParamInfo.landIndex.Contains(LandscapeIndex.MMeshIndex))
                {
                    MainForm.landscapeParamInfo.landIndex.Add(LandscapeIndex.MMeshIndex);
                }
            }
            else
            {

                btn_Config.Enabled = false;
                MainForm.landscapeParamInfo.landIndex.Remove(LandscapeIndex.MMeshIndex);
            }
        }

        private void checkBox_UMI_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_UMI.Checked)
            {
                patchLandIndexes.Add(PatchLandIndex.UMeshIndex);
            }
            else
            {
                patchLandIndexes.Remove(PatchLandIndex.UMeshIndex);

            }
        }

        private void checkBox_MI_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_MI.Checked)
            {
                patchLandIndexes.Add(PatchLandIndex.MeshIndex);
            }
            else
            {
                patchLandIndexes.Remove(PatchLandIndex.MeshIndex);

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            limitValue = Convert.ToInt32(textBox1.Text);
        }

        private void btn_input_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofg = new OpenFileDialog();
            ofg.Filter = "权重文件|*.csv";

            if (ofg.ShowDialog() == DialogResult.OK)
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
        }
        public static double GetWeightFromDt(DataTable dt, string type1, string type2)
        {
            double value = 0.0;
            string express = "Type=" + type1;
            if (dt.Select(express).Length > 0)
            {
                DataRow dr = dt.Select(express)[0];
                DataColumn dc = FragStats.Stats.GetTableColumnByName(dt, type2);
                object obj = dr[dc];
                value = Convert.ToDouble(obj);

            }
            else
            {

                return 0;
            }
            return value;
        }
        private void checkBox_CWED_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkBox_CWED.Checked)
            //{
            //    if (!MainForm.landscapeParamInfo.landIndex.Contains(LandscapeIndex.CWEDIndex))
            //    {
            //        MainForm.landscapeParamInfo.landIndex.Add(LandscapeIndex.CWEDIndex);
            //    }
            //}
            //else
            //{
            //    MainForm.landscapeParamInfo.landIndex.Remove(LandscapeIndex.CWEDIndex);

            //}
        }

        private void checkBox_TECI_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkBox_TECI.Checked)
            //{
            //    if (!MainForm.landscapeParamInfo.landIndex.Contains(LandscapeIndex.TECIIndex))
            //    {
            //        MainForm.landscapeParamInfo.landIndex.Add(LandscapeIndex.TECIIndex);
            //    }
            //}
            //else
            //{
            //    MainForm.landscapeParamInfo.landIndex.Remove(LandscapeIndex.TECIIndex);

            //}
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            MainForm.dt_land = landCaclute.result_dt;
            MessageBox.Show("保存成功！!");
            this.Close();
            
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox_NP_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_PD_CheckedChanged(object sender, EventArgs e)
        {

        }

       

        private void checkBox_MPS_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
