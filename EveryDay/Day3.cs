using System;

namespace DataStructure
{
    public class Day3
    {
        /// <summary>
        /// #1855. 下标对中的最大距离
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="nums2"></param>
        /// <returns></returns>
        public int MaxDistance(int[] nums1, int[] nums2)
        {
            //思路：双指针。 不要回溯，否则会超时。
            // 以下面2数组为例：
            // [9, 8, 7, 6, 5, 4]
            // [20,11,10,9, 6, 1]
            //初始： i=0,j=0，
            //然后： j递增1，直到nums1[0]<=nums2[j]。 则， 直到j=4, nums2[4]=6， 找到了i=0的最大距离： 4-1-0=3
            //然后，注意，j不需要回溯，钉在原地即可。
            //因为，对于i=1来说，要想max更大，必须要从j=4开始查，否则即使满足条件也没什么意思，肯定比4小
            
            //这样可以双指针一直往前遍历，时间复杂度为：m+n

            int max = 0;
            int j = 0;
            for (int i = 0; i < nums1.Length; i++)
            {
                while (j < nums2.Length)
                {
                    if (nums1[i] <= nums2[j])
                    {
                        j++;
                    }
                    else
                    {
                        break;
                    }
                }
                max = Math.Max((j - i - 1), max);
            }
            return max;
        }


        /// <summary>
        /// #1266. 访问所有点的最小时间
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>

        public int MinTimeToVisitAllPoints(int[][] points)
        {
            int steps = 0;
            int fromIndex = 0;
            for (int i = 1; i < points.Length; i++)
            {
                steps += FromTo(points[fromIndex], points[i]);
                fromIndex++;
            }
            return steps;
        }
        public int FromTo(int[] from, int[] to)
        {
            int steps = 0;
            int x = from[0];
            int y = from[1];
            while (x != to[0] || y != to[1])
            {
                if (x < to[0])
                    x++;
                else if (x > to[0])
                    x--;

                if (y < to[1])
                    y++;
                else if (y > to[1])
                    y--;

                steps++;
            }
            return steps;
        }
    }
}
