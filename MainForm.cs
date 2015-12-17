using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
 

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;

//自定义
using AE_Environment.Forms;
using AE_Environment.Command;
using AE_Environment.TypesStruct;

using BaseGIS.GISForm;
using BaseGIS;
using BaseCommon;
using AE_Environment.Model;


namespace AE_Environment
{
    public partial class MainForm :Form

    {

        private DataManager mDataManger = new DataManager();
        private ControlsSynchronizer mControlsSynchronizer = null;
        private int mTabControlIndex = -1;
        //钩子
        private IHookHelper map_hookHelper = null;
        private IActiveViewEvents_Event m_MapActiveViewEvents;
        //图层快捷菜单
        private IToolbarMenu m_menuLayer;
        private IToolbarMenu m_submenuRender;

        #region 全局变量
        //相关指标及数据配置
        public static bool dataConfiguration = false;
        public static DataInputInfo dataInputInfo;
        public static BaseData baseData = null;
        public static ClassParamInfo clssParamInfo;
        public static LandscapeParamInfo landscapeParamInfo;
        public static Dictionary<string, string> pData = null;
        //相关的指标结果表
        public static DataSet ds=null;
        public static DataTable weightTable = null;
       
        public static DataTable dt_LandFrm = null;
        public static DataTable dt_class = null;
        public static DataTable dt_land = null;
        public static DataTable dt_PopSpatial = null;
        public static DataTable dt_custom = null;
        public static DataTable dt_EcosystemIndex = null;
        public static DataTable dt_Population = null;
        //地图及内容控件
        public static IMapControl3 m_mapControl;
        public static ITOCControl2 m_tocControl;

        //存储路径
        public static string outshape = System.Environment.CurrentDirectory + "\\temp";
        public static string path = "";
        #endregion

        #region 主窗体初始化
        public MainForm()
        {
            InitializeComponent();

            mControlsSynchronizer = new ControlsSynchronizer((ESRI.ArcGIS.Controls.IMapControlDefault)axMapControl1.Object, (IPageLayoutControlDefault)axPageLayoutControl1.Object);
            PublicVariable.mControlsSynchronizer = mControlsSynchronizer;
            mControlsSynchronizer.AddFrameworkControl(axTOCControl1.Object);
            mControlsSynchronizer.BindControls(true);

            //mFrmLogin.progressBar1.Value = 12;
            Application.DoEvents();
            map_hookHelper = new HookHelperClass();
            pData = new Dictionary<string, string>();
            //
            dataInputInfo.clssValue = new List<string>();
            dataInputInfo.zoneValue = new List<string>();
           // dataInputInfo.zones = new List<IFeatureCursor>();
            dataInputInfo.clssObjectIDs = new List<List<int>>();
            dataInputInfo.zoneArea = new List<double>();
            dataInputInfo.clss_pCount = new List<int>();
            //类指标信息
            clssParamInfo.clss = new List<string>();
            clssParamInfo.clssIndex = new List<ClassIndex>();
            clssParamInfo.resultTable = new DataTable();
            //景观指标信息
            landscapeParamInfo.landIndex = new List<LandscapeIndex>();
            landscapeParamInfo.clss = new List<string>();
            landscapeParamInfo.resultTable = new DataTable();
            //侦听地图的事件  
            m_MapActiveViewEvents = axMapControl1.Map as IActiveViewEvents_Event;

            //对于Map，在添加图层后触发，对于PageLayout在添加任何要素时都会触发  
            //m_MapActiveViewEvents.ItemAdded += new IActiveViewEvents_ItemAddedEventHandler(m_MapActiveViewEvents_ItemAdded);  
        }
        #endregion

