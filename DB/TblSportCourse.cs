using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sports_course.DB
{
    /// <summary>
    /// 体育选课信息
    /// </summary>
    public class TblSportCourse
    {
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

        private string weekandwhen;
        /// <summary>
        /// 周几第几节
        /// </summary>
        public string Weekandwhen
        {
            get { return weekandwhen; }
            set { weekandwhen = value; }
        }

        private int choicenummax;
        /// <summary>
        /// 最大选择人数
        /// </summary>
        public int Choicenummax
        {
            get { return choicenummax; }
            set { choicenummax = value; }
        }

        private int choicenumbefore;
        /// <summary>
        /// 已选人数(筛选前,选课用)
        /// </summary>
        public int Choicenumbefore
        {
            get { return choicenumbefore; }
            set { choicenumbefore = value; }
        }

        private int choicenumafter;
        /// <summary>
        /// 已选人数(筛选后,抢课用)
        /// </summary>
        public int Choicenumafter
        {
            get { return choicenumafter; }
            set { choicenumafter = value; }
        }

        private int teacherno;
        /// <summary>
        /// 教师编号
        /// </summary>
        public int Teacherno
        {
            get { return teacherno; }
            set { teacherno = value; }
        }

        private string isselected;
        /// <summary>
        /// 是否选中
        /// </summary>
        public string Isselected
        {
            get { return isselected; }
            set { isselected = value; }
        }
    }
}
