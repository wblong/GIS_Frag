using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.Geodatabase;

namespace AE_Environment.Model.FunctionIndexes
{
    class LTotalArea:Model.InterfaceLandIndex
    {


        public string name="TotalArea";
        public string unit="m2";
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public LTotalArea( ){

             
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
        public double CaculateLandIndex(IFeatureCursor pFeatureCursor, BaseData basedata)
        {

            throw new NotImplementedException();
        }
    }
}
