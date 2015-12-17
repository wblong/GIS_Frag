using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;
using ESRI.ArcGIS.ADF;

namespace AE_Environment.Model.CustomIndex
{
    class CustomIndexCal
    {
        BaseData baseData = null;
        string strFormula;
        public CustomIndexCal(BaseData _baseData, string _strFormula)
        {
            baseData = _baseData;
            strFormula = _strFormula;
        }

        #region 查询数据
        public List<string> SearchData(IFeatureCursor featureCursor)
        {
            string[] strTemp = strFormula.Split(' ');
            List<string> query = new List<string>(strTemp);
            double[] data = new double[query.Count];

            string strA = "Area";
            string strP = "Perimeter";

            double tempArea = 0;
            double tempPerim = 0;
            double totalArea = 0;
            double totalPerim = 0;

            IFeature pFeature = null;

            #region 遍历当前分区的要素集，获取数据
            while ((pFeature = featureCursor.NextFeature()) != null)
            {
                string code = pFeature.get_Value(baseData.codeIndex).ToString();
                tempArea = (double)pFeature.get_Value(baseData.areaIndex);
                tempPerim = (double)pFeature.get_Value(baseData.perimeterIndex);
                totalArea += tempArea;
                totalPerim += tempPerim;
                for (int i = 0; i < query.Count;i++ )
                {
                    if (query[i].IndexOf(strA) > -1)
                    {
                        string str = query[i].Substring(query[i].IndexOf("'") + 1, query[i].LastIndexOf("'") - query[i].IndexOf("'") - 1);
                        if(code == str)
                        {
                            data[i] += tempArea;
                        }
                    }
                    else if (query[i].IndexOf(strP) > -1)
                    {
                        string str = query[i].Substring(query[i].IndexOf("'") + 1, query[i].LastIndexOf("'") - query[i].IndexOf("'") - 1);
                        if (code == str)
                        {
                            data[i] += tempPerim;
                        }
                    }
                }
            }
            #endregion


            #region 将查询到的数据替换原来的查询条件
            for (int i = 0; i < query.Count;i++ )
            {
                if (query[i].IndexOf(strA) > -1 || query[i].IndexOf(strP) > -1)
                {
                    string str = query[i].Substring(query[i].IndexOf("'") + 1, query[i].LastIndexOf("'") - query[i].IndexOf("'") - 1);
                    if (str == "0000")
                    {
                        if (query[i].IndexOf(strA) > -1)
                        {
                            data[i] = totalArea;
                        }
                        else if (query[i].IndexOf(strP) > -1)
                        {
                            data[i] = totalPerim;
                        }
                    }
                    query.RemoveAt(i);
                    query.Insert(i, Convert.ToString(data[i]));
                }
            }
            #endregion

            return query;
        }
        #endregion


    }

}
