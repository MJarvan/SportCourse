using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sports_course.DB
{
    /// <summary>
    /// 换课确认请求信息
    /// </summary>
    public class TblConfirmCourse
    {
        private int confirmno;
        /// <summary>
        /// 换课确认编号
        /// </summary>
        public int Confirmno
        {
            get { return confirmno; }
            set { confirmno = value; }
        }

        private int changeno;
        /// <summary>
        /// 换课编号
        /// </summary>
        public int Changeno
        {
            get { return changeno; }
            set { changeno = value; }
        }

        private int studentno_B;
        /// <summary>
        /// 学生A编号
        /// </summary>
        public int Studentno_B
        {
            get { return studentno_B; }
            set { studentno_B = value; }
        }

        private int sportcourseno_B;
        /// <summary>
        /// 体育选课A编号
        /// </summary>
        public int Sportcourseno_B
        {
            get { return sportcourseno_B; }
            set { sportcourseno_B = value; }
        }

        private string confirmchoice;
        /// <summary>
        /// 换课控制(非空,3代表换课已过期或者失效,2代表换课成功的,1代表已收到换课请求,0代表未收到换课请求)
        /// </summary>
        public string Confirmchoice
        {
            get { return confirmchoice; }
            set { confirmchoice = value; }
        }

        private DateTime confirmcreatetime;
        /// <summary>
        /// 换课创建时间
        /// </summary>
        public DateTime Confirmcreatetime
        {
            get { return confirmcreatetime; }
            set { confirmcreatetime = value; }
        }
    }
}
