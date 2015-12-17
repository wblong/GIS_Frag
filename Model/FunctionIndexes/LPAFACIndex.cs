using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.Geodatabase;

namespace AE_Environment.Model.FunctionIndexes
{
    class LPAFACIndex:InterfaceLandIndex
    {
        public string name = "PAFACIndex";
        public string unit = "1";

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
            double result = 0.0;
           
            double lnpij = 0.0;
            double lnaij = 0.0;
            double lnpij2 = 0.0;
            double lnaijlnpij = 0.0;
            int Count = 0;

             
            IFeature pFeature = null;

            while ((pFeature = pFeatureCursor.NextFeature()) != null)

            {
                Count++;
                double temparea = (double)pFeature.get_Value(basedata.areaIndex);
                double templength = (double)pFeature.get_Value(basedata.perimeterIndex);
                
                lnpij += System.Math.Log(templength, Math.E);
                lnaij += System.Math.Log(temparea, Math.E);
                lnpij2 += System.Math.Log(templength, Math.E) * System.Math.Log(templength, Math.E);
                lnaijlnpij += System.Math.Log(temparea, Math.E) * System.Math.Log(templength, Math.E);

                 
            }
            double temp1 = Count * lnpij2 - lnpij * lnpij;
            double temp2 = Count * lnaijlnpij - lnaij * lnpij;
            double temp = 2.0 / (temp2 / temp1);
            if (temp2 < 0.0000001 || temp1 < 0.0000001)
            {
                result = -9999;
            }
            else
            {
                result = temp;
            }
            return result;
        }
    }
}
