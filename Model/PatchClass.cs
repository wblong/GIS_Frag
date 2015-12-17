using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;

namespace AE_Environment.Model
{
   /// <summary>
   /// 类别尺度
   /// </summary>
    class PatchClass:  Analysis
    {
        public List<string> codeValues;//统计的分类
        public List<PatchClassIndex> patchClassIndexes;//计算的指标
        public List<InterfaceClassIndex> patchClassCac;//接口函数
        public List<List<List<double>>>result_list = null;//结果列表
       
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_codeValue"></param>
        /// <param name="_patchClassIndex"></param>
        public void InitData(List<string> _codeValues,List<PatchClassIndex>_patchClassIndexes,List<InterfaceClassIndex>_patchClassCac)
        {

            codeValues = _codeValues;

            patchClassIndexes = _patchClassIndexes;
            patchClassCac = _patchClassCac;
            result_list = new List<List<List<double>>>();
        }
        /// <summary>
        /// 重写计算指标
        /// </summary>
        /// <returns></returns>
        public override bool CalculateIndex(ProgressBar progress)
        {
            try
             {
	             //计算每一个分区
                 
                 List<string> zones = baseData.zoneValue;
                 progress.Maximum = zones.Count * patchClassCac.Count;
                 for (int i = 0; i < zones.Count; i++)
                 {
                     List<List<double>> temp = new List<List<double>>();
                     string zid = zones[i];
                     int[] oidlist=baseData.patchIDArray[i];

                     using (ComReleaser comReleaser = new ComReleaser())
                     {



                         for (int j = 0; j < patchClassCac.Count; j++)
                         {

                             IGeoDatabaseBridge geodatabaseBridge = new GeoDatabaseHelperClass();
                             IFeatureCursor featureCursor = geodatabaseBridge.GetFeatures(baseData.zoneLCA_FC,
                                 ref oidlist, true);
                             temp.Add(patchClassCac[j].CaculateClassIndex(featureCursor, baseData));
                             progress.Value++;
                         }
                         
                 }

                     result_list.Add(temp);

                 }
             }
             catch (System.Exception ex)
             {
                 return false;
             }



            return true;
             
        }
        /// <summary>
        /// 重写构建结果表
        /// </summary>
        /// <returns></returns>
        public override bool BuildResultTable()
        {
            bool isSuccess = true;
            if (result_list.Count==0)
            {
                isSuccess = false;
            }
            else
            {

                result_dt = new DataTable();
                DataColumn pDataColumn = null;


                pDataColumn = new DataColumn();
                pDataColumn.ColumnName = baseData.zidField;
                pDataColumn.DataType = System.Type.GetType("System.String");
                result_dt.Columns.Add(pDataColumn);

                for (int i = 0; i < patchClassCac.Count;i++ )//分指标
                {
                    for (int j = 0; j < codeValues.Count;j++ )//分类
                    {
                        pDataColumn = new DataColumn();
                        //pDataColumn.ColumnName = "C_" + codeValues[j] + "_" + patchClassCac[i].Name() + "_" + patchClassCac[i].Unit();
                        pDataColumn.ColumnName = "C_" + codeValues[j] + "_" + patchClassCac[i].Name();
                        pDataColumn.DataType = System.Type.GetType("System.Double");
                        result_dt.Columns.Add(pDataColumn);
                    }

                }
                //添加数据
                DataRow pDataRow = null;
                
                for (int i = 0; i < baseData.zoneValue.Count; i++)//分区
                {
                    List<List<double>>zone=result_list[i];
                    pDataRow = result_dt.NewRow();
                    pDataRow[0] = baseData.zoneValue[i];
                    int count=0 ;
                    for (int k = 0; k < patchClassCac.Count; k++)//分指标
                    {
 
                        List<double> indicates = zone[k];
                        for (int j = 0; j < indicates.Count;j++ )
                        {
                            count++;
                            pDataRow[count] = indicates[j];
                            
                        }
                    }
                    result_dt.Rows.Add(pDataRow);
                  
                }
                
            }
            return isSuccess;
        }
    }
}
