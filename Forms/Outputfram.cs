using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using AE_Environment.Model;

namespace AE_Environment.Forms
{
    public partial class Outputfram : Form
    {
        public string m_path = "";
        private ExportClass exportClass = null;
        public Outputfram()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MainForm.path != null)
            {
                this.folderBrowserDialog1.SelectedPath = MainForm.path;
            }
            this.folderBrowserDialog1.Description = "保存数据";
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            this.folderBrowserDialog1.ShowDialog();
            string selectedPath = this.folderBrowserDialog1.SelectedPath;
            if (selectedPath == "")
            {
                MessageBox.Show("请选择保存路径！");
            }
            else if (!this.folderBrowserDialog1.SelectedPath.Contains(".gdb"))
            {
                MessageBox.Show("必须选择FileGDB文件夹【.gdb】");
                this.textBox1.Text = "";
            }
            else
            {
                this.textBox1.Text = selectedPath;
            }
        }
        private bool CheckingInput()
        {


            if (this.textBox1.Text == "")
            {
                MessageBox.Show("请选择输出路径.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }

            return true;
        }
        private void butOk_Click(object sender, EventArgs e)
        {
            if (this.CheckingInput())
            {

                this.textBox1.Enabled = false;
                this.button1.Enabled = false;
                this.butOk.Enabled = false;
                this.progressBar1.Enabled = true;
                this.progressBar1.Visible = true;
                this.progressBar1.Minimum = 0;
                int progressValue=0;

                try
                {


                    if (MainForm.dt_class != null)
                    {
                        
                        progressValue+=MainForm.dt_class.Rows.Count;
                    }

                    if (MainForm.dt_land != null)
                    {
                         
                       progressValue+=MainForm.dt_land.Rows.Count;
                    }

                    if (MainForm.dt_EcosystemIndex != null)
                    {
                         progressValue+=MainForm.dt_EcosystemIndex.Rows.Count;
                    }

                    if (MainForm.dt_custom != null)
                    {
                         progressValue+=MainForm.dt_custom.Rows.Count;
                      
                    }
                    if (MainForm.dt_Population != null)
                    {

                        progressValue+=MainForm.dt_Population.Rows.Count;
                    }
                    this.progressBar1.Maximum = progressValue + 1;

                    if (MainForm.dt_class != null)
                    {
                        exportClass = new ExportClass(MainForm.dt_class);
                        string name = "C_" + textBox2.Text;
                        if (MainForm.baseData.zone_FC != null)
                        {
                            exportClass.ExportIFeatureClass(textBox1.Text, name, this.progressBar1);
                        }
                        else
                        {
                            exportClass.ExportITableClass(textBox1.Text, name,this.progressBar1);
                            
                        }

                    }

                    if (MainForm.dt_land != null)
                    {
                        exportClass = new ExportClass(MainForm.dt_land);
                        string name = "L_" + textBox2.Text;
                        if (MainForm.baseData.zone_FC != null)
                        {
                            exportClass.ExportIFeatureClass(textBox1.Text, name, this.progressBar1);
                        }
                        else
                        {
                            exportClass.ExportITableClass(textBox1.Text, name, this.progressBar1);

                        }
                       
                    }

                    if (MainForm.dt_EcosystemIndex != null)
                    {
                        exportClass = new ExportClass(MainForm.dt_EcosystemIndex);
                        string name = "eco_" + textBox2.Text;
                        if (MainForm.baseData.zone_FC != null)
                        {
                            exportClass.ExportIFeatureClass(textBox1.Text, name, this.progressBar1);
                        }
                        else
                        {
                            exportClass.ExportITableClass(textBox1.Text, name, this.progressBar1);

                        }
                        
                    }

                    if (MainForm.dt_custom != null)
                    {
                        exportClass = new ExportClass(MainForm.dt_custom);
                        string name = "custom_" + textBox2.Text;
                        if (MainForm.baseData.zone_FC != null)
                        {
                            exportClass.ExportIFeatureClass(textBox1.Text, name, this.progressBar1);
                        }
                        else
                        {
                            exportClass.ExportITableClass(textBox1.Text, name, this.progressBar1);

                        }
                    }
                    if (MainForm.dt_Population != null)
                    {

                        exportClass = new ExportClass(MainForm.dt_Population);
                        string name = "population_" + textBox2.Text;
                        if (MainForm.baseData.zone_FC != null)
                        {
                            exportClass.ExportIFeatureClass(textBox1.Text, name, this.progressBar1);
                        }
                        else
                        {
                            exportClass.ExportITableClass(textBox1.Text, name, this.progressBar1);

                        }
                       
                    }
                    MessageBox.Show("处理完成。");

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("处理失败!");
                }

                this.Close();
            }
        }

        private void butcancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void Outputfram_Load(object sender, EventArgs e)
        {

        }
    }
}
