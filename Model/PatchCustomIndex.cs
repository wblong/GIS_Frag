using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AE_Environment.Model.CustomIndex;

namespace AE_Environment.Model
{
    class PatchCustomIndex : Analysis
    {
        string indexName;                       //所要计算指标名称;
        string strFormula;                      //原始计算公式;
        List<double> resultList;                //计算的结果;
        private CustomIndexCal customCal;       //自定义指标计算;


        public void Initialize(string _indexName,string _strFormula)
        {
            indexName = _indexName;
            strFormula = _strFormula;
            resultList = new List<double>();
            customCal = new CustomIndexCal(baseData, strFormula);
        }

        #region 重写构建结果表
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

                DataColumn mDataColumn = new DataColumn();
                mDataColumn.ColumnName = indexName;
                mDataColumn.DataType = System.Type.GetType("System.String");
                result_dt.Columns.Add(mDataColumn);
                #endregion

                #region 添加数据
                DataRow pDataRow = null;
                for (int i = 0; i < baseData.zoneValue.Count; i++)//分区
                {
                    pDataRow = result_dt.NewRow();
                    pDataRow[0] = baseData.zoneValue[i];
                    pDataRow[1] = resultList[i];
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
        #endregion

        public override bool CalculateIndex()
        {
            if (indexName == "" || strFormula == "")
            {
                return false;
            }
            try
            {
                resultList = customCal.CalculatorIndex();
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
