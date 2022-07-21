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
            Day5 d5 = new Day5();
            int res =d5.MaxSubarraySumCircular(new[] { 5, -3, 5 });
            int[] arr = new int[10];
            Day3 d3 = new Day3();
            var dres2 = d3.Rob2(new[] { 1, 2, 3, 1 });
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
    public class Day2
    {
        /// <summary>
        /// #746. 使用最小花费爬楼梯
        /// 先倒推。 以 [1,2,3,4,5,6,7]为例：
        /// 要想爬到#7， 有两种办法：
        ///    1. 在 #6的花费基础上，买6，即可跳到7
        ///    2. 在 #5的花费基础上，买5，直接跳2层，到7 （不要跳1层啦，因为这样就回到了步骤1）
        ///    3. 比较这两者的花费，取最小的。
        /// 那么，怎么计算 #6、#5的花费呢？
        /// 同样的方法， #6 = Min（#4+cost[4]， #5+cost[5]）
        /// 因此，可得递推公式： #n = Min(#[n-1] + cost[n-1]， #[n-2] + cost[n-2])。
        /// 这样是递归解法，很耗时。
        /// 在此基础上，用正推法，再思考一遍：
        /// #1= 0
        /// #2= 0
        /// #3= Min(#2+cost2, #1+cost1) = 1
        /// #4= Min(#3+cost3, #2+cost2) = 2
        /// ....
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        public int MinCostClimbingStairs(int[] cost)
        {
            //f(n) =min( f(n-1)+cost(n-1) , f(n-2)+cost(n-2)  )
            int pprev = 0, prev = 0;
            for (int i = 2; i <= cost.Length; i++)
            {
                int t1 = prev + cost[i - 1];
                int t2 = pprev + cost[i - 2];
                int t = Math.Min(t1, t2);
                pprev = prev;
                prev = t;
            }
            return prev;
        }

    }
    public class Day3
    { 
        /// <summary>
        /// #198. 打家劫舍
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int Rob(int[] nums)
        {
            /*
            到末尾（第n个）房间，要想偷到最大值，只有两种算法
            Max（偷到第n-2个的总金额+第n个房间金额， 偷到第n-1个的总金额，不偷第n个）
            因此： Sum(n) = Max( Sum(n-2)+nums[n] , Sum(n-1) )
            假设：[2,1,1,9,2,1,5,1]
            #0: 2
            #1: Max(#0, #1) = 2
            #2: Max(#0+nums2, #1) = Max(3,2) =3 
            #3: Max(#1+nums3, #2) = Max(11,3) = 11
            #4: Max(#2+nums4, #3) = Max(5,11) = 11
            #5: Max(#3+nums5, #4) = Max(12,11)= 12
            #6: Max(#4+nums6, #5) = Max(16,12)= 16
            */
            int pprev = 0, prev = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                int t = Math.Max(prev, pprev + nums[i]);
                pprev = prev;
                prev = t;
            }
            return prev;
        }
        /// <summary>
        /// #213 打家劫舍II
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int Rob2(int[] nums)
        {
            if (nums.Length == 1)
                return nums[0];
            var arr1 = nums.Take(nums.Length - 1).ToArray();
            int s1 = Rob(arr1);
            var arr2 = nums.Skip(1).Take(nums.Length - 1).ToArray();
            int s2 = Rob(arr2);
            return Math.Max(s1, s2);
        }

        /// <summary>
        /// #740. 删除并获得点数
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int DeleteAndEarn(int[] nums)
        {
            //思路： 将数组变换，转换为每个值的所有点数。就成了打家劫舍问题
            // [2,2,3,3,5] => [0,0,4,6,0,5]
            //计算Max，得到数组长度。都初始化为0
            var max = nums.Max();
            var arr = new List<int>(max + 1);
            for (int i = 0; i <= max; i++)
                arr.Add(0);
            foreach (var num in nums)
            {
                arr[num] += num;
            }
            int pprev = 0, prev = 0;
            for (int i = 0; i < arr.Count; i++)
            {
                int t = Math.Max(pprev + arr[i], prev);
                pprev = prev;
                prev = t;
            }
            return prev;
        }
    }
    public class Day4
    {
        /// <summary>
        /// #55. 跳跃游戏
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public bool CanJump(int[] nums)
        {
            //记录能到达的最远距离。 然后在最远距离种前进，过程中不断更新 能到达的最远距离
            //         [ 2, 3, 1, 1, 4 ]
            //MaxReach   2  4  4  4  8   OK
            //         [ 3, 2, 1, 0, 4 ]
            //MaxReach   3  3  3  3  X   False
            int maxIndex = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (maxIndex < i)
                    return false;
                var cur = i + nums[i];
                maxIndex = Math.Max(cur, maxIndex);
            }
            return true;
        }

        /// <summary>
        /// #45. 跳跃游戏II
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int Jump(int[] nums)
        {
            /*
            思路:
            因为题目给出了 肯定能到末尾， 还要求最小步数。
            就从末尾分析，要想到达末尾，是从哪儿跳过来的。 然后逐渐向前递推。
            例如： [2,3,1,1,7]
            要想到达下标4， 可以从 nums[1]=3 跳1步到4， 也可以从 nums[3]=1 跳3步到4。
                要想到达下标1，可以从 nums[0]=2 跳过来
                要想到达下标3，可以从 nums[2]=1、 nums[1]=3 跳过来
                    要想到达下标2，可以从 nums[0]、nums[1] 跳过来
            因此，可以写一个树：
            4->[1,3]; 1->[0]; 3->[1,2]; 1->[0]; 2->[0,1]
            然后找最短路径即可。
            但这种思路，有大量重复计算。因此，需要演化成从前向后推导，或者用空间换时间。
            从头记录下来每个节点到达的最远距离，然后找能到达尾节点的最小节点。
              原数据：                 [2, 3, 1, 1, 7]
              从各下标能到达的最远距离：[2, 4, 3, 4, 11]
            然后，要想达到4，就看看从哪个节点能到4。 即： nums[1]=4，nums[3]=4。
            由此可以递推出结果。
            */
            int[] maxReach = new int[nums.Length];
            for (int i = 0; i < nums.Length; i++)
            {
                maxReach[i] = i + nums[i];
            }
            int steps = 0;
            int target = maxReach.Length - 1;
            while (target != 0)
            {
                steps++;
                target = FindMinIndex(maxReach, target);
            }
            return steps;
        }
        public int FindMinIndex(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] >= target)
                    return i;
            }
            return 0;
        }
    }
    public class Day5
    {
        public int MaxSubarraySumCircular(int[] nums)
        {
            var lst2 = nums.ToList();
            lst2.Add(lst2[0]);
            lst2.RemoveAt(0);
            var lst1 = nums.ToList();
            
            var max1 = MaxSubarraySum(lst1);
            var max2 = MaxSubarraySum(lst2);
            return Math.Max(max1, max2);
        }
        public int MaxSubarraySum(List<int> nums)
        {
            int sum = 0;
            int maxSum = 0;
            for (int i = 0; i < nums.Count; i++)
            {
                sum += nums[i];
                sum = Math.Max(sum, 0);
                maxSum = Math.Max(sum, maxSum);
            }
            return maxSum;
        }
    }
}
