using BaseGIS;
using BaseGIS.GISForm;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geoprocessing;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.DataSourcesFile;

namespace AE_Environment.Forms
{
    public partial class PopulationSpatialFrm : Form
    {
        private IMapControl3 mMapControl;
        private DataTable dt_Result = new DataTable();
        private DataTable dt_Population = new DataTable();

        private List<string> ZoneID = new List<string>();
        private List<double> ZoneBuildArea = new List<double>();
        private List<double> ZoneBuildDensity = new List<double>();

        private List<string> GridID = new List<string>();
        private List<double> GridPopu = new List<double>();
        
       

        public PopulationSpatialFrm(AxMapControl _axMapControl)
        {
            InitializeComponent();
            mMapControl = (IMapControl3)_axMapControl.Object;
        }

        private void PopulationSpatialFrm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < mMapControl.LayerCount; i++)
            {
                ILayer layer = mMapControl.get_Layer(i);
                IFeatureLayer layer2 = layer as IFeatureLayer;
                if ((((layer != null) && layer.Valid) && (layer is IFeatureLayer)) && (layer2.FeatureClass.ShapeType != esriGeometryType.esriGeometryPoint))
                {
                    cbox_Building_Layer.Items.Add(layer.Name);
                    cbox_Boundary_Layer.Items.Add(layer.Name);
                    cbox_Grid_Layer.Items.Add(layer.Name);
                }
            }
        }

         private void bt_Run_Click(object sender, EventArgs e)
        {
            #region
            //if (CheckInputData())
            //{
            //    #region 获取变量
            //    IFeatureLayer pFLayer_Build = mMapControl.get_Layer(cbox_Building_Layer.SelectedIndex) as IFeatureLayer;
            //    IFeatureLayer pFLayer_Bound = mMapControl.get_Layer(cbox_Boundary_Layer.SelectedIndex) as IFeatureLayer;
            //    IFeatureLayer pFLayer_Grid = mMapControl.get_Layer(cbox_Grid_Layer.SelectedIndex) as IFeatureLayer;

            //    string gridIDField = cbox_Grid_Field.SelectedItem.ToString();
            //    #endregion
            //    SetControlsEnable(false);
            //    #region 创建文件数据库，进行房屋建筑数据与行政边界数据的相交
            //    lab_Message.Text = "1、创建临时数据库文件••••";
            //    string gdbPath = CreateFileGDB();
            //    if (gdbPath == "")
            //    {
            //        return;
            //    }
            //    lab_Message.Text = "2、进行建筑数据和边界数据相交••••";
            //    IFeatureClass Build_Bound_In = Intersect(pFLayer_Build.FeatureClass, pFLayer_Bound.FeatureClass, gdbPath);
            //    if (Build_Bound_In == null)
            //    {
            //        return;
            //    }
            //    #endregion

            //    #region 对相交后的数据做融合
            //    lab_Message.Text = "3、相交完成，进行数据融合••••";
            //    IFeatureClass Build_Bound_Dis = Dissolve(Build_Bound_In, cbox_Zone_Field.SelectedItem.ToString(), gdbPath, "Build_Bound_Dis");
            //    if (Build_Bound_Dis == null)
            //    {
            //        return;
            //    }
            //    lab_Message.Text = "4、融合完成，计算人口密度••••";
            //    #endregion

            //    #region 计算人口密度
            //    if (dt_Population == null || Build_Bound_Dis == null)
            //    {
            //        return;
            //    }

            //    //人口数据表中的分区编码字段;
            //    int zoneIndex = dt_Population.Columns.IndexOf(cbox_Zone_Field.SelectedItem.ToString());
            //    //人口数据表中的人口数字段;
            //    int popuIndex = dt_Population.Columns.IndexOf(cbox_Pop_Field.SelectedItem.ToString());
            //    //获取相交要素中的分区编码字段;
            //    int zoneId = Build_Bound_Dis.FindField(cbox_Zone_Field.SelectedItem.ToString());
            //    IFeatureClass Build_Bound_PopDensity = AddDensityField(Build_Bound_Dis, zoneId, zoneIndex, popuIndex);
            //    lab_Message.Text = "5、融合完成，进行与格网数据相交••••";
            //    #endregion

            //    #region 计算格网人口
            //    IFeatureClass Build_Bound_Grid = Intersect(Build_Bound_PopDensity, pFLayer_Grid.FeatureClass, gdbPath);
            //    lab_Message.Text = "6、相交完毕，进行数据输出••••";
            //    if (GridPopulation(Build_Bound_Grid, gridIDField))
            //    {
            //        //边界数据和格网数据相交;
            //        IFeatureClass Bound_Grid_In = Intersect(pFLayer_Grid.FeatureClass, pFLayer_Bound.FeatureClass, gdbPath);
            //        IFeatureClass Bound_Grid_Dis = Dissolve(Bound_Grid_In, gridIDField, gdbPath, "GridPop");
            //        string outPutPath = tbox_OutPutPath.Text;
            //        string outName = tbox_Name.Text;
            //        OutPutData(Bound_Grid_Dis, gridIDField, outPutPath, outName);
            //    }
            //    #endregion
            //    SetControlsEnable(true);
            //    lab_Message.Text = "7、计算完毕，数据已保存••••";
            //    MessageBox.Show("计算完成");
            //    this.Close();
            //}
            //else
            //{
            //    MessageBox.Show("请检查数据输入", "提示");
            //}
            #endregion

            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            progressBar1.Maximum = 200;
            if (CheckInputData())
            {
                #region 获取变量
                IFeatureLayer pFLayer_Build = mMapControl.get_Layer(cbox_Building_Layer.SelectedIndex) as IFeatureLayer;
                IFeatureLayer pFLayer_Bound = mMapControl.get_Layer(cbox_Boundary_Layer.SelectedIndex) as IFeatureLayer;
                IFeatureLayer pFLayer_Grid = mMapControl.get_Layer(cbox_Grid_Layer.SelectedIndex) as IFeatureLayer;

                string gridIDField = cbox_Grid_Field.SelectedItem.ToString();
                string zoneIDField = cbox_Zone_Field.SelectedItem.ToString();
                string popuField = cbox_Pop_Field.SelectedItem.ToString();

                string outPutPath = tbox_OutPutPath.Text;
                string outName = tbox_Name.Text;

                //获取建筑密度和建筑高度字段;
                string bd_Field = "";
                string bh_Field = "";
                if (cbox_Density.SelectedItem != null)
                {
                    bd_Field = cbox_Density.SelectedItem.ToString();
                }
                if (cbox_Height.SelectedItem != null)
                {
                    bh_Field = cbox_Height.SelectedItem.ToString();
                }
                #endregion
                SetControlsEnable(false);
                #region 创建文件数据库
                lab_Message.Text = "1、创建临时数据库文件••••";
                string gdbPath = CreateFileGDB();
                for (int i = 0; i < 25;i ++ )
                {
                    progressBar1.Value++;
                }
                #endregion
                #region 进行房屋建筑数据与行政边界数据的相交
                lab_Message.Text = "2、进行建筑数据和行政边界数据相交••••";
                if (gdbPath == "")
                {
                    return;
                }
                IFeatureClass Build_Bound_In = Intersect(pFLayer_Build.FeatureClass, pFLayer_Bound.FeatureClass, gdbPath);
                for (int i = 0; i < 25; i++)
                {
                    progressBar1.Value++;
                }
                #endregion
                #region 计算分区人口密度
                lab_Message.Text = "3、统计分区建筑面积••••";
                if (GetZoneBuildArea(Build_Bound_In,zoneIDField,bd_Field,bh_Field))
                {
                    for (int i = 0; i < 25; i++)
                    {
                        progressBar1.Value++;
                    }
                    lab_Message.Text = "4、计算分区人口密度••••";
                    GetZoneBuildDensity(dt_Population, zoneIDField, popuField);
                    for (int i = 0; i < 25; i++)
                    {
                        progressBar1.Value++;
                    }
                }
                #endregion
                #region Build_Bound_In与格网数据相交
                lab_Message.Text = "5、建筑、边界、格网数据相交••••";
                IFeatureClass Build_Bound_Grid = Intersect(Build_Bound_In, pFLayer_Grid.FeatureClass, gdbPath);
                for (int i = 0; i < 25; i++)
                {
                    progressBar1.Value++;
                }
                #endregion

                #region 计算每个格网的格网编码和对应人口数
                lab_Message.Text = "6、计算每个格网的人口数••••";
                if (GetGridPopulation(Build_Bound_Grid,zoneIDField,gridIDField,bd_Field,bh_Field))
                {
                    for (int i = 0; i < 25; i++)
                    {
                        progressBar1.Value++;
                    }
                    
                    //边界数据和格网数据相交;
                    lab_Message.Text = "7、生成输出数据••••";
                    IFeatureClass Bound_Grid_In = Intersect(pFLayer_Grid.FeatureClass, pFLayer_Bound.FeatureClass, gdbPath);
                    IFeatureClass Bound_Grid_Dis = Dissolve(Bound_Grid_In, gridIDField, gdbPath, "GridPop");
                    for (int i = 0; i < 25; i++)
                    {
                        progressBar1.Value++;
                    }
                    
                    lab_Message.Text = "8、数据输出••••";
                    OutputGridPopulation(Bound_Grid_Dis,gridIDField,outPutPath,outName);
                    for (int i = 0; i < 25; i++)
                    {
                        progressBar1.Value++;
                    }
                }
                #endregion
                lab_Message.Text = "计算完成••••";
                MessageBox.Show("计算完成");
                this.Close();
            }
             else
             {
                 MessageBox.Show("请检查数据输入", "提示");
             }
         }

         private void bt_Cancel_Click(object sender, EventArgs e)
         {
             this.Close();
         }

         private void bt_LoadPopData_Click(object sender, EventArgs e)
         {
             #region 加载人口数据，并生成数据表
             string filePath = "";
             OpenFileDialog ofd = new OpenFileDialog();
             ofd.Filter = "*.xls|*.XLS";
             DialogResult result = ofd.ShowDialog();
             //如果用户选择"确定"
             if (result == DialogResult.OK)
             {
                 filePath = ofd.FileName;
             }
             if (filePath == "")
             {
                 MessageBox.Show("请选择正确的路径");
                 return;
             }
             tbox_Population.Text = filePath;
             dt_Population = ToDataTable(filePath).Tables[0];
             #endregion


             #region 添加人口数据表的字段名称
             cbox_Pop_Field.Items.Clear();
             for (int i = 0; i < dt_Population.Columns.Count; i++)
             {
                 cbox_Pop_Field.Items.Add(dt_Population.Columns[i].ColumnName);
             }
             #endregion
         }

         private void cbox_Boundary_Layer_SelectedIndexChanged(object sender, EventArgs e)
         {
             cbox_Zone_Field.Items.Clear();
             IFeatureLayer pLayer = mMapControl.get_Layer(cbox_Boundary_Layer.SelectedIndex) as IFeatureLayer;
             for (int i = 0; i < pLayer.FeatureClass.Fields.FieldCount; i++)
             {
                 cbox_Zone_Field.Items.Add(pLayer.FeatureClass.Fields.get_Field(i).Name);
             }
         }

         private void cbox_Grid_Layer_SelectedIndexChanged(object sender, EventArgs e)
         {
             cbox_Grid_Field.Items.Clear();
             IFeatureLayer pLayer = mMapControl.get_Layer(cbox_Grid_Layer.SelectedIndex) as IFeatureLayer;
             for (int i = 0; i < pLayer.FeatureClass.Fields.FieldCount; i++)
             {
                 cbox_Grid_Field.Items.Add(pLayer.FeatureClass.Fields.get_Field(i).Name);
             }
         }

         private void bt_AddPath_Click(object sender, EventArgs e)
         {
             FolderBrowserDialog fbd = new FolderBrowserDialog();
             fbd.Description = "保存叠加数据";
             fbd.ShowNewFolderButton = false;
             fbd.ShowDialog();
             string selectedPath = fbd.SelectedPath;
             if (selectedPath == "")
             { 
                 MessageBox.Show("请选择保存路径！");
             }
             else
             {
                 tbox_OutPutPath.Text = selectedPath;
             }
         }

         private void PopulationSpatialFrm_FormClosed(object sender, FormClosedEventArgs e)
         {
             //string fullPath = System.Environment.CurrentDirectory + @"\temp\temp.gdb";
             //if (Directory.Exists(fullPath))
             //{
             //    DirectoryInfo di = new DirectoryInfo(fullPath);
             //    di.Delete(true);
             //}
             //this.Dispose();

         }

         private void cbox_Optional_CheckedChanged(object sender, EventArgs e)
         {
             if (cbox_Building_Layer.SelectedItem == null)
             {
                 MessageBox.Show("请选择房屋建筑图层！");
                 return;
             }
             if (cbox_Optional.Checked)
             {
                 cbox_Density.Enabled = true;
                 cbox_Height.Enabled = true;

                 cbox_Density.Items.Clear();
                 cbox_Height.Items.Clear();
                 if (cbox_Optional.Checked)
                 {
                     IFeatureLayer pLayer = mMapControl.get_Layer(cbox_Building_Layer.SelectedIndex) as IFeatureLayer;
                     for (int i = 0; i < pLayer.FeatureClass.Fields.FieldCount; i++)
                     {
                         cbox_Density.Items.Add(pLayer.FeatureClass.Fields.get_Field(i).Name);
                         cbox_Height.Items.Add(pLayer.FeatureClass.Fields.get_Field(i).Name);
                     }
                 }
             }
             else
             {
                 cbox_Density.Enabled = false;
                 cbox_Height.Enabled = false;

                 cbox_Density.Items.Clear();
                 cbox_Height.Items.Clear();
             }
         }


         #region 将窗体中的控件设置为不可操作状态
         private void SetControlsEnable(bool b)
         {
             for (int i = 0; i < groupBox3.Controls.Count; i++)
             {
                 groupBox3.Controls[i].Enabled = b;
             }
             for (int i = 0; i < groupBox4.Controls.Count; i++)
             {
                 groupBox4.Controls[i].Enabled = b;
             }
             for (int i = 0; i < groupBox5.Controls.Count; i++)
             {
                 groupBox5.Controls[i].Enabled = b;
             }
             for (int i = 0; i < groupBox1.Controls.Count; i++)
             {
                 groupBox1.Controls[i].Enabled = b;
             }
             bt_Run.Enabled = b;
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

         #region 检查数据输入
         private bool CheckInputData()
         {
             bool isOK = true;
             #region 检查图层输入
             if (cbox_Building_Layer.SelectedIndex < 0)
             {
                 isOK = false;
             }
             if (cbox_Boundary_Layer.SelectedIndex < 0)
             {
                 isOK = false;
             }
             if (cbox_Grid_Field.SelectedIndex < 0)
             {
                 isOK = false;
             }
             if (tbox_Population.Text == "")
             {
                 isOK = false;
             }
             if (tbox_OutPutPath.Text == "")
             {
                 isOK = false;
             }
             if (tbox_Name.Text == "")
             {
                 isOK = false;
             }
             #endregion

             #region 检查字段选择
             if (cbox_Zone_Field.SelectedIndex < 0)
             {
                 isOK = false;
             }
             if (cbox_Pop_Field.SelectedIndex < 0)
             {
                 isOK = false;
             }
             if (cbox_Grid_Field.SelectedIndex < 0)
             {
                 isOK = false;
             }
             #endregion
             return isOK;
         }
         #endregion

         #region 创建文件数据库
         private string CreateFileGDB()
         {
             string filePath = System.Environment.CurrentDirectory + @"\temp\";
             string fileName = "temp";
             string fullPath = filePath + fileName + ".gdb";

             while (Directory.Exists(fullPath))
             {
                 fileName = fileName + "1";
                 fullPath = filePath + fileName + ".gdb";
             }
             if (!Directory.Exists(filePath))
             {
                 filePath = System.Environment.CurrentDirectory + @"\temp\";
                 DirectoryInfo di = new DirectoryInfo(filePath);
                 di.Create();
             }
             IWorkspaceFactory2 wsFctry = new FileGDBWorkspaceFactoryClass();
             wsFctry.Create(System.IO.Path.GetDirectoryName(filePath), fileName, null, 0);
             wsFctry = null;
             return fullPath;
         }
         #endregion

         #region 数据转换,从一个位置到另一个位置
         private IFeatureClass IFeatureDataConverter_ConvertFeatureClass(IWorkspace sourceWorkspace, IWorkspace targetWorkspace, string nameOfSourceFeatureClass, string nameOfTargetFeatureClass)
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
             for (int i = 1; this.isExitFeatureClass(targetWorkspace, temp); i++)
             {
                 temp = nameOfSourceFeatureClass + "_" + i.ToString();

             }
             //create target dataset name  
             //

             IFeatureClassName targetFeatureClassName = new FeatureClassNameClass();
             IDatasetName targetDatasetName = (IDatasetName)targetFeatureClassName;
             targetDatasetName.WorkspaceName = targetWorkspaceName;
             targetDatasetName.Name = temp;

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
         #endregion

         #region 判断工作空间中是否含有某个要素类
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
         #endregion

         #region 要素相交
         /// <param name="pFeatureClass1">相交要素1</param>
         /// <param name="pFeatureClass2">相交要素2</param>
         /// <param name="gdbPath">数据库路径</param>
         private IFeatureClass Intersect(IFeatureClass pFeatureClass1, IFeatureClass pFeatureClass2, string gdbPath)
         {
             IFeatureClass pOutFeatureClass = null;

             try
             {
                 //调用GP

                 ESRI.ArcGIS.Geoprocessor.Geoprocessor gp = new ESRI.ArcGIS.Geoprocessor.Geoprocessor();
                 gp.OverwriteOutput = true;

                 //多个对象的输入：使用IGpValueTableObject接口，该接口可以设置
                 IGpValueTableObject vtobject = new GpValueTableObjectClass();
                 object pFeature1 = pFeatureClass1;
                 object pFeature2 = pFeatureClass2;
                 vtobject.SetColumns(2);
                 vtobject.AddRow(ref pFeature1);
                 vtobject.AddRow(ref pFeature2);

                 ESRI.ArcGIS.AnalysisTools.Intersect pIntersect = new ESRI.ArcGIS.AnalysisTools.Intersect();
                 pIntersect.in_features = vtobject;
                 pIntersect.out_feature_class = System.Environment.CurrentDirectory + @"\temp\" + "temp.shp";
                 pIntersect.output_type = "Input";
                
                 IGeoProcessorResult2 result = (IGeoProcessorResult2)gp.Execute(pIntersect, null);
                
                 if (result.Status != ESRI.ArcGIS.esriSystem.esriJobStatus.esriJobSucceeded)
                 {
                     MessageBox.Show("操作失败！");
                     return null;
                 }
                 else
                 {
                     //获取结果;
                     IFeatureClass resultFClass = gp.Open(result.ReturnValue) as IFeatureClass;
                     IDataset pDataset = resultFClass as IDataset;
                     //目标数据库;
                     IWorkspaceFactory factory = new FileGDBWorkspaceFactoryClass();
                     IWorkspace objectWorkspace = factory.OpenFromFile(gdbPath, 0);

                     string fileName = pFeatureClass1.AliasName + "_" + pFeatureClass2.AliasName;
                     pOutFeatureClass = IFeatureDataConverter_ConvertFeatureClass(pDataset.Workspace, objectWorkspace, pDataset.Name, fileName);
                 }
             }
             catch (System.Exception ex)
             {
                 MessageBox.Show("操作失败！");
                 return null;
             }
             return pOutFeatureClass;
         }
         #endregion

         #region 要素融合
         /// <param name="pFeatureClass">融合要素</param>
         /// <param name="dissField">融合字段</param>
         /// <param name="outPath">输出数据库路径</param>
         /// <param name="name">输出要素名称</param>
         private IFeatureClass Dissolve(IFeatureClass pFeatureClass,string dissField,string outPath,string name)
         {
             IFeatureClass pOutFeatureClass = null;

             try
             {
	             ESRI.ArcGIS.Geoprocessor.Geoprocessor gp = new ESRI.ArcGIS.Geoprocessor.Geoprocessor();
	             ESRI.ArcGIS.DataManagementTools.Dissolve pDissolve = new ESRI.ArcGIS.DataManagementTools.Dissolve();
	             gp.OverwriteOutput = true;
	             pDissolve.in_features = pFeatureClass;
	             pDissolve.dissolve_field = dissField;
                 pDissolve.out_feature_class = outPath + "\\" + name;
	             pDissolve.multi_part = "true";     //跨区域融合;
	             
	             ESRI.ArcGIS.Geoprocessing.IGeoProcessorResult2 result = (ESRI.ArcGIS.Geoprocessing.IGeoProcessorResult2)gp.Execute(pDissolve, null);
	             
	             if (result.Status != ESRI.ArcGIS.esriSystem.esriJobStatus.esriJobSucceeded)
	             {
	                 MessageBox.Show("操作失败！");
	                 return null;
	             }
	             else
	             {
                     pOutFeatureClass = gp.Open(result.ReturnValue) as IFeatureClass;
	            }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("操作失败！");
                return null;
            }
            return pOutFeatureClass;
         }
         #endregion

         #region 
         //string densityField = "PopDensity";
         //#region 添加人口密度字段并赋值
         ///// <param name="pFClass">要素</param>
         ///// <param name="zone_Bound">行政边界数据的分区编码字段索引</param>
         ///// <param name="zone_Popu">人口数据的分区编码字段索引</param>
         ///// <param name="popIndex">人口数据的分区人口字段索引</param>
         //private IFeatureClass AddDensityField(IFeatureClass pFClass,int zone_Bound,int zone_Popu,int popIndex)
         //{
         //    if (dt_Population == null)
         //    {
         //        return null;
         //    }
         //    try
         //    {
         //        #region 添加字段
         //        IFields pFields = pFClass.Fields;
         //        if (pFields.FindField(densityField) >= 0)
         //        {
         //            densityField += 1;
         //        }
         //        IField pField = new FieldClass();
         //        IFieldEdit pFieldEdit = pField as IFieldEdit;
         //        pFieldEdit.Name_2 = densityField;
         //        pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
         //        pFieldEdit.Editable_2 = true;
         //        pFieldEdit.AliasName_2 = densityField;
         //        pFieldEdit.Length_2 = int.MaxValue;
         //        ITable pTable = (ITable)pFClass;
         //        pTable.AddField(pField);
         //        #endregion

         //        #region 给添加的字段赋值
         //        IFeatureCursor pFeatureCursor;
         //        pFeatureCursor = pFClass.Search(null, false);

         //        int densFieldIndex = pFClass.Fields.FindField(densityField);
         //        int areaFieldIndex = pFClass.Fields.FindField("Shape_Area");

         //        IFeature pFeature = pFeatureCursor.NextFeature();
         //        while (pFeature != null)
         //        {
         //            object zoneID = pFeature.get_Value(zone_Bound);
         //            double density = 0;
         //            for (int i = 0; i < dt_Population.Rows.Count;i++ )
         //            {
         //                DataRow dataRow = dt_Population.Rows[i];
         //                string zoneid = Convert.ToString(dataRow[zone_Popu]);
         //                if (Convert.ToString(zoneID) == zoneid)
         //                {
         //                    double population = Convert.ToDouble(dataRow[popIndex]);
         //                    object bulidArea = pFeature.get_Value(areaFieldIndex);

         //                    if (Convert.ToDouble(bulidArea) != 0)
         //                    {
         //                        density = population / Convert.ToDouble(bulidArea);
         //                    }
         //                    else
         //                    {
         //                        density = 0;
         //                    }
         //                    pFeature.set_Value(densFieldIndex, density);
         //                    pFeature.Store();
         //                }
         //            }
         //            pFeature = pFeatureCursor.NextFeature();
         //        }
         //        #endregion
         //    }
         //    catch (Exception ex)
         //    {
         //        return null;
         //    }
         //    return pFClass;
         //}
         //#endregion

         //#region 计算格网人口数
         ///// <param name="pFClass">要素</param>
         ///// <param name="gridIdField">格网数据的编码字段</param>
         //private bool GridPopulation(IFeatureClass pFClass, string gridIdField)
         //{
         //    try
         //    {
         //        #region 获取格网ID和对应的人口数
         //        IFeatureCursor pFeatureCursor;
         //        pFeatureCursor = pFClass.Search(null, false);

         //        int detyFieldIndex = pFClass.Fields.FindField(densityField);
         //        int areaFieldIndex = pFClass.Fields.FindField("Shape_Area");
         //        int gridIdIndex = pFClass.Fields.FindField(gridIdField);
         //        IFeature pFeature = pFeatureCursor.NextFeature();
         //        while (pFeature != null)
         //        {
         //            object density = pFeature.get_Value(detyFieldIndex);
         //            object buildArea = pFeature.get_Value(areaFieldIndex);
         //            string gridid = Convert.ToString(pFeature.get_Value(gridIdIndex));
         //            double population = Convert.ToDouble(density) * Convert.ToDouble(buildArea);

         //            #region 将重复的格网id对应的人口数相加;
         //            double tmp = 0;
         //            int index = 0;
         //            if (gridID.Contains(gridid))
         //            {
         //                index = gridID.IndexOf(gridid);
         //                tmp = gridPo[index] + population;
         //                gridPo.RemoveAt(index);
         //                gridPo.Insert(index, tmp);
         //            }
         //            else
         //            {
         //                gridID.Add(Convert.ToString(gridid));
         //                gridPo.Add(population);
         //            }
         //            #endregion
         //            pFeature = pFeatureCursor.NextFeature();
         //        }
         //        #endregion
         //    }
         //    catch (Exception ex)
         //    {
         //        return false;
         //    }
         //    return true;
         //}
         //#endregion

         //#region 输出数据
         ///// <param name="pFeatureClass"> 要素</param>
         ///// <param name="gridIdField">格网数据的编码字段</param>
         ///// <param name="outputPath">输出文件路径</param>
         ///// <param name="name">输出文件名称</param>
         //private void OutPutData(IFeatureClass pFeatureClass,string gridIdField, string outputPath,string name)
         //{
         //    try
         //    {
         //        #region 添加格网人口字段
         //        string gridPopField = "gridPopulation";
         //        IFields pFields = pFeatureClass.Fields;
         //        if (pFields.FindField(gridPopField) >= 0)
         //        {
         //            gridPopField += 1;
         //        }
         //        IField pField = new FieldClass();
         //        IFieldEdit pFieldEdit = pField as IFieldEdit;

         //        pFieldEdit.Name_2 = gridPopField;
         //        pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
         //        pFieldEdit.Editable_2 = true;
         //        pFieldEdit.AliasName_2 = gridPopField;
         //        pFieldEdit.Length_2 = int.MaxValue;
         //        ITable pTable = (ITable)pFeatureClass;
         //        pTable.AddField(pField);
         //        #endregion

         //        #region 给添加的字段赋值
         //        IFeatureCursor pFeatureCursor;
         //        pFeatureCursor = pFeatureClass.Search(null, false);

         //        int idIndex = pFeatureClass.Fields.FindField(gridIdField);
         //        int popIndex = pFeatureClass.Fields.FindField(gridPopField);
         //        IFeature pFeature = pFeatureCursor.NextFeature();
         //        while (pFeature != null)
         //        {
         //            string id = Convert.ToString(pFeature.get_Value(idIndex));
         //            if (gridID.Contains(id))
         //            {
         //                int n = gridID.IndexOf(id);
         //                pFeature.set_Value(popIndex, gridPo[n]);
         //                pFeature.Store();
         //            }
         //            else
         //            {
         //                pFeature.set_Value(popIndex, 0);
         //                pFeature.Store();
         //            }
         //            pFeature = pFeatureCursor.NextFeature();
         //        }
         //        #endregion

         //        #region 输出数据
         //        IDataset pDataset = pFeatureClass as IDataset;
         //        //目标文件夹或数据库;
         //        IWorkspace objectWorkspace = null;
         //        if (outputPath.Contains(".gdb"))
         //        {
         //            IWorkspaceFactory factory = new FileGDBWorkspaceFactoryClass();
         //            objectWorkspace = factory.OpenFromFile(outputPath, 0);
         //        }
         //        else
         //        {
         //            IWorkspaceFactory factory = new ShapefileWorkspaceFactory();
         //            objectWorkspace = factory.OpenFromFile(outputPath,0);
         //        }
         //        IFeatureClass pOutFeatureClass = IFeatureDataConverter_ConvertFeatureClass(pDataset.Workspace, objectWorkspace, pDataset.Name, name);
         //        IFeatureLayer pLayer = new FeatureLayerClass
         //        {
         //            FeatureClass = pOutFeatureClass,
         //            Name = pOutFeatureClass.AliasName
         //        };
         //        mMapControl.AddLayer(pLayer);
         //        mMapControl.ActiveView.Refresh();
         //        #endregion

         //    }
         //    catch (Exception ex)
         //    {
         //        return;
         //    }
         //}
         //#endregion
         #endregion



         #region 获取每个分区的建筑面积
         private bool GetZoneBuildArea(IFeatureClass pFClass, string zoneID_Field, string BD_Field, string BH_Field)
         {
             try
             {
                 IFeatureCursor pFeatureCursor;
                 pFeatureCursor = pFClass.Search(null, false);

                 int areaIndex = pFClass.Fields.FindField("Shape_Area");
                 int BD_Index = pFClass.Fields.FindField(BD_Field);
                 int BH_Index = pFClass.Fields.FindField(BH_Field);
                 int zoneID_Index = pFClass.Fields.FindField(zoneID_Field);

                 IFeature pFeature = pFeatureCursor.NextFeature();
                 while (pFeature != null)
                 {
                     string tempID = Convert.ToString(pFeature.get_Value(zoneID_Index));
                     double area = Convert.ToDouble(pFeature.get_Value(areaIndex));

                     #region 根据建筑密度和建筑高度确定建筑面积
                     double builddensity = 1;
                     double buildheight = 1;
                     if (BD_Index >= 0)
                     {
                         builddensity = Convert.ToDouble(pFeature.get_Value(BD_Index));
                     }
                     if (BH_Index >= 0)
                     {
                         buildheight = Convert.ToDouble(pFeature.get_Value(BH_Index));
                     }

                     double tempArea = area * builddensity * buildheight;
                     #endregion

                     #region 统计每个分区的建筑面积
                     double tmp = 0;
                     int index = 0;
                     if (ZoneID.Contains(tempID))
                     {
                         index = ZoneID.IndexOf(tempID);
                         tmp = ZoneBuildArea[index] + tempArea;
                         ZoneBuildArea.RemoveAt(index);
                         ZoneBuildArea.Insert(index, tmp);
                     }
                     else
                     {
                         ZoneID.Add(tempID);
                         ZoneBuildArea.Add(tempArea);
                     }
                     #endregion
                     pFeature = pFeatureCursor.NextFeature();
                 }
             }
             catch (Exception ex)
             {
                 return false;
             }
             return true;
         }
         #endregion

         #region 计算每个分区的人口密度
         private void GetZoneBuildDensity(DataTable dataTable, string zoneID_Field, string Pop_Field)
         {
             if (ZoneID.Count <= 0)
             {
                 return;
             }
             try
             {
                 for (int i = 0; i < ZoneID.Count; i++)
                 {
                     double density = 0;
                     foreach (DataRow dataRow in dt_Population.Rows)
                     {
                         string zoneid = Convert.ToString(dataRow[zoneID_Field]);
                         if (ZoneID[i] == zoneid)
                         {
                             double population = Convert.ToDouble(dataRow[Pop_Field]);
                             
                             if (ZoneBuildArea[i] != 0)
                             {
                                 density = population / ZoneBuildArea[i];
                             }
                         }
                     }
                     ZoneBuildDensity.Add(density);
                 }
             }
             catch (System.Exception ex)
             {
                 return;
             }
             
         }
         #endregion

         #region 计算相交格网的人口数
         private bool GetGridPopulation(IFeatureClass pFeatureClass, string zoneID_Field, string gridID_Field, string BD_Field, string BH_Field)
         {
             try
             {
                 IFeatureCursor pFeatureCursor;
                 pFeatureCursor = pFeatureClass.Search(null, false);
                 IFeature pFeature = pFeatureCursor.NextFeature();

                 int areaIndex = pFeatureClass.Fields.FindField("Shape_Area");
                 int BD_Index = pFeatureClass.Fields.FindField(BD_Field);
                 int BH_Index = pFeatureClass.Fields.FindField(BH_Field);
                 int zoneID_Index = pFeatureClass.Fields.FindField(zoneID_Field);
                 int gridID_Index = pFeatureClass.Fields.FindField(gridID_Field);

                 while (pFeature != null)
                 {
                     string gridid = Convert.ToString(pFeature.get_Value(gridID_Index));
                     string zoneid = Convert.ToString(pFeature.get_Value(zoneID_Index));
                     double area = Convert.ToDouble(pFeature.get_Value(areaIndex));

                     #region 计算格网人口数
                     //根据建筑密度和建筑高度确定当前板块建筑面积;
                     double builddensity = 1;
                     double buildheight = 1;
                     if (BD_Index >= 0)
                     {
                         builddensity = Convert.ToDouble(pFeature.get_Value(BD_Index));
                     }
                     if (BH_Index >= 0)
                     {
                         buildheight = Convert.ToDouble(pFeature.get_Value(BH_Index));
                     }
                     //根据每个图斑的行政分区代码，计算当前图斑的人口数;
                     double tempPop = 0;
                     if (ZoneID.Contains(zoneid))
                     {
                         int n = ZoneID.IndexOf(zoneid);
                         tempPop = area * builddensity * buildheight * ZoneBuildDensity[n];
                     }
                     #endregion

                     #region 统计每个格网的人口数
                     double tmp = 0;
                     int index = 0;
                     if (GridID.Contains(gridid))
                     {
                         index = GridID.IndexOf(gridid);
                         tmp = GridPopu[index] + tempPop;
                         GridPopu.RemoveAt(index);
                         GridPopu.Insert(index, tmp);
                     }
                     else
                     {
                         GridID.Add(gridid);
                         GridPopu.Add(tempPop);
                     }
                     #endregion

                     pFeature = pFeatureCursor.NextFeature();
                 }
             }
             catch (System.Exception ex)
             {
                 return false;
             }
             return true;
         }
         #endregion
         #region 输出格网人口数
         private void OutputGridPopulation(IFeatureClass pFeatureClass, string gridID_Field, string outputPath, string name)
         {
             #region 添加格网人口字段
             IFields pFields = pFeatureClass.Fields;
             string gridPop = "Population";
             if (pFields.FindField(gridPop) >= 0)
             {
                 gridPop = gridPop + 1;
             }

             IField pField = new FieldClass();
             IFieldEdit pFieldEdit = pField as IFieldEdit;
             pFieldEdit.Name_2 = gridPop;
             pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
             pFieldEdit.Editable_2 = true;
             pFieldEdit.AliasName_2 = gridPop;
             pFieldEdit.Length_2 = int.MaxValue;
             ITable pTable = (ITable)pFeatureClass;
             pTable.AddField(pField);
             #endregion

             #region 给格网人口字段赋值
             IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, false);
             IFeature pFeature = pFeatureCursor.NextFeature();

             int grididIndex = pFeatureClass.Fields.FindField(gridID_Field);
             int gridpopIndex = pFeatureClass.Fields.FindField(gridPop);
             while (pFeature != null)
             {
                 string id = Convert.ToString(pFeature.get_Value(grididIndex));

                 if (GridID.Contains(id))
                 {
                     int index = GridID.IndexOf(id);
                     pFeature.set_Value(gridpopIndex, GridPopu[index]);
                     pFeature.Store();
                 }
                 else
                 {
                     pFeature.set_Value(gridpopIndex, 0);
                     pFeature.Store();
                 }

                 pFeature = pFeatureCursor.NextFeature();
             }
             #endregion

             #region 输出数据
             IDataset pDataset = pFeatureClass as IDataset;
             //目标文件夹或数据库;
             IWorkspace objectWorkspace = null;
             if (outputPath.Contains(".gdb"))
             {
                 IWorkspaceFactory factory = new FileGDBWorkspaceFactoryClass();
                 objectWorkspace = factory.OpenFromFile(outputPath, 0);
             }
             else
             {
                 IWorkspaceFactory factory = new ShapefileWorkspaceFactory();
                 objectWorkspace = factory.OpenFromFile(outputPath, 0);
             }
             IFeatureClass pOutFeatureClass = IFeatureDataConverter_ConvertFeatureClass(pDataset.Workspace, objectWorkspace, pDataset.Name, name);
             IFeatureLayer pLayer = new FeatureLayerClass
             {
                 FeatureClass = pOutFeatureClass,
                 Name = pOutFeatureClass.AliasName
             };
             mMapControl.AddLayer(pLayer);
             mMapControl.ActiveView.Refresh();
             #endregion


         }
         #endregion
    }
}
