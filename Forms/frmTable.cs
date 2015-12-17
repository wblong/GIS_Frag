using BaseGIS;
using BaseGIS.GISForm;
using BaseGIS.GISCommand;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AE_Environment.Forms
{
    public partial class frmTable : Form
    {
        private ILayer mLayer;
        private IMapControl3 mMapControl;


       
        
        private int mCurrentRowIndex = -1;
        private Dictionary<int, string> mDictionary = new Dictionary<int, string>();
        private IFeatureClass mFeatureClass;
        private IFeatureLayer mFeatureLayer;
     
        private ILayerFields mLayerFields;
        
        private frmProgress mProgress;
        private string mSelectFieldCalcu = "";
        private string mSelectFieldName = "";
        private ITable mTable;
        private IWorkspace mWorkspace = null;

        public frmTable()
        {

        
            InitializeComponent();
        }
        public frmTable(ILayer pLyr, IMapControl3 pMapControl):this()
        {
            
            this.mLayer = pLyr;
            this.mMapControl = pMapControl;
        }
        //按钮是否启用
        private void controlBtnEnable()
        {
            IFeatureSelection mLayer = this.mLayer as IFeatureSelection;
            if (mLayer.SelectionSet.Count > 0)
            {
                this.toolStripButton1.Enabled = true;
                this.toolStripButton2.Enabled = true;
                this.toolStripButton4.Enabled = true;
                this.toolStripButton5.Enabled = true;
                this.toolStripButton6.Enabled = true;
            }
            else
            {
                this.toolStripButton1.Enabled = false;
                this.toolStripButton2.Enabled = false;
                this.toolStripButton4.Enabled = false;
                this.toolStripButton5.Enabled = false;
                this.toolStripButton6.Enabled = false;
            }
            if (this.dataGridViewX1.ReadOnly)
            {
                this.toolStripButton1.Enabled = false;
            }
            else
            {
                this.toolStripButton1.Enabled = true;
            }
        }
        private DataTable CreateDataTable(ILayer pLayer, string tableName)
        {
            DataTable table = this.CreateDataTableByLayer(pLayer, tableName);
            string str = this.getShapeType(pLayer);
            DataRow row = null;
            ICursor o = (pLayer as ITable).Search(null, false);
            IRow row2 = o.NextRow();
            int num = 0;
            while (row2 != null)
            {
                row = table.NewRow();
                for (int i = 0; i < row2.Fields.FieldCount; i++)
                {
                    if (row2.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        row[i] = str;
                    }
                    else if (row2.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeBlob)
                    {
                        row[i] = "Element";
                    }
                    else
                    {
                        row[i] = row2.get_Value(i);
                    }
                }
                table.Rows.Add(row);
                num++;
                Application.DoEvents();
                if (num == 0x7d0)
                {
                    row2 = null;
                }
                else
                {
                    row2 = o.NextRow();
                }
            }
            Marshal.ReleaseComObject(o);
            return table;
        }
        private DataTable CreateDataTableByLayer(ILayer pLayer, string tableName)
        {
            DataTable table = new DataTable(tableName);
            ITable table2 = pLayer as ITable;
            IField field = null;
            for (int i = 0; i < table2.Fields.FieldCount; i++)
            {
                field = table2.Fields.get_Field(i);
                DataColumn column = new DataColumn(field.Name);
                if (field.Name == table2.OIDFieldName)
                {
                    column.Unique = true;
                }
                column.AllowDBNull = field.IsNullable;
                column.Caption = field.AliasName;
                column.DataType = System.Type.GetType(this.ParseFieldType(field.Type));
                column.DefaultValue = field.DefaultValue;
                if (field.VarType == 8)
                {
                    column.MaxLength = field.Length;
                }
                table.Columns.Add(column);
                field = null;
                column = null;
            }
            return table;
        }

        private DataTable CreateSelectFeatureDataTable(ILayer pLayer, string tableName, IFeatureSelection pFeatureSelection)
        {
            DataTable table = this.CreateDataTableByLayer(pLayer, tableName);
            string str = this.getShapeType(pLayer);
            DataRow row = null;
            ICursor cursor = null;
            pFeatureSelection.SelectionSet.Search(null, false, out cursor);
            IRow row2 = cursor.NextRow();
            int num = 0;
            while (row2 != null)
            {
                row = table.NewRow();
                for (int i = 0; i < row2.Fields.FieldCount; i++)
                {
                    if (row2.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeGeometry)
                    {
                        row[i] = str;
                    }
                    else if (row2.Fields.get_Field(i).Type == esriFieldType.esriFieldTypeBlob)
                    {
                        row[i] = "Element";
                    }
                    else
                    {
                        row[i] = row2.get_Value(i);
                    }
                }
                table.Rows.Add(row);
                row = null;
                num++;
                if (num == 0x7d0)
                {
                    row2 = null;
                }
                else
                {
                    row2 = cursor.NextRow();
                }
            }
            Marshal.ReleaseComObject(cursor);
            return table;
        }

        private string getShapeType(ILayer pLayer)
        {
            IFeatureLayer layer = (IFeatureLayer)pLayer;
            switch (layer.FeatureClass.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    return "Point";

                case esriGeometryType.esriGeometryPolyline:
                    return "Polyline";

                case esriGeometryType.esriGeometryPolygon:
                    return "Polygon";
            }
            return "";
        }

        private string getValidFeatureClassName(string FCname)
        {
            if (FCname.IndexOf(".") != -1)
            {
                return FCname.Replace(".", "_");
            }
            return FCname;
        }
        private string ParseFieldType(esriFieldType fieldType)
        {
            switch (fieldType)
            {
                case esriFieldType.esriFieldTypeSmallInteger:
                    return "System.Int32";

                case esriFieldType.esriFieldTypeInteger:
                    return "System.Int32";

                case esriFieldType.esriFieldTypeSingle:
                    return "System.Single";

                case esriFieldType.esriFieldTypeDouble:
                    return "System.Double";

                case esriFieldType.esriFieldTypeString:
                    return "System.String";

                case esriFieldType.esriFieldTypeDate:
                    return "System.DateTime";

                case esriFieldType.esriFieldTypeOID:
                    return "System.String";

                case esriFieldType.esriFieldTypeGeometry:
                    return "System.String";

                case esriFieldType.esriFieldTypeBlob:
                    return "System.String";

                case esriFieldType.esriFieldTypeRaster:
                    return "System.String";

                case esriFieldType.esriFieldTypeGUID:
                    return "System.String";

                case esriFieldType.esriFieldTypeGlobalID:
                    return "System.String";
            }
            return "System.String";
        }
        private void isSelectCurrentRow(DataTable pDataTable, DataGridViewRow pDataGridViewRow)
        {
            if (pDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in pDataTable.Rows)
                {
                    if (pDataTable.Columns.Contains("OBJECTID") && (row["OBJECTID"].ToString() == pDataGridViewRow.Cells["OBJECTID"].Value.ToString()))
                    {
                        pDataGridViewRow.Selected = true;
                        break;
                    }
                }
            }
        }
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dataGridViewX1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //取得元素
            IFeature feature = this.mFeatureClass.GetFeature(Convert.ToInt32(this.dataGridViewX1.Rows[e.RowIndex].Cells[this.mFeatureClass.OIDFieldName].Value));
           //编辑一个元素
            if (feature.Fields.get_Field(e.ColumnIndex).Editable)
            {
                if (feature.Fields.get_Field(e.ColumnIndex).Type == esriFieldType.esriFieldTypeInteger)
                {
                    feature.set_Value(e.ColumnIndex, Convert.ToInt32(this.dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
                }
                else if (feature.Fields.get_Field(e.ColumnIndex).Type == esriFieldType.esriFieldTypeString)
                {
                    string str = this.dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    feature.set_Value(e.ColumnIndex, this.dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                }
                feature.Store();
            }
            this.mMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeography, this.mFeatureClass, null);
        }

        private void dataGridViewX1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }
        //单击标题右键菜单
        private void dataGridViewX1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                IField field = this.mFeatureClass.Fields.get_Field(e.ColumnIndex);
                if (((field.Type == esriFieldType.esriFieldTypeDouble) || (field.Type == esriFieldType.esriFieldTypeInteger)) || (field.Type == esriFieldType.esriFieldTypeSmallInteger))
                {
                    this.ToolStripMenuItemStatistics.Enabled = true;
                    this.mSelectFieldName = this.dataGridViewX1.Columns[e.ColumnIndex].HeaderText;
                    if (field.Required)
                    {
                        this.ToolStripMenuItemCalcultator.Enabled = false;
                        this.mSelectFieldCalcu = "";
                    }
                    else
                    {
                        this.ToolStripMenuItemCalcultator.Enabled = true;
                        this.mSelectFieldCalcu = this.dataGridViewX1.Columns[e.ColumnIndex].HeaderText;
                    }
                }
                else
                {
                    this.ToolStripMenuItemCalcultator.Enabled = false;
                    this.ToolStripMenuItemStatistics.Enabled = false;
                    this.mSelectFieldCalcu = "";
                    this.mSelectFieldName = "";
                }
                int num = 0;
                for (int i = 0; i < e.ColumnIndex; i++)
                {
                    num += this.dataGridViewX1.Columns[i].Width;
                }
                this.contextMenuStrip1.Show(this.dataGridViewX1, new System.Drawing.Point((e.X + num) + 20, e.Y));
            }
        }
        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewX1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.mCurrentRowIndex = -1;
                this.mCurrentRowIndex = e.RowIndex;
                if (this.mCurrentRowIndex != -1)
                {
                    if (this.dataGridViewX1.SelectedRows.Count > 0)
                    {
                        this.ToolStripMenuItemZoomToSel.Enabled = true;
                        this.ToolStripMenuItemClear.Enabled = true;
                    }
                    else
                    {
                        this.ToolStripMenuItemZoomToSel.Enabled = false;
                        this.ToolStripMenuItemClear.Enabled = false;
                    }
                    this.contextMenuStrip2.Show(this.dataGridViewX1, new System.Drawing.Point(e.X, e.Y));
                }
            }
        }

        private void dataGridViewX1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dataGridViewX1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {

        }

        private void dataGridViewX1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                this.mMapControl.Map.ClearSelection();
                foreach (DataGridViewRow row in this.dataGridViewX1.SelectedRows)
                {
                    IQueryFilter queryFilter = new QueryFilterClass();
                    if (row.Cells[0].Value.ToString() != "")
                    {
                        queryFilter.WhereClause = this.mFeatureClass.OIDFieldName + "=" + row.Cells[this.mFeatureClass.OIDFieldName].Value.ToString();
                        IFeatureCursor o = this.mFeatureLayer.Search(queryFilter, false);
                        for (IFeature feature = o.NextFeature(); feature != null; feature = o.NextFeature())
                        {
                            this.mMapControl.Map.SelectFeature(this.mFeatureLayer, feature);
                        }
                        if (o != null)
                        {
                            Marshal.ReleaseComObject(o);
                        }
                    }
                }
                IFeatureSelection mLayer = this.mLayer as IFeatureSelection;
                string name = this.getValidFeatureClassName(this.mLayer.Name);
                if (mLayer.SelectionSet.Count > 0)
                {
                    name = name + "_Selection";
                    if (PublicVariable.mDataSet.Tables.Contains(name))
                    {
                        PublicVariable.mDataSet.Tables.Remove(name);
                    }
                    DataTable table = this.CreateSelectFeatureDataTable(this.mLayer, name, mLayer);
                    PublicVariable.mDataSet.Tables.Add(table);
                    this.dataGridView1.DataSource = table;
                }
                else
                {
                    this.dataGridView1.DataSource = null;
                }
                this.controlBtnEnable();
                this.mMapControl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, this.mFeatureLayer, this.mMapControl.ActiveView.Extent);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void dataGridViewX1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            this.mFeatureClass.GetFeature(Convert.ToInt32(e.Row.Cells[this.mFeatureClass.OIDFieldName].Value)).Delete();
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("是否删除选择要素？", "删除询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    foreach (DataGridViewRow row in this.dataGridViewX1.SelectedRows)
                    {
                        this.mFeatureClass.GetFeature(Convert.ToInt32(row.Cells[this.mFeatureClass.OIDFieldName].Value)).Delete();
                        this.dataGridViewX1.Rows.Remove(row);
                    }
                    this.mMapControl.Refresh((esriViewDrawPhase)0xffff, Missing.Value, Missing.Value);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Visible = true;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Visible = false;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            int count = 0;
            IFeatureCursor selectedFeatures = DataEditFunction.GetSelectedFeatures(this.mFeatureLayer, ref count);
            if (selectedFeatures != null)
            {
                for (IFeature feature = selectedFeatures.NextFeature(); feature != null; feature = selectedFeatures.NextFeature())
                {
                    this.mMapControl.FlashShape(feature.Shape, 1, 300, null);
                }
                if (selectedFeatures != null)
                {
                    Marshal.ReleaseComObject(selectedFeatures);
                }
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ICommand command = new cmdMapZoomToSelectFeatures();
            command.OnCreate(this.mMapControl.Object);
            command.OnClick();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataGridViewX1.SelectedRows)
            {
                row.Selected = false;
            }
            this.dataGridView1.Refresh();
        }

        private void ToolStripMenuItemCalcultator_Click(object sender, EventArgs e)
        {
            new frmFieldCalculator(this.mFeatureLayer, this.dataGridViewX1, this.mSelectFieldCalcu) { TopMost = true }.ShowDialog();
        }

        private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否删除当前要素？", "删除询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                IFeature feature = this.mFeatureClass.GetFeature(Convert.ToInt32(this.dataGridViewX1.Rows[this.mCurrentRowIndex].Cells[this.mFeatureClass.OIDFieldName].Value));
                if (feature != null)
                {
                    feature.Delete();
                    this.dataGridViewX1.Rows.RemoveAt(this.mCurrentRowIndex);
                }
                this.mMapControl.Refresh((esriViewDrawPhase)0xffff, Missing.Value, Missing.Value);
            }
        }

        private void ToolStripMenuItemFlash_Click(object sender, EventArgs e)
        {
            IFeature feature = this.mFeatureClass.GetFeature(Convert.ToInt32(this.dataGridViewX1.Rows[this.mCurrentRowIndex].Cells[this.mFeatureClass.OIDFieldName].Value));
            if (feature != null)
            {
                this.mMapControl.FlashShape(feature.Shape, 3, 300, Missing.Value);
            }
        }

        private void ToolStripMenuItemPanTo_Click(object sender, EventArgs e)
        {
            IFeature feature = this.mFeatureClass.GetFeature(Convert.ToInt32(this.dataGridViewX1.Rows[this.mCurrentRowIndex].Cells[this.mFeatureClass.OIDFieldName].Value));
            if (feature != null)
            {
                IPoint centerPoint = new PointClass();
                ITopologicalOperator shape = (ITopologicalOperator)feature.Shape;
                IEnvelope envelope = shape.Buffer(10.0).Envelope;
                centerPoint.X = (envelope.XMax + envelope.XMin) / 2.0;
                centerPoint.Y = (envelope.YMax + envelope.YMin) / 2.0;
                this.mMapControl.CenterAt(centerPoint);
            }
        }

        private void ToolStripMenuItemSelectOrUnSel_Click(object sender, EventArgs e)
        {
            this.dataGridViewX1.Rows[this.mCurrentRowIndex].Selected = !this.dataGridViewX1.Rows[this.mCurrentRowIndex].Selected;
        }

        private void ToolStripMenuItemStatistics_Click(object sender, EventArgs e)
        {
            new frmFieldStatistics(this.dataGridViewX1, this.mFeatureLayer, this.mSelectFieldName) { TopMost = true }.ShowDialog();
        }

        private void ToolStripMenuItemZoomTo_Click(object sender, EventArgs e)
        {
            IFeature feature = this.mFeatureClass.GetFeature(Convert.ToInt32(this.dataGridViewX1.Rows[this.mCurrentRowIndex].Cells[this.mFeatureClass.OIDFieldName].Value));
            if (feature != null)
            {
                ITopologicalOperator shape = (ITopologicalOperator)feature.Shape;
                this.mMapControl.Extent = shape.Buffer(10.0).Envelope;
            }
        }

        private void frmTable_Load(object sender, EventArgs e)
        {
            try
            {
                this.mFeatureLayer = this.mLayer as IFeatureLayer;
                this.mFeatureClass = this.mFeatureLayer.FeatureClass;
                if (this.mFeatureLayer != null)
                {
                    this.mLayerFields = this.mFeatureLayer as ILayerFields;
                    IDataset mFeatureClass = this.mFeatureClass as IDataset;
                    if (this.mFeatureLayer.DataSourceType.Substring(0, 3) == "CAD")
                    {
                        this.dataGridViewX1.ReadOnly = true;
                        this.dataGridViewX1.AllowUserToAddRows = false;
                        this.dataGridViewX1.AllowUserToDeleteRows = false;
                        this.dataGridViewX1.UserDeletingRow -= new DataGridViewRowCancelEventHandler(this.dataGridViewX1_UserDeletingRow);
                        this.dataGridView1.ReadOnly = true;
                        this.dataGridView1.AllowUserToAddRows = false;
                        this.dataGridView1.AllowUserToDeleteRows = false;
                        this.dataGridView1.UserDeletingRow -= new DataGridViewRowCancelEventHandler(this.dataGridViewX1_UserDeletingRow);
                        this.toolStripButton1.Enabled = false;
                    }
                    else
                    {
                        IWorkspaceEdit workspace = mFeatureClass.Workspace as IWorkspaceEdit;
                        if (workspace.IsBeingEdited())
                        {
                            this.dataGridViewX1.ReadOnly = false;
                            this.dataGridView1.ReadOnly = false;
                            this.dataGridViewX1.AllowUserToAddRows = true;
                            this.dataGridView1.AllowUserToAddRows = false;
                            this.dataGridViewX1.AllowUserToDeleteRows = true;
                            this.dataGridView1.AllowUserToDeleteRows = true;
                            this.dataGridViewX1.UserDeletingRow += new DataGridViewRowCancelEventHandler(this.dataGridViewX1_UserDeletingRow);
                            this.dataGridViewX1.CellEndEdit += new DataGridViewCellEventHandler(this.dataGridViewX1_CellEndEdit);
                            this.dataGridView1.UserDeletingRow += new DataGridViewRowCancelEventHandler(this.dataGridViewX1_UserDeletingRow);
                            this.toolStripButton1.Enabled = true;
                        }
                        else
                        {
                            this.dataGridViewX1.ReadOnly = true;
                            this.dataGridViewX1.AllowUserToAddRows = false;
                            this.dataGridViewX1.AllowUserToDeleteRows = false;
                            this.dataGridViewX1.UserDeletingRow -= new DataGridViewRowCancelEventHandler(this.dataGridViewX1_UserDeletingRow);
                            this.dataGridView1.ReadOnly = true;
                            this.dataGridView1.AllowUserToAddRows = false;
                            this.dataGridView1.AllowUserToDeleteRows = false;
                            this.dataGridViewX1.CellEndEdit -= new DataGridViewCellEventHandler(this.dataGridViewX1_CellEndEdit);
                            this.dataGridView1.UserDeletingRow -= new DataGridViewRowCancelEventHandler(this.dataGridViewX1_UserDeletingRow);
                            this.toolStripButton1.Enabled = false;
                        }
                    }
                    if (this.mFeatureLayer != null)
                    {
                        DataTable table;
                        string name = this.getValidFeatureClassName(this.mLayer.Name);
                        if (!PublicVariable.mDataSet.Tables.Contains(name))
                        {
                            table = this.CreateDataTable(this.mLayer, name);
                            PublicVariable.mDataSet.Tables.Add(table);
                        }
                        else
                        {
                            PublicVariable.mDataSet.Tables.Remove(name);
                            table = this.CreateDataTable(this.mLayer, name);
                            PublicVariable.mDataSet.Tables.Add(table);
                        }
                        PublicVariable.mBindingSource.DataSource = PublicVariable.mDataSet;
                        PublicVariable.mBindingSource.DataMember = name;
                        this.dataGridViewX1.DataSource = PublicVariable.mDataSet.Tables[name];
                        this.dataGridViewX1.Refresh();
                        IFeatureSelection mLayer = this.mLayer as IFeatureSelection;
                        if (mLayer.SelectionSet.Count > 0)
                        {
                            name = name + "_Selection";
                            if (PublicVariable.mDataSet.Tables.Contains(name))
                            {
                                PublicVariable.mDataSet.Tables.Remove(name);
                            }
                            table = this.CreateSelectFeatureDataTable(this.mLayer, name, mLayer);
                            PublicVariable.mDataSet.Tables.Add(table);
                            foreach (DataGridViewRow row in (IEnumerable) this.dataGridViewX1.Rows)
                            {
                                this.isSelectCurrentRow(table, row);
                            }
                        }
                        if (this.mFeatureLayer != null)
                        {
                            this.Text = "属性表[" + this.mFeatureLayer.Name + "]";
                        }
                        this.dataGridViewX1.SelectionChanged += new EventHandler(this.dataGridViewX1_SelectionChanged);
                        this.controlBtnEnable();
                        this.dataGridViewX1.AlternatingRowsDefaultCellStyle.BackColor = Color.LemonChiffon;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("读取属性表失败：" + exception.Message);
                base.Close();
            }
        }

        private void ToolStripMenuItemZoomToSel_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItemClear_Click(object sender, EventArgs e)
        {

        }

    }
}
