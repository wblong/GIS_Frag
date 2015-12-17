using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using BaseGIS;
using System.Collections;
namespace AE_Environment.Forms
{
    public partial class frmMerge : Form
    {

        private AxMapControl m_axMapControl;
        private Dictionary<int, ILayer> MapLayer = null;

        public frmMerge()
        {
            InitializeComponent();
        }
        public frmMerge(AxMapControl axMapControl):this() {
            m_axMapControl = axMapControl;
        }

        private void butcancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            if (this.CheckingInput())
            {
                Application.DoEvents();
                this.comboxlayer.Enabled = false;
                this.comboxFiled.Enabled = false;
                this.comboClss1.Enabled = false;
                this.comboClss2.Enabled = false;
                this.textBox1.Enabled = false;
                this.button1.Enabled = false;
                this.butOk.Enabled = false;
                this.progressBar1.Visible = true;
                this.progressBar1.Minimum = 0;


                ILayer layer = this.MapLayer[this.comboxlayer.SelectedIndex];
                IDataset pDataSet = (layer as IFeatureLayer).FeatureClass as IDataset;
                //string outputDir = System.Environment.CurrentDirectory + "\\Default.gdb";
                IWorkspace pWorkspace = Utilities.WorkspaceHelper.GetShapefileWorkspace(MainForm.outshape);
                //
                IEnumDataset pDataSetsEnum = pWorkspace.get_Datasets(esriDatasetType.esriDTAny);
                IDataset pDataTM = pDataSetsEnum.Next();
                while (pDataTM != null)
                {

                    if (pDataTM.Name=="temp")
                    {
                        if (pDataTM.CanDelete())
                        {
                            pDataTM.Delete();
                            break;
                        }
                        else
                        {

                            MessageBox.Show("请检查是否处于打开状态！");
                            return;
                        }
                    }
                    
                    pDataTM = pDataSetsEnum.Next();
                }
                ArcGISUtilities.CopyData((layer as IFeatureLayer).FeatureClass, pWorkspace, "temp");
                IEnumDataset pEnumDS = pWorkspace.get_Datasets(esriDatasetType.esriDTAny);

                IFeatureClass featureClass = ArcGISUtilities.GetDatasetByName(pEnumDS, "temp") as IFeatureClass;
                IQueryFilter   pQueryFilter=null;
                IFeatureCursor pFCursor=null;

               

                int index = featureClass.Fields.FindField(comboxFiled.Text);
                if (index < 0)
                {
                    MessageBox.Show("请检查数据是否含分类码或检查编码文件.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }

                foreach (ListViewItem lvi in listView1.Items)
                {
                    string code = lvi.Text;
                     if (code.Substring(0, 1) == "0" || code.Substring(0, 1) == "1")
                    {
                        string temp = lvi.Text.Substring(2, 2);
                        //一级类
                        if (temp == "00")
                        {
                            code = code.Substring(0, 2);
                            pQueryFilter = new QueryFilterClass();
                            //pQueryFilter.WhereClause = "[" + comboxFiled.Text + "]" + " LIKE " + "\'" + code + "**" + "\'";//GeoDatase中
                            pQueryFilter.WhereClause = comboxFiled.Text + " LIKE " + "\'" + code + "__" + "\'";
                            pFCursor = featureClass.Update(pQueryFilter, false);
                            IFeature pFeat = pFCursor.NextFeature();
                            while (pFeat != null)
                            {
                                pFeat.set_Value(index, lvi.Text);
                                pFCursor.UpdateFeature(pFeat);
                                pFeat = pFCursor.NextFeature();
                            }

                        }
                        //二级类
                        else if (temp.Substring(1, 1) == "0")
                        {

                            code = code.Substring(0, 3);
                            pQueryFilter = new QueryFilterClass();
                            pQueryFilter.WhereClause = "[" + comboxFiled.Text + "]" + " LIKE " + "\'" + code + "*" + "\'";
                            pFCursor = featureClass.Update(pQueryFilter, false);
                            IFeature pFeat = pFCursor.NextFeature();
                            while (pFeat != null)
                            {
                                pFeat.set_Value(index, lvi.Text);
                                pFCursor.UpdateFeature(pFeat);
                                pFeat = pFCursor.NextFeature();
                            }

                        }
                    }
                    
                    this.progressBar1.Value += 1;
    
                }

               ////////////////////////////////////////////////////////////////////////////////////////////////////
                Application.DoEvents();
                this.progressBar1.Value = 70;
                IFeatureClass pFeatureClass = null;

                VectorOperation operation = new VectorOperation();

                pFeatureClass = operation.Dissolv(featureClass, comboxFiled.Text, textBox1.Text.Trim(), this.textBox2.Text.Trim()
                    , this.progressBar1);

                IFeatureLayer pLayer = new FeatureLayerClass
                {
                    FeatureClass = pFeatureClass,
                    Name = pFeatureClass.AliasName
                };

                this.m_axMapControl.AddLayer(pLayer);
                this.m_axMapControl.ActiveView.Refresh();
                //释放资源
                if (pFCursor!=null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pFCursor);
                }
                if (pQueryFilter!=null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pQueryFilter);
                    
                }
                if (featureClass!=null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(featureClass);
                }
               
                MessageBox.Show(pLayer.Name + "处理完成！");
                base.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
            if (this.comboxlayer.SelectedItem == null)
            {
                MessageBox.Show("请选择输入图层.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (this.comboxFiled.Text == "")
            {
                MessageBox.Show("请选择融合字段.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
//             if (this.comboClss1.SelectedItem.ToString() == "")
//             {
//                 MessageBox.Show("请分级类别.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
//                 return false;
//             }
            if (this.textBox1.Text == "")
            {
                MessageBox.Show("请选择输出路径.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (this.listView1.Items.Count<1)
            {
                MessageBox.Show("没有要合并的类别.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            return true;
        }

        private void frmMerge_Load(object sender, EventArgs e)
        {

            listView1.Columns.Add("编码", 40, HorizontalAlignment.Left);
            listView1.Columns.Add("类型", 80, HorizontalAlignment.Left);
            ////排序
            ////排序
            this.listView1.ListViewItemSorter = new Utilities.ListViewColumnSorter();
            listView1.ColumnClick += new ColumnClickEventHandler(Utilities.ListViewHelper.ListView_ColumnClick);
             
            this.MapLayer = new Dictionary<int, ILayer>();
            for (int i = 0; i < this.m_axMapControl.LayerCount; i++)
            {
                ILayer layer = this.m_axMapControl.get_Layer(i);
                IFeatureLayer layer2 = layer as IFeatureLayer;
                if ((((layer != null) && layer.Valid) && (layer is IFeatureLayer)))
                {
                    this.comboxlayer.Items.Add(layer.Name);
                   
                    this.MapLayer.Add(this.comboxlayer.Items.Count - 1, layer);
                }
            }
            if (this.comboxlayer.Items.Count > 0)
            {
                this.comboxlayer.SelectedIndex = 0;
            }
            //this.textBox1.Text = Application.StartupPath + @"\Default.gdb";
        }
        /// <summary>
        /// 初始化一二级分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboxFiled_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboClss1.Items.Count > 0)
            {
                comboClss1.Items.Clear();
            }
            if (comboClss2.Items.Count > 0)
            {
                comboClss2.Items.Clear();
            }
            ILayer layer = this.MapLayer[this.comboxlayer.SelectedIndex];
            IFeatureClass pFeatureClass = (layer as IFeatureLayer).FeatureClass;
            IFields pFields = pFeatureClass.Fields;

            string fieldName = comboxFiled.Text;

            if (fieldName == "" )
                return;
           
            //组合二级类别
            IEnumerator pEnumerator = ArcGISUtilities.GetUniqueValue(pFeatureClass, fieldName);
            List<string> classes = new List<string>();
            List<string> firstclass = new List<string>();
            pEnumerator.Reset();
            while (pEnumerator.MoveNext())
            {                         
                string tempcode = pEnumerator.Current.ToString();
                classes.Add(pEnumerator.Current.ToString().Substring(0, 3).PadRight(4, '0'));

            }
            object[] clss2 = FragStats.Stats.GetDistinctValues(classes);//二级类的唯一值
            for (int i = 0; i < clss2.Length; i++)
            {
                firstclass.Add(clss2[i].ToString().Substring(0, 2).PadRight(4, '0'));

                string temp = clss2[i].ToString().Substring(2, 2);
                if (temp == "00")
                {
                    continue;
                }
                if (MainForm.pData.ContainsKey(clss2[i].ToString()))
                {
                    comboClss2.Items.Add(MainForm.pData[clss2[i].ToString()]);

                }
            }

            comboClss2.Text = "";
            //组合一级类别
            object[] clss1 = FragStats.Stats.GetDistinctValues(firstclass);
            for (int i = 0; i < clss1.Length; i++)
            {
                if (MainForm.pData.ContainsKey(clss1[i].ToString()))
                {
                    comboClss1.Items.Add(MainForm.pData[clss1[i].ToString()]);
                }
            }
            comboClss1.Text = "";
            //释放资源
           // System.Runtime.InteropServices.Marshal.ReleaseComObject(pFeatureClass);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(pEnumerator);
        }

        private void comboxlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboxFiled.Items.Count > 0)
            {
                comboxFiled.Items.Clear();
            }
            ILayer layer = this.MapLayer[this.comboxlayer.SelectedIndex];
            IFeatureClass featureClass = (layer as IFeatureLayer).FeatureClass;
            IFields pFields = featureClass.Fields;
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                comboxFiled.Items.Add(pFields.get_Field(i).Name);

            }
            comboxFiled.Text = "";
            this.textBox2.Text = comboxlayer.Text + "_Merge";
        }
        /// <summary>
        /// 一级类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboClss1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboClss1.SelectedIndex;
            string value = comboClss1.Items[index].ToString();
            string key = "";
            foreach (string k in MainForm.pData.Keys)
            {
                if (MainForm.pData[k] == value)
                {
                    key = k;
                    break;
                }
            }
            this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度 
            ListViewItem lvi = new ListViewItem();

            lvi.Text = key;
            lvi.SubItems.Add(value);
            this.listView1.Items.Add(lvi);

            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
        }
        /// <summary>
        /// 二级类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboClss2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboClss2.SelectedIndex;
            string value = comboClss2.Items[index].ToString();
            string key = "";
            foreach (string k in MainForm.pData.Keys)
            {
                if (MainForm.pData[k] == value)
                {
                    key = k;
                    break;
                }
            }
            this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度 
            ListViewItem lvi = new ListViewItem();

            lvi.Text = key;
            lvi.SubItems.Add(value);
            this.listView1.Items.Add(lvi);
            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
        }

        private void btnALL_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < comboClss1.Items.Count; i++)
            {

                string value = comboClss1.Items[i].ToString();
                string key = "";
                foreach (string k in MainForm.pData.Keys)
                {
                    if (MainForm.pData[k] == value)
                    {
                        key = k;
                        break;
                    }
                }
                this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度 
                ListViewItem lvi = new ListViewItem();

                lvi.Text = key;
                lvi.SubItems.Add(value);
                this.listView1.Items.Add(lvi);

                this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。 
            }
        }

        private void btnALL2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < comboClss2.Items.Count; i++)
            {

                string value = comboClss2.Items[i].ToString();
                string key = "";
                foreach (string k in MainForm.pData.Keys)
                {
                    if (MainForm.pData[k] == value)
                    {
                        key = k;
                        break;
                    }
                }
                this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度 
                ListViewItem lvi = new ListViewItem();

                lvi.Text = key;
                lvi.SubItems.Add(value);
                this.listView1.Items.Add(lvi);

                this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。 
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listView1.SelectedItems)
            {

                listView1.Items.RemoveAt(lvi.Index);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }
        /// <summary>
        /// 自定义分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Custom_Click(object sender, EventArgs e)
        {

        }

        

        

         
    }


}
