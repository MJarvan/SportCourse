using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sports_course.DB
{
    /// <summary>
    /// 主修课程信息
    /// </summary>
    public class TblMainCourse
    {
        private int maincourseno;
        /// <summary>
        /// 主修课程编号
        /// </summary>
        public int Maincourseno
        {
            get { return maincourseno; }
            set { maincourseno = value; }
        }

        private string maincoursename;
        /// <summary>
        /// 主修课程名称
        /// </summary>
        public string Maincoursename
        {
            get { return maincoursename; }
            set { maincoursename = value; }
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

        private int majorno;
        /// <summary>
        /// 专业编号
        /// </summary>
        public int Majorno
        {
            get { return majorno; }
            set { majorno = value; }
        }
    }
}
