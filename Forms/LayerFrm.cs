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
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;

using AE_Environment.Command;

namespace AE_Environment.Forms
{
    public partial class LayerFrm : Form
    {

        //in
        private IMapControl3 m_axMapControl;
        private Dictionary<int, ILayer> MapLayer = null;
        public ILayer m_Layer = null;
        public LayerFrm()
        {

            InitializeComponent();
        }
        public LayerFrm(IMapControl3 axMapControl)
        {
            InitializeComponent();
            this.m_axMapControl = axMapControl;
        }
        public ILayer PLayer
        {

            get {
                return m_Layer;
            }
        }
        private void btn_OK_Click(object sender, EventArgs e)
        {

            if (this.CheckingInput())
            {
                Application.DoEvents();
                this.comboInput.Enabled = false;
                m_Layer = this.MapLayer[this.comboInput.SelectedIndex];
               
            }
            else { return; }
            base.Close();
        }

        private void LayerFrm_Load(object sender, EventArgs e)
        {
            this.MapLayer = new Dictionary<int, ILayer>();
            for (int i = 0; i < this.m_axMapControl.LayerCount; i++)
            {
                ILayer layer = this.m_axMapControl.get_Layer(i);
                IFeatureLayer layer2 = layer as IFeatureLayer;
                if ((((layer != null) && layer.Valid) && (layer is IFeatureLayer)))
                {
                    this.comboInput.Items.Add(layer.Name);

                    this.MapLayer.Add(this.comboInput.Items.Count - 1, layer);
                }
            }
            if (this.comboInput.Items.Count > 0)
            {
                this.comboInput.SelectedIndex = 0;
            }
            
        }
        private bool CheckingInput()
        {
            if (this.comboInput.SelectedItem == null)
            {
                MessageBox.Show("请选择输入图层.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }


            return true;
        }

       
    }
}
