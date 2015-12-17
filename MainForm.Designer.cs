namespace AE_Environment
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.IDM_FILE = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_DATA = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_LodeData = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_DataInput = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_Analysis = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_ClassIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_Landscape = new System.Windows.Forms.ToolStripMenuItem();
            this.生态环境状况评价指标国标ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自定义指标ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_Tool = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_Intersect = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_Dissolve = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_UpdateAreaPrim = new System.Windows.Forms.ToolStripMenuItem();
            this.人口空间化ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_Result = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_Result_Show = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_Table_Show = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_Result_Out = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_Map_Out = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_About = new System.Windows.Forms.ToolStripMenuItem();
            this.IDM_HelpContent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tool_Open = new System.Windows.Forms.ToolStripButton();
            this.tool_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tool_AddData = new System.Windows.Forms.ToolStripButton();
            this.tool_DataInput = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tool_ClassIndex = new System.Windows.Forms.ToolStripButton();
            this.tool_LandIndex = new System.Windows.Forms.ToolStripButton();
            this.tool_Environment = new System.Windows.Forms.ToolStripButton();
            this.tool_Custom = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tool_Tools = new System.Windows.Forms.ToolStripDropDownButton();
            this.tool_Diss = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_Intersect = new System.Windows.Forms.ToolStripMenuItem();
            this.更新添加面积周长ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.人口空间化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_Result_Table = new System.Windows.Forms.ToolStripButton();
            this.tool_OutFeature = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripBarXY = new System.Windows.Forms.ToolStripStatusLabel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IDM_FILE,
            this.IDM_DATA,
            this.IDM_Analysis,
            this.IDM_Tool,
            this.IDM_Result,
            this.IDM_Help});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(979, 25);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // IDM_FILE
            // 
            this.IDM_FILE.BackColor = System.Drawing.SystemColors.Control;
            this.IDM_FILE.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IDM_Open,
            this.IDM_Save,
            this.IDM_SaveAs,
            this.IDM_Exit});
            this.IDM_FILE.Name = "IDM_FILE";
            this.IDM_FILE.Size = new System.Drawing.Size(58, 21);
            this.IDM_FILE.Text = "文件(&F)";
            // 
            // IDM_Open
            // 
            this.IDM_Open.Image = ((System.Drawing.Image)(resources.GetObject("IDM_Open.Image")));
            this.IDM_Open.Name = "IDM_Open";
            this.IDM_Open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.IDM_Open.Size = new System.Drawing.Size(147, 22);
            this.IDM_Open.Text = "打开";
            this.IDM_Open.Click += new System.EventHandler(this.IDM_Open_Click);
            // 
            // IDM_Save
            // 
            this.IDM_Save.Image = ((System.Drawing.Image)(resources.GetObject("IDM_Save.Image")));
            this.IDM_Save.Name = "IDM_Save";
            this.IDM_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.IDM_Save.Size = new System.Drawing.Size(147, 22);
            this.IDM_Save.Text = "保存";
            this.IDM_Save.Click += new System.EventHandler(this.IDM_Save_Click);
            // 
            // IDM_SaveAs
            // 
            this.IDM_SaveAs.Name = "IDM_SaveAs";
            this.IDM_SaveAs.Size = new System.Drawing.Size(147, 22);
            this.IDM_SaveAs.Text = "另存为";
            this.IDM_SaveAs.Click += new System.EventHandler(this.IDM_SaveAs_Click);
            // 
            // IDM_Exit
            // 
            this.IDM_Exit.Image = ((System.Drawing.Image)(resources.GetObject("IDM_Exit.Image")));
            this.IDM_Exit.Name = "IDM_Exit";
            this.IDM_Exit.Size = new System.Drawing.Size(147, 22);
            this.IDM_Exit.Text = "退出";
            this.IDM_Exit.Click += new System.EventHandler(this.IDM_Exit_Click);
            // 
            // IDM_DATA
            // 
            this.IDM_DATA.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IDM_LodeData,
            this.IDM_DataInput});
            this.IDM_DATA.Name = "IDM_DATA";
            this.IDM_DATA.Size = new System.Drawing.Size(61, 21);
            this.IDM_DATA.Text = "数据(&D)";
            // 
            // IDM_LodeData
            // 
            this.IDM_LodeData.Image = ((System.Drawing.Image)(resources.GetObject("IDM_LodeData.Image")));
            this.IDM_LodeData.Name = "IDM_LodeData";
            this.IDM_LodeData.Size = new System.Drawing.Size(124, 22);
            this.IDM_LodeData.Text = "加载数据";
            this.IDM_LodeData.Click += new System.EventHandler(this.IDM_LodeData_Click);
            // 
            // IDM_DataInput
            // 
            this.IDM_DataInput.Image = ((System.Drawing.Image)(resources.GetObject("IDM_DataInput.Image")));
            this.IDM_DataInput.Name = "IDM_DataInput";
            this.IDM_DataInput.Size = new System.Drawing.Size(124, 22);
            this.IDM_DataInput.Text = "数据配置";
            this.IDM_DataInput.Click += new System.EventHandler(this.IDM_DataInput_Click);
            // 
            // IDM_Analysis
            // 
            this.IDM_Analysis.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IDM_ClassIndex,
            this.IDM_Landscape,
            this.生态环境状况评价指标国标ToolStripMenuItem,
            this.自定义指标ToolStripMenuItem});
            this.IDM_Analysis.Name = "IDM_Analysis";
            this.IDM_Analysis.Size = new System.Drawing.Size(60, 21);
            this.IDM_Analysis.Text = "计算(&C)";
            // 
            // IDM_ClassIndex
            // 
            this.IDM_ClassIndex.Image = ((System.Drawing.Image)(resources.GetObject("IDM_ClassIndex.Image")));
            this.IDM_ClassIndex.Name = "IDM_ClassIndex";
            this.IDM_ClassIndex.Size = new System.Drawing.Size(196, 22);
            this.IDM_ClassIndex.Text = "分类指标";
            this.IDM_ClassIndex.Click += new System.EventHandler(this.IDM_ClassIndex_Click);
            // 
            // IDM_Landscape
            // 
            this.IDM_Landscape.Image = ((System.Drawing.Image)(resources.GetObject("IDM_Landscape.Image")));
            this.IDM_Landscape.Name = "IDM_Landscape";
            this.IDM_Landscape.Size = new System.Drawing.Size(196, 22);
            this.IDM_Landscape.Text = "景观指标";
            this.IDM_Landscape.Click += new System.EventHandler(this.IDM_Landscape_Click);
            // 
            // 生态环境状况评价指标国标ToolStripMenuItem
            // 
            this.生态环境状况评价指标国标ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("生态环境状况评价指标国标ToolStripMenuItem.Image")));
            this.生态环境状况评价指标国标ToolStripMenuItem.Name = "生态环境状况评价指标国标ToolStripMenuItem";
            this.生态环境状况评价指标国标ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.生态环境状况评价指标国标ToolStripMenuItem.Text = "生态环境状况评价指标";
            this.生态环境状况评价指标国标ToolStripMenuItem.Click += new System.EventHandler(this.生态环境状况评价指标国标ToolStripMenuItem_Click);
            // 
            // 自定义指标ToolStripMenuItem
            // 
            this.自定义指标ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("自定义指标ToolStripMenuItem.Image")));
            this.自定义指标ToolStripMenuItem.Name = "自定义指标ToolStripMenuItem";
            this.自定义指标ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.自定义指标ToolStripMenuItem.Text = "自定义指标";
            this.自定义指标ToolStripMenuItem.Click += new System.EventHandler(this.自定义指标ToolStripMenuItem_Click);
            // 
            // IDM_Tool
            // 
            this.IDM_Tool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IDM_Intersect,
            this.IDM_Dissolve,
            this.IDM_UpdateAreaPrim,
            this.人口空间化ToolStripMenuItem1});
            this.IDM_Tool.Name = "IDM_Tool";
            this.IDM_Tool.Size = new System.Drawing.Size(59, 21);
            this.IDM_Tool.Text = "工具(&T)";
            // 
            // IDM_Intersect
            // 
            this.IDM_Intersect.Image = ((System.Drawing.Image)(resources.GetObject("IDM_Intersect.Image")));
            this.IDM_Intersect.Name = "IDM_Intersect";
            this.IDM_Intersect.Size = new System.Drawing.Size(177, 22);
            this.IDM_Intersect.Text = "相交";
            this.IDM_Intersect.Click += new System.EventHandler(this.IDM_Intersect_Click);
            // 
            // IDM_Dissolve
            // 
            this.IDM_Dissolve.Image = ((System.Drawing.Image)(resources.GetObject("IDM_Dissolve.Image")));
            this.IDM_Dissolve.Name = "IDM_Dissolve";
            this.IDM_Dissolve.Size = new System.Drawing.Size(177, 22);
            this.IDM_Dissolve.Text = "融合";
            this.IDM_Dissolve.Click += new System.EventHandler(this.IDM_Dissolve_Click);
            // 
            // IDM_UpdateAreaPrim
            // 
            this.IDM_UpdateAreaPrim.Image = ((System.Drawing.Image)(resources.GetObject("IDM_UpdateAreaPrim.Image")));
            this.IDM_UpdateAreaPrim.Name = "IDM_UpdateAreaPrim";
            this.IDM_UpdateAreaPrim.Size = new System.Drawing.Size(177, 22);
            this.IDM_UpdateAreaPrim.Text = "更新/添加面积周长";
            this.IDM_UpdateAreaPrim.Click += new System.EventHandler(this.IDM_UpdateAreaPrim_Click);
            // 
            // 人口空间化ToolStripMenuItem1
            // 
            this.人口空间化ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("人口空间化ToolStripMenuItem1.Image")));
            this.人口空间化ToolStripMenuItem1.Name = "人口空间化ToolStripMenuItem1";
            this.人口空间化ToolStripMenuItem1.Size = new System.Drawing.Size(177, 22);
            this.人口空间化ToolStripMenuItem1.Text = "人口空间化";
            this.人口空间化ToolStripMenuItem1.Click += new System.EventHandler(this.人口空间化ToolStripMenuItem_Click);
            // 
            // IDM_Result
            // 
            this.IDM_Result.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IDM_Result_Show,
            this.IDM_Result_Out});
            this.IDM_Result.Name = "IDM_Result";
            this.IDM_Result.Size = new System.Drawing.Size(60, 21);
            this.IDM_Result.Text = "结果(&R)";
            // 
            // IDM_Result_Show
            // 
            this.IDM_Result_Show.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IDM_Table_Show});
            this.IDM_Result_Show.Name = "IDM_Result_Show";
            this.IDM_Result_Show.Size = new System.Drawing.Size(100, 22);
            this.IDM_Result_Show.Text = "显示";
            // 
            // IDM_Table_Show
            // 
            this.IDM_Table_Show.Image = ((System.Drawing.Image)(resources.GetObject("IDM_Table_Show.Image")));
            this.IDM_Table_Show.Name = "IDM_Table_Show";
            this.IDM_Table_Show.Size = new System.Drawing.Size(100, 22);
            this.IDM_Table_Show.Text = "表格";
            this.IDM_Table_Show.Click += new System.EventHandler(this.IDM_Table_Show_Click);
            // 
            // IDM_Result_Out
            // 
            this.IDM_Result_Out.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IDM_Map_Out});
            this.IDM_Result_Out.Name = "IDM_Result_Out";
            this.IDM_Result_Out.Size = new System.Drawing.Size(100, 22);
            this.IDM_Result_Out.Text = "输出";
            // 
            // IDM_Map_Out
            // 
            this.IDM_Map_Out.Image = ((System.Drawing.Image)(resources.GetObject("IDM_Map_Out.Image")));
            this.IDM_Map_Out.Name = "IDM_Map_Out";
            this.IDM_Map_Out.Size = new System.Drawing.Size(100, 22);
            this.IDM_Map_Out.Text = "要素";
            this.IDM_Map_Out.Click += new System.EventHandler(this.IDM_Map_Out_Click);
            // 
            // IDM_Help
            // 
            this.IDM_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.IDM_About,
            this.IDM_HelpContent});
            this.IDM_Help.Name = "IDM_Help";
            this.IDM_Help.Size = new System.Drawing.Size(61, 21);
            this.IDM_Help.Text = "帮助(&H)";
            // 
            // IDM_About
            // 
            this.IDM_About.Name = "IDM_About";
            this.IDM_About.Size = new System.Drawing.Size(124, 22);
            this.IDM_About.Text = "关于(&A)";
            this.IDM_About.Click += new System.EventHandler(this.IDM_About_Click);
            // 
            // IDM_HelpContent
            // 
            this.IDM_HelpContent.Image = ((System.Drawing.Image)(resources.GetObject("IDM_HelpContent.Image")));
            this.IDM_HelpContent.Name = "IDM_HelpContent";
            this.IDM_HelpContent.Size = new System.Drawing.Size(124, 22);
            this.IDM_HelpContent.Text = "查看帮助";
            this.IDM_HelpContent.Click += new System.EventHandler(this.IDM_HelpContent_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_Open,
            this.tool_Save,
            this.toolStripSeparator2,
            this.tool_AddData,
            this.tool_DataInput,
            this.toolStripSeparator1,
            this.tool_ClassIndex,
            this.tool_LandIndex,
            this.tool_Environment,
            this.tool_Custom,
            this.toolStripSeparator3,
            this.tool_Tools,
            this.tool_Result_Table,
            this.tool_OutFeature});
            this.toolStrip.Location = new System.Drawing.Point(0, 25);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(979, 25);
            this.toolStrip.TabIndex = 1;
            // 
            // tool_Open
            // 
            this.tool_Open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_Open.Image = ((System.Drawing.Image)(resources.GetObject("tool_Open.Image")));
            this.tool_Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_Open.Name = "tool_Open";
            this.tool_Open.Size = new System.Drawing.Size(23, 22);
            this.tool_Open.ToolTipText = "打开";
            this.tool_Open.Click += new System.EventHandler(this.IDM_Open_Click);
            // 
            // tool_Save
            // 
            this.tool_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_Save.Image = ((System.Drawing.Image)(resources.GetObject("tool_Save.Image")));
            this.tool_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_Save.Name = "tool_Save";
            this.tool_Save.Size = new System.Drawing.Size(23, 22);
            this.tool_Save.ToolTipText = "保存";
            this.tool_Save.Click += new System.EventHandler(this.IDM_Save_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tool_AddData
            // 
            this.tool_AddData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_AddData.Image = ((System.Drawing.Image)(resources.GetObject("tool_AddData.Image")));
            this.tool_AddData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_AddData.Name = "tool_AddData";
            this.tool_AddData.Size = new System.Drawing.Size(23, 22);
            this.tool_AddData.ToolTipText = "加载数据";
            this.tool_AddData.Click += new System.EventHandler(this.IDM_LodeData_Click);
            // 
            // tool_DataInput
            // 
            this.tool_DataInput.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_DataInput.Image = ((System.Drawing.Image)(resources.GetObject("tool_DataInput.Image")));
            this.tool_DataInput.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_DataInput.Name = "tool_DataInput";
            this.tool_DataInput.Size = new System.Drawing.Size(23, 22);
            this.tool_DataInput.ToolTipText = "数据配置";
            this.tool_DataInput.Click += new System.EventHandler(this.IDM_DataInput_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tool_ClassIndex
            // 
            this.tool_ClassIndex.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_ClassIndex.Image = ((System.Drawing.Image)(resources.GetObject("tool_ClassIndex.Image")));
            this.tool_ClassIndex.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_ClassIndex.Name = "tool_ClassIndex";
            this.tool_ClassIndex.Size = new System.Drawing.Size(23, 22);
            this.tool_ClassIndex.ToolTipText = "分类统计";
            this.tool_ClassIndex.Click += new System.EventHandler(this.IDM_ClassIndex_Click);
            // 
            // tool_LandIndex
            // 
            this.tool_LandIndex.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_LandIndex.Image = ((System.Drawing.Image)(resources.GetObject("tool_LandIndex.Image")));
            this.tool_LandIndex.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_LandIndex.Name = "tool_LandIndex";
            this.tool_LandIndex.Size = new System.Drawing.Size(23, 22);
            this.tool_LandIndex.ToolTipText = "景观统计";
            this.tool_LandIndex.Click += new System.EventHandler(this.IDM_Landscape_Click);
            // 
            // tool_Environment
            // 
            this.tool_Environment.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_Environment.Image = ((System.Drawing.Image)(resources.GetObject("tool_Environment.Image")));
            this.tool_Environment.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_Environment.Name = "tool_Environment";
            this.tool_Environment.Size = new System.Drawing.Size(23, 22);
            this.tool_Environment.ToolTipText = "生态环境评价指标";
            this.tool_Environment.Click += new System.EventHandler(this.生态环境状况评价指标国标ToolStripMenuItem_Click);
            // 
            // tool_Custom
            // 
            this.tool_Custom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_Custom.Image = ((System.Drawing.Image)(resources.GetObject("tool_Custom.Image")));
            this.tool_Custom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_Custom.Name = "tool_Custom";
            this.tool_Custom.Size = new System.Drawing.Size(23, 22);
            this.tool_Custom.ToolTipText = "自定义指标";
            this.tool_Custom.Click += new System.EventHandler(this.自定义指标ToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tool_Tools
            // 
            this.tool_Tools.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_Diss,
            this.tool_Intersect,
            this.更新添加面积周长ToolStripMenuItem,
            this.人口空间化ToolStripMenuItem});
            this.tool_Tools.Image = ((System.Drawing.Image)(resources.GetObject("tool_Tools.Image")));
            this.tool_Tools.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_Tools.Name = "tool_Tools";
            this.tool_Tools.Size = new System.Drawing.Size(29, 22);
            this.tool_Tools.ToolTipText = "工具";
            // 
            // tool_Diss
            // 
            this.tool_Diss.Image = ((System.Drawing.Image)(resources.GetObject("tool_Diss.Image")));
            this.tool_Diss.Name = "tool_Diss";
            this.tool_Diss.Size = new System.Drawing.Size(177, 22);
            this.tool_Diss.Text = "融合";
            this.tool_Diss.Click += new System.EventHandler(this.IDM_Dissolve_Click);
            // 
            // tool_Intersect
            // 
            this.tool_Intersect.Image = ((System.Drawing.Image)(resources.GetObject("tool_Intersect.Image")));
            this.tool_Intersect.Name = "tool_Intersect";
            this.tool_Intersect.Size = new System.Drawing.Size(177, 22);
            this.tool_Intersect.Text = "相交";
            this.tool_Intersect.Click += new System.EventHandler(this.IDM_Intersect_Click);
            // 
            // 更新添加面积周长ToolStripMenuItem
            // 
            this.更新添加面积周长ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("更新添加面积周长ToolStripMenuItem.Image")));
            this.更新添加面积周长ToolStripMenuItem.Name = "更新添加面积周长ToolStripMenuItem";
            this.更新添加面积周长ToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.更新添加面积周长ToolStripMenuItem.Text = "更新/添加面积周长";
            this.更新添加面积周长ToolStripMenuItem.Click += new System.EventHandler(this.IDM_UpdateAreaPrim_Click);
            // 
            // 人口空间化ToolStripMenuItem
            // 
            this.人口空间化ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("人口空间化ToolStripMenuItem.Image")));
            this.人口空间化ToolStripMenuItem.Name = "人口空间化ToolStripMenuItem";
            this.人口空间化ToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.人口空间化ToolStripMenuItem.Text = "人口空间化";
            this.人口空间化ToolStripMenuItem.Click += new System.EventHandler(this.人口空间化ToolStripMenuItem_Click);
            // 
            // tool_Result_Table
            // 
            this.tool_Result_Table.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_Result_Table.Image = ((System.Drawing.Image)(resources.GetObject("tool_Result_Table.Image")));
            this.tool_Result_Table.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_Result_Table.Name = "tool_Result_Table";
            this.tool_Result_Table.Size = new System.Drawing.Size(23, 22);
            this.tool_Result_Table.ToolTipText = "显示表格";
            this.tool_Result_Table.Click += new System.EventHandler(this.IDM_Table_Show_Click);
            // 
            // tool_OutFeature
            // 
            this.tool_OutFeature.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_OutFeature.Image = ((System.Drawing.Image)(resources.GetObject("tool_OutFeature.Image")));
            this.tool_OutFeature.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_OutFeature.Name = "tool_OutFeature";
            this.tool_OutFeature.Size = new System.Drawing.Size(23, 22);
            this.tool_OutFeature.ToolTipText = "输出要素";
            this.tool_OutFeature.Click += new System.EventHandler(this.IDM_Map_Out_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBarXY});
            this.statusStrip.Location = new System.Drawing.Point(0, 600);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(979, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripBarXY
            // 
            this.toolStripBarXY.Name = "toolStripBarXY";
            this.toolStripBarXY.Size = new System.Drawing.Size(0, 17);
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.axToolbarControl1.Location = new System.Drawing.Point(0, 50);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(979, 28);
            this.axToolbarControl1.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 78);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.axTOCControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl);
            this.splitContainer1.Size = new System.Drawing.Size(979, 522);
            this.splitContainer1.SplitterDistance = 183;
            this.splitContainer1.TabIndex = 5;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axTOCControl1.Location = new System.Drawing.Point(0, 0);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(183, 522);
            this.axTOCControl1.TabIndex = 1;
            this.axTOCControl1.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.axTOCControl1_OnMouseDown);
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(792, 522);
            this.tabControl.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.axLicenseControl1);
            this.tabPage1.Controls.Add(this.axMapControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(784, 496);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据视图";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(389, 183);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 1;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(3, 3);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(778, 490);
            this.axMapControl1.TabIndex = 0;
            this.axMapControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseDownEventHandler(this.axMapControl1_OnMouseDown);
            this.axMapControl1.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl1_OnMouseMove);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.axPageLayoutControl1);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(784, 496);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "布局视图";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // axPageLayoutControl1
            // 
            this.axPageLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axPageLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
            this.axPageLayoutControl1.Size = new System.Drawing.Size(784, 496);
            this.axPageLayoutControl1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 622);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "     ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem IDM_FILE;
        private System.Windows.Forms.ToolStripMenuItem IDM_Open;
        private System.Windows.Forms.ToolStripMenuItem IDM_Save;
        private System.Windows.Forms.ToolStripMenuItem IDM_SaveAs;
        private System.Windows.Forms.ToolStripMenuItem IDM_Analysis;
        private System.Windows.Forms.ToolStripMenuItem IDM_Help;
        private System.Windows.Forms.ToolStripMenuItem IDM_Exit;
        private System.Windows.Forms.ToolStripMenuItem IDM_About;
        private System.Windows.Forms.ToolStripMenuItem IDM_ClassIndex;
        private System.Windows.Forms.ToolStripMenuItem IDM_Landscape;
        private System.Windows.Forms.ToolStripMenuItem IDM_Result;
        private System.Windows.Forms.ToolStripMenuItem IDM_Result_Show;
        private System.Windows.Forms.ToolStripMenuItem IDM_Result_Out;
        private System.Windows.Forms.ToolStripMenuItem IDM_Table_Show;
        private System.Windows.Forms.ToolStripMenuItem IDM_Map_Out;
        private System.Windows.Forms.ToolStripButton tool_AddData;
        private System.Windows.Forms.ToolStripButton tool_Result_Table;
        private System.Windows.Forms.ToolStripButton tool_DataInput;
        private System.Windows.Forms.ToolStripButton tool_ClassIndex;
        private System.Windows.Forms.ToolStripButton tool_LandIndex;
        private System.Windows.Forms.ToolStripStatusLabel toolStripBarXY;
        private System.Windows.Forms.ToolStripMenuItem IDM_Tool;
        private System.Windows.Forms.ToolStripMenuItem IDM_Intersect;
        private System.Windows.Forms.ToolStripMenuItem IDM_UpdateAreaPrim;
        private System.Windows.Forms.ToolStripMenuItem IDM_Dissolve;
        private System.Windows.Forms.ToolStripMenuItem IDM_DATA;
        private System.Windows.Forms.ToolStripMenuItem IDM_LodeData;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem IDM_DataInput;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tool_Open;
        private System.Windows.Forms.ToolStripButton tool_Save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton tool_Tools;
        private System.Windows.Forms.ToolStripMenuItem tool_Diss;
        private System.Windows.Forms.ToolStripMenuItem tool_Intersect;
        private System.Windows.Forms.ToolStripButton tool_OutFeature;
        private System.Windows.Forms.ToolStripButton tool_Environment;
        private System.Windows.Forms.ToolStripMenuItem 生态环境状况评价指标国标ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IDM_HelpContent;
        private System.Windows.Forms.ToolStripMenuItem 自定义指标ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tool_Custom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 更新添加面积周长ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 人口空间化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 人口空间化ToolStripMenuItem1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
         
    }
}