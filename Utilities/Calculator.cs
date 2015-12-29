using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Windows.Forms;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace AE_Environment
{
    class Calculator
    {
        private IMapControl3 mMapControl;

        public Calculator(IMapControl3 mapControl)
        {
            mMapControl = mapControl;
        }




        /// <summary>
        /// 根据字符提取面积周长查询条件;
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string Match_str(string str)
        {
            string temp = string.Empty;
            Regex rex = new Regex(@"^\d+$");
            if (rex.IsMatch(str))
            {
                return temp;
            }
            else
            {
                string[] sql = str.Split('\'');
                for (int i = 0; i < sql.Length; i++)
                {
                    if (rex.IsMatch(sql[i]))
                    {
                        temp = sql[i];
                    }
                }
            }
            return temp;
        }

        /// <summary>
        ///  字符输入;
        /// </summary>
        /// <param name="strInset">指定输入的字符</param>
        /// <param name="richTextBox">指定输入显示控件</param>
        public void InsertString(string strInset, RichTextBox textBox)
        {
            string sql = textBox.Text;
            int start = textBox.SelectionStart;
            int length = textBox.SelectionLength;
            string str1 = "";
            string str2 = "";
            if (start == sql.Length)
            {
                textBox.Text = textBox.Text + strInset;
            }
            else
            {
                str1 = sql.Substring(0, start);
                str2 = sql.Substring(start + length);
                textBox.Text = str1 + strInset + str2;
            }
            textBox.Focus();
            textBox.SelectionStart = start + strInset.Length;
            textBox.SelectionLength = 0;
        }

        public double[] Calculate(string sql, int count, IFeatureClass pFeatureClass)
        {
            double[] temp = new double[count];
            ArrayList array = new ArrayList();
            ArrayList result = new ArrayList();
            if (sql == "")
            {
                MessageBox.Show("请输入计算公式");
                return temp;
            }
            string[] str = sql.Split(' ');
            array = new ArrayList(str);
            if (array.Contains("(") && array.Contains(")"))
            {
                ArrayList arrayTemp = BracketRemove(array, count,pFeatureClass);
                result = new ArrayList(math(arrayTemp, count,pFeatureClass));
            }
            else
            {
                result = new ArrayList(math(array, count,pFeatureClass));
            }
            if (result.Count == 1)
            {
                return temp;
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    temp[i] = Convert.ToDouble(result[i]);
                }
            }
            return temp;
        }

        public ArrayList math(ArrayList arrayList, int count,IFeatureClass pFeatureClass)
        {
            ArrayList temp = new ArrayList() { 0 };
            double[] result = new double[count];
            Regex rex = new Regex(@"^[1-9]\d*\.\d*|0\.\d*[1-9]\d*$");
            ArrayList arrayF = new ArrayList();
            ArrayList arrayL = new ArrayList();
            int indexMp = 0;
            int indexD = 0;
            int indexA = 0;
            int indexMs = 0;
            string str1 = "Area(";
            string str2 = "Perimeter(";
            if (arrayList.Count < 1)
            {
                return temp;
            }
            else if (arrayList.Count == 1)
            {
                if (rex.IsMatch(arrayList[0].ToString()))
                {
                    return temp;
                }
                else
                {
                    string str = Match_str(arrayList[0].ToString());
                    ArrayList array = new ArrayList();
                    if (Convert.ToString(arrayList[0]).IndexOf(str1) > -1)
                    {
                        array = new ArrayList(ZoneArea(str, count, pFeatureClass));
                    }
                    else if (Convert.ToString(arrayList[0]).IndexOf(str2) > -1)
                    {
                        array = new ArrayList(ZoneLeng(str, count, pFeatureClass));
                    }
                    return array;
                }
            }
            //计算乘除法;
            if (arrayList.Contains("*") || arrayList.Contains("/"))
            {
                while (arrayList.Contains("*") || arrayList.Contains("/"))
                {
                    indexMp = arrayList.IndexOf("*");
                    indexD = arrayList.IndexOf("/");
#region //乘法;
                    if ((0 < indexMp && indexMp < indexD) || (0 < indexMp && indexD < 0))
                    {
                        string strF = Match_str(Convert.ToString(arrayList[indexMp - 1]));
                        string strL = Match_str(Convert.ToString(arrayList[indexMp + 1]));
#region//前后都是数组; 
                        if (strF != "" && strL != "")
                        {
                            if (Convert.ToString(arrayList[indexMp - 1]).IndexOf(str1) > -1)
                            {
                                arrayF = new ArrayList(ZoneArea(strF, count, pFeatureClass));
                            }
                            else if (Convert.ToString(arrayList[indexMp - 1]).IndexOf(str2) > -1)
                            {
                                arrayF = new ArrayList(ZoneLeng(strF, count, pFeatureClass));
                            }
                            if (Convert.ToString(arrayList[indexMp + 1]).IndexOf(str1) > -1)
                            {
                                arrayL = new ArrayList(ZoneArea(strL, count, pFeatureClass));
                            }
                            else if (Convert.ToString(arrayList[indexMp + 1]).IndexOf(str2) > -1)
                            {
                                arrayL = new ArrayList(ZoneLeng(strL, count, pFeatureClass));
                            }
                            for (int i = 0; i < count; i++)
                            {
                                result[i] = Convert.ToDouble(arrayF[i]) * Convert.ToDouble(arrayL[i]);
                            }
                            arrayList.RemoveRange(indexMp - 1, 3);
                            arrayList.InsertRange(indexMp - 1, result);
                        }
#endregion
                        
#region //前后都不是数组;
                        if (strF == "" && strL == "")
                        {
                            if (indexMp - 2 < 0 && indexMp + 2 > arrayList.Count - 1)
                            {
                                for (int i = 0; i < count; i++)
                                {
                                    result[i] = Convert.ToDouble(arrayList[indexMp - 1]) * Convert.ToDouble(arrayList[indexMp + 1]);
                                }
                                arrayList.RemoveRange(indexMp - 1, 3);
                                arrayList.InsertRange(indexMp - 1, result);
                            }
                            else if (indexMp - 2 < 0 && indexMp + 2 < arrayList.Count - 1)
                            {
                                if (rex.IsMatch(Convert.ToString(arrayList[indexMp + 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexMp - 1]) * Convert.ToDouble(arrayList[indexMp + i + 1]);
                                    }
                                    arrayList.RemoveRange(indexMp - 1, count + 2);
                                    arrayList.InsertRange(indexMp - 1, result);
                                }
                                else
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexMp - 1]) * Convert.ToDouble(arrayList[indexMp + 1]);
                                    }
                                    arrayList.RemoveRange(indexMp - 1, 3);
                                    arrayList.InsertRange(indexMp - 1, result);
                                }
                            }
                            else if (indexMp - 2 > 0 && indexMp + 2 > arrayList.Count - 1)
                            {
                                if (rex.IsMatch(Convert.ToString(arrayList[indexMp - 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexMp - count + i]) * Convert.ToDouble(arrayList[indexMp + 1]);
                                    }
                                    arrayList.RemoveRange(indexMp - count, count + 2);
                                    arrayList.InsertRange(indexMp - count, result);
                                }
                                else
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexMp - 1]) * Convert.ToDouble(arrayList[indexMp + 1]);
                                    }
                                    arrayList.RemoveRange(indexMp - 1, 3);
                                    arrayList.InsertRange(indexMp - 1, result);
                                }
                            }
                            else if (indexMp - 2 > 0 && indexMp + 2 < arrayList.Count - 1)
                            {
                                if (rex.IsMatch(Convert.ToString(arrayList[indexMp - 2])) && rex.IsMatch(Convert.ToString(arrayList[indexMp + 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexMp - count + i]) * Convert.ToDouble(arrayList[indexMp + i + 1]);
                                    }
                                    arrayList.RemoveRange(indexMp - count, count * 2 + 1);
                                    arrayList.InsertRange(indexMp - count, result);
                                }
                                else if (rex.IsMatch(Convert.ToString(arrayList[indexMp - 2])) == false && rex.IsMatch(Convert.ToString(arrayList[indexMp + 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexMp - 1]) * Convert.ToDouble(arrayList[indexMp + i + 1]);
                                    }
                                    arrayList.RemoveRange(indexMp - 1, count + 2);
                                    arrayList.InsertRange(indexMp - 1, result);
                                }
                                else if (rex.IsMatch(Convert.ToString(arrayList[indexMp - 2])) && rex.IsMatch(Convert.ToString(arrayList[indexMp + 2])) == false)
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexMp - count + i]) * Convert.ToDouble(arrayList[indexMp + 1]);
                                    }
                                    arrayList.RemoveRange(indexMp - count, count + 2);
                                    arrayList.InsertRange(indexMp - count, result);
                                }
                                else if (rex.IsMatch(Convert.ToString(arrayList[indexMp - 2])) == false && rex.IsMatch(Convert.ToString(arrayList[indexMp + 2])) == false)
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexMp - 1]) * Convert.ToDouble(arrayList[indexMp + 1]);
                                    }
                                    arrayList.RemoveRange(indexMp - 1, 3);
                                    arrayList.InsertRange(indexMp - 1, result);
                                }
                            }
                        }
