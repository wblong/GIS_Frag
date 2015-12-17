using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using System.Collections;
using ESRI.ArcGIS.DataSourcesFile;
using System.Threading;
 

namespace AE_Environment.Forms
{
    public partial class frmDataConfig : Form
    {


        private AxMapControl m_axMapControl;
        private Dictionary<int, ILayer> MapLayer = null;
        private IFeatureClass datacoverFC = null;
        private IFeatureClass dataZoneFc = null;
        private string codeField = "";
        private string zoneField = "";
        private Thread calThread;
        public frmDataConfig()
        {
            InitializeComponent();
        }
        public frmDataConfig(AxMapControl axMapControl):this() {
            m_axMapControl = axMapControl;
             
        }

        private void loadvalue()
       {

        }

        //获取唯一值
        private IEnumerator GetUniqueValues(IFeatureClass pFeatureClass, string strFieldName)
        {

            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = strFieldName;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();
            return pEnumerator;
        }
        /// <summary>
        /// 创建查询表
        /// </summary>
        /// <param name="queryDef"></param>
        /// <param name="str_pk"></param>
        /// <param name="workspaceName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private ITable CreateQueryTable(IQueryDef queryDef,string str_pk,IWorkspaceName workspaceName,string tableName)
        {

            IQueryName2 queryName2 = (IQueryName2)new TableQueryNameClass();
            queryName2.QueryDef = queryDef;
            //queryName2.PrimaryKey = "streets.StreetID";
            queryName2.PrimaryKey = str_pk;
            queryName2.CopyLocally = true;

            // Set the workspace and name of the new QueryTable.
            IDatasetName datasetName = (IDatasetName)queryName2;
            datasetName.WorkspaceName = workspaceName;
            datasetName.Name = tableName;

            // Open the virtual table.
            IName name = (IName)queryName2;
            ITable table = (ITable)name.Open();
            return table;
        }

        private bool CheckingInput()
        {
            if (this.comboxlayer.SelectedItem == null)
            {
                MessageBox.Show("请选择输入图层.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            
            if (this.comboxFiledCC.Text == "")
            {
                MessageBox.Show("请选择编码字段.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (this.comboZone.Text == "")
            {
                MessageBox.Show("请选择分区字段.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            return true;
        }

        private void frmDataConfig_Load(object sender, EventArgs e)
        {
            this.MapLayer = new Dictionary<int, ILayer>();
            for (int i = 0; i < this.m_axMapControl.LayerCount; i++)
            {
                ILayer layer = this.m_axMapControl.get_Layer(i);
                IFeatureLayer layer2 = layer as IFeatureLayer;
                if ((((layer != null) && layer.Valid) && (layer is IFeatureLayer)))
                {
                    this.comboxlayer.Items.Add(layer.Name);
                    this.comboxZonelayer.Items.Add(layer.Name);
                    this.MapLayer.Add(this.comboxlayer.Items.Count - 1, layer);
                    
                }
            }
            this.comboxZonelayer.Items.Add("无");
            if (this.comboxlayer.Items.Count > 0)
            {
                this.comboxlayer.SelectedIndex = 0;
                 
            }
            if (this.comboxZonelayer.Items.Count>0)
            {
                this.comboxZonelayer.SelectedIndex = comboxZonelayer.Items.Count-1;
            }
            //是否已经设置了输入数据,是：显示已经设置好的数据
            if (MainForm.dataInputInfo.layerDataCover != null)
            {
                comboxlayer.Text = MainForm.dataInputInfo.layerDataCover.AliasName;
            }
             
            
            if (MainForm.dataInputInfo.zoneField != "")
            {

                comboZone.Text = MainForm.dataInputInfo.zoneField;
            }
            if (MainForm.dataInputInfo.codeField != "")
            {
                comboxFiledCC.Text = MainForm.dataInputInfo.codeField;
            }
             
        }

        private void comboxlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboxFiledCC.Items.Count > 0)
            {
                comboxFiledCC.Items.Clear();
            }
            ILayer layer = this.MapLayer[this.comboxlayer.SelectedIndex];
            IFeatureClass featureClass = (layer as IFeatureLayer).FeatureClass;
            IFields pFields = featureClass.Fields;
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                comboxFiledCC.Items.Add(pFields.get_Field(i).Name);

            }

            if (comboZone.Items.Count > 0)
            {
                comboZone.Items.Clear();
            }

            for (int i = 0; i < pFields.FieldCount; i++)
            {
                comboZone.Items.Add(pFields.get_Field(i).Name);

            }
            comboxFiledCC.Text = "";
            comboZone.Text = "";
        }
        /// <summary>
        /// 分区图层
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboLayerZone_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_Zone_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ButCancel_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            if (calThread!=null)
            {
                if (calThread.ThreadState == ThreadState.Running)
                {
                    calThread.Abort();
                }
            }
           
            base.Close();
        }

        private void ButOK_Click(object sender, EventArgs e)
        {
            if (this.CheckingInput())
            {
                
                this.comboxlayer.Enabled = false;
                this.comboxFiledCC.Enabled = false;
                this.comboZone.Enabled = false;
                this.ButCreate.Enabled = false;
                this.ButCancel.Enabled = true;
                this.progressBar1.Visible = true;
                this.progressBar1.Minimum = 0;
                ILayer layer1 = this.MapLayer[this.comboxlayer.SelectedIndex];
                if (this.comboxZonelayer.SelectedIndex!=comboxZonelayer.Items.Count-1)
                {
                    ILayer layer2 = this.MapLayer[this.comboxZonelayer.SelectedIndex];
                    dataZoneFc = (layer2 as IFeatureLayer).FeatureClass;
                }
                else
                {
                    dataZoneFc = null;
                }
                
                datacoverFC = (layer1 as IFeatureLayer).FeatureClass;
               
                zoneField = comboZone.Text;
                codeField = comboxFiledCC.Text;
                this.progressBar1.Maximum = datacoverFC.FeatureCount(null)*3;
                MainForm.baseData = new Model.BaseData(datacoverFC, zoneField, codeField);
                MainForm.baseData.SetZoneData(dataZoneFc);

                MainForm.dataConfiguration = true;
               
                DateTime beforDT = System.DateTime.Now;
          
             //耗时巨大的代码  
                try
                {
                    MainForm.baseData.InitData(progressBar1);
                    DateTime afterDT = System.DateTime.Now;
                    TimeSpan ts = afterDT.Subtract(beforDT);
                    string timer1 = string.Format("配置完成，总共花费{0:0.##}分。", ts.TotalMilliseconds * 0.001/60);
                    MessageBox.Show(timer1);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("数据配置错误!");
                }
            }
            else { return; }

            base.Close();
        }

        private void comboxZonelayer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        

    }
}
