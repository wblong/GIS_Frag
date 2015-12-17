namespace AE_Environment.Forms
{
    partial class VegetationInterfereIndexFrm
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbox_Unit_Layer = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbox_Line_Layer = new System.Windows.Forms.ComboBox();
            this.btn_Run = new System.Windows.Forms.Button();
            this.tbox_Path = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_SelectPath = new System.Windows.Forms.Button();
            this.cbox_ZoneID_Field = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbox_Name = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "统计单元图层：";
            // 
            // cbox_Unit_Layer
            // 
            this.cbox_Unit_Layer.FormattingEnabled = true;
            this.cbox_Unit_Layer.Location = new System.Drawing.Point(14, 24);
            this.cbox_Unit_Layer.Name = "cbox_Unit_Layer";
            this.cbox_Unit_Layer.Size = new System.Drawing.Size(335, 20);
            this.cbox_Unit_Layer.TabIndex = 1;
            this.cbox_Unit_Layer.SelectedIndexChanged += new System.EventHandler(this.cbox_Unit_Layer_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "廊道图层：";
            // 
            // cbox_Line_Layer
            // 
            this.cbox_Line_Layer.FormattingEnabled = true;
            this.cbox_Line_Layer.Location = new System.Drawing.Point(14, 106);
            this.cbox_Line_Layer.Name = "cbox_Line_Layer";
            this.cbox_Line_Layer.Size = new System.Drawing.Size(335, 20);
            this.cbox_Line_Layer.TabIndex = 1;
            // 
            // btn_Run
            // 
            this.btn_Run.Location = new System.Drawing.Point(227, 216);
            this.btn_Run.Name = "btn_Run";
            this.btn_Run.Size = new System.Drawing.Size(58, 23);
            this.btn_Run.TabIndex = 2;
            this.btn_Run.Text = "计算";
            this.btn_Run.UseVisualStyleBackColor = true;
            this.btn_Run.Click += new System.EventHandler(this.btn_Run_Click);
            // 
            // tbox_Path
            // 
            this.tbox_Path.Location = new System.Drawing.Point(14, 147);
            this.tbox_Path.Name = "tbox_Path";
            this.tbox_Path.Size = new System.Drawing.Size(281, 21);
            this.tbox_Path.TabIndex = 3;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(14, 216);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(207, 23);
            this.progressBar1.TabIndex = 4;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(291, 216);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(58, 23);
            this.btn_cancel.TabIndex = 2;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_SelectPath
            // 
            this.btn_SelectPath.Location = new System.Drawing.Point(301, 146);
            this.btn_SelectPath.Name = "btn_SelectPath";
            this.btn_SelectPath.Size = new System.Drawing.Size(48, 21);
            this.btn_SelectPath.TabIndex = 2;
            this.btn_SelectPath.Text = "打开";
            this.btn_SelectPath.UseVisualStyleBackColor = true;
            this.btn_SelectPath.Click += new System.EventHandler(this.btn_SelectPath_Click);
            // 
            // cbox_ZoneID_Field
            // 
            this.cbox_ZoneID_Field.FormattingEnabled = true;
            this.cbox_ZoneID_Field.Location = new System.Drawing.Point(14, 65);
            this.cbox_ZoneID_Field.Name = "cbox_ZoneID_Field";
            this.cbox_ZoneID_Field.Size = new System.Drawing.Size(335, 20);
            this.cbox_ZoneID_Field.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "分区编码字段：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "保存路径：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "文件名称：";
            // 
            // tbox_Name
            // 
            this.tbox_Name.Location = new System.Drawing.Point(14, 189);
            this.tbox_Name.Name = "tbox_Name";
            this.tbox_Name.Size = new System.Drawing.Size(335, 21);
            this.tbox_Name.TabIndex = 3;
            // 
            // VegetationInterfereIndexFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 252);
            this.Controls.Add(this.cbox_ZoneID_Field);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.tbox_Name);
            this.Controls.Add(this.tbox_Path);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_SelectPath);
            this.Controls.Add(this.btn_Run);
            this.Controls.Add(this.cbox_Line_Layer);
            this.Controls.Add(this.cbox_Unit_Layer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VegetationInterfereIndexFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "植被受干扰指数";
            this.Load += new System.EventHandler(this.VegetationInterfereIndexFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbox_Unit_Layer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbox_Line_Layer;
        private System.Windows.Forms.Button btn_Run;
        private System.Windows.Forms.TextBox tbox_Path;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_SelectPath;
        private System.Windows.Forms.ComboBox cbox_ZoneID_Field;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbox_Name;
    }
}