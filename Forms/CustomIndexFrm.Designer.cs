namespace AE_Environment.Forms
{
    partial class CustomIndexFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomIndexFrm));
            this.label1 = new System.Windows.Forms.Label();
            this.bt_Calculate = new System.Windows.Forms.Button();
            this.bt_Clear = new System.Windows.Forms.Button();
            this.bt_RBracket = new System.Windows.Forms.Button();
            this.bt_Division = new System.Windows.Forms.Button();
            this.bt_Minus = new System.Windows.Forms.Button();
            this.lv_UniqueValue = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.bt_Multiplicative = new System.Windows.Forms.Button();
            this.tb_IndexName = new System.Windows.Forms.TextBox();
            this.bt_AddToList = new System.Windows.Forms.Button();
            this.bt_LoadFormula = new System.Windows.Forms.Button();
            this.rtb_Math = new System.Windows.Forms.RichTextBox();
            this.bt_LBracket = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbt_Perimeter = new System.Windows.Forms.RadioButton();
            this.rbt_Area = new System.Windows.Forms.RadioButton();
            this.lv_Formula = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ctmstrip_lv = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除该指标ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgv_Result = new System.Windows.Forms.DataGridView();
            this.ctmstrip_dgv = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除列ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bt_Save = new System.Windows.Forms.Button();
            this.bt_Add = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.bt_SaveFormula = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.ctmstrip_lv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Result)).BeginInit();
            this.ctmstrip_dgv.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "自定义指标名称：\r\n";
            // 
            // bt_Calculate
            // 
            this.bt_Calculate.Location = new System.Drawing.Point(277, 326);
            this.bt_Calculate.Name = "bt_Calculate";
            this.bt_Calculate.Size = new System.Drawing.Size(75, 23);
            this.bt_Calculate.TabIndex = 7;
            this.bt_Calculate.Text = "计算";
            this.bt_Calculate.UseVisualStyleBackColor = true;
            this.bt_Calculate.Click += new System.EventHandler(this.bt_Calculate_Click);
            // 
            // bt_Clear
            // 
            this.bt_Clear.Location = new System.Drawing.Point(189, 326);
            this.bt_Clear.Name = "bt_Clear";
            this.bt_Clear.Size = new System.Drawing.Size(75, 23);
            this.bt_Clear.TabIndex = 7;
            this.bt_Clear.Text = "清除";
            this.bt_Clear.UseVisualStyleBackColor = true;
            this.bt_Clear.Click += new System.EventHandler(this.bt_Clear_Click);
            // 
            // bt_RBracket
            // 
            this.bt_RBracket.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_RBracket.Location = new System.Drawing.Point(69, 212);
            this.bt_RBracket.Name = "bt_RBracket";
            this.bt_RBracket.Size = new System.Drawing.Size(50, 23);
            this.bt_RBracket.TabIndex = 5;
            this.bt_RBracket.Text = " )";
            this.bt_RBracket.UseVisualStyleBackColor = true;
            this.bt_RBracket.Click += new System.EventHandler(this.bt_RBracket_Click);
            // 
            // bt_Division
            // 
            this.bt_Division.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_Division.Location = new System.Drawing.Point(69, 183);
            this.bt_Division.Name = "bt_Division";
            this.bt_Division.Size = new System.Drawing.Size(50, 23);
            this.bt_Division.TabIndex = 5;
            this.bt_Division.Text = "/";
            this.bt_Division.UseVisualStyleBackColor = true;
            this.bt_Division.Click += new System.EventHandler(this.bt_Division_Click);
            // 
            // bt_Minus
            // 
            this.bt_Minus.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_Minus.Location = new System.Drawing.Point(69, 154);
            this.bt_Minus.Name = "bt_Minus";
            this.bt_Minus.Size = new System.Drawing.Size(50, 23);
            this.bt_Minus.TabIndex = 5;
            this.bt_Minus.Text = "-";
            this.bt_Minus.UseVisualStyleBackColor = true;
            this.bt_Minus.Click += new System.EventHandler(this.bt_Minus_Click);
            // 
            // lv_UniqueValue
            // 
            this.lv_UniqueValue.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lv_UniqueValue.Location = new System.Drawing.Point(125, 125);
            this.lv_UniqueValue.Name = "lv_UniqueValue";
            this.lv_UniqueValue.Size = new System.Drawing.Size(195, 110);
            this.lv_UniqueValue.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lv_UniqueValue.TabIndex = 3;
            this.lv_UniqueValue.UseCompatibleStateImageBehavior = false;
            this.lv_UniqueValue.View = System.Windows.Forms.View.Details;
            this.lv_UniqueValue.DoubleClick += new System.EventHandler(this.lv_UniqueValue_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "编码";
            this.columnHeader1.Width = 59;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "类别名称";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 100;
            // 
            // bt_Multiplicative
            // 
            this.bt_Multiplicative.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_Multiplicative.Location = new System.Drawing.Point(13, 183);
            this.bt_Multiplicative.Name = "bt_Multiplicative";
            this.bt_Multiplicative.Size = new System.Drawing.Size(50, 23);
            this.bt_Multiplicative.TabIndex = 5;
            this.bt_Multiplicative.Text = "*";
            this.bt_Multiplicative.UseVisualStyleBackColor = true;
            this.bt_Multiplicative.Click += new System.EventHandler(this.bt_Multiplicative_Click);
            // 
            // tb_IndexName
            // 
            this.tb_IndexName.Location = new System.Drawing.Point(114, 6);
            this.tb_IndexName.Name = "tb_IndexName";
            this.tb_IndexName.Size = new System.Drawing.Size(326, 21);
            this.tb_IndexName.TabIndex = 1;
            this.tb_IndexName.Text = "自定义指标";
            // 
            // bt_AddToList
            // 
            this.bt_AddToList.Location = new System.Drawing.Point(13, 326);
            this.bt_AddToList.Name = "bt_AddToList";
            this.bt_AddToList.Size = new System.Drawing.Size(75, 23);
            this.bt_AddToList.TabIndex = 25;
            this.bt_AddToList.Text = "添加公式";
            this.bt_AddToList.UseVisualStyleBackColor = true;
            this.bt_AddToList.Click += new System.EventHandler(this.bt_AddToList_Click);
            // 
            // bt_LoadFormula
            // 
            this.bt_LoadFormula.Location = new System.Drawing.Point(13, 125);
            this.bt_LoadFormula.Name = "bt_LoadFormula";
            this.bt_LoadFormula.Size = new System.Drawing.Size(106, 23);
            this.bt_LoadFormula.TabIndex = 26;
            this.bt_LoadFormula.Text = "加载公式模板";
            this.bt_LoadFormula.UseVisualStyleBackColor = true;
            this.bt_LoadFormula.Click += new System.EventHandler(this.bt_LoadFormula_Click);
            // 
            // rtb_Math
            // 
            this.rtb_Math.BackColor = System.Drawing.SystemColors.ControlLight;
            this.rtb_Math.Location = new System.Drawing.Point(13, 241);
            this.rtb_Math.Name = "rtb_Math";
            this.rtb_Math.Size = new System.Drawing.Size(427, 79);
            this.rtb_Math.TabIndex = 22;
            this.rtb_Math.Text = "";
            // 
            // bt_LBracket
            // 
            this.bt_LBracket.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_LBracket.Location = new System.Drawing.Point(13, 212);
            this.bt_LBracket.Name = "bt_LBracket";
            this.bt_LBracket.Size = new System.Drawing.Size(50, 23);
            this.bt_LBracket.TabIndex = 5;
            this.bt_LBracket.Text = "( ";
            this.bt_LBracket.UseVisualStyleBackColor = true;
            this.bt_LBracket.Click += new System.EventHandler(this.bt_LBracket_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbt_Perimeter);
            this.groupBox2.Controls.Add(this.rbt_Area);
            this.groupBox2.Location = new System.Drawing.Point(326, 125);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(114, 110);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Function";
            // 
            // rbt_Perimeter
            // 
            this.rbt_Perimeter.AutoSize = true;
            this.rbt_Perimeter.Location = new System.Drawing.Point(6, 39);
            this.rbt_Perimeter.Name = "rbt_Perimeter";
            this.rbt_Perimeter.Size = new System.Drawing.Size(101, 16);
            this.rbt_Perimeter.TabIndex = 0;
            this.rbt_Perimeter.Text = "Perimeter（）";
            this.rbt_Perimeter.UseVisualStyleBackColor = true;
            // 
            // rbt_Area
            // 
            this.rbt_Area.AutoSize = true;
            this.rbt_Area.Checked = true;
            this.rbt_Area.Location = new System.Drawing.Point(6, 20);
            this.rbt_Area.Name = "rbt_Area";
            this.rbt_Area.Size = new System.Drawing.Size(71, 16);
            this.rbt_Area.TabIndex = 0;
            this.rbt_Area.TabStop = true;
            this.rbt_Area.Text = "Area（）";
            this.rbt_Area.UseVisualStyleBackColor = true;
            // 
            // lv_Formula
            // 
            this.lv_Formula.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.lv_Formula.ContextMenuStrip = this.ctmstrip_lv;
            this.lv_Formula.Location = new System.Drawing.Point(13, 33);
            this.lv_Formula.Name = "lv_Formula";
            this.lv_Formula.Scrollable = false;
            this.lv_Formula.Size = new System.Drawing.Size(427, 86);
            this.lv_Formula.TabIndex = 27;
            this.lv_Formula.UseCompatibleStateImageBehavior = false;
            this.lv_Formula.View = System.Windows.Forms.View.Details;
            this.lv_Formula.DoubleClick += new System.EventHandler(this.lv_Formula_DoubleClick);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "指标名称";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "公式";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 365;
            // 
            // ctmstrip_lv
            // 
            this.ctmstrip_lv.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除该指标ToolStripMenuItem});
            this.ctmstrip_lv.Name = "ctmstrip_lv";
            this.ctmstrip_lv.Size = new System.Drawing.Size(137, 26);
            // 
            // 删除该指标ToolStripMenuItem
            // 
            this.删除该指标ToolStripMenuItem.Name = "删除该指标ToolStripMenuItem";
            this.删除该指标ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.删除该指标ToolStripMenuItem.Text = "删除该指标";
            this.删除该指标ToolStripMenuItem.Click += new System.EventHandler(this.删除该指标ToolStripMenuItem_Click);
            // 
            // dgv_Result
            // 
            this.dgv_Result.AllowUserToAddRows = false;
            this.dgv_Result.AllowUserToResizeColumns = false;
            this.dgv_Result.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Result.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_Result.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Result.ContextMenuStrip = this.ctmstrip_dgv;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Result.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_Result.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgv_Result.Location = new System.Drawing.Point(13, 355);
            this.dgv_Result.Name = "dgv_Result";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Result.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_Result.RowHeadersVisible = false;
            this.dgv_Result.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_Result.RowTemplate.Height = 23;
            this.dgv_Result.Size = new System.Drawing.Size(427, 187);
            this.dgv_Result.TabIndex = 8;
            // 
            // ctmstrip_dgv
            // 
            this.ctmstrip_dgv.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除行ToolStripMenuItem,
            this.删除列ToolStripMenuItem});
            this.ctmstrip_dgv.Name = "ctmstrip_dgv";
            this.ctmstrip_dgv.Size = new System.Drawing.Size(113, 48);
            // 
            // 删除行ToolStripMenuItem
            // 
            this.删除行ToolStripMenuItem.Name = "删除行ToolStripMenuItem";
            this.删除行ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.删除行ToolStripMenuItem.Text = "删除行";
            this.删除行ToolStripMenuItem.Click += new System.EventHandler(this.删除行ToolStripMenuItem_Click);
            // 
            // 删除列ToolStripMenuItem
            // 
            this.删除列ToolStripMenuItem.Name = "删除列ToolStripMenuItem";
            this.删除列ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.删除列ToolStripMenuItem.Text = "删除列";
            this.删除列ToolStripMenuItem.Click += new System.EventHandler(this.删除列ToolStripMenuItem_Click);
            // 
            // bt_Save
            // 
            this.bt_Save.Location = new System.Drawing.Point(365, 326);
            this.bt_Save.Name = "bt_Save";
            this.bt_Save.Size = new System.Drawing.Size(75, 23);
            this.bt_Save.TabIndex = 7;
            this.bt_Save.Text = "保存";
            this.bt_Save.UseVisualStyleBackColor = true;
            this.bt_Save.Click += new System.EventHandler(this.bt_Save_Click);
            // 
            // bt_Add
            // 
            this.bt_Add.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bt_Add.Location = new System.Drawing.Point(13, 154);
            this.bt_Add.Name = "bt_Add";
            this.bt_Add.Size = new System.Drawing.Size(50, 23);
            this.bt_Add.TabIndex = 5;
            this.bt_Add.Text = "+";
            this.bt_Add.UseVisualStyleBackColor = true;
            this.bt_Add.Click += new System.EventHandler(this.bt_Add_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.lv_Formula);
            this.panel1.Controls.Add(this.bt_SaveFormula);
            this.panel1.Controls.Add(this.bt_AddToList);
            this.panel1.Controls.Add(this.bt_LoadFormula);
            this.panel1.Controls.Add(this.rtb_Math);
            this.panel1.Controls.Add(this.dgv_Result);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.bt_Save);
            this.panel1.Controls.Add(this.bt_Calculate);
            this.panel1.Controls.Add(this.bt_Clear);
            this.panel1.Controls.Add(this.bt_RBracket);
            this.panel1.Controls.Add(this.bt_Division);
            this.panel1.Controls.Add(this.tb_IndexName);
            this.panel1.Controls.Add(this.bt_Minus);
            this.panel1.Controls.Add(this.lv_UniqueValue);
            this.panel1.Controls.Add(this.bt_LBracket);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.bt_Multiplicative);
            this.panel1.Controls.Add(this.bt_Add);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(454, 580);
            this.panel1.TabIndex = 21;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(13, 548);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(427, 23);
            this.progressBar1.TabIndex = 28;
            // 
            // bt_SaveFormula
            // 
            this.bt_SaveFormula.Location = new System.Drawing.Point(101, 326);
            this.bt_SaveFormula.Name = "bt_SaveFormula";
            this.bt_SaveFormula.Size = new System.Drawing.Size(75, 23);
            this.bt_SaveFormula.TabIndex = 25;
            this.bt_SaveFormula.Text = "保存公式模板";
            this.bt_SaveFormula.UseVisualStyleBackColor = true;
            this.bt_SaveFormula.Click += new System.EventHandler(this.bt_SaveFormula_Click);
            // 
            // CustomIndexFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 580);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomIndexFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自定义指标";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CustomIndexFrm_FormClosing);
            this.Load += new System.EventHandler(this.CustomIndexFrm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ctmstrip_lv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Result)).EndInit();
            this.ctmstrip_dgv.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_Calculate;
        private System.Windows.Forms.Button bt_Clear;
        private System.Windows.Forms.Button bt_RBracket;
        private System.Windows.Forms.Button bt_Division;
        private System.Windows.Forms.Button bt_Minus;
        private System.Windows.Forms.ListView lv_UniqueValue;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button bt_Multiplicative;
        private System.Windows.Forms.TextBox tb_IndexName;
        private System.Windows.Forms.Button bt_AddToList;
        private System.Windows.Forms.Button bt_LoadFormula;
        private System.Windows.Forms.RichTextBox rtb_Math;
        private System.Windows.Forms.Button bt_LBracket;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbt_Perimeter;
        private System.Windows.Forms.RadioButton rbt_Area;
        private System.Windows.Forms.ListView lv_Formula;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ContextMenuStrip ctmstrip_lv;
        private System.Windows.Forms.ToolStripMenuItem 删除该指标ToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgv_Result;
        private System.Windows.Forms.ContextMenuStrip ctmstrip_dgv;
        private System.Windows.Forms.ToolStripMenuItem 删除行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除列ToolStripMenuItem;
        private System.Windows.Forms.Button bt_Save;
        private System.Windows.Forms.Button bt_Add;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button bt_SaveFormula;
    }
}