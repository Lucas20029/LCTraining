using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class SortAndSearch
    {
        #region 初级
        #region 合并两个有序数组
        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            int index = nums1.Length - 1;
            int index1 = m - 1;
            int index2 = n - 1;

            while (index >= 0)
            {
                if (index1 < 0 && index2 < 0)
                    break;
                else if (index1 < 0 && index2 >= 0)
                {
                    nums1[index] = nums2[index2];
                    index2--;
                }
                else if (index1 >= 0 && index2 < 0)
                {
                    nums1[index] = nums1[index1];
                    index1--;
                }
                else
                {
                    if (nums1[index1] >= nums2[index2])
                    {
                        nums1[index] = nums1[index1];
                        index1--;
                    }
                    else
                    {
                        nums1[index] = nums2[index2];
                        index2--;
                    }
                }
                index--;
            }
        }

        public void Merge1(int[] nums1, int m, int[] nums2, int n)
        {
            int index1 = m - 1;
            int index2 = n - 1;
            int index = nums1.Length - 1;
            while (index1 >= 0 && index2 >= 0)
            {
                if (nums1[index1] > nums2[index2])
                {
                    nums1[index] = nums1[index1];
                    index1--;
                }
                else
                {
                    nums1[index] = nums2[index2];
                    index2--;
                }   
                index--;
            }
            while (index2 >= 0)
            {
                nums1[index] = nums2[index2];
                index2--;
                index--;
            }
        }
        #endregion

        #region 第一个错误的版本
        public static int FirstBadVersion(int n)
        {
            //long start = 1, end = n;
            int start = 1, end = n;
            while (start < end)
            {
                //注意：这里计算中点的方法： start+差值的一半，能避免 2个int相加越界。
                //代替的方法，是把 start、end都定义为long，它俩相加 就不会越界了。
                int mid =start +(end-start)/2;
                //long temp = start + end;
                //int mid = (int)(temp / 2);
                if (IsBadVersion(mid))
                {
                    end = mid;
                }
                else
                {
                    start = mid + 1;
                }
                Console.WriteLine(mid);
            }
            return (int)start;
        }
        static bool IsBadVersion(int version)
        {
            if (version >= 1702766719)
                return true;
            else
                return false;
        }
        #endregion

        #endregion

        #region 中级
        #region 颜色分类
        //思路：从头到尾遍历，碰到0，放到最前面，碰到2放到最后面。
        public void SortColors(int[] nums)
        {
            //0的右边界
            int left = 0;
            //2的左边界
            int right = nums.Length - 1;
            //指向当前数字
            int index = 0;
            while (index <= right)
            {
                if (nums[index] == 0)
                {
                    //如果是0，就往前面移
                    Swap(nums, left++, index++);
                }
                else if (nums[index] == 1)
                {
                    index++;
                }
                else if (nums[index] == 2)
                {
                    //如果是2就往后面移，right铁定要-1，因为确定移过去了一个2。
                    //对于移到前面的数，还要继续分析，因此index还不能+1
                    Swap(nums, right--, index);
                }
            }
        }
        #endregion

        #region  前K个高频元素
        public int[] TopKFrequent(int[] nums, int k)
        {
            var dic = nums.GroupBy(n => n).ToDictionary(n => n.Key, n => n.Count());
            return dic.OrderByDescending(p => p.Value).Take(k).Select(p => p.Key).ToArray();
        }
        #endregion

        #region 寻找峰值
        //思路：寻找下降点。
        //还有个二分查找的思路，没理解为啥时间复杂的为 log n
        public int FindPeakElement(int[] nums)
        {
            if (!nums.Any())
                return 0;
            for(int i = 0; i < nums.Length-1; i++)
            {
                if (nums[i] > nums[i + 1])
                    return i;
            }
            return nums.Length - 1;
        }
        #endregion


        #region 在排序数组中查第一个和最后一个
        public int[] SearchRange(int[] nums, int target)
        {
            if (!nums.Any())
                return new[] { -1, -1 };
            int left = 0, right = nums.Length - 1;
            if (nums[left] > target || nums[right]<target)
                return new[] { -1, -1 };
            while (left <= right)
            {
                if (nums[left] < target)
                    left++;
                if (nums[right] > target)
                    right--;
                if (nums[left] == target && nums[right] == target)
                    return new[] { left, right };
            }
            return new[] { -1, -1 };
        }

        #endregion

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
                if ((interval[0] >= result.Last()[0]) && (interval[0] <= result.Last()[1]))
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
            while (left <= right)
            {
                var mid = left + (right - left) / 2;
                if (nums[mid] == target)
                    return mid;
                if (nums[left] == target)
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
                else if (nums[mid] > nums[right])
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


        #region 搜索二维矩阵
        //从给的矩阵中高效搜索元素。矩阵特点：
        //  - 每行的元素从左到右升序排列。
        //  - 每列的元素从上到下升序排列。
        //思路：从 右上角、左下角观察，这种矩阵就是天然的 二叉搜索树。（左子节点<Root<右子节点）
        public static bool SearchMatrix(int[][] matrix, int target)
        {
            int i = 0, j = matrix[0].Length - 1;

            while (i < matrix.Length && j >= 0)
            {
                if (matrix[i][j] > target)
                    j--;
                else if (matrix[i][j] < target)
                    i++;
                else
                    return true;
            }
            return false;
        }
        #endregion

        #endregion

        private void Swap(int[] nums, int a, int b)
        {
            int temp = nums[a];
            nums[a] = nums[b];
            nums[b] = temp;
        }
    }
}
