using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;




namespace AE_Environment
{
    class ArcGISUtilities
    {




        public static bool isExitFeatureClass(IWorkspace pW, string pFeatureClassName)
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
       /// 删除数据集
       /// </summary>
       /// <param name="pDataset"></param>
        public static void DeleteDataset(IDataset pDataset)
        {

            if (pDataset.CanDelete())
            {
                pDataset.Delete();

            }
            else
            {
                MessageBox.Show("无法删除该数据集！");
            }
        }

        /// <summary>
        /// 获取唯一值
        /// </summary>
        /// <param name="pFeatureClass"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static IEnumerator GetUniqueValue(IFeatureClass pFeatureClass,string fieldName)
        {
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = fieldName;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator= pDataStatistics.UniqueValues;
            return pEnumerator;

        }
        /// <summary>
        ///从图层获取其路径问题即数据源
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static string LayerToShapefilePath(ILayer pLayer)
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
        /// 打开工作空间
        /// </summary>
        /// <param name="dir">目录</param>
        /// <returns></returns>
        static public IWorkspace GetShapefileWorkspace(string dir)
        {
            IWorkspaceFactory pWsF = new ShapefileWorkspaceFactoryClass();
            IWorkspace pWs = pWsF.OpenFromFile(dir, 0);
            return pWs;

        }
         //求交分析
//             Geoprocessor gpIns = new Geoprocessor();
//             gpIns.OverwriteOutput = true;
//             ESRI.ArcGIS.AnalysisTools.Intersect ins = new ESRI.ArcGIS.AnalysisTools.Intersect();
//             ins.in_features = textBox1.Text+";"+textBox2.Text;
//             ins.out_feature_class = System.IO.Path.GetDirectoryName(textBox1.Text)+
//                 "\\intersect.shp";
//             ins.output_type = "INPUT";
//             gpIns.Execute(ins, null);
        //栅格转换

//                 ESRI.ArcGIS.Geoprocessor.Geoprocessor pGp = new ESRI.ArcGIS.Geoprocessor.Geoprocessor();
//                 pGp.OverwriteOutput = true;
//                 //pGp.SetEnvironmentValue("Workspace", System.IO.Path.GetDirectoryName(textBox4.Text));
//                 ESRI.ArcGIS.ConversionTools.FeatureToRaster pFToRaster = new ESRI.ArcGIS.ConversionTools.FeatureToRaster();
//                // pFToRaster.in_features = System.IO.Path.GetDirectoryName(textBox4.Text)+"\\temp.shp";
//                 pFToRaster.in_features = pTempFC;
//                 pFToRaster.field = "景观破碎度";
//                 pFToRaster.out_raster = textBox4.Text;
//                 //可选参数
//                // pFToRaster.cell_size = "90";
//                 try
//                 {
//                     pGp.Execute(pFToRaster, null);
//                 }
//                 catch (System.Exception ex)
//                 {
//                     MessageBox.Show("输出栅格错误！");
//                 }
//                 

