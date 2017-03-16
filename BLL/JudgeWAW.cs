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

            switch (waw)
            {
                case "11":
                    {
                        result = "周一第一节";
                        break;
                    }
                case "12":
                    {
                        result = "周一第二节";
                        break;
                    }
                case "13":
                    {
                        result = "周一第三节";
                        break;
                    }
                case "14":
                    {
                        result = "周一第四节";
                        break;
                    }
                case "21":
                    {
                        result = "周二第一节";
                        break;
                    }
                case "22":
                    {
                        result = "周二第二节";
                        break;
                    }
                case "23":
                    {
                        result = "周二第三节";
                        break;
                    }
                case "24":
                    {
                        result = "周二第四节";
                        break;
                    }
                case "31":
                    {
                        result = "周三第一节";
                        break;
                    }
                case "32":
                    {
                        result = "周三第二节";
                        break;
                    }
                case "33":
                    {
                        result = "周三第三节";
                        break;
                    }
                case "34":
                    {
                        result = "周三第四节";
                        break;
                    }
                case "41":
                    {
                        result = "周四第一节";
                        break;
                    }
                case "42":
                    {
                        result = "周四第二节";
                        break;
                    }
                case "43":
                    {
                        result = "周四第三节";
                        break;
                    }
                case "44":
                    {
                        result = "周四第四节";
                        break;
                    }
                case "51":
                    {
                        result = "周五第一节";
                        break;
                    }
                case "52":
                    {
                        result = "周五第二节";
                        break;
                    }
                case "53":
                    {
                        result = "周五第三节";
                        break;
                    }
                case "54":
                    {
                        result = "周五第四节";
                        break;
                    }
            }

            return result;
        }
    }
}
