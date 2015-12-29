using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.IO;


using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Carto;
using AE_Environment.TypesStruct;
using ESRI.ArcGIS.ADF;
 
namespace AE_Environment.FragStats
{
    class Stats
    {

        public Stats()
        {


        }
        /// <summary>
        /// 检查输入数据
        /// </summary>
        /// <returns></returns>
        private static bool checkData()
        {

            bool flag = true;
            IFeatureClass cover = MainForm.dataInputInfo.layerDataCover;
            IFeatureClass zone = MainForm.dataInputInfo.layerDataZone;
            try
            {
                if (cover.Fields.FindField("Shape_Area")<0)
                {
                    flag = false;
                }
                if (cover.Fields.FindField("Shape_Length")<0)
                {
                    flag = false;
                }
               

            }
            catch (System.Exception ex)
            {
                flag = false;
            }
             
            return flag;
        }
        #region 景观指标
        /// <summary>
        /// 对比度：边界对比度
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="classInfo"></param>
        public static void TECIIndex(DataInputInfo dataInput, LandscapeParamInfo landInfo)
        {

            //具体公式有待商榷
            DataTable landTable = landInfo.resultTable;
            DataColumn pDataColumn = GetTableColumnByName(landTable, "TECI(%)");

            //
            IFeatureClass pFeatureClass = dataInput.layerDataCover;
            IFields pFields = pFeatureClass.Fields;
            int indexArea = pFields.FindField("Shape_Area");
            int indexLeng = pFields.FindField("Shape_Length");
            if (indexLeng < 0)
            {
                indexLeng = pFields.FindField("Shape_Leng");
            }
            int indexCode = pFields.FindField(dataInput.codeField);
            //

            //
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = dataInput.zoneField;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();

            DataRow pDataRow = null;
            ISpatialFilter pSpatialFilter = new SpatialFilterClass();
            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;


                double result = 0.0;
                double totallen = 0.0;

                //按类别统计
                IQueryFilter pQueryFilterClss = new QueryFilterClass();
                pQueryFilterClss.WhereClause = dataInput.zoneField + " = \'" + obj + "\'";
                IFeatureCursor pFeatureCursorClss = pFeatureClass.Search(pQueryFilterClss, true);
                IFeature pFeature = pFeatureCursorClss.NextFeature();

                while (pFeature != null)
                {

                    
                    pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    pSpatialFilter.GeometryField = pFeatureClass.ShapeFieldName;
                    pSpatialFilter.Geometry = pFeature.Shape as IGeometry;
                    IFeatureCursor pFeatureSpatialCursor = pFeatureClass.Search(pSpatialFilter, true);
                    IFeature pFeatureSpatial = pFeatureSpatialCursor.NextFeature();
                    int count = 0;
                    while (pFeatureSpatial != null)
                    {
                        count++;
                        ICurve polygon = pFeature.Shape as ICurve;
                        ICurve polygon2 = pFeatureSpatial.Shape as ICurve;

                        ITopologicalOperator pTopo = polygon as ITopologicalOperator;
                        ICurve pCurve = pTopo.Intersect(polygon2 as IGeometry, esriGeometryDimension.esriGeometry1Dimension) as ICurve;
                        result += pCurve.Length * Forms.LandFrm.GetWeightFromDt(Forms.LandFrm.weightDt, pFeature.get_Value(indexCode).ToString(), pFeatureSpatial.get_Value(indexCode).ToString());
                        totallen += pCurve.Length;
                        pFeatureSpatial = pFeatureSpatialCursor.NextFeature();
                    }
                    pFeature = pFeatureCursorClss.NextFeature();
                    //MessageBox.Show(count.ToString());
                }


                //确定更新还是添加
                string express = "ZID=" + obj;
                if (landInfo.resultTable.Select(express).Length > 0)
                {
                    pDataRow = landInfo.resultTable.Select(express)[0];
                }

                if (pDataRow == null)
                {
                    pDataRow = landInfo.resultTable.NewRow();
                    pDataRow[1] = obj;
                    pDataRow.SetField(pDataColumn, result / totallen * 100);
                    landInfo.resultTable.Rows.Add(pDataRow);
                    pDataRow = null;
                }
                else
                {
                    pDataRow.SetField(pDataColumn, result / totallen * 100);
                }
               

            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatistics);
        }
        /// <summary>
        /// 对比度：边界密度
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="classInfo"></param>
        public static void ContrastWeightedED(DataInputInfo dataInput, LandscapeParamInfo landInfo)
        {

            //具体公式有待商榷
            DataTable landTable = landInfo.resultTable;
            DataColumn pDataColumn = GetTableColumnByName(landTable, "CWED(m/hectare)");

            //
            IFeatureClass pFeatureClass = dataInput.layerDataCover;
            IFields pFields = pFeatureClass.Fields;
            int indexArea = pFields.FindField("Shape_Area");
            int indexLeng = pFields.FindField("Shape_Length");
            if (indexLeng < 0)
            {
                indexLeng = pFields.FindField("Shape_Leng");
            }
            int indexCode = pFields.FindField(dataInput.codeField);
            //
            IFeatureClass pFeatureClassZone = dataInput.layerDataZone;
            IQueryFilter pQueryFilterZone = new QueryFilterClass();
            double ZoneArea = 0.0;
            int ZoneAreaIndex = pFeatureClassZone.FindField("Shape_Area");
            //
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = dataInput.zoneField;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();

            DataRow pDataRow = null;
            IQueryFilter pQueryFilterClss = new QueryFilterClass();

            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;

                pQueryFilterZone.WhereClause = dataInput.zoneField + " = \'" + obj + "\'";
                IFeatureCursor pFeatureCursorZone = pFeatureClassZone.Search(pQueryFilterZone, true);
                try
                {
                    ZoneArea = (double)pFeatureCursorZone.NextFeature().get_Value(ZoneAreaIndex);
                }
                catch (System.Exception ex)
                {
                    string str = obj.ToString() + "查询分区失败！";
                    MessageBox.Show(str);
                }
                double result = 0.0;
                //double totallen = 0.0;
                //按类别统计
               
                
                pQueryFilterClss.WhereClause = dataInput.zoneField + " = \'" + obj + "\'" ;
                IFeatureCursor pFeatureCursorClss = pFeatureClass.Search(pQueryFilterClss, true);
                IFeature pFeature = pFeatureCursorClss.NextFeature();

                while (pFeature != null)
                {

                    ISpatialFilter pSpatialFilter = new SpatialFilterClass();
                    pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    pSpatialFilter.GeometryField = pFeatureClass.ShapeFieldName;
                    pSpatialFilter.Geometry = pFeature.Shape as IGeometry;
                    IFeatureCursor pFeatureSpatialCursor = pFeatureClass.Search(pSpatialFilter, true);
                    IFeature pFeatureSpatial = pFeatureSpatialCursor.NextFeature();
                    int count = 0;
                    while (pFeatureSpatial != null)
                    {
                        count++;
                        ICurve polygon = pFeature.Shape as ICurve;
                        ICurve polygon2 = pFeatureSpatial.Shape as ICurve;

                        ITopologicalOperator pTopo = polygon as ITopologicalOperator;
                        ICurve pCurve = pTopo.Intersect(polygon2 as IGeometry, esriGeometryDimension.esriGeometry1Dimension) as ICurve;
                        result += pCurve.Length * Forms.LandFrm.GetWeightFromDt(Forms.LandFrm.weightDt, pFeature.get_Value(indexCode).ToString(), pFeatureSpatial.get_Value(indexCode).ToString());
                        // totallen += pCurve.Length;
                        pFeatureSpatial = pFeatureSpatialCursor.NextFeature();
                    }
                    pFeature = pFeatureCursorClss.NextFeature();
                    //MessageBox.Show(count.ToString());
                }


                //确定更新还是添加
                string express = "ZID=" + obj ;
                if (landInfo.resultTable.Select(express).Length > 0)
                {
                    pDataRow = landInfo.resultTable.Select(express)[0];
                }

                if (pDataRow == null)
                {
                    pDataRow = landInfo.resultTable.NewRow();
                    pDataRow[1] = obj;
                    pDataRow.SetField(pDataColumn, result / ZoneArea);
                    landInfo.resultTable.Rows.Add(pDataRow);
                    pDataRow = null;
                }
                else
                {
                    pDataRow.SetField(pDataColumn, result / ZoneArea);
                }
                

            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatistics);
        }
        /// <summary>
        ///  修正后的破碎指标
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="classInfo"></param>
        public static void MMeshIndex(DataInputInfo dataInput, LandscapeParamInfo landInfo)
        {

            //读取配置图层
            ILayer pLayer = Forms.LandFrm.pLayer;
            IFeatureClass spatialFC = (pLayer as IFeatureLayer).FeatureClass;
            int indexSpatialArea = spatialFC.Fields.FindField("Shape_Area");
            //读取结果表
            DataTable landTable = landInfo.resultTable;
            DataColumn pDataColumn = GetTableColumnByName(landTable, "MMI(km2)");
            //读取覆盖图层
            IFeatureClass pFeatureClass = dataInput.layerDataCover;
            IFeatureClass pFeatureClassZone = dataInput.layerDataZone;
            int ZoneAreaIndex = pFeatureClassZone.Fields.FindField("Shape_Area");
            IQueryFilter pQueryFilterZone = new QueryFilterClass();
            double ZoneArea = 0.0;

            IFields pFields = pFeatureClass.Fields;
            int indexArea = pFields.FindField("Shape_Area");
            int indexCode = pFields.FindField(dataInput.codeField);

            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = dataInput.zoneField;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();


            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;
                pQueryFilterZone.WhereClause = dataInput.zoneField + " = \'" + obj + "\'";
                IFeatureCursor pFeatureCursorZone = pFeatureClassZone.Search(pQueryFilterZone, true);
                try
                {
                    ZoneArea = (double)pFeatureCursorZone.NextFeature().get_Value(ZoneAreaIndex);
                }
                catch (System.Exception ex)
                {
                    string str = obj.ToString() + "查询分区失败！";
                    MessageBox.Show(str);
                }

            
                IFeatureCursor pFeatureCursorClss = pFeatureClass.Search(pQueryFilterZone, true);
                IFeature pFeature = pFeatureCursorClss.NextFeature();
                /////////////////////////////////////////////////////////
                double area = 0;
                
                while (pFeature != null)
                {
                    double pArea = (double)pFeature.get_Value(indexArea);
                    int fid = (int)pFeature.get_Value(0);
                    ISpatialFilter spatialFilter = new SpatialFilterClass();


                    spatialFilter.Geometry = pFeature.Shape;
                    spatialFilter.GeometryField = spatialFC.ShapeFieldName;

                    spatialFilter.SpatialRelDescription = "T********";
                    spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelRelation;
                    //                         spatialFilter.WhereClause = "Shape_Area >= " + pArea.ToString();
                    //                         spatialFilter.SearchOrder = esriSearchOrder.esriSearchOrderAttribute;

                    IFeatureCursor pSpatialFCursor = spatialFC.Search(spatialFilter, false);
                    IFeature pSpatialFeature = pSpatialFCursor.NextFeature();

                    int fidsp = 0;
                    double computeArea = 0;
                    //待修改
                    int count = 0;
                    while (pSpatialFeature != null)
                    {

                        fidsp = (int)pSpatialFeature.get_Value(0);
                        count++;
                        computeArea += (double)pSpatialFeature.get_Value(indexSpatialArea);
                        pSpatialFeature = pSpatialFCursor.NextFeature();


                    }
                    if (count!=1) MessageBox.Show(count.ToString());

                    double areatemp = (double)pFeature.get_Value(indexArea) * computeArea;
                    area += areatemp;
                    pFeature = pFeatureCursorClss.NextFeature();
                    //释放资源
                    if (pSpatialFCursor != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(pSpatialFCursor);
                    }
                    if (pSpatialFeature != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(pSpatialFeature);
                    }
                    if (spatialFilter != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(spatialFilter);
                    }


                }//end of while
                //  MessageBox.Show(count.ToString());
                DataRow pDataRow = null;
                //确定更新还是添加
                string express = "ZID=" + obj;
                if (landInfo.resultTable.Select(express).Length > 0)
                {
                    pDataRow = landInfo.resultTable.Select(express)[0];
                }

                if (pDataRow == null)
                {
                    pDataRow = landInfo.resultTable.NewRow();
                    pDataRow[1] = obj;
                   
                    pDataRow.SetField(pDataColumn, area / ZoneArea/ 1000000);
                    landInfo.resultTable.Rows.Add(pDataRow);
                    pDataRow = null;
                }
                else
                {
                    pDataRow.SetField(pDataColumn, area / ZoneArea / 1000000);
                }

               
                if (pFeatureCursorClss != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursorClss);
                }



                ///////////

            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatistics);

        }
        /// <summary>
        /// 破碎度mesh
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="landInfo"></param>
        public static void MeshIndex(DataInputInfo dataInput, LandscapeParamInfo landInfo)
        {
            //具体公式有待商榷
            DataTable landTable = landInfo.resultTable;
            DataColumn pDataColumn = GetTableColumnByName(landTable, "MI(km2)");

            //
            IFeatureClass pFeatureClass = dataInput.layerDataCover;
            IFeatureClass pFeatureClassZone = dataInput.layerDataZone;
            int ZoneAreaIndex = pFeatureClassZone.Fields.FindField("Shape_Area");
            IQueryFilter pQueryFilterZone = new QueryFilterClass();
            double ZoneArea = 0.0;

            IFields pFields = pFeatureClass.Fields;
            int indexArea = pFields.FindField("Shape_Area");

            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = dataInput.zoneField;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();

            DataRow pDataRow = null;
            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;

                pQueryFilterZone.WhereClause = dataInput.zoneField + " = \'" + obj + "\'";
                IFeatureCursor pFeatureCursorZone = pFeatureClassZone.Search(pQueryFilterZone, true);
                try
                {
                    ZoneArea = (double)pFeatureCursorZone.NextFeature().get_Value(ZoneAreaIndex);
                }
                catch (System.Exception ex)
                {
                    string str = obj.ToString() + "查询分区失败！";
                    MessageBox.Show(str);
                }


               
                    IFeatureCursor pFeatureCursorClss = pFeatureClass.Search(pQueryFilterZone, true);
                    IFeature pFeature = pFeatureCursorClss.NextFeature();
                    double area = 0;
                    while (pFeature != null)
                    {

                        double areatemp = (double)pFeature.get_Value(indexArea);
                        area += areatemp * areatemp;
                        pFeature = pFeatureCursorClss.NextFeature();
                    }


                    //确定更新还是添加
                    string express = "ZID=" + obj ;
                    if (landInfo.resultTable.Select(express).Length > 0)
                    {
                        pDataRow = landInfo.resultTable.Select(express)[0];
                    }

                    if (pDataRow == null)
                    {
                        pDataRow = landInfo.resultTable.NewRow();
                        pDataRow[1] = obj;
                        pDataRow.SetField(pDataColumn, area / ZoneArea / 1000000.0);
                        landInfo.resultTable.Rows.Add(pDataRow);
                        pDataRow = null;
                    }
                    else
                    {
                        pDataRow.SetField(pDataColumn, area / ZoneArea / 1000000.0);
                    }
                }

          
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatistics);

        }
        /// <summary>
        /// 未受交通网络分割区域面积比率
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="classInfo"></param>
        public static void UMeshIndex(DataInputInfo dataInput, LandscapeParamInfo landInfo)
        {
            //具体公式有待商榷
            DataTable landTable = landInfo.resultTable;
            DataColumn pDataColumn = GetTableColumnByName(landTable, "UMI(km2)");

            //
            IFeatureClass pFeatureClass = dataInput.layerDataCover;
            IFeatureClass pFeatureClassZone = dataInput.layerDataZone;
            int ZoneAreaIndex = pFeatureClassZone.Fields.FindField("Shape_Area");
            IQueryFilter pQueryFilterZone = new QueryFilterClass();
            double ZoneArea = 0.0;

            IFields pFields = pFeatureClass.Fields;
            int indexArea = pFields.FindField("Shape_Area");

            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = dataInput.zoneField;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();

            DataRow pDataRow = null;
            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;

                pQueryFilterZone.WhereClause = dataInput.zoneField + " = \'" + obj + "\'";
                IFeatureCursor pFeatureCursorZone = pFeatureClassZone.Search(pQueryFilterZone, true);
                try
                {
                    ZoneArea = (double)pFeatureCursorZone.NextFeature().get_Value(ZoneAreaIndex);
                }
                catch (System.Exception ex)
                {
                    string str = obj.ToString() + "查询分区失败！";
                    MessageBox.Show(str);
                }

                    IFeatureCursor pFeatureCursorClss = pFeatureClass.Search(pQueryFilterZone, true);
                    IFeature pFeature = pFeatureCursorClss.NextFeature();
                    double area = 0;
                    while (pFeature != null)
                    {

                        double areatemp = (double)pFeature.get_Value(indexArea);
//                         if (areatemp >= Forms.ClssFrm.limitValue * 1000000)
//                         {
//                             area += areatemp;
//                         }

                        pFeature = pFeatureCursorClss.NextFeature();
                    }


                    //确定更新还是添加
                    string express = "ZID=" + obj;
                    if (landInfo.resultTable.Select(express).Length > 0)
                    {
                        pDataRow = landInfo.resultTable.Select(express)[0];
                    }

                    if (pDataRow == null)
                    {
                        pDataRow = landInfo.resultTable.NewRow();
                        pDataRow[1] = obj;
                       
                        pDataRow.SetField(pDataColumn, area / ZoneArea*100 );
                        landInfo.resultTable.Rows.Add(pDataRow);
                        pDataRow = null;
                    }
                    else
                    {
                        pDataRow.SetField(pDataColumn, area / ZoneArea*100);
                    }
                }

         
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatistics);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumerator);
        }
        /// <summary>
        /// 景观指标计算
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="landInfo"></param>
        public static void LandscapeIndexStats(DataInputInfo dataInput, LandscapeParamInfo landInfo)
        {
            Application.DoEvents();
            if (!checkData())
            {

                MessageBox.Show("请检查数据是否含有Shape_Area和Shape_Length字段！");
                return;
            }

            for (int i = 0; i < landInfo.landIndex.Count; i++)
            {
                switch (landInfo.landIndex[i])
                {
                    case LandscapeIndex.TotalArea:
                        LandTotalArea(dataInput, landInfo);
                        break;
                    case LandscapeIndex.TotalEgde:
                        LandTotalEdge(dataInput, landInfo);
                        break;
                    case LandscapeIndex.EdgeDensity:
                        LandEdgeDensity(dataInput, landInfo);
                        break;
                    case LandscapeIndex.MaxAreaIndex:
                        LandMaxAreaIndex(dataInput, landInfo);
                        break;
                    case LandscapeIndex.PAFRACIndex:
                        LandPAFRAC(dataInput, landInfo);
                        break;
                    case LandscapeIndex.MeshIndex:
                        MeshIndex(dataInput, landInfo);
                        break;
                    case LandscapeIndex.UMeshIndex:
                        UMeshIndex(dataInput, landInfo);
                        break;
                    case LandscapeIndex.MMeshIndex:
                        MMeshIndex(dataInput, landInfo);
                        break;
                    case LandscapeIndex.CWEDIndex:
                        ContrastWeightedED(dataInput, landInfo);
                        break;
                    case LandscapeIndex.TECIIndex:
                        TECIIndex(dataInput, landInfo);
                        break;
                }
                Application.DoEvents();
            }

        }
      
        /// <summary>
        /// 景观总面积
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="landInfo"></param>
        /// 
