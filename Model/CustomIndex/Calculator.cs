using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AE_Environment.Model.CustomIndex
{
    class Calculator
    {
        public double CalculateFormula(List<string> list)
        {
            double result = 0;
            if (list.Contains("(") || list.Contains(")"))
            {
                result = MathCal(BracketRemove(list));
            } 
            else
            {
                result = MathCal(list);
            }
            return result;
        }
        /************************************************************************
        //计算公式实例：r*(a*(a*(b+c)-d)-e)*(a*(a*(b+c)-d)-e)/(a*(a*(b+c)-d)-e)-s
         1、提取计算公式：式1：a*(a*(b+c)-d)-e)*(a*(a*(b+c)-d)-e)/(a*(a*(b+c)-d)-e 和 式0：r*-s
         2、对计算公式 式1进行递归：得：式3：a*(b+c)-d)-e)*(a*(a*(b+c)-d)-e)/(a*(a*(b+c)-d
         * 对计算公式 式1进行递归：得：式4：b+c)-d)-e)*(a*(a*(b+c)-d)-e)/(a*(a*(b+c
         * 对计算公式 式1进行递归：得：式5：a*(a*(b+c)-d)-e=n
         * 对计算公式 式1进行递归：得：式6：a*(b+c)-d=m
         * 对计算公式 式1进行递归：得：式7：b+c  计算得：b+c=z;
         3、将z代入式6中，得a*z-d=m;
         * 3、将m代入式5中，得a*m-e=n;
         4、将n代入式4中得：式8:b+c)-d)-e)*n/(a*(a*(b+c
         * 将式8分解为两部分：式9：b+c)-d)-e和式10：*n/(a*(a*(b+c
         * 计算式9：b+c)-d)-e=o,将o代入式10中得：o*n/(a*(a*(b+c=p
         
        ************************************************************************/

        #region 分层去除公式中的括号进行计算：
        private List<string> BracketRemove(List<string> list)
        {
            while (list.Contains("(") || list.Contains(")"))
            {
                List<string> listTmp1 = new List<string>(list);
                List<string> listTmp2 = new List<string>();         //子项;
                int frontBracket = list.IndexOf("(");
                int backBracket = list.LastIndexOf(")");

                #region 有括号的第一种情况：a * (arrayList) / b
                if (frontBracket >= 0 && frontBracket < backBracket)
                {
                    list.RemoveRange(frontBracket, backBracket - frontBracket + 1);
                    listTmp1.RemoveRange(0, frontBracket + 1);
                    int backBracket2 = listTmp1.LastIndexOf(")");
                    listTmp1.RemoveRange(backBracket2, listTmp1.Count - backBracket2);

                    listTmp2 = new List<string>(BracketRemove(listTmp1));
                    list.InsertRange(frontBracket, listTmp2);
                }
                #endregion
                #region 有括号的第二种情况：a + b) * (a + b
                else if (backBracket > 0 && backBracket < frontBracket)
                {
                    //计算)前的部分;
                    list.RemoveRange(0, backBracket + 1);
                    listTmp1.RemoveRange(backBracket, listTmp1.Count - backBracket);

                    listTmp2 = new List<string>(BracketRemove(listTmp1));
                    list.InsertRange(0, listTmp2);

                    //计算(后的部分;
                    int frontBracket2 = list.IndexOf("(");
                    listTmp1 = new List<string>(list);
                    list.RemoveRange(frontBracket2, list.Count - frontBracket2);
                    listTmp1.RemoveRange(0, frontBracket2 + 1);

                    listTmp2 = new List<string>(BracketRemove(listTmp1));
                    list.InsertRange(frontBracket2, listTmp2);
                }
                #endregion
                #region 有括号的第三种情况：a * ( b + c
                else if (backBracket < 0 && frontBracket > 0)
                {
                    int frontBracket2 = list.IndexOf("(");
                    list.RemoveRange(frontBracket2, list.Count - frontBracket2);
                    listTmp1.RemoveRange(0, frontBracket2 + 1);

                    listTmp2 = new List<string>(BracketRemove(listTmp1));
                    list.InsertRange(frontBracket2, listTmp2);
                }
                #endregion
                #region 有括号的第四种情况：a + b )* c
                else if (frontBracket < 0 && backBracket > 0)
                {
                    int backBracket2 = list.IndexOf(")");
                    list.RemoveRange(0, backBracket2 + 1);
                    listTmp1.RemoveRange(backBracket2, listTmp1.Count - backBracket2);

                    listTmp2 = new List<string>(BracketRemove(listTmp1));
                    list.InsertRange(0, listTmp2);
                }
                #endregion
            }
            double temp = MathCal(list);
            list.RemoveRange(0, list.Count);
            list.Add(Convert.ToString(temp));
            return list;
        }
        #endregion

        #region 基本的加减乘除运算（无括号的）
        private double MathCal(List<string> list)
        {
            double result = 0;
            if (list.Count == 1)
            {
                result = Convert.ToDouble(list[0]);
                return result;
            }

            #region 计算乘除法
            while (list.Contains("*") || list.Contains("/"))
            {
                double temp = 0;
                int indexMul = list.IndexOf("*");
                int indexDiv = list.IndexOf("/");

                #region 计算情况一：乘法在前，除法在后，先计算乘法后计算除法
                if (indexMul > 0 && indexMul < indexDiv)
                {
                    //乘法;
                    temp = Convert.ToDouble(list[indexMul - 1]) * Convert.ToDouble(list[indexMul + 1]);
                    list.RemoveRange(indexMul - 1, 3);
                    list.Insert(indexMul - 1, Convert.ToString(temp));

                    //除法;
                    indexDiv = list.IndexOf("/");
                    if (Convert.ToDouble(list[indexDiv + 1]) != 0)
                    {
                        temp = Convert.ToDouble(list[indexDiv - 1]) / Convert.ToDouble(list[indexDiv + 1]);
                    }
                    else
                    {
                        temp = 0;
                    }
                    list.RemoveRange(indexDiv - 1, 3);
                    list.Insert(indexDiv - 1, Convert.ToString(temp));
                }
                #endregion
                #region 计算情况二：除法在前，乘法在后，先计算除法后计算乘法
                else if (indexDiv > 0 && indexDiv < indexMul)
                {
                    //除法;
                    if (Convert.ToDouble(list[indexDiv + 1]) != 0)
                    {
                        temp = Convert.ToDouble(list[indexDiv - 1]) / Convert.ToDouble(list[indexDiv + 1]);
                    }
                    else
                    {
                        temp = 0;
                    }
                    list.RemoveRange(indexDiv - 1, 3);
                    list.Insert(indexDiv - 1, Convert.ToString(temp));

                    //乘法;
                    indexMul = list.IndexOf("*");
                    temp = Convert.ToDouble(list[indexMul - 1]) * Convert.ToDouble(list[indexMul + 1]);
                    list.RemoveRange(indexMul - 1, 3);
                    list.Insert(indexMul - 1, Convert.ToString(temp));
                }
                #endregion
                #region 计算情况三：只有乘法
                else if (indexMul > 0 && indexDiv < 0)
                {
                    //乘法;
                    temp = Convert.ToDouble(list[indexMul - 1]) * Convert.ToDouble(list[indexMul + 1]);
                    list.RemoveRange(indexMul - 1, 3);
                    list.Insert(indexMul - 1, Convert.ToString(temp));
                }
                #endregion
                #region 计算情况四：只有除法
                else if (indexDiv > 0 && indexMul < 0)
                {
                    //除法;
                    if (Convert.ToDouble(list[indexDiv + 1]) != 0)
                    {
                        temp = Convert.ToDouble(list[indexDiv - 1]) / Convert.ToDouble(list[indexDiv + 1]);
                    }
                    else
                    {
                        temp = 0;
                    }
                    list.RemoveRange(indexDiv - 1, 3);
                    list.Insert(indexDiv - 1, Convert.ToString(temp));
                }
                #endregion
            }
            #endregion

            #region 计算加减法
            while (list.Contains("+") || list.Contains("-"))
            {
                double temp = 0;
                int indexAdd = list.IndexOf("+");
                int indexMin = list.IndexOf("-");

                #region 计算情况一：加法在前，减法在后，先计算加法后计算减法
                if (indexAdd > 0 && indexAdd < indexMin)
                {
                    //加法;
                    temp = Convert.ToDouble(list[indexAdd - 1]) + Convert.ToDouble(list[indexAdd + 1]);
                    list.RemoveRange(indexAdd - 1, 3);
                    list.Insert(indexAdd - 1, Convert.ToString(temp));

                    //减法;
                    indexMin = list.IndexOf("-");
                    temp = Convert.ToDouble(list[indexMin - 1]) - Convert.ToDouble(list[indexMin + 1]);
                    list.RemoveRange(indexMin - 1, 3);
                    list.Insert(indexMin - 1, Convert.ToString(temp));
                }
                #endregion
                #region 计算情况二：减法在前，加法在后，先计算减法后计算加法
                else if (indexMin > 0 && indexMin < indexAdd)
                {
                    //减法;
                    temp = Convert.ToDouble(list[indexMin - 1]) - Convert.ToDouble(list[indexMin + 1]);
                    list.RemoveRange(indexMin - 1, 3);
                    list.Insert(indexMin - 1, Convert.ToString(temp));

                    //加法;
                    indexAdd = list.IndexOf("+");
                    temp = Convert.ToDouble(list[indexAdd - 1]) + Convert.ToDouble(list[indexAdd + 1]);
                    list.RemoveRange(indexAdd - 1, 3);
                    list.Insert(indexAdd - 1, Convert.ToString(temp));
                }
                #endregion
                #region 计算情况三：只有加法
                else if (indexMin < 0 && indexAdd > 0)
                {
                    temp = Convert.ToDouble(list[indexAdd - 1]) + Convert.ToDouble(list[indexAdd + 1]);
                    list.RemoveRange(indexAdd - 1, 3);
                    list.Insert(indexAdd - 1, Convert.ToString(temp));
                }
                #endregion
                #region 计算情况四：只有减法
                else if (indexAdd < 0 && indexMin > 0)
                {
                    temp = Convert.ToDouble(list[indexMin - 1]) - Convert.ToDouble(list[indexMin + 1]);
                    list.RemoveRange(indexMin - 1, 3);
                    list.Insert(indexMin - 1, Convert.ToString(temp));
                }
                #endregion

            }
            #endregion

            result = Convert.ToDouble(list[0]);
            return result;
        }
        #endregion

    }
}
