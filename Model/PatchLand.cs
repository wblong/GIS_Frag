using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using System.Data;
using System.Windows.Forms;

namespace AE_Environment.Model
{
    class PatchLand:Analysis
    {

        public List<PatchLandIndex> patchLandIndexes;//计算的指标
        public List<InterfaceLandIndex> patchLandCac;//接口函数
        public List<List<double>> result_list = null;//结果列表

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_codeValue"></param>
        /// <param name="_patchClassIndex"></param>
        public void InitData(List<PatchLandIndex> _patchLandIndexes, List<InterfaceLandIndex> _patchLandCac)
        {

             

            patchLandIndexes = _patchLandIndexes;
            patchLandCac = _patchLandCac;
            result_list = new List<List<double>>();
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
                progress.Maximum = zones.Count*patchLandCac.Count;
                for (int i = 0; i < zones.Count; i++)
                {
                    List<double> temp = new List<double>();
                    string zid = zones[i];
                    int[] oidlist = baseData.patchIDArray[i];
                    double zoneArea = baseData.zoneArea[i];
                    double zoneLength = 0.0;
                    double zoneAreaTrue = 0.0;
                    if (baseData.zone_FC!=null)
                    {
                       
                        zoneLength = (double)baseData.zone_FC.GetFeature(baseData.zoneObjectID[i]).get_Value(baseData.perimeterIndex_zone);
                        zoneAreaTrue = (double)baseData.zone_FC.GetFeature(baseData.zoneObjectID[i]).get_Value(baseData.areaIndex_zone);
                    }
                    
                   using (ComReleaser comReleaser = new ComReleaser())
                    {

                       
                        for (int j = 0; j < patchLandCac.Count; j++)
                        {

                            IGeoDatabaseBridge geodatabaseBridge = new GeoDatabaseHelperClass();
                            IFeatureCursor featureCursor = geodatabaseBridge.GetFeatures(baseData.zoneLCA_FC,
                                ref oidlist, true);
                            switch (patchLandCac[j].Name())
                            {
                                case "UMeshIndex":
                                    temp.Add(patchLandCac[j].CaculateLandIndex(featureCursor, baseData)/zoneAreaTrue*100);
                                    break;
                                case "MeshIndex":
                                    temp.Add(patchLandCac[j].CaculateLandIndex(featureCursor, baseData) / zoneAreaTrue * 0.000001);
                                    break;
                                case "MEffectMeshArea":
                                    temp.Add(patchLandCac[j].CaculateLandIndex(featureCursor, baseData) / zoneAreaTrue * 0.000001);
                                    break;
                                case "TotalEdge":
                                    temp.Add((patchLandCac[j].CaculateLandIndex(featureCursor, baseData)-zoneLength)*0.5+zoneLength);
                                    break;
                                case "TotalArea":
                                    temp.Add(zoneArea);
                                    break;
                                case "EdgeDensity":
                                    temp.Add(((patchLandCac[j].CaculateLandIndex(featureCursor, baseData) - zoneLength) * 0.5 + zoneLength)/zoneArea);
                                    break;
                                 default:
                                    temp.Add(patchLandCac[j].CaculateLandIndex(featureCursor, baseData));
                                    break;
                            }
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

               
                for (int j = 0; j < patchLandCac.Count;j++ )//分指标
                {
                    pDataColumn = new DataColumn();
                    //pDataColumn.ColumnName = "L_" + patchLandCac[j].Name() +"_"+ patchLandCac[j].Unit();
                    pDataColumn.ColumnName = "L_" + patchLandCac[j].Name() ;
                    pDataColumn.DataType = System.Type.GetType("System.Double");
                    result_dt.Columns.Add(pDataColumn);
                }

                
                //添加数据
                DataRow pDataRow = null;
                
                for (int i = 0; i < baseData.zoneValue.Count; i++)//分区
                {
                    List<double> zone=result_list[i];
                    pDataRow = result_dt.NewRow();
                    pDataRow[0] = baseData.zoneValue[i];
                    for (int k = 0; k < patchLandCac.Count; k++)//分指标
                    {
                         
                        pDataRow[k + 1] = zone[k];
                    }
                    result_dt.Rows.Add(pDataRow);
                  
                }
                
            }
            return isSuccess;
        }
    }
}
