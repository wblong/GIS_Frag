using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using System.IO;

namespace AE_Environment.Model
{
   public class ExportClass
   {
       private DataTable dt = null;
       public ExportClass(DataTable _dt)
       {

           this.dt = _dt;
       }
       /// <summary>
       /// 创建数据表
       /// </summary>
       /// <param name="_TablePath"></param>
       /// <param name="_TableName"></param>
       /// <returns></returns>
       public ITable CreateTable(string _TablePath, string _TableName)
       {
           
           IWorkspaceFactory pWks = new FileGDBWorkspaceFactoryClass();
           IFeatureWorkspace pFwk = pWks.OpenFromFile(_TablePath, 0) as IFeatureWorkspace;

           ESRI.ArcGIS.Geodatabase.IObjectClassDescription objectClassDescription = new ESRI.ArcGIS.Geodatabase.ObjectClassDescriptionClass();
           IFields pTableFields = objectClassDescription.RequiredFields;
           IFieldsEdit pTableFieldsEdit = pTableFields as IFieldsEdit;
           IField pField = null;
           IFieldEdit pFieldEdit = null;

           for (int i = 0; i < dt.Columns.Count;i++ )
           {
               DataColumn dc=dt.Columns[i];
               pField = new FieldClass();
               pFieldEdit = pField as IFieldEdit;
               pFieldEdit.Name_2 = dc.ColumnName;
               switch(dc.DataType.ToString()){
                   case "System.Double":
                       pFieldEdit.Type_2=esriFieldType.esriFieldTypeDouble;
                       break;
                   case "System.String":
                       pFieldEdit.Type_2=esriFieldType.esriFieldTypeString;
                       break;

               }
               pTableFieldsEdit.AddField(pField);
                
           }

           ITable pTable = pFwk.CreateTable(_TableName, pTableFields, null, null, "");
           return pTable;
       }


       /// <summary>
       /// 导出数据shp
       /// </summary>
       /// <param name="_TablePath"></param>
       /// <param name="_TableName"></param>
       /// <returns></returns>
       public bool ExportIFeatureClass(string _TablePath, string _TableName, ProgressBar progress)
       {

           bool isSuccess = true;
           ITable pTable = CreateTable(_TablePath, _TableName);
           DataRow row = null;
           progress.Minimum = 0;

           for (int i = 0; i < dt.Rows.Count; i++)
           {
               row = dt.Rows[i];
               IRow pRow = pTable.CreateRow();
               for (int j = 0; j < dt.Columns.Count; j++)
               {
                   pRow.set_Value(j + 1, row[dt.Columns[j]]);
               }
               progress.Value++;
               pRow.Store();
           }
           //测试//复制并添加属性链接功能
           IFeatureClass pFeatureClass = MainForm.baseData.zone_FC;
           IFeatureClass pFC = IFeatureDataConverter_ConvertFeatureClass((pFeatureClass as IDataset).Workspace, (pTable as IDataset).Workspace, pFeatureClass.AliasName, _TableName + "_shp");
           ESRI.ArcGIS.DataManagementTools.JoinField pJoinField = new ESRI.ArcGIS.DataManagementTools.JoinField();
           pJoinField.in_data = pFC;
           pJoinField.in_field = MainForm.baseData.zidField;
           pJoinField.join_table = pTable;
           pJoinField.join_field = MainForm.baseData.zidField;
           ESRI.ArcGIS.Geoprocessor.Geoprocessor gp = new ESRI.ArcGIS.Geoprocessor.Geoprocessor();
           gp.OverwriteOutput = true;
           gp.Execute(pJoinField, null);
           //删除数据表
           IDataset pDataSet = pTable as IDataset;
           pDataSet.Delete();
          
           return isSuccess;
       }
       ////数据复制
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
           //create target dataset name   
           IFeatureClassName targetFeatureClassName = new FeatureClassNameClass();
           IDatasetName targetDatasetName = (IDatasetName)targetFeatureClassName;
           targetDatasetName.WorkspaceName = targetWorkspaceName;
           targetDatasetName.Name = nameOfTargetFeatureClass;
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
       /// 导出数据DBF
       /// </summary>
       /// <param name="_TablePath"></param>
       /// <param name="_TableName"></param>
       /// <returns></returns>
       public bool ExportITableClass(string _TablePath,string _TableName,ProgressBar progress)
       {
           bool isSuccess = true;
           ITable pTable = CreateTable(_TablePath, _TableName);
           DataRow row = null;
          
           for (int i = 0; i < dt.Rows.Count;i++ )
           {
               row = dt.Rows[i];
               IRow pRow = pTable.CreateRow();
               for (int j = 0; j < dt.Columns.Count;j++ )
               {
                   pRow.set_Value(j+1, row[dt.Columns[j]]);
               }
               progress.Value++;
               pRow.Store();
           }
           return isSuccess;
       }
       /// <summary>
       /// 导出CSV文件
       /// </summary>
       /// <param name="fileName"></param>
       /// <returns></returns>
       public bool ExportCSV(string fileName)
       {

           bool isSuccess = true;
           try
           {

               CreateCSV(fileName);
           }
           catch (Exception ex)
           {

               isSuccess = false;
           }

           return isSuccess;
              
               
           

       }
       public  void CreateCSV(string fileName)
       {
           System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
           StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
           string data = "";
           //写出列名称
           for (int i = 0; i < dt.Columns.Count; i++)
           {
               data += dt.Columns[i].ColumnName.ToString();
               if (i < dt.Columns.Count - 1)
               {
                   data += ",";
               }
           }
           sw.WriteLine(data);
           //写出各行数据
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               data = "";
               for (int j = 0; j < dt.Columns.Count; j++)
               {
                   data += dt.Rows[i][j].ToString();
                   if (j < dt.Columns.Count - 1)
                   {
                       data += ",";
                   }
               }
               sw.WriteLine(data);
           }
           sw.Close();
           fs.Close();

       }
    }
}