#endregion

#region  //前为数组，后为数字;
                        if (strF != "" && strL == "")
                        {
                            if (Convert.ToString(arrayList[indexMp - 1]).IndexOf(str1) > -1)
                            {
                                arrayF = new ArrayList(ZoneArea(strF, count, pFeatureClass));
                            }
                            else if (Convert.ToString(arrayList[indexMp - 1]).IndexOf(str2) > -1)
                            {
                                arrayF = new ArrayList(ZoneLeng(strF, count, pFeatureClass));
                            }
                            if (indexMp + 2 > arrayList.Count - 1)
                            {
                                for (int i = 0; i < count; i++)
                                {
                                    result[i] = Convert.ToDouble(arrayF[i]) * Convert.ToDouble(arrayList[indexMp + 1]);
                                }
                                arrayList.RemoveRange(indexMp - 1, 3);
                                arrayList.InsertRange(indexMp - 1, result);
                            }
                            else
                            { 
                                if (rex.IsMatch(Convert.ToString(arrayList[indexMp + 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayF[i]) * Convert.ToDouble(arrayList[indexMp + i + 1]);
                                    }
                                    arrayList.RemoveRange(indexMp - 1, count + 2);
                                    arrayList.InsertRange(indexMp - 1, result);
                                }
                                else
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayF[i]) * Convert.ToDouble(arrayList[indexMp + 1]);
                                    }
                                    arrayList.RemoveRange(indexMp - 1, 3);
                                    arrayList.InsertRange(indexMp - 1, result);
                                }
                            }
                        }
#endregion
                       
#region //前为数字,后为数组;
                        if (strF == "" && strL != "")
                        {
                            if (Convert.ToString(arrayList[indexMp + 1]).IndexOf(str1) > -1)
                            {
                                arrayL = new ArrayList(ZoneArea(strL, count, pFeatureClass));
                            }
                            else if (Convert.ToString(arrayList[indexMp + 1]).IndexOf(str2) > -1)
                            {
                                arrayL = new ArrayList(ZoneLeng(strL, count, pFeatureClass));
                            }
                            if (indexMp - 2 < 0)
                            {
                                for (int i = 0; i < count; i++)
                                {
                                    result[i] = Convert.ToDouble(arrayList[indexMp - 1]) * Convert.ToDouble(arrayL[i]);
                                }
                                arrayList.RemoveRange(indexMp - 1, 3);
                                arrayList.InsertRange(indexMp - 1, result);
                            }
                            else
                            {
                                if (rex.IsMatch(Convert.ToString(arrayList[indexMp - 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexMp - count + i]) * Convert.ToDouble(arrayL[i]);
                                    }
                                    arrayList.RemoveRange(indexMp - count, count + 2);
                                    arrayList.InsertRange(indexMp - count, result);
                                }
                                else
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexMp - 1]) * Convert.ToDouble(arrayL[i]);
                                    }
                                    arrayList.RemoveRange(indexMp - 1, 3);
                                    arrayList.InsertRange(indexMp - 1, result);
                                }
                            }
                        }
