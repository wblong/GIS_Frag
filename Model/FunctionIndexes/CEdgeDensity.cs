﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace AE_Environment.Model.FunctionIndexes
{
    class CEdgeDensity:InterfaceClassIndex
    {
        public string name = "EdgeDensity";
        public string unit = "m_m2";
        public List<string> classvalue = null;//计算的类别
        public CEdgeDensity(List<string>_clssValue){

            classvalue = _clssValue;
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
            for (int i = 0; i < classvalue.Count; i++) { result.Add(0.0); }
            IFeature pFeature = null;
             double totalarea = 0.0;
            while ((pFeature = pFeatureCursor.NextFeature()) != null)
            {
                double tempedge = (double)pFeature.get_Value(basedata.perimeterIndex);
                double temparea = (double)pFeature.get_Value(basedata.areaIndex);
                for (int j = 0; j < classvalue.Count; j++)//分类
                {
                    string code = pFeature.get_Value(basedata.codeIndex).ToString();
                    if (code == classvalue[j])
                    {
                        result[j] += tempedge;
                        
                    }

                }
               totalarea += temparea;
            }
            for (int i = 0; i < classvalue.Count; i++)
            {
                result[i] = result[i] / totalarea;
                //result[i] = temp * 100;
            }
            return result;

            //throw new NotImplementedException();
        }
    }
}