//         public static void LandTotalArea(DataInputInfo dataInput, LandscapeParamInfo landInfo)
//         {
//             DataTable landTable = landInfo.resultTable;
//             DataColumn pDataColumn = GetTableColumnByName(landTable, "TA(m2)");
//             //获取分区
//             IFeatureClass pFeatureClass = dataInput.layerDataCover;
//             IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
//             IDataStatistics pDataStatistics = new DataStatisticsClass();
//             pDataStatistics.Field = dataInput.zoneField;
//             pDataStatistics.Cursor = pFeatureCursor as ICursor;
//             IEnumerator pEnumerator = pDataStatistics.UniqueValues;
//             pEnumerator.Reset();
// 
//             DataRow pDataRow = null;
// 
//             List<string> zoneId = dataInput.zoneValue;
//             for (int i = 0; i < zoneId.Count;i++ )
//             {
//                 string obj = zoneId[i];
//                  
//                 IFeatureCursor pFeatureCursor1 = dataInput.zones[i];
//                 IDataStatistics pAreaStatistics = new DataStatisticsClass();
//                 pAreaStatistics.Field = "Shape_Area";
//                 pAreaStatistics.Cursor = pFeatureCursor1 as ICursor;
//                 IStatisticsResults pStatistics = pAreaStatistics.Statistics;
//                 string express = "ZID=" + obj;
//                 if (landInfo.resultTable.Select(express).Length > 0)
//                 {
//                     pDataRow = landInfo.resultTable.Select(express)[0];
//                 }
// 
//                 if (pDataRow == null)
//                 {
//                     pDataRow = landInfo.resultTable.NewRow();
//                     pDataRow[1] = obj;
//                     pDataRow.SetField(pDataColumn, pStatistics.Sum);
//                     landInfo.resultTable.Rows.Add(pDataRow);
//                     pDataRow = null;
//                 }
//                 else
//                 {
//                     pDataRow.SetField(pDataColumn, pStatistics.Sum);
//                 }
//             }
// 
//             System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
//             System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatistics);
// 
// 
//         }
        public static void LandTotalArea(DataInputInfo dataInput, LandscapeParamInfo landInfo)
        {
            DataTable landTable = landInfo.resultTable;
            DataColumn pDataColumn = GetTableColumnByName(landTable, "TA(m2)");
          //  获取分区
            IFeatureClass pFeatureClass = dataInput.layerDataCover;
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = dataInput.zoneField;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();

            DataRow pDataRow = null;

            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;
                IQueryFilter pQueryFilter = new QueryFilterClass();
                pQueryFilter.WhereClause = dataInput.zoneField + " = \'" + obj + "\'";
                IFeatureCursor pFeatureCursor1 = pFeatureClass.Search(pQueryFilter, true);
                IDataStatistics pAreaStatistics = new DataStatisticsClass();
                pAreaStatistics.Field = "Shape_Area";
                pAreaStatistics.Cursor = pFeatureCursor1 as ICursor;
                IStatisticsResults pStatistics = pAreaStatistics.Statistics;
                string express = "ZID=" + obj;
                if (landInfo.resultTable.Select(express).Length > 0)
                {
                    pDataRow = landInfo.resultTable.Select(express)[0];
                }

                if (pDataRow == null)
                {
                    pDataRow = landInfo.resultTable.NewRow();
                    pDataRow[1] = obj;
                    pDataRow.SetField(pDataColumn, pStatistics.Sum);
                    landInfo.resultTable.Rows.Add(pDataRow);
                    pDataRow = null;
                }
                else
                {
                    pDataRow.SetField(pDataColumn, pStatistics.Sum);
                }
            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatistics);


        }

        /// <summary>
        /// 景观中面积最大的值
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="landInfo"></param>
        public static void LandMaxAreaIndex(DataInputInfo dataInput, LandscapeParamInfo landInfo)
        {
            //添加字段
            DataTable landTable = landInfo.resultTable;
            DataColumn pDataColumn = GetTableColumnByName(landTable, "LPI(%)"); 

            //获取分区
            IFeatureClass pFeatureClass = dataInput.layerDataCover;
            IFeatureClass pFeatureClassZone = dataInput.layerDataZone;
            int ZoneAreaIndex = pFeatureClassZone.Fields.FindField("Shape_Area");
            IQueryFilter pQueryFilterZone = new QueryFilterClass();
            double ZoneArea = 0.0;

            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = dataInput.zoneField;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();

            DataRow pDataRow = null;

            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;

                pQueryFilterZone.WhereClause = dataInput.zoneField + " = \'" + obj + "\'";
                IFeatureCursor pFeatureCursorZone = pFeatureClassZone.Search(pQueryFilterZone, true);
                try
                {
                    ZoneArea = (double)pFeatureCursorZone.NextFeature().get_Value(ZoneAreaIndex);
                }
                catch (System.Exception ex)
                {
                    string str = obj.ToString() + "查询分区失败！";
                    MessageBox.Show(str);
                }

                IFeatureCursor pFeatureCursor1 = pFeatureClass.Search(pQueryFilterZone, true);
                IDataStatistics pAreaStatistics = new DataStatisticsClass();
                pAreaStatistics.Field = "Shape_Area";
                pAreaStatistics.Cursor = pFeatureCursor1 as ICursor;
                IStatisticsResults pStatistics = pAreaStatistics.Statistics;

                string express = "ZID=" + obj;
                if (landInfo.resultTable.Select(express).Length > 0)
                {
                    pDataRow = landInfo.resultTable.Select(express)[0];
                }

                if (pDataRow == null)
                {
                    pDataRow = landInfo.resultTable.NewRow();
                    pDataRow[1] = obj;
                    if (pStatistics.Count==0)
                    {
                        pDataRow.SetField(pDataColumn, 0);
                    }
                    else
                    {
                        pDataRow.SetField(pDataColumn, pStatistics.Maximum / ZoneArea * 100);
                    }
                    landInfo.resultTable.Rows.Add(pDataRow);
                    pDataRow = null;
                }
                else
                {
                    if (pStatistics.Count == 0)
                    {
                        pDataRow.SetField(pDataColumn, 0);
                    }
                    else
                    {
                        pDataRow.SetField(pDataColumn, pStatistics.Maximum / ZoneArea * 100);
                    }
                }

            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatistics);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumerator);

        }

        /// <summary>
        /// 景观总周长
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="landInfo"></param>
        public static void LandTotalEdge(DataInputInfo dataInput, LandscapeParamInfo landInfo)
        {

            //添加字段
            DataTable landTable = landInfo.resultTable;
            DataColumn pDataColumn = GetTableColumnByName(landTable, "TE(m)"); 

            //获取分区
            IFeatureClass pFeatureClass = dataInput.layerDataCover;
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = dataInput.zoneField;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            //
            IFeatureClass pFeatureClass2 = dataInput.layerDataZone;

            IQueryFilter pQueryFilter = new QueryFilterClass();
            int indexLength = pFeatureClass2.Fields.FindField("Shape_Length");
            if (indexLength<0)
            {
                indexLength = pFeatureClass2.Fields.FindField("Shape_Leng");
            }
            pEnumerator.Reset();

            DataRow pDataRow = null;

            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;
                //FID_renkou_slq2 = 0 AND CC = '0110'
               
                // pQueryFilter.SubFields = paramInfo.zoneID + ",CC,Shape_Area";
                pQueryFilter.WhereClause = dataInput.zoneField + " = \'" + obj+"\'";
                IFeatureCursor pFeatureCursor1 = pFeatureClass.Search(pQueryFilter, true);

                IFeatureCursor pFeatureCursor2 = pFeatureClass2.Search(pQueryFilter, true);
                double boundary = (double)pFeatureCursor2.NextFeature().get_Value(indexLength);

                IDataStatistics pAreaStatistics = new DataStatisticsClass();
                pAreaStatistics.Field = "Shape_Length";
                pAreaStatistics.Cursor = pFeatureCursor1 as ICursor;
                IStatisticsResults pStatistics = pAreaStatistics.Statistics;
                //确定更新还是添加
                string express = "ZID=" + obj;
                if (landInfo.resultTable.Select(express).Length > 0)
                {
                    pDataRow = landInfo.resultTable.Select(express)[0];
                }

                if (pDataRow == null)
                {
                    pDataRow = landInfo.resultTable.NewRow();
                    pDataRow[1] = obj;
                    pDataRow.SetField(pDataColumn, (pStatistics.Sum-boundary)/2);
                    landInfo.resultTable.Rows.Add(pDataRow);
                    pDataRow = null;
                }
                else
                {
                    pDataRow.SetField(pDataColumn, (pStatistics.Sum-boundary)/2);
                }

            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatistics);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumerator);
        }


        /// <summary>
        /// 景观周长/面积
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="landInfo"></param>
        public static void LandEdgeDensity(DataInputInfo dataInput, LandscapeParamInfo landInfo)
        {

            //添加字段
            DataTable landTable = landInfo.resultTable;
            DataColumn pDataColumn = GetTableColumnByName(landTable, "ED(m/hectare)"); 

            //获取分区
            IFeatureClass pFeatureClass = dataInput.layerDataCover;
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = dataInput.zoneField;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();
            //
            IFeatureClass pFeatureClass2 = dataInput.layerDataZone;
            int indexLength = pFeatureClass2.Fields.FindField("Shape_Length");
            IQueryFilter pQueryFilter = new QueryFilterClass();
            if (indexLength < 0)
            {
                indexLength = pFeatureClass2.Fields.FindField("Shape_Leng");
            }
            DataRow pDataRow = null;

            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;
                pQueryFilter.WhereClause = dataInput.zoneField + " = \'" + obj+"\'";
                IFeatureCursor pFeatureCursor1 = pFeatureClass.Search(pQueryFilter, true);
                //
                IFeatureCursor pFeatureCursor3 = pFeatureClass2.Search(pQueryFilter, true);
                IFeature pLengthFeature = pFeatureCursor3.NextFeature();
                double zoneLength = (double)pLengthFeature.get_Value(indexLength);
                //

                //计算周长
                IDataStatistics pEdgeStatistics = new DataStatisticsClass();
                pEdgeStatistics.Field = "Shape_Length";
                pEdgeStatistics.Cursor = pFeatureCursor1 as ICursor;
                IStatisticsResults pStatistics = pEdgeStatistics.Statistics;
                //确定更新还是添加
                //计算面积
                IFeatureCursor pFeatureCursor2 = pFeatureClass.Search(pQueryFilter, true);
                IDataStatistics pAreaStatistics = new DataStatisticsClass();
                pAreaStatistics.Field = "Shape_Area";
                pAreaStatistics.Cursor = pFeatureCursor2 as ICursor;
                IStatisticsResults pStatisticsResult = pAreaStatistics.Statistics;

                string express = "ZID=" + obj;
                if (landInfo.resultTable.Select(express).Length > 0)
                {
                    pDataRow = landInfo.resultTable.Select(express)[0];
                }

                if (pDataRow == null)
                {
                    pDataRow = landInfo.resultTable.NewRow();
                    pDataRow[1] = obj;
                    pDataRow.SetField(pDataColumn, (pStatistics.Sum-zoneLength)*0.5/pStatisticsResult.Sum*10000);
                    landInfo.resultTable.Rows.Add(pDataRow);
                    pDataRow = null;
                }
                else
                {
                    pDataRow.SetField(pDataColumn, (pStatistics.Sum - zoneLength) * 0.5 / pStatisticsResult.Sum*10000);
                }

              

            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatistics);
             
        }
        /// <summary>
        /// 景观周长-面积破碎指数
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="landInfo"></param>
        public static void LandPAFRAC(DataInputInfo dataInput, LandscapeParamInfo landInfo)
        {
            //具体公式有待商榷
            DataTable clssTable = landInfo.resultTable;
            DataColumn pDataColumn = GetTableColumnByName(clssTable, "LPAFRACIndex(None)");

            //
            IFeatureClass pFeatureClass = dataInput.layerDataCover;
            IFields pFields = pFeatureClass.Fields;
            int indexArea = pFields.FindField("Shape_Area");
            int indexLeng = pFields.FindField("Shape_Length");
            if (indexLeng < 0)
            {
                indexLeng = pFields.FindField("Shape_Leng");
            }
            //分区
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = dataInput.zoneField;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();
            //分类
            IFeatureCursor pFeatureCursorClss = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatisticsClss = new DataStatisticsClass();
            pDataStatisticsClss.Field = dataInput.codeField;
            pDataStatisticsClss.Cursor = pFeatureCursorClss as ICursor;
            IEnumerator pEnumeratorClss = pDataStatisticsClss.UniqueValues;
           

           

            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;

                int NCount = 0;
                double lnpij = 0;
                double lnaij = 0;
                double lnpij2 = 0;
                double lnaijlnpij = 0;
                pEnumeratorClss.Reset();
                while (pEnumeratorClss.MoveNext())
                {

                   
                    
                    object objclss = pEnumeratorClss.Current;
                    IQueryFilter pQueryFilterClss = new QueryFilterClass();
                    pQueryFilterClss.WhereClause = dataInput.zoneField + " = \'" + obj + "\' AND " + dataInput.codeField + " = " + "\'" +  objclss + "\'";
                    IFeatureCursor pFeatureCursorClss2= pFeatureClass.Search(pQueryFilterClss, true);
                    IFeature pFeature = pFeatureCursorClss2.NextFeature();

                    while (pFeature != null)
                    {
                        NCount++;
                        double areatemp = (double)pFeature.get_Value(indexArea);
                        double lengtemp = (double)pFeature.get_Value(indexLeng);
                        lnpij += System.Math.Log(lengtemp, Math.E);
                        lnaij += System.Math.Log(areatemp, Math.E);
                        lnpij2 += System.Math.Log(lengtemp, Math.E) * System.Math.Log(lengtemp, Math.E);
                        lnaijlnpij += System.Math.Log(lengtemp, Math.E) * System.Math.Log(areatemp, Math.E);
                        pFeature = pFeatureCursorClss2.NextFeature();
                    }
                   
                }
               
                double result = 2.0 / ((NCount * lnaijlnpij - lnaij * lnpij) / (NCount * lnpij2 - lnpij * lnpij));
                DataRow pDataRow = null;
                string express = "ZID=" + obj;
                if (landInfo.resultTable.Select(express).Length > 0)
                {
                    pDataRow = landInfo.resultTable.Select(express)[0];
                }

                if (pDataRow == null)
                {
                    pDataRow = landInfo.resultTable.NewRow();
                    pDataRow[1] = obj;
                    pDataRow.SetField(pDataColumn,result);
                    landInfo.resultTable.Rows.Add(pDataRow);
                    pDataRow = null;
                }
                else
                {
                    pDataRow.SetField(pDataColumn, result);
                }
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatistics);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursorClss);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatisticsClss);

 
        }
        #endregion

        #region 类别指标
        /// <summary>
        /// 类别指标计算
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="classInfo"></param>
        public static void ClassIndexStats(DataInputInfo dataInput,
            ClassParamInfo classInfo)
        {
            Application.DoEvents();
            if (!checkData())
            {

                MessageBox.Show("请检查数据是否含有Shape_Area和Shape_Length字段！");
                return;
            }
            for (int i = 0; i < classInfo.clssIndex.Count; i++)
            {


                switch (classInfo.clssIndex[i])
                {

                    case ClassIndex.TotalArea:
                        TotalArea(dataInput, classInfo);
                        break;
                    case ClassIndex.AreaRatio:

                        AreaRatio(MainForm.baseData, classInfo);
                        break;
                    case ClassIndex.TotalEgde:
                        TotalEgde(dataInput, classInfo);

                        break;
                    case ClassIndex.EdgeDensity:
                        EdgeDensity(dataInput, classInfo);
                        break;
                    case ClassIndex.MaxAreaIndex:
                        AreaIndex(dataInput, classInfo);
                        break;
                    case ClassIndex.MeshIndex:
                        MeshIndex(dataInput, classInfo);
                        break;
                    case ClassIndex.UMeshIndex:
                        UMeshIndex(dataInput, classInfo);
                        break;
                    case ClassIndex.MMeshIndex:
                        MMeshIndex(dataInput, classInfo);
                        break;
                    case ClassIndex.PAFRACIndex:
                        PAFRACIndex(dataInput, classInfo);
                        break;
                    case ClassIndex.CWEDIndex:
                        ContrastWeightedED(dataInput, classInfo);
                        break;
                    case ClassIndex.TECIIndex:
                        TECIIndex(dataInput, classInfo);
                        break;

                }

                Application.DoEvents();

            }



        }
        /// <summary>
        /// 对比度：边界对比度
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="classInfo"></param>
        public static void TECIIndex(DataInputInfo dataInput, ClassParamInfo classInfo)
        {

            //具体公式有待商榷
            DataTable clssTable = classInfo.resultTable;
            DataColumn pDataColumn = GetTableColumnByName(clssTable, "TECI(%)");

            //
            IFeatureClass pFeatureClass = dataInput.layerDataCover;
            IFields pFields = pFeatureClass.Fields;
            int indexArea = pFields.FindField("Shape_Area");
            int indexLeng = pFields.FindField("Shape_Length");
            if (indexLeng < 0)
            {
                indexLeng = pFields.FindField("Shape_Leng");
            }
            int indexCode = pFields.FindField(dataInput.codeField);
            //
            
            //
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = dataInput.zoneField;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();

            DataRow pDataRow = null;
            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;

                 
                 double result = 0.0;
                 double totallen = 0.0;

                //按类别统计
                for (int i = 0; i < classInfo.clss.Count; i++)
                {
                    IQueryFilter pQueryFilterClss = new QueryFilterClass();
                    pQueryFilterClss.WhereClause = dataInput.zoneField + " = \'" + obj+"\'" + " AND " + dataInput.codeField + " = " + "\'" + classInfo.clss[i] + "\'";
                    IFeatureCursor pFeatureCursorClss = pFeatureClass.Search(pQueryFilterClss, true);
                    IFeature pFeature = pFeatureCursorClss.NextFeature();

                    while (pFeature != null)
                    {

                        ISpatialFilter pSpatialFilter = new SpatialFilterClass();
                        pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                        pSpatialFilter.GeometryField = pFeatureClass.ShapeFieldName;
                        pSpatialFilter.Geometry = pFeature.Shape as IGeometry;
                        IFeatureCursor pFeatureSpatialCursor = pFeatureClass.Search(pSpatialFilter, true);
                        IFeature pFeatureSpatial = pFeatureSpatialCursor.NextFeature();
                        int count = 0;
                        while (pFeatureSpatial != null)
                        {
                            count++;
                            ICurve polygon = pFeature.Shape as ICurve;
                            ICurve polygon2 = pFeatureSpatial.Shape as ICurve;

                            ITopologicalOperator pTopo = polygon as ITopologicalOperator;
                            ICurve pCurve = pTopo.Intersect(polygon2 as IGeometry, esriGeometryDimension.esriGeometry1Dimension) as ICurve;
                            result += pCurve.Length * Forms.ClssFrm.GetWeightFromDt(Forms.ClssFrm.weightDt, pFeature.get_Value(indexCode).ToString(), pFeatureSpatial.get_Value(indexCode).ToString());
                            totallen += pCurve.Length;
                            pFeatureSpatial = pFeatureSpatialCursor.NextFeature();
                        }
                        pFeature = pFeatureCursorClss.NextFeature();
                        //MessageBox.Show(count.ToString());
                    }


                    //确定更新还是添加
                    string express = "ZID=" + obj + " AND Type=" + "\'" + MainForm.pData[classInfo.clss[i].ToString()] + "\'";
                    if (classInfo.resultTable.Select(express).Length > 0)
                    {
                        pDataRow = classInfo.resultTable.Select(express)[0];
                    }

                    if (pDataRow == null)
                    {
                        pDataRow = classInfo.resultTable.NewRow();
                        pDataRow[1] = obj;
                        pDataRow[2] = MainForm.pData[classInfo.clss[i].ToString()];
                        pDataRow.SetField(pDataColumn, result / totallen*100);
                        classInfo.resultTable.Rows.Add(pDataRow);
                        pDataRow = null;
                    }
                    else
                    {
                        pDataRow.SetField(pDataColumn, result /totallen*100);
                    }
                }

            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatistics);
        }
        /// <summary>
        /// 对比度：边界密度
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="classInfo"></param>
        public static void ContrastWeightedED(DataInputInfo dataInput, ClassParamInfo classInfo)
        {

            //具体公式有待商榷
            DataTable classTable = classInfo.resultTable;
            DataColumn pDataColumn = GetTableColumnByName(classTable, "CWED(m/hectare)");

            //
            IFeatureClass pFeatureClass = dataInput.layerDataCover;
            IFields pFields = pFeatureClass.Fields;
            int indexArea = pFields.FindField("Shape_Area");
            int indexLeng = pFields.FindField("Shape_Length");
            if (indexLeng<0)
            {
                indexLeng = pFields.FindField("Shape_Leng");
            }
            int indexCode = pFields.FindField(dataInput.codeField);
            //
            IFeatureClass pFeatureClassZone = dataInput.layerDataZone;
            IQueryFilter pQueryFilterZone = new QueryFilterClass();
            double ZoneArea = 0.0;
            int ZoneAreaIndex = pFeatureClassZone.FindField("Shape_Area");
            //
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = dataInput.zoneField;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();

            DataRow pDataRow = null;
            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;
               
                pQueryFilterZone.WhereClause = dataInput.zoneField + " = \'" + obj+"\'";
                IFeatureCursor pFeatureCursorZone = pFeatureClassZone.Search(pQueryFilterZone, true);
                try
                {
                    ZoneArea = (double)pFeatureCursorZone.NextFeature().get_Value(ZoneAreaIndex);
                }
                catch (System.Exception ex)
                {
                    string str = obj.ToString() + "查询分区失败！";
                    MessageBox.Show(str);
                }
                double result = 0.0;
               // double totallen = 0.0;

                //按类别统计
                for (int i = 0; i < classInfo.clss.Count; i++)
                {
                    IQueryFilter pQueryFilterClss = new QueryFilterClass();
                    pQueryFilterClss.WhereClause = dataInput.zoneField + " = \'" + obj +"\'"+ " AND " + dataInput.codeField + " = " + "\'" + classInfo.clss[i] + "\'";
                    IFeatureCursor pFeatureCursorClss = pFeatureClass.Search(pQueryFilterClss, true);
                    IFeature pFeature = pFeatureCursorClss.NextFeature();

                    while (pFeature != null)
                    {

                        ISpatialFilter pSpatialFilter = new SpatialFilterClass();
                        pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                        pSpatialFilter.GeometryField = pFeatureClass.ShapeFieldName;
                        pSpatialFilter.Geometry = pFeature.Shape as IGeometry;
                        IFeatureCursor pFeatureSpatialCursor = pFeatureClass.Search(pSpatialFilter, true);
                        IFeature pFeatureSpatial = pFeatureSpatialCursor.NextFeature();
                        int count = 0;
                        while (pFeatureSpatial != null)
                        {
                            count++;
                            ICurve polygon = pFeature.Shape as ICurve;
                            ICurve polygon2 = pFeatureSpatial.Shape as ICurve;

                            ITopologicalOperator pTopo = polygon as ITopologicalOperator;
                            ICurve pCurve = pTopo.Intersect(polygon2 as IGeometry, esriGeometryDimension.esriGeometry1Dimension) as ICurve;
                            result += pCurve.Length * Forms.ClssFrm.GetWeightFromDt(Forms.ClssFrm.weightDt, pFeature.get_Value(indexCode).ToString(), pFeatureSpatial.get_Value(indexCode).ToString());
                           // totallen += pCurve.Length;
                            pFeatureSpatial = pFeatureSpatialCursor.NextFeature();
                        }
                        pFeature = pFeatureCursorClss.NextFeature();
                        //MessageBox.Show(count.ToString());
                    }


                    //确定更新还是添加
                    string express = "ZID=" + obj + " AND Type=" + "\'" + MainForm.pData[classInfo.clss[i].ToString()] + "\'";
                    if (classInfo.resultTable.Select(express).Length > 0)
                    {
                        pDataRow = classInfo.resultTable.Select(express)[0];
                    }

                    if (pDataRow == null)
                    {
                        pDataRow = classInfo.resultTable.NewRow();
                        pDataRow[1] = obj;
                        pDataRow[2] = MainForm.pData[classInfo.clss[i].ToString()];
                        pDataRow.SetField(pDataColumn, result / ZoneArea);
                        classInfo.resultTable.Rows.Add(pDataRow);
                        pDataRow = null;
                    }
                    else
                    {
                        pDataRow.SetField(pDataColumn, result / ZoneArea);
                    }
                }

            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatistics);
        }
        /// <summary>
        /// 周长-面积碎片指数
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="classInfo"></param>
        public static void PAFRACIndex(DataInputInfo dataInput, ClassParamInfo classInfo)
        {

            //具体公式有待商榷
            DataTable clssTable = classInfo.resultTable;
            DataColumn pDataColumn = GetTableColumnByName(clssTable, "PAFRACIndex(None)");

            //
            IFeatureClass pFeatureClass = dataInput.layerDataCover;
            IFields pFields = pFeatureClass.Fields;
            int indexArea = pFields.FindField("Shape_Area");
            int indexLeng = pFields.FindField("Shape_Length");
            if (indexLeng<0)
            {
                indexLeng = pFields.FindField("Shape_Leng");
            }
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = dataInput.zoneField;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();

            DataRow pDataRow = null;
            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;


                //按类别统计
                for (int i = 0; i < classInfo.clss.Count; i++)
                {
                    //FID_renkou_slq2 = 0 AND CC = '0110'
                    IQueryFilter pQueryFilterClss = new QueryFilterClass();
                    // pQueryFilter.SubFields = paramInfo.zoneID + ",CC,Shape_Area";
                    pQueryFilterClss.WhereClause = dataInput.zoneField + " = \'" + obj + "\' AND " + dataInput.codeField + " = " + "\'" + classInfo.clss[i] + "\'";
                    IFeatureCursor pFeatureCursorClss = pFeatureClass.Search(pQueryFilterClss, true);
                    IFeature pFeature = pFeatureCursorClss.NextFeature();
                    double lnpij = 0;
                    double lnaij = 0;
                    double lnpij2 = 0;
                    double lnaijlnpij = 0;
                    int count = 0;


                    while (pFeature != null)
                    {
                        count++;
                        double areatemp = (double)pFeature.get_Value(indexArea);
                        double lengtemp = (double)pFeature.get_Value(indexLeng);
                        lnpij += System.Math.Log(lengtemp, Math.E);
                        lnaij += System.Math.Log(areatemp, Math.E);
                        lnpij2 += System.Math.Log(lengtemp, Math.E) * System.Math.Log(lengtemp, Math.E);
                        lnaijlnpij += System.Math.Log(lengtemp, Math.E) * System.Math.Log(areatemp, Math.E);

                        pFeature = pFeatureCursorClss.NextFeature();
                    }
                    double result = 2.0/((count * lnaijlnpij - lnaij * lnpij) / (count * lnpij2 - lnpij * lnpij));

                    //确定更新还是添加
                    string express = "ZID=" + obj + " AND Type=" + "\'" + MainForm.pData[classInfo.clss[i].ToString()] + "\'";
                    if (classInfo.resultTable.Select(express).Length > 0)
                    {
                        pDataRow = classInfo.resultTable.Select(express)[0];
                    }

                    if (pDataRow == null)
                    {
                        pDataRow = classInfo.resultTable.NewRow();
                        pDataRow[1] = obj;
                        pDataRow[2] = MainForm.pData[classInfo.clss[i].ToString()];
                        pDataRow.SetField(pDataColumn, result);
                        classInfo.resultTable.Rows.Add(pDataRow);
                        pDataRow = null;
                    }
                    else
                    {
                        pDataRow.SetField(pDataColumn, result);
                    }
                }

            }
        }
        /// <summary>
        /// 未受交通网络分割区域面积比率
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="classInfo"></param>
        public static void UMeshIndex(DataInputInfo dataInput, ClassParamInfo classInfo)
        {
            //具体公式有待商榷
            DataTable clssTable = classInfo.resultTable;
            DataColumn pDataColumn = GetTableColumnByName(clssTable, "UMI(km2)");

            //
            IFeatureClass pFeatureClass = dataInput.layerDataCover;
            IFields pFields = pFeatureClass.Fields;
            int indexArea = pFields.FindField("Shape_Area");
            IFeatureClass pFeatureClassZone = dataInput.layerDataZone;
            int ZoneAreaIndex = pFeatureClassZone.Fields.FindField("Shape_Area");
            IQueryFilter pQueryFilterZone = new QueryFilterClass();
            double ZoneArea = 0.0;
            //
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = dataInput.zoneField;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();

            DataRow pDataRow = null;
            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;

                pQueryFilterZone.WhereClause = dataInput.zoneField + " = \'" + obj + "\'";
                IFeatureCursor pFeatureCursorZone = pFeatureClassZone.Search(pQueryFilterZone, true);
                try
                {
                    ZoneArea = (double)pFeatureCursorZone.NextFeature().get_Value(ZoneAreaIndex);
                }
                catch (System.Exception ex)
                {
                    string str = obj.ToString() + "查询分区失败！";
                    MessageBox.Show(str);
                }
                //按类别统计
                for (int i = 0; i < classInfo.clss.Count; i++)
                {
                    //FID_renkou_slq2 = 0 AND CC = '0110'
                    IQueryFilter pQueryFilterClss = new QueryFilterClass();
                    // pQueryFilter.SubFields = paramInfo.zoneID + ",CC,Shape_Area";
                    pQueryFilterClss.WhereClause = dataInput.zoneField + " = \'" + obj + "\' AND " + dataInput.codeField + " = " + "\'" + classInfo.clss[i] + "\'";
                    IFeatureCursor pFeatureCursorClss = pFeatureClass.Search(pQueryFilterClss, true);
                    IFeature pFeature = pFeatureCursorClss.NextFeature();
                    double area = 0;
                    while (pFeature != null)
                    {

                        double areatemp = (double)pFeature.get_Value(indexArea);
//                         if (areatemp>=Forms.ClssFrm.limitValue*1000000)
//                         {
//                             area += areatemp;
//                         }
                        
                        pFeature = pFeatureCursorClss.NextFeature();
                    }


                    //确定更新还是添加
                    string express = "ZID=" + obj + " AND Type=" + "\'" + MainForm.pData[classInfo.clss[i].ToString()] + "\'";
                    if (classInfo.resultTable.Select(express).Length > 0)
                    {
                        pDataRow = classInfo.resultTable.Select(express)[0];
                    }

                    if (pDataRow == null)
                    {
                        pDataRow = classInfo.resultTable.NewRow();
                        pDataRow[1] = obj;
                        pDataRow[2] = MainForm.pData[classInfo.clss[i].ToString()];
                        pDataRow.SetField(pDataColumn, area / ZoneArea /1000000);
                        classInfo.resultTable.Rows.Add(pDataRow);
                        pDataRow = null;
                    }
                    else
                    {
                        pDataRow.SetField(pDataColumn, area / ZoneArea/1000000);
                    }
                }

            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatistics);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumerator);
        }
        /// <summary>
        ///  修正后的破碎指标
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="classInfo"></param>
        public static void MMeshIndex(DataInputInfo dataInput, ClassParamInfo classInfo)
        {

            //读取配置图层
            ILayer pLayer = Forms.ClssFrm.pLayer;
            IFeatureClass spatialFC = (pLayer as IFeatureLayer).FeatureClass;
            int indexSpatialArea = spatialFC.Fields.FindField("Shape_Area");
            //读取结果表
            DataTable clssTable = classInfo.resultTable;
            DataColumn pDataColumn = GetTableColumnByName(clssTable, "MMI(km2)");
            //读取覆盖图层
            IFeatureClass pFeatureClass = dataInput.layerDataCover;
            IFields pFields = pFeatureClass.Fields;
            int indexArea = pFields.FindField("Shape_Area");
            IFeatureClass pFeatureClassZone = dataInput.layerDataZone;
            int ZoneAreaIndex = pFeatureClassZone.Fields.FindField("Shape_Area");
            IQueryFilter pQueryFilterZone = new QueryFilterClass();
            double ZoneArea = 0.0;

            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = dataInput.zoneField;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();


            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;
                pQueryFilterZone.WhereClause = dataInput.zoneField + " = \'" + obj + "\'";
                IFeatureCursor pFeatureCursorZone = pFeatureClassZone.Search(pQueryFilterZone, true);
                try
                {
                    ZoneArea = (double)pFeatureCursorZone.NextFeature().get_Value(ZoneAreaIndex);
                }
                catch (System.Exception ex)
                {
                    string str = obj.ToString() + "查询分区失败！";
                    MessageBox.Show(str);
                }
                //按类别统计
                for (int i = 0; i < classInfo.clss.Count; i++)
                {
                    //FID_renkou_slq2 = 0 AND CC = '0110'
                    IQueryFilter pQueryFilterClss = new QueryFilterClass();
                    // pQueryFilter.SubFields = paramInfo.zoneID + ",CC,Shape_Area";
                    pQueryFilterClss.WhereClause = dataInput.zoneField + " = \'" + obj + "\' AND " + dataInput.codeField + " = " + "\'" + classInfo.clss[i] + "\'";
                    IFeatureCursor pFeatureCursorClss = pFeatureClass.Search(pQueryFilterClss, true);
                    IFeature pFeature = pFeatureCursorClss.NextFeature();
                    /////////////////////////////////////////////////////////
                    double area = 0;
                    int count = 0;
                    while (pFeature != null)
                    {
                        //////////////////////////////////////////////////////////////////////////
                        //空间查询有待修改
                        int fid = (int)pFeature.get_Value(0);
                        ISpatialFilter spatialFilter = new SpatialFilterClass();
                        spatialFilter.Geometry = pFeature.Shape;
                        spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                        spatialFilter.WhereClause = dataInput.codeField + " = " + "\'" + classInfo.clss[i] + "\'";
                        spatialFilter.SearchOrder = esriSearchOrder.esriSearchOrderAttribute;
                        spatialFilter.GeometryField = spatialFC.ShapeFieldName;
                        IFeatureCursor pSpatialFCursor = spatialFC.Search(spatialFilter, false);
                        IFeature pSpatialFeature = pSpatialFCursor.NextFeature();

                        int fidsp = 0;
                        double computeArea = 0;
                        //待修改
                        while (pSpatialFeature != null)
                        {

                            fidsp = (int)pSpatialFeature.get_Value(0);
                            count++;
                            computeArea += (double)pSpatialFeature.get_Value(indexSpatialArea);
                            pSpatialFeature = pSpatialFCursor.NextFeature();


                        }
                        double areatemp = (double)pFeature.get_Value(indexArea) * computeArea;
                        area += areatemp;
                        pFeature = pFeatureCursorClss.NextFeature();
                        //释放资源
                        if (pSpatialFCursor != null)
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(pSpatialFCursor);
                        }
                        if (pSpatialFeature != null)
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(pSpatialFeature);
                        }
                        if (spatialFilter != null)
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(spatialFilter);
                        }


                    }//end of while
                    //  MessageBox.Show(count.ToString());
                    DataRow pDataRow = null;
                    //确定更新还是添加
                    string express = "ZID=" + obj + " AND Type=" + "\'" + MainForm.pData[classInfo.clss[i].ToString()] + "\'";
                    if (classInfo.resultTable.Select(express).Length > 0)
                    {
                        pDataRow = classInfo.resultTable.Select(express)[0];
                    }

                    if (pDataRow == null)
                    {
                        pDataRow = classInfo.resultTable.NewRow();
                        pDataRow[1] = obj;
                        pDataRow[2] = MainForm.pData[classInfo.clss[i].ToString()];
                        pDataRow.SetField(pDataColumn, area /ZoneArea / 1000000);
                        classInfo.resultTable.Rows.Add(pDataRow);
                        pDataRow = null;
                    }
                    else
                    {
                        pDataRow.SetField(pDataColumn, area / ZoneArea / 1000000);
                    }

                    if (pQueryFilterClss != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(pQueryFilterClss);

                    }
                    if (pFeatureCursorClss != null)
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursorClss);
                    }
                }//end of for

            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatistics);

        }
        /// <summary>
        /// 破碎度mesh
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="landInfo"></param>
        public static void MeshIndex(DataInputInfo dataInput, ClassParamInfo classInfo)
        {
            //具体公式有待商榷
            DataTable clssTable = classInfo.resultTable;
            DataColumn pDataColumn = GetTableColumnByName(clssTable, "MI(km2)");
            
            //
            IFeatureClass pFeatureClass = dataInput.layerDataCover;
            IFields pFields = pFeatureClass.Fields;
            int indexArea = pFields.FindField("Shape_Area");
            IFeatureClass pFeatureClassZone = dataInput.layerDataZone;
            int ZoneAreaIndex = pFeatureClassZone.Fields.FindField("Shape_Area");
            IQueryFilter pQueryFilterZone = new QueryFilterClass();
            double ZoneArea = 0.0;
            //
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = dataInput.zoneField;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();

            DataRow pDataRow = null;
            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;
                pQueryFilterZone.WhereClause = dataInput.zoneField + " = \'" + obj + "\'";
                IFeatureCursor pFeatureCursorZone = pFeatureClassZone.Search(pQueryFilterZone, true);
                try
                {
                    ZoneArea = (double)pFeatureCursorZone.NextFeature().get_Value(ZoneAreaIndex);
                }
                catch (System.Exception ex)
                {
                    string str = obj.ToString() + "查询分区失败！";
                    MessageBox.Show(str);
                }      

                //按类别统计
                for (int i = 0; i < classInfo.clss.Count; i++)
                {
                    //FID_renkou_slq2 = 0 AND CC = '0110'
                    IQueryFilter pQueryFilterClss = new QueryFilterClass();
                    // pQueryFilter.SubFields = paramInfo.zoneID + ",CC,Shape_Area";
                    pQueryFilterClss.WhereClause = dataInput.zoneField + " = \'" + obj + "\' AND " + dataInput.codeField + " = " + "\'" + classInfo.clss[i] + "\'";
                    IFeatureCursor pFeatureCursorClss = pFeatureClass.Search(pQueryFilterClss, true);
                    IFeature pFeature = pFeatureCursorClss.NextFeature();
                    double area = 0;
                    while (pFeature != null)
                    {

                        double areatemp = (double)pFeature.get_Value(indexArea);
                        area += areatemp * areatemp;
                        pFeature = pFeatureCursorClss.NextFeature();
                    }
 

                    //确定更新还是添加
                    string express = "ZID=" + obj + " AND Type=" + "\'" + MainForm.pData[classInfo.clss[i].ToString()] + "\'"; 
                    if (classInfo.resultTable.Select(express).Length > 0)
                    {
                        pDataRow = classInfo.resultTable.Select(express)[0];
                    }

                    if (pDataRow == null)
                    {
                        pDataRow = classInfo.resultTable.NewRow();
                        pDataRow[1] = obj;
                        pDataRow[2] = MainForm.pData[classInfo.clss[i].ToString()];
                        pDataRow.SetField(pDataColumn, area / ZoneArea/1000000.0);
                        classInfo.resultTable.Rows.Add(pDataRow);
                        pDataRow = null;
                    }
                    else
                    {
                        pDataRow.SetField(pDataColumn, area / ZoneArea/1000000.0);
                    }
                }

            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatistics);
            
        }
        /// <summary>
        /// 总周长
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="clssInput"></param>
        public static void TotalEgde(DataInputInfo dataInput, ClassParamInfo clssInput)
        {
            //添字段
            DataTable clssTable = clssInput.resultTable;
            DataColumn pDataColumn = GetTableColumnByName(clssTable, "TE(m)");

            IFeatureClass pFeatureClass = dataInput.layerDataCover;
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            //获取分区唯一值
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = dataInput.zoneField;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();

            DataRow pDataRow = null;

            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;
                for (int i = 0; i < clssInput.clss.Count; i++)
                {
                    //FID_renkou_slq2 = 0 AND CC = '0110'
                    IQueryFilter pQueryFilter = new QueryFilterClass();
                    // pQueryFilter.SubFields = paramInfo.zoneID + ",CC,Shape_Area";
                    pQueryFilter.WhereClause = dataInput.zoneField + " = \'" + obj + "\' AND " + dataInput.codeField + " = " + "\'" + clssInput.clss[i] + "\'";
                    IFeatureCursor pFeatureCursor1 = pFeatureClass.Search(pQueryFilter, true);

                    IDataStatistics pEdgeStatistics = new DataStatisticsClass();
                    pEdgeStatistics.Field = "Shape_Length";
                    pEdgeStatistics.Cursor = pFeatureCursor1 as ICursor;
                    IStatisticsResults pStatistics = pEdgeStatistics.Statistics;

                    //确定更新还是添加
                    string express = "ZID=" + obj + " AND Type=" + "\'" + MainForm.pData[clssInput.clss[i].ToString()] + "\'";
                    if (clssInput.resultTable.Select(express).Length > 0)
                    {
                        pDataRow = clssInput.resultTable.Select(express)[0];
                    }

                    if (pDataRow == null)
                    {
                        pDataRow = clssInput.resultTable.NewRow();
                        pDataRow[1] = obj;
                        pDataRow[2] = MainForm.pData[clssInput.clss[i].ToString()];
                        pDataRow.SetField(pDataColumn, pStatistics.Sum);
                        clssInput.resultTable.Rows.Add(pDataRow);
                        pDataRow = null;
                    }
                    else
                    {
                        pDataRow.SetField(pDataColumn, pStatistics.Sum);
                    }

                }


            }

            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatistics);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumerator);

        }
        /// <summary>
        /// 周长比
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="clssInput"></param>
        public static void EdgeDensity(DataInputInfo dataInput, ClassParamInfo clssInput)
        {

            //添字段
            DataTable clssTable = clssInput.resultTable;
            DataColumn pDataColumn = GetTableColumnByName(clssTable, "ED(m/hectare)");

            IFeatureClass pFeatureClass = dataInput.layerDataCover;
            IFeatureClass pFeatureClassZone = dataInput.layerDataZone;
            int ZoneAreaIndex = pFeatureClassZone.Fields.FindField("Shape_Area");
            IQueryFilter pQueryFilterZone = new QueryFilterClass();
            double ZoneArea = 0.0;
            //获取分区
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = dataInput.zoneField;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();

            DataRow pDataRow = null;

            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;
                pQueryFilterZone.WhereClause = dataInput.zoneField + " = \'" + obj + "\'";
                IFeatureCursor pFeatureCursorZone = pFeatureClassZone.Search(pQueryFilterZone, true);
                try
                {
                    ZoneArea = (double)pFeatureCursorZone.NextFeature().get_Value(ZoneAreaIndex);
                }
                catch (System.Exception ex)
                {
                    string str = obj.ToString() + "查询分区失败！";
                    MessageBox.Show(str);
                }

                for (int i = 0; i < clssInput.clss.Count; i++)
                {
                    //FID_renkou_slq2 = 0 AND CC = '0110'
                    IQueryFilter pQueryFilter = new QueryFilterClass();
                    // pQueryFilter.SubFields = paramInfo.zoneID + ",CC,Shape_Area";
                    pQueryFilter.WhereClause = dataInput.zoneField + " = \'" + obj + "\' AND " + dataInput.codeField + " = " + "\'" + clssInput.clss[i] + "\'";
                    IFeatureCursor pFeatureCursor1 = pFeatureClass.Search(pQueryFilter, true);

                    //计算周长
                    IDataStatistics pEdgeStatistics = new DataStatisticsClass();
                    pEdgeStatistics.Field = "Shape_Length";
                    pEdgeStatistics.Cursor = pFeatureCursor1 as ICursor;
                    IStatisticsResults pStatisticsEdge = pEdgeStatistics.Statistics;

                    //确定更新还是添加
                    string express = "ZID=" + obj + " AND Type=" + "\'" + MainForm.pData[clssInput.clss[i].ToString()] + "\'";
                    if (clssInput.resultTable.Select(express).Length > 0)
                    {
                        pDataRow = clssInput.resultTable.Select(express)[0];
                    }

                    if (pDataRow == null)
                    {
                        pDataRow = clssInput.resultTable.NewRow();
                        pDataRow[1] = obj;
                        pDataRow[2] = MainForm.pData[clssInput.clss[i].ToString()];
                        pDataRow.SetField(pDataColumn, pStatisticsEdge.Sum / ZoneArea*10000);
                        clssInput.resultTable.Rows.Add(pDataRow);
                        pDataRow = null;
                    }
                    else
                    {

                        pDataRow.SetField(pDataColumn, pStatisticsEdge.Sum / ZoneArea*10000);
                    }

                }

            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatistics);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumerator);
        }
        /// <summary>
        /// 统计面积和
        /// </summary>
        /// <param name="paramInfo"></param>
        /// <returns></returns>
        /// 
        public static void TotalArea(DataInputInfo dataInput, ClassParamInfo clssInput)
        {


            //添加面积字段
            DataTable clssTable = clssInput.resultTable;
            DataColumn pDataColumn = GetTableColumnByName(clssTable, "TA(m2)");

            IFeatureClass pFeatureClass = dataInput.layerDataCover;

            //获取分区

            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = dataInput.zoneField;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();

            DataRow pDataRow = null;

            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;
                for (int i = 0; i < clssInput.clss.Count; i++)
                {
                    //FID_renkou_slq2 = 0 AND CC = '0110'
                    //dataInput.zoneField + " = \'" + obj + "\' AND "
                    IQueryFilter pQueryFilter = new QueryFilterClass();
                    // pQueryFilter.SubFields = paramInfo.zoneID + ",CC,Shape_Area";
                    pQueryFilter.WhereClause = dataInput.zoneField + " = \'" + obj + "\' AND " + dataInput.codeField + " = " + "\'" + clssInput.clss[i] + "\'";
                    IFeatureCursor pFeatureCursor1 = pFeatureClass.Search(pQueryFilter, true);

                    //计算总面积
                    IDataStatistics pStatistics = new DataStatisticsClass();
                    pStatistics.Field = "Shape_Area";
                    pStatistics.Cursor = pFeatureCursor1 as ICursor;
                    IStatisticsResults pStatisticsResult = pStatistics.Statistics;

                    //确定更新还是添加
                    string express = "ZID=" + obj + " AND Type=" + "\'" + MainForm.pData[clssInput.clss[i].ToString()] + "\'";
                    if (clssInput.resultTable.Select(express).Length > 0)
                    {
                        pDataRow = clssInput.resultTable.Select(express)[0];
                    }

                    if (pDataRow == null)
                    {
                        pDataRow = clssInput.resultTable.NewRow();
                        pDataRow[1] = obj;
                        pDataRow[2] = MainForm.pData[clssInput.clss[i].ToString()];
                        pDataRow.SetField(pDataColumn, pStatisticsResult.Sum);
                        clssInput.resultTable.Rows.Add(pDataRow);
                        pDataRow = null;
                    }
                    else
                    {
                        pDataRow.SetField(pDataColumn, pStatisticsResult.Sum);
                    }

                }


            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatistics);


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="clssInput"></param>
        public static void AreaRatio(Model.BaseData dataInput, ClassParamInfo clssInput)
        {
            DataTable clssTable = clssInput.resultTable;
            DataColumn pDataColumn = GetTableColumnByName(clssTable, "AR(%)");


            int clss_count = clssInput.clss.Count;
            List<string> zoneId = dataInput.zoneValue;
            IFeature pFeature = null;
            List<List<double>> zoneResult = new List<List<double>>();
           
            for (int i = 0; i < zoneId.Count; i++)
            {
                List<double> result = new List<double>();//每一类的面积
                for (int j = 0; j < clss_count; j++) result.Add(0.0);
                double zonearea = dataInput.zoneArea[i];//每一个分区的面积
                string zid = zoneId[i];

                int[] oidlist=null;// dataInput.GetZonePids(i); ;
                    using (ComReleaser comReleaser = new ComReleaser())
                    {

                        IGeoDatabaseBridge geodatabaseBridge = new GeoDatabaseHelperClass();
                        IFeatureCursor featureCursor = geodatabaseBridge.GetFeatures(dataInput.zoneLCA_FC,
                            ref oidlist, true);
                        while ((pFeature = featureCursor.NextFeature()) != null)
                        {
                            double temparea = (double)pFeature.get_Value(dataInput.areaIndex);
                            for (int j = 0; j < clss_count; j++)
                            {
                                string code = pFeature.get_Value(dataInput.codeIndex).ToString();
                                if (code == clssInput.clss[j])
                                {
                                    result[j] += temparea;
                                }

                            }
                        }
                        for (int j = 0; j < clss_count; j++)
                        {

                            result[j] /= zonearea;
                        }


                    }
                    zoneResult.Add(result);

            }




        }
        //public static void TotalArea(DataInputInfo dataInput, ClassParamInfo clssInput)
        //{


        //    //添加面积字段
        //    DataTable clssTable = clssInput.resultTable;
        //    DataColumn pDataColumn = GetTableColumnByName(clssTable, "TA(m2)");

        //    IFeatureClass pFeatureClass = dataInput.layerDataCover;

        //    //获取分区
        //    IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
        //    IDataStatistics pDataStatistics = new DataStatisticsClass();
        //    pDataStatistics.Field = dataInput.zoneField;
        //    pDataStatistics.Cursor = pFeatureCursor as ICursor;
        //    IEnumerator pEnumerator = pDataStatistics.UniqueValues;
        //    pEnumerator.Reset();

        //    DataRow pDataRow = null;

        //    while (pEnumerator.MoveNext())
        //    {
        //        object obj = pEnumerator.Current;
        //        for (int i = 0; i < clssInput.clss.Count; i++)
        //        {
        //            //FID_renkou_slq2 = 0 AND CC = '0110'
        //            //dataInput.zoneField + " = \'" + obj + "\' AND "
        //            IQueryFilter pQueryFilter = new QueryFilterClass();
        //            // pQueryFilter.SubFields = paramInfo.zoneID + ",CC,Shape_Area";
        //            pQueryFilter.WhereClause = dataInput.zoneField + " = \'" + obj + "\' AND " + dataInput.codeField + " = " + "\'" + clssInput.clss[i] + "\'";
        //            IFeatureCursor pFeatureCursor1 = pFeatureClass.Search(pQueryFilter, true);

        //            //计算总面积
        //            IDataStatistics pStatistics = new DataStatisticsClass();
        //            pStatistics.Field = "Shape_Area";
        //            pStatistics.Cursor = pFeatureCursor1 as ICursor;
        //            IStatisticsResults pStatisticsResult = pStatistics.Statistics;

        //            //确定更新还是添加
        //            string express = "ZID=" + obj + " AND Type=" + "\'" + MainForm.pData[clssInput.clss[i].ToString()] + "\'";
        //            if (clssInput.resultTable.Select(express).Length > 0)
        //            {
        //                pDataRow = clssInput.resultTable.Select(express)[0];
        //            }

        //            if (pDataRow == null)
        //            {
        //                pDataRow = clssInput.resultTable.NewRow();
        //                pDataRow[1] = obj;
        //                pDataRow[2] = MainForm.pData[clssInput.clss[i].ToString()];
        //                pDataRow.SetField(pDataColumn, pStatisticsResult.Sum);
        //                clssInput.resultTable.Rows.Add(pDataRow);
        //                pDataRow = null;
        //            }
        //            else
        //            {
        //                pDataRow.SetField(pDataColumn, pStatisticsResult.Sum);
        //            }

        //        }


        //    }
        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatistics);
           

        //}
        /// <summary>
        /// 面积占比
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="clssInput"></param>
        /// 
        //public static void AreaRatio(DataInputInfo dataInput, ClassParamInfo clssInput)
        //{
        //    DataTable  clssTable = clssInput.resultTable;
        //    DataColumn pDataColumn = GetTableColumnByName(clssTable, "AR(%)");


        //     int clss_count = clssInput.clss.Count;
        //     List<string>zoneId=dataInput.zoneValue;
        //     IFeature pFeature = null;
        //     DataRow pDataRow=null;
        //     List<double> result = new List<double>();//每一类的面积
        //     for (int i = 0; i < zoneId.Count;i++ )
        //     {
        //         double zonearea = 0.0;//每一个分区的面积
        //         result.Clear();
        //         for (int j = 0; j < clss_count; j++) result.Add(0.0);
        //         string zid = zoneId[i];
        //         IFeatureCursor pFeatureCursor = dataInput.zones[i];
        //         pFeature = pFeatureCursor.NextFeature();
        //         while (pFeature != null)
        //         {
                  
        //             double temparea = (double)pFeature.get_Value(dataInput.areaIndex);
        //             for (int j= 0; j < clss_count;j++ )
        //             {
        //                 string code = pFeature.get_Value(dataInput.codeIndex).ToString();
        //                 if (code==clssInput.clss[j])
        //                 {
        //                     result[j] += temparea;
        //                 }

        //             }
        //             zonearea += temparea;
        //             pFeature = pFeatureCursor.NextFeature();

        //         }

        //         for (int j = 0; j < clss_count; j++)
        //         {
                     
        //             //确定更新还是添加
        //             string express = "ZID=" + zid+ " AND Type=" + "\'" + MainForm.pData[clssInput.clss[j].ToString()] + "\'";
        //             if (clssInput.resultTable.Select(express).Length > 0)
        //             {
        //                 pDataRow = clssInput.resultTable.Select(express)[0];
        //             }

        //             if (pDataRow == null)
        //             {
        //                 pDataRow = clssInput.resultTable.NewRow();
        //                 pDataRow[1] = zid;
        //                 pDataRow[2] = MainForm.pData[clssInput.clss[j].ToString()];
        //                 pDataRow.SetField(pDataColumn, result[j] / zonearea*100);
        //                 clssInput.resultTable.Rows.Add(pDataRow);
        //                 pDataRow = null;
        //             }
        //             else
        //             {

        //                 pDataRow.SetField(pDataColumn, result[j] / zonearea*100);
        //             }

        //         }
                 
             
        //     }

             


        //}
        //public static void AreaRatio(DataInputInfo dataInput, ClassParamInfo clssInput)
        //{
        //    //添字段
        //    DataTable clssTable = clssInput.resultTable;
        //    DataColumn pDataColumn = GetTableColumnByName(clssTable, "AR(%)");



        //    IFeatureClass pFeatureClass = dataInput.layerDataCover;
        //    IFeatureClass pFeatureClassZone = dataInput.layerDataZone;
        //    int ZoneAreaIndex = pFeatureClassZone.Fields.FindField("Shape_Area");
        //    IQueryFilter pQueryFilterZone = new QueryFilterClass();
        //    double ZoneArea = 0.0;

        //    //获取分区
        //    IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
        //    IDataStatistics pDataStatistics = new DataStatisticsClass();
        //    pDataStatistics.Field = dataInput.zoneField;
        //    pDataStatistics.Cursor = pFeatureCursor as ICursor;
        //    IEnumerator pEnumerator = pDataStatistics.UniqueValues;
        //    pEnumerator.Reset();

        //    DataRow pDataRow = null;

        //    while (pEnumerator.MoveNext())
        //    {
        //        object obj = pEnumerator.Current;
        //        pQueryFilterZone.WhereClause = dataInput.zoneField + " = \'" + obj + "\'";
        //        IFeatureCursor pFeatureCursorZone = pFeatureClassZone.Search(pQueryFilterZone, true);
        //        try
        //        {
        //            ZoneArea = (double)pFeatureCursorZone.NextFeature().get_Value(ZoneAreaIndex);
        //        }
        //        catch (System.Exception ex)
        //        {
        //            string str = obj.ToString() + "查询分区失败！";
        //            MessageBox.Show(str);
        //        }

        //        for (int i = 0; i < clssInput.clss.Count; i++)
        //        {
        //            //FID_renkou_slq2 = 0 AND CC = '0110'
        //            IQueryFilter pQueryFilter = new QueryFilterClass();
        //            // pQueryFilter.SubFields = paramInfo.zoneID + ",CC,Shape_Area";
        //            pQueryFilter.WhereClause = dataInput.zoneField + " = \'" + obj + "\' AND " + dataInput.codeField + " = " + "\'" + clssInput.clss[i] + "\'";
        //            IFeatureCursor pFeatureCursor1 = pFeatureClass.Search(pQueryFilter, true);

        //            //计算总面积
        //            IDataStatistics pAreaStatistics = new DataStatisticsClass();
        //            pAreaStatistics.Field = "Shape_Area";
        //            pAreaStatistics.Cursor = pFeatureCursor1 as ICursor;
        //            IStatisticsResults pAreaStatisticsResult = pAreaStatistics.Statistics;
        //            //确定更新还是添加
        //            string express = "ZID=" + obj + " AND Type=" + "\'" + MainForm.pData[clssInput.clss[i].ToString()] + "\'";
        //            if (clssInput.resultTable.Select(express).Length > 0)
        //            {
        //                pDataRow = clssInput.resultTable.Select(express)[0];
        //            }

        //            if (pDataRow == null)
        //            {
        //                pDataRow = clssInput.resultTable.NewRow();
        //                pDataRow[1] = obj;
        //                pDataRow[2] = MainForm.pData[clssInput.clss[i].ToString()];
        //                pDataRow.SetField(pDataColumn, pAreaStatisticsResult.Sum /ZoneArea * 100);
        //                clssInput.resultTable.Rows.Add(pDataRow);
        //                pDataRow = null;
        //            }
        //            else
        //            {

        //                pDataRow.SetField(pDataColumn, pAreaStatisticsResult.Sum / ZoneArea * 100);
        //            }

        //        }

        //    }
        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatistics);
        //    //System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumerator);


        //}

        /// <summary>
        /// 所在分区，类别的最大值
        /// </summary>
        /// <param name="dataInput"></param>
        /// <param name="clssInput"></param>
        public static void AreaIndex(DataInputInfo dataInput, ClassParamInfo clssInput)
        {
            //添字段
            DataTable clssTable = clssInput.resultTable;
            DataColumn pDataColumn = GetTableColumnByName(clssTable, "LPI(%)");



            IFeatureClass pFeatureClass = dataInput.layerDataCover;
            IFeatureClass pFeatureClassZone = dataInput.layerDataZone;
            int ZoneAreaIndex = pFeatureClassZone.Fields.FindField("Shape_Area");
            IQueryFilter pQueryFilterZone = new QueryFilterClass();
            double ZoneArea = 0.0;
            //获取分区
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = dataInput.zoneField;
            pDataStatistics.Cursor = pFeatureCursor as ICursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();

            DataRow pDataRow = null;

            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;

                //计算总面积
                pQueryFilterZone.WhereClause = dataInput.zoneField + " = \'" + obj+"\'";
                IFeatureCursor pFeatureCursorZone = pFeatureClassZone.Search(pQueryFilterZone, true);
                try
                {
                    ZoneArea = (double)pFeatureCursorZone.NextFeature().get_Value(ZoneAreaIndex);
                }
                catch (System.Exception ex)
                {
                    string str = obj.ToString() + "查询分区失败！";
                    MessageBox.Show(str);
                }
               
                

                for (int i = 0; i < clssInput.clss.Count; i++)
                {
                    //FID_renkou_slq2 = 0 AND CC = '0110'
                    IQueryFilter pQueryFilter = new QueryFilterClass();
                    // pQueryFilter.SubFields = paramInfo.zoneID + ",CC,Shape_Area";
                    pQueryFilter.WhereClause = dataInput.zoneField + " = \'" + obj + "\' AND " + dataInput.codeField + " = " + "\'" + clssInput.clss[i] + "\'";
                    IFeatureCursor pFeatureCursor1 = pFeatureClass.Search(pQueryFilter, true);

                    //计算总面积
                    IDataStatistics pAreaStatistics = new DataStatisticsClass();
                    pAreaStatistics.Field = "Shape_Area";
                    pAreaStatistics.Cursor = pFeatureCursor1 as ICursor;
                    IStatisticsResults pAreaStatisticsResult = pAreaStatistics.Statistics;
                    //确定更新还是添加
                    string express = "ZID=" + obj + " AND Type=" + "\'" + MainForm.pData[clssInput.clss[i].ToString()] + "\'";
                    if (clssInput.resultTable.Select(express).Length > 0)
                    {
                        pDataRow = clssInput.resultTable.Select(express)[0];
                    }

                    if (pDataRow == null)
                    {
                        pDataRow = clssInput.resultTable.NewRow();
                        pDataRow[1] = obj;
                        pDataRow[2] = MainForm.pData[clssInput.clss[i].ToString()];
                        if (pAreaStatisticsResult.Count==0)
                        {
                            pDataRow.SetField(pDataColumn, 0);
                        }
                        else
                        {

                            pDataRow.SetField(pDataColumn, pAreaStatisticsResult.Maximum / ZoneArea * 100);
                        }
                       
                        clssInput.resultTable.Rows.Add(pDataRow);
                        pDataRow = null;
                    }
                    else
                    {

                        if (pAreaStatisticsResult.Count == 0)
                        {
                            pDataRow.SetField(pDataColumn, 0);
                        }
                        else
                        {

                            pDataRow.SetField(pDataColumn, pAreaStatisticsResult.Maximum / ZoneArea * 100);
                        }
                    }

                }

            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureCursor);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pDataStatistics);
             


        }


   