#endregion
              
                    }
#endregion

#region //除法;
                    else if ((indexMp < 0 && indexD > 0) || (0 < indexD && indexD < indexMp))
                    {
                        string strF = Match_str(Convert.ToString(arrayList[indexD - 1]));
                        string strL = Match_str(Convert.ToString(arrayList[indexD + 1]));
#region//前后都是数组; 
                        if (strF != "" && strL != "")
                        {
                            if (Convert.ToString(arrayList[indexD - 1]).IndexOf(str1) > -1)
                            {
                                arrayF = new ArrayList(ZoneArea(strF, count, pFeatureClass));
                            }
                            else if (Convert.ToString(arrayList[indexD - 1]).IndexOf(str2) > -1)
                            {
                                arrayF = new ArrayList(ZoneLeng(strF, count, pFeatureClass));
                            }
                            if (Convert.ToString(arrayList[indexD + 1]).IndexOf(str1) > -1)
                            {
                                arrayL = new ArrayList(ZoneArea(strL, count, pFeatureClass));
                            }
                            else if (Convert.ToString(arrayList[indexD + 1]).IndexOf(str2) > -1)
                            {
                                arrayL = new ArrayList(ZoneLeng(strL, count, pFeatureClass));
                            }
                            for (int i = 0; i < count; i++)
                            {
                                if (Convert.ToDouble(arrayL[i]) !=0)
                                {
                                    result[i] = Convert.ToDouble(arrayF[i]) / Convert.ToDouble(arrayL[i]);
                                }
                                else
                                {
                                    result[i] = 0;
                                }
                            }
                            arrayList.RemoveRange(indexD - 1, 3);
                            arrayList.InsertRange(indexD - 1, result);
                        }
#endregion
                        
#region //前后都不是数组;
                        if (strF == "" && strL == "")
                        {
                            if (indexD - 2 < 0 && indexD + 2 > arrayList.Count - 1)
                            {
                                for (int i = 0; i < count; i++)
                                {
                                    if (Convert.ToDouble(arrayList[indexD + 1]) != 0)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexD - 1]) / Convert.ToDouble(arrayList[indexD + 1]);
                                    }
                                    else
                                    {
                                        result[i] = 0;
                                    }
                                }
                                arrayList.RemoveRange(indexD - 1, 3);
                                arrayList.InsertRange(indexD - 1, result);
                            }
                            else if (indexD - 2 < 0 && indexD + 2 < arrayList.Count - 1)
                            {
                                if (rex.IsMatch(Convert.ToString(arrayList[indexD + 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        if (Convert.ToDouble(arrayList[indexD + i + 1]) != 0)
                                        {
                                            result[i] = Convert.ToDouble(arrayList[indexD - 1]) / Convert.ToDouble(arrayList[indexD + i + 1]);
                                        }
                                        else
                                        {
                                            result[i] = 0;
                                        }
                                    }
                                    arrayList.RemoveRange(indexD - 1, count + 2);
                                    arrayList.InsertRange(indexD - 1, result);
                                }
                                else
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        if (Convert.ToDouble(arrayList[indexD + 1]) !=0)
                                        { 
                                            result[i] = Convert.ToDouble(arrayList[indexD - 1]) / Convert.ToDouble(arrayList[indexD + 1]);
                                        }
                                        else
                                        {
                                            result[i] = 0;
                                        }
                                    }
                                    arrayList.RemoveRange(indexD - 1, 3);
                                    arrayList.InsertRange(indexD - 1, result);
                                }
                            }
                            else if (indexD - 2 > 0 && indexD + 2 > arrayList.Count - 1)
                            {
                                if (rex.IsMatch(Convert.ToString(arrayList[indexD - 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        if (Convert.ToDouble(arrayList[indexD + 1]) != 0)
                                        {
                                            result[i] = Convert.ToDouble(arrayList[indexD - count + i]) / Convert.ToDouble(arrayList[indexD + 1]);
                                        }
                                        else
                                        {
                                            result[i] = 0;
                                        }
                                    }
                                    arrayList.RemoveRange(indexD - count, count + 2);
                                    arrayList.InsertRange(indexD - count, result);
                                }
                                else
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        if (Convert.ToDouble(arrayList[indexD + 1]) != 0)
                                        {
                                            result[i] = Convert.ToDouble(arrayList[indexD - 1]) / Convert.ToDouble(arrayList[indexD + 1]);
                                        }
                                        else
                                        {
                                            result[i] = 0;
                                        }
                                    }
                                    arrayList.RemoveRange(indexD - 1, 3);
                                    arrayList.InsertRange(indexD - 1, result);
                                }
                            }
                            else if (indexD - 2 > 0 && indexD + 2 < arrayList.Count - 1)
                            {
                                if (rex.IsMatch(Convert.ToString(arrayList[indexD - 2])) && rex.IsMatch(Convert.ToString(arrayList[indexD + 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        if (Convert.ToDouble(arrayList[indexD + i + 1]) != 0)
                                        {
                                            result[i] = Convert.ToDouble(arrayList[indexD - count + i]) / Convert.ToDouble(arrayList[indexD + i + 1]);
                                        }
                                        else
                                        {
                                            result[i] = 0;
                                        }
                                    }
                                    arrayList.RemoveRange(indexD - count, count * 2 + 1);
                                    arrayList.InsertRange(indexD - count, result);
                                }
                                else if (rex.IsMatch(Convert.ToString(arrayList[indexD - 2])) == false && rex.IsMatch(Convert.ToString(arrayList[indexD + 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        if (Convert.ToDouble(arrayList[indexD + i + 1]) != 0)
                                        {
                                            result[i] = Convert.ToDouble(arrayList[indexD - 1]) / Convert.ToDouble(arrayList[indexD + i + 1]);
                                        }
                                        else
                                        {
                                            result[i] = 0;
                                        }
                                    }
                                    arrayList.RemoveRange(indexD - 1, count + 2);
                                    arrayList.InsertRange(indexD - 1, result);
                                }
                                else if (rex.IsMatch(Convert.ToString(arrayList[indexD - 2])) && rex.IsMatch(Convert.ToString(arrayList[indexD + 2])) == false)
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        if (Convert.ToDouble(arrayList[indexD + 1]) != 0)
                                        {
                                            result[i] = Convert.ToDouble(arrayList[indexD - count + i]) / Convert.ToDouble(arrayList[indexD + 1]);
                                        }
                                        else
                                        {
                                            result[i] = 0;
                                        }
                                    }
                                    arrayList.RemoveRange(indexD - count, count + 2);
                                    arrayList.InsertRange(indexD - count, result);
                                }
                                else if (rex.IsMatch(Convert.ToString(arrayList[indexD - 2])) == false && rex.IsMatch(Convert.ToString(arrayList[indexD + 2])) == false)
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        if (Convert.ToDouble(arrayList[indexD + 1]) != 0)
                                        {
                                            result[i] = Convert.ToDouble(arrayList[indexD - 1]) / Convert.ToDouble(arrayList[indexD + 1]);
                                        }
                                        else
                                        {
                                            result[i] = 0;
                                        }
                                    }
                                    arrayList.RemoveRange(indexD - 1, 3);
                                    arrayList.InsertRange(indexD - 1, result);
                                }
                            }
                        }
