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
            for(int i =0;i<nums.Length-1;i++)
            {
                var maxReach = Math.Max(lastMaxReach - 1, nums[i]);
                if (maxReach + i+1 >= nums.Length)
                    return true;
                if (maxReach == 0)
                    return false;
                lastMaxReach = maxReach;
            }
            return false;
        }
    }
}
