using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sports_course.DB
{
    /// <summary>
    /// 教师信息
    /// </summary>
    public class TblTeacher
    {
        private int teacherno;
        /// <summary>
        /// 教师编号
        /// </summary>
        public int Teacherno
        {
            get { return teacherno; }
            set { teacherno = value; }
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
    }
}
