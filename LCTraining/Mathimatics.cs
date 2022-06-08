using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.MathAlgorithm
{
    public class Mathimatics
    {
        public static Mathimatics Instance = new Mathimatics();

        #region 数学初级
        #region Fizz Buzz
        public static void Test_FizzBuzz()
        {
            Mathimatics ma = new Mathimatics();
            var res =ma.FizzBuzz(30);
        }
        public IList<string> FizzBuzz(int n)
        {
            List<string> result = new List<string>();
            for(int i = 1; i < n; i++)
            {
                string item = "";
                if (i % 3 == 0)
                    item += "Fizz";
                if (i % 5 == 0)
                    item += "Buzz";
                if (item == "")
                    item = i.ToString();
                result.Add(item);
            }
            return result;
        }
        public IList<string> FizzBuzz1(int n)
        {
            int threeIdx = 2;
            int fiveIdx = 4;
            List<string> result = new List<string>();
            for(int i = 1; i <= n; i++)
            {
                if (threeIdx == 0 && fiveIdx==0)
                {
                    result.Add("FizzBuzz");
                }
                else if (threeIdx == 0)
                {
                    result.Add("Fizz");
                }
                else if (fiveIdx == 0)
                {
                    result.Add("Buzz");
                }
                else
                {
                    result.Add(i.ToString());
                }
                threeIdx = Minus(threeIdx, 2);
                fiveIdx = Minus(fiveIdx, 4);
            }
            return result;
        }
        private int Minus(int x, int original)
        {
            if (x == 0)
                return original;
            else
                return --x;
        }
        #endregion
        #region 计算质数
        public static void Test_CountPrimes()
        {
            Mathimatics ma = new Mathimatics();
            var res = ma.CountPrimes(30);
        }
        //埃氏筛选
        public int CountPrimes(int n)
        {
            if (n <= 2)
                return 0;

            bool[] isHe = new bool[n];
            isHe[0] = true;
            isHe[1] = true;
            for(int i = 2; i < n; i++)
            {
                if (isHe[i])
                    continue;
                int idx = 2*i;
                while (idx < n)
                {
                    isHe[idx] = true;
                    idx += i;
                }
            }
            return isHe.Count(p => !p);
        }
        #endregion

        #region 罗马数字转整数
        public int RomanToInt(string s)
        {
            Dictionary<char, int> nums = new Dictionary<char, int>
            {
                {'I',1 },{ 'V',5},{ 'X',10},{ 'L',50},{ 'C',100},{ 'D',500},{ 'M',1000}
            };
            Dictionary<string, int> special = new Dictionary<string, int>
            {
                {"IV",4 },{"IX", 9},{"XL", 40},{"XC", 90},{"CD", 400 },{"CM", 900 }
            };
            int result = 0;
            for(int i = 0; i < s.Length; i ++)
            {
                if (i < s.Length - 1)
                {
                    string token = $"{s[i]}{s[i + 1]}";
                    if (special.ContainsKey(token))
                    {
                        result += special[token];
                        i++;
                        continue;
                    }
                }
                result += nums[s[i]];
            }
            return result;
        }
        #endregion

        #endregion

        #region 数学中级

        #region 快乐数
        //思路：如果出现循环，则认为不是快乐数；否则一直计算下去
        //内置步骤： 遍历十进制数的每一位， 计算平方和，得到下一个数
        public bool IsHappy(int n)
        {
            int maxLoop = 1000;
            HashSet<int> nextLp = new HashSet<int>();
            int next = n;
            for(int i = 0; i < maxLoop; i++)
            {
                next = NextNumber(n);
                if (next == 1)
                    return true;
                if (nextLp.Contains(next))
                    return false;
                nextLp.Add(next);
                n = next;
            }
            return false;
        }
        private int NextNumber(int n)
        {
            //如何遍历 十进制数的每一位：
            int next = 0;
            while (n != 0) 
            {
                next += (n % 10)*(n%10);
                n = n / 10;
            }
            return next;
        }
        #endregion

        #region 阶乘后，零的数量
        //思路：每出现一个0，代表中间乘的过程中出现了1次10，即2*5。因此，n阶乘中，所有数进行质因数分解，得到几次（2、5），就有几个10。
        //由于分解出来2的数量远多于5，因此，只要计算5就可以（肯定有足够的2跟它配对）。
        public int TrailingZeroes(int n)
        {
            int count = 0;
            while (n >= 5)
            {
                //例如，，49/5=9，代表从49递减到5，中的数，可以分解出来9个5（5，10，15，20，25，30，35，40，45）。
                //注意：由于25能分解出来2个5，因此，还要继续算
                count += n / 5;
                //这一步，把n除以5。进入下个循环。
                //比如：
                //49->9，下个循环中， 9/5=1。 表示 49缩到5，中间经历了1次25。
                //200->40，下个循环中，40/5=8，表示 49缩到5过程中，经历了8次25，然后再缩一次，40->8，表示经历了1次125。
                n = n / 5; 
            }
            return count;
        }
        #endregion


        #region EXCEL 表列序号
        public int TitleToNumber(string columnTitle)
        {
            int count = 0;
            foreach (var n in columnTitle)
            {
                count = (n - 'A'+1) + count * 26;
            }
            return count;
        }
        #endregion

        #region 自己实现指数函数：POW(x,n)
        
        #endregion

        #region 求算术平方根
        //思路：二分递进查找
        public int MySqrt(int x)
        {
            if (x == 1)
                return 1;
            int left = 0, right = x;
            long mid = 0;
            do
            {
                mid = left + (right - left) / 2;
                var diff = mid * mid - x;

                if (diff > 0)
                    right = (int)mid;
                else if (diff < 0)
                    left = (int)mid;
                else
                    return (int)mid;
            } while (mid >= 1 && right-left>1);
            return left;
        }
        #endregion
        #endregion
    }
}
