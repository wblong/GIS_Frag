using AE_Environment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AE_Environment.Model.EcoSystemIndex;
using System.Collections;

namespace AE_Environment.EcoSystemIndex
{
    class SpecialEcoIndex 
    {
        public List<string> speEcoIndexName;
        public List<double> speEcoIndexResult;

        private SCLASSAREA zoneClassArea;
        List<string> speIndexName;
        IndexFormula specialIndexFla;

        #region 初始化生态功能区指标计算类
        public SpecialEcoIndex(SCLASSAREA _zoneClassArea, List<string> _speIndexName)
        {
            zoneClassArea = _zoneClassArea;
            speIndexName = _speIndexName;
            speEcoIndexResult = new List<double>();
            speEcoIndexName = new List<string>();
        }
        #endregion

        #region 添加指标名称
        public void GetIndexName()
        {
            if (speIndexName.Count <= 0)
            {
                return ;
            }

            string strGNQ = "";

            #region 功能区选择
            string str = speIndexName[0].Substring(0, speIndexName[0].IndexOf("_"));
            if (str == "防风固沙生态功能区")
            {
                strGNQ = "SFEFA_";
            }   
            else if (str == "水土保持生态功能区")
            {
                strGNQ = "SWCEFA_";
            }
            else if (str == "水源涵养生态功能区")
            {
                strGNQ = "WCEFA_";
            }
            else if (str == "生物多样性维护生态功能区")
            {
                strGNQ = "BMEFA_";
            }
            #endregion

            for (int j = 0; j < speIndexName.Count; j++)
            {
                str = speIndexName[j].Substring(speIndexName[j].IndexOf("_") + 1);

                #region 添加指标名称
                switch (str)
                {
                    case "林地覆盖率":
                        speEcoIndexName.Add(strGNQ + "FCR");
                        break;
                    case "草地覆盖率":
                        speEcoIndexName.Add(strGNQ + "GCR");
                        break;
                    case "耕地和建设用地面积比":
                        speEcoIndexName.Add(strGNQ + "ACLR");
                        break;
                    case "水域湿地面积比":
                        speEcoIndexName.Add(strGNQ + "WWR");
                        break;
                    case "林草地覆盖率":
                        speEcoIndexName.Add(strGNQ + "FGCR");
                        break;
                    case "水源涵养指数":
                        speEcoIndexName.Add(strGNQ + "WCI");
                        break;
                    case "受保护区域面积比":
                        speEcoIndexName.Add(strGNQ + "PAR");
                        break;
                    default:
                        break;
                }
                #endregion
            }
        }

        #endregion

        #region 计算生态功能区生态环境状况评价指标
        public List<double> SpecialEcoIndexCal()
        {
            if (speIndexName.Count <= 0)
            {
                return speEcoIndexResult;
            }
            specialIndexFla = new IndexFormula(zoneClassArea);
            double LD_Weight = 0;
            double CD_Weight = 0;
            double LCD_Weight = 0;
            double SYSD_Weight = 0;
            double GDJSYD_Weight = 0;
            double[] SYHY_Weight = new double[4];

            string strGNQ = string.Empty;

            #region 生态功能区生态环境状况评价指标计算系数
            string str = speIndexName[0].Substring(0, speIndexName[0].IndexOf("_"));
            if (str == "防风固沙生态功能区")
            {
                LCD_Weight = 105.4407423028;
                SYSD_Weight = 824.4023083265;
                GDJSYD_Weight = 165.0437365902;
            }
            else if (str == "水土保持生态功能区")
            {
                LCD_Weight = 104.5696957022;
                SYSD_Weight = 1418.4397163121;
                GDJSYD_Weight = 150.6477854776;
            }
            else if (str == "水源涵养生态功能区")
            {
                LD_Weight = 104.4277360067;
                CD_Weight = 120.5836247438;
                SYSD_Weight = 321.4400514304;
                GDJSYD_Weight = 102.7221366204;
                SYHY_Weight = new double[] { 526.7925984400, 0.45, 0.35, 0.20 };
            }
            else if (str == "生物多样性维护生态功能区")
            {
                LD_Weight = 113.3915409910;
                CD_Weight = 130.9414691633;
                SYSD_Weight = 329.9241174530;
                GDJSYD_Weight = 116.2115049390;
            }
            #endregion

            #region 指标计算
            
            for (int j = 0; j < speIndexName.Count; j++)
            {
                str = speIndexName[j].Substring(speIndexName[j].IndexOf("_") + 1);
                #region 根据所选指标调用公式
                switch (str)
                {
                    case "林地覆盖率":
                        speEcoIndexResult.Add(specialIndexFla.ForestAreaRatio(LD_Weight));
                        break;
                    case "草地覆盖率":
                        speEcoIndexResult.Add(specialIndexFla.GrassAreaRatio(CD_Weight));
                        break;
                    case "耕地和建设用地面积比":
                        speEcoIndexResult.Add(specialIndexFla.FarmConstructionAreaRatio(GDJSYD_Weight));
                        break;
                    case "水域湿地面积比":
                        speEcoIndexResult.Add(specialIndexFla.WaterAreaRatio(SYSD_Weight));
                        break;
                    case "林草地覆盖率":
                        speEcoIndexResult.Add(specialIndexFla.ForestGrassAreaRatio(LCD_Weight));
                        break;
                    case "水源涵养指数":
                        speEcoIndexResult.Add(specialIndexFla.SYHYIndex(SYHY_Weight));
                        break;
                    case "受保护区域面积比":
                        speEcoIndexResult.Add(specialIndexFla.ProtectAreaRatio());
                        break;
                    default:
                        break;
                }
                #endregion
            }
            #endregion 
            return speEcoIndexResult;
        }
        #endregion
    }
}
