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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IFeatureClass pStats = (MapLayer[combo_Stats.SelectedIndex] as IFeatureLayer).FeatureClass;
            IFeatureClass pFrag = (MapLayer[combo_Frag.SelectedIndex] as IFeatureLayer).FeatureClass;
            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = pStats.FeatureCount(null);

            int fieldindex = pStats.FindField("meff");
            if (fieldindex<0)
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

            this.Close();


        }
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
                area =( pGeometry as IArea).Area;
                 
                pGeometry = IntersectGeo(_pFeature.Shape as ITopologicalOperator, pGeometry);
                areaIntersect = (pGeometry as IArea).Area;
                result += area * areaIntersect;
                pFeature = pFeatureCursor.NextFeature();
            }
            return result;

        }
        private IGeometry IntersectGeo(ITopologicalOperator _pFeature, IGeometry _pOther) 
        
        {
            IGeometry pOutGeometry=null;
            pOutGeometry = _pFeature.Intersect(_pOther, esriGeometryDimension.esriGeometry2Dimension);
            return pOutGeometry;
        }
        
    }
}
