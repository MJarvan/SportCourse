using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sports_course.BLL
{
    /// <summary>
    /// 课程控制
    /// </summary>
    public class CoursecControl
    {
        private int choicecontrol;
        /// <summary>
        /// 选课开放或关闭控制
        /// </summary>
        public int Choicecontrol
        {
            get { return choicecontrol; }
            set { choicecontrol = value; }
        }

        private int grabcontrol;
        /// <summary>
        /// 抢课开放或关闭控制
        /// </summary>
        public int Grabcontrol
        {
            get { return grabcontrol; }
            set { grabcontrol = value; }
        }

        private int changecontrol;
        /// <summary>
        /// 换课开放或关闭控制
        /// </summary>
        public int Changecontrol
        {
            get { return changecontrol; }
            set { changecontrol = value; }
        }
    }
}
