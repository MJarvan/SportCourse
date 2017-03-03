using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sports_course.BLL
{
    /// <summary>
    /// 缓存学生选课信息
    /// </summary>
    public class StudentSportChoiceNo
    {

        private static int sportcoursenoA = 0;
        /// <summary>
        /// 体育选课编号A
        /// </summary>
        public static int SportcoursenoA
        {
            get { return sportcoursenoA; }
            set { sportcoursenoA = value; }
        }

        private static int sportcoursenoB = 0;
        /// <summary>
        /// 体育选课编号B
        /// </summary>
        public static int SportcoursenoB
        {
            get { return sportcoursenoB; }
            set { sportcoursenoB = value; }
        }
    }
}
