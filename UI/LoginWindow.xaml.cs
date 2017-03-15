using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace sports_course
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        List<DB.TblManager> manager = new List<DB.TblManager>();
        List<DB.TblStudent> student = new List<DB.TblStudent>();


        public LoginWindow()
        {
            InitializeComponent();
            studentBTN.IsChecked = true;
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            #region 学生登录验证

            if (studentBTN.IsChecked == true)
            {
                Regex r = new Regex("^[0-9]*$");
                if (r.IsMatch(No.Text.ToString().Trim()) == true)
                {
                    int checkstudentno = Convert.ToInt32(No.Text);
                    string checkstudentpassword = Checkstudentno(checkstudentno);
                    if (checkstudentpassword != "")
                    {
                        if (Password.Password == checkstudentpassword)
                        {
                            StudentWindow student = new StudentWindow();
                            student.studentno = checkstudentno;
                            student.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("您的密码错误!");
                            Password.Focus();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("该账号不存在!");
                        No.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("请输入八位数字的学号!");
                    No.Clear();
                    return;
                }

            }
            #endregion

            #region 管理员登录验证
            else if (managerBTN.IsChecked == true)
            {
                string checkmanagerno = No.Text;
                string checkmanagerpassword = Checkmanagerno(checkmanagerno);
                if (checkmanagerpassword != "")
                {
                    if (Password.Password == checkmanagerpassword)
                    {
                        ManagerWindow manager = new ManagerWindow();
                        manager.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("您的密码错误!");
                        Password.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("该账号不存在!");
                    No.Focus();
                    return;
                }
            }

            #endregion
            else
            {
                MessageBox.Show("请选择您的身份类型!");
                return;
            }
        }

        /// <summary>
        /// 重置登录信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            No.Text = "";
            Password.Password = "";
            studentBTN.IsChecked = false;
            managerBTN.IsChecked = false;
        }

        #region 检验函数
        /// <summary>
        /// 检验管理员登录名
        /// </summary>
        /// <param name="checkmanagerno"></param>
        /// <returns></returns>
        private string Checkmanagerno(string checkmanagerno)
        {
            string password = "";

            for(int i = 0; i < manager.Count; i++)
            {
                if (checkmanagerno == manager[i].Managerno.ToString().Trim())
                {
                    password = manager[i].Managerpassword.ToString().Trim();
                }
                else
                {
                    continue;
                }
            }

            return password;

        }

        /// <summary>
        /// 检验学生登录名
        /// </summary>
        /// <param name="checkstudentno"></param>
        /// <returns></returns>
        private string Checkstudentno(int checkstudentno)
        {
            string password = "";

            for (int i = 0; i < student.Count; i++)
            {
                if (checkstudentno == student[i].Studentno)
                {
                    password = student[i].Studentpassword.ToString().Trim();
                }
                else
                {
                    continue;
                }
            }

            return password;
        }
        #endregion

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region datareader添加管理员信息
            DAL.DbHelper db = new DAL.DbHelper();
            DbCommand selectmanager = db.GetSqlStringCommond("select ManagerNo,Password from Manager");


            using (DbDataReader reader = db.ExecuteReader(selectmanager))
            {
                while (reader.Read())
                {
                    DB.TblManager model = new DB.TblManager();

                    model.Managerno = reader.GetString(0).Trim();
                    model.Managerpassword = reader.GetString(1).Trim();
                    manager.Add(model);
                }
            }
            #endregion

            #region datareader添加学生信息
            DbCommand selectstudent = db.GetSqlStringCommond("select StudentNo,Password from Student");


            using (DbDataReader reader = db.ExecuteReader(selectstudent))
            {
                while (reader.Read())
                {
                    DB.TblStudent model = new DB.TblStudent();

                    model.Studentno = reader.GetInt32(0);
                    model.Studentpassword = reader.GetString(1).Trim();
                    student.Add(model);
                }
            }
            #endregion
        }

        #region 回车应用
        /// <summary>
        /// 回车登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)   //  if (e.KeyValue == 13) 判断是回车键
            {
                this.Confirm.Focus();
                Confirm_Click(sender, e);   //调用登录按钮的事件处理代码
            }
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)   //  if (e.KeyValue == 13) 判断是回车键
            {
                if (studentBTN.IsChecked == true || managerBTN.IsChecked == true)
                {
                    this.Confirm.Focus();
                    Confirm_Click(sender, e);   //调用登录按钮的事件处理代码
                }
                else
                {
                    MessageBox.Show("请选择您的身份类型!");
                    return;
                }
            }
        }
        #endregion
    }
}
