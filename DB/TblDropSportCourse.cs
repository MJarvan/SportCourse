using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sports_course.DB
{
    /// <summary>
    /// 学生体育退课表
    /// </summary>
    public class TblDropSportCourse
    {
        private int dsno;
        /// <summary>
        /// 学生体育退课序号
        /// </summary>
        public int Dsno
        {
            get { return dsno; }
            set { dsno = value; }
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
    }
}
