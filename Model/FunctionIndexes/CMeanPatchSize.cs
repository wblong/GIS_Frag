using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace AE_Environment.Model.FunctionIndexes
{
    class CMeanPatchSize:InterfaceClassIndex
    {

        public string name = "CMeanPatchSize";
        public string unit = "ha";
        public List<string> clssValue = null;
        public CMeanPatchSize(List<string> _clssValue)
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
            //throw new NotImplementedException();
            List<double> result = new List<double>();
            List<int> count = new List<int>();
            for (int i = 0; i < clssValue.Count; i++)
            {
                result.Add(0.0);
                count.Add(0);
            }
            IFeature pFeature = null;
            double tmparea = 0.0;

            while ((pFeature = pFeatureCursor.NextFeature()) != null)
            {
                tmparea = (double)pFeature.get_Value(basedata.areaIndex);
                for (int j = 0; j < clssValue.Count; j++)
                {

                    string code = pFeature.get_Value(basedata.codeIndex).ToString();
                    if (code == clssValue[j])
                    {
                        count[j] += 1;
                        result[j] += tmparea;
                    }
                }
            }
             
            for (int i = 0; i < clssValue.Count; i++)
            {
                if (count[i]<0.01)
                {
                    result[i] = 0;
                }
                else
                {

                    result[i] = result[i] / 10000 / count[i];
                }
                
            }
            return result;
        }
    }
}