        #region 窗体加载
        private void MainForm_Load(object sender, EventArgs e)
        {
            //
            m_tocControl = (ITOCControl2)axTOCControl1.Object;
            m_mapControl = (IMapControl3)axMapControl1.Object;
            //设置控件的关联
            axToolbarControl1.SetBuddyControl(axMapControl1);
            axTOCControl1.SetBuddyControl(axMapControl1);
            axTOCControl1.EnableLayerDragDrop = true;

            //设置钩子
            map_hookHelper.Hook = this.axMapControl1.Object;

            //图层快捷菜单
            m_menuLayer = new ToolbarMenuClass();
            m_menuLayer.AddItem(new RemoveLayer(), -1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
            //m_menuLayer.AddItem(new ExportToFGDB(), -1, 1, false, esriCommandStyles.esriCommandStyleTextOnly);

            m_submenuRender = new ToolbarMenuClass();
            m_submenuRender.Caption = "渲染";
            m_submenuRender.AddItem(new ClassBreakRender(), -1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
            //Set the hook of each menu
            m_menuLayer.AddSubMenu(m_submenuRender, 1, true);
            m_menuLayer.AddItem(new LayerProperty(), -1, 2, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_menuLayer.SetHook(m_mapControl);

            //加载分类码
            loadcode();

            axToolbarControl1.Enabled = true;
            //控制TOC控件中图层名称的修改
            this.axTOCControl1.LabelEdit = esriTOCControlEdit.esriTOCControlManual;
            //
        }
        #endregion

        #region 内容视图的快捷菜单
        private void axTOCControl1_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            //单击
            if (e.button != 2)
            {
                return;
            }

            esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap map = null; ILayer layer = null;
            object other = null; object index = null;

            //Determine what kind of item is selected
            m_tocControl.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);

            //Ensure the item gets selected 
            if (item == esriTOCControlItem.esriTOCControlItemMap)
                m_tocControl.SelectItem(map, null);
            else
                m_tocControl.SelectItem(layer, null);

            //Set the layer into the CustomProperty (this is used by the custom layer commands)			
            m_mapControl.CustomProperty = layer;

            //Popup the correct context menu

            if (item == esriTOCControlItem.esriTOCControlItemLayer)
            {
                //动态添加OpenAttributeTable菜单项
                m_menuLayer.AddItem(new OpenAttributeTable(layer), -1, 1, true, esriCommandStyles.esriCommandStyleTextOnly);
                m_menuLayer.AddItem(new UpdateAreaPrimeter(layer), -1, 2, true, esriCommandStyles.esriCommandStyleTextOnly);
                m_menuLayer.PopupMenu(e.x, e.y, m_tocControl.hWnd);
                //移除OpenAttributeTable菜单项，以防止重复添加
                m_menuLayer.Remove(1);
                m_menuLayer.Remove(1);
            }
        }
        #endregion

        #region 文件菜单
        //打开文档
        private void IDM_Open_Click(object sender, EventArgs e)
        {
            mDataManger.OpenMxdFile(axMapControl1);
            mControlsSynchronizer.RepalceMap(axMapControl1.Map);
            axMapControl1.Extent = axMapControl1.FullExtent;

        }
        //保存文档
        private void IDM_Save_Click(object sender, EventArgs e)
        {
            mDataManger.SaveMxdFile(axMapControl1);

        }
        //另存文档

        private void IDM_SaveAs_Click(object sender, EventArgs e)
        {

            mDataManger.SaveAsMxd(axMapControl1);

        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDM_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region 数据菜单
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDM_LodeData_Click(object sender, EventArgs e)
        {
            frmAddData addData = new frmAddData(axMapControl1,path);
            addData.ShowDialog();
            path = addData.mPath;
        }
        /// <summary>
        /// 数据配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDM_DataInput_Click(object sender, EventArgs e)
        {
            frmDataConfig fdc = new frmDataConfig(axMapControl1);
            fdc.StartPosition = FormStartPosition.CenterParent;
            fdc.ShowDialog();
        }
        #endregion

        #region 工具菜单

        /// <summary>
        /// 打开叠加求交窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDM_Intersect_Click(object sender, EventArgs e)
        {
            AE_Environment.Forms.frmIntersect frmInter = new AE_Environment.Forms.frmIntersect(axMapControl1);
            frmInter.ShowDialog();
        }
        /// <summary>
        /// 更新面积及周长
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDM_UpdateAreaPrim_Click(object sender, EventArgs e)
        {
            Forms.LayerFrm layerfrm = new Forms.LayerFrm(m_mapControl);
            layerfrm.ShowDialog();
            ICommand pCommand = new Command.UpdateAreaPrimeter(layerfrm.m_Layer);
            pCommand.OnCreate(m_mapControl.Object as IHookHelper);
            pCommand.OnClick();
            MessageBox.Show("更新面积周长！");

        }
        /// <summary>
        /// 融合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDM_Dissolve_Click(object sender, EventArgs e)
        {
            Forms.frmDissolve diss = new Forms.frmDissolve(axMapControl1);
            diss.ShowDialog();
        }
        /// <summary>
        /// 类别合并
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDM_Merge_Click(object sender, EventArgs e)
        {
            frmMerge fm = new frmMerge(axMapControl1);
            fm.ShowDialog();
        }

        /// <summary>
        /// 人口空间化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 人口空间化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PopulationSpatialFrm popuSpaFrm = new PopulationSpatialFrm(axMapControl1);
            popuSpaFrm.Show();
        }

        #endregion

        #region 分析菜单
        /// <summary>
        /// 打开类指标窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDM_ClassIndex_Click(object sender, EventArgs e)
        {
            if (dataConfiguration)
            {
                ClssFrm cf = new ClssFrm(baseData);
                cf.Show();
            }
            else
                MessageBox.Show("请先进行数据配置!", "提示");
        }
        /// <summary>
        /// 景观指标窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDM_Landscape_Click(object sender, EventArgs e)
        {
            if (dataConfiguration)
            {
                LandFrm lf = new LandFrm(m_mapControl, baseData);
                lf.Show();
            }
            else
                MessageBox.Show("请先进行数据配置!", "提示");

        }
        /// <summary>
        /// 生态环境状况评价指标国标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 生态环境状况评价指标国标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataConfiguration)
            {
                EcoSystemIndexFrm ecoSystemFrm = new EcoSystemIndexFrm(baseData);
                ecoSystemFrm.Show();
            }
            else
                MessageBox.Show("请先进行数据配置!", "提示");
        }

