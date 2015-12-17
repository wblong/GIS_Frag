using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AE_Environment.Model;
using ESRI.ArcGIS.Geodatabase;
using System.Text.RegularExpressions;
using AE_Environment.Model.EcoSystemIndex;
using AE_Environment.Model.EcosystemIndex;

namespace AE_Environment.EcoSystemIndex
{
    
    class CommonEcoIndex
    {
        public List<double> comEcoIndexResult;
        public List<string> comEcoIndexName;

        private SCLASSAREA zoneClassArea;
        private List<string> comIndexName;
        IndexFormula comIndexFla;

        #region 初始化普通指标计算类
        public CommonEcoIndex(SCLASSAREA _zoneClassArea, List<string> _comIndexName)
        {
            zoneClassArea = _zoneClassArea;
            comIndexName = _comIndexName;
            comEcoIndexResult = new List<double>();
            comEcoIndexName = new List<string>();
        }
        #endregion

        #region 添加计算指标名称
        public void GetIndexName()
        {
            if (comIndexName.Count <= 0)
            {
                return;
            }
            //添加计算的指标名称;
            for (int j = 0; j < comIndexName.Count; j++)
            {
                #region 根据所选指标调用公式
                switch (comIndexName[j].ToString())
                {
                    case "生境质量指数":
                        comEcoIndexName.Add("Com_HQI");
                        break;
                    case "林地覆盖率":
                        comEcoIndexName.Add("Com_FCR");
                        break;
                    case "草地覆盖率":
                        comEcoIndexName.Add("Com_GCR");
                        break;
                    case "耕地面积比":
                        comEcoIndexName.Add("Com_ALR");
                        break;
                    case "不透水地表面积比":
                        comEcoIndexName.Add("Com_ISR");
                        break;
                    case "耕地和建设用地面积比":
                        comEcoIndexName.Add("Com_ACLR");
                        break;
                    case "水域湿地面积比":
                        comEcoIndexName.Add("Com_WWR");
                        break;
                    case "荒漠与自然裸露地表面积比":
                        comEcoIndexName.Add("Com_DNBSR");
                        break;
                    case "水源涵养指数":
                        comEcoIndexName.Add("Com_WCI");
                        break;
                    case "保护区面积比":
                        comEcoIndexName.Add("Com_PAR");
                        break;
                    case "行、蓄、滞洪区面积比":
                        comEcoIndexName.Add("Com_FAR");
                        break;
                    case "固沙草地、护坡灌草面积比":
                        comEcoIndexName.Add("Com_SFSGR");
                        break;
                    case "人均林地":
                        if (MainForm.dt_Population.Rows.Count > 0 )
                        {
                            comEcoIndexName.Add("Com_PCF");
                        }
                        break;
                    case "人均耕地":
                        if (MainForm.dt_Population.Rows.Count > 0)
                        {
                            comEcoIndexName.Add("Com_PCA");
                        }
                        break;
                    case "人均休闲用地":
                        if (MainForm.dt_Population.Rows.Count > 0)
                        {
                            comEcoIndexName.Add("Com_PCL");
                        }
                        break;
                    default:
                        break;
                }
                #endregion
            }
        }
        #endregion


        #region 计算基础生态环境状况评价指标
        public List<double> ComEcoIndexCal()
        {
            #region 基础生态环境状况评价指标计算系数
            double[] ComSJZLI = new double[] { 511.2642131067, 0.35, 0.21, 0.28, 0.11, 0.04, 0.01 };
            double[] ComSYHYI = new double[] { 1, 1, 1, 1};
            #endregion
            if (comIndexName.Count <= 0)
            {
                return comEcoIndexResult;
            }
            comIndexFla = new IndexFormula(zoneClassArea);
            for (int i = 0; i < comIndexName.Count; i++)
            {
                #region 根据所选指标调用公式
                switch (comIndexName[i].ToString())
                {
                    case "生境质量指数":
                        comEcoIndexResult.Add(comIndexFla.SJZLIndex(ComSJZLI));
                        break;
                    case "林地覆盖率":
                        comEcoIndexResult.Add(comIndexFla.ForestAreaRatio(1));
                        break;
                    case "草地覆盖率":
                        comEcoIndexResult.Add(comIndexFla.GrassAreaRatio(1));
                        break;
                    case "耕地面积比":
                        comEcoIndexResult.Add(comIndexFla.FarmAreaRatio(1));
                        break;
                    case "不透水地表面积比":
                        comEcoIndexResult.Add(comIndexFla.HardenSurfaceRatio(1));
                        break;
                    case "耕地和建设用地面积比":
                        comEcoIndexResult.Add(comIndexFla.FarmConstructionAreaRatio(1));
                        break;
                    case "水域湿地面积比":
                        comEcoIndexResult.Add(comIndexFla.WaterAreaRatio(1));
                        break;
                    case "荒漠与自然裸露地表面积比":
                        comEcoIndexResult.Add(comIndexFla.DesertAreaRatio(1));
                        break;
                    case "水源涵养指数":
                        comEcoIndexResult.Add(comIndexFla.SYHYIndex(ComSYHYI));
                        break;
                    case "受保护区域面积比":
                        comEcoIndexResult.Add(comIndexFla.ProtectAreaRatio());
                        break;
                    case "行、蓄、滞洪区域面积比":
                        comEcoIndexResult.Add(comIndexFla.FloodAreaRatio());
                        break;
                    case "固沙草地、护坡灌草面积比":
                        comEcoIndexResult.Add(comIndexFla.SandFixationAreaRatio());
                        break;
                    case "人均林地":
                        double pcf = 0;
                        if (PatchEcosystem.population != 0)
                        {
                            pcf = zoneClassArea.ForestArea / PatchEcosystem.population;
                        }
                        comEcoIndexResult.Add(pcf);
                        break;
                    case "人均耕地":
                        double pca = 0;
                        if (PatchEcosystem.population != 0)
                        {
                            pca = zoneClassArea.FarmArea / PatchEcosystem.population;
                        }
                        comEcoIndexResult.Add(pca);
                        break;
                    case "人均休闲用地":
                        double pcl = 0;
                        if (PatchEcosystem.population != 0)
                        {
                            pcl = zoneClassArea.ProtectArea / PatchEcosystem.population;
                        }
                        comEcoIndexResult.Add(pcl);
                        break;
                    default:
                        break;
                }
                #endregion
            }
            return comEcoIndexResult;
        }
        #endregion
    }
}
