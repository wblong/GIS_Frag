using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;
using System.Windows.Forms;
using AE_Environment.Forms;

namespace AE_Environment.Utilities
{
    class CalculatContourTask:ESRI.ArcGIS.Controls.IEngineEditTask
    {
        IEngineEditor pEngineEditor;
        IEngineEditSketch pEditSketch;
        IEngineEditLayers pEditLayer;

        IFeatureLayer pFeatureLayer;

        private double pHeight;
        private double pInterval;
        private string pHeightName;


        public void Activate(IEngineEditor editor, IEngineEditTask oldTask)
        {
            //throw new NotImplementedException();

            if (editor==null)
            {
                return;
            }
            pEngineEditor = editor;
            pEditSketch = editor as IEngineEditSketch;
            pEditSketch.GeometryType = ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline;

            pEditLayer = pEditSketch as IEngineEditLayers;
            //

            ((IEngineEditEvents_Event)pEditSketch).OnTargetLayerChanged += new IEngineEditEvents_OnTargetLayerChangedEventHandler(OnTargetLayerChanged);

            ((IEngineEditEvents_Event)pEditSketch).OnCurrentTaskChanged+=new IEngineEditEvents_OnCurrentTaskChangedEventHandler(OnCurrentTaskChanged);
        }
        public void OnTargetLayerChanged() {
            PerformSketchToolEnabledChecks();
        }
        public void OnCurrentTaskChanged() {
          if (pEngineEditor.CurrentTask.Name=="CalculateContourTask")
          {
              PerformSketchToolEnabledChecks();
          }
        }
        public void Deactivate()
        {
           // throw new NotImplementedException();
            pEditSketch.RefreshSketch();
            ((IEngineEditEvents_Event)pEditSketch).OnTargetLayerChanged -= new IEngineEditEvents_OnTargetLayerChangedEventHandler(OnTargetLayerChanged);

            ((IEngineEditEvents_Event)pEditSketch).OnCurrentTaskChanged -= new IEngineEditEvents_OnCurrentTaskChangedEventHandler(OnCurrentTaskChanged);
            pEngineEditor = null;
            pEditSketch = null;
            pEditLayer = null;

        }

        public string GroupName
        {
            get { //throw new NotImplementedException(); 
                return "Modify Tasks";
            }
        }

        public string Name
        {
            get { //throw new NotImplementedException(); 
                return "CalculateContourTask";
            }
        }

        public void OnDeleteSketch()
        {
            //throw new NotImplementedException();
        }

        public void OnFinishSketch()
        {
           // throw new NotImplementedException();
            pFeatureLayer = pEditLayer.TargetLayer as IFeatureLayer;
            IGeometry pPolyline = pEditSketch.Geometry;
            if (pPolyline.IsEmpty==false)
            {
                ParaSetting pFormSetting = new ParaSetting(pFeatureLayer.FeatureClass);
                pFormSetting.ShowDialog();
                if (pFormSetting.DialogResult==DialogResult.OK)
                {
                    pHeightName = pFormSetting.pFieldName;
                    pHeight = Convert.ToDouble(pFormSetting.Height);
                    pInterval = Convert.ToDouble(pFormSetting.Interval);
                    pFormSetting.Dispose();
                    pFormSetting = null;
                    IFeatureCursor pFeatureCusor = GetFeatureCursor(pPolyline, pFeatureLayer.FeatureClass);
                    CalculateIntersect(pFeatureCusor, pPolyline);
                    
                    MessageBox.Show("计算完毕");
                }
            }
            IActiveView pActiveView = pEngineEditor.Map as IActiveView;
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, (object)pFeatureLayer, pActiveView.Extent);

        }

