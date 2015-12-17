using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geoprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AE_Environment.Forms
{
    public partial class VegetationInterfereIndexFrm : Form
    {
        IMapControl3 mapControl = null;
        private List<string> ZoneID = new List<string>();
        private List<double> ZoneLength = new List<double>();

        public VegetationInterfereIndexFrm(IMapControl3 _mapControl)
        {
            InitializeComponent();
            mapControl = _mapControl;
        }

        private void VegetationInterfereIndexFrm_Load(object sender, EventArgs e)
        {
            if(mapControl.LayerCount <= 0)
            {
                MessageBox.Show("请添加数据");
            }
            else
            {
                for (int i = 0;i < mapControl.LayerCount;i++)
                {
                    ILayer pLayer = mapControl.get_Layer(i);
                    cbox_Line_Layer.Items.Add(pLayer.Name);
                    cbox_Unit_Layer.Items.Add(pLayer.Name);
                }
            }
        }

        private void btn_Run_Click(object sender, EventArgs e)
        {
            if (CheckedInput())
            {
                progressBar1.Value = 0;
                progressBar1.Maximum = 100;
                for (int i = 0; i < 25;i++ )
                {
                    progressBar1.Value++;
                }
                
                IFeatureLayer pFLayer_Line = mapControl.get_Layer(cbox_Line_Layer.SelectedIndex) as IFeatureLayer;
                IFeatureLayer pFLayer_Unit = mapControl.get_Layer(cbox_Unit_Layer.SelectedIndex) as IFeatureLayer;

                string zoneIDField = cbox_ZoneID_Field.SelectedItem.ToString();
                string outPutPath = tbox_Path.Text;
                string outName = tbox_Name.Text;
                string gdbPath = CreateFileGDB();
                IFeatureClass pFeatureClass_In = Intersect(pFLayer_Line.FeatureClass, pFLayer_Unit.FeatureClass,gdbPath);
                for (int i = 0; i < 25; i++)
                {
                    progressBar1.Value++;
                }
                if (GetResult(pFeatureClass_In,zoneIDField))
                {
                    for (int i = 0; i < 25; i++)
                    {
                        progressBar1.Value++;
                    }
                    IDataset pDataset = pFLayer_Unit.FeatureClass as IDataset;
                    //目标数据库;
                    IWorkspaceFactory factory = new FileGDBWorkspaceFactoryClass();
                    IWorkspace objectWorkspace = factory.OpenFromFile(gdbPath, 0);

                    string fileName = pFLayer_Unit.FeatureClass.AliasName;
                    IFeatureClass pOutFeatureClass = IFeatureDataConverter_ConvertFeatureClass(pDataset.Workspace, objectWorkspace, pDataset.Name, fileName);
                    OutPutData(pOutFeatureClass, zoneIDField, outPutPath, outName);
                    for (int i = 0; i < 25; i++)
                    {
                        progressBar1.Value++;
                    }
                }
                MessageBox.Show("计算完成！");
                base.Close();
            }
        }

        private void btn_SelectPath_Click(object sender, EventArgs e)
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
                tbox_Path.Text = selectedPath;
            }
        }

        private void cbox_Unit_Layer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbox_ZoneID_Field.Items.Clear();
            IFeatureLayer pLayer = mapControl.get_Layer(cbox_Unit_Layer.SelectedIndex) as IFeatureLayer;
            for (int i = 0; i < pLayer.FeatureClass.Fields.FieldCount; i++)
            {
                cbox_ZoneID_Field.Items.Add(pLayer.FeatureClass.Fields.get_Field(i).Name);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

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

        #region 判断数据的输入
        private bool CheckedInput()
        {
            if (cbox_Line_Layer.SelectedItem == null)
            {
                return false;
            }
            if (cbox_Unit_Layer.SelectedItem == null)
            {
                return false;
            }
            if (cbox_ZoneID_Field.SelectedItem == null)
            {
                return false;
            }
            if (tbox_Path.Text == "")
            {
                return false;
            }
            if (tbox_Name.Text == "")
            {
                return false;
            }
            return true;
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
        private IFeatureClass Dissolve(IFeatureClass pFeatureClass, string dissField, string outPath, string name)
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

        #region 获取每个分区的廊道长度
        private bool GetResult(IFeatureClass pFeatureClass, string zoneIDField)
        {
            try
            {
                IFeatureCursor pFeatureCursor;
                pFeatureCursor = pFeatureClass.Search(null, false);
                IFeature pFeature = pFeatureCursor.NextFeature();
                int idIndex = pFeatureClass.Fields.FindField(zoneIDField);
                int lengIndex = pFeatureClass.Fields.FindField("Shape_Length");
                while (pFeature != null)
                {
                    string id = Convert.ToString(pFeature.get_Value(idIndex));
                    double length = Convert.ToDouble(pFeature.get_Value(lengIndex));
                    #region 统计每个格网的人口数
                    double tmp = 0;
                    int index = 0;
                    if (ZoneID.Contains(id))
                    {
                        index = ZoneID.IndexOf(id);
                        tmp = ZoneLength[index] + length;
                        ZoneLength.RemoveAt(index);
                        ZoneLength.Insert(index, tmp);
                    }
                    else
                    {
                        ZoneID.Add(id);
                        ZoneLength.Add(length);
                    }
                    #endregion
                    pFeature = pFeatureCursor.NextFeature();
                }
            }
            catch (System.Exception ex)
            {
                return false ;
            }
            return true;
        }
        #endregion

        #region 数据输出
        private void OutPutData(IFeatureClass pFeatureClass, string zoneIDField, string filePath, string fileName)
        {
            try
            {
                #region 添加格网人口字段
                IFields pFields = pFeatureClass.Fields;
                string VIIndex = "VIIndex";
                if (pFields.FindField(VIIndex) >= 0)
                {
                    VIIndex = VIIndex + 1;
                }

                IField pField = new FieldClass();
                IFieldEdit pFieldEdit = pField as IFieldEdit;
                pFieldEdit.Name_2 = VIIndex;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.Editable_2 = true;
                pFieldEdit.AliasName_2 = VIIndex;
                pFieldEdit.Length_2 = int.MaxValue;
                ITable pTable = (ITable)pFeatureClass;
                pTable.AddField(pField);
                #endregion

                #region 给添加的字段赋值
                IFeatureCursor pFeatureCursor;
                pFeatureCursor = pFeatureClass.Search(null, false);
                IFeature pFeature = pFeatureCursor.NextFeature();
                int idIndex = pFeatureClass.Fields.FindField(zoneIDField);
                int areaIndex = pFeatureClass.Fields.FindField("Shape_Area");
                int VIIndexIndex = pFeatureClass.Fields.FindField(VIIndex);
                while (pFeature != null)
                {
                    string id = Convert.ToString(pFeature.get_Value(idIndex));
                    double area = Convert.ToDouble(pFeature.get_Value(areaIndex));

                    if (ZoneID.Contains(id))
                    {
                        int index = ZoneID.IndexOf(id);
                        double temp = 0;
                        if (area != 0)
                        {
                            temp = ZoneLength[index] / area;
                        } 
                        pFeature.set_Value(VIIndexIndex, temp);
                        pFeature.Store();
                    }
                    else
                    {
                        pFeature.set_Value(VIIndexIndex, 0);
                        pFeature.Store();
                    }
                    pFeature = pFeatureCursor.NextFeature();
                }
                #endregion

                #region 输出数据
                IDataset pDataset = pFeatureClass as IDataset;
                //目标文件夹或数据库;
                IWorkspace objectWorkspace = null;
                if (filePath.Contains(".gdb"))
                {
                    IWorkspaceFactory factory = new FileGDBWorkspaceFactoryClass();
                    objectWorkspace = factory.OpenFromFile(filePath, 0);
                }
                else
                {
                    IWorkspaceFactory factory = new ShapefileWorkspaceFactory();
                    objectWorkspace = factory.OpenFromFile(filePath, 0);
                }
                IFeatureClass pOutFeatureClass = IFeatureDataConverter_ConvertFeatureClass(pDataset.Workspace, objectWorkspace, pDataset.Name, fileName);
                #endregion
            }
            catch (System.Exception ex)
            {
                return;
            }

        }
        #endregion

        
    }
}
