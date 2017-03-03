using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sports_course.DB
{
    /// <summary>
    /// 专业信息
    /// </summary>
    public class TblMajor
    {
        private int majorno;
        /// <summary>
        /// 专业编号
        /// </summary>
        public int Majorno
        {
            get { return majorno; }
            set { majorno = value; }
        }

        private string majorname;
        /// <summary>
        /// 专业名称
        /// </summary>
        public string Majorname
        {
            get { return majorname; }
            set { majorname = value; }
        }
    }
}
