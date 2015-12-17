namespace AE_Environment.Forms
{
    partial class ClssFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClssFrm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.comboValue = new System.Windows.Forms.ComboBox();
            this.btnALL = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBox_TA = new System.Windows.Forms.CheckBox();
            this.checkBox_ED = new System.Windows.Forms.CheckBox();
            this.checkBox_MAI = new System.Windows.Forms.CheckBox();
            this.checkBox_AR = new System.Windows.Forms.CheckBox();
            this.checkBox_TE = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_Config = new System.Windows.Forms.Button();
            this.checkBox_UMI = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_MI = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox_MMI = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_CWED = new System.Windows.Forms.CheckBox();
            this.checkBox_TECI = new System.Windows.Forms.CheckBox();
            this.btn_input = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox_PAFRAC = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.bt_save = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.btn_OK = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(389, 522);
            this.panel1.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.comboValue);
            this.panel5.Controls.Add(this.btnALL);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.tabControl1);
            this.panel5.Controls.Add(this.btnClear);
            this.panel5.Controls.Add(this.bt_save);
            this.panel5.Controls.Add(this.btn_Delete);
            this.panel5.Controls.Add(this.dataGridView1);
            this.panel5.Controls.Add(this.listView1);
            this.panel5.Controls.Add(this.btn_OK);
            this.panel5.Controls.Add(this.progressBar1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(389, 522);
            this.panel5.TabIndex = 27;
            // 
            // comboValue
            // 
            this.comboValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboValue.FormattingEnabled = true;
            this.comboValue.Location = new System.Drawing.Point(52, 11);
            this.comboValue.Name = "comboValue";
            this.comboValue.Size = new System.Drawing.Size(324, 20);
            this.comboValue.TabIndex = 22;
            this.comboValue.SelectedIndexChanged += new System.EventHandler(this.comboValue_SelectedIndexChanged);
            // 
            // btnALL
            // 
            this.btnALL.Location = new System.Drawing.Point(331, 46);
            this.btnALL.Name = "btnALL";
            this.btnALL.Size = new System.Drawing.Size(45, 23);
            this.btnALL.TabIndex = 21;
            this.btnALL.Text = "ALL";
            this.btnALL.UseVisualStyleBackColor = true;
            this.btnALL.Click += new System.EventHandler(this.btnALL_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "类别：";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(14, 145);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(362, 152);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabIndexChanged += new System.EventHandler(this.tabControl1_TabIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBox_TA);
            this.tabPage1.Controls.Add(this.checkBox_ED);
            this.tabPage1.Controls.Add(this.checkBox_MAI);
            this.tabPage1.Controls.Add(this.checkBox_AR);
            this.tabPage1.Controls.Add(this.checkBox_TE);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(354, 126);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "面积-周长";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBox_TA
            // 
            this.checkBox_TA.AutoSize = true;
            this.checkBox_TA.Location = new System.Drawing.Point(39, 26);
            this.checkBox_TA.Name = "checkBox_TA";
            this.checkBox_TA.Size = new System.Drawing.Size(84, 16);
            this.checkBox_TA.TabIndex = 0;
            this.checkBox_TA.Text = "总面积(TA)";
            this.checkBox_TA.UseVisualStyleBackColor = true;
            this.checkBox_TA.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox_ED
            // 
            this.checkBox_ED.AutoSize = true;
            this.checkBox_ED.Location = new System.Drawing.Point(193, 86);
            this.checkBox_ED.Name = "checkBox_ED";
            this.checkBox_ED.Size = new System.Drawing.Size(108, 16);
            this.checkBox_ED.TabIndex = 4;
            this.checkBox_ED.Text = "边界密度（ED）";
            this.checkBox_ED.UseVisualStyleBackColor = true;
            this.checkBox_ED.CheckedChanged += new System.EventHandler(this.checkBox5_CheckedChanged);
            // 
            // checkBox_MAI
            // 
            this.checkBox_MAI.AutoSize = true;
            this.checkBox_MAI.Location = new System.Drawing.Point(39, 86);
            this.checkBox_MAI.Name = "checkBox_MAI";
            this.checkBox_MAI.Size = new System.Drawing.Size(138, 16);
            this.checkBox_MAI.TabIndex = 2;
            this.checkBox_MAI.Text = "最大面积占比（LPI）";
            this.checkBox_MAI.UseVisualStyleBackColor = true;
            this.checkBox_MAI.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // checkBox_AR
            // 
            this.checkBox_AR.AutoSize = true;
            this.checkBox_AR.Location = new System.Drawing.Point(39, 56);
            this.checkBox_AR.Name = "checkBox_AR";
            this.checkBox_AR.Size = new System.Drawing.Size(108, 16);
            this.checkBox_AR.TabIndex = 1;
            this.checkBox_AR.Text = "面积占比（AR）";
            this.checkBox_AR.UseVisualStyleBackColor = true;
            this.checkBox_AR.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox_TE
            // 
            this.checkBox_TE.AutoSize = true;
            this.checkBox_TE.Location = new System.Drawing.Point(193, 26);
            this.checkBox_TE.Name = "checkBox_TE";
            this.checkBox_TE.Size = new System.Drawing.Size(108, 16);
            this.checkBox_TE.TabIndex = 3;
            this.checkBox_TE.Text = "边界长度（TE）";
            this.checkBox_TE.UseVisualStyleBackColor = true;
            this.checkBox_TE.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_Config);
            this.tabPage2.Controls.Add(this.checkBox_UMI);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.checkBox_MI);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.checkBox_MMI);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(354, 126);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "破碎度";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_Config
            // 
            this.btn_Config.Enabled = false;
            this.btn_Config.Location = new System.Drawing.Point(223, 78);
            this.btn_Config.Name = "btn_Config";
            this.btn_Config.Size = new System.Drawing.Size(65, 23);
            this.btn_Config.TabIndex = 4;
            this.btn_Config.Text = "参考图层";
            this.btn_Config.UseVisualStyleBackColor = true;
            this.btn_Config.Click += new System.EventHandler(this.btn_Config_Click);
            // 
            // checkBox_UMI
            // 
            this.checkBox_UMI.AutoSize = true;
            this.checkBox_UMI.Location = new System.Drawing.Point(31, 21);
            this.checkBox_UMI.Name = "checkBox_UMI";
            this.checkBox_UMI.Size = new System.Drawing.Size(162, 16);
            this.checkBox_UMI.TabIndex = 1;
            this.checkBox_UMI.Text = "有效斑块面积占比（UMI）";
            this.checkBox_UMI.UseVisualStyleBackColor = true;
            this.checkBox_UMI.CheckedChanged += new System.EventHandler(this.checkBox_UMI_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(294, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "km²";
            // 
            // checkBox_MI
            // 
            this.checkBox_MI.AutoSize = true;
            this.checkBox_MI.Location = new System.Drawing.Point(31, 53);
            this.checkBox_MI.Name = "checkBox_MI";
            this.checkBox_MI.Size = new System.Drawing.Size(132, 16);
            this.checkBox_MI.TabIndex = 1;
            this.checkBox_MI.Text = "有效斑块面积（MI）";
            this.checkBox_MI.UseVisualStyleBackColor = true;
            this.checkBox_MI.CheckedChanged += new System.EventHandler(this.checkBox6_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(223, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(65, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "0";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // checkBox_MMI
            // 
            this.checkBox_MMI.AutoSize = true;
            this.checkBox_MMI.Location = new System.Drawing.Point(31, 85);
            this.checkBox_MMI.Name = "checkBox_MMI";
            this.checkBox_MMI.Size = new System.Drawing.Size(186, 16);
            this.checkBox_MMI.TabIndex = 1;
            this.checkBox_MMI.Text = "有效斑块面积（修正）（MMI）";
            this.checkBox_MMI.UseVisualStyleBackColor = true;
            this.checkBox_MMI.CheckedChanged += new System.EventHandler(this.checkBox_MMI_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(354, 126);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "形状对比度";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_CWED);
            this.groupBox1.Controls.Add(this.checkBox_TECI);
            this.groupBox1.Controls.Add(this.btn_input);
            this.groupBox1.Location = new System.Drawing.Point(3, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 62);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "对比度";
            // 
            // checkBox_CWED
            // 
            this.checkBox_CWED.AutoSize = true;
            this.checkBox_CWED.Location = new System.Drawing.Point(46, 20);
            this.checkBox_CWED.Name = "checkBox_CWED";
            this.checkBox_CWED.Size = new System.Drawing.Size(156, 16);
            this.checkBox_CWED.TabIndex = 6;
            this.checkBox_CWED.Text = "加权对比边界密度(CWED)";
            this.checkBox_CWED.UseVisualStyleBackColor = true;
            this.checkBox_CWED.CheckedChanged += new System.EventHandler(this.checkBox_CWED_CheckedChanged);
            // 
            // checkBox_TECI
            // 
            this.checkBox_TECI.AutoSize = true;
            this.checkBox_TECI.Location = new System.Drawing.Point(46, 42);
            this.checkBox_TECI.Name = "checkBox_TECI";
            this.checkBox_TECI.Size = new System.Drawing.Size(144, 16);
            this.checkBox_TECI.TabIndex = 6;
            this.checkBox_TECI.Text = "边界总长对比度(TECI)";
            this.checkBox_TECI.UseVisualStyleBackColor = true;
            this.checkBox_TECI.CheckedChanged += new System.EventHandler(this.checkBox_TECI_CheckedChanged);
            // 
            // btn_input
            // 
            this.btn_input.Location = new System.Drawing.Point(214, 17);
            this.btn_input.Name = "btn_input";
            this.btn_input.Size = new System.Drawing.Size(75, 23);
            this.btn_input.TabIndex = 1;
            this.btn_input.Text = " 权重参数";
            this.btn_input.UseVisualStyleBackColor = true;
            this.btn_input.Click += new System.EventHandler(this.btn_input_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox_PAFRAC);
            this.groupBox2.Location = new System.Drawing.Point(3, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(348, 46);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "形状";
            // 
            // checkBox_PAFRAC
            // 
            this.checkBox_PAFRAC.AutoSize = true;
            this.checkBox_PAFRAC.Location = new System.Drawing.Point(46, 20);
            this.checkBox_PAFRAC.Name = "checkBox_PAFRAC";
            this.checkBox_PAFRAC.Size = new System.Drawing.Size(132, 16);
            this.checkBox_PAFRAC.TabIndex = 5;
            this.checkBox_PAFRAC.Text = "分形指数（PAFRAC）";
            this.checkBox_PAFRAC.UseVisualStyleBackColor = true;
            this.checkBox_PAFRAC.CheckedChanged += new System.EventHandler(this.checkBox_PAFRAC_CheckedChanged);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(331, 116);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(45, 23);
            this.btnClear.TabIndex = 21;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // bt_save
            // 
            this.bt_save.Location = new System.Drawing.Point(301, 303);
            this.bt_save.Name = "bt_save";
            this.bt_save.Size = new System.Drawing.Size(75, 23);
            this.bt_save.TabIndex = 23;
            this.bt_save.Text = "保存";
            this.bt_save.UseVisualStyleBackColor = true;
            this.bt_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Location = new System.Drawing.Point(331, 81);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(45, 23);
            this.btn_Delete.TabIndex = 21;
            this.btn_Delete.Text = "X";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(14, 328);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(362, 152);
            this.dataGridView1.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(14, 46);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(311, 93);
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView1.TabIndex = 20;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(14, 299);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 23;
            this.btn_OK.Text = "计算";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(14, 486);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(362, 23);
            this.progressBar1.TabIndex = 26;
            // 
            // ClssFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 522);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ClssFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "类指标计算";
            this.Load += new System.EventHandler(this.ClssFrm_Load);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox_ED;
        private System.Windows.Forms.CheckBox checkBox_TA;
        private System.Windows.Forms.CheckBox checkBox_TE;
        private System.Windows.Forms.CheckBox checkBox_AR;
        private System.Windows.Forms.CheckBox checkBox_MAI;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.ComboBox comboValue;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBox_MI;
        private System.Windows.Forms.Button btnALL;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox checkBox_UMI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox_MMI;
        private System.Windows.Forms.Button btn_Config;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox checkBox_PAFRAC;
        private System.Windows.Forms.CheckBox checkBox_TECI;
        private System.Windows.Forms.CheckBox checkBox_CWED;
        private System.Windows.Forms.Button btn_input;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button bt_save;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox groupBox1;

    }
}