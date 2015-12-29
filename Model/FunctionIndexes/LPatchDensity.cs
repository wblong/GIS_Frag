using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace AE_Environment.Model.FunctionIndexes
{
    class LPatchDensity:InterfaceLandIndex
    {
        public string name = "PatchDensity";
        public string unit = "num_100ha";
        public LPatchDensity() { }
        string InterfaceLandIndex.Name()
        {
            //throw new NotImplementedException();
            return name;
        }

        string InterfaceLandIndex.Unit()
        {
            //throw new NotImplementedException();
            return unit;
        }

        double InterfaceLandIndex.CaculateLandIndex(ESRI.ArcGIS.Geodatabase.IFeatureCursor pFeatureCursor, BaseData basedata)
        {
            //throw new NotImplementedException();
            double result = 0.0;
            IFeature pFeature = null;
            double count = 0.0;
            while ((pFeature=pFeatureCursor.NextFeature())!=null)
            {
                result += (double)pFeature.get_Value(basedata.areaIndex);
                count++;

            }
            result /= 1000000;
            if (result<0.01)
            {
                result = 0.0;
            }
            else
            {

                result = count / result;
            }
            return result;

        }
    }
}