        public string UniqueName
        {
            get { return "CalculateContourTask"; }
        }
        private void CalculateIntersect(IFeatureCursor pFeatureCursor, IGeometry pGeometry)
        {

            if (pFeatureCursor==null)
            {
                return;
            }
            IMultipoint pIntersectionPoints = null;
            IPointCollection pPointColl = null;
            List<IFeature> pFeatureList = new List<IFeature>();
            ITopologicalOperator pTopoOperator = pGeometry as ITopologicalOperator;
            //画线的几何体
            IPointCollection pSkecthPiontColl = pGeometry as IPointCollection;
            IPoint pPoint0 = pSkecthPiontColl.get_Point(0);
            IFeature pFeature = pFeatureCursor.NextFeature();
            pFeatureList.Clear();
            while ((pFeature != null))
            {

                pFeatureList.Add(pFeature);
                pFeature = pFeatureCursor.NextFeature();

            }
            IPolyline pPolyline = pGeometry as IPolyline;
            IPoint pPointF = pPolyline.FromPoint;
            Dictionary<double, IFeature> pDic = new Dictionary<double, IFeature>();
            int pCount = pFeatureList.Count;
            double[] sortArray = new double[pCount];
            for (int i = 0; i <= pCount;i++ )
            {
                try
                {
                    pFeature = pFeatureList[i];
                    pIntersectionPoints = pTopoOperator.Intersect(pFeature.Shape, esriGeometryDimension.esriGeometry0Dimension) as IMultipoint;
                    pPointColl = pIntersectionPoints as IPointCollection;
                    sortArray[i] = GetDistance(pPointF, pPointColl.get_Point(0));
                    pDic.Add(GetDistance(pPointF, pPointColl.get_Point(0)), pFeatureList[i]);
                    pFeature = pFeatureCursor.NextFeature();


                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            for (int H = sortArray.Length - 1; H >= 0;H-- )
            {

                for (int j = 0; j < H;j++ )
                {
                    if (sortArray[j]>sortArray[j+1])
                    {
                        double temp = sortArray[j];
                        sortArray[j] = sortArray[j + 1];
                        sortArray[j + 1] = temp;

                    }
                }
            }
            int pFieldIndex = pFeatureLayer.FeatureClass.Fields.FindField(pHeightName);
            Dictionary<IFeature, double> pDicdis = new Dictionary<IFeature, double>();
            for (int m = 0; m < sortArray.Length; m++)
            {
                foreach (KeyValuePair<double, IFeature> pKey in pDic)
                {

                    if (sortArray[m]==pKey.Key)
                    {
                        IFeature pFeatureH = pKey.Value;
                        pFeatureH.set_Value(pFieldIndex, pHeight + pInterval * m);
                        pFeatureH.Store();

                    }
                }
            }
        }
        /// <summary>
        /// 空间查询
        /// 
        /// </summary>
        /// <param name="pGeometry"></param>
        /// <param name="pFeatureClass"></param>
        /// <returns></returns>
        private IFeatureCursor GetFeatureCursor(IGeometry pGeometry, IFeatureClass pFeatureClass)
        {
            ISpatialFilter pSpatialFilter = new SpatialFilterClass();
            pSpatialFilter.Geometry = pGeometry;
            pSpatialFilter.GeometryField = pFeatureClass.ShapeFieldName;
            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIndexIntersects;
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(pSpatialFilter, false);
            return pFeatureCursor;

        }
        /// <summary>
        /// 等高线之间的距离
        /// </summary>
        /// <param name="pPoint1"></param>
        /// <param name="pPoint2"></param>
        /// <returns></returns>
        private double GetDistance(IPoint pPoint1, IPoint pPoint2)
        {

            IProximityOperator pProximity = pPoint1 as IProximityOperator;
            return pProximity.ReturnDistance(pPoint2);
        }
        /// <summary>
        /// 执行绘制工具检查
        /// </summary>
        private void PerformSketchToolEnabledChecks()
        {


            if (pEditLayer==null)
            {
                return;

            }
            if (pEditLayer.TargetLayer.FeatureClass.ShapeType!=esriGeometryType.esriGeometryPolyline)
            {
                pEditSketch.GeometryType = esriGeometryType.esriGeometryNull;
                return;
            }
            pEditSketch.GeometryType = esriGeometryType.esriGeometryPolyline;

        }
    }
}
