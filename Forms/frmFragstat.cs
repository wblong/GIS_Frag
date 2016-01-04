using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace AE_Environment.Forms
{
    public partial class frmFragstat : Form
    {

        private IMapControl3 m_axMapControl;
        private Dictionary<int, ILayer> MapLayer = null;
        /// <summary>
        /// 
        /// </summary>
        public frmFragstat()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="axMapControl"></param>
        public frmFragstat(IMapControl3 axMapControl)
        {
            InitializeComponent();
            this.m_axMapControl = axMapControl;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmFragstat_Load(object sender, EventArgs e)
        {
            this.MapLayer = new Dictionary<int, ILayer>();
            for (int i = 0; i < this.m_axMapControl.LayerCount; i++)
            {
                ILayer layer = this.m_axMapControl.get_Layer(i);
                IFeatureLayer layer2 = layer as IFeatureLayer;
                if ((((layer != null) && layer.Valid) && (layer is IFeatureLayer)))
                {
                    this.combo_Frag.Items.Add(layer.Name);
                    this.combo_Stats.Items.Add(layer.Name);
                    this.combo_LCA.Items.Add(layer.Name);

                    this.MapLayer.Add(this.combo_Frag.Items.Count - 1, layer);
                }
            }
            if (this.combo_Frag.Items.Count > 0)
            {
                this.combo_Frag.SelectedIndex = 0;
            }
            if (this.combo_Stats.Items.Count>0)
            {
                this.combo_Stats.SelectedIndex = 0;
            }
            if (this.combo_LCA.Items.Count>0)
            {
                this.combo_LCA.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IFeatureClass pStats = (MapLayer[combo_Stats.SelectedIndex] as IFeatureLayer).FeatureClass;
            IFeatureClass pFrag = (MapLayer[combo_Frag.SelectedIndex] as IFeatureLayer).FeatureClass;


            IFeatureClass pLCA = (MapLayer[combo_LCA.SelectedIndex] as IFeatureLayer).FeatureClass;
            string code = combo_CodeField.Text;
             


            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = pStats.FeatureCount(null);
            //计算整体景观
            if (!checkBox1.Checked)
            {
                int fieldindex = pStats.FindField("meff");
                if (fieldindex < 0)
                {
                    IField pField = new FieldClass();
                    IFieldEdit pFieldEdit = pField as IFieldEdit;
                    pFieldEdit.Name_2 = "meff";
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                    pStats.AddField(pField);

                }
                IFeatureCursor pFeatureCursor = pStats.Search(null, true);
                IFeature pFeature = pFeatureCursor.NextFeature();
                while (pFeature != null)
                {
                    double result = sumArea(pFeature, pFrag);
                    double area = (pFeature.Shape as IArea).Area;
                    result = result / area;
                    pFeature.set_Value(fieldindex, result / 1000000);
                    pFeature.Store();
                    pFeature = pFeatureCursor.NextFeature();
                    this.progressBar1.Value++;
                }

            }
                //计算类别景观
            else
            {
                string temp = "class_meff";
                int fieldindex = pStats.FindField(temp);
                if (fieldindex < 0)
                {
                    IField pField = new FieldClass();
                    IFieldEdit pFieldEdit = pField as IFieldEdit;
                    pFieldEdit.Name_2 = temp;
                    pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                    pStats.AddField(pField);

                }
                 fieldindex = pStats.FindField(temp);
                IFeatureCursor pFeatureCursor = pStats.Search(null, true);
                IFeature pFeature = pFeatureCursor.NextFeature();
                while (pFeature != null)
                {
                    double result = 0.0;

                    IGeometry pGeometry = pFeature.Shape;
                    IArea pArea = pGeometry as IArea;
                    result = sumArea(pFeature, pFrag, code, pLCA) / pArea.Area;
                    pFeature.set_Value(fieldindex, result / 1000000);
                    pFeature.Store();
                    pFeature = pFeatureCursor.NextFeature();
                    this.progressBar1.Value++;
                }

            }

            MessageBox.Show("计算完毕!");
            this.Close();


        }

        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) groupBox1.Enabled = true;
            else groupBox1.Enabled = false;
        }

        private void combo_LCA_SelectedIndexChanged(object sender, EventArgs e)
        {
            
               IFeatureClass pFeatureClass = (MapLayer[combo_LCA.SelectedIndex] as IFeatureLayer).FeatureClass;
               IFields pFields = pFeatureClass.Fields;
               for (int i = 0; i < pFields.FieldCount; i++)
               {
                   combo_CodeField.Items.Add(pFields.get_Field(i).Name);

               }
               if (combo_CodeField.Items.Count > 0)
               {
                   combo_CodeField.SelectedIndex = 0;
               }
            
            
        }

       



        #region 整体景观函数计算
        /// <summary>
        /// 计算与统计单元相交要素的面积和
        /// </summary>
        /// <param name="_pFeature"></param>
        /// <param name="_pFeatureClass"></param>
        /// <returns></returns>
        private double sumArea(IFeature _pFeature, IFeatureClass _pFeatureClass)
        {
            double result = 0.0;
            //选择与pFeature相交的所有斑块


            IFeature pFeature;
            IGeometry pGeometry;
            double area = 0.0;
            double areaIntersect = 0.0;


            IFeatureCursor pFeatureCursor = _pFeatureClass.Search(null, true);
            pFeature = pFeatureCursor.NextFeature();
            while (pFeature != null)
            {

                pGeometry = pFeature.Shape;
                area = (pGeometry as IArea).Area;

                pGeometry = IntersectGeo(_pFeature.Shape as ITopologicalOperator, pGeometry);
                areaIntersect = (pGeometry as IArea).Area;
                result += area * areaIntersect;
                pFeature = pFeatureCursor.NextFeature();
            }
            return result;

        }
        /// <summary>
        /// 两个几何取相交的部分
        /// </summary>
        /// <param name="_pFeature"></param>
        /// <param name="_pOther"></param>
        /// <returns></returns>
        private IGeometry IntersectGeo(ITopologicalOperator _pFeature, IGeometry _pOther)
        {
            IGeometry pOutGeometry = null;
            pOutGeometry = _pFeature.Intersect(_pOther, esriGeometryDimension.esriGeometry2Dimension);
            return pOutGeometry;
        }

        #endregion
        #region 类别破碎化计算
        private double sumArea(IFeature _pFeature, IFeatureClass _pFeatureClass,string zoneValue,IFeatureClass _pFeatureClass1)
        {
            double result = 0.0;
            
//             IGeometry pGeometry;
//             IFeatureCursor pFeatureCursor = _pFeatureClass.Search(null, true);
//             IFeature  pFeature = pFeatureCursor.NextFeature();
//             while (pFeature != null)
//             {
// 
//                 pGeometry = pFeature.Shape;
//                 double area = (pGeometry as IArea).Area;
//                 pGeometry = IntersectGeo(_pFeature.Shape as ITopologicalOperator, pGeometry);
//                 //result += area * sumArea(codeValue, codeField, _pFeatureClass1,pGeometry);
//                 pFeature = pFeatureCursor.NextFeature();
//             }
            IGeometry pGeometry;
            ISpatialFilter spatialFilter = new SpatialFilterClass();


            spatialFilter.Geometry = _pFeature.Shape;
            spatialFilter.GeometryField = _pFeatureClass.ShapeFieldName;

            spatialFilter.SpatialRelDescription = "T********";
            spatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelRelation;
            IFeatureCursor pCursor = _pFeatureClass.Search(spatialFilter, true);
            IFeature pFeature = pCursor.NextFeature();
            while (pFeature != null)
            {
                pGeometry = pFeature.Shape;
                double area = (pGeometry as IArea).Area;

                IQueryFilter pQueryFilter = new QueryFilterClass();
                pQueryFilter.WhereClause = zoneValue + " = " + pFeature.get_Value(0);
                IFeatureCursor pFeatureCursor1 = _pFeatureClass1.Search(pQueryFilter, true);
                IDataStatistics pAreaStatistics = new DataStatisticsClass();
                pAreaStatistics.Field = "Shape_Area";
                pAreaStatistics.Cursor = pFeatureCursor1 as ICursor;
                IStatisticsResults pStatistics = pAreaStatistics.Statistics;
                result += area*pStatistics.Sum;

                pFeature = pCursor.NextFeature();
            }
            return result;


        }
        private double sumArea(string codeValue,string codeField, IFeatureClass _pFeatureClass, IGeometry _pGeometry)
        {
            double result = 0.0;

//             IQueryFilter pQueryFilter = new QueryFilterClass();
//             IFeature pFeature;
//             IFeatureCursor pCursor;
//             pQueryFilter.SubFields =codeField+","+_pFeatureClass.ShapeFieldName;
//             pQueryFilter.WhereClause=codeField+"="+"'"+codeValue+"'";
// 
//             pCursor = _pFeatureClass.Search(pQueryFilter, true);
//             pFeature = pCursor.NextFeature();
//             
//             while (pFeature != null)
//             {
// 
//                 IGeometry pGeometry = IntersectGeo(_pGeometry as ITopologicalOperator, pFeature.Shape);
//                 IArea pArea= pGeometry as IArea;
//                 result += pArea.Area;
//                 pFeature = pCursor.NextFeature();
// 
// 
//             }
           
            return result;


        }
        #endregion
    }
}
