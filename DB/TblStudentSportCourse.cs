using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sports_course.DB
{
    /// <summary>
    /// 学生体育选课信息
    /// </summary>
    public class TblStudentSportCourse
    {
        private int ssno;
        /// <summary>
        /// 学生体育选课序号
        /// </summary>
        public int Ssno
        {
            get { return ssno; }
            set { ssno = value; }
        }

        private int studentno;
        /// <summary>
        /// 学生编号
        /// </summary>
        public int Studentno
        {
            get { return studentno; }
            set { studentno = value; }
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

        private string sschoice;
        /// <summary>
        /// 选课控制(非空,3代表成功退课,2代表换课中的,1代表成功选课,0代表选中)
        /// </summary>
        public string SSchoice
        {
            get { return sschoice; }
            set { sschoice = value; }
        }
    }
}
