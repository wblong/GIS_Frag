using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using ESRI.ArcGIS.Geodatabase;

namespace AE_Environment.Utilities
{
    class Common
    {
        public static void SetProgress(ProgressBar progress)
        {
            for (int i = 0; i < progress.Maximum*0.5;i++ )
            {
                if (progress.Value < progress.Maximum )
                {
                    progress.Value++;
                }
            }
            
        }
#region 获取唯一值

        /// <summary>
        /// 通过IDataStatistic获取要素类的唯一值
        /// </summary>
        /// <param name="pFeatureClass"></param>
        /// <param name="strFieldName"></param>
        /// <returns></returns>
        public static List<string> GetUVByDataStatistics(IFeatureClass pFeatureClass, string strFieldName)
        {
            List<string> uvList = new List<string>();
            IQueryFilter pQueryFilter = new QueryFilterClass();
            pQueryFilter.SubFields = strFieldName;
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(pQueryFilter, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = strFieldName;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();
            while (pEnumerator.MoveNext())
            {

                object obj = pEnumerator.Current;
                uvList.Add(obj.ToString());
            }
            return uvList;
        }
        /// <summary>
        /// 通过IQueryDef获取要素类的唯一值
        /// </summary>
        /// <param name="pFeatureClass"></param>
        /// <param name="strFieldName"></param>
        /// <returns></returns>
        public static List<string> GetUVByQueryDef(IFeatureClass pFeatureClass, string strFieldName)
        {
            List<string> uvList = new List<string>();
            IQueryDef pQueryDef;
            IRow pRow;
            ICursor pCursor;
            IFeatureWorkspace pFeatureWorkspace;
            IDataset pDataset;

            pDataset = pFeatureClass as IDataset;
            pFeatureWorkspace = pDataset.Workspace as IFeatureWorkspace;
            pQueryDef = pFeatureWorkspace.CreateQueryDef();
            pQueryDef.SubFields = "DISTINCT("+strFieldName+")";
            pQueryDef.Tables = pDataset.Name;
            pCursor = pQueryDef.Evaluate();
            pRow = pCursor.NextRow();
            while (pRow != null)
            {

                object obj = pRow.get_Value(0);
                uvList.Add(obj.ToString());
                pRow = pCursor.NextRow();

            }

            return uvList;
        }
        
#endregion
        

        /// <summary>
        /// 查找某一节点
        /// </summary>
        /// <param name="tnParent"></param>
        /// <param name="strValue"></param>
        /// <returns></returns>
      static  public TreeNode FindNode(TreeNode tnParent, string strValue)
        {
            if (tnParent == null) return null;
            if (tnParent.Text == strValue) return tnParent;

            TreeNode tnRet = null;
            foreach (TreeNode tn in tnParent.Nodes)
            {
                tnRet = FindNode(tn, strValue);
                if (tnRet != null) break;
            }
            return tnRet;
        }
        /// <summary>
        /// 根据文本查找某一节点
        /// </summary>
        /// <param name="tnParent"></param>
        /// <param name="strText"></param>
        /// <returns></returns>
       static public TreeNode FindNodeByText(TreeNode tnParent, string strText)
        {
            if (tnParent == null) return null;
            if (tnParent.Text == strText) return tnParent;

            TreeNode tnRet = null;
            foreach (TreeNode tn in tnParent.Nodes)
            {
                tnRet = FindNodeByText(tn, strText);
                if (tnRet != null) break;
            }
            return tnRet;
        }

       /// <summary>
       /// 加载分类码
       /// </summary>
       static public void LoadCcode(string filename)
       {

           MainForm.pData.Clear();
           System.IO.FileStream fileStream = null;
           StreamReader streamReader = null;
           fileStream = new System.IO.FileStream(filename, FileMode.Open, FileAccess.Read);
           streamReader = new StreamReader(fileStream, Encoding.Default);
           fileStream.Seek(0, SeekOrigin.Begin);
           string content = streamReader.ReadLine();
           while (content != null)
           {
               string key = content.Substring(0, content.LastIndexOf(",")).Trim();
               string value = content.Substring(content.LastIndexOf(",") + 1).Trim();
               MainForm.pData.Add(key, value);
               content = streamReader.ReadLine();
           }
           streamReader.Close();
           fileStream.Close();


       }
    }
}
