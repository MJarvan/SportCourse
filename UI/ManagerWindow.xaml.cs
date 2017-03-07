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
using System.Windows.Shapes;
using System.Data.Common;
using System.Data;
using sports_course.DAL;
using System.ComponentModel;

namespace sports_course
{
    /// <summary>
    /// ManagerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ManagerWindow : Window
    {
        //数据访问
        List<BLL.CoursecControl> control = new List<BLL.CoursecControl>();
        List<DB.TblSportCourse> sportcourse = new List<DB.TblSportCourse>();
        List<BLL.SSCCompare> result = new List<BLL.SSCCompare>();
        DataTable dt = new DataTable();//StudentSportCourse的datatable
        DataTable view = new DataTable();//ViewStudentSportCourse

        public ManagerWindow()
        {
            InitializeComponent();
            query.Clear();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region datareader添加课程控制信息
            DAL.DbHelper db = new DAL.DbHelper();
            DbCommand selectCC = db.GetSqlStringCommond("select * from CourseControl");


            using (DbDataReader reader = db.ExecuteReader(selectCC))
            {
                while (reader.Read())
                {
                    BLL.CoursecControl model = new BLL.CoursecControl();

                    model.Choicecontrol = reader.GetInt32(0);
                    model.Grabcontrol = reader.GetInt32(1);
                    model.Changecontrol = reader.GetInt32(2);
                    control.Add(model);
                }
            }
            #endregion

            #region dataset添加学生体育选课信息

            DbCommand selectSSC = db.GetSqlStringCommond("select * from StudentSportCourse");
            dt = db.ExecuteDataTable(selectSSC);
            int RowsCount = dt.Rows.Count;

            //向table里增加多一列控制属性 
            dt.Columns.Add("Check");
            for (int j = 0; j < RowsCount; j++)//为该列增加相应的数值  
            {
                dt.Rows[j]["Check"] = 0;
            }
            #endregion

            #region datareader添加学生体育选课编号
            DbCommand selectSC = db.GetSqlStringCommond("select * from SportCourse");


            using (DbDataReader reader = db.ExecuteReader(selectSC))
            {
                while (reader.Read())
                {
                    DB.TblSportCourse model = new DB.TblSportCourse();

                    model.Sportcourseno = reader.GetInt32(0);
                    model.Choicenummax = reader.GetInt32(3);
                    model.Choicenumbefore = reader.GetInt32(4);
                    model.Choicenumafter = reader.GetInt32(5);
                    sportcourse.Add(model);
                }
            }
            #endregion

            Judge();

            Set();
        }

        #region 界面操作

        /// <summary>
        /// 刷新界面
        /// </summary>
        private void Set()
        {
            if (control[0].Choicecontrol == 2)
            {
                SSC_Loaded();
                Random.Visibility = Visibility.Collapsed;
                DropCourse.Visibility = Visibility.Visible;
                OpenChoice.Visibility = Visibility.Collapsed;
                CloseChoice.Visibility = Visibility.Collapsed;
            }
            else if (control[0].Choicecontrol == 1)
            {
                Random.Visibility = Visibility.Visible;
                DropCourse.Visibility = Visibility.Collapsed;
                OpenChoice.Visibility = Visibility.Collapsed;
                CloseChoice.Visibility = Visibility.Visible;
            }
            else
            {
                Random.Visibility = Visibility.Collapsed;
                DropCourse.Visibility = Visibility.Collapsed;
                OpenChoice.Visibility = Visibility.Visible;
                CloseChoice.Visibility = Visibility.Collapsed;
            }

            if(control[0].Grabcontrol == 1)
            {
                OpenGrab.Visibility = Visibility.Collapsed;
                CloseGrab.Visibility = Visibility.Visible;
            }
            else
            {
                OpenGrab.Visibility = Visibility.Visible;
                CloseGrab.Visibility = Visibility.Collapsed;
            }

            if(control[0].Changecontrol == 1)
            {
                OpenChange.Visibility = Visibility.Collapsed;
                CloseChange.Visibility = Visibility.Visible;
            }
            else
            {
                OpenChange.Visibility = Visibility.Visible;
                CloseChange.Visibility = Visibility.Collapsed;
            }
        }

        private void SSC_Loaded()
        {
            #region dataset添加视图数据
            DAL.DbHelper db = new DAL.DbHelper();
            DbCommand selectview = db.GetSqlStringCommond("select * from Student_Sportcourse where SSChoice= @SSChoice");
            db.AddInParameter(selectview, "@SSChoice", DbType.String, "1");

            view = db.ExecuteDataTable(selectview);
            sportcourseview.ItemsSource = view.DefaultView;
            #endregion
        }

        /// <summary>
        /// 快捷显示管理信息
        /// </summary>
        private void Judge()
        {
            if (control[0].Choicecontrol == 2)
            {
                txtchoice.Text = "选课已完成";
            }
            else if (control[0].Choicecontrol == 1)
            {
                txtchoice.Text = "选课已开放";
            }
            else if (control[0].Choicecontrol == 0)
            {
                txtchoice.Text = "选课已关闭";
            }

            if (control[0].Grabcontrol == 1)
            {
                txtgrab.Text = "抢课已开放";
            }
            else if (control[0].Grabcontrol == 0)
            {
                txtgrab.Text = "抢课已关闭";
            }

            if (control[0].Changecontrol == 1)
            {
                txtchange.Text = "换课已开放";
            }
            else if (control[0].Changecontrol == 0)
            {
                txtchange.Text = "换课已关闭";
            }
        }

        /// <summary>
        /// 快捷栏操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Quick_Click(object sender, RoutedEventArgs e)
        {
            if (youce.Content.ToString() == "隐藏右侧快捷栏")
            {
                youce.Content = "显示右侧快捷栏";
                quick.Visibility = Visibility.Collapsed;
            }
            else if (youce.Content.ToString() == "显示右侧快捷栏")
            {
                youce.Content = "隐藏右侧快捷栏";
                quick.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// 注销返回登录界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void return_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("是否注销?", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes))//如果点击“确定”按钮
            {
                LoginWindow login = new LoginWindow();
                login.Show();
                this.Close();
            }
            else//如果点击“取消”按钮
            {
                return;
            }
        }

        /// <summary>
        /// 关闭系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void shutdown_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("是否退出系统?", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes))//如果点击“确定”按钮
            {
                Application.Current.Shutdown();
            }
            else//如果点击“取消”按钮
            {
                return;
            }
        }

        #endregion

        #region 课程控制

        /// <summary>
        /// 快捷栏课程控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenorClose_Click(object sender, RoutedEventArgs e)
        {
            string item = ((Button)sender).Name;

            //数据库操作
            DAL.DbHelper db = new DAL.DbHelper();
            DbCommand updateChoice = db.GetSqlStringCommond("update CourseControl set ChoiceControl=" + control[0].Choicecontrol);
            DbCommand updateGrab = db.GetSqlStringCommond("update CourseControl set GrabControl=" + control[0].Grabcontrol);
            DbCommand updateChange = db.GetSqlStringCommond("update CourseControl set ChangeControl=" + control[0].Changecontrol);

            if (item == "OpenChoice")
            {
                control[0].Choicecontrol = 1;
                //update [表名] set [字段名]=[值] Where [条件]
                //"update CourseControl set ChoiceControl=' "+control[0].Choicecontrol+" '";
                int i = db.ExecuteNonQuery(updateChoice);
                if(i > 0)
                {
                    MessageBox.Show("开放选课成功!");
                }
                else
                {
                    MessageBox.Show("开放选课失败!");
                }

            }
            else if (item == "CloseChoice")
            {
                control[0].Choicecontrol = 0;
                int i = db.ExecuteNonQuery(updateChoice);
                if (i > 0)
                {
                    MessageBox.Show("关闭选课成功!");
                }
                else
                {
                    MessageBox.Show("关闭选课失败!");
                }
            }
            else if (item == "OpenGrab")
            {
                control[0].Grabcontrol = 1;
                int i = db.ExecuteNonQuery(updateGrab);
                if (i > 0)
                {
                    MessageBox.Show("开放抢课成功!");
                }
                else
                {
                    MessageBox.Show("开放抢课失败!");
                }
            }
            else if (item == "CloseGrab")
            {
                control[0].Grabcontrol = 0;
                int i = db.ExecuteNonQuery(updateGrab);
                if (i > 0)
                {
                    MessageBox.Show("关闭抢课成功!");
                }
                else
                {
                    MessageBox.Show("关闭抢课失败!");
                }
            }
            else if (item == "OpenChange")
            {
                control[0].Changecontrol = 1;
                int i = db.ExecuteNonQuery(updateChange);
                if (i > 0)
                {
                    MessageBox.Show("开放换课成功!");
                }
                else
                {
                    MessageBox.Show("开放换课失败!");
                }
            }
            else if (item == "CloseChange")
            {
                control[0].Changecontrol = 0;
                int i = db.ExecuteNonQuery(updateChange);
                if (i > 0)
                {
                    MessageBox.Show("关闭换课成功!");
                }
                else
                {
                    MessageBox.Show("关闭换课失败!");
                }
            }

            Judge();
            Set();
        }


        /// <summary>
        /// 菜单栏课程控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            string item = ((MenuItem)sender).Name;
            youce.Content = "隐藏右侧快捷栏";
            quick.Visibility = Visibility.Visible;
            if (item == "openchoice")
            {
                if(OpenChoice.IsVisible == true)
                {
                    MessageBox.Show("请在快捷栏操作!");
                }
                else
                {
                    MessageBox.Show("选课已开启!");
                }
            }
            else if (item == "closechoice")
            {
                if (CloseChoice.IsVisible == true)
                {
                    MessageBox.Show("请在快捷栏操作!");
                }
                else
                {
                    MessageBox.Show("选课已完成或者已关闭!");
                }
            }
            else if (item == "opengrab")
            {
                if (OpenGrab.IsVisible == true)
                {
                    MessageBox.Show("请在快捷栏操作!");
                }
                else
                {
                    MessageBox.Show("抢课已开启!");
                }
            }
            else if (item == "closegrab")
            {
                if (CloseGrab.IsVisible == true)
                {
                    MessageBox.Show("请在快捷栏操作!");
                }
                else
                {
                    MessageBox.Show("抢课已完成或者已关闭!");
                }
            }
            else if (item == "openchange")
            {
                if (OpenChange.IsVisible == true)
                {
                    MessageBox.Show("请在快捷栏操作!");
                }
                else
                {
                    MessageBox.Show("换课已开启!");
                }
            }
            else if (item == "closechange")
            {
                if (CloseChange.IsVisible == true)
                {
                    MessageBox.Show("请在快捷栏操作!");
                }
                else
                {
                    MessageBox.Show("换课已完成或者已关闭!");
                }
            }

            Judge();

        }

