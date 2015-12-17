using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;

namespace AE_Environment.Model.FunctionIndexes
{
    class LMMeshIndex:InterfaceLandIndex
    {
        public string name = "MEffectMeshArea";
        public string unit = "km2";
        private IFeatureClass pFeatureClass = null;
        public LMMeshIndex(IFeatureClass pFeatureClass)
        {

            this.pFeatureClass = pFeatureClass;

        }
        string InterfaceLandIndex.Name()
        {
            return name;
        }

        string InterfaceLandIndex.Unit()
        {
            return unit;
        }

        double InterfaceLandIndex.CaculateLandIndex(IFeatureCursor pFeatureCursor, BaseData basedata)
        {
            double result = 0.0;
            IFeature pFeature = null;
            ISpatialFilter spatialFilter;
            IFeatureCursor pSpatialFCursor;
            IFeature pSpatialFeature;
            int indexArea = pFeatureClass.FindField("Shape_Area");       
            while ((pFeature = pFeatureCursor.NextFeature()) != null)
            {
                double temparea = (double)pFeature.get_Value(basedata.areaIndex);
                spatialFilter = new SpatialFilterClass();
                spatialFilter.Geometry = pFeature.Shape;
                spatialFilter.GeometryField = pFeatureClass.ShapeFieldName;

                spatialFilter.SpatialRelDescription = "T********";
                spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelRelation;
                pSpatialFCursor = pFeatureClass.Search(spatialFilter, false);
                pSpatialFeature = pSpatialFCursor.NextFeature();
                double computeArea = 0;
                //待修改
                 
                while (pSpatialFeature != null)
                {

                   
                    computeArea += (double)pSpatialFeature.get_Value(indexArea);
                    pSpatialFeature = pSpatialFCursor.NextFeature();


                }
               
                result+=(double)pFeature.get_Value(basedata.areaIndex) * computeArea;
                
            }

            return result;
        }
    }
}
