using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sports_course.BLL
{
    public class SSCCompare
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
    }
}
