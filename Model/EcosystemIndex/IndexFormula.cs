using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AE_Environment.Model.EcoSystemIndex
{
    #region 分区的土地覆盖分类面积（一级类）
    struct SCLASSAREA
    {
        public double ForestArea;           //林地;
        public double GrassArea;            //草地;
        public double FarmArea;             //耕地;
        public double WaterArea;            //水域湿地;
        public double HardenSurfaceArea;    //不透水地表;
        public double ConstructionArea;     //建设用地;
        public double DesertedArea;         //荒漠与裸露地表;
        public double ProtectArea;          //保护区域面积;
        public double FloodArea;            //行、蓄、滞洪区域面积;
        public double SandFixationArea;     //固沙草地、护坡灌草面积;
        public double TotalArea;            //总面积;

    }
    #endregion

    #region 生态环境状况评价指标计算公式
    class IndexFormula
    {
        SCLASSAREA classArea;
        public IndexFormula(SCLASSAREA _index)
        {
            classArea = _index;
        }

        #region 水源涵养指数
        public double SYHYIndex(double[] type)
        {
            double SYHYindex = 0;
            SYHYindex = type[0] * (type[1] * classArea.WaterArea + type[2] * classArea.ForestArea + 
                type[3] * classArea.GrassArea ) / (classArea.TotalArea);
            return SYHYindex;
        }
        #endregion

        #region 生境质量指数
        public double SJZLIndex(double[] type)
        {
            double SJZLindex = 0;
            SJZLindex = type[0] * (type[1] * classArea.ForestArea + type[2] * classArea.GrassArea +
                type[3] * classArea.WaterArea + type[4] * classArea.FarmArea +
                type[5] * classArea.ConstructionArea + type[6] * classArea.DesertedArea) / (classArea.TotalArea);
            return SJZLindex;
        }
        #endregion

        #region 水域湿地面积比
        public double WaterAreaRatio(double type)
        {
            double WaterArea_Ratio = 0;
            WaterArea_Ratio = type * classArea.WaterArea / classArea.TotalArea;
            return WaterArea_Ratio;
        }
        #endregion

        #region 耕地面积比
        public double FarmAreaRatio(double type)
        {
            double FarmArea_Ratio = 0;
            FarmArea_Ratio = type * classArea.FarmArea / classArea.TotalArea;
            return FarmArea_Ratio;
        }
        #endregion

        #region 不透水地表面积比
        public double HardenSurfaceRatio(double type)
        {
            double HardenSurface_Ratio = 0;
            HardenSurface_Ratio = type * classArea.HardenSurfaceArea / classArea.TotalArea;
            return HardenSurface_Ratio;
        }
        #endregion

        #region 耕地和建设用地面积比
        public double FarmConstructionAreaRatio(double type)
        {
            double CropConstructionArea_Ratio = 0;
            CropConstructionArea_Ratio = type * (classArea.FarmArea + classArea.ConstructionArea) / classArea.TotalArea;
            return CropConstructionArea_Ratio;
        }
        #endregion

        #region 林草地覆盖率
        public double ForestGrassAreaRatio(double type)
        {
            double FoestGrassArea_Ratio = 0;
            FoestGrassArea_Ratio = type * (classArea.ForestArea + classArea.GrassArea) / classArea.TotalArea;
            return FoestGrassArea_Ratio;
        }
        #endregion

        #region 林地覆盖率
        public double ForestAreaRatio(double type)
        {
            double FoestArea_Ratio = 0;
            FoestArea_Ratio = type * classArea.ForestArea / classArea.TotalArea;
            return FoestArea_Ratio;
        }
        #endregion

        #region 草地覆盖率
        public double GrassAreaRatio(double type)
        {
            double GrassArea_Ratio = 0;
            GrassArea_Ratio = type * classArea.GrassArea / classArea.TotalArea;
            return GrassArea_Ratio;
        }
        #endregion

        #region 荒漠与自然裸露地表面积比
        public double DesertAreaRatio(double type)
        {
            double DesertArea_Ratio = 0;
            DesertArea_Ratio = type * classArea.DesertedArea / classArea.TotalArea;
            return DesertArea_Ratio;
        }
        #endregion

        #region 保护区面积比
        public double ProtectAreaRatio()
        {
            double ProtectArea_Ratio = 0;
            ProtectArea_Ratio = classArea.ProtectArea / classArea.TotalArea;
            return ProtectArea_Ratio;
        }
        #endregion

        #region 行、蓄、滞洪区面积比
        public double FloodAreaRatio()
        {
            double FloodArea_Ratio = 0;
            FloodArea_Ratio = classArea.FloodArea / classArea.TotalArea;
            return FloodArea_Ratio;
        }
        #endregion

        #region 固沙草地、护坡灌草面积比
        public double SandFixationAreaRatio()
        {
            double SandFixationArea_Ratio = 0;
            SandFixationArea_Ratio = classArea.SandFixationArea / classArea.TotalArea;
            return SandFixationArea_Ratio;
        }
        #endregion

    }
    #endregion
}
