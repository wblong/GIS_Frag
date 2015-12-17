using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace AE_Environment.Model.FunctionIndexes
{
    class LUMeshIndex:InterfaceLandIndex
    {
        public string name="UMeshIndex";
        public string unit="percent";
        private int limitevalue = 0;
        /// <summary>
        /// 构造函数
        /// </summary>
        public LUMeshIndex(int _limitevalue ){

            this.limitevalue = _limitevalue*1000000;
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
                double temparea = (double)pFeature.get_Value(basedata.areaIndex);
               
                if (temparea>=limitevalue)
                {
                    result += temparea;
                }
               
            }

            return result;

        }
    }
}
