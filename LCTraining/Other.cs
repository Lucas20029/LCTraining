using System;
using System.Collections.Generic;
using System.Linq;

namespace LCTraining
{
    public class Other
    {
        public static Other Instance = new Other();
        //简单
        #region 位1的个数
        //思路：遍历位数。
        //  方法：  >>向右移位。
        //          n&1=1 用于判断n的最右1位是否为1
        public int HammingWeight(uint n)
        {
            int count = 0;
            while (n != 0)
            {
                if ((n & 1) == 1)
                    count++;
                n = n >> 1;
            }
            return count;
        }
        #endregion

        #region 汉明距离 （2个数，每个位不同的个数）
        //思路： 先两数异或。 然后对于结果，计算位1个数
        public int HammingDistance(int x, int y)
        {
            var temp = x ^ y;
            int count = 0;
            while (temp != 0)
            {
                if ((temp & 1) == 1)
                    count++;
                temp = temp >> 1;
            }
            return count;
        }
        #endregion

        #region 颠倒二进制位
        //思路： 进行32次循环，每次 n右移1位，result左移1位
        // 每次判断n的末位是1还是0。
        //  如果是1，则Result末尾变成1。  方法：  Result = Result | 1;  即 Result对1求或
        public uint reverseBits(uint n)
        {
            uint result = 0;
            for(int i = 0; i < 32; i++)
            {
                result = result << 1;
                if ((n & 1) == 1)
                    result |= 1;
                n = n >> 1;
            }
            return result;
        }
        #endregion

        #region 杨辉三角
        public IList<IList<int>> GenerateYanghuiTriangle(int numRows)
        {
            List<IList<int>> result = new List<IList<int>>();
            for(int i = 1; i <= numRows; i++)
            {
                List<int> row = new List<int>();
                for(int j = 0; j < i; j++)
                {
                    if (j == 0 || j == i - 1)
                    {
                        row.Add(1);
                    }
                    else
                    {
                        var val =  result.Last()[j - 1] + result.Last()[j];
                        row.Add(val);
                    }
                }
                result.Add(row);
            }
            return result;
        }
        #endregion

        #region 有效的括号
        //判断 () {} [] 组成的字符串 是否是有效的括号
        //思路： 碰到左括号，入栈；碰到右括号，出栈并匹配
        public bool IsValidBrakets(string s) 
        {
            Stack<char> tokens = new Stack<char>();
            foreach (var charactor in s)
            {
                if (charactor == '(' || charactor == '{' || charactor == '[')
                {
                    tokens.Push(charactor);
                }
                else
                {
                    if (!tokens.Any())
                        return false;
                    var pair = tokens.Pop();
                    if (pair == '(' && charactor == ')')
                        continue;
                    if (pair == '[' && charactor == ']')
                        continue;
                    if (pair == '{' && charactor == '}')
                        continue;
                    return false;
                }
            }
            if (!tokens.Any())
                return true;
            else
                return false;
        }
        #endregion

        #region 缺失数字
        //题目：严格限定，  数组有n个元素，且元素不重复，取值范围是 [0,n]闭区间。
        //求，缺失了哪个元素。 比如，数组是 [0,1,3]，缺了2。
        public int MissingNumber(int[] nums)
        {
            var result = 0;
            foreach(var num in nums)
            {
                result ^= num;
            }
            for(int i = 0; i <= nums.Length; i++)
            {
                result ^= i;
            }
            return result;
        }
        #endregion

        //中级
        #region 两数之和，不用+、-实现
        //思路：  a^b： ab异或，表示 a+b 不进位的部分
        //        a&b： a且b，表示 a+b 进位的部分， 再右移1位（<<1），进行进位。 
        //        这俩相加，得到进位运算结果。
        //举例：  0111 + 0111 = 1110
        //步骤1： 异或运算：0111^0111=0000； 且运算结果右移： 0111&0111=0111， 0111<<1=1110， 二者相加： 0000+1110
        //步骤2： 异或运算：0000^1110=1110； 且运算结构右移： 0000&1110=0000， 0000<<1=0000， 二者相加： 1110+0000
        //步骤3： 0000=0， 结束，返回 1110
        public int GetSum(int a, int b)
        {
            if (b == 0)
                return a;
            return GetSum((a ^ b), (a & b)<<1);
        }
        #endregion

