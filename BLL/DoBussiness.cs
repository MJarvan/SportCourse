using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;
using sports_course.DAL;

namespace sports_course.BLL
{
    /// <summary>
    /// 事务判断集合
    /// </summary>
    public class DoBussiness
    {
        /// <summary>
        /// 向学生选课表(SSC)进行操作
        /// </summary>
        /// <param name="t"></param>
        /// <param name="SportCourseNo"></param>
        /// <param name="SSChoice"></param>
        public static void D1(Trans t, int StudentNo, int SportCourseNo, int i)
        {
            DAL.DbHelper db = new DAL.DbHelper();
            //插入数据到SSC
            if (i == 1)
            {
                DbCommand insert = db.GetSqlStringCommond("insert into StudentSportCourse values (@StudentNo, @SportCourseNo, @SSChoice)");

                db.AddInParameter(insert, "@StudentNo", DbType.Int32, StudentNo);
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
            //更新SSC中学生换课中状态
            else if (i == 2)
            {
                DbCommand updateSSC = db.GetSqlStringCommond("update StudentSportCourse set SSChoice= @SSChoice where StudentNo= @StudentNo and SportCourseNo= @SportCourseNo");
                db.AddInParameter(updateSSC, "@StudentNo", DbType.Int32, StudentNo);
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
            //更新SSC中学生换课成功状态
            else if (i == 3)
            {
                DbCommand updateSSC = db.GetSqlStringCommond("update StudentSportCourse set SSChoice= @SSChoice where StudentNo= @StudentNo and SportCourseNo= @SportCourseNo");
                db.AddInParameter(updateSSC, "@StudentNo", DbType.Int32, StudentNo);
                db.AddInParameter(updateSSC, "@SportCourseNo", DbType.Int32, SportCourseNo);
                db.AddInParameter(updateSSC, "@SSChoice", DbType.String, "1");
                if (t == null)
                {
                    db.ExecuteNonQuery(updateSSC);
                }
                else
                {
                    db.ExecuteNonQuery(updateSSC, t);
                }
            }

            //更新SSC中学生退课
            else if (i == 4)
            {
                DbCommand updateSSC = db.GetSqlStringCommond("update StudentSportCourse set SSChoice= @SSChoice where StudentNo= @StudentNo and SportCourseNo= @SportCourseNo");
                db.AddInParameter(updateSSC, "@StudentNo", DbType.Int32, StudentNo);
                db.AddInParameter(updateSSC, "@SportCourseNo", DbType.Int32, SportCourseNo);
                db.AddInParameter(updateSSC, "@SSChoice", DbType.String, "3");
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
        /// 学生选课或者抢课后更新课程信息
        /// </summary>
        /// <param name="t"></param>
        /// <param name="sportCourseNo"></param>
        public static void D2(Trans t, DataTable dt, int SportCourseNo, int i)
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
                    break;
                }
            }
        }

        /// <summary>
        /// 更新学生换课表的学生信息
        /// </summary>
        /// <param name="t"></param>
        /// <param name="i"></param>
        public static void D3(Trans t,int studentchangeno, int i)
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
        public static void D4(Trans t, int studentconfirmno, int i)
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
        /// 更新体育选课的已选人数
        /// </summary>
        /// <param name="t"></param>
        /// <param name="SportCourseNo"></param>
        public static void D5(Trans t, int SportCourseNo, int i)
        {
            //读取该课程的choicenumafter
            DAL.DbHelper db = new DAL.DbHelper();
            int choicenumafter = 0;

            DbCommand selectSC = db.GetSqlStringCommond("select ChoiceNumAfter from SportCourse where SportCourseNo=" + SportCourseNo);
            using (DbDataReader reader = db.ExecuteReader(selectSC))
            {
                while (reader.Read())
                {
                    choicenumafter = reader.GetInt32(0);
                }
            }
            //如果是筛选的+1
            if (i == 1)
            {
                choicenumafter = choicenumafter + 1;
            }
            //如果是退课的-1
            else if (i == 2)
            {
                choicenumafter = choicenumafter - 1;
            }

            //把choicenumafter存回去
            DbCommand updateSC = db.GetSqlStringCommond("update SportCourse set ChoiceNumAfter=" + choicenumafter + "where SportCourseNo=" + SportCourseNo);

            if (t == null)
            {
                db.ExecuteNonQuery(updateSC);
            }
            else
            {
                db.ExecuteNonQuery(updateSC, t);
            }
        }

        /// <summary>
        /// 更新选课控制的选课状态
        /// </summary>
        /// <param name="t"></param>
        public static void D6(Trans t)
        {
            DAL.DbHelper db = new DAL.DbHelper();
            DbCommand updateChoice = db.GetSqlStringCommond("update CourseControl set ChoiceControl=" + 2);

            if (t == null)
            {
                db.ExecuteNonQuery(updateChoice);
            }
            else
            {
                db.ExecuteNonQuery(updateChoice, t);
            }
        }

        /// <summary>
        /// 生成退课表
        /// </summary>
        /// <param name="t"></param>
        /// <param name="studentNo"></param>
        /// <param name="sportCourseNo"></param>
        public static void D7(Trans t, int studentNo, int sportCourseNo)
        {
            DAL.DbHelper db = new DAL.DbHelper();
            DateTime DropCreateTime = new DateTime();
            DropCreateTime = DateTime.Now;
            DbCommand insert = db.GetSqlStringCommond("insert into DropCourse values (@StudentNo, @SportCourseNo, @DropCreateTime)");

            db.AddInParameter(insert, "@StudentNo", DbType.Int32, studentNo);
            db.AddInParameter(insert, "@SportCourseNo", DbType.Int32, sportCourseNo);
            db.AddInParameter(insert, "@DropCreateTime", DbType.DateTime, DropCreateTime);


            if (t == null)
            {
                db.ExecuteNonQuery(insert);
            }
            else
            {
                db.ExecuteNonQuery(insert, t);
            }
        }

        /// <summary>
        /// 重置所有学生的课程控制
        /// </summary>
        /// <param name="t"></param>
        public static void D8(Trans t , int v)
        {
            DAL.DbHelper db = new DAL.DbHelper();
            //重置选课
            if(v == 1)
            {
                DbCommand updateControl = db.GetSqlStringCommond("update Student set ChoiceControl=" + 0);
                if (t == null)
                {
                    db.ExecuteNonQuery(updateControl);
                }
                else
                {
                    db.ExecuteNonQuery(updateControl, t);
                }
            }
            //重置换课
            else if (v == 2)
            {
                DbCommand updateControl = db.GetSqlStringCommond("update Student set ChangeControl=" + 0);
                if (t == null)
                {
                    db.ExecuteNonQuery(updateControl);
                }
                else
                {
                    db.ExecuteNonQuery(updateControl, t);
                }
            }
            //重置换课确认
            else if (v == 3)
            {
                DbCommand updateControl = db.GetSqlStringCommond("update Student set ConfirmControl=" + 0);
                if (t == null)
                {
                    db.ExecuteNonQuery(updateControl);
                }
                else
                {
                    db.ExecuteNonQuery(updateControl, t);
                }
            }
        }

        /// <summary>
        /// 删除失效记录
        /// </summary>
        /// <param name="t"></param>
        public static void D9(Trans t, int v)
        {
            DAL.DbHelper db = new DAL.DbHelper();
            //删除换课失效记录
            if (v == 1)
            {
                DbCommand delete = db.GetSqlStringCommond("delete from ChangeCourse where ChangeControl!=" + 2);
                if (t == null)
                {
                    db.ExecuteNonQuery(delete);
                }
                else
                {
                    db.ExecuteNonQuery(delete, t);
                }
            }
            //删除换课确认失效记录
            else if (v == 2)
            {
                DbCommand delete = db.GetSqlStringCommond("delete from ConfirmCourse where ConfirmControl!=" + 1);
                if (t == null)
                {
                    db.ExecuteNonQuery(delete);
                }
                else
                {
                    db.ExecuteNonQuery(delete, t);
                }
            }
        }
    }
}
