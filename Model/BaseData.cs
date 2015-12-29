using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using System.Collections;
using System.Windows.Forms;
using System.Threading;

namespace AE_Environment.Model
{
    /// <summary>
    /// 基本数据类
    /// </summary>
  public  class BaseData
    {
       
        public IFeatureClass zoneLCA_FC = null; //覆盖相交数据
        public IFeatureClass zone_FC = null;
        public string zidField = null;           //分区ID字段
        public string codeField = null;          //分类码字段

        public List<string> ccValue = null;       //编码唯一值
        public List<string> zoneValue = null;     //分区ZID唯一值
        public List<int> zoneObjectID = null;
        public List<double> zoneArea = null;       //分区面积
        public List<List<int>> patchIDs = null;    //分区中斑块IDs
        public List<int[]> patchIDArray = null;


        public int areaIndex;                    //面积字段的索引
        public int perimeterIndex;               //周长字段的索引
        public int codeIndex;                    //编码字段的索引
        public int perimeterIndex_zone;
        public int areaIndex_zone;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseData(IFeatureClass pFeatureClass, string zidField, string codeField)
        {
            zoneLCA_FC = pFeatureClass;
            this.zidField = zidField;
            this.codeField = codeField;

            ccValue = new List<string>();
            zoneValue = new List<string>();
            zoneObjectID = new List<int>();

            zoneArea = new List<double>();
            patchIDs = new List<List<int>>();
            patchIDArray = new List<int[]>();
           
        }
      /// <summary>
      /// 设置分区数据
      /// </summary>
      /// <param name="pFeatureClass"></param>
        public void SetZoneData(IFeatureClass pFeatureClass)
        {

            this.zone_FC = pFeatureClass;
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitData(object progress1)
        {
            ProgressBar progress = (ProgressBar)progress1;
           
            //耗时巨大的代码  
           // MainForm.baseData.InitData(progressBar1);
        
            //////////////////////////////////////////////////////////////////////////
            //建立分区索引
            IEnumIndex pEnumIndex = zoneLCA_FC.Indexes.FindIndexesByFieldName(zidField);
            IIndex pTemIndex= pEnumIndex.Next();
            if (pTemIndex==null)
            {

                IIndex pIndex = new IndexClass();
                IIndexEdit pIndexEdit = pIndex as IIndexEdit;
                IFields pFields = new FieldsClass();
                IFieldsEdit pFieldsEdit = pFields as IFieldsEdit;
                int zidindex = zoneLCA_FC.Fields.FindField(zidField);
                IField pField = zoneLCA_FC.Fields.Field[zidindex];
                pFieldsEdit.FieldCount_2 = 1;
                pFieldsEdit.set_Field(0, pField);

                pIndexEdit.Fields_2 = pFields;
                pIndexEdit.Name_2 = zidField;
                pIndexEdit.IsAscending_2 = true;

                zoneLCA_FC.AddIndex(pIndex);
            }
            //建立分类索引
            IEnumIndex pEnumIndex1 = zoneLCA_FC.Indexes.FindIndexesByFieldName(codeField);
            IIndex pTemIndex1= pEnumIndex1.Next();
            if (pTemIndex == null)
            {

                IIndex pIndex = new IndexClass();
                IIndexEdit pIndexEdit = pIndex as IIndexEdit;
                IFields pFields = new FieldsClass();
                IFieldsEdit pFieldsEdit = pFields as IFieldsEdit;
                int codeindex = zoneLCA_FC.Fields.FindField(codeField);
                IField pField = zoneLCA_FC.Fields.Field[codeindex];
                pFieldsEdit.FieldCount_2 = 1;
                pFieldsEdit.set_Field(0, pField);

                pIndexEdit.Fields_2 = pFields;
                pIndexEdit.Name_2 = codeField;
                pIndexEdit.IsAscending_2 = true;

                zoneLCA_FC.AddIndex(pIndex);
            }

            areaIndex = zoneLCA_FC.FindField("Shape_Area");
            perimeterIndex = zoneLCA_FC.FindField("Shape_Length");

            if (zone_FC!=null)
            {
                areaIndex_zone = zone_FC.FindField("Shape_Area");
                perimeterIndex_zone = zone_FC.FindField("Shape_Length");
            }
           
            codeIndex = zoneLCA_FC.FindField(codeField);
            Utilities.Common.SetProgress(progress);

            IQueryDef pQueryDef;
            IRow pRow;
            ICursor pCursor;
            IWorkspace pWorkspace;
            IFeatureWorkspace pFeatureWorkspace;
            IDataset pDataset;
            pDataset = zoneLCA_FC as IDataset;
            pWorkspace = pDataset.Workspace;
            pFeatureWorkspace = pDataset.Workspace as IFeatureWorkspace;
            pQueryDef = pFeatureWorkspace.CreateQueryDef();

            //////////////////////////////////////////////////////////////////////////
            //添加分区唯一值
            pQueryDef.SubFields = "DISTINCT(" + zidField + ")";
            pQueryDef.Tables = pDataset.Name;
            pCursor = pQueryDef.Evaluate();
            pRow = pCursor.NextRow();
            while (pRow != null)
            {

                object obj = pRow.get_Value(0);
                zoneValue.Add(obj.ToString());
                pRow = pCursor.NextRow();
                if (progress.Value < progress.Maximum)
                {
                    progress.Value++;
                }
               
            }
            //////////////////////////////////////////////////////////////////////////
            //添加 分类唯一值
            pQueryDef.SubFields = "DISTINCT(" + codeField + ")";
            pQueryDef.Tables = pDataset.Name;
            pCursor = pQueryDef.Evaluate();
            pRow = pCursor.NextRow();
            while (pRow != null)
            {

                object obj = pRow.get_Value(0);
                ccValue.Add(obj.ToString());
                pRow = pCursor.NextRow();
                if (progress.Value < progress.Maximum)
                {
                    progress.Value++;
                }
            }


            if (zone_FC!=null)
            {

                string objectFiled = "";
                objectFiled = zone_FC.Fields.Field[0].Name;
                pQueryDef.SubFields = (zone_FC as IDataset).Name + "."+objectFiled;
                //pQueryDef.SubFields = pDataset.Name + "." + zidField + "," + (zone_FC as IDataset).Name + "." + zidField + "," + (zone_FC as IDataset).Name + ".OBJECTID";
                pQueryDef.Tables =  (zone_FC as IDataset).Name;
                for (int i = 0; i < zoneValue.Count; i++)
                {
                    pQueryDef.WhereClause = zidField + " = " + "'" + zoneValue[i]+"'";
                    pCursor = pQueryDef.Evaluate();
                    pRow = pCursor.NextRow();

                    while (pRow != null)
                    {
                        object obj1 = pRow.get_Value(0);
                        //  zoneValue.Add(obj.ToString());
                        zoneObjectID.Add((int)obj1);
                        pRow = pCursor.NextRow();
                        if (progress.Value < progress.Maximum)
                        {
                            progress.Value++;
                        }
                    }
                }
            }

            string objectFiled1 = "";
            objectFiled1 = zoneLCA_FC.Fields.Field[0].Name;
          
            /////////////////////////
            //1
            pQueryDef.SubFields = objectFiled1+",Shape_Area";
            pQueryDef.Tables = pDataset.Name;

            for (int i = 0; i < zoneValue.Count; i++)
            {
                 
                double area = 0.0;
                pQueryDef.WhereClause = zidField + "=\'" + zoneValue[i] + "\'";
                pCursor = pQueryDef.Evaluate();
                pRow = pCursor.NextRow();
                List<int> pids = new List<int>();
                while (pRow != null)
                {

                    area += (double)pRow.get_Value(1);
                    pids.Add((int)pRow.get_Value(0));
                    pRow = pCursor.NextRow();
//                     progress.Invoke((MethodInvoker)delegate()
//                     {
//                         if (progress.Value < progress.Maximum)
//                         {
//                             progress.Value++;
//                         }
//                     });
                    if (progress.Value < progress.Maximum)
                    {
                        progress.Value++;
                    }
                }
                zoneArea.Add(area);
                patchIDs.Add(pids);



            }
             
            for (int i = 0; i < patchIDs.Count; i++)
            {

                patchIDArray.Add(GetZonePids(i));
//                 progress.Invoke((MethodInvoker)delegate()
//                 {
//                     if (progress.Value < progress.Maximum)
//                     {
//                         progress.Value++;
//                     }
//                 });
                if (progress.Value < progress.Maximum)
                {
                    progress.Value++;
                }
            }


            
        }
        /// <summary>
        /// 获取某一个分区所有斑块的IDs
        /// </summary>
        /// <param name="zid"></param>
        /// <returns></returns>
        public int[] GetZonePids(int zid)
        {

            int count = patchIDs[zid].Count;
            int []oidlist = new int[count];
            for (int i = 0; i < count; i++)
            {
                oidlist[i] = patchIDs[zid][i];
            }
            return oidlist;
        }
      /// <summary>
      /// 获取唯一值从列表中
      /// </summary>
      /// <param name="list"></param>
      /// <returns></returns>
        public  List<string> GetDistinctValues(List<string> list)
        {
            Hashtable hTable = new Hashtable();
            foreach (string str in list)
            {
                try
                {
                    hTable.Add(str, string.Empty);
                }
                catch { }
            }
            object[] objArray = new object[hTable.Keys.Count];
            List<string> temp = new List<string>();
            hTable.Keys.CopyTo(objArray, 0);
            for (int i = 0; i < hTable.Keys.Count;i++ )
            {
                temp.Add(objArray[i].ToString());
            }
            return temp;
        }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="pFeatureClass"></param>
      /// <param name="strFieldName"></param>
      /// <returns></returns>
        public  List<string> GetUVByQueryDef(IFeatureClass pFeatureClass, string strFieldName)
        {
            List<string> uvList = new List<string>();
            List<int> zoneids = new List<int>();

            IQueryDef pQueryDef;
            IRow pRow;
            ICursor pCursor;
            IFeatureWorkspace pFeatureWorkspace;
            IDataset pDataset;

            pDataset = pFeatureClass as IDataset;
            pFeatureWorkspace = pDataset.Workspace as IFeatureWorkspace;
            pQueryDef = pFeatureWorkspace.CreateQueryDef();
            pQueryDef.SubFields = "DISTINCT(" + pDataset.Name + "." + strFieldName + ")," + (zone_FC as IDataset).Name + "." + strFieldName + "," + (zone_FC as IDataset).Name + ".OBJECTID";
            pQueryDef.Tables = pDataset.Name + " INNER JOIN " + (zone_FC as IDataset).Name + " ON " + pDataset.Name + "." + strFieldName+"="+(zone_FC as IDataset).Name + "." + strFieldName ;
            pCursor = pQueryDef.Evaluate();
            pRow = pCursor.NextRow();
            while (pRow != null)
            {

                object obj = pRow.get_Value(0);
                object obj1 = pRow.get_Value(2);
                uvList.Add(obj.ToString());
                zoneids.Add((int)obj1);
                pRow = pCursor.NextRow();

            }

            return uvList;
        }
    }
}
