using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AE_Environment.Model
{
    /// <summary>
    /// 类别指标
    /// </summary>
    public enum PatchClassIndex
    {
        TotalArea,//总面积
        AreaRatio,//面积占比
        TotalEgde,//总周长
        MaxAreaIndex,//最大面积
        EdgeDensity,//周长/面积
        MeshIndex,//破碎度
        UMeshIndex,//未受交通网络分割区域面积比率
        MMeshIndex,//修正的破碎度
        PAFRACIndex,//面积周长破碎度
        CWEDIndex,
        TECIIndex,
        NumberOfPatch,
        PatchDensity,
        MeanPatchSize

    }
    /// <summary>
    /// 景观指标
    /// </summary>
    public enum PatchLandIndex
    {
        TotalArea,//总面积
        AreaRatio,//面积占比
        TotalEgde,//总周长
        MaxAreaIndex,//最大面积
        EdgeDensity,//周长/面积
        MeshIndex,//破碎度
        UMeshIndex,//未受交通网络分割区域面积比率
        MMeshIndex,//修正的破碎度
        PAFRACIndex,//面积周长破碎度
        CWEDIndex,
        TECIIndex,
        NumberOfPatch,
        PatchDensity,
        MeanPatchSize

    }
}