        /// <summary>
        /// 打开Featureclass
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>IFeatureClass</returns>
        static public IFeatureClass GetFeatureClass(string  path) {

             
            string fileDir = System.IO.Path.GetDirectoryName(path);
            IFeatureWorkspace pFWs = GetShapefileWorkspace(fileDir) as IFeatureWorkspace;
            string fileName = System.IO.Path.GetFileName(path);
            IFeatureClass pFC = pFWs.OpenFeatureClass(fileName);

            return pFC;
        }
        static public IFeatureClass GetFeatureClass(string filepath, string name)
        {

            IFeatureWorkspace pFWs = Utilities.WorkspaceHelper.GetAccessWorkspace(filepath) as IFeatureWorkspace;
            
            IFeatureClass pFC = pFWs.OpenFeatureClass(name);
            return pFC;
        }
        static public IFeatureClass GetFeatureClassFromGDB(string filepath, string name)
        {

            IFeatureWorkspace pFWs = Utilities.WorkspaceHelper.GetFGDBWorkspace(filepath) as IFeatureWorkspace;

            IFeatureClass pFC = pFWs.OpenFeatureClass(name);
            return pFC;
        }
        /// <summary>
        /// 获取要素图层
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        static public IFeatureLayer GetFeatureLayer(string Path)
        {

            IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            IFeatureClass pFeatureClass = GetFeatureClass(Path);
            pFeatureLayer.FeatureClass = pFeatureClass;
            pFeatureLayer.Name = System.IO.Path.GetFileNameWithoutExtension(Path);
            return pFeatureLayer;

        }
        static public IFeatureLayer GetFeatureLayerFromMdb(string Path,string name)
        {

            IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            IFeatureClass pFeatureClass = GetFeatureClass(Path,name);
            pFeatureLayer.FeatureClass = pFeatureClass;
            pFeatureLayer.Name = System.IO.Path.GetFileNameWithoutExtension(Path);
            return pFeatureLayer;

        }
        static public IFeatureLayer GetFeatureLayerFromGDB(string Path, string name)
        {

            IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            IFeatureClass pFeatureClass = GetFeatureClassFromGDB(Path, name);
            pFeatureLayer.FeatureClass = pFeatureClass;
            pFeatureLayer.Name = System.IO.Path.GetFileNameWithoutExtension(Path);
            return pFeatureLayer;

        }
        /// <summary>
        /// 创建数据表
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
//        static public ITable CreateTable(string path)
//         {
//             
// 
//            string _TablePath=Path.GetDirectoryName(path);
//            string _TableName=Path.GetFileName(path);  
//            IWorkspaceFactory pWks = new ShapefileWorkspaceFactoryClass();
//              IFeatureWorkspace pFwk = pWks.OpenFromFile(_TablePath,0) as IFeatureWorkspace;
//             //用于分区的ID;
//             IField pFieldID = new FieldClass();
//             IFieldEdit pFieldIID = pFieldID as IFieldEdit;
//             pFieldIID.Type_2 = esriFieldType.esriFieldTypeInteger;
//             pFieldIID.Name_2 = "ZID";
//            //用于记录比值的;
//             IField pFieldValue = new FieldClass();
//             IFieldEdit pFieldICount = pFieldValue as IFieldEdit;
//             pFieldICount.Type_2 = esriFieldType.esriFieldTypeDouble;
//             pFieldICount.Name_2 = "Value";
//             //用于添加表中的必要字段
//             ESRI.ArcGIS.Geodatabase.IObjectClassDescription objectClassDescription =
//             new ESRI.ArcGIS.Geodatabase.ObjectClassDescriptionClass();
//           
//             IFields pTableFields = objectClassDescription.RequiredFields;
//             IFieldsEdit pTableFieldsEdit = pTableFields as IFieldsEdit;
// 
//             pTableFieldsEdit.AddField(pFieldID);
//             pTableFieldsEdit.AddField(pFieldValue);
// 
//             ITable pTable = pFwk.CreateTable(_TableName,pTableFields, null,null, "");
//             return pTable;
//             
//         }
        /// <summary>
        /// 创建包含指定字段的数据表
        /// </summary>
        /// <param name="path"></param>
        /// <param name="pFields"></param>
        /// <returns></returns>
       static public ITable CreateTable(string path,IFields pFields)
       {


           string _TablePath = System.IO.Path.GetDirectoryName(path);
           string _TableName = System.IO.Path.GetFileName(path);
           IWorkspaceFactory pWks = new ShapefileWorkspaceFactoryClass();
           IFeatureWorkspace pFwk = pWks.OpenFromFile(_TablePath, 0) as IFeatureWorkspace;
            
           //用于添加表中的必要字段
           ESRI.ArcGIS.Geodatabase.IObjectClassDescription objectClassDescription =
           new ESRI.ArcGIS.Geodatabase.ObjectClassDescriptionClass();

           IFields pTableFields = objectClassDescription.RequiredFields;
           IFieldsEdit pTableFieldsEdit = pTableFields as IFieldsEdit;
           for (int i = 0; i < pFields.FieldCount;i++ )
           {
               IField pField = pFields.Field[i];
               pTableFieldsEdit.AddField(pField);
           }
           ITable pTable = pFwk.CreateTable(_TableName, pTableFields, null, null, "");
           return pTable;

       }
       /// <summary>
       /// 创建一个只有ZID的数据表
       /// </summary>
       /// <param name="path"></param>
       /// <returns></returns>
        static public ITable CreateTable(string path)
       {


           string _TablePath =System.IO. Path.GetDirectoryName(path);
           string _TableName = System.IO.Path.GetFileName(path);
           IWorkspaceFactory pWks = new ShapefileWorkspaceFactoryClass();
           IFeatureWorkspace pFwk = pWks.OpenFromFile(_TablePath, 0) as IFeatureWorkspace;
          
           IField pField = new FieldClass();
           IFieldEdit pFieldEdit = pField as IFieldEdit;
           pFieldEdit.Name_2 = "ZID";
           pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
           //用于添加表中的必要字段
           ESRI.ArcGIS.Geodatabase.IObjectClassDescription objectClassDescription =
           new ESRI.ArcGIS.Geodatabase.ObjectClassDescriptionClass();

           IFields pTableFields = objectClassDescription.RequiredFields;
           IFieldsEdit pFieldsEdit = pTableFields as IFieldsEdit;
           pFieldsEdit.AddField(pField);
           ITable pTable = pFwk.CreateTable(_TableName, pTableFields, null, null, "");
           return pTable;

       }
        /// <summary>
        /// 计算面积,周长字段
        /// </summary>
        /// <param name="pFeatureClass"></param>
        static public void ComputeAreaField(IFeatureClass pFeatureClass,string name)
        {
                int index = FieldIndex(pFeatureClass, name);
                IFeatureCursor pFCursor = pFeatureClass.Update(null,false);
                IFeature pFeature = pFCursor.NextFeature();
                IArea pArea;
                ICurve pCurve;

                while (pFeature != null)
                {
                    
                     if (name=="Shape_Area")
                    {
                        pArea = pFeature.Shape as IArea;
                        pFeature.set_Value(index, pArea.Area);
                    }
                     else
                    {
                        pCurve = pFeature.Shape as ICurve;
                        pFeature.set_Value(index, pCurve.Length);
                    }
                    // pFeature.Store();
                     pFCursor.UpdateFeature(pFeature);
                    pFeature = pFCursor.NextFeature();
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFCursor);
            
        }
        /// <summary>
        /// 更新面积字段
        /// </summary>
        /// <param name="pFeatureClass"></param>
        static public void UpdateArea(IFeatureClass pFeatureClass) {
            int index = FieldIndex(pFeatureClass, "Shape_Area");
            if (index > 0)
            {

                ComputeAreaField(pFeatureClass, "Shape_Area");
            }
            else
            {
                IField pFieldValue = new FieldClass();
                IFieldEdit pFieldEdit = pFieldValue as IFieldEdit;
                pFieldEdit.Name_2 = "Shape_Area";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFeatureClass.AddField(pFieldValue);
                ComputeAreaField(pFeatureClass, "Shape_Area");
            }
        }
        /// <summary>
        /// 更新周长字段
        /// </summary>
        /// <param name="pFeatureClass"></param>
        static public void UpdatePrim(IFeatureClass pFeatureClass)
        {
            int index = FieldIndex(pFeatureClass, "Shape_Leng");
            if (index > 0)
            {

                ComputeAreaField(pFeatureClass, "Shape_Leng");
            }
            else
            {
                IField pFieldValue = new FieldClass();
                IFieldEdit pFieldEdit = pFieldValue as IFieldEdit;
                pFieldEdit.Name_2 = "Shape_Leng";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFeatureClass.AddField(pFieldValue);
                ComputeAreaField(pFeatureClass, "Shape_Leng");
            }
        }
        /// <summary>
        /// 更新面积和周长字段
        /// </summary>
        /// <param name="pFeatureClass"></param>
        static public void UpdateAreaAndPrim(IFeatureClass pFeatureClass)
        {
            UpdateArea(pFeatureClass);
            UpdatePrim(pFeatureClass);

        }
        /// <summary>
        /// 返回字段的索引
        /// </summary>
        /// <param name="pFeatureClass"></param>
        /// <param name="pFieldName"></param>
        /// <returns></returns>
        static public int FieldIndex(IFeatureClass pFeatureClass,string pFieldName)
        {
            
            IFields pFields = pFeatureClass.Fields;
            int index= pFields.FindField(pFieldName);
            return index;
        }

