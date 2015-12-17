using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using BaseGIS;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AE_Environment.Forms
{
	public partial class frmDissolve: Form
	{
        private AxMapControl m_axMapControl;
        private Dictionary<int, ILayer> MapLayer = null;
        
		public frmDissolve(AxMapControl axMapControl)
		{
			InitializeComponent();
            this.m_axMapControl = axMapControl;
		}

        private void butcancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.Description = "保存叠加数据";
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            this.folderBrowserDialog1.ShowDialog();
            string selectedPath = this.folderBrowserDialog1.SelectedPath;
            if (selectedPath == "")
            {
                MessageBox.Show("请选择保存路径！");
            }
            else if (!this.folderBrowserDialog1.SelectedPath.Contains(".gdb"))
            {
                MessageBox.Show("必须选择FileGDB文件夹【.gdb】");
                this.textBox1.Text = "";
            }
            else
            {
                this.textBox1.Text = selectedPath;
            }
            
        }

        private bool CheckingInput()
        {
            if (this.comboxlayer.SelectedItem == null)
            {
                MessageBox.Show("请选择输入图层.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (this.comboxFiled.SelectedItem.ToString()== "")
            {
                MessageBox.Show("请选择融合字段.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (this.textBox1.Text == "")
            {
                MessageBox.Show("请选择输出路径.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            return true;
        }

       

        private void frmDissolve_Load(object sender, EventArgs e)
        {
            this.MapLayer = new Dictionary<int, ILayer>();
            for (int i = 0; i < this.m_axMapControl.LayerCount; i++)
            {
                ILayer layer = this.m_axMapControl.get_Layer(i);
                IFeatureLayer layer2 = layer as IFeatureLayer;
                if ((((layer != null) && layer.Valid) && (layer is IFeatureLayer)))
                {
                    this.comboxlayer.Items.Add(layer.Name);
                    this.MapLayer.Add(this.comboxlayer.Items.Count - 1, layer);
                }
            }
            if (this.comboxlayer.Items.Count > 0)
            {
                this.comboxlayer.SelectedIndex = 0;
            }
             
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            if (this.CheckingInput())
            {
                Application.DoEvents();
                this.comboxlayer.Enabled = false;
                this.comboxFiled.Enabled = false;
                this.textBox1.Enabled = false;
                this.button1.Enabled = false;
                this.butOk.Enabled = false;
                this.progressBar1.Visible = true;
                this.progressBar1.Minimum = 0;
                this.progressBar1.Maximum = 100;

                ILayer layer = this.MapLayer[this.comboxlayer.SelectedIndex];
                IFeatureClass pInputFeatureClass = (layer as IFeatureLayer).FeatureClass;



                try
               {
	                ESRI.ArcGIS.Geoprocessor.Geoprocessor gp = new ESRI.ArcGIS.Geoprocessor.Geoprocessor();
	                ESRI.ArcGIS.DataManagementTools.Dissolve pDiss = new ESRI.ArcGIS.DataManagementTools.Dissolve();
	                gp.OverwriteOutput = true;
	                pDiss.in_features = LayerToShapefilePath(layer);
	                pDiss.dissolve_field = comboxFiled.Text;
	                pDiss.out_feature_class = textBox1.Text +"\\"+ textBox2.Text;
	                if (checkBox1.Checked)
	                {
	                    pDiss.multi_part = "true";
	
	                }
	                else
	                {
	                    pDiss.multi_part = "false";
	
	                }
	
	                /////
	                for (int i = 0; i < 20; i++)
	                {
	                    this.progressBar1.Value++;
	                }
	                /////////////
	                ESRI.ArcGIS.Geoprocessing.IGeoProcessorResult2 result = (ESRI.ArcGIS.Geoprocessing.IGeoProcessorResult2)gp.Execute(pDiss, null);
	                /////
	                for (int i = 0; i < 20; i++)
	                {
	                    this.progressBar1.Value++;
	                }
	                /////////////
	                if (result.Status != ESRI.ArcGIS.esriSystem.esriJobStatus.esriJobSucceeded)
	                {
	                    MessageBox.Show("操作失败！");
	                    this.Close();
	                }
	                else
	                {
	
	                    /////
	                    for (int i = 0; i < 20; i++)
	                    {
	                        this.progressBar1.Value++;
	                    }
	                    /////////////
	                    //获取结果
	                    IFeatureClass resultFClass = gp.Open(result.ReturnValue) as IFeatureClass;
	                    IFeatureLayer pLayer = new FeatureLayerClass
	                    {
	                        FeatureClass = resultFClass,
	                        Name = resultFClass.AliasName
	                    };
	                    /////
	                    for (int i = 0; i < 20; i++)
	                    {
	                        this.progressBar1.Value++;
	                    }
	                    /////////////
	                    this.m_axMapControl.AddLayer(pLayer);
	                    this.m_axMapControl.ActiveView.Refresh();
	                    MessageBox.Show(pLayer.Name + "处理完成！");
	                    base.Close();
	                }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("操作失败！");
                    this.Close();
                }
                
            }
        }
        /// <summary>
        /// 从图层中获取,要素的路径
        /// </summary>
        /// <param name="pLayer"></param>
        /// <returns></returns>
        public string LayerToShapefilePath(ILayer pLayer)
        {


            string filename = "";

            IDataLayer2 pDLayer = (IDataLayer2)pLayer;
            IDatasetName pDsName = (IDatasetName)(pDLayer.DataSourceName);
            string dsname = pDsName.Name;
            IWorkspaceName ws = pDsName.WorkspaceName;

            //filename = ws.PathName+"\\"+dsname+".shp";
            filename = ws.PathName + "\\" + dsname;
            return filename;
        }
        private void comboxlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboxFiled.Items.Count > 0)
            {
                comboxFiled.Items.Clear();
            }
            ILayer layer = this.MapLayer[this.comboxlayer.SelectedIndex];
            IFeatureClass featureClass = (layer as IFeatureLayer).FeatureClass;
            IFields pFields = featureClass.Fields;
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                comboxFiled.Items.Add(pFields.get_Field(i).Name);

            }
            comboxFiled.Text = "";
            this.textBox2.Text = comboxlayer.Text + "_diss";
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboxFiled_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

         

       

    }
}