        #endregion

        #region 选课策略
        /// <summary>
        /// 筛选课程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Random_Click(object sender, RoutedEventArgs e)
        {
            BLL.RandomizedAlgorithm go = new BLL.RandomizedAlgorithm();
            int num = 0;
            int choicenummax = 0;
            int choicenumbefore = 0;
            //int choicenumafter = 0;
            int sportcourseno = 0;
            //添加课表数组
            DataRow[] dr = dt.Select("SSChoice = 0");
            int i = 0;
            foreach (DataRow row in dr)
            {
                for (int j = 0; j < sportcourse.Count; j++)
                {
                    if ((int)dt.Rows[i]["SportCourseNo"] == sportcourse[j].Sportcourseno && dt.Rows[i]["SSChoice"].ToString().Trim() == "0" && dt.Rows[i]["Check"].ToString().Trim() == "0")
                    {
                        sportcourseno = (int)dt.Rows[i]["SportCourseNo"];
                        choicenummax = sportcourse[j].Choicenummax;
                        choicenumbefore = sportcourse[j].Choicenumbefore;
                        StartRandom(sportcourseno, choicenummax, choicenumbefore);
                    }
                }
                i++;
            }

            result = result.Distinct(m => m.Studentno).ToList();

            //把算法返回的SSNo的SSChoice改成1并且输出结果
            for (int k = 0; k < result.Count; k++)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (result[k].Ssno == (int)dt.Rows[j]["SSNo"])
                    {
                        int StudentNo = (int)dt.Rows[j]["StudentNo"];
                        int SportCourseNo = (int)dt.Rows[j]["SportCourseNo"];
                        num = num + DoChoiceBussiness(StudentNo, SportCourseNo);
                        dt.Rows[j]["SSChoice"] = 1;
                    }
                }
            }

            if( num > 0 && num == result.Count)
            {
                MessageBox.Show("极大成功");
            }
            else if (num == 0)
            {
                MessageBox.Show("失败");
            }
            else
            {
                MessageBox.Show("成功");
            }

            Set();
        }

        /// <summary>
        /// 事务判断
        /// </summary>
        /// <param name="SSno"></param>
        /// <param name="SportCourseNo"></param>
        private int DoChoiceBussiness(int StudentNo, int SportCourseNo)
        {
            int i = 0;
            using (DAL.Trans t = new DAL.Trans())
            {
                try
                {
                    BLL.DoBussiness.D1(t, StudentNo, SportCourseNo, 3);
                    BLL.DoBussiness.D5(t, SportCourseNo,1);
                    BLL.DoBussiness.D6(t);
                    i = 1;
                    t.Commit();
                }
                catch
                {
                    t.RollBack();
                }
            }
            return i;
        }

        /// <summary>
        /// 随机算法
        /// </summary>
        /// <param name="sportcourseno"></param>
        /// <param name="choicenummax"></param>
        /// <param name="choicenumbefore"></param>
        private void StartRandom(int sportcourseno, int choicenummax, int choicenumbefore)
        {

            //选出SSno进行随机筛选算法的数组
            int[] a = new int[10];
            //算法返回result的数组
            int[] b = new int[choicenummax];

            if (choicenumbefore <= choicenummax)
            {
                //选课人数少于选课总人数,不进行随机筛选,直接加进结果
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (sportcourseno == (int)dt.Rows[i]["SportCourseNo"])
                    {
                        BLL.SSCCompare model = new BLL.SSCCompare();
                        model.Ssno = (int)dt.Rows[i]["SSNo"];
                        model.Studentno = (int)dt.Rows[i]["StudentNo"];
                        result.Add(model);
                        dt.Rows[i]["SSChoice"] = 1;
                        dt.Rows[i]["Check"] = 1;
                    }
                }

            }
            else
            {
                //选课人数多于选课总人数,进行随机筛选,a[]数组存放需要进行筛选的SSNo,b[]数组接收筛选后的数组,在加入结果
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (sportcourseno == (int)dt.Rows[i]["SportCourseNo"])
                    {
                        for (int j = 0; j < a.Length; j++)
                        {
                            if (a[j] == 0)
                            {
                                a[j] = (int)dt.Rows[i]["SSNo"];
                                dt.Rows[i]["Check"] = 1;
                                break;
                            }
                        }
                    }
                }

                //执行算法,将b[]数组的信息存进result
                BLL.RandomizedAlgorithm go = new BLL.RandomizedAlgorithm();
                b = go.Random(a, choicenummax);
                for (int i = 0; i < b.Length; i++)
                {
                    if (b[i] != 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (b[i] == (int)dt.Rows[j]["SSNo"])
                            {
                                BLL.SSCCompare model = new BLL.SSCCompare();
                                model.Ssno = (int)dt.Rows[j]["SSNo"];
                                model.Studentno = (int)dt.Rows[j]["StudentNo"];
                                result.Add(model);
                            }
                        }
                    }
                }

                Array.Clear(a, 0, a.Length);
                Array.Clear(b, 0, b.Length);
            }
        }


        #endregion

        #region 过滤查询

        /// <summary>
        /// 过滤查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quertBTN_Click(object sender, RoutedEventArgs e)
        {
            string txt = query.Text.Trim();

            if (txt == "" || txt == null)
            {
                MessageBox.Show("请输入您要查询的内容!");
                return;
            }
            else if (noBTN.IsChecked == true)
            {
                if (txt.Length == 6)
                {
                    IBindingListView blv = view.DefaultView;
                    blv.Filter = "SportCourseNo = '" + txt + "'";
                    sportcourseview.ItemsSource = blv;
                }
                else
                {
                    MessageBox.Show("请输入六位数的课程编号!");
                    Reset();
                    return;
                }
            }
            else if (nameBTN.IsChecked == true)
            {
                if (txt.Length == 2 || txt.Length == 3)
                {
                    IBindingListView blv = view.DefaultView;
                    blv.Filter = "SportCourseName = '" + txt + "'";
                    sportcourseview.ItemsSource = blv;
                }
                else
                {
                    MessageBox.Show("请输入正确的课程名称!");
                    Reset();
                    return;
                }
            }
            else
            {
                MessageBox.Show("请选择要查询的类型!");
                return;
            }
        }

        /// <summary>
        /// 重置查询信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resetBTN_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }
        private void Reset()
        {
            query.Text = null;
            IBindingListView blv = view.DefaultView;
            blv.Filter = null;
            sportcourseview.ItemsSource = blv;
        }

        #endregion

        #region 退课

        /// <summary>
        /// 退课确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Drop_Click(object sender, RoutedEventArgs e)
        {
            int num = 0;
            DataRowView dr = (DataRowView)sportcourseview.SelectedItem;
            if(dr == null)
            {
                MessageBox.Show("请选中要退课的行!");
                return;
            }
            else
            {
                int StudentNo = (int)dr.Row["StudentNo"];
                int SportCourseNo = (int)dr.Row["SportCourseNo"];
                num = DoDropBussiness(StudentNo, SportCourseNo);
            }

            if (num == 1)
            {
                dr.Delete();
                dt.AcceptChanges();
                MessageBox.Show("退课成功!");
            }
            else
            {
                MessageBox.Show("退课失败!");
                return;
            }

            Reset();
        }

        private int DoDropBussiness(int studentNo, int sportCourseNo)
        {
            int i = 0;
            using (DAL.Trans t = new DAL.Trans())
            {
                try
                {
                    BLL.DoBussiness.D1(t, studentNo, sportCourseNo, 4);
                    BLL.DoBussiness.D5(t, sportCourseNo, 2);
                    BLL.DoBussiness.D7(t, studentNo, sportCourseNo);
                    i++;
                    t.Commit();
                }
                catch
                {
                    t.RollBack();
                }
            }
            return i;
        }



        #endregion

    }
}
