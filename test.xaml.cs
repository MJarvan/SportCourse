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

namespace sports_course
{
    /// <summary>
    /// test.xaml 的交互逻辑
    /// </summary>
    public partial class test : Window
    {
        public test()
        {
            InitializeComponent();
        }

        private void BTN1_Click(object sender, RoutedEventArgs e)
        {
            int[] num = { 1, 5, 9, 11, 15, 20, 38, 49, 50, 68, 99, 103, 105, 120, 555 };

            int choicenum = num.Length;

            for (int i = 0; i < choicenum; i++)
            {
                string a = test1.Text.ToString();

                string b = num[i].ToString();

                test1.Text = a + " " + b;
            }

            BLL.RandomizedAlgorithm ran = new BLL.RandomizedAlgorithm();

            int[] result;

            result = ran.Random(num, 5);

            for (int i = 0; i < result.Length; i++)
            {
                string a = test2.Text.ToString();

                string b = result[i].ToString();

                test2.Text = a + " " + b;
            }
        }
    }
}
