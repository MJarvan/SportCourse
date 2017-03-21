using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace sports_course
{
    /// <summary>
    /// TimeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TimeWindow : Window
    {
        public TimeWindow()
        {
            InitializeComponent();
        }

        System.Timers.Timer aTimer = new System.Timers.Timer();
        static int elapsedTimes;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            aTimer.Interval = 2000;    // 1秒 = 1000毫秒
        }

        /// <summary>
        /// Timer的Elapsed事件处理程序
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            MessageBox.Show((++elapsedTimes).ToString(), "Timer测试");
        }


        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            aTimer.Start();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            aTimer.Stop();
            MessageBox.Show("Timer已停止，之前共触发次" + (elapsedTimes).ToString() + "事件", "Timer测试");
            elapsedTimes = 0;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            aTimer.Dispose();   // 清理aTimer使用的内存
            MessageBox.Show("如果您是在点击停止之前点击此按钮，将会造成无法停止Timer！\n此时您可以返回您的开发工具停止调试项目。\n或直接在资源管理器中终止进程。\n\n\n 在使用本Demo的同时，由于个人的开发环境不同，所以请不要简单的拷贝代码");
            this.Close();
        }
    }
}