        #region 逆波兰表达式
        public int EvalRPN(string[] tokens)
        {
            Stack<int> stack = new Stack<int>();
            foreach(var token in tokens)
            {
                if (Operators.ContainsKey(token))
                {
                    var num1 = stack.Pop();
                    var num2 = stack.Pop();
                    stack.Push( Operators[token](num2,num1) );
                }
                else
                {
                    stack.Push(int.Parse(token));
                }
            }
            return stack.Pop();
        }
        Dictionary<string, Func<int, int, int>> Operators = new Dictionary<string, Func<int, int, int>>
        {
            { "+", (a,b)=>a+b },
            { "-", (a,b)=>a-b },
            { "*", (a,b)=>a*b },
            { "/", (a,b)=>a/b },
        };
        #endregion

        #region 多数元素
        //思路：注意题目中降低了难度，则数组中一定存在多数元素
        public int MajorityElement(int[] nums)
        {
            int count = 0;
            int major = nums[0];
            for(int i=0;i<nums.Length;i++)
            {
                var item = nums[i];
                if (item == major)
                    count++;
                else
                {
                    count--;
                    if (count == 0)
                    {
                        major = nums[i+1];
                    }
                }
            }
            return major;
        }
        #endregion

        #region 任务调度器
        //思路： 找到出现次数最大的元素，计出现次数为MaxFreq。 那么，如果恰好完成它，所需最小时间为 MinTime = (MaxFreq-1)*(n+1)+1
        //特例1，如果出现多个出现次数最多的元素，比如 [A,A,A,B,B,B]，那么，公式末尾有几（MaxEleCount）个最多的元素，就加几 MinTime = (MaxFreq-1)*(n+1)+MaxEleCount
        //特例2，如果数列长度超出了 MinTime，则表示该数列为不饱和任务数列，返回数列长度即可。
        public int LeastInterval(char[] tasks, int n)
        {
            var frequencies = tasks.GroupBy(c => c).Select(c => c.Count()).OrderByDescending(c=>c).ToList();
            var maxFreq = frequencies[0];
            var maxFreqCount = frequencies.Count(c => c == maxFreq);
            int taskCount = tasks.Length;

            int needWaitingCase = (maxFreq - 1) * (n + 1) + maxFreqCount;
            return taskCount > needWaitingCase ? taskCount : needWaitingCase;
        }
        #endregion

        #region 1652 拆炸弹  简单
        public void Decrypt_Test()
        {
            var res = Decrypt(new[] { 2,4,9,3 }, -2);
        }
        public int[] Decrypt(int[] code, int k)
        {
            List<int> result = new List<int>();
            for(int i = 0; i < code.Length; i++)
            {
                result.Add(SumKElements(code, k, i));
            }
            return result.ToArray();
        }
        private int SumKElements(int [] code, int k, int index)
        {
            if (k == 0)
                return 0;
            int sum = 0;
            for(int i = 1; i <= Math.Abs(k); i++)
            {
                if (k > 0)
                    sum += code[(i + index) % code.Length];
                else
                    sum += code[(-i+index+ code.Length) %code.Length]; //注意，要加上code.Length，避免index出现负数
            }
            return sum;
        }
        #endregion

        #region 9 回文数
        public void Test_IsPalindrome()
        {
            var res0 = IsPalindrome(13231) == true;
            var res1 = IsPalindrome(1000021) == false;
            var res2 = IsPalindrome(13255231) == true;
            var res3 = IsPalindrome(13221) == false;
            var res4 = IsPalindrome(9) == true;
            var res5 = IsPalindrome(10) == false;
        }
        public bool IsPalindrome(int x)
        {
            // 23132
            // 23132 % 10 = 2,  23132/10000=2，  23132%10000=3132, 3132/10=313
            // 313 % 10 =3  313/100=3    313%100=13， 13/10=1

            // 77 %10 =7, 77/10=7   77%10=7, 7/10=0
            if (x < 0)
                return false;
            if (x < 10)
                return true;

            var num = x;
            int maxDividor = 1;
            while (num >= 10)
            {
                num = num / 10;
                maxDividor *= 10;
            }
            num = x;
            while (num>0)
            {
                var right = num % 10;
                var left = num / maxDividor;
                if (right != left)
                    return false;

                num = num % maxDividor;
                num = num / 10;
                maxDividor = maxDividor / 100;
            }
            return true;
        }
        #endregion
    }
}
