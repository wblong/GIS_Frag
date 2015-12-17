using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace AE_Environment.Model
{
    /// <summary>
    /// 类别计算接口
    /// </summary>
    public interface InterfaceClassIndex
    {
        string Name();
        string Unit();
       List<double>CaculateClassIndex(IFeatureCursor pFeatureCursor,BaseData basedata);
    }
    /// <summary>
    /// 景观指标计算接口
    /// </summary>
    public interface InterfaceLandIndex
    {
        string Name();
        string Unit();
        double CaculateLandIndex(IFeatureCursor pFeatureCursor,BaseData basedata);
    }
}
