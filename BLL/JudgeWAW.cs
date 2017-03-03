using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sports_course.BLL
{
    /// <summary>
    /// 判断WeekAndWhen
    /// </summary>
    public class JudgeWAW
    {
        public static string Judge(string waw)
        {
            string result = null;

            if (waw == "11")
            {
                result = "周一第一节";
            }
            else if (waw == "12")
            {
                result = "周一第二节";
            }
            else if (waw == "13")
            {
                result = "周一第三节";
            }
            else if (waw == "14")
            {
                result = "周一第四节";
            }
            else if (waw == "15")
            {
                result = "周一第五节";
            }
            else if (waw == "21")
            {
                result = "周二第一节";
            }
            else if (waw == "22")
            {
                result = "周二第二节";
            }
            else if (waw == "23")
            {
                result = "周二第三节";
            }
            else if (waw == "24")
            {
                result = "周二第四节";
            }
            else if (waw == "25")
            {
                result = "周二第五节";
            }
            else if (waw == "31")
            {
                result = "周三第一节";
            }
            else if (waw == "32")
            {
                result = "周三第二节";
            }
            else if (waw == "33")
            {
                result = "周三第三节";
            }
            else if (waw == "34")
            {
                result = "周三第四节";
            }
            else if (waw == "35")
            {
                result = "周三第五节";
            }
            else if (waw == "41")
            {
                result = "周四第一节";
            }
            else if (waw == "42")
            {
                result = "周四第二节";
            }
            else if (waw == "43")
            {
                result = "周四第三节";
            }
            else if (waw == "44")
            {
                result = "周四第四节";
            }
            else if (waw == "45")
            {
                result = "周四第五节";
            }
            else if (waw == "51")
            {
                result = "周五第一节";
            }
            else if (waw == "52")
            {
                result = "周五第二节";
            }
            else if (waw == "53")
            {
                result = "周五第三节";
            }
            else if (waw == "54")
            {
                result = "周五第四节";
            }
            else if (waw == "55")
            {
                result = "周五第五节";
            }

            return result;
        }
    }
}