#endregion

#region  //前为数组，后为数字;
                        if (strF != "" && strL == "")
                        {
                            if (Convert.ToString(arrayList[indexD - 1]).IndexOf(str1) > -1)
                            {
                                arrayF = new ArrayList(ZoneArea(strF, count, pFeatureClass));
                            }
                            else if (Convert.ToString(arrayList[indexD - 1]).IndexOf(str2) > -1)
                            {
                                arrayF = new ArrayList(ZoneLeng(strF, count, pFeatureClass));
                            }
                            if (indexD + 2 > arrayList.Count - 1)
                            {
                                for (int i = 0; i < count; i++)
                                {
                                    if (Convert.ToDouble(arrayList[indexD + 1]) != 0)
                                    {
                                        result[i] = Convert.ToDouble(arrayF[i]) / Convert.ToDouble(arrayList[indexD + 1]);
                                    }
                                    else
                                    {
                                        result[i] = 0;
                                    }
                                }
                                arrayList.RemoveRange(indexD - 1, 3);
                                arrayList.InsertRange(indexD - 1, result);
                            }
                            else
                            { 
                                if (rex.IsMatch(Convert.ToString(arrayList[indexD + 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        if (Convert.ToDouble(arrayList[indexD + i + 1]) != 0)
                                        {
                                            result[i] = Convert.ToDouble(arrayF[i]) / Convert.ToDouble(arrayList[indexD + i + 1]);
                                        }
                                        else
                                        {
                                            result[i] = 0;
                                        }
                                    }
                                    arrayList.RemoveRange(indexD - 1, count + 2);
                                    arrayList.InsertRange(indexD - 1, result);
                                }
                                else
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        if (Convert.ToDouble(arrayList[indexD + 1]) != 0)
                                        {
                                            result[i] = Convert.ToDouble(arrayF[i]) / Convert.ToDouble(arrayList[indexD + 1]);
                                        }
                                        else
                                        {
                                            result[i] = 0;
                                        }
                                    }
                                    arrayList.RemoveRange(indexD - 1, 3);
                                    arrayList.InsertRange(indexD - 1, result);
                                }
                            }
                        }
#endregion
                       
#region //前为数字,后为数组;
                        if (strF == "" && strL != "")
                        {
                            if (Convert.ToString(arrayList[indexD + 1]).IndexOf(str1) > -1)
                            {
                                arrayL = new ArrayList(ZoneArea(strL, count, pFeatureClass));
                            }
                            else if (Convert.ToString(arrayList[indexD + 1]).IndexOf(str2) > -1)
                            {
                                arrayL = new ArrayList(ZoneLeng(strL, count, pFeatureClass));
                            }
                            if (indexD - 2 < 0)
                            {
                                for (int i = 0; i < count; i++)
                                {
                                    if (Convert.ToDouble(arrayL[i]) != 0)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexD - 1]) / Convert.ToDouble(arrayL[i]);
                                    }
                                    else
                                    {
                                        result[i] = 0;
                                    }
                                }
                                arrayList.RemoveRange(indexD - 1, 3);
                                arrayList.InsertRange(indexD - 1, result);
                            }
                            else
                            {
                                if (rex.IsMatch(Convert.ToString(arrayList[indexD - 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        if (Convert.ToDouble(arrayL[i]) != 0)
                                        {
                                            result[i] = Convert.ToDouble(arrayList[indexD - count + i]) / Convert.ToDouble(arrayL[i]);
                                        }
                                        else
                                        {
                                            result[i] = 0;
                                        }
                                    }
                                    arrayList.RemoveRange(indexD - count, count + 2);
                                    arrayList.InsertRange(indexD - count, result);
                                }
                                else
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        if (Convert.ToDouble(arrayL[i]) != 0)
                                        {
                                            result[i] = Convert.ToDouble(arrayList[indexD - 1]) / Convert.ToDouble(arrayL[i]);
                                        }
                                        else
                                        {
                                            result[i] = 0;
                                        }
                                    }
                                    arrayList.RemoveRange(indexD - 1, 3);
                                    arrayList.InsertRange(indexD - 1, result);
                                }
                            }
                        }
#endregion
                    }
