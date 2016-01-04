namespace AE_Environment.Forms
{
    partial class frmFragstat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFragstat));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.combo_Frag = new System.Windows.Forms.ComboBox();
            this.combo_Stats = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.combo_LCA = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.combo_CodeField = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "破碎要素：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "统计单元：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(111, 244);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "计算";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // combo_Frag
            // 
            this.combo_Frag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_Frag.FormattingEnabled = true;
            this.combo_Frag.Location = new System.Drawing.Point(88, 24);
            this.combo_Frag.Name = "combo_Frag";
            this.combo_Frag.Size = new System.Drawing.Size(178, 20);
            this.combo_Frag.TabIndex = 5;
            // 
            // combo_Stats
            // 
            this.combo_Stats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_Stats.FormattingEnabled = true;
            this.combo_Stats.Location = new System.Drawing.Point(88, 49);
            this.combo_Stats.Name = "combo_Stats";
            this.combo_Stats.Size = new System.Drawing.Size(178, 20);
            this.combo_Stats.TabIndex = 5;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(3, 273);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(315, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "LCA：";
            // 
            // combo_LCA
            // 
            this.combo_LCA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_LCA.FormattingEnabled = true;
            this.combo_LCA.Location = new System.Drawing.Point(70, 20);
            this.combo_LCA.Name = "combo_LCA";
            this.combo_LCA.Size = new System.Drawing.Size(178, 20);
            this.combo_LCA.TabIndex = 5;
            this.combo_LCA.SelectedIndexChanged += new System.EventHandler(this.combo_LCA_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.combo_CodeField);
            this.groupBox1.Controls.Add(this.combo_LCA);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(18, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 126);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "类别";
            // 
            // combo_CodeField
            // 
            this.combo_CodeField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_CodeField.FormattingEnabled = true;
            this.combo_CodeField.Location = new System.Drawing.Point(70, 52);
            this.combo_CodeField.Name = "combo_CodeField";
            this.combo_CodeField.Size = new System.Drawing.Size(178, 20);
            this.combo_CodeField.TabIndex = 5;
            //this.combo_CodeField.SelectedIndexChanged += new System.EventHandler(this.combo_CodeField_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(-2, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "分区字段：";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(18, 75);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(96, 16);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "是否计算类别";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // frmFragstat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 296);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.combo_Stats);
            this.Controls.Add(this.combo_Frag);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFragstat";
            this.Text = "破碎化分析";
            this.Load += new System.EventHandler(this.frmFragstat_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox combo_Frag;
        private System.Windows.Forms.ComboBox combo_Stats;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox combo_LCA;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox combo_CodeField;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ImageList imageList1;
    }
}