        //自定义函数
        /// <summary>  
        /// 获取图层  
        /// </summary>  
        /// <param name="layerName"></param>  
        /// <returns></returns>  
       public static IFeatureLayer GetFeatureLayer(string layerName,IHookHelper pHookHelper)
        {
            //get the layers from the maps  
            IEnumLayer layers = GetLayers(pHookHelper);
            layers.Reset();

            ILayer layer = null;
            while ((layer = layers.Next()) != null)
            {
                if (layer.Name == layerName)
                    return layer as IFeatureLayer;
            }

            return null;
        }

        /// <summary>  
        /// 获取图层  
        /// </summary>  
        /// <returns></returns>  
       public static IEnumLayer GetLayers(IHookHelper pHookHelper)
        {
            
            IEnumLayer layers =pHookHelper.FocusMap. Layers;
            return layers;
        }
        /// <summary>
        /// 获取颜色
        /// </summary>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public IRgbColor GetRGBColor(int R,int G,int B)
       {

           IRgbColor pRGB;
           pRGB = new RgbColorClass();
           pRGB.Blue = B;
           pRGB.Red = R;
           pRGB.Green = G;
           return pRGB;
        }
        /// <summary>
        /// 实现导入导出
        /// </summary>
        /// <param name="inputFeatureClass"></param>
        /// <param name="targetpath"></param>
        /// <param name="targetname"></param>
        public static void CopyData(IFeatureClass inputFeatureClass,IWorkspace pWorkspace,string targetname)
        {


             
            IDataset inputDataset = (IDataset)inputFeatureClass;
            IFeatureClassName inputclassName = (IFeatureClassName)inputDataset.FullName;
            // Get the layer's selection set. 
            //利用属性打开
//             IPropertySet ps = new PropertySetClass();
//             ps.SetProperty("DATABASE", targetpath);
//             IWorkspaceFactory wsf = new FileGDBWorkspaceFactoryClass();
//             IWorkspace ws = null;
//             try
//             {
//                 ws = wsf.Open(ps, 0);
//             }
//             catch (Exception e)
//             {
// 
//             }
            //设置输出要素属性
            IDataset ds = (IDataset)pWorkspace;
            IWorkspaceName wsName = (IWorkspaceName)ds.FullName;
            IFeatureClassName featClsName = new FeatureClassNameClass();
            IDatasetName dsName = (IDatasetName)featClsName;
            dsName.WorkspaceName = wsName;
            dsName.Name = targetname;

            //// Use the IFieldChecker interface to make sure all of the field names are valid for a shapefile. 
            IFieldChecker fieldChecker = new FieldCheckerClass();
            IFields shapefileFields = null;
            IEnumFieldError enumFieldError = null;
            fieldChecker.InputWorkspace = inputDataset.Workspace;
            fieldChecker.ValidateWorkspace = pWorkspace;
            //out and ref
            //////////////////用Ref型参数时，传入的参数必须先被初始化。而Out则不要要，对Out而言，就必须在方法中对其完成初始化。
            // ///////////////用Ref和Out时都必须注意，在方法的参数和执行方法时，都要加Ref或Out关键字。以满足匹配。
            /////////////////// Out更适合用在要要Return多个返回值的地方，而Ref则用在要要被调出使用的方法修改调出使用者的引用的时候。
            fieldChecker.Validate(inputFeatureClass.Fields, out enumFieldError, out shapefileFields);

            // At this point, reporting/inspecting invalid fields would be useful, but for this example it's omitted.

            // We also need to retrieve the GeometryDef from the input feature class. 
            int shapeFieldPosition = inputFeatureClass.FindField(inputFeatureClass.ShapeFieldName);
            IFields inputFields = inputFeatureClass.Fields;

            IField shapeField = inputFields.get_Field(shapeFieldPosition);
            IGeometryDef geometryDef = shapeField.GeometryDef;

            IGeometryDef pGeometryDef = new GeometryDef();
            IGeometryDefEdit pGeometryDefEdit = pGeometryDef as IGeometryDefEdit;
            pGeometryDefEdit.GeometryType_2 = inputFeatureClass.ShapeType;
            IFeature pFeature=inputFeatureClass.Search(null,true).NextFeature();

            pGeometryDefEdit.SpatialReference_2 = pFeature.Shape.SpatialReference;

            // Now we can create a feature data converter. 
            IFeatureDataConverter featureDataConverter = new FeatureDataConverterClass();


            IEnumInvalidObject enumInvalidObject = featureDataConverter.ConvertFeatureClass(inputclassName, null,
                null, featClsName, pGeometryDef, shapefileFields, "", 1000, 0);

            // Again, checking for invalid objects would be useful at this point...

            inputFeatureClass = null;
            ds = null;
             

        }
        /// <summary>
        /// 查找数据集
        /// </summary>
        /// <param name="pEnumDataset"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IDataset GetDatasetByName(IEnumDataset pEnumDataset, string name) {


            IDataset pDataset = pEnumDataset.Next();
            while (pDataset != null)
            {
                if (pDataset.Type == esriDatasetType.esriDTContainer)
                {

                    IEnumDataset pEnumDs = pDataset.Subsets;
                    GetDatasetByName(pEnumDs, name);
                }
                else  
                {

                    if (pDataset.Name==name)
                    {
                        break;
                    }
                  

                }
                pDataset = pEnumDataset.Next();
            }
            return pDataset;
            
        }
        
    }

}
