using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AE_Environment.Model.EcoSystemIndex
{
    class NatureEcoIndex
    {
        public List<string> natEcoIndexName;
        public List<double> natEcoIndexResult;


        private SCLASSAREA zoneClassArea;
        List<string> natIndexName;
        IndexFormula natureIndexFla;

        #region 初始化自然保护区指标计算类
        public NatureEcoIndex(SCLASSAREA _zoneClassArea, List<string> _natIndexName)
        {
            zoneClassArea = _zoneClassArea;
            natIndexName = _natIndexName;
            natEcoIndexResult = new List<double>();
            natEcoIndexName = new List<string>();
        }
        #endregion

        #region 添加指标名称
        public void GetIndexName()
        {
            string strZRBHQ = string.Empty;
            string str = natIndexName[0].Substring(0, natIndexName[0].IndexOf("_"));
            if (str == "森林生态系统")
            {
                strZRBHQ = "FENR_";
            }
            else if (str == "草原与草甸生态系统")
            {
                strZRBHQ = "GMENR_";
            }
            else if (str == "荒漠生态系统")
            {
                strZRBHQ = "DENR_";
            }
            else if (str == "水域湿地生态系统")
            {
                strZRBHQ = "WWENR_";
            }

            for (int j = 0; j < natIndexName.Count; j++)
            {
                str = natIndexName[j].Substring(natIndexName[j].IndexOf("_") + 1);
                switch (str)
                {
                    case "生境质量指数":
                        natEcoIndexName.Add(strZRBHQ + "HQI");
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region 计算自然保护区生态环境状况评价指标
        public List<double> NatureEcoIndexCal()
        {
            double[] SJZL_weight= new double[7];
            #region 自然保护区生态环境状况评价指标计算系数
            string str = natIndexName[0].Substring(0, natIndexName[0].IndexOf("_"));
            if (str == "森林生态系统")
            {
                SJZL_weight = new double[] { 417.4399622443, 0.40, 0.18, 0.23, 0.08, 0.01, 0.10 };
            }
            else if (str == "草原与草甸生态系统")
            {
                SJZL_weight = new double[] { 569.0200678452, 0.18, 0.40, 0.23, 0.08, 0.01, 0.10 };
            }
            else if (str == "荒漠生态系统")
            {
                SJZL_weight = new double[] { 1146.3997531042, 0.15, 0.34, 0.30, 0.08, 0.01, 0.12 };
            }
            else if (str == "水域湿地生态系统")
            {
                SJZL_weight = new double[] { 785.6026937848, 0.18, 0.23, 0.40, 0.08, 0.01, 0.10 };
            }
            #endregion
            #region 指标计算
            natureIndexFla = new IndexFormula(zoneClassArea);
            for (int j = 0; j < natIndexName.Count; j++)
            {
                str = natIndexName[j].Substring(natIndexName[j].IndexOf("_") + 1);
                switch (str)
                {
                    case "生境质量指数":
                        natEcoIndexResult.Add(natureIndexFla.SJZLIndex(SJZL_weight));
                        break;
                    default:
                        break;
                }
            }
            #endregion 
            return natEcoIndexResult;
        }
        #endregion
    }
}
