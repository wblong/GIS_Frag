using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

namespace AE_Environment.Forms
{
    public partial class ParaSetting : Form
    {


        private IFeatureClass pFeatureClass = null;
        public string pFieldName
        {

            get
            {

                return pFieldNames.Text;
            }
        }
        public string Height
        {

            get
            {

                return textBox1.Text;

            }
        }
        public string Interval
        {

            get
            {

                return textBox2.Text;

            }
        }
        public ParaSetting(IFeatureClass pFeatureClass)
        {
            InitializeComponent();
            this.pFeatureClass = pFeatureClass;
        }
      
        private void ParaSetting_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < pFeatureClass.Fields.FieldCount; i++)
            {
                pFieldNames.Items.Add(pFeatureClass.Fields.get_Field(i).Name);

            }
            if (pFieldNames.Items.Count > 0)
            {
                pFieldNames.SelectedIndex = 0;
            }
        }

         

     

     

        
    }
}
