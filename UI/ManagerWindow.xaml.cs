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
        #region 变量

        //数据访问
        List<BLL.CoursecControl> control = new List<BLL.CoursecControl>();
        List<DB.TblSportCourse> sportcourse = new List<DB.TblSportCourse>();
        List<BLL.SSCCompare> result = new List<BLL.SSCCompare>();

        DataTable dt = new DataTable();//StudentSportCourse的datatable
        DataTable view = new DataTable();//ViewStudentSportCourse
        DataTable viewchange = new DataTable();//ChangeCourse的datatable
        DataTable viewconfirm = new DataTable();//ConfirmCourse的datatable

        #endregion

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

            DeleteChangeCourse();

            DeleteConfirmCourse();

            Judge();

            Set();
        }

        /// <summary>
        /// 加载换课确认失效记录
        /// </summary>
        private void DeleteConfirmCourse()
        {
            viewconfirm.Clear();

            DAL.DbHelper db = new DAL.DbHelper();
            #region datareader加载换课视图信息

            DbCommand selectSCC2 = db.GetSqlStringCommond("select * from ConfirmCourse where ConfirmChoice =@ConfirmChoice");
            db.AddInParameter(selectSCC2, "@ConfirmChoice", DbType.String, "2");
            viewconfirm = db.ExecuteDataTable(selectSCC2);
            deleteconfirm.ItemsSource = viewconfirm.DefaultView;
            #endregion
        }

        /// <summary>
        /// 加载换课失效记录
        /// </summary>
        private void DeleteChangeCourse()
        {
            viewchange.Clear();

            DAL.DbHelper db = new DAL.DbHelper();
            #region datareader加载换课视图信息

            DbCommand selectSCC1 = db.GetSqlStringCommond("select * from ChangeCourse where ChangeChoice =@ChangeChoice");
            db.AddInParameter(selectSCC1, "@ChangeChoice", DbType.String, "3");
            viewchange = db.ExecuteDataTable(selectSCC1);
            deletechange.ItemsSource = viewchange.DefaultView;
            #endregion
        }

        #region 界面操作

        /// <summary>
        /// 刷新界面
        /// </summary>
        private void Set()
        {
            switch (control[0].Choicecontrol)
            {
                case 2:
                    {
                        SSC_Loaded();
                        Random.Visibility = Visibility.Collapsed;
                        DropCourse.Visibility = Visibility.Visible;
                        exportBTN.Visibility = Visibility.Visible;
                        OpenChoice.Visibility = Visibility.Collapsed;
                        CloseChoice.Visibility = Visibility.Collapsed;
                        break;
                    }
                case 1:
                    {
                        Random.Visibility = Visibility.Visible;
                        DropCourse.Visibility = Visibility.Collapsed;
                        exportBTN.Visibility = Visibility.Collapsed;
                        OpenChoice.Visibility = Visibility.Collapsed;
                        CloseChoice.Visibility = Visibility.Visible;
                        break;
                    }
                default:
                    {
                        Random.Visibility = Visibility.Collapsed;
                        DropCourse.Visibility = Visibility.Collapsed;
                        exportBTN.Visibility = Visibility.Collapsed;
                        OpenChoice.Visibility = Visibility.Visible;
                        CloseChoice.Visibility = Visibility.Collapsed;
                        break;
                    }
            }

            switch (control[0].Grabcontrol)
            {
                case 1:
                    {
                        OpenGrab.Visibility = Visibility.Collapsed;
                        CloseGrab.Visibility = Visibility.Visible;
                        break;
                    }
                default:
                    {
                        OpenGrab.Visibility = Visibility.Visible;
                        CloseGrab.Visibility = Visibility.Collapsed;
                        break;
                    }
            }

            switch (control[0].Changecontrol)
            {
                case 1:
                    {
                        OpenChange.Visibility = Visibility.Collapsed;
                        CloseChange.Visibility = Visibility.Visible;
                        break;
                    }
                default:
                    {
                        OpenChange.Visibility = Visibility.Visible;
                        CloseChange.Visibility = Visibility.Collapsed;
                        break;
                    }
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
            switch (control[0].Choicecontrol)
            {
                case 2:
                    {
                        txtchoice.Text = "选课已完成";
                        break;
                    }
                case 1:
                    {
                        txtchoice.Text = "选课已开放";
                        break;
                    }
                default:
                    {
                        txtchoice.Text = "选课已关闭";
                        break;
                    }
            }

            switch (control[0].Grabcontrol)
            {
                case 1:
                    {
                        txtgrab.Text = "抢课已开放";
                        break;
                    }
                default:
                    {
                        txtgrab.Text = "抢课已关闭";
                        break;
                    }
            }

            switch (control[0].Changecontrol)
            {
                case 1:
                    {
                        txtchange.Text = "换课已开放";
                        break;
                    }
                default:
                    {
                        txtchange.Text = "换课已关闭";
                        break;
                    }
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

            switch(item)
            {
                case "OpenChoice":
                    {
                        control[0].Choicecontrol = 1;
                        //update [表名] set [字段名]=[值] Where [条件]
                        //"update CourseControl set ChoiceControl=' "+control[0].Choicecontrol+" '";
                        int i = db.ExecuteNonQuery(updateChoice);
                        if (i > 0)
                        {
                            MessageBox.Show("开放选课成功!");
                        }
                        else
                        {
                            MessageBox.Show("开放选课失败!");
                        }
                        break;
                    }
                case "CloseChoice":
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
                        break;
                    }
                case "OpenGrab":
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
                        break;
                    }
                case "CloseGrab":
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
                        break;
                    }
                case "OpenChange":
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
                        break;
                    }
                case "CloseChange":
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
                        break;
                    }
            }
            Judge();
            Set();
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
        private void queryBTN_Click(object sender, RoutedEventArgs e)
        {
            //旧版简单的过滤查询
            //string txt = query.Text.Trim();

            //if (txt == "" || txt == null)
            //{
            //    MessageBox.Show("请输入您要查询的内容!");
            //    return;
            //}
            //else if (noBTN.IsChecked == true)
            //{
            //    if (txt.Length == 6)
            //    {
            //        IBindingListView blv = view.DefaultView;
            //        blv.Filter = "SportCourseNo = '" + txt + "'";
            //        sportcourseview.ItemsSource = blv;
            //    }
            //    else
            //    {
            //        MessageBox.Show("请输入六位数的课程编号!");
            //        Reset();
            //        return;
            //    }
            //}
            //else if (nameBTN.IsChecked == true)
            //{
            //    if (txt.Length == 2 || txt.Length == 3)
            //    {
            //        IBindingListView blv = view.DefaultView;
            //        blv.Filter = "SportCourseName = '" + txt + "'";
            //        sportcourseview.ItemsSource = blv;
            //    }
            //    else
            //    {
            //        MessageBox.Show("请输入正确的课程名称!");
            //        Reset();
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("请选择要查询的类型!");
            //    return;
            //}

            ////第一次改版但是studentno和sportcourseno只能完全匹配
            DataTable querydt = new DataTable();
            querydt = view.Clone();
            string txt = query.Text.Trim();
            string filter = "StudentName like '%" + txt + "%' or SportCourseName like '%" + txt + "%' or StudentNo=" + txt + "or SportCourseNo=" + txt;
            DataRow[] rows = view.Select(filter);
            foreach (DataRow row in rows)  // 将查询的结果添加到dt中； 
            {
                querydt.Rows.Add(row.ItemArray);
            }
            sportcourseview.ItemsSource = querydt.DefaultView;
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
            //IBindingListView blv = view.DefaultView;
            //blv.Filter = null;
            sportcourseview.ItemsSource = view.DefaultView;
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

        /// <summary>
        /// 事务判断
        /// </summary>
        /// <param name="studentNo"></param>
        /// <param name="sportCourseNo"></param>
        /// <returns></returns>
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

        #region 重置选课控制

        #region 重置一个

        /// <summary>
        /// 重置一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetControl_Click(object sender, RoutedEventArgs e)
        {
            int num = 0;

            if(choiceBTN.IsChecked == true)
            {
                if ((MessageBox.Show("是否重置全部学生的选课控制?", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes))//如果点击“确定”按钮
                {
                    num = DoResetBussiness(1);
                }
                else//如果点击“取消”按钮
                {
                    return;
                }
            }
            else if (changeBTN.IsChecked == true)
            {
                if ((MessageBox.Show("是否重置全部学生的换课控制?", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes))//如果点击“确定”按钮
                {
                    num = DoResetBussiness(2);
                }
                else//如果点击“取消”按钮
                {
                    return;
                }
            }
            else if (confirmBTN.IsChecked == true)
            {
                if ((MessageBox.Show("是否重置全部学生的换课确认控制?", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes))//如果点击“确定”按钮
                {
                    num = DoResetBussiness(3);
                }
                else//如果点击“取消”按钮
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("请选择要重置的类型!");
                return;
            }

            if (num == 1)
            {
                MessageBox.Show("重置成功!");
                return;
            }
            else
            {
                MessageBox.Show("重置失败!");
                return;
            }
        }

        /// <summary>
        /// 事务判断
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private int DoResetBussiness(int v)
        {
            int i = 0;

            using (DAL.Trans t = new DAL.Trans())
            {
                try
                {
                    BLL.DoBussiness.D8(t, v);
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

        #region 重置全部

        /// <summary>
        /// 重置全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetAllControl_Click(object sender, RoutedEventArgs e)
        {
            int num = 0;

            if ((MessageBox.Show("是否重置全部学生的全部控制?", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes))//如果点击“确定”按钮
            {
                num = DoResetAllBussiness();
            }
            else//如果点击“取消”按钮
            {
                return;
            }

            if (num == 1)
            {
                MessageBox.Show("重置成功!");
                return;
            }
            else
            {
                MessageBox.Show("重置失败!");
                return;
            }
        }

        /// <summary>
        /// 事务判断
        /// </summary>
        /// <returns></returns>
        private int DoResetAllBussiness()
        {
            int i = 0;

            using (DAL.Trans t = new DAL.Trans())
            {
                try
                {
                    BLL.DoBussiness.D8(t, 1);
                    BLL.DoBussiness.D8(t, 2);
                    BLL.DoBussiness.D8(t, 3);
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

        #endregion

        #region 一键删除失效记录

        /// <summary>
        /// 一键删除失效记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int numchange = 0;
            int numconfirm = 0;

            if (viewchange.Rows.Count != 0 && viewconfirm.Rows.Count == 0)
            {
                if ((MessageBox.Show("没有可以删除的换课确认记录,是否删除换课记录?", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes))//如果点击“确定”按钮
                {
                    numchange = numchange + DeleteCC(2);
                }
                else//如果点击“取消”按钮
                {
                    return;
                }
            }
            else if (viewchange.Rows.Count != 0 && viewconfirm.Rows.Count != 0)
            {
                //删除换课确认请求
                numconfirm = DeleteCC(1);
                //删除换课请求
                numchange = DeleteCC(2);
            }
            else
            {
                MessageBox.Show("没有可以删除的内容!");
                return;
            }

            if (numchange == viewchange.Rows.Count && numconfirm == viewconfirm.Rows.Count)
            {
                MessageBox.Show("全部删除成功!");
                DeleteConfirmCourse();
                DeleteChangeCourse();
                return;
            }
            else if (numchange == viewchange.Rows.Count)
            {
                MessageBox.Show("删除换课失效记录成功!");
                DeleteChangeCourse();
                return;
            }
            else if (numconfirm == viewconfirm.Rows.Count)
            {
                MessageBox.Show("删除换课确认失效记录成功!");
                DeleteConfirmCourse();
                return;
            }
            else
            {
                MessageBox.Show("删除失败!请检查数据库!");
                return;
            }
        }

        private int DeleteCC(int v)
        {
            int num = 0;

            if (v == 1)
            {
                for (int i = 0; i < viewconfirm.Rows.Count; i++)
                {
                    //删除换课确认失效记录
                    int StudentNo = (int)viewconfirm.Rows[i]["StudentNo_B"];
                    int SportCourseNo = (int)viewconfirm.Rows[i]["SportCourseNo_B"];
                    num = num + DoDeleteBussiness(StudentNo, SportCourseNo, 2);
                }
            }
            else if (v == 2)
            {
                for (int i = 0; i < viewchange.Rows.Count; i++)
                {
                    //删除换课失效记录
                    int StudentNo = (int)viewchange.Rows[i]["StudentNo_A"];
                    int SportCourseNo = (int)viewchange.Rows[i]["SportCourseNo_A"];
                    num = num + DoDeleteBussiness(StudentNo, SportCourseNo, 1);
                }
            }

            return num;
        }

        /// <summary>
        /// 事务判断
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private int DoDeleteBussiness(int StudentNo,int SportCourseNo ,int v)
        {
            int i = 0;

            using (DAL.Trans t = new DAL.Trans())
            {
                try
                {
                    BLL.DoBussiness.D1(t, StudentNo, SportCourseNo, 3);
                    BLL.DoBussiness.D9(t, v);
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

        #region 导出

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportBTN_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog b = new System.Windows.Forms.FolderBrowserDialog();
            b.ShowDialog();
            if (b.SelectedPath != string.Empty)
            {
                //输出当前时间
                DateTime now = new DateTime();
                now = DateTime.Now;
                string a = now.ToShortDateString();
                a = a.Replace('/', '-');

                //输出表头
                string[] header = new string[sportcourseview.Columns.Count];

                for (int i = 0; i < sportcourseview.Columns.Count; i++)
                {
                    header[i] = sportcourseview.Columns[i].Header.ToString().Trim();
                }

                //判断用户选择路径的大小
                if (b.SelectedPath.Length <= 3)
                {
                    string file = b.SelectedPath + a + "选课情况.xlsx";
                    BLL.ExportToExcel.CreateExcel(view, file, header);
                }
                else if (b.SelectedPath.Length > 3)
                {
                    string file = b.SelectedPath + "\\" + a + "选课情况.xlsx";
                    BLL.ExportToExcel.CreateExcel(view, file, header);
                }
            }
            else
            {
                MessageBox.Show("请选择要导出的路径!");
                return;
            }
        }

        #endregion
    }
}
