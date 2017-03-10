using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sports_course.DB
{
    /// <summary>
    /// 换课信息
    /// </summary>
    public class TblChangeCourse
    {
        private int changeno;
        /// <summary>
        /// 换课编号
        /// </summary>
        public int Changeno
        {
            get { return changeno; }
            set { changeno = value; }
        }

        private int studentno_A;
        /// <summary>
        /// 学生A编号
        /// </summary>
        public int Studentno_A
        {
            get { return studentno_A; }
            set { studentno_A = value; }
        }

        private int sportcourseno_A;
        /// <summary>
        /// 体育选课A编号
        /// </summary>
        public int Sportcourseno_A
        {
            get { return sportcourseno_A; }
            set { sportcourseno_A = value; }
        }

        private string changechoice;
        /// <summary>
        /// 换课控制(非空,3代表换课已过期或者失效,2代表换课成功的,1代表已收到换课请求,0代表未收到换课请求)
        /// </summary>
        public string Changechoice
        {
            get { return changechoice; }
            set { changechoice = value; }
        }

        private DateTime changecreatetime;
        /// <summary>
        /// 换课创建时间
        /// </summary>
        public DateTime Changecreatetime
        {
            get { return changecreatetime; }
            set { changecreatetime = value; }
        }
    }
}