#endregion
                }
            }
            //计算加减法;
            if (arrayList.Contains("+") || arrayList.Contains("-"))
            {
                while (arrayList.Contains("+") || arrayList.Contains("-"))
                {
                    indexA = arrayList.IndexOf("+");
                    indexMs = arrayList.IndexOf("-");
#region //加法;
                    if ((0 < indexA && indexA < indexMs) || (0 < indexA && indexMs < 0))
                    {
                        string strF = Match_str(Convert.ToString(arrayList[indexA - 1]));
                        string strL = Match_str(Convert.ToString(arrayList[indexA + 1]));
                        #region//前后都是数组;
                        if (strF != "" && strL != "")
                        {
                            if (Convert.ToString(arrayList[indexA - 1]).IndexOf(str1) > -1)
                            {
                                arrayF = new ArrayList(ZoneArea(strF, count, pFeatureClass));
                            }
                            else if (Convert.ToString(arrayList[indexA - 1]).IndexOf(str2) > -1)
                            {
                                arrayF = new ArrayList(ZoneLeng(strF, count, pFeatureClass));
                            }
                            if (Convert.ToString(arrayList[indexA + 1]).IndexOf(str1) > -1)
                            {
                                arrayL = new ArrayList(ZoneArea(strL, count, pFeatureClass));
                            }
                            else if (Convert.ToString(arrayList[indexA + 1]).IndexOf(str2) > -1)
                            {
                                arrayL = new ArrayList(ZoneLeng(strL, count, pFeatureClass));
                            }
                            for (int i = 0; i < count; i++)
                            {
                                result[i] = Convert.ToDouble(arrayF[i]) + Convert.ToDouble(arrayL[i]);
                            }
                            arrayList.RemoveRange(indexA - 1, 3);
                            arrayList.InsertRange(indexA - 1, result);
                        }
                        #endregion

                        #region //前后都不是数组;
                        if (strF == "" && strL == "")
                        {
                            if (indexA - 2 < 0 && indexA + 2 > arrayList.Count - 1)
                            {
                                for (int i = 0; i < count; i++)
                                {
                                    result[i] = Convert.ToDouble(arrayList[indexA - 1]) + Convert.ToDouble(arrayList[indexA + 1]);
                                }
                                arrayList.RemoveRange(indexA - 1, 3);
                                arrayList.InsertRange(indexA - 1, result);
                            }
                            else if (indexA - 2 < 0 && indexA + 2 < arrayList.Count - 1)
                            {
                                if (rex.IsMatch(Convert.ToString(arrayList[indexA + 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexA - 1]) + Convert.ToDouble(arrayList[indexA + i + 1]);
                                    }
                                    arrayList.RemoveRange(indexA - 1, count + 2);
                                    arrayList.InsertRange(indexA - 1, result);
                                }
                                else
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexA - 1]) + Convert.ToDouble(arrayList[indexA + 1]);
                                    }
                                    arrayList.RemoveRange(indexA - 1, 3);
                                    arrayList.InsertRange(indexA - 1, result);
                                }
                            }
                            else if (indexA - 2 > 0 && indexA + 2 > arrayList.Count - 1)
                            {
                                if (rex.IsMatch(Convert.ToString(arrayList[indexA - 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexA - count + i]) + Convert.ToDouble(arrayList[indexA + 1]);
                                    }
                                    arrayList.RemoveRange(indexA - count, count + 2);
                                    arrayList.InsertRange(indexA - count, result);
                                }
                                else
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexA - 1]) + Convert.ToDouble(arrayList[indexA + 1]);
                                    }
                                    arrayList.RemoveRange(indexA - 1, 3);
                                    arrayList.InsertRange(indexA - 1, result);
                                }
                            }
                            else if (indexA - 2 > 0 && indexA + 2 < arrayList.Count - 1)
                            {
                                if (rex.IsMatch(Convert.ToString(arrayList[indexA - 2])) && rex.IsMatch(Convert.ToString(arrayList[indexA + 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexA - count + i]) + Convert.ToDouble(arrayList[indexA + i + 1]);
                                    }
                                    arrayList.RemoveRange(indexA - count, count * 2 + 1);
                                    arrayList.InsertRange(indexA - count, result);
                                }
                                else if (rex.IsMatch(Convert.ToString(arrayList[indexA - 2])) == false && rex.IsMatch(Convert.ToString(arrayList[indexA + 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexA - 1]) + Convert.ToDouble(arrayList[indexA + i + 1]);
                                    }
                                    arrayList.RemoveRange(indexA - 1, count + 2);
                                    arrayList.InsertRange(indexA - 1, result);
                                }
                                else if (rex.IsMatch(Convert.ToString(arrayList[indexA - 2])) && rex.IsMatch(Convert.ToString(arrayList[indexA + 2])) == false)
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexA - count + i]) + Convert.ToDouble(arrayList[indexA + 1]);
                                    }
                                    arrayList.RemoveRange(indexA - count, count + 2);
                                    arrayList.InsertRange(indexA - count, result);
                                }
                                else if (rex.IsMatch(Convert.ToString(arrayList[indexA - 2])) == false && rex.IsMatch(Convert.ToString(arrayList[indexA + 2])) == false)
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexA - 1]) + Convert.ToDouble(arrayList[indexA + 1]);
                                    }
                                    arrayList.RemoveRange(indexA - 1, 3);
                                    arrayList.InsertRange(indexA - 1, result);
                                }
                            }
                        }
                        #endregion

                        #region  //前为数组，后为数字;
                        if (strF != "" && strL == "")
                        {
                            if (Convert.ToString(arrayList[indexA - 1]).IndexOf(str1) > -1)
                            {
                                arrayF = new ArrayList(ZoneArea(strF, count, pFeatureClass));
                            }
                            else if (Convert.ToString(arrayList[indexA - 1]).IndexOf(str2) > -1)
                            {
                                arrayF = new ArrayList(ZoneLeng(strF, count, pFeatureClass));
                            }
                            if (indexA + 2 > arrayList.Count - 1)
                            {
                                for (int i = 0; i < count; i++)
                                {
                                    result[i] = Convert.ToDouble(arrayF[i]) + Convert.ToDouble(arrayList[indexA + 1]);
                                }
                                arrayList.RemoveRange(indexA - 1, 3);
                                arrayList.InsertRange(indexA - 1, result);
                            }
                            else
                            {
                                if (rex.IsMatch(Convert.ToString(arrayList[indexA + 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayF[i]) + Convert.ToDouble(arrayList[indexA + i + 1]);
                                    }
                                    arrayList.RemoveRange(indexA - 1, count + 2);
                                    arrayList.InsertRange(indexA - 1, result);
                                }
                                else
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayF[i]) + Convert.ToDouble(arrayList[indexA + 1]);
                                    }
                                    arrayList.RemoveRange(indexA - 1, 3);
                                    arrayList.InsertRange(indexA - 1, result);
                                }
                            }
                        }
                        #endregion

                        #region //前为数字,后为数组;
                        if (strF == "" && strL != "")
                        {
                            if (Convert.ToString(arrayList[indexA + 1]).IndexOf(str1) > -1)
                            {
                                arrayL = new ArrayList(ZoneArea(strL, count, pFeatureClass));
                            }
                            else if (Convert.ToString(arrayList[indexA + 1]).IndexOf(str2) > -1)
                            {
                                arrayL = new ArrayList(ZoneLeng(strL, count, pFeatureClass));
                            }
                            if (indexA - 2 < 0)
                            {
                                for (int i = 0; i < count; i++)
                                {
                                    result[i] = Convert.ToDouble(arrayList[indexA - 1]) + Convert.ToDouble(arrayL[i]);
                                }
                                arrayList.RemoveRange(indexA - 1, 3);
                                arrayList.InsertRange(indexA - 1, result);
                            }
                            else
                            {
                                if (rex.IsMatch(Convert.ToString(arrayList[indexA - 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexA - count + i]) + Convert.ToDouble(arrayL[i]);
                                    }
                                    arrayList.RemoveRange(indexA - count, count + 2);
                                    arrayList.InsertRange(indexA - count, result);
                                }
                                else
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexA - 1]) + Convert.ToDouble(arrayL[i]);
                                    }
                                    arrayList.RemoveRange(indexA - 1, 3);
                                    arrayList.InsertRange(indexA - 1, result);
                                }
                            }
                        }
                        #endregion
                    }
                    #endregion
