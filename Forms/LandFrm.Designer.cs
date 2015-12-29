namespace AE_Environment.Forms
{
    partial class LandFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LandFrm));
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBox_ED = new System.Windows.Forms.CheckBox();
            this.checkBox_TA = new System.Windows.Forms.CheckBox();
            this.checkBox_TE = new System.Windows.Forms.CheckBox();
            this.checkBox_MAI = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.checkBox_PAFRAC = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_Config = new System.Windows.Forms.Button();
            this.checkBox_MI = new System.Windows.Forms.CheckBox();
            this.checkBox_MMI = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_UMI = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.checkBox_NP = new System.Windows.Forms.CheckBox();
            this.checkBox_MPS = new System.Windows.Forms.CheckBox();
            this.checkBox_PD = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(275, 160);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(85, 23);
            this.btn_save.TabIndex = 13;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(12, 160);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(85, 23);
            this.btn_OK.TabIndex = 13;
            this.btn_OK.Text = "计算";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 336);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(347, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 189);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(347, 141);
            this.dataGridView1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(347, 142);
            this.tabControl1.TabIndex = 21;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBox_ED);
            this.tabPage1.Controls.Add(this.checkBox_TA);
            this.tabPage1.Controls.Add(this.checkBox_TE);
            this.tabPage1.Controls.Add(this.checkBox_MAI);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(339, 116);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "面积-周长";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBox_ED
            // 
            this.checkBox_ED.AutoSize = true;
            this.checkBox_ED.Location = new System.Drawing.Point(201, 49);
            this.checkBox_ED.Name = "checkBox_ED";
            this.checkBox_ED.Size = new System.Drawing.Size(96, 16);
            this.checkBox_ED.TabIndex = 1;
            this.checkBox_ED.Text = "边界密度(ED)";
            this.checkBox_ED.UseVisualStyleBackColor = true;
            this.checkBox_ED.CheckedChanged += new System.EventHandler(this.checkBox_ED_CheckedChanged);
            // 
            // checkBox_TA
            // 
            this.checkBox_TA.AutoSize = true;
            this.checkBox_TA.Location = new System.Drawing.Point(39, 21);
            this.checkBox_TA.Name = "checkBox_TA";
            this.checkBox_TA.Size = new System.Drawing.Size(96, 16);
            this.checkBox_TA.TabIndex = 0;
            this.checkBox_TA.Text = "总面积（TA）";
            this.checkBox_TA.UseVisualStyleBackColor = true;
            this.checkBox_TA.CheckedChanged += new System.EventHandler(this.checkBox_TA_CheckedChanged);
            // 
            // checkBox_TE
            // 
            this.checkBox_TE.AutoSize = true;
            this.checkBox_TE.Location = new System.Drawing.Point(39, 77);
            this.checkBox_TE.Name = "checkBox_TE";
            this.checkBox_TE.Size = new System.Drawing.Size(102, 16);
            this.checkBox_TE.TabIndex = 3;
            this.checkBox_TE.Text = "边界长度（TE)";
            this.checkBox_TE.UseVisualStyleBackColor = true;
            this.checkBox_TE.CheckedChanged += new System.EventHandler(this.checkBox_TE_CheckedChanged);
            // 
            // checkBox_MAI
            // 
            this.checkBox_MAI.AutoSize = true;
            this.checkBox_MAI.Location = new System.Drawing.Point(39, 49);
            this.checkBox_MAI.Name = "checkBox_MAI";
            this.checkBox_MAI.Size = new System.Drawing.Size(150, 16);
            this.checkBox_MAI.TabIndex = 2;
            this.checkBox_MAI.Text = "最大斑块面积占比(LPI)";
            this.checkBox_MAI.UseVisualStyleBackColor = true;
            this.checkBox_MAI.CheckedChanged += new System.EventHandler(this.checkBox_MAI_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.checkBox_PAFRAC);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(339, 116);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "形状";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // checkBox_PAFRAC
            // 
            this.checkBox_PAFRAC.AutoSize = true;
            this.checkBox_PAFRAC.Location = new System.Drawing.Point(38, 46);
            this.checkBox_PAFRAC.Name = "checkBox_PAFRAC";
            this.checkBox_PAFRAC.Size = new System.Drawing.Size(132, 16);
            this.checkBox_PAFRAC.TabIndex = 7;
            this.checkBox_PAFRAC.Text = "分形指数（PAFRAC）";
            this.checkBox_PAFRAC.UseVisualStyleBackColor = true;
            this.checkBox_PAFRAC.CheckedChanged += new System.EventHandler(this.checkBox_PAFRAC_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_Config);
            this.tabPage2.Controls.Add(this.checkBox_MI);
            this.tabPage2.Controls.Add(this.checkBox_MMI);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.checkBox_UMI);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(339, 116);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "破碎度";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_Config
            // 
            this.btn_Config.Enabled = false;
            this.btn_Config.Location = new System.Drawing.Point(213, 80);
            this.btn_Config.Name = "btn_Config";
            this.btn_Config.Size = new System.Drawing.Size(65, 23);
            this.btn_Config.TabIndex = 4;
            this.btn_Config.Text = "参考图层";
            this.btn_Config.UseVisualStyleBackColor = true;
            this.btn_Config.Click += new System.EventHandler(this.btn_Config_Click);
            // 
            // checkBox_MI
            // 
            this.checkBox_MI.AutoSize = true;
            this.checkBox_MI.Location = new System.Drawing.Point(24, 17);
            this.checkBox_MI.Name = "checkBox_MI";
            this.checkBox_MI.Size = new System.Drawing.Size(132, 16);
            this.checkBox_MI.TabIndex = 1;
            this.checkBox_MI.Text = "有效斑块面积（MI）";
            this.checkBox_MI.UseVisualStyleBackColor = true;
            this.checkBox_MI.CheckedChanged += new System.EventHandler(this.checkBox_MI_CheckedChanged);
            // 
            // checkBox_MMI
            // 
            this.checkBox_MMI.AutoSize = true;
            this.checkBox_MMI.Location = new System.Drawing.Point(24, 83);
            this.checkBox_MMI.Name = "checkBox_MMI";
            this.checkBox_MMI.Size = new System.Drawing.Size(186, 16);
            this.checkBox_MMI.TabIndex = 1;
            this.checkBox_MMI.Text = "有效斑块面积（修正）（MMI）";
            this.checkBox_MMI.UseVisualStyleBackColor = true;
            this.checkBox_MMI.CheckedChanged += new System.EventHandler(this.checkBox_MMI_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(284, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "km²";
            // 
            // checkBox_UMI
            // 
            this.checkBox_UMI.AutoSize = true;
            this.checkBox_UMI.Location = new System.Drawing.Point(24, 50);
            this.checkBox_UMI.Name = "checkBox_UMI";
            this.checkBox_UMI.Size = new System.Drawing.Size(162, 16);
            this.checkBox_UMI.TabIndex = 1;
            this.checkBox_UMI.Text = "有效斑块面积占比（UMI）";
            this.checkBox_UMI.UseVisualStyleBackColor = true;
            this.checkBox_UMI.CheckedChanged += new System.EventHandler(this.checkBox_UMI_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(213, 48);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(65, 21);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "0";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.checkBox_NP);
            this.tabPage4.Controls.Add(this.checkBox_MPS);
            this.tabPage4.Controls.Add(this.checkBox_PD);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(339, 116);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "聚集度";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // checkBox_NP
            // 
            this.checkBox_NP.AutoSize = true;
            this.checkBox_NP.Location = new System.Drawing.Point(21, 21);
            this.checkBox_NP.Name = "checkBox_NP";
            this.checkBox_NP.Size = new System.Drawing.Size(96, 16);
            this.checkBox_NP.TabIndex = 6;
            this.checkBox_NP.Text = "斑块个数(NP)";
            this.checkBox_NP.UseVisualStyleBackColor = true;
            this.checkBox_NP.CheckedChanged += new System.EventHandler(this.checkBox_NP_CheckedChanged);
            // 
            // checkBox_MPS
            // 
            this.checkBox_MPS.AutoSize = true;
            this.checkBox_MPS.Location = new System.Drawing.Point(21, 81);
            this.checkBox_MPS.Name = "checkBox_MPS";
            this.checkBox_MPS.Size = new System.Drawing.Size(138, 16);
            this.checkBox_MPS.TabIndex = 8;
            this.checkBox_MPS.Text = "平均斑块面积（MPS）";
            this.checkBox_MPS.UseVisualStyleBackColor = true;
            this.checkBox_MPS.CheckedChanged += new System.EventHandler(this.checkBox_MPS_CheckedChanged);
            // 
            // checkBox_PD
            // 
            this.checkBox_PD.AutoSize = true;
            this.checkBox_PD.Location = new System.Drawing.Point(21, 51);
            this.checkBox_PD.Name = "checkBox_PD";
            this.checkBox_PD.Size = new System.Drawing.Size(108, 16);
            this.checkBox_PD.TabIndex = 7;
            this.checkBox_PD.Text = "斑块密度（PD）";
            this.checkBox_PD.UseVisualStyleBackColor = true;
            this.checkBox_PD.CheckedChanged += new System.EventHandler(this.checkBox_PD_CheckedChanged);
            // 
            // LandFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 371);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LandFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "整体景观指标";
            this.Load += new System.EventHandler(this.LandFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox checkBox_ED;
        private System.Windows.Forms.CheckBox checkBox_TA;
        private System.Windows.Forms.CheckBox checkBox_TE;
        private System.Windows.Forms.CheckBox checkBox_MAI;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox checkBox_PAFRAC;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btn_Config;
        private System.Windows.Forms.CheckBox checkBox_MI;
        private System.Windows.Forms.CheckBox checkBox_MMI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox_UMI;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckBox checkBox_NP;
        private System.Windows.Forms.CheckBox checkBox_MPS;
        private System.Windows.Forms.CheckBox checkBox_PD;
    }
}