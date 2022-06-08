using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCTraining
{
    public class DP
    {
        public static DP Instance = new DP();
        /*先举例子：
         * 总共1级台阶，那么只能 0->1， 记为 (0,1)
         * 总共2级台阶，那么可以 (0,1,2), (0,2)
         * 总共3级台阶，那么 如果 先跳到1阶，然后每条路径有2种办法到3阶：
         *                      (0,1) -> (0,1,2,3) 
         *                            -> (0,1,3)
         *                   如果 先跳到2阶，每条路径只有1种办法跳到3阶：
         *                      (0,1,2) -> (0,1,2,3)
         *                      (0,2)   -> (0,2,3)
         *              然后把所有路径汇总一下：
         *                      (0,1,2,3)
         *                      (0,1,3)
         *                      (0,1,2,3)
         *                      (0,2,3)
         *              可以发现有重复的。
         *              分析重复的原因是：从1阶跳3阶，中间经过2阶的话，就跟2阶的重复了。
         *              因此，计算的时候，从1阶跳3阶不能经过2阶。也就只能在1阶的基础上，直接跳到3阶。因此，有如下3种跳法：
         *                      (0,1,2,3)
         *                      (0,1,3)
         *                      (0,2,3)
         *                      
         * 总共4级台阶， 从2阶跳到4阶： (0,1,2,4)
         *                              (0,2,4)
         *               从3阶跳到4阶： (0,1,2,3,4)
         *                              (0,1,3,4)
         *                              (0,2,3,4)
         *               共5种跳法
         *  因此，总结规律， f(n) 为跳到n阶的路径数。 那么 f(n)=f(n-1)+f(n-2)
         */
        //思路：每次只能爬1、2级台阶。
        //因此， 要爬到第n个台阶，需要在第n-1个台阶上爬1，或在第n-2个台阶上爬 1+1 或 2。 
        //但由于 n-2上爬1+1，已经被n-1爬过了，因此不能重复算。 因此： f(n)=f(n-1)+f(n-2)
        public static int ClimbStairs(int n)
        {
            if (n == 1) return 1;
            if (n == 2) return 2;
            return ClimbStairs(n - 1) + ClimbStairs(n - 2);
        }//这种解法会超时，因此需要优化
            
        //思路：暂存
        public static int ClimbStairs2(int n)
            {
            int[] arr = new int[n + 2];
            arr[1] = 1; arr[2] = 2;
            return ClimbStairs2_inner(n, arr);
            }
        public static int ClimbStairs2_inner(int n, int[] ways)
        {
            if (ways[n] != 0)
                return ways[n];
            var res = ClimbStairs2_inner(n - 1, ways) + ways[n - 2];
            ways[n] = res;
            return res;
        }


        //思路：双指针
        public int MaxProfit(int[] prices)
        {
            int buypoint = 0;
            int profit = 0;
            int maxProfit = 0;
            for (int i = 1; i < prices.Length; i++)
            {
                //如果利润为增，则累加
                //如果利润为减，
                //             但 p[i] > p[buypoint]，则 但整体还是大于0，则利润不累加
                //             否则，p[i]为更低点，更新buypoint
                if (prices[i] > prices[i - 1])
                {
                    profit = prices[i] - prices[buypoint];
                    if (maxProfit < profit)
                        maxProfit = profit;
                    continue;
                }
                else
                {
                    if (prices[i] < prices[buypoint])
                        buypoint = i;
                }
            }
            return maxProfit;
        }

        /*动态规划，一定不要想太多。因为它是动态地规划，也就是说，仅看当下。看当下满足目标，需要依赖前面几步。
         * 
         例如这题，我们记录下来偷到每间房间手里的最大现金 TotalMoney，那么记录到最后，就是所有全局的最大值。 （或者说，就是分析末尾元素，从数组尾部向前分析）
        并且可知TotalMoney一定是单调递增的，不可能递减。
         那么，到该房间最大，只有 偷/不偷 两个选项。 
        偷，则只能是 TotalMoney(i-2)+Num(i)。 不能是 TotalMoney(i-3)，因为TotalMoney单调递增的特点
        不偷，也只能是 TotalMoney(i-1)，也不能再少了。
        那么，就看这两个选项，哪个大，就用哪个。
        至于 TotalMoney(i-2)是怎么算，不是这一步考虑的事
         */
        public int Rob(int[] nums)
        {
            int[] totalMoney = new int[nums.Length];
            for(int i = 0; i < nums.Length; i++)
            {
                if (i == 0)
        {
                    totalMoney[0] = nums[0];
                    continue;
                }
                if (i == 1)
            {
                    totalMoney[1] = Math.Max(nums[0], nums[1]);
                    continue;
                }

                var rob = totalMoney[i - 2] + nums[i];
                var notRob = totalMoney[i - 1];
                totalMoney[i] = Math.Max(rob, notRob);
            }
            return totalMoney[nums.Length - 1];
        }


        //---------------------------------------
        //加上当前元素，只要和还是大于0，就保留； 否则中断。
        public int MaxSubArray(int[] nums)
        {

            int[] sum = new int[nums.Length];
            sum[0] = nums[0];
            int max = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                sum[i] = Math.Max(sum[i - 1], 0) + nums[i];
                max = Math.Max(max, sum[i]);
            }
            return max;
        }
        public int MaxSubArray1(int[] nums)
        {
            int maxSum = int.MinValue;
            int sum = int.MinValue;

            for (int i = 0; i < nums.Length; i++)
            {
                int temp = (sum < 0 ? 0 : sum) + nums[i];
                if (temp > 0)
                {
                    sum = temp;
                }
                else
                {
                    sum = nums[i];
                }

                if (maxSum < sum)
                {
                    maxSum = sum;
                }
            }
            return maxSum;
        }

        //思路：寻找当前节点 跟它前一个节点之间的关系
        //前一个节点最远能跳到N处，当前节点值为M， 那么当前节点最远能跳到 Max(N-1,M) 处
        public bool CanJump(int[] nums)
        {
            if (nums.Length <= 1)
                return true;
            int lastMaxReach = 0;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                var maxReach = Math.Max(lastMaxReach - 1, nums[i]);
                if (maxReach + i + 1 >= nums.Length)
                    return true;
                if (maxReach == 0)
                    return false;
                lastMaxReach = maxReach;
            }
            return false;
        }
    }
}
