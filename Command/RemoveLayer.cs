using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace AE_Environment.Command
{
     
    public sealed class RemoveLayer : BaseCommand
    {
        private IMapControl3 m_mapControl;

        public RemoveLayer()
        {
            base.m_caption = "ÒÆ³ýÍ¼²ã";
        }

        public override void OnClick()
        {
            ILayer layer = (ILayer)m_mapControl.CustomProperty;
            m_mapControl.Map.DeleteLayer(layer);
            
        }

        public override void OnCreate(object hook)
        {
            m_mapControl = (IMapControl3)hook;
        }
    }
}
