namespace AE_Environment.Forms
{
    partial class frmMerge
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMerge));
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboxlayer = new System.Windows.Forms.ComboBox();
            this.comboxFiled = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnALL = new System.Windows.Forms.Button();
            this.btnALL2 = new System.Windows.Forms.Button();
            this.comboClss1 = new System.Windows.Forms.ComboBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.comboClss2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.listView1 = new System.Windows.Forms.ListView();
            this.butcancel = new System.Windows.Forms.Button();
            this.butOk = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboxlayer);
            this.panel1.Controls.Add(this.comboxFiled);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnALL);
            this.panel1.Controls.Add(this.btnALL2);
            this.panel1.Controls.Add(this.comboClss1);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.comboClss2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btn_Delete);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Controls.Add(this.butcancel);
            this.panel1.Controls.Add(this.butOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 353);
            this.panel1.TabIndex = 2;
            // 
            // comboxlayer
            // 
            this.comboxlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboxlayer.FormattingEnabled = true;
            this.comboxlayer.Location = new System.Drawing.Point(95, 14);
            this.comboxlayer.Name = "comboxlayer";
            this.comboxlayer.Size = new System.Drawing.Size(276, 20);
            this.comboxlayer.TabIndex = 1;
            this.comboxlayer.SelectedIndexChanged += new System.EventHandler(this.comboxlayer_SelectedIndexChanged);
            // 
            // comboxFiled
            // 
            this.comboxFiled.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboxFiled.FormattingEnabled = true;
            this.comboxFiled.Location = new System.Drawing.Point(95, 40);
            this.comboxFiled.Name = "comboxFiled";
            this.comboxFiled.Size = new System.Drawing.Size(276, 20);
            this.comboxFiled.TabIndex = 2;
            this.comboxFiled.SelectedIndexChanged += new System.EventHandler(this.comboxFiled_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入图层：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(12, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "输出位置：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "编码字段：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(312, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(59, 26);
            this.button1.TabIndex = 4;
            this.button1.Text = "浏览";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(12, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 14);
            this.label6.TabIndex = 2;
            this.label6.Text = "输出图层名称：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(95, 69);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(211, 21);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(123, 98);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(248, 21);
            this.textBox2.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(12, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 14);
            this.label4.TabIndex = 4;
            this.label4.Text = "一级分类：";
            // 
            // btnALL
            // 
            this.btnALL.Location = new System.Drawing.Point(318, 125);
            this.btnALL.Name = "btnALL";
            this.btnALL.Size = new System.Drawing.Size(53, 23);
            this.btnALL.TabIndex = 22;
            this.btnALL.Text = "ALL";
            this.btnALL.UseVisualStyleBackColor = true;
            this.btnALL.Click += new System.EventHandler(this.btnALL_Click);
            // 
            // btnALL2
            // 
            this.btnALL2.Location = new System.Drawing.Point(318, 154);
            this.btnALL2.Name = "btnALL2";
            this.btnALL2.Size = new System.Drawing.Size(53, 23);
            this.btnALL2.TabIndex = 22;
            this.btnALL2.Text = "ALL";
            this.btnALL2.UseVisualStyleBackColor = true;
            this.btnALL2.Click += new System.EventHandler(this.btnALL2_Click);
            // 
            // comboClss1
            // 
            this.comboClss1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboClss1.FormattingEnabled = true;
            this.comboClss1.Location = new System.Drawing.Point(95, 126);
            this.comboClss1.Name = "comboClss1";
            this.comboClss1.Size = new System.Drawing.Size(217, 20);
            this.comboClss1.TabIndex = 5;
            this.comboClss1.SelectedIndexChanged += new System.EventHandler(this.comboClss1_SelectedIndexChanged);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(112, 287);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(65, 23);
            this.btnClear.TabIndex = 22;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // comboClss2
            // 
            this.comboClss2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboClss2.FormattingEnabled = true;
            this.comboClss2.Location = new System.Drawing.Point(95, 155);
            this.comboClss2.Name = "comboClss2";
            this.comboClss2.Size = new System.Drawing.Size(217, 20);
            this.comboClss2.TabIndex = 7;
            this.comboClss2.SelectedIndexChanged += new System.EventHandler(this.comboClss2_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(12, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 14);
            this.label5.TabIndex = 6;
            this.label5.Text = "二级分类：";
            // 
            // btn_Delete
            // 
            this.btn_Delete.Location = new System.Drawing.Point(15, 287);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(65, 23);
            this.btn_Delete.TabIndex = 16;
            this.btn_Delete.Text = "X";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 316);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(356, 26);
            this.progressBar1.TabIndex = 8;
            // 
            // listView1
            // 
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(15, 181);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(356, 100);
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // butcancel
            // 
            this.butcancel.Location = new System.Drawing.Point(306, 287);
            this.butcancel.Name = "butcancel";
            this.butcancel.Size = new System.Drawing.Size(65, 23);
            this.butcancel.TabIndex = 6;
            this.butcancel.Text = "取消";
            this.butcancel.UseVisualStyleBackColor = true;
            this.butcancel.Click += new System.EventHandler(this.butcancel_Click);
            // 
            // butOk
            // 
            this.butOk.Location = new System.Drawing.Point(209, 287);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(65, 23);
            this.butOk.TabIndex = 5;
            this.butOk.Text = "确定";
            this.butOk.UseVisualStyleBackColor = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // frmMerge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 353);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMerge";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "分类类别";
            this.Load += new System.EventHandler(this.frmMerge_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button butOk;
        private System.Windows.Forms.Button butcancel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboxFiled;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboxlayer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ComboBox comboClss1;
        private System.Windows.Forms.ComboBox comboClss2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnALL2;
        private System.Windows.Forms.Button btnALL;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox2;
    }
}