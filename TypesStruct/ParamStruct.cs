using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using ESRI.ArcGIS.Geodatabase;


namespace AE_Environment.TypesStruct
{
    //输入数据
    public struct DataInputInfo
    {
        public IFeatureClass layerDataCover;   //覆盖数据
        public IFeatureClass layerDataZone;
        public string zoneField;               //分区ID字段
        public string codeField;               //分类码字段
        public List<string> clssValue;

        public int areaIndex;
        public int perimeterIndex;
        public int codeIndex;

        public List<string> zoneValue;
        public List<List<int>> clssObjectIDs;

        public List<double> zoneArea;

        public List<int> clss_pCount;

    }
    
     
    //类别指标参数 
   public struct ClassParamInfo
   {  

        public List<string>clss;           //分类码
        public List<ClassIndex> clssIndex;
        public DataTable resultTable;      //输出表

   }
    //景观指标参数
   public struct LandscapeParamInfo
   {
       public List<string> clss;
       public List<LandscapeIndex> landIndex;
       public DataTable resultTable;
   }
    /// <summary>
    /// 类指标
    /// </summary>
   public enum ClassIndex
   {
       TotalArea,//总面积
       AreaRatio,//面积比
       TotalEgde,//总周长
       EdgeDensity,//周长/面积
       MaxAreaIndex,//最大面积
       MeshIndex,//破碎度
       UMeshIndex,//未受交通网络分割区域面积比率
       MMeshIndex,//修正的破碎度
       PAFRACIndex,
       CWEDIndex,
       TECIIndex
    
   }
    /// <summary>
    /// 景观指标
    /// </summary>
   public enum LandscapeIndex
   {
       TotalArea,//总面积
       TotalEgde,//总周长
       MaxAreaIndex,//最大面积
       EdgeDensity,//周长/面积
       MeshIndex,//破碎度
       UMeshIndex,//未受交通网络分割区域面积比率
       MMeshIndex,//修正的破碎度
       PAFRACIndex,//面积周长破碎度
       CWEDIndex,
       TECIIndex

   }
}
