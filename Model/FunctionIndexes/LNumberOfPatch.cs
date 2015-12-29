using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace AE_Environment.Model.FunctionIndexes
{
    class LNumberOfPatch:InterfaceLandIndex
    {
        public string name = "NumberOfPatch";
        public string unit = "";
        public LNumberOfPatch()
        {


        }
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
           // throw new NotImplementedException();
            double result = 0.0;
            IFeature pFeature = null;
            
            while ((pFeature = pFeatureCursor.NextFeature()) != null)
            {
                result++;

            }
            return result;
        }
    }
}
