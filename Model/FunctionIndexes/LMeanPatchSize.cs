using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace AE_Environment.Model.FunctionIndexes
{
    class LMeanPatchSize:InterfaceLandIndex
    {
        public string name = "LMeshPatchSize";
        public string unit = "ha";
        public LMeanPatchSize() { }

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
            double result=0.0,count = 0.0;
            IFeature pFeature = null;
            while ((pFeature=pFeatureCursor.NextFeature())!=null)
            {
                result+= (double)pFeature.get_Value(basedata.areaIndex);
                count++;
            }
            if (count<0.01)
            {
                result = 0.0;

            }
            else
            {

                result = result / 10000 / count;
            }
            return result;
        }
    }
}
