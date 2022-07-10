using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    public class Day1
    {
        /// <summary>
        /// #509. 斐波那契数列
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int Fib(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;
            int last = 1, lastlast = 0;
            for (int i = 2; i <= n; i++)
            {
                var t = last + lastlast;
                lastlast = last;
                last = t;
            }
            return last;
        }
        /// <summary>
        /// #1137. 第 N 个泰波那契数
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int Tribonacci(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;
            if (n == 2) return 1;
            int prev1 = 1, prev2 = 1, prev3 = 0;
            for (int i = 3; i <= n; i++)
            {
                int t = prev1 + prev2 + prev3;
                prev3 = prev2;
                prev2 = prev1;
                prev1 = t;
            }
            return prev1;
        }
    }
}
