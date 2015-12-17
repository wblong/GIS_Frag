namespace AE_Environment.Forms
{
    partial class frmDataConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataConfig));
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboxlayer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboxFiledCC = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboZone = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboxZonelayer = new System.Windows.Forms.ComboBox();
            this.ButCreate = new System.Windows.Forms.Button();
            this.ButCancel = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboxlayer);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboxFiledCC);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.comboZone);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboxZonelayer);
            this.panel1.Controls.Add(this.ButCreate);
            this.panel1.Controls.Add(this.ButCancel);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(354, 151);
            this.panel1.TabIndex = 3;
            // 
            // comboxlayer
            // 
            this.comboxlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboxlayer.FormattingEnabled = true;
            this.comboxlayer.Location = new System.Drawing.Point(83, 12);
            this.comboxlayer.Name = "comboxlayer";
            this.comboxlayer.Size = new System.Drawing.Size(256, 20);
            this.comboxlayer.TabIndex = 1;
            this.comboxlayer.SelectedIndexChanged += new System.EventHandler(this.comboxlayer_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "覆盖图层：";
            // 
            // comboxFiledCC
            // 
            this.comboxFiledCC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboxFiledCC.FormattingEnabled = true;
            this.comboxFiledCC.Location = new System.Drawing.Point(83, 38);
            this.comboxFiledCC.Name = "comboxFiledCC";
            this.comboxFiledCC.Size = new System.Drawing.Size(256, 20);
            this.comboxFiledCC.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "编码字段：";
            // 
            // comboZone
            // 
            this.comboZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboZone.FormattingEnabled = true;
            this.comboZone.Items.AddRange(new object[] {
            "一级类别",
            "二级类别"});
            this.comboZone.Location = new System.Drawing.Point(83, 64);
            this.comboZone.Name = "comboZone";
            this.comboZone.Size = new System.Drawing.Size(256, 20);
            this.comboZone.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(12, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "分区字段：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(12, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "分区图层：";
            // 
            // comboxZonelayer
            // 
            this.comboxZonelayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboxZonelayer.FormattingEnabled = true;
            this.comboxZonelayer.Location = new System.Drawing.Point(83, 90);
            this.comboxZonelayer.Name = "comboxZonelayer";
            this.comboxZonelayer.Size = new System.Drawing.Size(256, 20);
            this.comboxZonelayer.TabIndex = 3;
            this.comboxZonelayer.SelectedIndexChanged += new System.EventHandler(this.comboxZonelayer_SelectedIndexChanged);
            // 
            // ButCreate
            // 
            this.ButCreate.Location = new System.Drawing.Point(217, 116);
            this.ButCreate.Name = "ButCreate";
            this.ButCreate.Size = new System.Drawing.Size(58, 23);
            this.ButCreate.TabIndex = 9;
            this.ButCreate.Text = "确定";
            this.ButCreate.UseVisualStyleBackColor = true;
            this.ButCreate.Click += new System.EventHandler(this.ButOK_Click);
            // 
            // ButCancel
            // 
            this.ButCancel.Enabled = false;
            this.ButCancel.Location = new System.Drawing.Point(281, 116);
            this.ButCancel.Name = "ButCancel";
            this.ButCancel.Size = new System.Drawing.Size(58, 23);
            this.ButCancel.TabIndex = 10;
            this.ButCancel.Text = "取消";
            this.ButCancel.UseVisualStyleBackColor = true;
            this.ButCancel.Click += new System.EventHandler(this.ButCancel_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(14, 116);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(197, 23);
            this.progressBar1.TabIndex = 11;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            // 
            // frmDataConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 151);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDataConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据配置";
            this.Load += new System.EventHandler(this.frmDataConfig_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboZone;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboxFiledCC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboxlayer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ButCreate;
        private System.Windows.Forms.Button ButCancel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ComboBox comboxZonelayer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
    }
}