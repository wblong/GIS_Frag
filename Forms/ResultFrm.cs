using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AE_Environment.Forms
{
    public partial class ResultFrm : Form
    {
        private DataSet m_DataSet;
        public static int indexCount = 0;
        public ResultFrm(DataSet pDataSet)
        {
            InitializeComponent();

            this.m_DataSet = pDataSet;
        }

        private void ResultFrm_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = MainForm.dt_class;
            this.dataGridView2.DataSource = MainForm.dt_land;
            this.dataGridView3.DataSource = MainForm.dt_EcosystemIndex;
            this.dataGridView4.DataSource = MainForm.dt_custom;
            this.dataGridView5.DataSource = MainForm.dt_PopSpatial;
        }

        private void 类别指标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainForm.dt_class!=null)
            {
                FragStats.Stats.DataTableToTxt(MainForm.dt_class);
            }
        }

        private void 景观指标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainForm.dt_land!=null)
            {
                FragStats.Stats.DataTableToTxt(MainForm.dt_land);
            }
        }

        private void 生态环境指标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainForm.dt_EcosystemIndex!=null)
            {
                FragStats.Stats.DataTableToTxt(MainForm.dt_EcosystemIndex);
            }

        }

        private void 自定义指标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainForm.dt_custom!=null)
            {
                FragStats.Stats.DataTableToTxt(MainForm.dt_custom);
            }
        }

        private void 人口格网化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainForm.dt_PopSpatial != null)
            {
                FragStats.Stats.DataTableToTxt(MainForm.dt_PopSpatial);
            }
        }

        private void ResultFrm_FormClosed(object sender, FormClosedEventArgs e)
        { 
           
        }

        private void ResultFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        

    }
}
