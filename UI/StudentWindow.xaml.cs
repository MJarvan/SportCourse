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

namespace sports_course
{
    /// <summary>
    /// StudentWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StudentWindow : Window
    {
        //数据访问
        List<BLL.CoursecControl> control = new List<BLL.CoursecControl>();
        List<DB.TblStudent> student = new List<DB.TblStudent>();
        List<DB.ViewMajorMainCourse> majorcourse = new List<DB.ViewMajorMainCourse>();
        List<DB.ViewStudentSportCourse> viewSSC = new List<DB.ViewStudentSportCourse>();
        List<DB.TblStudentSportCourse> SScourse = new List<DB.TblStudentSportCourse>();

        DataTable dt = new DataTable();//sportcourse的datatable
        DataTable viewchange = new DataTable();//ViewStudentChangeCourse的datatable
        DataTable viewconfirm = new DataTable();//ViewStudentConfirmCourse的datatable


        //逻辑操作
        int check = 0;

        //继承学生信息
        public int studentno { get; set; }
        public int studentchoicecontrol { get; set; }
        public int studentchangecontrol { get; set; }
        public int studentconfirmcontrol { get; set; }

        //存放学生已有选课(换课用)
        public int studentchangeno { get; set; }
        public int studentconfirmno { get; set; }
        public int studentsportcourseno { get; set; }
        public string studentsportcoursewaw { get; set; }

        public StudentWindow()
        {
            InitializeComponent();
            AddBorder();
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

            #region datareader添加学生课表视图信息
            DbCommand selectMM = db.GetSqlStringCommond("select * from Major_Maincourse");

            using (DbDataReader reader = db.ExecuteReader(selectMM))
            {
                while (reader.Read())
                {
                    DB.ViewMajorMainCourse model = new DB.ViewMajorMainCourse();

                    model.Majorno = reader.GetInt32(0);
                    model.Majorname = reader.GetString(1).Trim();
                    model.Maincourseno = reader.GetInt32(2);
                    model.Maincoursename = reader.GetString(3).Trim();
                    model.Weekandwhen = reader.GetString(4).Trim();
                    majorcourse.Add(model);
                }
            }
            #endregion

            AddStudentInfo();

            int studentmajor = GetStudentMajorNo();
            AddStudentCourse(studentmajor);

            AddSSC();

            AddChosen();

            AddInterface();

        }

        #region 界面操作

        /// <summary>
        /// 添加边框
        /// </summary>
        private void AddBorder()
        {
            int rows = maincourse.RowDefinitions.Count;
            int columns = maincourse.ColumnDefinitions.Count;
            for (int i = 0; i < rows; i++)
            {
                if (i != rows - 1)
                {
                    #region

                    for (int j = 0; j < columns; j++)
                    {
                        Border border = null;
                        if (j == columns - 1)
                        {
                            border = new Border()
                            {
                                BorderBrush = new SolidColorBrush(Colors.Black),
                                BorderThickness = new Thickness(2.5, 2.5, 2.5, 0)
                            };
                        }
                        else
                        {
                            border = new Border()
                            {
                                BorderBrush = new SolidColorBrush(Colors.Black),
                                BorderThickness = new Thickness(2.5, 2.5, 0, 0)
                            };
                        }
                        Grid.SetRow(border, i);
                        Grid.SetColumn(border, j);
                        maincourse.Children.Add(border);
                    }
                    #endregion
                }
                else
                {
                    for (int j = 0; j < columns; j++)
                    {
                        Border border = null;
                        if (j + 1 != columns)
                        {
                            border = new Border
                            {
                                BorderBrush = new SolidColorBrush(Colors.Black),
                                BorderThickness = new Thickness(2.5, 2.5, 0, 2.5)
                            };
                        }
                        else
                        {
                            border = new Border
                            {
                                BorderBrush = new SolidColorBrush(Colors.Black),
                                BorderThickness = new Thickness(2.5, 2.5, 2.5, 2.5)
                            };
                        }
                        Grid.SetRow(border, i);
                        Grid.SetColumn(border, j);
                        maincourse.Children.Add(border);
                    }
                }
            }
        }

        /// <summary>
        /// 获取学生界面显示
        /// </summary>
        private void AddInterface()
        {
            //获取学生姓名
            for (int i = 0; i < student.Count; i++)
            {
                if (studentno == student[i].Studentno)
                {
                    information.Text = "欢迎您!  " + student[i].Studentname;
                    break;
                }
            }

            //获取学生体育选课信息
            for (int j = 0; j < viewSSC.Count; j++)
            {
                if (studentno == viewSSC[j].Studentno && viewSSC[j].SSchoice.ToString().Trim() == "1")
                {
                    studentsportcourseno = viewSSC[j].Sportcourseno;
                    studentsportcoursewaw = viewSSC[j].Weekandwhen.ToString().Trim();
                    string waw = BLL.JudgeWAW.Judge(studentsportcoursewaw);
                    sportcourseinfo.Text = viewSSC[j].Sportcoursename.ToString().Trim() + " | " + waw;
                    changecourseinfo.Text = studentsportcourseno.ToString().Trim() + " | " + viewSSC[j].Sportcoursename.ToString().Trim() + " | " + waw;
                    break;
                }
                else if (studentno == viewSSC[j].Studentno && viewSSC[j].SSchoice.ToString().Trim() == "2")
                {
                    studentsportcourseno = viewSSC[j].Sportcourseno;
                    studentsportcoursewaw = viewSSC[j].Weekandwhen.ToString().Trim();
                    string waw = BLL.JudgeWAW.Judge(studentsportcoursewaw);
                    sportcourseinfo.Text = viewSSC[j].Sportcoursename.ToString().Trim() + " | " + waw + "  (换课中)";
                    changecourseinfo.Text = studentsportcourseno.ToString().Trim() + " | " + viewSSC[j].Sportcoursename.ToString().Trim() + " | " + waw + "  (换课中)";
                    break;
                }
                else
                {
                    studentsportcourseno = 0;
                    studentsportcoursewaw = null;
                    sportcourseinfo.Text = "无";
                    changecourseinfo.Text = "无";
                }
            }
        }

        /// <summary>
        /// 打开tab页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string item = ((MenuItem)sender).Name;

            if (item == "choicecourse")
            {
                if (control[0].Choicecontrol == 1)
                {
                    choice.Visibility = Visibility.Visible;
                    choice.IsSelected = true;
                    int studentmajor = GetStudentMajorNo();
                    AddSportCourse(studentmajor,1);
                }
                else
                {
                    MessageBox.Show("当前选课还未开放!");
                }
            }
            else if (item == "grabcourse")
            {
                if (control[0].Grabcontrol == 1)
                {
                    grab.Visibility = Visibility.Visible;
                    grab.IsSelected = true;
                    int studentmajor = GetStudentMajorNo();
                    AddSportCourse(studentmajor, 2);
                    AddChosen();
                }
                else
                {
                    MessageBox.Show("当前抢课还未开放!");
                }
            }
            else if (item == "changecourse")
            {
                if (control[0].Changecontrol == 1)
                {
                    change.Visibility = Visibility.Visible;
                    change.IsSelected = true;
                    AddStudentInfo();
                    AddSSC();
                    int studentmajor = GetStudentMajorNo();
                    AddChange(studentmajor);
                    AddConfirm();
                }
                else
                {
                    MessageBox.Show("当前换课还未开放!");
                }
            }

        }




