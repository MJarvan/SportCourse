using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sports_course.DB
{
    /// <summary>
    /// 学生信息
    /// </summary>
    public class TblStudent
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

        private string studentpassword;
        /// <summary>
        /// 学生密码
        /// </summary>
        public string Studentpassword
        {
            get { return studentpassword; }
            set { studentpassword = value; }
        }

        private int changecontrol;
        /// <summary>
        /// 选课请求发送控制
        /// </summary>
        public int Changecontrol
        {
            get { return changecontrol; }
            set { changecontrol = value; }
        }

        private int confirmcontrol;
        /// <summary>
        /// 选课确认请求发送控制
        /// </summary>
        public int Confirmcontrol
        {
            get { return confirmcontrol; }
            set { confirmcontrol = value; }
        }

        private int choicecontrol;
        /// <summary>
        /// 选课总数控制
        /// </summary>
        public int Choicecontrol
        {
            get { return choicecontrol; }
            set { choicecontrol = value; }
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
