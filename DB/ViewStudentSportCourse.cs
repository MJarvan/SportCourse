using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sports_course.DB
{
    /// <summary>
    /// 学生体育选课信息视图
    /// </summary>
    public class ViewStudentSportCourse
    {

        private int studentno;
        /// <summary>
        /// 学生编号
        /// </summary>
        public int Studentno
        {
            get { return studentno; }
            set { studentno = value; }
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

        private int sportcourseno;
        /// <summary>
        /// 体育选课编号
        /// </summary>
        public int Sportcourseno
        {
            get { return sportcourseno; }
            set { sportcourseno = value; }
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

        private string teachername;
        /// <summary>
        /// 教师名称
        /// </summary>
        public string Teachername
        {
            get { return teachername; }
            set { teachername = value; }
        }

        private string sschoice;
        /// <summary>
        /// 选课控制(非空,3代表成功退课,2代表换课中的,1代表成功选课,0代表选中)
        /// </summary>
        public string SSchoice
        {
            get { return sschoice; }
            set { sschoice = value; }
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
    }
}
