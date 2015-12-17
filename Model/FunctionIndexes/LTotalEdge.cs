using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace AE_Environment.Model.FunctionIndexes
{
    class LTotalEdge:Model.InterfaceLandIndex
    {
        public string name="TotalEdge";
        public string unit="m";
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public LTotalEdge( ){

             
        }
        public string Name()
        {

            return name;
        }
        public string Unit()
        {

            return unit;
        }
        /// <summary>
        /// 计算
        /// </summary>
        /// <param name="pFeatureCursor"></param>
        /// <param name="basedata"></param>
        /// <returns></returns>
        public double CaculateLandIndex(IFeatureCursor pFeatureCursor,BaseData basedata)
        {
            double result =0.0;
          
            IFeature pFeature=null;
            
            while ((pFeature = pFeatureCursor.NextFeature()) != null)
            {
                double templength = (double)pFeature.get_Value(basedata.perimeterIndex);
                result += templength;
            }
            
            return result;

        }
    }
}
