using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace AE_Environment.Model.FunctionIndexes
{

    class CTotalArea : Model.InterfaceClassIndex
    {

        public string name="TotalArea";
        public string unit="m2";
        public List<string>classvalue=null;//计算的类别
        /// <summary>
        /// 构造函数
        /// </summary>
        public CTotalArea(List<string>_clssValue){

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
            }
            
            return result;

        }
    }
}
