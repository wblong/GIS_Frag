using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AE_Environment.Model.EcoSystemIndex;
using AE_Environment.EcoSystemIndex;
using System.Data;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;

namespace AE_Environment.Model.EcosystemIndex
{
    class PatchEcosystem : Analysis
    {
        private DataTable dt_Popualtion = new DataTable();
        public static double population = 0;
        ZoneClassArea zoneArea;             //分区土地分类面积;
        List<List<string>> indexNameList;   //所要计算指标名称;
        List<List<double>> indexCalResult;  //指标计算的结果;

        CommonEcoIndex commonIndex;         //普通生态指标计算;    
        SpecialEcoIndex specialIndex;       //生态功能区生态指标计算;    
        NatureEcoIndex natureIndex;         //自然保护区生态指标计算;    

        public void Initialize(List<List<string>> _indexNameList)
        {
            indexNameList = _indexNameList;
            indexCalResult = new List<List<double>>();
        }
        public override bool BuildResultTable()
        {
            result_dt = new DataTable();
            try
            {
                #region 添加列名;
                DataColumn pDataColumn = null;
                pDataColumn = new DataColumn();
                pDataColumn.ColumnName = baseData.zidField;                     // 添加分区ID列标题;
                pDataColumn.DataType = System.Type.GetType("System.String");
                result_dt.Columns.Add(pDataColumn);

                List<string> indexName = new List<string>();
                if (indexNameList[0].Count > 0)
                {
                    commonIndex.GetIndexName();
                    indexName.AddRange(commonIndex.comEcoIndexName);
                }
                else if (indexNameList[1].Count > 0)
                {
                    specialIndex.GetIndexName();
                    indexName.AddRange(specialIndex.speEcoIndexName);
                }
                else if (indexNameList[2].Count > 0)
                {
                    natureIndex.GetIndexName();
                    indexName.AddRange(natureIndex.natEcoIndexName);
                }

                for (int i = 0; i < indexName.Count; i++)                            // 添加指标名称;
                {
                    DataColumn mDataColumn = new DataColumn();
                    mDataColumn.ColumnName = indexName[i];
                    mDataColumn.DataType = System.Type.GetType("System.String");
                    result_dt.Columns.Add(mDataColumn);
                }
                #endregion

                #region 添加数据
                DataRow pDataRow = null;
                List<double> temp;
                for (int i = 0; i < baseData.zoneValue.Count; i++)//分区
                {
                    pDataRow = result_dt.NewRow();
                    pDataRow[0] = baseData.zoneValue[i];
                    temp = indexCalResult[i];
                    for (int j = 0; j < indexName.Count; j++)//分指标
                    {
                        pDataRow[j + 1] = temp[j];
                    }
                    result_dt.Rows.Add(pDataRow);
                }
                #endregion
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public override bool CalculateIndex(ProgressBar progress)
        {
            try
            {
                progress.Maximum = baseData.zoneValue.Count;
                progress.Value = 0;
                zoneArea = new ZoneClassArea(baseData);
                for (int i = 0; i < baseData.zoneValue.Count; i++)
                {
                    List<double> temp = new List<double>();
                    int[] oidlist = baseData.patchIDArray[i];
                    using (ComReleaser comReleaser = new ComReleaser())
                    {
                        SCLASSAREA ClassArea = new SCLASSAREA();    //定义分区结构体;
                        //获取每个分区的要素集;
                        IGeoDatabaseBridge geodatabaseBridge = new GeoDatabaseHelperClass();
                        IFeatureCursor featureCursor = geodatabaseBridge.GetFeatures(baseData.zoneLCA_FC, ref oidlist, true);
                        
                        ClassArea = zoneArea.StatisticClassArea(featureCursor);
                        ClassArea.TotalArea = baseData.zoneArea[i];

                        //普通生态环境评价指标计算;
                        if (indexNameList[0].Count > 0)
                        {
                            dt_Popualtion = MainForm.dt_Population;
                            if (dt_Popualtion != null)
                            {
                                for (int j = 0; j < dt_Popualtion.Rows.Count;j++ )
                                {
                                    if (baseData.zoneValue[i].ToString() == dt_Popualtion.Rows[j][0].ToString())
                                    {
                                        population = Convert.ToDouble(dt_Popualtion.Rows[j][1].ToString());
                                    }
                                }
                            }
                            commonIndex = new CommonEcoIndex(ClassArea, indexNameList[0]);
                            commonIndex.ComEcoIndexCal();
                            temp.AddRange(commonIndex.comEcoIndexResult);
                        }

                        //生态功能区生态环境评价指标计算;
                        if (indexNameList[1].Count > 0)
                        {
                            specialIndex = new SpecialEcoIndex(ClassArea, indexNameList[1]);
                            specialIndex.SpecialEcoIndexCal();
                            temp.AddRange(specialIndex.speEcoIndexResult);
                        }
                        //自然保护区生态环境评价指标计算;
                        if (indexNameList[2].Count > 0)
                        {
                            natureIndex = new NatureEcoIndex(ClassArea, indexNameList[2]);
                            natureIndex.NatureEcoIndexCal();
                            temp.AddRange(natureIndex.natEcoIndexResult);
                        }
                        progress.Value++;
                    }
                    indexCalResult.Add(temp);
                }

            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
