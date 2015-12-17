using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.Geodatabase;

namespace AE_Environment.Model.FunctionIndexes
{
    class CMaxAreaIndex:Model.InterfaceClassIndex
    {

        public string name = "MaxAreaRatio";
        public string unit = "percent";
        public List<string> classvalue = null;//计算的类别
        public CMaxAreaIndex(List<string>_clssValue){

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

        public List<double> CaculateClassIndex(ESRI.ArcGIS.Geodatabase.IFeatureCursor pFeatureCursor, BaseData basedata)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < classvalue.Count; i++) { result.Add(0.0); }
            IFeature pFeature = null;
            double totalArea = 0.0;
            while ((pFeature = pFeatureCursor.NextFeature()) != null)
            {
                double temparea = (double)pFeature.get_Value(basedata.areaIndex);
                for (int j = 0; j < classvalue.Count; j++)//分类
                {
                    string code = pFeature.get_Value(basedata.codeIndex).ToString();
                    if (code == classvalue[j])
                    {
                        if (result[j]<temparea)result[j]=temparea;
                        
                    }

                }
                totalArea += temparea;
            }
            for (int i = 0; i < classvalue.Count; i++)
            { 
                double temp=result[i]/  totalArea;
                result[i] = temp * 100;
            }
            return result;

            //throw new NotImplementedException();
        }
    }
}
