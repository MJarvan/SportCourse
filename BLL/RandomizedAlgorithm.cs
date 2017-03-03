using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sports_course.BLL
{
    /// <summary>
    /// 选课随机抽取算法
    /// </summary>
    public class RandomizedAlgorithm
    {
        public int[] Random (int[] index, int choicemax)
        {
            Random r = new Random();
            int site = index.Length;//设置上限
            int[] result = new int[choicemax];
            int id;
            for (int j = 0; j < choicemax; j++)
            {
                id = r.Next(1, site - 1);
                //在随机位置取出一个数，保存到结果数组 
                result[j] = index[id];
                //最后一个数复制到当前位置 
                index[id] = index[site - 1];
                //位置的上限减少一 
                site--;
            }

            return result;
        }

    }
}