        /// <summary>
        /// 自定义指标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 自定义指标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataConfiguration)
            {
                CustomIndexFrm customIndexFrm = new CustomIndexFrm(baseData);
                customIndexFrm.Show();
            }
            else
                MessageBox.Show("请先进行数据配置!", "提示");

        }

        #endregion

        #region 计算菜单
        /// <summary>
        /// 表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDM_Table_Show_Click(object sender, EventArgs e)
        {
            ResultFrm rf = new ResultFrm(ds);
            rf.Show();
        }
        /// <summary>
        /// 文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDM_Table_Out_Click(object sender, EventArgs e)
        {
            if (ds == null)
            {

                ds = FragStats.Stats.CreateDataTable(null);
            }
            FragStats.Stats.DataTableToTxt(ds);
        }
        /// <summary>
        /// 要素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDM_Map_Out_Click(object sender, EventArgs e)
        {
            Outputfram outfrm = new Outputfram();
            outfrm.ShowDialog();
            path = outfrm.m_path;

        }

        #endregion

        #region 帮助菜单
        /// <summary>
        /// 关于
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDM_About_Click(object sender, EventArgs e)
        {
            Forms.About_Frm af = new Forms.About_Frm();
            af.Show();

        }
        /// <summary>
        /// 帮助内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IDM_HelpContent_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, Application.StartupPath + @"\Eco-Fragmenta.chm");
        }
        #endregion


       

        #region 主窗体状态栏设置
        /// <summary>
        /// 工具条显示坐标信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axMapControl1_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            toolStripBarXY.Text = string.Format("{0}, {1}  {2}", e.mapX.ToString("#######.##"), e.mapY.ToString("#######.##"), axMapControl1.MapUnits.ToString().Substring(4));
        }
        #endregion

        #region 窗口关闭
        /// <summary>
        /// 窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mDataManger.CloseMxd(axMapControl1);
        }
        #endregion

        #region  窗体响应时间
        /// <summary>
        /// 数据视图与布局视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex != mTabControlIndex)
            {
                if (tabControl.SelectedIndex == 1)
                {
                    axMapControl1.Visible = false;
                    PublicVariable.mControlsSynchronizer.ActivatePageLayout();
                    axToolbarControl1.Enabled = false;
                }
                else
                {
                    axMapControl1.Visible = true;
                    PublicVariable.mControlsSynchronizer.ActivateMap();
                    axToolbarControl1.Enabled = true;

                }

                mTabControlIndex = tabControl.SelectedIndex;
            }
        }

        private void axTOCControl1_OnEndLabelEdit(object sender, ITOCControlEvents_OnEndLabelEditEvent e)
        {
            if (e.newLabel == "")
            {
                MessageBox.Show("图层名不能为空");
                e.canEdit = false;
            }
        }

        private void axMapControl1_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {

        }
        #endregion
        
        #region 加载编码文件方法
        private void loadcode()
        {
            string codeFN = System.Environment.CurrentDirectory + @"\CC.config";

            try
            {
                Utilities.Common.LoadCcode(codeFN);

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("请检查并配置编码文件！");
                return;
            }

            if (pData.Count < 1)
            {
                MessageBox.Show("请检查编码文件！");
                return;
            }



        }
        #endregion
        
    }
}