#region //减法;
                    else if ((0 < indexMs && indexMs < indexA) || (0 < indexMs && indexA < 0))
                    {
                        string strF = Match_str(Convert.ToString(arrayList[indexMs - 1]));
                        string strL = Match_str(Convert.ToString(arrayList[indexMs + 1]));
#region//前后都是数组; 
                        if (strF != "" && strL != "")
                        {
                            if (Convert.ToString(arrayList[indexMs - 1]).IndexOf(str1) > -1)
                            {
                                arrayF = new ArrayList(ZoneArea(strF, count, pFeatureClass));
                            }
                            else if (Convert.ToString(arrayList[indexMs - 1]).IndexOf(str2) > -1)
                            {
                                arrayF = new ArrayList(ZoneLeng(strF, count, pFeatureClass));
                            }
                            if (Convert.ToString(arrayList[indexMs + 1]).IndexOf(str1) > -1)
                            {
                                arrayL = new ArrayList(ZoneArea(strL, count, pFeatureClass));
                            }
                            else if (Convert.ToString(arrayList[indexMs + 1]).IndexOf(str2) > -1)
                            {
                                arrayL = new ArrayList(ZoneLeng(strL, count, pFeatureClass));
                            }
                            for (int i = 0; i < count; i++)
                            {
                                result[i] = Convert.ToDouble(arrayF[i]) - Convert.ToDouble(arrayL[i]);
                            }
                            arrayList.RemoveRange(indexMs - 1, 3);
                            arrayList.InsertRange(indexMs - 1, result);
                        }
#endregion
                        
#region //前后都不是数组;
                        if (strF == "" && strL == "")
                        {
                            if (indexMs - 2 < 0 && indexMs + 2 > arrayList.Count - 1)
                            {
                                for (int i = 0; i < count; i++)
                                {
                                    result[i] = Convert.ToDouble(arrayList[indexMs - 1]) - Convert.ToDouble(arrayList[indexMs + 1]);
                                }
                                arrayList.RemoveRange(indexMs - 1, 3);
                                arrayList.InsertRange(indexMs - 1, result);
                            }
                            else if (indexMs - 2 < 0 && indexMs + 2 < arrayList.Count - 1)
                            {
                                if (rex.IsMatch(Convert.ToString(arrayList[indexMs + 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexMs - 1]) - Convert.ToDouble(arrayList[indexMs + i + 1]);
                                    }
                                    arrayList.RemoveRange(indexMs - 1, count + 2);
                                    arrayList.InsertRange(indexMs - 1, result);
                                }
                                else
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexMs - 1]) - Convert.ToDouble(arrayList[indexMs + 1]);
                                    }
                                    arrayList.RemoveRange(indexMs - 1, 3);
                                    arrayList.InsertRange(indexMs - 1, result);
                                }
                            }
                            else if (indexMs - 2 > 0 && indexMs + 2 > arrayList.Count - 1)
                            {
                                if (rex.IsMatch(Convert.ToString(arrayList[indexMs - 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexMs - count + i]) - Convert.ToDouble(arrayList[indexMs + 1]);
                                    }
                                    arrayList.RemoveRange(indexMs - count, count + 2);
                                    arrayList.InsertRange(indexMs - count, result);
                                }
                                else
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexMs - 1]) - Convert.ToDouble(arrayList[indexMs + 1]);
                                    }
                                    arrayList.RemoveRange(indexMs - 1, 3);
                                    arrayList.InsertRange(indexMs - 1, result);
                                }
                            }
                            else if (indexMs - 2 > 0 && indexMs + 2 < arrayList.Count - 1)
                            {
                                if (rex.IsMatch(Convert.ToString(arrayList[indexMs - 2])) && rex.IsMatch(Convert.ToString(arrayList[indexMs + 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexMs - count + i]) - Convert.ToDouble(arrayList[indexMs + i + 1]);
                                    }
                                    arrayList.RemoveRange(indexMs - count, count * 2 + 1);
                                    arrayList.InsertRange(indexMs - count, result);
                                }
                                else if (rex.IsMatch(Convert.ToString(arrayList[indexMs - 2])) == false && rex.IsMatch(Convert.ToString(arrayList[indexMs + 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexMs - 1]) - Convert.ToDouble(arrayList[indexMs + i + 1]);
                                    }
                                    arrayList.RemoveRange(indexMs - 1, count + 2);
                                    arrayList.InsertRange(indexMs - 1, result);
                                }
                                else if (rex.IsMatch(Convert.ToString(arrayList[indexMs - 2])) && rex.IsMatch(Convert.ToString(arrayList[indexMs + 2])) == false)
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexMs - count + i]) - Convert.ToDouble(arrayList[indexMs + 1]);
                                    }
                                    arrayList.RemoveRange(indexMs - count, count + 2);
                                    arrayList.InsertRange(indexMs - count, result);
                                }
                                else if (rex.IsMatch(Convert.ToString(arrayList[indexMs - 2])) == false && rex.IsMatch(Convert.ToString(arrayList[indexMs + 2])) == false)
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexMs - 1]) - Convert.ToDouble(arrayList[indexMs + 1]);
                                    }
                                    arrayList.RemoveRange(indexMs - 1, 3);
                                    arrayList.InsertRange(indexMs - 1, result);
                                }
                            }
                        }
