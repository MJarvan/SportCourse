using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sports_course.DB
{
    /// <summary>
    /// 学生换课视图
    /// </summary>
    public class ViewStudentChangeCourse
    {

        private int studentnoA;
        /// <summary>
        /// 学生A编号
        /// </summary>
        public int StudentnoA
        {
            get { return studentnoA; }
            set { studentnoA = value; }
        }

        private string studentname;
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string Studentname
        {
            get { return studentname; }
            set { studentname = value; }
        }

        private int sportcoursenoA;
        /// <summary>
        /// 体育选课A编号
        /// </summary>
        public int SportcoursenoA
        {
            get { return sportcoursenoA; }
            set { sportcoursenoA = value; }
        }

        private string sportcoursename;
        /// <summary>
        /// 体育选课名称
        /// </summary>
        public string Sportcoursename
        {
            get { return sportcoursename; }
            set { sportcoursename = value; }
        }

        private string weekandwhen;
        /// <summary>
        /// 周几第几节
        /// </summary>
        public string Weekandwhen
        {
            get { return weekandwhen; }
            set { weekandwhen = value; }
        }

        private string changechoice;
        /// <summary>
        /// 换课控制(非空,2代表成功换课,1代表已收到换课确认请求,0代表没收到换课确认请求)
        /// </summary>
        public string Changechoice
        {
            get { return changechoice; }
            set { changechoice = value; }
        }

    }
}
