namespace AE_Environment.Forms
{
    
    using ESRI.ArcGIS.Carto;
    using ESRI.ArcGIS.Controls;
    using ESRI.ArcGIS.Geodatabase;
    using ESRI.ArcGIS.Geometry;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.IO;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
    using System.Runtime.InteropServices;

    public class frmIntersect : Form
    {
        private Button butcancel;
        private Button butOk;
        private Button button1;
        private ComboBox comboxlayer1;
        private ComboBox comboxlayer2;
        private IContainer components = null;
        private FolderBrowserDialog folderBrowserDialog1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private AxMapControl m_axMapControl;
        private Dictionary<int, ILayer> MapLayer = null;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private Panel panel6;
        private ProgressBar progressBar1;
        private TextBox textBox1;
        private TextBox textBox2;

        public frmIntersect(AxMapControl axMapControl)
        {
            this.InitializeComponent();
            this.m_axMapControl = axMapControl;
        }

        private void butcancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            if (this.CheckingInput())
            {
                Application.DoEvents();
                this.comboxlayer1.Enabled = false;
                this.comboxlayer2.Enabled = false;
                this.textBox1.Enabled = false;
                this.button1.Enabled = false;
                this.butOk.Enabled = false;
                this.progressBar1.Visible = true;
                this.progressBar1.Minimum = 0;
                this.progressBar1.Maximum = 100;
          
                IFeatureLayer  layer1 = this.MapLayer[this.comboxlayer1.SelectedIndex] as IFeatureLayer;
                IFeatureLayer  layer2 = this.MapLayer[this.comboxlayer2.SelectedIndex] as IFeatureLayer;
                try
                {
	                //调用GP
	
	            ESRI.ArcGIS.Geoprocessor.Geoprocessor gp = new ESRI.ArcGIS.Geoprocessor.Geoprocessor();
	            ESRI.ArcGIS.AnalysisTools.Intersect pIntersect = new ESRI.ArcGIS.AnalysisTools.Intersect();
	            gp.OverwriteOutput = true;
	            pIntersect.in_features=LayerToShapefilePath(layer1 as ILayer)+";"+LayerToShapefilePath(layer2 as ILayer);
	            pIntersect.out_feature_class = System.Environment.CurrentDirectory + @"\temp\"+"temp.shp";
	            pIntersect.output_type="Input";
	            /////
	            for (int i=0;i<20;i++)
	            {
	                this.progressBar1.Value++;
	            }
	            /////////////
	            ESRI.ArcGIS.Geoprocessing.IGeoProcessorResult2 result=(ESRI.ArcGIS.Geoprocessing.IGeoProcessorResult2) gp.Execute(pIntersect,null);
	            /////
	            for (int i=0;i<20;i++)
	            {
	                this.progressBar1.Value++;
	            }
	            /////////////
	            if (result.Status!=ESRI.ArcGIS.esriSystem.esriJobStatus.esriJobSucceeded)
	            {
	                MessageBox.Show("操作失败！");
	                this.Close();
	            }
	            else
	            {
	                //获取结果
	                IFeatureClass resultFClass = gp.Open(result.ReturnValue) as IFeatureClass;
	                IDataset pDataset = resultFClass as IDataset;
	                    //目标
	                    IWorkspaceFactory factory = new FileGDBWorkspaceFactoryClass();
	                    IWorkspace objectWorkspace = factory.OpenFromFile(textBox1.Text.Trim(), 0);
	                    ////////////////////////////
	                for (int i=0;i<20;i++)
	                {
	                    this.progressBar1.Value++;
	                }
	                /////////////////////////////////////////
	                    IFeatureClass pOutFeatureClass = IFeatureDataConverter_ConvertFeatureClass(pDataset.Workspace, objectWorkspace, pDataset.Name, textBox2.Text.Trim());
	                    //////////////////////////////////////////////////
	                for (int i=0;i<20;i++)
	                {
	                    this.progressBar1.Value++;
	                }
	                ///////////////////////////////////////////////////
	                    IFeatureLayer pLayer = new FeatureLayerClass
	                    {
	                        FeatureClass = pOutFeatureClass,
	                        Name = pOutFeatureClass.AliasName
	                    };
	                     
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
        /// 数据转换,从一个位置到另一个位置
        /// </summary>
        /// <param name="sourceWorkspace"></param>
        /// <param name="targetWorkspace"></param>
        /// <param name="nameOfSourceFeatureClass"></param>
        /// <param name="nameOfTargetFeatureClass"></param>
        /// <returns></returns>
        public IFeatureClass IFeatureDataConverter_ConvertFeatureClass(IWorkspace sourceWorkspace, IWorkspace targetWorkspace, string nameOfSourceFeatureClass, string nameOfTargetFeatureClass)
        {
            //create source workspace name   
            IDataset sourceWorkspaceDataset = (IDataset)sourceWorkspace;
            IWorkspaceName sourceWorkspaceName = (IWorkspaceName)sourceWorkspaceDataset.FullName;
            //create source dataset name   
            IFeatureClassName sourceFeatureClassName = new FeatureClassNameClass();
            IDatasetName sourceDatasetName = (IDatasetName)sourceFeatureClassName;
            sourceDatasetName.WorkspaceName = sourceWorkspaceName;
            sourceDatasetName.Name = nameOfSourceFeatureClass;
            //create target workspace name   
            IDataset targetWorkspaceDataset = (IDataset)targetWorkspace;
            IWorkspaceName targetWorkspaceName = (IWorkspaceName)targetWorkspaceDataset.FullName;

            //检查名称冲突
            string temp = nameOfTargetFeatureClass;
            for (int i = 1; this.isExitFeatureClass(targetWorkspace,temp); i++)
            {
                temp = nameOfSourceFeatureClass + "_" + i.ToString();
                
            }
            //create target dataset name  
            //

            IFeatureClassName targetFeatureClassName = new FeatureClassNameClass();
            IDatasetName targetDatasetName = (IDatasetName)targetFeatureClassName;
            targetDatasetName.WorkspaceName = targetWorkspaceName;
            targetDatasetName.Name =temp;

            //Open input Featureclass to get field definitions.   
            ESRI.ArcGIS.esriSystem.IName sourceName = (ESRI.ArcGIS.esriSystem.IName)sourceFeatureClassName;
            IFeatureClass sourceFeatureClass = (IFeatureClass)sourceName.Open();

            //Validate the field names because you are converting between different workspace types.   
            IFieldChecker fieldChecker = new FieldCheckerClass();
            IFields targetFeatureClassFields;
            IFields sourceFeatureClassFields = sourceFeatureClass.Fields;
            IEnumFieldError enumFieldError;
            // Most importantly set the input and validate workspaces!     
            fieldChecker.InputWorkspace = sourceWorkspace;
            fieldChecker.ValidateWorkspace = targetWorkspace;
            fieldChecker.Validate(sourceFeatureClassFields, out enumFieldError, out targetFeatureClassFields);
            // Loop through the output fields to find the geomerty field   
            IField geometryField;
            for (int i = 0; i < targetFeatureClassFields.FieldCount; i++)
            {
                if (targetFeatureClassFields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                {
                    geometryField = targetFeatureClassFields.get_Field(i);
                    // Get the geometry field's geometry defenition            
                    IGeometryDef geometryDef = geometryField.GeometryDef;
                    //Give the geometry definition a spatial index grid count and grid size        
                    IGeometryDefEdit targetFCGeoDefEdit = (IGeometryDefEdit)geometryDef;
                    targetFCGeoDefEdit.GridCount_2 = 1;
                    targetFCGeoDefEdit.set_GridSize(0, 0);
                    //Allow ArcGIS to determine a valid grid size for the data loaded      
                    targetFCGeoDefEdit.SpatialReference_2 = geometryField.GeometryDef.SpatialReference;
                    // we want to convert all of the features   
                    IQueryFilter queryFilter = new QueryFilterClass();
                    queryFilter.WhereClause = "";
                    // Load the feature class     
                    IFeatureDataConverter fctofc = new FeatureDataConverterClass();
                    IEnumInvalidObject enumErrors = fctofc.ConvertFeatureClass(sourceFeatureClassName, queryFilter, null, targetFeatureClassName, geometryDef, targetFeatureClassFields, "", 1000, 0);
                    break;
                }
            }

            //Open input Featureclass to get field definitions.   
            ESRI.ArcGIS.esriSystem.IName targetName = (ESRI.ArcGIS.esriSystem.IName)targetFeatureClassName;
            return (IFeatureClass)targetName.Open();

        }
        /// <summary>
        /// 判断工作空间中是否含有某个要素类
        /// </summary>
        /// <param name="pW"></param>
        /// <param name="pFeatureClassName"></param>
        /// <returns></returns>
        private bool isExitFeatureClass(IWorkspace pW, string pFeatureClassName)
        {
            IEnumDataset o = pW.get_Datasets(esriDatasetType.esriDTAny);
            o.Reset();
            for (IDataset dataset2 = o.Next(); dataset2 != null; dataset2 = o.Next())
            {
                if (dataset2.Name == pFeatureClassName)
                {
                    return true;
                }
                IEnumDataset subsets = dataset2.Subsets;
                for (IDataset dataset4 = subsets.Next(); dataset4 != null; dataset4 = subsets.Next())
                {
                    IFeatureClass class2 = dataset4 as IFeatureClass;
                    if (((class2 != null) && (class2.FeatureType != esriFeatureType.esriFTAnnotation)) && (class2.AliasName == pFeatureClassName))
                    {
                        return true;
                    }
                }
                if (subsets != null)
                {
                    Marshal.ReleaseComObject(subsets);
                }
            }
            if (o != null)
            {
                Marshal.ReleaseComObject(o);
            }
            return false;
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
        /// <summary>
        /// 求交,暂时未用
        /// </summary>
        /// <param name="_pFtClass"></param>
        /// <param name="_pFtOverlay"></param>
        /// <param name="_FilePath"></param>
        /// <param name="_pFileName"></param>
        /// <returns></returns>
        public IFeatureClass Intsect(IFeatureClass _pFtClass,IFeatureClass _pFtOverlay,string _FilePath,string _pFileName)
         {
            //设置输出
            IFeatureClassName pOutPut = new FeatureClassNameClass();
            pOutPut.ShapeType = _pFtClass.ShapeType;
            pOutPut.ShapeFieldName = _pFtClass.ShapeFieldName;
            pOutPut.FeatureType = esriFeatureType.esriFTSimple;
            //set output location and feature class name
            IWorkspaceName pWsN = new WorkspaceNameClass();
            pWsN.WorkspaceFactoryProgID = "esriDataSourcesFile.ShapefileWorkspaceFactory";
            pWsN.PathName = _FilePath;
            //也可以用这种方法，IName 和IDataset的用法
//             IWorkspaceFactory pWsFc = new ShapefileWorkspaceFactoryClass();
//             IWorkspace pWs = pWsFc.OpenFromFile(_FilePath,0);
//             IDataset pDataset = pWs as IDataset;
//             IWorkspaceName pWsN = pDataset.FullName as IWorkspaceName;
            
            IDatasetName pDatasetName = pOutPut as IDatasetName;
            pDatasetName.Name = _pFileName;
            pDatasetName.WorkspaceName =pWsN;
            
            IBasicGeoprocessor pBasicGeo = new BasicGeoprocessorClass();
            IFeatureClass pFeatureClass = pBasicGeo.Intersect(_pFtClass as ITable,false,_pFtOverlay as ITable,false,0.1,pOutPut);
             return pFeatureClass;
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// 数据检查
        /// </summary>
        /// <returns></returns>
        private bool CheckingInput()
        {
            if (this.comboxlayer1.SelectedItem == null)
            {
                MessageBox.Show("请选择输入图层1.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (this.comboxlayer2.SelectedItem == null)
            {
                MessageBox.Show("请选择输入图层2.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (this.textBox1.Text == "")
            {
                MessageBox.Show("请选择输出路径.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmIntersect_Load(object sender, EventArgs e)
        {
            this.MapLayer = new Dictionary<int, ILayer>();
            for (int i = 0; i < this.m_axMapControl.LayerCount; i++)
            {
                ILayer layer = this.m_axMapControl.get_Layer(i);
                IFeatureLayer layer2 = layer as IFeatureLayer;
                if ((((layer != null) && layer.Valid) && (layer is IFeatureLayer)) && (layer2.FeatureClass.ShapeType != esriGeometryType.esriGeometryPoint))
                {
                    this.comboxlayer1.Items.Add(layer.Name);
                    this.comboxlayer2.Items.Add(layer.Name);
                    this.MapLayer.Add(this.comboxlayer1.Items.Count - 1, layer);
                }
            }
            if (this.comboxlayer1.Items.Count > 0)
            {
                this.comboxlayer1.SelectedIndex = 0;
            }
         
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIntersect));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.butOk = new System.Windows.Forms.Button();
            this.butcancel = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.comboxlayer2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboxlayer1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(546, 175);
            this.panel1.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.textBox2);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 101);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(5);
            this.panel6.Size = new System.Drawing.Size(546, 30);
            this.panel6.TabIndex = 6;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(125, 5);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(321, 21);
            this.textBox2.TabIndex = 5;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(5, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "输出要素名称：";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.progressBar1);
            this.panel5.Controls.Add(this.butOk);
            this.panel5.Controls.Add(this.butcancel);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 139);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(5);
            this.panel5.Size = new System.Drawing.Size(546, 36);
            this.panel5.TabIndex = 3;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(5, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(386, 26);
            this.progressBar1.TabIndex = 8;
            // 
            // butOk
            // 
            this.butOk.Dock = System.Windows.Forms.DockStyle.Right;
            this.butOk.Location = new System.Drawing.Point(391, 5);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(75, 26);
            this.butOk.TabIndex = 5;
            this.butOk.Text = "确定";
            this.butOk.UseVisualStyleBackColor = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // butcancel
            // 
            this.butcancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.butcancel.Location = new System.Drawing.Point(466, 5);
            this.butcancel.Name = "butcancel";
            this.butcancel.Size = new System.Drawing.Size(75, 26);
            this.butcancel.TabIndex = 6;
            this.butcancel.Text = "取消";
            this.butcancel.UseVisualStyleBackColor = true;
            this.butcancel.Click += new System.EventHandler(this.butcancel_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.textBox1);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 65);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(5);
            this.panel4.Size = new System.Drawing.Size(546, 36);
            this.panel4.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(125, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(321, 21);
            this.textBox1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(5, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "输出位置： ";
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Location = new System.Drawing.Point(466, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 26);
            this.button1.TabIndex = 4;
            this.button1.Text = "浏览";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.comboxlayer2);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 31);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(546, 34);
            this.panel3.TabIndex = 1;
            // 
            // comboxlayer2
            // 
            this.comboxlayer2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboxlayer2.FormattingEnabled = true;
            this.comboxlayer2.Location = new System.Drawing.Point(125, 5);
            this.comboxlayer2.Name = "comboxlayer2";
            this.comboxlayer2.Size = new System.Drawing.Size(321, 20);
            this.comboxlayer2.TabIndex = 2;
            this.comboxlayer2.SelectedIndexChanged += new System.EventHandler(this.comboxlayer2_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(5, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "输入图层2：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.comboxlayer1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(546, 31);
            this.panel2.TabIndex = 0;
            // 
            // comboxlayer1
            // 
            this.comboxlayer1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboxlayer1.FormattingEnabled = true;
            this.comboxlayer1.Location = new System.Drawing.Point(125, 5);
            this.comboxlayer1.Name = "comboxlayer1";
            this.comboxlayer1.Size = new System.Drawing.Size(321, 20);
            this.comboxlayer1.TabIndex = 1;
            this.comboxlayer1.SelectedIndexChanged += new System.EventHandler(this.comboxlayer1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入图层1：";
            // 
            // frmIntersect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 175);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmIntersect";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "叠加分析";
            this.Load += new System.EventHandler(this.frmIntersect_Load);
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboxlayer2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox2.Text = comboxlayer1.Text +"_" +comboxlayer2.Text+"_In";
        }

        private void comboxlayer1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.textBox2.Text = comboxlayer1.Text + "_" + comboxlayer2.Text+"_In";
        }
    }
}