#endregion

#region  //前为数组，后为数字;
                        if (strF != "" && strL == "")
                        {
                            if (Convert.ToString(arrayList[indexMs - 1]).IndexOf(str1) > -1)
                            {
                                arrayF = new ArrayList(ZoneArea(strF, count, pFeatureClass));
                            }
                            else if (Convert.ToString(arrayList[indexMs - 1]).IndexOf(str2) > -1)
                            {
                                arrayF = new ArrayList(ZoneLeng(strF, count, pFeatureClass));
                            }
                            if (indexMs + 2 > arrayList.Count - 1)
                            {
                                for (int i = 0; i < count; i++)
                                {
                                    result[i] = Convert.ToDouble(arrayF[i]) - Convert.ToDouble(arrayList[indexMs + 1]);
                                }
                                arrayList.RemoveRange(indexMs - 1, 3);
                                arrayList.InsertRange(indexMs - 1, result);
                            }
                            else
                            {
                                if (rex.IsMatch(Convert.ToString(arrayList[indexMs + 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayF[i]) - Convert.ToDouble(arrayList[indexMs + i + 1]);
                                    }
                                    arrayList.RemoveRange(indexMs - 1, count + 2);
                                    arrayList.InsertRange(indexMs - 1, result);
                                }
                                else
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayF[i]) - Convert.ToDouble(arrayList[indexMs + 1]);
                                    }
                                    arrayList.RemoveRange(indexMs - 1, 3);
                                    arrayList.InsertRange(indexMs - 1, result);
                                }
                            }
                        }
#endregion
                       
#region //前为数字,后为数组;
                        if (strF == "" && strL != "")
                        {
                            if (Convert.ToString(arrayList[indexMs + 1]).IndexOf(str1) > -1)
                            {
                                arrayL = new ArrayList(ZoneArea(strL, count, pFeatureClass));
                            }
                            else if (Convert.ToString(arrayList[indexMs + 1]).IndexOf(str2) > -1)
                            {
                                arrayL = new ArrayList(ZoneLeng(strL, count, pFeatureClass));
                            }
                            if (indexMs - 2 < 0)
                            {
                                for (int i = 0; i < count; i++)
                                {
                                    result[i] = Convert.ToDouble(arrayList[indexMs - 1]) - Convert.ToDouble(arrayL[i]);
                                }
                                arrayList.RemoveRange(indexMs - 1, 3);
                                arrayList.InsertRange(indexMs - 1, result);
                            }
                            else
                            {
                                if (rex.IsMatch(Convert.ToString(arrayList[indexMs - 2])))
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexMs - count + i]) - Convert.ToDouble(arrayL[i]);
                                    }
                                    arrayList.RemoveRange(indexMs - count, count + 2);
                                    arrayList.InsertRange(indexMs - count, result);
                                }
                                else
                                {
                                    for (int i = 0; i < count; i++)
                                    {
                                        result[i] = Convert.ToDouble(arrayList[indexMs - 1]) - Convert.ToDouble(arrayL[i]);
                                    }
                                    arrayList.RemoveRange(indexMs - 1, 3);
                                    arrayList.InsertRange(indexMs - 1, result);
                                }
                            }
                        }
#endregion
                    }
