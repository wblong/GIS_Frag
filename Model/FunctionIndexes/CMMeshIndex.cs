using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace AE_Environment.Model.FunctionIndexes
{
    class CMMeshIndex : InterfaceClassIndex
    {

        public string name = "MEffectMeshArea";
        public string unit = "km2";
        private IFeatureClass pFeatureClass = null;
        private List<string> classvalue = null;

        public CMMeshIndex(List<string> clssValue, IFeatureClass pFeatureClass)
        {
            this.classvalue = clssValue;
            this.pFeatureClass = pFeatureClass;

        }

        public string Name()
        {
            return name;
        }

        public string Unit()
        {
            return unit;
        }

        public List<double> CaculateClassIndex(ESRI.ArcGIS.Geodatabase.IFeatureCursor pFeatureCursor, BaseData basedata)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < classvalue.Count; i++) { result.Add(0.0); }
            IFeature pFeature = null;

            double totalarea = 0.0;
            while ((pFeature = pFeatureCursor.NextFeature()) != null)
            {

                double temparea = (double)pFeature.get_Value(basedata.areaIndex);
                for (int j = 0; j < classvalue.Count; j++)//分类
                {
                    string code = pFeature.get_Value(basedata.codeIndex).ToString();
                    if (code == classvalue[j])
                    {
                        double computeArea = 0.0;
                        ISpatialFilter spatialFilter = new SpatialFilterClass();
                        spatialFilter.Geometry = pFeature.Shape;
                        spatialFilter.GeometryField = pFeatureClass.ShapeFieldName;
                        spatialFilter.SubFields = "Shape_Area";
                        spatialFilter.SpatialRelDescription = "T********";
                        spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelRelation;
                        IFeatureCursor pSpatialFCursor = pFeatureClass.Search(spatialFilter, false);
                        IFeature pSpatialFeature = pSpatialFCursor.NextFeature();
                        while (pSpatialFeature != null)
                        {

                            computeArea += (double)pSpatialFeature.get_Value(0);
                            pSpatialFeature = pSpatialFCursor.NextFeature();


                        }
                        result[j] += (double)pFeature.get_Value(basedata.areaIndex) * computeArea;

                    }

                }
                totalarea += temparea;
            }
            for (int i = 0; i < result.Count; i++)
            {
                result[i] /= totalarea;
            }
            return result;

        }
    }
}
