using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sports_course.BLL
{
    /// <summary>
    /// 时间计算
    /// </summary>
    public class TimeCount
    {
        public int time (DateTime startTime)
        {
            //获取当前时间
            DateTime endTime = new DateTime();
            endTime = DateTime.Now;

            //计算时间差
            TimeSpan ts = startTime - endTime;
            int time;
            time = ((int)ts.TotalMinutes);

            //返回
            return time;
        }
    }
}
