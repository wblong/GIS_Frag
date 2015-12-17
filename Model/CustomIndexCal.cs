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
        Calculator calculator;
        public CustomIndexCal(BaseData _baseData, string _strFormula)
        {
            baseData = _baseData;
            strFormula = _strFormula;
            calculator = new Calculator();
        }

        public List<double> CalculatorIndex()
        {
            string strA = "Area";
            string strP = "Perimeter";
            List<double> result = new List<double>();               //计算结果;
            List<List<double>> data = new List<List<double>>();     //根据查询条件获取每个分区所需的数据;
            List<List<string>> dataList = new List<List<string>>(); //替换查询条件后的数据集;
            List<string> query = new List<string>();


            string[] str = strFormula.Split(' ');
            List<string> arrayList = new List<string>(str);

            #region 获取计算公式中的查询条件
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i].Contains(strA) || str[i].Contains(strP))
                {
                    query.Add(str[i]);
                }
            }
            #endregion

            #region 根据查询到的数据替换查询条件，存储为一个二维表，每行存储计算所需的数据和运算符
            data = GetData(query);
            for (int i = 0; i < data.Count; i++)
            {
                List<double> temp = data[i];
                int count = 0;
                for (int j = 0; j < str.Length; j++)
                {
                    if (str[j].Contains(strA) || str[j].Contains(strP))
                    {
                        arrayList.RemoveAt(j);
                        arrayList.Insert(j, temp[count].ToString());
                        count++;
                    }

                }
                dataList.Add(arrayList);
            }
            #endregion

            #region 分区计算各自的指标
            for (int i = 0; i < dataList.Count; i++)
            {
                List<string> temp = dataList[i];
                double intdex = calculator.CalculateFormula(temp);
                result.Add(intdex);
            }
            #endregion

            return result;
        }

        #region 根据查询条件分区获得所需的数据
        private List<List<double>> GetData(List<string> query)
        {
            string strA = "Area";
            string strP = "Perimeter";
            List<List<double>> data = null;

            #region 查询每个分区的所需数据
            for (int i = 0; i < baseData.zoneValue.Count; i++)
            {
                int[] oidlist = baseData.patchIDArray[i];
                List<double> eachArea = null;
                using (ComReleaser comReleaser = new ComReleaser())
                {
                    IGeoDatabaseBridge geodatabaseBridge = new GeoDatabaseHelperClass();
                    //每个分区的要素集;
                    IFeatureCursor featureCursor = geodatabaseBridge.GetFeatures(baseData.zoneLCA_FC,
                        ref oidlist, true);
                    IFeature pFeature = null;

                    #region 根据计算公式中的查询条件获取当前分区的数据
                    for (int j = 0; j < query.Count; j++)
                    {
                        double zoneData = 0;
                        double tempData = 0;
                        string str = query[j].Substring(query[j].IndexOf("'") + 1, query[j].LastIndexOf("'") - query[j].IndexOf("'") - 1);
                        if (str == "0000")
                        {
                            //确认查询的是面积;
                            if (query[j].IndexOf(strA) > -1)
                            {
                                tempData = baseData.areaIndex;
                            }
                            //确认查询的是周长;
                            else if (query[j].IndexOf(strP) > -1)
                            {
                                tempData = baseData.perimeterIndex;
                            }
                            eachArea.Add(tempData);
                        }
                        else
                        {
                            while ((pFeature = featureCursor.NextFeature()) != null)
                            {
                                //确认查询的是面积;
                                if (query[j].IndexOf(strA) > -1)
                                {
                                    tempData = (double)pFeature.get_Value(baseData.areaIndex);
                                }
                                //确认查询的是周长;
                                else if (query[j].IndexOf(strP) > -1)
                                {
                                    tempData = (double)pFeature.get_Value(baseData.perimeterIndex);
                                }
                                string code = pFeature.get_Value(baseData.codeIndex).ToString();
                                if (code == str)
                                {
                                    zoneData += tempData;
                                }
                            }
                            eachArea.Add(zoneData);
                        }
                    }
                    #endregion
                }
                data.Add(eachArea);
            }
            #endregion
            return data;
        }
        #endregion

    }
}
