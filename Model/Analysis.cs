using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using System.Windows.Forms;

namespace AE_Environment.Model
{
  abstract  class Analysis
    {
        public string name=null;//名称
        public DataTable result_dt = null;//结果表
        public BaseData baseData = null;  //输入基本数据

        public Analysis() { }

        public void SetBaseData(BaseData _baseData)
        {

            this.baseData = _baseData;
        }
      /// <summary>
      /// 构建结果表
      /// </summary>
      /// <returns></returns>
        public abstract bool BuildResultTable();
        /// <summary>
        /// 指标计算
        /// </summary>
        public abstract bool CalculateIndex(ProgressBar progress);
       
        /// <summary>
        /// 清楚结果表
        /// </summary>
        public void ClearResultTable()
        {

            if (result_dt!=null)
            {
                result_dt.Clear();
            }
        }
       
    }
}
