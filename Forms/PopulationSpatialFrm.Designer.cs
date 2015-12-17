namespace AE_Environment.Forms
{
    partial class PopulationSpatialFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopulationSpatialFrm));
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbox_Grid_Field = new System.Windows.Forms.ComboBox();
            this.cbox_Grid_Layer = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bt_LoadPopData = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cbox_Zone_Field = new System.Windows.Forms.ComboBox();
            this.cbox_Boundary_Layer = new System.Windows.Forms.ComboBox();
            this.cbox_Pop_Field = new System.Windows.Forms.ComboBox();
            this.tbox_Population = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbox_Optional = new System.Windows.Forms.CheckBox();
            this.cbox_Density = new System.Windows.Forms.ComboBox();
            this.cbox_Height = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbox_Building_Layer = new System.Windows.Forms.ComboBox();
            this.bt_Cancel = new System.Windows.Forms.Button();
            this.bt_Run = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbox_Name = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbox_OutPutPath = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.bt_AddPath = new System.Windows.Forms.Button();
            this.lab_Message = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.cbox_Grid_Field);
            this.groupBox5.Controls.Add(this.cbox_Grid_Layer);
            this.groupBox5.Location = new System.Drawing.Point(12, 240);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(355, 74);
            this.groupBox5.TabIndex = 23;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "格网数据";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "格网数据图层：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "格网编码字段：";
            // 
            // cbox_Grid_Field
            // 
            this.cbox_Grid_Field.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbox_Grid_Field.FormattingEnabled = true;
            this.cbox_Grid_Field.Location = new System.Drawing.Point(101, 46);
            this.cbox_Grid_Field.Name = "cbox_Grid_Field";
            this.cbox_Grid_Field.Size = new System.Drawing.Size(248, 20);
            this.cbox_Grid_Field.TabIndex = 11;
            // 
            // cbox_Grid_Layer
            // 
            this.cbox_Grid_Layer.FormattingEnabled = true;
            this.cbox_Grid_Layer.Location = new System.Drawing.Point(101, 20);
            this.cbox_Grid_Layer.Name = "cbox_Grid_Layer";
            this.cbox_Grid_Layer.Size = new System.Drawing.Size(248, 20);
            this.cbox_Grid_Layer.TabIndex = 11;
            this.cbox_Grid_Layer.SelectedIndexChanged += new System.EventHandler(this.cbox_Grid_Layer_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.bt_LoadPopData);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.cbox_Zone_Field);
            this.groupBox4.Controls.Add(this.cbox_Boundary_Layer);
            this.groupBox4.Controls.Add(this.cbox_Pop_Field);
            this.groupBox4.Controls.Add(this.tbox_Population);
            this.groupBox4.Location = new System.Drawing.Point(12, 107);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(355, 127);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "人口统计数据";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "行政边界图层：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "分区编码字段：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "行政边界图层：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "添加人口数据：";
            // 
            // bt_LoadPopData
            // 
            this.bt_LoadPopData.Location = new System.Drawing.Point(305, 73);
            this.bt_LoadPopData.Name = "bt_LoadPopData";
            this.bt_LoadPopData.Size = new System.Drawing.Size(44, 20);
            this.bt_LoadPopData.TabIndex = 13;
            this.bt_LoadPopData.Text = "选择";
            this.bt_LoadPopData.UseVisualStyleBackColor = true;
            this.bt_LoadPopData.Click += new System.EventHandler(this.bt_LoadPopData_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "人口数据字段：";
            // 
            // cbox_Zone_Field
            // 
            this.cbox_Zone_Field.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbox_Zone_Field.FormattingEnabled = true;
            this.cbox_Zone_Field.Location = new System.Drawing.Point(101, 46);
            this.cbox_Zone_Field.Name = "cbox_Zone_Field";
            this.cbox_Zone_Field.Size = new System.Drawing.Size(248, 20);
            this.cbox_Zone_Field.TabIndex = 11;
            // 
            // cbox_Boundary_Layer
            // 
            this.cbox_Boundary_Layer.FormattingEnabled = true;
            this.cbox_Boundary_Layer.Location = new System.Drawing.Point(101, 20);
            this.cbox_Boundary_Layer.Name = "cbox_Boundary_Layer";
            this.cbox_Boundary_Layer.Size = new System.Drawing.Size(248, 20);
            this.cbox_Boundary_Layer.TabIndex = 11;
            this.cbox_Boundary_Layer.SelectedIndexChanged += new System.EventHandler(this.cbox_Boundary_Layer_SelectedIndexChanged);
            // 
            // cbox_Pop_Field
            // 
            this.cbox_Pop_Field.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbox_Pop_Field.FormattingEnabled = true;
            this.cbox_Pop_Field.Location = new System.Drawing.Point(101, 99);
            this.cbox_Pop_Field.Name = "cbox_Pop_Field";
            this.cbox_Pop_Field.Size = new System.Drawing.Size(248, 20);
            this.cbox_Pop_Field.TabIndex = 11;
            // 
            // tbox_Population
            // 
            this.tbox_Population.Location = new System.Drawing.Point(101, 72);
            this.tbox_Population.Name = "tbox_Population";
            this.tbox_Population.Size = new System.Drawing.Size(198, 21);
            this.tbox_Population.TabIndex = 12;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbox_Optional);
            this.groupBox3.Controls.Add(this.cbox_Density);
            this.groupBox3.Controls.Add(this.cbox_Height);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.cbox_Building_Layer);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(355, 89);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "人口空间分布参考图层";
            // 
            // cbox_Optional
            // 
            this.cbox_Optional.AutoSize = true;
            this.cbox_Optional.Location = new System.Drawing.Point(8, 65);
            this.cbox_Optional.Name = "cbox_Optional";
            this.cbox_Optional.Size = new System.Drawing.Size(84, 16);
            this.cbox_Optional.TabIndex = 14;
            this.cbox_Optional.Text = "可选参数：";
            this.cbox_Optional.UseVisualStyleBackColor = true;
            this.cbox_Optional.CheckedChanged += new System.EventHandler(this.cbox_Optional_CheckedChanged);
            // 
            // cbox_Density
            // 
            this.cbox_Density.Enabled = false;
            this.cbox_Density.FormattingEnabled = true;
            this.cbox_Density.Location = new System.Drawing.Point(101, 63);
            this.cbox_Density.Name = "cbox_Density";
            this.cbox_Density.Size = new System.Drawing.Size(120, 20);
            this.cbox_Density.TabIndex = 13;
            // 
            // cbox_Height
            // 
            this.cbox_Height.Enabled = false;
            this.cbox_Height.FormattingEnabled = true;
            this.cbox_Height.Location = new System.Drawing.Point(229, 63);
            this.cbox_Height.Name = "cbox_Height";
            this.cbox_Height.Size = new System.Drawing.Size(120, 20);
            this.cbox_Height.TabIndex = 13;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(211, 48);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 10;
            this.label12.Text = "建筑高度：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(99, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 10;
            this.label11.Text = "建筑密度：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "房屋建筑图层：";
            // 
            // cbox_Building_Layer
            // 
            this.cbox_Building_Layer.FormattingEnabled = true;
            this.cbox_Building_Layer.Location = new System.Drawing.Point(101, 20);
            this.cbox_Building_Layer.Name = "cbox_Building_Layer";
            this.cbox_Building_Layer.Size = new System.Drawing.Size(248, 20);
            this.cbox_Building_Layer.TabIndex = 11;
            // 
            // bt_Cancel
            // 
            this.bt_Cancel.Location = new System.Drawing.Point(307, 400);
            this.bt_Cancel.Name = "bt_Cancel";
            this.bt_Cancel.Size = new System.Drawing.Size(60, 23);
            this.bt_Cancel.TabIndex = 19;
            this.bt_Cancel.Text = "取消";
            this.bt_Cancel.UseVisualStyleBackColor = true;
            this.bt_Cancel.Click += new System.EventHandler(this.bt_Cancel_Click);
            // 
            // bt_Run
            // 
            this.bt_Run.Location = new System.Drawing.Point(241, 400);
            this.bt_Run.Name = "bt_Run";
            this.bt_Run.Size = new System.Drawing.Size(60, 23);
            this.bt_Run.TabIndex = 20;
            this.bt_Run.Text = "计算";
            this.bt_Run.UseVisualStyleBackColor = true;
            this.bt_Run.Click += new System.EventHandler(this.bt_Run_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbox_Name);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.tbox_OutPutPath);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.bt_AddPath);
            this.groupBox1.Location = new System.Drawing.Point(12, 320);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 74);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据输出";
            // 
            // tbox_Name
            // 
            this.tbox_Name.Location = new System.Drawing.Point(101, 47);
            this.tbox_Name.Name = "tbox_Name";
            this.tbox_Name.Size = new System.Drawing.Size(248, 21);
            this.tbox_Name.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 10;
            this.label10.Text = "输出文件名称：";
            // 
            // tbox_OutPutPath
            // 
            this.tbox_OutPutPath.Location = new System.Drawing.Point(101, 20);
            this.tbox_OutPutPath.Name = "tbox_OutPutPath";
            this.tbox_OutPutPath.Size = new System.Drawing.Size(198, 21);
            this.tbox_OutPutPath.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 12);
            this.label9.TabIndex = 10;
            this.label9.Text = "数据输出路径：";
            // 
            // bt_AddPath
            // 
            this.bt_AddPath.Location = new System.Drawing.Point(305, 21);
            this.bt_AddPath.Name = "bt_AddPath";
            this.bt_AddPath.Size = new System.Drawing.Size(44, 20);
            this.bt_AddPath.TabIndex = 13;
            this.bt_AddPath.Text = "选择";
            this.bt_AddPath.UseVisualStyleBackColor = true;
            this.bt_AddPath.Click += new System.EventHandler(this.bt_AddPath_Click);
            // 
            // lab_Message
            // 
            this.lab_Message.AutoSize = true;
            this.lab_Message.Location = new System.Drawing.Point(10, 405);
            this.lab_Message.Name = "lab_Message";
            this.lab_Message.Size = new System.Drawing.Size(125, 12);
            this.lab_Message.TabIndex = 25;
            this.lab_Message.Text = "••••数据初始化中••••";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 429);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(355, 23);
            this.progressBar1.TabIndex = 26;
            // 
            // PopulationSpatialFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 462);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lab_Message);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.bt_Cancel);
            this.Controls.Add(this.bt_Run);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PopulationSpatialFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "人口数据格网化";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PopulationSpatialFrm_FormClosed);
            this.Load += new System.EventHandler(this.PopulationSpatialFrm_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbox_Grid_Field;
        private System.Windows.Forms.ComboBox cbox_Grid_Layer;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button bt_LoadPopData;
        private System.Windows.Forms.ComboBox cbox_Boundary_Layer;
        private System.Windows.Forms.TextBox tbox_Population;
        private System.Windows.Forms.ComboBox cbox_Zone_Field;
        private System.Windows.Forms.ComboBox cbox_Pop_Field;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbox_Building_Layer;
        private System.Windows.Forms.Button bt_Cancel;
        private System.Windows.Forms.Button bt_Run;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbox_OutPutPath;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button bt_AddPath;
        private System.Windows.Forms.Label lab_Message;
        private System.Windows.Forms.TextBox tbox_Name;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbox_Density;
        private System.Windows.Forms.ComboBox cbox_Height;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox cbox_Optional;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}