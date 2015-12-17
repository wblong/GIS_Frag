using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace AE_Environment.Model.FunctionIndexes
{
    class CNumberOfPatch:InterfaceClassIndex
    {

        public string name = "NumOfPatch";
        public string unit = "";
        public List<string> classvalue = null;
        public CNumberOfPatch(List<string> _clssValue)
        {

            classvalue = _clssValue;
        }
        string InterfaceClassIndex.Name()
        {
            return name;
            //throw new NotImplementedException();
        }

        string InterfaceClassIndex.Unit()
        {
            //throw new NotImplementedException();
            return unit;
        }

        List<double> InterfaceClassIndex.CaculateClassIndex(ESRI.ArcGIS.Geodatabase.IFeatureCursor pFeatureCursor, BaseData basedata)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < classvalue.Count;i++ )
            {
                result.Add(0.0);
            }
            IFeature pFeature = null;
            while((pFeature=pFeatureCursor.NextFeature())!=null){

                for (int j = 0; j < classvalue.Count;j++ )
                {

                    string code = pFeature.get_Value(basedata.codeIndex).ToString();
                    if (code==classvalue[j])
                    {
                        result[j] += 1;
                    }
                }
            }
            return result;
        }
    }
}