        /// <summary>
        /// 关闭当前tab页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_Click(object sender, RoutedEventArgs e)
        {
            string item = ((Button)sender).Name;

            if (item == "changeclose")
            {
                change.Visibility = Visibility.Collapsed;
                viewcourse.IsSelected = true;
            }
            else if (item == "grabclose")
            {
                grab.Visibility = Visibility.Collapsed;
                viewcourse.IsSelected = true;
            }
            else if (item == "choiceclose")
            {
                choice.Visibility = Visibility.Collapsed;
                viewcourse.IsSelected = true;
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
                //要删除这两句话(测试用)
                {
                    BLL.StudentSportChoiceNo.SportcoursenoA = 0;
                    BLL.StudentSportChoiceNo.SportcoursenoB = 0;
                }
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

        #region 学生课表信息操作

        /// <summary>
        /// 加载学生基本信息
        /// </summary>
        /// <param name="studentno"></param>
        private void AddStudentInfo()
        {
            #region datareader添加学生信息

            student.Clear();

            DAL.DbHelper db = new DAL.DbHelper();

            DbCommand selectstudent = db.GetSqlStringCommond("select * from Student");

            using (DbDataReader reader = db.ExecuteReader(selectstudent))
            {
                while (reader.Read())
                {
                    DB.TblStudent model = new DB.TblStudent();

                    model.Studentno = reader.GetInt32(0);
                    model.Studentname = reader.GetString(1).Trim();
                    model.Changecontrol = reader.GetInt32(3);
                    model.Confirmcontrol = reader.GetInt32(4);
                    model.Choicecontrol = reader.GetInt32(5);
                    model.Majorno = reader.GetInt32(6);
                    student.Add(model);
                }
            }
            #endregion
        }


        /// <summary>
        /// 加载换课信息
        /// </summary>
        private void AddChange(int studentmajor)
        {
            viewchange.Clear();

            DAL.DbHelper db = new DAL.DbHelper();

            #region datareader加载换课视图信息

            DbCommand selectSCC = db.GetSqlStringCommond("select * from Student_ChangeCourse");
            viewchange = db.ExecuteDataTable(selectSCC);
            changeconfirm.ItemsSource = viewchange.DefaultView;
            #endregion
            int RowsCount = viewchange.Rows.Count;

            //删掉体育选课与课表重复时间的行
            for (int a = 0; a < RowsCount; a++)
            {
                for (int b = 0; b < majorcourse.Count; b++)
                {
                    if (studentmajor == majorcourse[b].Majorno)
                    {
                        if (viewchange.Rows[a]["WeekAndWhen"].ToString().Trim() == majorcourse[b].Weekandwhen.Trim())
                        {
                            viewchange.Rows[a].Delete();
                            break;
                        }
                    }
                }
            }

            viewchange.AcceptChanges();

            #region 改正字符类型

            DataRow[] waw = viewchange.Select("WeekAndWhen>9");
            foreach (DataRow row in waw)
            {
                row["WeekAndWhen"] = BLL.JudgeWAW.Judge(row["WeekAndWhen"].ToString());
            }

            #endregion
        }

        /// <summary>
        /// 加载换课确认信息
        /// </summary>
        private void AddConfirm()
        {
            viewconfirm.Clear();

            DAL.DbHelper db = new DAL.DbHelper();

            #region datareader加载换课视图信息

            DbCommand selectSCC = db.GetSqlStringCommond("select * from Student_ConfirmCourse where StudentNo_A=" +studentno);
            viewconfirm = db.ExecuteDataTable(selectSCC);
            receiveconfirm.ItemsSource = viewconfirm.DefaultView;
            #endregion

            #region 改正字符类型

            DataRow[] waw = viewconfirm.Select("WeekAndWhen>9");
            foreach (DataRow row in waw)
            {
                row["WeekAndWhen"] = BLL.JudgeWAW.Judge(row["WeekAndWhen"].ToString());
            }

            #endregion
        }

        /// <summary>
        /// 加载SSC信息
        /// </summary>
        private void AddSSC()
        {
            SScourse.Clear();

            DAL.DbHelper db = new DAL.DbHelper();

            #region datareader加载SSC信息

            DbCommand selectSSC = db.GetSqlStringCommond("select StudentNo,SportCourseNo,SSChoice from StudentSportCourse");

            using (DbDataReader reader = db.ExecuteReader(selectSSC))
            {
                while (reader.Read())
                {
                    DB.TblStudentSportCourse model = new DB.TblStudentSportCourse();

                    model.Studentno = reader.GetInt32(0);
                    model.Sportcourseno = reader.GetInt32(1);
                    model.SSchoice = reader.GetString(2).Trim();
                    SScourse.Add(model);
                }
            }
            #endregion
        }

        /// <summary>
        /// 体育选课操作
        /// </summary>
        /// <param name="studentmajor"></param>
        /// <param name="i">1是选课,2是抢课</param>
        private void AddSportCourse(int studentmajor, int i)
        {
            //i=1,代表是因为选课添加数据
            if(i == 1)
            {
                #region dataset添加选课数据
                DAL.DbHelper db = new DAL.DbHelper();
                DbCommand selectsport = db.GetSqlStringCommond("select * from SportCourse");
                dt = db.ExecuteDataTable(selectsport);
                int RowsCount = dt.Rows.Count;

                //向table里增加多一列控制属性 
                dt.Columns.Add("Check");
                for (int j = 0; j < RowsCount; j++)//为该列增加相应的数值  
                {
                    dt.Rows[j]["Check"] = 0;
                }

                //删掉体育选课与课表重复时间的行
                for (int a = 0; a < RowsCount; a++)
                {
                    for (int b = 0; b < majorcourse.Count; b++)
                    {

                        if (studentmajor == majorcourse[b].Majorno)
                        {
                            if (dt.Rows[a]["WeekAndWhen"].ToString().Trim() == majorcourse[b].Weekandwhen.Trim())
                            {
                                dt.Rows[a].Delete();
                                break;
                            }
                        }
                    }
                }
                dt.AcceptChanges();

                #region 改正字符类型
                DataRow[] arrRows = dt.Select("IsSelected=0");
                foreach (DataRow row in arrRows)
                {
                    row["IsSelected"] = false;
                }

                DataRow[] waw = dt.Select("WeekAndWhen>9");
                foreach (DataRow row in waw)
                {
                    row["WeekAndWhen"] = BLL.JudgeWAW.Judge(row["WeekAndWhen"].ToString());
                }

                #endregion

                choicesportcourse.ItemsSource = dt.DefaultView;
                #endregion
            }
            //i=2,代表是因为抢课添加数据
            else if (i ==2)
            {
                #region dataset添加抢课数据
                DAL.DbHelper db = new DAL.DbHelper();
                DbCommand selectsport = db.GetSqlStringCommond("select * from SportCourse");
                dt = db.ExecuteDataTable(selectsport);
                int RowsCount = dt.Rows.Count;

                //删掉体育选课与课表重复时间的行
                for (int a = 0; a < RowsCount; a++)
                {
                    for (int b = 0; b < majorcourse.Count; b++)
                    {

                        if (studentmajor == majorcourse[b].Majorno)
                        {
                            if (dt.Rows[a]["WeekAndWhen"].ToString().Trim() == majorcourse[b].Weekandwhen.Trim())
                            {
                                dt.Rows[a].Delete();
                                break;
                            }
                        }
                    }
                }
                dt.AcceptChanges();

                #region 改正字符类型
                DataRow[] arrRows = dt.Select("IsSelected=0");
                foreach (DataRow row in arrRows)
                {
                    row["IsSelected"] = false;
                }

                DataRow[] waw = dt.Select("WeekAndWhen>9");
                foreach (DataRow row in waw)
                {
                    if (row["WeekAndWhen"].ToString() == "11")
                    {
                        row["WeekAndWhen"] = "周一第一节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "12")
                    {
                        row["WeekAndWhen"] = "周一第二节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "13")
                    {
                        row["WeekAndWhen"] = "周一第三节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "14")
                    {
                        row["WeekAndWhen"] = "周一第四节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "15")
                    {
                        row["WeekAndWhen"] = "周一第五节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "21")
                    {
                        row["WeekAndWhen"] = "周二第一节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "22")
                    {
                        row["WeekAndWhen"] = "周二第二节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "23")
                    {
                        row["WeekAndWhen"] = "周二第三节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "24")
                    {
                        row["WeekAndWhen"] = "周二第四节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "25")
                    {
                        row["WeekAndWhen"] = "周二第五节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "31")
                    {
                        row["WeekAndWhen"] = "周三第一节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "32")
                    {
                        row["WeekAndWhen"] = "周三第二节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "33")
                    {
                        row["WeekAndWhen"] = "周三第三节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "34")
                    {
                        row["WeekAndWhen"] = "周三第四节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "35")
                    {
                        row["WeekAndWhen"] = "周三第五节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "41")
                    {
                        row["WeekAndWhen"] = "周四第一节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "42")
                    {
                        row["WeekAndWhen"] = "周四第二节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "43")
                    {
                        row["WeekAndWhen"] = "周四第三节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "44")
                    {
                        row["WeekAndWhen"] = "周四第四节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "45")
                    {
                        row["WeekAndWhen"] = "周四第五节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "51")
                    {
                        row["WeekAndWhen"] = "周五第一节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "52")
                    {
                        row["WeekAndWhen"] = "周五第二节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "53")
                    {
                        row["WeekAndWhen"] = "周五第三节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "54")
                    {
                        row["WeekAndWhen"] = "周五第四节";
                    }
                    else if (row["WeekAndWhen"].ToString() == "55")
                    {
                        row["WeekAndWhen"] = "周五第五节";
                    }
                }

                #endregion

                grabsportcourse.ItemsSource = dt.DefaultView;
                #endregion
            }
        }

        /// <summary>
        /// 获取学生是否已经选中过课
        /// </summary>
        private void AddChosen()
        {
            viewSSC.Clear();

            DAL.DbHelper db = new DbHelper();
            #region datareader添加学生体育选课视图信息
            DbCommand selectView = db.GetSqlStringCommond("select * from Student_Sportcourse");

            using (DbDataReader reader = db.ExecuteReader(selectView))
            {
                while (reader.Read())
                {
                    DB.ViewStudentSportCourse model = new DB.ViewStudentSportCourse();

                    model.Studentno = reader.GetInt32(0);
                    model.Sportcourseno = reader.GetInt32(2);
                    model.Sportcoursename = reader.GetString(3).Trim();
                    model.SSchoice = reader.GetString(5).Trim();
                    model.Weekandwhen = reader.GetString(6).Trim();
                    viewSSC.Add(model);
                }
            }
            #endregion
        }

        /// <summary>
        /// 获取学生专业编号
        /// </summary>
        private int GetStudentMajorNo()
        {
            int studentmajor = 0;

            for (int i = 0; i < student.Count; i++)
            {
                if (studentno == student[i].Studentno)
                {
                    studentmajor = student[i].Majorno;
                    break;
                }
                else
                {
                    continue;
                }
            }
            return studentmajor;
        }

        /// <summary>
        /// 添加学生课表信息
        /// </summary>
        private void AddStudentCourse(int studentmajor)
        {
            #region 获取周几第几节的赋值

            string[] waw = new string[majorcourse.Count];
            string[] coursename = new string[majorcourse.Count];

            for (int i = 0; i < majorcourse.Count; i++)
            {
                if (studentmajor == majorcourse[i].Majorno)
                {
                    waw[i] = majorcourse[i].Weekandwhen.Trim();
                    coursename[i] = majorcourse[i].Maincoursename.Trim();
                }
            }

            for(int j = 0; j < waw.Length; j++)
            {
                if(waw[j] == "11")
                {
                    D1C1.Text = coursename[j].Trim();
                }
                else if(waw[j] == "12")
                {
                    D1C2.Text = coursename[j].Trim();
                }
                else if (waw[j] == "13")
                {
                    D1C3.Text = coursename[j].Trim();
                }
                else if (waw[j] == "14")
                {
                    D1C4.Text = coursename[j].Trim();
                }
                else if (waw[j] == "21")
                {
                    D2C1.Text = coursename[j].Trim();
                }
                else if (waw[j] == "22")
                {
                    D2C2.Text = coursename[j].Trim();
                }
                else if (waw[j] == "23")
                {
                    D2C3.Text = coursename[j].Trim();
                }
                else if (waw[j] == "24")
                {
                    D2C4.Text = coursename[j].Trim();
                }
                else if (waw[j] == "31")
                {
                    D3C1.Text = coursename[j].Trim();
                }
                else if (waw[j] == "32")
                {
                    D3C2.Text = coursename[j].Trim();
                }
                else if (waw[j] == "33")
                {
                    D3C3.Text = coursename[j].Trim();
                }
                else if (waw[j] == "34")
                {
                    D3C4.Text = coursename[j].Trim();
                }
                else if (waw[j] == "41")
                {
                    D4C1.Text = coursename[j].Trim();
                }
                else if (waw[j] == "42")
                {
                    D4C2.Text = coursename[j].Trim();
                }
                else if (waw[j] == "43")
                {
                    D4C3.Text = coursename[j].Trim();
                }
                else if (waw[j] == "44")
                {
                    D4C4.Text = coursename[j].Trim();
                }
                else if (waw[j] == "51")
                {
                    D5C1.Text = coursename[j].Trim();
                }
                else if (waw[j] == "52")
                {
                    D5C2.Text = coursename[j].Trim();
                }
                else if (waw[j] == "53")
                {
                    D5C3.Text = coursename[j].Trim();
                }
                else if (waw[j] == "54")
                {
                    D5C4.Text = coursename[j].Trim();
                }
            }
            #endregion
        }
        #endregion

        #region 数据库逻辑操作

        /// <summary>
        /// 更新学生选课控制
        /// </summary>
        /// <param name="t"></param>
        private void D1(Trans t, int i)
        {
            DAL.DbHelper db = new DAL.DbHelper();
            //更新选课
            if (i == 1)
            {
                studentchoicecontrol = studentchoicecontrol + 1;
                DbCommand updateCC = db.GetSqlStringCommond("update Student set ChoiceControl=" + studentchoicecontrol + "where student.StudentNo =" + studentno);
                if (t == null)
                {
                    db.ExecuteNonQuery(updateCC);
                }
                else
                {
                    db.ExecuteNonQuery(updateCC, t);
                }
            }
            //更新换课
            else if(i == 2)
            {
                studentchangecontrol = studentchangecontrol + 1;
                DbCommand updateCC = db.GetSqlStringCommond("update Student set ChangeControl=" + studentchangecontrol + "where student.StudentNo =" + studentno);
                if (t == null)
                {
                    db.ExecuteNonQuery(updateCC);
                }
                else
                {
                    db.ExecuteNonQuery(updateCC, t);
                }
            }
            //更新换课确认
            else if (i == 3)
            {
                studentconfirmcontrol = studentconfirmcontrol + 1;
                DbCommand updateCC = db.GetSqlStringCommond("update Student set ConfirmControl=" + studentconfirmcontrol + "where student.StudentNo =" + studentno);
                if (t == null)
                {
                    db.ExecuteNonQuery(updateCC);
                }
                else
                {
                    db.ExecuteNonQuery(updateCC, t);
                }
            }
        }

        /// <summary>
        /// 向学生选课表插入信息
        /// </summary>
        /// <param name="t"></param>
        /// <param name="SportCourseNo"></param>
        /// <param name="SSChoice"></param>
        private void D2(Trans t, int SportCourseNo,int i)
        {
            DAL.DbHelper db = new DAL.DbHelper();
            //插入SSC
            if (i == 1)
            {
                DbCommand insert = db.GetSqlStringCommond("insert into StudentSportCourse values (@StudentNo, @SportCourseNo, @SSChoice)");

                db.AddInParameter(insert, "@StudentNo", DbType.Int32, studentno);
                db.AddInParameter(insert, "@SportCourseNo", DbType.Int32, SportCourseNo);
                db.AddInParameter(insert, "@SSChoice", DbType.String, "1");
                if (t == null)
                {
                    db.ExecuteNonQuery(insert);
                }
                else
                {
                    db.ExecuteNonQuery(insert, t);
                }

            }
            //更新SSC
            else if (i == 2)
            {
                DbCommand updateSSC = db.GetSqlStringCommond("update StudentSportCourse set SSChoice= @SSChoice where StudentNo= @StudentNo and SportCourseNo= @SportCourseNo");
                db.AddInParameter(updateSSC, "@StudentNo", DbType.Int32, studentno);
                db.AddInParameter(updateSSC, "@SportCourseNo", DbType.Int32, SportCourseNo);
                db.AddInParameter(updateSSC, "@SSChoice", DbType.String, "2");
                if (t == null)
                {
                    db.ExecuteNonQuery(updateSSC);
                }
                else
                {
                    db.ExecuteNonQuery(updateSSC, t);
                }
            }
        }

        /// <summary>
        /// 更新学生信息
        /// </summary>
        /// <param name="t"></param>
        /// <param name="sportCourseNo"></param>
        private void D3(Trans t, int SportCourseNo, int i)
        {
            DAL.DbHelper db = new DAL.DbHelper();

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                if (SportCourseNo == (int)dt.Rows[j]["SportCourseNo"])
                {
                    //更新选课
                    if (i == 1)
                    {
                        int choicenumbefore = (int)dt.Rows[j]["ChoiceNumBefore"];
                        choicenumbefore = choicenumbefore + 1;
                        DbCommand updateCNB = db.GetSqlStringCommond("update SportCourse set ChoiceNumBefore=" + choicenumbefore + "where SportCourse.SportCourseNo =" + SportCourseNo);

                        if (t == null)
                        {
                            db.ExecuteNonQuery(updateCNB);
                        }
                        else
                        {
                            db.ExecuteNonQuery(updateCNB, t);
                        }
                    }
                    //更新抢课
                    else if (i == 2)
                    {
                        int choicenumafter = (int)dt.Rows[j]["ChoiceNumAfter"];
                        choicenumafter = choicenumafter + 1;
                        DbCommand updateCNA = db.GetSqlStringCommond("update SportCourse set ChoiceNumAfter=" + choicenumafter + "where SportCourse.SportCourseNo =" + SportCourseNo);

                        if (t == null)
                        {
                            db.ExecuteNonQuery(updateCNA);
                        }
                        else
                        {
                            db.ExecuteNonQuery(updateCNA, t);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 插入学生信息
        /// </summary>
        /// <param name="t"></param>
        private void D4(Trans t, int i)
        {
            DAL.DbHelper db = new DAL.DbHelper();
            // 插入学生信息到换课表
            if (i == 1)
            {
                DateTime ChangeCreateTime = new DateTime();
                ChangeCreateTime = DateTime.Now;
                DbCommand insert = db.GetSqlStringCommond("insert into ChangeCourse values (@StudentNoA, @SportCourseNoA, @WeekAndWhen, @ChangeChoice, @ChangeCreateTime)");

                db.AddInParameter(insert, "@StudentNoA", DbType.Int32, studentno);
                db.AddInParameter(insert, "@SportCourseNoA", DbType.Int32, studentsportcourseno);
                db.AddInParameter(insert, "@WeekAndWhen", DbType.String, studentsportcoursewaw);
                db.AddInParameter(insert, "@ChangeChoice", DbType.String, "0");
                db.AddInParameter(insert, "@ChangeCreateTime", DbType.DateTime, ChangeCreateTime);

                if (t == null)
                {
                    db.ExecuteNonQuery(insert);
                }
                else
                {
                    db.ExecuteNonQuery(insert, t);
                }
            }
            // 插入学生信息到换课确认表
            else if(i == 2)
            {
                DateTime ConfirmCreateTime = new DateTime();
                ConfirmCreateTime = DateTime.Now;
                DbCommand insert = db.GetSqlStringCommond("insert into ConfirmCourse values (@ChangeNo, @StudentNoB, @SportCourseNoB, @WeekAndWhen, @ConfirmChoice, @ConfirmCreateTime)");

                db.AddInParameter(insert, "@ChangeNo", DbType.Int32, studentchangeno);
                db.AddInParameter(insert, "@StudentNoB", DbType.Int32, studentno);
                db.AddInParameter(insert, "@SportCourseNoB", DbType.Int32, studentsportcourseno);
                db.AddInParameter(insert, "@WeekAndWhen", DbType.String, studentsportcoursewaw);
                db.AddInParameter(insert, "@ConfirmChoice", DbType.String, "0");
                db.AddInParameter(insert, "@ConfirmCreateTime", DbType.DateTime, ConfirmCreateTime);

                if (t == null)
                {
                    db.ExecuteNonQuery(insert);
                }
                else
                {
                    db.ExecuteNonQuery(insert, t);
                }
            }
        }

        /// <summary>
        /// 更新学生换课表的学生信息
        /// </summary>
        /// <param name="t"></param>
        /// <param name="i"></param>
        private void D5(Trans t, int i)
        {
            DAL.DbHelper db = new DAL.DbHelper();
            DbCommand updateCC = db.GetSqlStringCommond("update ChangeCourse set ChangeChoice= @ChangeChoice where ChangeNo= @ChangeNo");
            db.AddInParameter(updateCC, "@ChangeNo", DbType.Int32, studentchangeno);
            //更新换课表之已收到换课确认请求
            if (i == 1)
            {
                db.AddInParameter(updateCC, "@ChangeChoice", DbType.String, "1");
            }
            //更新换课表之已接受换课确认请求
            else if (i == 2)
            {
                db.AddInParameter(updateCC, "@ChangeChoice", DbType.String, "2");
            }
            //更新换课表之已拒绝换课确认请求
            else if (i == 3)
            {
                db.AddInParameter(updateCC, "@ChangeChoice", DbType.String, "3");
            }

            if (t == null)
            {
                db.ExecuteNonQuery(updateCC);
            }
            else
            {
                db.ExecuteNonQuery(updateCC, t);
            }
        }

        /// <summary>
        /// 更新换课确认表操作
        /// </summary>
        /// <param name="t"></param>
        /// <param name="i"></param>
        private void D6(Trans t, int i)
        {
            DAL.DbHelper db = new DAL.DbHelper();
            DbCommand updateCC = db.GetSqlStringCommond("update ConfirmCourse set ConfirmChoice= @ConfirmChoice where ConfirmNo= @ConfirmNo");
            db.AddInParameter(updateCC, "@ConfirmNo", DbType.Int32, studentconfirmno);
            //更新换课确认表之换课确认请求被已接受
            if (i == 1)
            {
                db.AddInParameter(updateCC, "@ConfirmChoice", DbType.String, "1");
            }
            //更新换课确认表之换课确认请求被已拒绝
            else if (i == 2)
            {
                db.AddInParameter(updateCC, "@ConfirmChoice", DbType.String, "2");
            }

            if (t == null)
            {
                db.ExecuteNonQuery(updateCC);
            }
            else
            {
                db.ExecuteNonQuery(updateCC, t);
            }
        }

        /// <summary>
        /// 将学生A和学生B的课调换
        /// </summary>
        /// <param name="t"></param>
        private void D7(Trans t, int i)
        {
            //将同学A的课程信息换成同学B的
            if (i == 1)
            {

            }
            //将同学B的课程信息换成同学A的
            else if (i == 2)
            {

            }
        }

        #endregion

        #region 选课功能逻辑操作

        #region 界面按钮
        /// <summary>
        /// 确定选课按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void choiceconfirm_Click(object sender, RoutedEventArgs e)
        {
            studentchoicecontrol = Checkstudentchoice(studentno,1);

            if (studentchoicecontrol > 2)
            {
                MessageBox.Show("您的选课已经满了");
                ResetChoice();
                viewcourse.IsSelected = true;
            }
            else
            {
                StartChoice();
            }
        }

        /// <summary>
        /// 学生选课控制
        /// </summary>
        /// <param name="studentno"></param>
        /// <returns></returns>
        private int Checkstudentchoice(int studentno,int j)
        {
            int studentchoice = 3;

            //返回选课控制
            if (j == 1)
            {
                for (int i = 0; i < student.Count; i++)
                {
                    if (studentno == student[i].Studentno)
                    {
                        studentchoice = student[i].Choicecontrol;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            //返回换课控制
            if (j == 2)
            {
                for (int i = 0; i < student.Count; i++)
                {
                    if (studentno == student[i].Studentno)
                    {
                        studentchoice = student[i].Changecontrol;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            //返回
            if (j == 3)
            {
                for (int i = 0; i < student.Count; i++)
                {
                    if (studentno == student[i].Studentno)
                    {
                        studentchoice = student[i].Confirmcontrol;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return studentchoice;
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void choicereset_Click(object sender, RoutedEventArgs e)
        {
            ResetChoice();
        }

        private void ResetChoice()
        {
            check = 0;
            DataRow[] select = dt.Select("IsSelected=True");
            foreach (DataRow row in select)
            {
                row["IsSelected"] = false;
            }
            DataRow[] checknum = dt.Select("Check=1");
            foreach (DataRow row in checknum)
            {
                row["Check"] = 0;
            }
        }

        /// <summary>
        /// 最多选择三门课控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkbox_Click(object sender, RoutedEventArgs e)
        {
            //筛选出所有行
            DataRow[] True = dt.Select("IsSelected=True");
            foreach (DataRow row in True)
            {
                if (row["IsSelected"].ToString() == "True" && row["Check"].ToString() == "0")
                {
                    check++;
                    //获取当前行的控制编号
                    DataRowView mySelectedElement = (DataRowView)choicesportcourse.SelectedItem;
                    mySelectedElement.Row["Check"] = 1;
                }
                else if (row["IsSelected"].ToString() == "True" && row["Check"].ToString() == "1")
                {
                    continue;
                }
            }

            //筛选出所有行
            DataRow[] False = dt.Select("IsSelected=False");
            foreach (DataRow row in False)
            {
                if (row["IsSelected"].ToString() == "False" && row["Check"].ToString() == "1")
                {
                    check--;
                    //获取当前行的控制编号
                    DataRowView mySelectedElement = (DataRowView)choicesportcourse.SelectedItem;
                    mySelectedElement.Row["Check"] = 0;
                }
                else if (row["IsSelected"].ToString() == "False" && row["Check"].ToString() == "0")
                {
                    continue;
                }
            }

            //选课总数控制
            if (check > 3)
            {
                MessageBox.Show("您最多选择三门课!");
                DataRowView mySelectedElement = (DataRowView)choicesportcourse.SelectedItem;
                mySelectedElement.Row["IsSelected"] = "False";
                mySelectedElement.Row["Check"] = 0;
                check = 3;
                return;
            }
        }
        #endregion

        #region 界面逻辑

        /// <summary>
        /// 添加学生选课信息到学生选课表
        /// </summary>
        private void StartChoice()
        {
            SScourse.Clear();
            int RowsCount = dt.Rows.Count;
            int a = 0;

            //将选中的体育课信息存入List
            DataRow[] dr = dt.Select("IsSelected=True and Check=1");
            foreach (DataRow row in dr)
            {

                if (BLL.StudentSportChoiceNo.SportcoursenoA == (int)row["SportCourseNo"] || BLL.StudentSportChoiceNo.SportcoursenoB == (int)row["SportCourseNo"])
                {
                    MessageBox.Show("您已经选过该课程!");
                    ResetChoice();
                    return;
                }
                else
                {
                    //满足条件,开始存入
                    DB.TblStudentSportCourse model = new DB.TblStudentSportCourse();

                    model.Studentno = studentno;
                    model.Sportcourseno = (int)row["SportCourseNo"];
                    model.SSchoice = "0";
                    SScourse.Add(model);
                    AddToSSC();
                    SScourse.Clear();
                    a++;
                }

            }

            if(a > 0)
            {
                MessageBox.Show("已经成功选择"+ a + "门课!");
                SScourse.Clear();
                ResetChoice();
                int studentmajor = GetStudentMajorNo();
                AddSportCourse(studentmajor,1);
                viewcourse.IsSelected = true;
            }
            else
            {
                MessageBox.Show("选课失败");
            }
        }
        #endregion

        #region 数据库操作
        /// <summary>
        /// 插入并更新数据到学生选课表
        /// </summary>
        private void AddToSSC()
        {
            //INSERT INTO 表名称 VALUES (值1, 值2,....)
            DAL.DbHelper db = new DAL.DbHelper();
            for (int i = 0; i < SScourse.Count; i++)
            {
                int SportCourseNo = SScourse[i].Sportcourseno;
                //事务
                DoChoiceBussiness(SportCourseNo);

                if(studentchoicecontrol <= 2 )
                {
                    if (BLL.StudentSportChoiceNo.SportcoursenoA == 0)
                    {
                        BLL.StudentSportChoiceNo.SportcoursenoA = SportCourseNo;
                    }
                    else if (BLL.StudentSportChoiceNo.SportcoursenoA != 0)
                    {
                        BLL.StudentSportChoiceNo.SportcoursenoB = SportCourseNo;
                    }
                }
            }
        }

        /// <summary>
        /// 事务判断
        /// </summary>
        private void DoChoiceBussiness(int SportCourseNo)
        {
            using (DAL.Trans t = new DAL.Trans())
            {
                try
                {
                    D1(t,1);
                    D2(t, SportCourseNo,1);
                    D3(t, SportCourseNo,1);
                    t.Commit();
                }
                catch
                {
                    t.RollBack();
                }
            }
        }
      
        #endregion

        #endregion

        #region 抢课功能逻辑操作

        #region 界面按钮

        /// <summary>
        /// 抢课确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grabconfirm_Click(object sender, RoutedEventArgs e)
        {
            bool IsChosen = CheckStudentIsChosen();

            if ( IsChosen == true)
            {
                MessageBox.Show("您已经成功过选课了!");
                grab.Visibility = Visibility.Collapsed;
                viewcourse.IsSelected = true;
            }
            else
            {
                StartGrab();
            }
        }

        /// <summary>
        /// 检测学生是否已经选中课
        /// </summary>
        /// <returns></returns>
        private bool CheckStudentIsChosen()
        {
            bool IsChosen = false;

            for (int i = 0; i < viewSSC.Count; i++)
            {
                if (studentno == viewSSC[i].Studentno && viewSSC[i].SSchoice.ToString().Trim() == "1")
                {
                    IsChosen = true;
                    break;
                }
                else
                {
                    continue;
                }
            }

            return IsChosen;
        }


        #endregion

        #region 界面逻辑

        /// <summary>
        /// 添加学生抢课信息
        /// </summary>
        private void StartGrab()
        {
            int num = 0;
            DataRowView dr = (DataRowView)grabsportcourse.SelectedItem;
            int choicenumafter = (int)dr["ChoiceNumAfter"];
            int choicenummax = (int)dr["ChoiceNumMax"];

            if (dr == null)
            {
                MessageBox.Show("请选中要抢课的行!");
            }
            //学生抢课控制
            else if (choicenumafter >= choicenummax)
            {
                MessageBox.Show("该课程已满人!请您选择别的课程");
                return;
            }
            else
            {
                int SportCourseNo = (int)dr.Row["SportCourseNo"];
                num = DoGrabBussiness(SportCourseNo);
            }

            if (num == 1)
            {
                MessageBox.Show("抢课成功!");
                int studentmajor = GetStudentMajorNo();
                AddSportCourse(studentmajor, 2);
                viewcourse.IsSelected = true;
                grab.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("抢课失败!");
                return;
            }
        }
        #endregion

        #region 数据库操作

        /// <summary>
        /// 事务判断
        /// </summary>
        /// <param name="sportCourseNo"></param>
        /// <returns></returns>
        private int DoGrabBussiness(int sportCourseNo)
        {
            int i = 0;
            using (DAL.Trans t = new DAL.Trans())
            {
                try
                {
                    D2(t,sportCourseNo,1);
                    D3(t,sportCourseNo,2);
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

        #region 同学A换课功能逻辑操作

        #region 界面按钮

        /// <summary>
        /// 发送换课请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendchangecourse_Click(object sender, RoutedEventArgs e)
        {
            studentchangecontrol = Checkstudentchoice(studentno, 2);

            if (studentsportcourseno == 0)
            {
                MessageBox.Show("您没有课程可以换课");
                viewcourse.IsSelected = true;
                return;
            }
            else if (studentchangecontrol > 2)
            {
                MessageBox.Show("您今天的换课次数已经达到上限!");
                viewcourse.IsSelected = true;
                return;
            }
            else
            {
                StartChange();
            }
        }

        #endregion

        #region 界面逻辑
        /// <summary>
        /// 添加学生换课信息
        /// </summary>
        private void StartChange()
        {
            int num = 0;
            for(int i = 0; i < SScourse.Count; i++)
            {
                if (studentno == SScourse[i].Studentno && SScourse[i].SSchoice.ToString().Trim() == "2")
                {
                    MessageBox.Show("您已经在换课中了!");
                    change.Visibility = Visibility.Collapsed;
                    return;
                }
                else if (studentno == SScourse[i].Studentno && SScourse[i].SSchoice.ToString().Trim() == "1")
                {
                    num = DoChangeBussiness();
                    break;
                }
            }

            if (num == 1)
            {
                MessageBox.Show("换课请求发送成功!");
                viewcourse.IsSelected = true;
                change.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("换课请求发送失败!");
                return;
            }
        }
        #endregion

        #region 数据库操作

        /// <summary>
        /// 事务判断
        /// </summary>
        private int DoChangeBussiness()
        {
            int i = 0;
            using (DAL.Trans t = new DAL.Trans())
            {
                try
                {
                    D1(t,2);
                    D2(t, studentsportcourseno, 2);
                    D4(t,1);
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

        #region 同学B换课确认功能逻辑操作

        #region 界面按钮

        /// <summary>
        /// 刷新换课请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refreshsended_Click(object sender, RoutedEventArgs e)
        {
            int studentmajor = GetStudentMajorNo();
            AddChange(studentmajor);
        }

        /// <summary>
        /// 发送换课确认请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendconfirm_Click(object sender, RoutedEventArgs e)
        {
            studentconfirmcontrol = Checkstudentchoice(studentno, 3);

            if (studentsportcourseno == 0)
            {
                MessageBox.Show("您没有课程可以换课");
                viewcourse.IsSelected = true;
                return;
            }
            else if (studentconfirmcontrol > 2)
            {
                MessageBox.Show("您今天的换课确认次数已经达到上限!");
                viewcourse.IsSelected = true;
                return;
            }
            else
            {
                StarConfirm();
            }
        }

        #endregion

        #region 界面逻辑

        /// <summary>
        /// 添加学生换课确认信息
        /// </summary>
        private void StarConfirm()
        {
            int num = 0;
            DataRowView dr = (DataRowView)changeconfirm.SelectedItem;
            if (dr == null)
            {
                MessageBox.Show("请选中要发送换课确认的行!");
                return;
            }

            int StudentNo = (int)dr.Row["StudentNo_A"];
            int SportCourseNo = (int)dr.Row["SportCourseNo_A"];
            int MajorNo = (int)dr.Row["MajorNo"];
            studentchangeno = (int)dr.Row["ChangeNo"];

            if( studentno == StudentNo)
            {
                MessageBox.Show("您不能对自己进行换课!");
                return;
            }
            else if (studentsportcourseno == SportCourseNo)
            {
                MessageBox.Show("您已经有这节课了!");
                return;
            }
            else
            {
                int i = JudgeStudentWAW(MajorNo);

                if (i == 1)
                {
                    num = DoConfirmBussiness();
                }
                else if (i == 0)
                {
                    MessageBox.Show("您的体育选课与对方的课表冲突!");
                    return;
                }
            }

            if (num == 1)
            {
                MessageBox.Show("发送换课确认成功!");
                return;
            }
            else
            {
                MessageBox.Show("发送换课确认失败!");
                return;
            }
        }

        /// <summary>
        /// 判断换课学生B的体育换课是否与换课学生A的课表冲突
        /// </summary>
        /// <param name="MajorNo"></param>
        /// <returns></returns>
        private int JudgeStudentWAW(int MajorNo)
        {
            int i = 1;

            for (int b = 0; b < majorcourse.Count; b++)
            {
                if (MajorNo == majorcourse[b].Majorno)
                {
                    if (studentsportcoursewaw == majorcourse[b].Weekandwhen.Trim())
                    {
                        i = 0;
                        break;
                    }
                }
            }

            return i;
        }
        #endregion

        #region 数据库操作

        /// <summary>
        /// 事务判断
        /// </summary>
        /// <returns></returns>
        private int DoConfirmBussiness()
        {
            int i = 0;
            using (DAL.Trans t = new DAL.Trans())
            {
                try
                {
                    D1(t, 3);
                    D2(t, studentsportcourseno, 2);
                    D4(t, 2);
                    D5(t, 1);
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

        #region 同学A确认换课功能逻辑操作

        #region 界面按钮

        /// <summary>
        /// 刷新换课确认请求
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refreshconfirmed_Click(object sender, RoutedEventArgs e)
        {
            AddConfirm();
        }

        /// <summary>
        /// 确认换课
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void confirmchangecourse_Click(object sender, RoutedEventArgs e)
        {
            StartConfirm();
        }

        /// <summary>
        /// 拒绝换课
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refusechangecourse_Click(object sender, RoutedEventArgs e)
        {
            EndConfirm();
        }

        #endregion

        #region 界面逻辑

        /// <summary>
        /// 确认换课
        /// </summary>
        private void StartConfirm()
        {
            int num = 0;
            DataRowView dr = (DataRowView)receiveconfirm.SelectedItem;
            if (dr == null)
            {
                MessageBox.Show("请选中要操作的行!");
                return;
            }

            studentchangeno = (int)dr.Row["ChangeNo"];
            studentconfirmno = (int)dr.Row["ConfirmNo"];

            if ((MessageBox.Show("是否确认接受?", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes))//如果点击“确定”按钮
            {
                num = DoFinalConfirmBussiness();
            }
            else//如果点击“取消”按钮
            {
                return;
            }

            if (num == 1)
            {
                MessageBox.Show("换课成功!");
                return;
            }
            else
            {
                MessageBox.Show("换课失败!");
                return;
            }
        }


        /// <summary>
        /// 拒绝换课
        /// </summary>
        private void EndConfirm()
        {
            int num = 0;
            DataRowView dr = (DataRowView)receiveconfirm.SelectedItem;
            if (dr == null)
            {
                MessageBox.Show("请选中要操作的行!");
                return;
            }

            studentchangeno = (int)dr.Row["ChangeNo"];
            studentconfirmno = (int)dr.Row["ConfirmNo"];

            if ((MessageBox.Show("是否确认拒绝?", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes))//如果点击“确定”按钮
            {
                num = DoRefuseBussiness();
            }
            else//如果点击“取消”按钮
            {
                return;
            }

            if (num == 1)
            {
                MessageBox.Show("拒绝成功!");
                AddConfirm();
                return;
            }
            else
            {
                MessageBox.Show("拒绝失败!");
                return;
            }
        }

        #endregion

        #region 数据库操作

        /// <summary>
        /// 事务判断
        /// </summary>
        /// <returns></returns>
        private int DoRefuseBussiness()
        {
            int i = 0;
            using (DAL.Trans t = new DAL.Trans())
            {
                try
                {
                    D5(t, 3);
                    D6(t, 2);
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

        /// <summary>
        /// 事务判断
        /// </summary>
        /// <returns></returns>
        private int DoFinalConfirmBussiness()
        {
            int i = 0;
            using (DAL.Trans t = new DAL.Trans())
            {
                try
                {
                    D5(t, 2);
                    D6(t, 1);
                    D7(t, 1);
                    D7(t, 2);
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


    }
}
