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

        #region 67 二进制求和
        public void Test_AddBinary()
        {
            var res1 =AddBinary("10010", "11011") == "101101";
            var res2 = AddBinary("110110111", "10111") == "111001110";
        }
        public string AddBinary(string a, string b)
        {
            var min = Math.Min(a.Length, b.Length);
            var max = Math.Max(a.Length, b.Length);
            if (max > min)
            {
                string s = new string('0', max - min);
                if (min == a.Length)
                    a = s + a;
                else
                    b = s + b;
            }
            
            StringBuilder stringBuilder = new StringBuilder();
            bool isProceed = false;
            for(int i = a.Length-1; i >= 0; i--)
            {
                var res = a[i] - '0' + b[i] - '0' + (isProceed ? 1 : 0);
                isProceed = res >= 2;
                stringBuilder.Insert(0, isProceed ? res - 2  : res);
            }
            if (isProceed)
                stringBuilder.Insert(0, 1);

            return stringBuilder.ToString();
        }
        #endregion

        #region 168 Excel表列名称  (看答案会的)
        /* A=0,B=1,C=2,,,,Z=25 : (Num-1)%26
         * Num/26
         * AA=26, AB=27, BA=53,BB=54,ZA=651,ZB=652,ZZ=676: 
         * 以651为例： 
         *   循环1：（651-1）%26 = 0 ->A,   651/26=1
         *   循环2： （1-1）%26=0 ->A 
         * 以676为例：
         *   循环1： （676-1）%26 = 25 ->Z， 676/26=26
         *   循环2： （26-1）%26 = 25 ->Z
         * 
         * 总结规律： (num-1)%26 得末位， num/26得到下一循环的num，继续循环计算
         ***/
        public void Test_ConvertToTitle()
        {
            var res1 = ConvertToTitle(26);//Z
            var res2 = ConvertToTitle(27);//AA
            var res3 = ConvertToTitle(676);//ZZ
            var res4 = ConvertToTitle(17576);//AAAZ
        }
        public string ConvertToTitle(int columnNumber)
        {
            StringBuilder stringBuilder = new StringBuilder();
            while (columnNumber>0)
            {
                char c = (char)((--columnNumber) % 26 + 'A');
                stringBuilder.Insert(0, c);
                columnNumber = columnNumber / 26;
            }
            return stringBuilder.ToString();
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

        #region 29 两数相除
        /*
        思路：使用除数累加，跟被除数比较。 例如： 13/4
        1. 4<31
        2. 4+4=8<31
        3. 8+8=16<31
        4. 16+16=32>31  超了！
        然后从16开始，从第一步递归
        5. 16+4=20
        6. 16+4+4=16+8=24<31
        7. 16+8+8=16+16=32<31  超了！
        递归
        8. 24+4....
        注意整数边界问题
        */
        public void Test_Divide()
        {
            var res0 = Divide(7, -3);
            var res1 = Divide(47, 3);
            var res2 = Divide(-8, 5);
            var res3 = Divide(-101, -3);
            var res4 = Divide(-2147483648, -1);
        }
        public int Divide(int dividend, int divisor)
        {

            if (dividend == 0)
                return 0;
            if (divisor == 1)
                return dividend;
            if (divisor == -1)
            {
                if (dividend > int.MinValue) return dividend * -1;
                else return int.MaxValue;
            }

            bool neg = (dividend < 0) ^ (divisor < 0);
            long dividend1 = Math.Abs((long)dividend);
            long divisor1 = Math.Abs((long)divisor);
            return JumpForDivide(dividend1, divisor1, 0, 0) * (neg ? -1 : 1);
        }
        public int JumpForDivide(long target, long step, long baseVal, long baseStep)
        {
            if (target < step + baseVal)
                return (int)baseStep;
            int times = 1;
            long sum = step;
            while (sum + sum + baseVal <= target)
            {
                sum += sum;
                times += times;
            }
            if (times == 1 || sum == target)
                return (int)(times + baseStep);
            else
                return JumpForDivide(target, step, sum + baseVal, baseStep + times);
        }
        #endregion
        #endregion

        #region 特殊 -- 卡特兰数
        /*
         * Q96： 给定数字n，求可以组成多少种 二叉搜索树？
         * 
         * -- 这是卡特兰数的经典题目。  
         * 
         * 卡特兰数的特点：  f(n)= f(0)f(n-1) + f(1)f(n-2) +......+ f(n-1)f(0)
         * 
         * 以本题为例，
         *  若i为二叉搜索树的根节点， 那么， 有i-1个节点，必然挂在i的left下； 有n-i个节点，必然挂在i的right下。
         *  而挂在left下的i-1个节点组成的 子二叉搜索树 又是f(i-1)的问题； 同样，right下的n-i个节点组成的 子二叉搜索树 也是 f(n-i)的问题。
         *  而左右各自不同的挂法，是个排列问题，因此是f(left)*f(right)，即f(i-1)*f(n-i)；
         *  
         *  例如，有5个节点，以3为根节点，那么，1、2在3的left下， 4、5在3的right下。即：f(3-1)*f(5-3) = f(2)*f(2)
         *  而，f(2)= f(0)*f(1)+f(1)*f(0) = 1+1 = 2
         * 
         * 卡特兰数的其他经典题目：
         *     1. n个数，入栈顺序为 1,2,3,4,5...n， 求有多少种出栈可能。注意：入栈操作中可能插入出栈操作。 例如，n=3，  in(1) -> in(2) - out(2) - out(1) - in(3) - out(3)， 即213 为一种可能。
         *        解析： 我们这样想，最后一个出栈的数是k， 那么 比k早进栈且早出栈的有k-1个数，一共有h(k-1)种方案；比k晚进栈且早出栈的有n-k个数，一共有h(n-k)种方案。所以一共有h(k-1)*h(n-k)种方案
         * 
         * 在计算时，由于f(i)是确定的数，因此可以缓存。
        */

        #region 96. 不同的二叉搜索树  
        public void Test_NumTrees()
        {
            var res0 = NumTrees(0);
            var res1 = NumTrees(1);
            var res2 = NumTrees(2);
            var res3 = NumTrees(3);
            var res4 = NumTrees(4);
        }
        public int NumTrees(int n)
        {
            if (n == 0 || n == 1)
                return 1;
            int[] arr = new int[n+1];
            arr[0] = arr[1] = 1;
            for(int i = 2; i <= n; i++)
            {
                int sum = 0;
                for(int j = 0; j < i; j++)
                {
                    sum += arr[j] * arr[i - j-1];
                }
                arr[i] = sum;
            }
            return arr[n];
        }
        #endregion
        #endregion
    }
}