#endregion

        /// <summary>
        ///  创建数据集
        /// </summary>
        /// <param name="pLayer"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataSet CreateDataTable(string tableName)
        {
            DataSet pDataSet = new DataSet();
            
            //创建第一个DataTable表
            DataTable pDataTable = new DataTable();
            //创建必要的属性列
            DataColumn pDataColumn0 = new DataColumn();
            pDataColumn0.ColumnName = "OID";
            pDataColumn0.Unique = true;
            pDataColumn0.DataType = System.Type.GetType("System.Int32");
            pDataColumn0.AllowDBNull = false;
            pDataColumn0.AutoIncrement = true;
           
            pDataTable.Columns.Add(pDataColumn0);
            DataColumn pDataColumn1 = new DataColumn();
            pDataColumn1.ColumnName = "ZID";
            pDataColumn1.DataType = System.Type.GetType("System.Int32");
            pDataColumn1.AllowDBNull = false;
            pDataTable.Columns.Add(pDataColumn1);

            DataColumn pDataColumn2 = new DataColumn();
            pDataColumn2.ColumnName = "Type";
            pDataColumn2.DataType = System.Type.GetType("System.String");
            pDataColumn2.MaxLength = 20;
            pDataColumn2.AllowDBNull = false;
            pDataTable.Columns.Add(pDataColumn2);
            pDataSet.Tables.Add(pDataTable);

            //创建第二个DataTable表

            DataTable pDataTable1 = new DataTable();
            //创建必要的属性列
            DataColumn pDataColum1 = new DataColumn();
            pDataColum1.ColumnName = "OID";
            pDataColum1.Unique = true;
            pDataColum1.DataType = System.Type.GetType("System.Int32");
            pDataColum1.AllowDBNull = false;
            pDataColum1.AutoIncrement = true;

            pDataTable1.Columns.Add(pDataColum1);
            DataColumn pDataColum2 = new DataColumn();
            pDataColum2.ColumnName = "ZID";
            pDataColum2.DataType = System.Type.GetType("System.Int32");
            pDataColum2.AllowDBNull = false;
            pDataTable1.Columns.Add(pDataColum2);
            pDataSet.Tables.Add(pDataTable1);

            return pDataSet;
        }
        /// <summary>
        /// 通过已知要素类创建要素
        /// </summary>
        /// <param name="input"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IFeatureClass CreateFeatureClass(IFeatureClass input,string path)
        {

            string dir=System.IO.Path.GetDirectoryName(path);
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            IWorkspace pWorkspace = ArcGISUtilities.GetShapefileWorkspace(dir);
            IDataset pDataSet = input as IDataset;
            IFeatureClass pFeatureClass=pDataSet.Copy(name, pWorkspace) as IFeatureClass;
//             //删除不必要的字段
//             IFields pFields = pFeatureClass.Fields;
//             
//             for (int i = 2; i < pFields.FieldCount;i++ )
//             {
// //                 if (pFields.Field[i].Name=="FID"||pFields.Field[i].Name=="Shape")
// //                 {
// //                    continue;
// //                 }
//                 pFeatureClass.DeleteField(pFields.Field[i]);
//             }
            return pFeatureClass;
        }

        /// <summary>
        /// 获取数据表的列
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static DataColumn GetTableColumnByName(DataTable dt, string columnName)
        {
            DataColumn dc = null;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (columnName == dt.Columns[i].ColumnName)
                {
                    dc = dt.Columns[i];
                    break;
                }
            }
            return dc;
        }

        /// <summary>
        /// 导出文本文件
        /// </summary>
        /// <param name="table"></param>
        public static void DataTableToTxt(DataTable dt)
        {

            SaveFileDialog sfg = new SaveFileDialog();

            sfg.Filter = "CSV文件|*.CSV";
            if (sfg.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else
            {
               
                SaveCSV(dt, sfg.FileName);
                MessageBox.Show("文件保存成功！");
            }
        }
        /// <summary>
        /// 导出文本文件
        /// </summary>
        /// <param name="table"></param>
        public static void DataTableToTxt(DataSet ds)
        {

            SaveFileDialog sfg = new SaveFileDialog();

            sfg.Filter = "所有文件|*.*";
            if (sfg.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(sfg.FileName);
                string filename0 = System.IO.Path.GetDirectoryName(sfg.FileName)+"\\"+fileName + ".class";
                string filename1 = System.IO.Path.GetDirectoryName(sfg.FileName) + "\\" + fileName + ".land";
                SaveCSV(ds.Tables[0], filename0);
                SaveCSV(ds.Tables[1], filename1);
                MessageBox.Show("文件保存成功！");
            }
        }
        public static DataSet TxtToDataTabe( )
        {
            DataSet ds = new DataSet();
             
            OpenFileDialog ofg = new OpenFileDialog();
            ofg.Filter = "所有文件|*.*";
            if (ofg.ShowDialog()==DialogResult.OK)
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(ofg.FileName);
                string filename0 = System.IO.Path.GetDirectoryName(ofg.FileName) + "\\" + fileName + ".class";
                string filename1 = System.IO.Path.GetDirectoryName(ofg.FileName) + "\\" + fileName + ".land";
                DataTable dt0=OpenCSV(filename0);
                DataTable dt1 = OpenCSV(filename1);
                ds.Tables.Add(dt0);
                ds.Tables.Add(dt1);
            }
            return ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="fileName"></param>
        public static void SaveCSV(DataTable dt, string fileName)
        {
             System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
            string data = "";
            //写出列名称
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                data += dt.Columns[i].ColumnName.ToString();
                if (i < dt.Columns.Count - 1)
                {
                    data += ",";
                }
            }
            sw.WriteLine(data);
            //写出各行数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                data = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    data += dt.Rows[i][j].ToString();
                    if (j < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
            }
            sw.Close();
            fs.Close();
           
        }
        public static DataTable OpenCSV(string fileName)
        {
            DataTable dt = new DataTable();
            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine;
            //标示列数
            int columnCount = 0;
            //标示是否是读取的第一行
            bool IsFirst = true;
            //逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                aryLine = strLine.Split(',');
                if (IsFirst == true)
                {
                    IsFirst = false;
                    columnCount = aryLine.Length;
                    //创建列
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn dc = new DataColumn(aryLine[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
                }
            }
            sr.Close();
            fs.Close();
            return dt;
        }
        /// <summary>
        /// 获取表中的某一字段的唯一值
        /// </summary>
        /// <param name="dtable"></param>
        /// <param name="colName"></param>
        /// <returns></returns>
        public  static object [] GetDistinctValues(DataTable dtable, string  colName)
     {
         Hashtable hTable = new Hashtable();
         foreach (DataRow drow in dtable.Rows)
         {
             try
             {
                 hTable.Add(drow[colName], string.Empty);
             }
             catch { }
         }
         object[] objArray = new object[hTable.Keys.Count];
         hTable.Keys.CopyTo(objArray, 0);
         return objArray;
     }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static object[] GetDistinctValues(List<string>list)
     {
         Hashtable hTable = new Hashtable();
         foreach (string str in list)
         {
             try
             {
                 hTable.Add(str, string.Empty);
             }
             catch { }
         }
         object[] objArray = new object[hTable.Keys.Count];
         hTable.Keys.CopyTo(objArray, 0);
         return objArray;
     }
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="pWorkSpace"></param>
        /// <param name="clssInfo"></param>
        /// <param name="landInfo"></param>
        public static void CreateFeatureClass( ref IFeatureClass pFeatureClass,ClassParamInfo clssInfo, LandscapeParamInfo landInfo)
        {
             
            IField field = null;
            IFieldEdit fieldEdit = null;
            for (int i = pFeatureClass.Fields.FieldCount-1; i >=0;i--)
            {
                IField pField = pFeatureClass.Fields.get_Field(i);
                if (pField == pFeatureClass.AreaField) continue;
                if (pField == pFeatureClass.LengthField) continue;
                if (pField.Name == pFeatureClass.ShapeFieldName) continue;
                if (pField.Name == MainForm.dataInputInfo.zoneField) continue;
                if (pField.Name == pFeatureClass.OIDFieldName) continue;
                pFeatureClass.DeleteField(pField);
            }
            
            
            try
            {
                //类别指标
                for (int i = 0; i < clssInfo.clss.Count; i++)
                {
                    for (int j = 0; j < clssInfo.clssIndex.Count; j++)
                    {

                        string fieldName = "";
                        switch (clssInfo.clssIndex[j])
                        {

                            case ClassIndex.AreaRatio:
                                fieldName = "C_" + clssInfo.clss[i] + "_AR";
                                break;
                            case ClassIndex.EdgeDensity:
                                fieldName = "C_" + clssInfo.clss[i] + "_ED"  + "_m_hectare";
                                break;
                            case ClassIndex.MaxAreaIndex:
                                fieldName = "C_" + clssInfo.clss[i] + "_LPI";
                                break;
                            case ClassIndex.MeshIndex:
                                fieldName = "C_" + clssInfo.clss[i] + "_MI"+"_km2";
                                break;
                            case ClassIndex.TotalArea:
                                fieldName = "C_" + clssInfo.clss[i] + "_TA" +"_m2";
                                break;
                            case ClassIndex.TotalEgde:
                                fieldName = "C_" + clssInfo.clss[i] + "_TE"+"_m";
                                break;
                            case ClassIndex.UMeshIndex:
                                fieldName = "C_" + clssInfo.clss[i] + "_UMI" +"_km2";
                                break;
                            case ClassIndex.MMeshIndex:
                                fieldName = "C_" + clssInfo.clss[i] + "_MMI" +"_km2";
                                break;
                            case ClassIndex.PAFRACIndex:
                                fieldName = "C_" + clssInfo.clss[i] + "_PAFRACI";
                                break;
                            case ClassIndex.CWEDIndex:
                                fieldName = "C_" + clssInfo.clss[i] + "_CWED_m_hectare";
                                break;
                            case ClassIndex.TECIIndex:
                                fieldName = "C_" + clssInfo.clss[i] + "_TECI";
                                break;

                        }
                        
                        field = new FieldClass();
                        fieldEdit = field as IFieldEdit;
                        fieldEdit.Name_2 = fieldName;
                        fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                        pFeatureClass.AddField(field);

                    }
                }

                //景观指标
                for (int i = 0; i < landInfo.landIndex.Count; i++)
                {

                    string fieldName = "";
                    switch (landInfo.landIndex[i])
                    {

                        case LandscapeIndex.EdgeDensity:
                            fieldName = "L_ED_m_hectare";
                            break;
                        case LandscapeIndex.MaxAreaIndex:
                            fieldName = "L_LPI";
                            break;
                        case LandscapeIndex.PAFRACIndex:
                            fieldName = "L_PAFRACI";
                            break;
                        case LandscapeIndex.TotalArea:
                            fieldName = "L_TA_m2";
                            break;
                        case LandscapeIndex.TotalEgde:
                            fieldName = "L_TE_m";
                            break;
                        case LandscapeIndex.UMeshIndex:
                            fieldName = "L_UMI" + "_km2";
                            break;
                        case LandscapeIndex.MMeshIndex:
                            fieldName = "L_MMI" + "_km2";
                            break;
                        case LandscapeIndex.MeshIndex:
                            fieldName =  "L_MI" + "_km2";
                            break;
                        case LandscapeIndex.CWEDIndex:
                            fieldName = "L_CWED_m_hectare";
                            break;
                        case LandscapeIndex.TECIIndex:
                            fieldName = "L_TECI";
                            break;
                    }
                    
                    field = new FieldClass();
                    fieldEdit = field as IFieldEdit;
                    fieldEdit.Name_2 = fieldName;
                    fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                    pFeatureClass.AddField(field);


                }

                //生境指标
                if (MainForm.dt_EcosystemIndex!=null)
                {

                    for (int i = 1; i < MainForm.dt_EcosystemIndex.Columns.Count; i++)
                    {
                        DataColumn dc = MainForm.dt_EcosystemIndex.Columns[i];
                        string fieldName = dc.ColumnName;
                        field = new FieldClass();
                        fieldEdit = field as IFieldEdit;
                        fieldEdit.Name_2 = fieldName;
                        fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                        pFeatureClass.AddField(field);
                    }
                }
              

                //自定义指标
                if (MainForm.dt_LandFrm!=null)
                {

                    for (int i = 1; i < MainForm.dt_LandFrm.Columns.Count; i++)
                    {
                        DataColumn dc = MainForm.dt_LandFrm.Columns[i];
                        string fieldName = dc.ColumnName;
                        field = new FieldClass();
                        fieldEdit = field as IFieldEdit;
                        fieldEdit.Name_2 = fieldName;
                        fieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                        pFeatureClass.AddField(field);
                    }

                }
               
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("创建数据表失败！");
                return;
            }
           
             
        }
    }
}
