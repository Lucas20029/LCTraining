using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCTraining
{
    class Program
    {
        static void Main(string[] args)
        {
            var res = SortAndSearch.Search(new[] { 4, 5, 6, 7, 0, 1, 2 }, 5);
        }
    }

    
    public class SortAndSearch
    {
        #region 合并区间
        public int[][] Merge(int[][] intervals)
        {
            var sortedIntervals = intervals.OrderBy(p => p[0]).ToArray();
            List<int[]> result = new List<int[]>();
            foreach (var interval in sortedIntervals)
            {
                if (!result.Any())
                {
                    result.Add(interval);
                    continue;
                }
                if((interval[0]>= result.Last()[0]) && (interval[0] <= result.Last()[1]))
                {
                    if (result.Last()[1] < interval[1])
                        result.Last()[1] = interval[1];
                }
                else
                {
                    result.Add(interval);
                }
            }
            return result.ToArray();
        }
        #endregion

        #region 在排序数组旋转后的数组中，搜索目标
        //思路：二分的思想就是：根据有序元素段判断元素位置以不断收缩边界选择搜索空间。 这里虽然不是全局有序，但可以分成几个有序的区段。
        //然后这几个区段都有明显的特点。
        //例如：8 9 1 2 3 4 5， mid=2，必然 8>2
        //整个数组可以分为3个区段，  left~断点：8 9 ， 断点~mid：1 2， mid~right：3 4 5
        public static int Search(int[] nums, int target)
        {
            int left = 0, right = nums.Length - 1;
            while (left<=right)
            {
                var mid = left + (right - left) / 2;
                if (nums[mid] == target)
                    return mid;
                if(nums[left]==target)
                    return left;
                if (nums[right] == target)
                    return right;
                //如果旋转点在左边，那么 肯定 nums[mid]<nums[left]。  
                if (nums[mid] < nums[left])
                {
                    //Target 属于 left~断点
                    if (target > nums[left])
                        right = mid - 1;
                    //Target 属于 断点~mid
                    else if (target < nums[mid])
                        left = left + 1;
                    //Target 属于 mid~right  （正常区段）
                    else
                        left = mid + 1;
                }
                //如果旋转点在右边， 那么，肯定 nums[mid]>nums[right]
                else if (nums[mid]> nums[right])
                {
                    if (target < nums[right])
                        left = mid + 1;
                    else if (target > nums[mid])
                        right = right - 1;
                    else
                        right = mid - 1;
                }
                else //是顺子的情况， 数组是： 0 1 2 3 4 5 6 7，断点任意。用正常二分法即可
                {
                    if (target < nums[mid])
                        right = mid - 1;
                    else
                        left = mid + 1;
                }
            }
            return -1;
        }
        #endregion

    }
}
