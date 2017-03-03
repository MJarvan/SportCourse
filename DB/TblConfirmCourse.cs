using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sports_course.DB
{
    /// <summary>
    /// 学生换课确认请求控制表
    /// </summary>
    public class TblConfirmCourse
    {
        private int confirmno;
        /// <summary>
        /// 学生体育换课确认序号
        /// </summary>
        public int Confirmno
        {
            get { return confirmno; }
            set { confirmno = value; }
        }

        private int changeno;
        /// <summary>
        /// 学生体育换课序号
        /// </summary>
        public int Changeno
        {
            get { return changeno; }
            set { changeno = value; }
        }

        private int studentnoB;
        /// <summary>
        /// 学生B编号
        /// </summary>
        public int StudentnoB
        {
            get { return studentnoB; }
            set { studentnoB = value; }
        }

        private int sportcoursenoA;
        /// <summary>
        /// 体育选课B编号
        /// </summary>
        public int SportcoursenoA
        {
            get { return sportcoursenoA; }
            set { sportcoursenoA = value; }
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
        /// 换课确认控制(非空,2代表成功换课,1代表已收到换课确认请求,0代表没收到换课确认请求)
        /// </summary>
        public string Changechoice
        {
            get { return changechoice; }
            set { changechoice = value; }
        }

        private DateTime changecreatetime;
        /// <summary>
        /// 学生换课确认请求创建时间
        /// </summary>
        public DateTime Changecreatetime
        {
            get { return changecreatetime; }
            set { changecreatetime = value; }
        }
    }
}
