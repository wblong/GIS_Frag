using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace AE_Environment
{
    class EcosystemIndexClass
    {
        private IMapControl3 mMapControl;
        public EcosystemIndexClass(IMapControl3 mapControl)
        {
            mMapControl = mapControl;
        }

        /************************************************************************/
        /*                         最新添加                                 */
        /************************************************************************/
        public double Com_LDRatio(double ForestArea, double TotalArea)
        {
            double LDRatio = 0;
            LDRatio = ForestArea / TotalArea;
            return LDRatio;
        }
        public double Com_CDRatio(double GrassArea, double TotalArea)
        {
            double GDRatio = 0;
            GDRatio = GrassArea / TotalArea;
            return GDRatio;
        }
        public double Com_SYSDRatio(double WaterArea, double TotalArea)
        {
            double SYSDRatio = 0;
            SYSDRatio = WaterArea / TotalArea;
            return SYSDRatio;
        }
        public double Com_GDJSYDRatio(double CropArea, double ConstructedArea)
        {
            double GDJSYDRatio = 0;
            GDJSYDRatio = CropArea / ConstructedArea;
            return GDJSYDRatio;
        }
        public double Com_WLYDRatio(double UnutilizedArea, double TotalArea)
        {
            double WLYDRatio = 0;
            WLYDRatio = UnutilizedArea / TotalArea;
            return WLYDRatio;
        }




        /************************************************************************/
        /*                       生境质量指数                              */
        /************************************************************************/
        //普通生境质量指数;
        public double Com_SJZLIndex(double ForestArea, double GrassArea, double WaterArea,
            double CropArea, double ConstructedArea, double UnutilizedArea, double TotalArea)
        {
            double SJZLindex = 0;
            SJZLindex = 511.2642131067 * (0.35 * ForestArea + 0.21 * GrassArea +
                0.28 * WaterArea + 0.11 * CropArea +
                0.04 * ConstructedArea + 0.01 * UnutilizedArea) / (TotalArea);
            return SJZLindex;
        }

        //森林生态系统生境质量指数;
        public double ForestEco_SJZLIndex(double ForestArea, double GrassArea, double WaterArea,
            double CropArea, double ConstructedArea, double UnutilizedArea, double TotalArea)
        {
            double SJZLindex = 0;
            SJZLindex = 417.4399622443 * (0.40 * ForestArea + 0.18 * GrassArea +
            0.23 * WaterArea + 0.08 * CropArea +
            0.01 * ConstructedArea + 0.10 * UnutilizedArea) / (TotalArea);
            return SJZLindex;
        }

        //草原与草甸生态系统生境质量指数;
        public double GrassEco_SJZLIndex(double ForestArea, double GrassArea, double WaterArea,
            double CropArea, double ConstructedArea, double UnutilizedArea, double TotalArea)
        {
            double SJZLindex = 0;
            SJZLindex = 569.0200678452 * (0.18 * ForestArea + 0.40 * GrassArea +
            0.23 * WaterArea + 0.08 * CropArea +
            0.01 * ConstructedArea + 0.10 * UnutilizedArea) / (TotalArea);
            return SJZLindex;
        }

        //荒漠生态系统生境质量指数;
        public double DesertEco_SJZLIndex(double ForestArea, double GrassArea, double WaterArea,
            double CropArea, double ConstructedArea, double UnutilizedArea, double TotalArea)
        {
            double SJZLindex = 0;
            SJZLindex = 1146.3997531042 * (0.15 * ForestArea + 0.34 * GrassArea +
            0.30 * WaterArea + 0.08 * CropArea +
            0.01 * ConstructedArea + 0.12 * UnutilizedArea) / TotalArea;
            return SJZLindex;
        }

        //水域湿地生态系统生境质量指数;
        public double WaterEco_SJZLIndex(double ForestArea, double GrassArea, double WaterArea,
            double CropArea, double ConstructedArea, double UnutilizedArea, double TotalArea)
        {
            double SJZLindex = 0;
            SJZLindex = 785.6026937848 * (0.18 * ForestArea + 0.23 * GrassArea +
            0.40 * WaterArea + 0.08 * CropArea +
            0.01 * ConstructedArea + 0.10 * UnutilizedArea) / (TotalArea);
            return SJZLindex;
        }

        /************************************************************************/
        /*                    水域湿地面积比;                                   */
        /************************************************************************/
        //防风固沙功能区水域湿地面积比;
        public double Sand_WaterAreaRatio(double WaterArea, double TotalArea)
        {
            double WaterArea_Ratio = 0;
            WaterArea_Ratio = 824.4023083265 * WaterArea / TotalArea;
            return WaterArea_Ratio;
        }
        //水土保持功能区水域湿地面积比;
        public double Land_WaterAreaRatio(double WaterArea, double TotalArea)
        {
            double WaterArea_Ratio = 0;
            WaterArea_Ratio = 1418.4397163121 * WaterArea / TotalArea;
            return WaterArea_Ratio;
        }
        //水源涵养功能区水域湿地面积比;
        public double Water_WaterAreaRatio(double WaterArea, double TotalArea)
        {
            double WaterArea_Ratio = 0;
            WaterArea_Ratio = 321.4400514304 * WaterArea / TotalArea;
            return WaterArea_Ratio;
        }
        //生物多样性维护功能区水域湿地面积比;
        public double Biology_WaterAreaRatio(double WaterArea, double TotalArea)
        {
            double WaterArea_Ratio = 0;
            WaterArea_Ratio = 329.9241174530 * WaterArea / TotalArea;
            return WaterArea_Ratio;
        }

        /************************************************************************/
        /*                    耕地和建设用地面积比  ;                          */
        /************************************************************************/
        //防风固沙功能区耕地和建设用地面积比;
        public double Sand_CropConstructionAreaRatio(double CropArea, double ConstructionArea)
        {
            double CropConstructionArea_Ratio = 0;
            CropConstructionArea_Ratio = 165.0437365902 * CropArea / ConstructionArea;
            return CropConstructionArea_Ratio;
        }
        //水土保持功能区耕地和建设用地面积比;
        public double Land_CropConstructionAreaRatio(double CropArea, double ConstructionArea)
        {
            double CropConstructionArea_Ratio = 0;
            CropConstructionArea_Ratio = 150.6477854776 * CropArea / ConstructionArea;
            return CropConstructionArea_Ratio;
        }
        //水源涵养功能区耕地和建设用地面积比;
        public double Water_CropConstructionAreaRatio(double CropArea, double ConstructionArea)
        {
            double CropConstructionArea_Ratio = 0;
            CropConstructionArea_Ratio = 102.7221366204 * CropArea / ConstructionArea;
            return CropConstructionArea_Ratio;
        }
        //生物多样性维护功能区耕地和建设用地面积比;
        public double Biology_CropConstructionAreaRatio(double CropArea, double ConstructionArea)
        {
            double CropConstructionArea_Ratio = 0;
            CropConstructionArea_Ratio = 116.2115049390 * CropArea / ConstructionArea;
            return CropConstructionArea_Ratio;
        }

        /************************************************************************/
        /*                         林草地覆盖率;                               */
        /************************************************************************/
        //防风固沙功能区林草地覆盖率;
        public double Sand_FoestGrassAreaRatio(double FoestArea, double GrassArea, double TotalArea)
        {
            double FoestGrassArea_Ratio = 0;
            FoestGrassArea_Ratio = 105.4407423028 * (FoestArea + GrassArea) / TotalArea;
            return FoestGrassArea_Ratio;
        }
        //水土保持功能区林草地覆盖率;
        public double Land_FoestGrassAreaRatio(double FoestArea, double GrassArea, double TotalArea)
        {
            double FoestGrassArea_Ratio = 0;
            FoestGrassArea_Ratio = 104.56996957022 * (FoestArea + GrassArea) / TotalArea;
            return FoestGrassArea_Ratio;
        }

        /************************************************************************/
        /*                    林地覆盖率;                                     */
        /************************************************************************/
        //水源涵养功能区林地覆盖率;
        public double Water_FoestAreaRatio(double FoestArea, double TotalArea)
        {
            double FoestArea_Ratio = 0;
            FoestArea_Ratio = 104.4277360067 * FoestArea / TotalArea;
            return FoestArea_Ratio;
        }
        //生物多样性维护功能区林地覆盖率;
        public double Biology_FoestAreaRatio(double FoestArea, double TotalArea)
        {
            double FoestArea_Ratio = 0;
            FoestArea_Ratio = 113.3915409910 * FoestArea / TotalArea;
            return FoestArea_Ratio;
        }

        /************************************************************************/
        /*                     草地覆盖率;                                      */
        /************************************************************************/
        //水源涵养功能区草地覆盖率;
        public double Water_GrassAreaRatio(double GrassArea, double TotalArea)
        {
            double GrassArea_Ratio = 0;
            GrassArea_Ratio = 120.5836247438 * GrassArea / TotalArea;
            return GrassArea_Ratio;
        }
        //生物多样性维护功能区草地覆盖率;
        public double Biology_GrassAreaRatio(double GrassArea, double TotalArea)
        {
            double GrassArea_Ratio = 0;
            GrassArea_Ratio = 130.9414691633 * GrassArea / TotalArea;
            return GrassArea_Ratio;
        }

        public double[] Area(string sql,IFeatureClass pFeatureClass)
        {
            double[] area ;
            List<double> list = new List<double>();

            //获取当前图层;
          
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = MainForm.dataInputInfo.zoneField;
            pDataStatistics.Cursor = (ICursor)pFeatureCursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();

            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;

                //计算耕地面积;
                IQueryFilter pQueryFilter = new QueryFilterClass();
                pQueryFilter.WhereClause = "\""+MainForm.dataInputInfo.zoneField+"\"" + " = \'" + obj +"\'"+ sql;
                IFeatureCursor pFeatureCursor3 = pFeatureClass.Search(pQueryFilter, true);

                IDataStatistics pAreaStatistics3 = new DataStatisticsClass();
                pAreaStatistics3.Field = "Shape_Area";
                pAreaStatistics3.Cursor = pFeatureCursor3 as ICursor;
                IStatisticsResults results3 = pAreaStatistics3.Statistics;
                list.Add(results3.Sum);
            }
            area = list.ToArray();
            return area;
        }

    }
}
