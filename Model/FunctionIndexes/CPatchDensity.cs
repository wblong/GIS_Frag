using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace AE_Environment.Model.FunctionIndexes
{
    class CPatchDensity:InterfaceClassIndex
    {

        public string name = "PatchDensity";
        public string unit = "num_100ha";
        public List<string> clssValue = null;
        public CPatchDensity(List<string> _clssValue)
        {

            clssValue = _clssValue;
        }
        string InterfaceClassIndex.Name()
        {
            //throw new NotImplementedException();
            return name;
        }

        string InterfaceClassIndex.Unit()
        {
            //throw new NotImplementedException();
            return unit;
        }

        List<double> InterfaceClassIndex.CaculateClassIndex(ESRI.ArcGIS.Geodatabase.IFeatureCursor pFeatureCursor, BaseData basedata)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < clssValue.Count; i++)
            {
                result.Add(0.0);
            }
            IFeature pFeature = null;
            double tmparea = 0.0;

            while ((pFeature = pFeatureCursor.NextFeature()) != null)
            {
                tmparea += (double)pFeature.get_Value(basedata.areaIndex);
                for (int j = 0; j < clssValue.Count; j++)
                {

                    string code = pFeature.get_Value(basedata.codeIndex).ToString();
                    if (code == clssValue[j])
                    {
                        result[j] += 1;
                    }
                }
            }
            tmparea /= 1000000;
            for (int i = 0; i < clssValue.Count; i++)
            {

                result[i] /= tmparea;
            }
            return result;
        }
    }
}
