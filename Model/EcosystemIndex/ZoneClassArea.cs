using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using AE_Environment.Model;
using ESRI.ArcGIS.Geodatabase;
using System.Text.RegularExpressions;
using ESRI.ArcGIS.ADF;

namespace AE_Environment.Model.EcoSystemIndex
{
    class ZoneClassArea
    {
        private BaseData baseData;  //输入基本数据

        public ZoneClassArea(BaseData _baseData)
        {
            baseData = _baseData;
        }

        #region 计算每个分区的土地分类面积
        public SCLASSAREA StatisticClassArea(IFeatureCursor featureCursor)
        {
            SCLASSAREA ClassArea = new SCLASSAREA();

            #region 定义土地覆盖一级类查询条件
            Regex FarmLandArea = new Regex("^0(1|2)");              //识别耕地;
            Regex ForesLandArea = new Regex("^03");                 //识别林地;
            Regex GrassLandArea = new Regex("^04");                 //识别草地;
            Regex HardenSurfArea = new Regex("^0(5|6|7)");          //识别不透水地表;
            Regex ConstructionArea = new Regex("^0(5|6|7|8)");      //识别建设用地;
            Regex DesertedArea = new Regex("^09");                  //识别未利用地;
            Regex WaterLandArea = new Regex("^10");                 //识别水域湿地;
            Regex ProtectLandArea = new Regex("^112(4|5|6|7|8)");   //识别受保护区域;
            Regex FloodLandArea = new Regex("^1129");               //识别行、蓄、滞洪区域;
            Regex SandFixationLandArea = new Regex("^042(3|4)");    //识别固沙草地、护坡灌草面积;

            #endregion
            double FarmArea = 0;            //耕地面积;
            double ForestArea = 0;          //林地面积;
            double GrassArea = 0;           //草地面积;
            double HardSurfArea = 0;        //不透水地表面积;
            double ConstructArea = 0;       //建设用地面积;
            double DesertArea = 0;          //未利用地面积;
            double WaterArea = 0;           //水域湿地面积;

            double ProtectArea = 0;         //受保护区域面积;
            double FloodArea = 0;           //行、蓄、滞洪区域面积;
            double SandFixationArea = 0;    //固沙草地、护坡灌草面积;

            //遍历要素集，读取土地分类面积;
            IFeature pFeature = null;
            while ((pFeature = featureCursor.NextFeature()) != null)
            {
                double temparea = (double)pFeature.get_Value(baseData.areaIndex);
                string code = pFeature.get_Value(baseData.codeIndex).ToString();

                //耕地面积;
                if (FarmLandArea.IsMatch(code))
                {
                    FarmArea += temparea;
                }
                //林地面积;
                if (ForesLandArea.IsMatch(code))
                {
                    ForestArea += temparea;
                }
                //草地面积;
                if (GrassLandArea.IsMatch(code))
                {
                    GrassArea += temparea;
                }
                //水域湿地面积;
                if (WaterLandArea.IsMatch(code))
                {
                    WaterArea += temparea;
                }
                //不透水地表面积;
                if (HardenSurfArea.IsMatch(code))
                {
                    HardSurfArea += temparea;
                }
                //建设用地面积;
                if (ConstructionArea.IsMatch(code))
                {
                    ConstructArea += temparea;
                }
                //荒漠与自然裸露地表面积;
                if (DesertedArea.IsMatch(code))
                {
                    DesertArea += temparea;
                }
                //受保护区域面积;
                if (ProtectLandArea.IsMatch(code))
                {
                    ProtectArea += temparea;
                }
                //行、蓄、滞洪区域面积;
                if (FloodLandArea.IsMatch(code))
                {
                    FloodArea += temparea;
                }
                //固沙草地、护坡灌草面积;
                if (SandFixationLandArea.IsMatch(code))
                {
                    SandFixationArea += temparea;
                }
            }
            ClassArea.FarmArea = FarmArea;
            ClassArea.ForestArea = ForestArea;
            ClassArea.GrassArea = GrassArea;
            ClassArea.HardenSurfaceArea = HardSurfArea;
            ClassArea.ConstructionArea = ConstructArea;
            ClassArea.DesertedArea = DesertArea;
            ClassArea.WaterArea = WaterArea;
            ClassArea.ProtectArea = ProtectArea;
            ClassArea.FloodArea = FloodArea;
            ClassArea.SandFixationArea = SandFixationArea;
            return ClassArea;
        }
        #endregion
    }
}
