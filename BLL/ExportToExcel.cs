using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Windows;

namespace sports_course.BLL
{
    /// <summary>
    /// 导出到Excel
    /// </summary>
    public class ExportToExcel
    {
        /// <summary>
        /// 创建Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="File"></param>
        /// <param name="header"></param>
        public static void CreateExcel (System.Data.DataTable dt , string File , string[] header)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            Workbook excelWB = excelApp.Workbooks.Add(System.Type.Missing);    //创建工作簿（WorkBook：即Excel文件主体本身）  
            Worksheet excelWS = (Worksheet)excelWB.Worksheets[1];   //创建工作表（即Excel里的子表sheet） 1表示在子表sheet1里进行数据导出  

            excelWS.Cells.NumberFormat = "@";     //  如果数据中存在数字类型 可以让它变文本格式显示

            //导入表头
            for (int a = 0; a < header.Length; a++)
            {
                excelWS.Cells[1,a + 1] = header[a].ToString().Trim();
            }

            //将数据导入到工作表的单元格  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    excelWS.Cells[i + 2, j + 1] = dt.Rows[i][j].ToString().Trim();   //Excel单元格第一个从索引1开始  
                }
            }
            try
            {
                excelWB.SaveAs(File);  //将其进行保存到指定的路径  
            }
            catch
            {
                MessageBox.Show("导出失败!文件已存在!");
                return;
            }
            excelWB.Close();
            //释放可能还没释放的进程 
            if(KillAllExcel(excelApp) == true)
            {
                MessageBox.Show("导出成功!");
            }
            else
            {
                MessageBox.Show("导出失败!");
            }
        }

        /// <summary>
        /// 释放Excel进程
        /// </summary>
        /// <param name="excelApp"></param>
        /// <returns></returns>
        public static bool KillAllExcel(Microsoft.Office.Interop.Excel.Application excelApp)
        {
            try
            {
                if (excelApp != null)
                {
                    excelApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                    //释放COM组件，其实就是将其引用计数减1     
                    //System.Diagnostics.Process theProc;     
                    foreach (System.Diagnostics.Process theProc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
                    {
                        //先关闭图形窗口。如果关闭失败.有的时候在状态里看不到图形窗口的excel了，     
                        //但是在进程里仍然有EXCEL.EXE的进程存在，那么就需要释放它     
                        if (theProc.CloseMainWindow() == false)
                        {
                            theProc.Kill();
                        }
                    }
                    excelApp = null;
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
