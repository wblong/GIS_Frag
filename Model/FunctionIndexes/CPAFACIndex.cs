using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace AE_Environment.Model.FunctionIndexes
{
    class CPAFACIndex:InterfaceClassIndex
    {

        public string name = "PAFRACIndex";
        public string unit = "1";
        public List<string> classvalue = null;//计算的类别
        public CPAFACIndex(List<string> _classvalue)
        {

            classvalue = _classvalue;

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
            int count=classvalue.Count;
            double[] lnpij = new double[count];
            double[] lnaij = new double[count];
            double[] lnpij2 = new double[count];
            double[] lnaijlnpij = new double[count];
            int[] classCount = new int[count];

            for (int i = 0; i <count; i++) 
            {
                result.Add(0.0);
                lnpij[i] = 0.0;
                lnaij[i] = 0.0;
                lnpij2[i] = 0.0;
                lnaijlnpij[i] = 0.0;
                classCount[i] = 0;
            }

            IFeature pFeature = null;
            while ((pFeature = pFeatureCursor.NextFeature()) != null)
            {
                double temparea = (double)pFeature.get_Value(basedata.areaIndex);
                double templength = (double)pFeature.get_Value(basedata.perimeterIndex);

                for (int j = 0; j < classvalue.Count; j++)//分类
                {
                    string code = pFeature.get_Value(basedata.codeIndex).ToString();
                    if (code == classvalue[j])
                    {
                        classCount[j]++;
                        lnpij[j] += System.Math.Log(templength, Math.E);
                        lnaij [j]+= System.Math.Log(temparea, Math.E);
                        lnpij2[j] += System.Math.Log(templength, Math.E) * System.Math.Log(templength, Math.E);
                        lnaijlnpij[j] += System.Math.Log(templength, Math.E) * System.Math.Log(temparea, Math.E);
                    }

                }
                

            }///end of while
            for (int j = 0; j <count; j++)
            {
                double temp1 = classCount[j] * lnpij2[j] - lnpij[j] * lnpij[j];
                double temp2 = classCount[j] * lnaijlnpij[j] - lnaij[j] * lnpij[j];
                double temp=2.0/(temp2 /temp1);
                if (temp1<0.000001||temp2<0.000001)
                {
                    result[j] = -9999;
                }
                else
                {
                    result[j] = temp;

                }
               
            }

            return result;    
        }
    }
}
