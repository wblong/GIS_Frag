using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace AE_Environment.Model.FunctionIndexes
{
    class LEdgeDensity:InterfaceLandIndex
    {

        public string name = "EdgeDensity";
        public string unit = "m/m2";
        string InterfaceLandIndex.Name()
        {
            return name;
        }

        string InterfaceLandIndex.Unit()
        {
            return unit;
        }

        double InterfaceLandIndex.CaculateLandIndex(ESRI.ArcGIS.Geodatabase.IFeatureCursor pFeatureCursor, BaseData basedata)
        {
            double result = 0.0;

            IFeature pFeature = null;

            while ((pFeature = pFeatureCursor.NextFeature()) != null)
            {
                double templength = (double)pFeature.get_Value(basedata.perimeterIndex);
                result += templength;
            }

            return result;
        }
    }
}