#endregion
                }
            }
            return arrayList;
        }

        public ArrayList BracketRemove(ArrayList arrayList, int count,IFeatureClass pFeatureClass)
        {
            while (arrayList.Contains("(") || arrayList.Contains(")"))
            {
                ArrayList arrayTemp = new ArrayList(arrayList);
                ArrayList array = new ArrayList();
                int indexFront = arrayList.IndexOf("(");
                int indexLater1 = arrayList.LastIndexOf(")");
                if (indexLater1 < 0 && 0 < indexFront)
                {
                    arrayList.RemoveRange(indexFront, arrayList.Count - indexFront);
                    arrayTemp.RemoveRange(0, indexFront + 1);
                    array = new ArrayList(BracketRemove(arrayTemp, count, pFeatureClass));
                    if (array.Contains("(") || array.Contains(")"))
                    {
                        array = new ArrayList(BracketRemove(array, count, pFeatureClass));

                    }
                    arrayList.InsertRange(indexFront, math(array, count, pFeatureClass));
                }
                else if (indexFront < 0 && 0 < indexLater1)
                {
                    arrayList.RemoveRange(0, indexLater1 + 1);
                    arrayTemp.RemoveRange(indexLater1, arrayTemp.Count - indexLater1);
                    array = new ArrayList(BracketRemove(arrayTemp, count, pFeatureClass));
                    if (array.Contains("(") || array.Contains(")"))
                    {
                        array = new ArrayList(BracketRemove(array, count, pFeatureClass));

                    }
                    arrayList.InsertRange(0, math(array, count, pFeatureClass));
                }
                else if (indexFront < indexLater1)
                {
                    arrayList.RemoveRange(indexFront, indexLater1 - indexFront + 1);
                    arrayTemp.RemoveRange(0, indexFront + 1);
                    int indexLater2 = arrayTemp.LastIndexOf(")");
                    arrayTemp.RemoveRange(indexLater2, arrayTemp.Count - indexLater2);
                    array = new ArrayList(BracketRemove(arrayTemp, count, pFeatureClass));
                    if (array.Contains("(") || array.Contains(")"))
                    {
                        array = new ArrayList(BracketRemove(array, count, pFeatureClass));

                    }
                    arrayList.InsertRange(indexFront, math(array, count, pFeatureClass));
                }
                else if (indexFront > indexLater1)
                {
                    arrayList.RemoveRange(0, indexLater1 + 1);
                    arrayTemp.RemoveRange(indexLater1, arrayTemp.Count - indexLater1);
                    array = new ArrayList(BracketRemove(arrayTemp, count, pFeatureClass));
                    if (array.Contains("(") || array.Contains(")"))
                    {
                        array = new ArrayList(BracketRemove(array, count, pFeatureClass));

                    }
                    arrayList.InsertRange(0, math(array, count, pFeatureClass));

                    arrayTemp = new ArrayList(arrayList);
                    int indexFront2 = arrayTemp.LastIndexOf("(");
                    arrayList.RemoveRange(indexFront2, arrayList.Count - indexFront2);
                    arrayTemp.RemoveRange(0, indexFront2 + 1);
                    array = new ArrayList(BracketRemove(arrayTemp, count, pFeatureClass));
                    if (array.Contains("(") || array.Contains(")"))
                    {
                        array = new ArrayList(BracketRemove(array, count, pFeatureClass));
                    }
                    arrayList.InsertRange(indexFront2, math(array, count, pFeatureClass));
                }
            }
            return arrayList;
        }

#region 
        /// <summary>
        /// 计算面积;
        /// </summary>
        /// <param name="sql">面积查询条件</param>
        /// <returns></returns>
        public double[] ZoneArea(string sql, int count,IFeatureClass pFeatureClass)
        {
            double[] Area = new double[count];
            List<double> Area_list = new List<double>();
            //获取当前图层;
            
            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = MainForm.dataInputInfo.zoneField;
            pDataStatistics.Cursor = (ICursor)pFeatureCursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();

            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;
                //计算耕地面积;
                IQueryFilter pQueryFilter = new QueryFilterClass();
                if (sql == "0000")
                {
                    pQueryFilter.WhereClause = "\""+MainForm.dataInputInfo.zoneField+"\"" + " = \'" + obj+"\'";
                }
                else
                {
                    pQueryFilter.WhereClause = "\"" + MainForm.dataInputInfo.zoneField + "\"" + " = \'" + obj+"\'" + " AND " + "\"" + MainForm.dataInputInfo.codeField + "\"" + " = " + "\'" + sql + "\'";
                }
                IFeatureCursor mFeatureCursor = pFeatureClass.Search(pQueryFilter, true);

                IDataStatistics pAreaStatistics = new DataStatisticsClass();
                pAreaStatistics.Field = "Shape_Area";
                pAreaStatistics.Cursor = mFeatureCursor as ICursor;
                IStatisticsResults results = pAreaStatistics.Statistics;
                Area_list.Add(results.Sum);
            }
            Area = Area_list.ToArray();
            return Area;
        }

        /// <summary>
        /// 计算周长;
        /// </summary>
        /// <param name="sql">周长查询条件</param>
        /// <returns></returns>
        public double[] ZoneLeng(string sql, int count,IFeatureClass pFeatureClass)
        {
            double[] ZoneLeng = new double[count];
            List<double> ZoneLeng_list = new List<double>();

            IFeatureCursor pFeatureCursor = pFeatureClass.Search(null, true);
            IDataStatistics pDataStatistics = new DataStatisticsClass();
            pDataStatistics.Field = MainForm.dataInputInfo.zoneField;
            pDataStatistics.Cursor = (ICursor)pFeatureCursor;
            IEnumerator pEnumerator = pDataStatistics.UniqueValues;
            pEnumerator.Reset();

            while (pEnumerator.MoveNext())
            {
                object obj = pEnumerator.Current;
                IQueryFilter pQueryFilter = new QueryFilterClass();
                if (sql == "0000")
                {
                    pQueryFilter.WhereClause = "\"" + MainForm.dataInputInfo.zoneField + "\"" + " = \'" + obj+"\'";
                }
                else
                {
                    pQueryFilter.WhereClause = "\"" + MainForm.dataInputInfo.zoneField + "\"" + " = \'" + obj +"\'"+ " AND " + "\"" + MainForm.dataInputInfo.codeField + "\"" + " = " + "\'" + sql + "\'";
                }
                IFeatureCursor mFeatureCursor = pFeatureClass.Search(pQueryFilter, true);

                IDataStatistics pAreaStatistics = new DataStatisticsClass();
                pAreaStatistics.Field = "Shape_Length";
                pAreaStatistics.Cursor = mFeatureCursor as ICursor;
                IStatisticsResults results = pAreaStatistics.Statistics;
                ZoneLeng_list.Add(results.Sum);
            }
            ZoneLeng = ZoneLeng_list.ToArray();
            return ZoneLeng;
        }
#endregion
    }
}
