using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sports_course.DB
{
    /// <summary>
    /// 管理员信息
    /// </summary>
    public class TblManager
    {
        private string managerno;
        /// <summary>
        /// 管理员编号
        /// </summary>
        public string Managerno
        {
            get { return managerno; }
            set { managerno = value; }
        }

        private string managerpassword;
        /// <summary>
        /// 管理员密码
        /// </summary>
        public string Managerpassword
        {
            get { return managerpassword; }
            set { managerpassword = value; }
        }

        private string managername;
        /// <summary>
        /// 管理员姓名
        /// </summary>
        public string Managername
        {
            get { return managername; }
            set { managername = value; }
        }
    }
}
