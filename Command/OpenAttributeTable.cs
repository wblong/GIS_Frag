using System;
using System.Drawing;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;


using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;


using AE_Environment.Forms;

namespace AE_Environment.Command
{
    /// <summary>
    /// Command that works in ArcMap/Map/PageLayout
    /// </summary>
    [Guid("7e00706a-5ad1-4d32-9b67-0e65d7a13a49")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("AE_Environment.BaseCommand.OpenAttributeTable")]
    public sealed class OpenAttributeTable : BaseCommand
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);
            ControlsCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);
            ControlsCommands.Unregister(regKey);
        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper = null;
        private ILayer m_pLayer;
        private IMapControl3 mMapControl = null;
        public OpenAttributeTable(ILayer pLayer)
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text
            base.m_caption = "�����Ա�"; //localizable text
            base.m_message = "�����Ա�"; //localizable text
            base.m_toolTip = "�����Ա�"; //localizable text
            base.m_name = "�����Ա�"; //unique id, non-localizable (e.g. "MyCategory_MyCommand")
            m_pLayer = pLayer;
            try
            {
                //
                // TODO: change bitmap name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                mMapControl = hook as IMapControl3;
                if (m_hookHelper.ActiveView == null)
                    m_hookHelper = null;
            }
            catch
            {
                m_hookHelper = null;
            }

            if (m_hookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            // TODO: Add OpenAttributeTable.OnClick implementation

//             AttributeTableFrm attributeTable = new AttributeTableFrm();
//             attributeTable.CreateAttributeTable(m_pLayer);
//             attributeTable.ShowDialog();
            frmTable ft = new frmTable(m_pLayer, mMapControl);
            ft.ShowDialog();
            
        }

        #endregion
    }
}
