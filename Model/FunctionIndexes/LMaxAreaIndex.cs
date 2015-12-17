using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace AE_Environment.Model.FunctionIndexes
{
    class LMaxAreaIndex:Model.InterfaceLandIndex
    {

        public string name = "MaxAreaRatio";
        public string unit = "percent";
        public string Name()
        {
            return name;
        }

        public string Unit()
        {
            return unit;
        }

        public double CaculateLandIndex(ESRI.ArcGIS.Geodatabase.IFeatureCursor pFeatureCursor, BaseData basedata)
        {
           double result = 0.0,total=0.0;

            IFeature pFeature = null;

            while ((pFeature = pFeatureCursor.NextFeature()) != null)
            {
                double temparea = (double)pFeature.get_Value(basedata.areaIndex);
                total += temparea;
                if (result<=temparea)
                {
                    result = temparea;
                }
            }

            return result/total*100;
        
        }
    }
}
