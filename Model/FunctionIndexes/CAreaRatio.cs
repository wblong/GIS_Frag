using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace AE_Environment.Model.FunctionIndexes
{
    /// <summary>
    /// 面积占比
    /// </summary>
    class CAreaRatio : Model.InterfaceClassIndex
    {
        public string name="AreaRatio";
        public string unit="percent";
        public List<string>classvalue=null;//计算的类别
        /// <summary>
        /// 构造函数
        /// </summary>
        public CAreaRatio(List<string>_clssValue){

            classvalue = _clssValue;
        }
        public string Name()
        {

            return name;
        }
        public string Unit()
        {

            return unit;
        }
        /// <summary>
        /// 计算
        /// </summary>
        /// <param name="pFeatureCursor"></param>
        /// <param name="basedata"></param>
        /// <returns></returns>
        public List<double> CaculateClassIndex(IFeatureCursor pFeatureCursor,BaseData basedata)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < classvalue.Count; i++) { result.Add(0.0); }
            IFeature pFeature=null;
            double totalArea=0.0;
            while ((pFeature = pFeatureCursor.NextFeature()) != null)
            {
                double temparea = (double)pFeature.get_Value(basedata.areaIndex);
                for (int j = 0; j < classvalue.Count; j++)//分类
                {
                    string code = pFeature.get_Value(basedata.codeIndex).ToString();
                    if (code == classvalue[j])
                    {
                        result[j] += temparea;
                    }

                }
                totalArea+=temparea;//分区总面积

            }///end of while
             for (int j=0;j<classvalue.Count;j++)
             {
                 result[j]=result[j]/totalArea*100;
             }
             
            return result;

        }
    }

    }

