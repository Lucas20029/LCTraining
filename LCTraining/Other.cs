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


        #region 15 三数之和 --双指针
        //思路：排序， 然后逐个遍历， 把3数之和降为2数之和。  2数之和，使用首尾双指针解决
        //难点：在于如何去除重复解。
        /*
        特判，对于数组长度 nn，如果数组为 nullnull 或者数组长度小于 33，返回 [][]。
        对数组进行排序。
        遍历排序后数组：
        若 nums[i]>0nums[i]>0：因为已经排序好，所以后面不可能有三个数加和等于 00，直接返回结果。
        对于重复元素：跳过，避免出现重复解
        令左指针 L=i+1L=i+1，右指针 R=n-1R=n−1，当 L<RL<R 时，执行循环：
        当 nums[i]+nums[L]+nums[R]==0nums[i]+nums[L]+nums[R]==0，执行循环，判断左界和右界是否和下一位置重复，去除重复解。并同时将 L,RL,R 移到下一位置，寻找新的解
        若和大于 00，说明 nums[R]nums[R] 太大，RR 左移
        若和小于 00，说明 nums[L]nums[L] 太小，LL 右移

        
        链接：https://leetcode.cn/problems/3sum/solution/pai-xu-shuang-zhi-zhen-zhu-xing-jie-shi-python3-by/
             
        */
        public void ThreeSum_Test()
        {
            int[] nums = new int[] { -5,-3,-3,-1,1,2,4 };
            nums = new int[] { 0,0,0};
            var res = ThreeSum(nums);
        }
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            List<IList<int>> result = new List<IList<int>>();
            if (nums == null || nums.Length < 3)
                return result;

            nums = nums.OrderBy(p => p).ToArray();
            int lastVal = int.MaxValue;
            for(int i = 0; i < nums.Length; i++)
            {
                if (lastVal != int.MaxValue && lastVal == nums[i])
                {
                    lastVal = nums[i];
                    continue;
                }
                var res = TwoSum(nums, -nums[i], i);
                result.AddRange(res);
                lastVal = nums[i];
            }
            return result;
        }
        public IList<IList<int>> TwoSum(int [] nums, int target, int skipIndex)
        {
            List<IList<int>> result = new List<IList<int>>();
            int left = skipIndex+1, right = nums.Length-1;
            while (left < right)
            {
                if(left==skipIndex)
                {
                    left++;
                    continue;
                }
                if (right == skipIndex)
                {
                    right--;
                    continue;
                }
                var sum = nums[left] + nums[right];
                if (sum < target)
                    left++;
                else if (sum > target)
                    right--;
                else //==
                {
                    result.Add(new List<int> { -target, nums[left], nums[right] });
                    int leftVal = nums[left], rightVal = nums[right];
                    while (left<nums.Length && nums[left] == leftVal)
                        left++;
                    while (right>=0 && nums[right] == rightVal)
                        right--;
                }
            }
            return result;
        }
        #endregion

        //#region 9 回文数
        //public bool IsPalindrome(int x)
        //{

        //}
        //#endregion

        #region 152 乘积最大子数组
        public void Test_MaxProduct()
        {
            var res1 = MaxProduct(new[] { 2, 3, -2, 4 });
            var res2 = MaxProduct(new[] { -2, 0, -1 });
            var res3 = MaxProduct(new[] { 2, -5, -2, -4, 3 });

        }
        /*
         自己理解：
         1. 数组中出现0， 那么结果集肯定不能跨越0（否则乘积就是0了）。 因此，可以按0拆分为多个 不含0的数组。
         2. 对于不含0的数组，分两种情况：
            1. 乘积>0， 那么就是该数组的Max
            2. 乘积<0， 那么里面肯定有奇数个负数。
                举例：  6,3,-5,6,2,-1,9,-8，2,3。  要想结果为最大， 必须要舍弃两头的某一个负数。
                对于本例中，要么舍弃头： 6、3、-5， 要么舍弃尾：-8、2、3。 就是比较剩余哪个大即可。
        */
        public int MaxProduct(int[] nums)
        {
            var hasZero = false;
            List<List<int>> numarrs = new List<List<int>>() { new List<int>() };
            foreach(var num in nums)
            {
                if (num == 0)
                {
                    numarrs.Add(new List<int>());
                    hasZero = true;
                }   
                else
                    numarrs.Last().Add(num);
            }
            int max =hasZero?0: int.MinValue;
            foreach(var numarr in numarrs)
            {
                max = Math.Max(max, MaxNoneZero(numarr));
            }
            return max;
        }
        public int MaxNoneZero(List<int> nums)
        {
            if (nums.Count == 0)
                return 0;
            if (nums.Count == 1)
                return nums[0];
            int product = 1;
            foreach(var num in nums)
            {
                product *= num;
            }
            if (product > 0)
                return product;
            int leftProduct = product, rightProduct = product;
            foreach(var num in nums)
            {
                leftProduct = leftProduct / num;
                if (num < 0)
                    break;
            }
            for (int i =nums.Count-1; i>=0;i--)
            {
                rightProduct = rightProduct / nums[i];
                if (nums[i] < 0)
                    break;
            }
            return Math.Max(leftProduct, rightProduct);
        }

        //官方题解，没看懂
        public int Standard_MaxProduct(int [] nums)
        {
            int max = int.MinValue, imax = 1, imin = 1;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] < 0)
                {
                    int temp = imin;
                    imin = imax;
                    imax = temp;
                }
                imax = Math.Max(imax * nums[i], nums[i]);
                imin = Math.Min(imin * nums[i], nums[i]);

                max = Math.Max(imax, max);
            }
            return max;
        }
        #endregion
    }
}
