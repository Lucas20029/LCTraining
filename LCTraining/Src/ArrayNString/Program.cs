using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayNString
{
    class Program
    {
        static void Main(string[] args)
        {
            var row=  Summary.Instance.GetRow(3);
            var testMatrix = Matrix.Instance.BuildMatrix();
            var findDiagonalOrder = Matrix.Instance.FindDiagonalOrder(testMatrix);
            var pivotIndexRes1 = ArrayIntroduction.Instance.PivotIndex(new[] { 2, 1, -1 });
        }
    }

    /// <summary>
    /// 数据简介部分
    /// </summary>
    public class ArrayIntroduction
    {
        public static ArrayIntroduction Instance = new ArrayIntroduction();

        #region 寻找数组中心索引
        public int PivotIndex(int[] nums)
        {
            int sum = nums.Sum();
            int left = 0;
            int right = sum;
            for (int i = 0; i < nums.Length; i++)
            {

                left += i > 0 ? nums[i - 1] : 0;
                right -= nums[i];
                if (left == right)
                    return i;
            }
            return -1;
        }
        #endregion
        #region 搜索插入位置
        //思路：二分查找。 手画一下，确定查找结束的条件
        public int SearchInsert(int[] nums, int target)
        {
            if (target <= nums[0])
                return 0;
            if (target >= nums[nums.Length - 1])
                return nums.Length;

            int left = 0;
            int right = nums.Length;
            int mid = left + (right - left) / 2;
            while (left < right)
            {
                if (nums[mid] >= target && nums[mid - 1] < target)
                    break;

                if (nums[mid] < target)
                {
                    left = mid;
                }
                else
                {
                    right = mid;
                }
                mid = left + (right - left) / 2;
            }
            return mid;
        }
        #endregion
    }

    /// <summary>
    /// 二维数组简介
    /// </summary>
    public class Matrix
    {
        public static Matrix Instance = new Matrix();

        public int[][] BuildMatrix()
        {
            return new[]
            {
                new[] { 1, 2, 3,4,5,6,7 },
                new[] { 8, 9, 10,11,12,13,14 },
                new[] { 15,16,17,18,19,20,21 },
                new[] { 22,23,24,25,26,27,28 },
            };
        }

        #region 对角线遍历
        //思路：
        //1. 每条正45度对角线上，  每个点的 x坐标+y坐标 ，都相等
        //2. 奇数向下走， 偶数向上走
        //3. 碰到边界，改变运动规律
        //4. 注意：入参矩阵不是正方形
        public int[] FindDiagonalOrder(int[][] mat)
        {
            List<int> arr = new List<int>();
            int width = mat[0].Length - 1;
            int height = mat.Length - 1;
            int maxSum = width + height;
            for (int i = 0; i <= maxSum; i++)
            {
                if (i % 2 == 0 && i < height) //坐标和为偶数
                {
                    for (int j = i; IsInMatrix(j, i - j, width, height); j--)
                    {
                        arr.Add(mat[j][i - j]);
                    }
                }
                else if (i % 2 == 0 && i >= height)
                {
                    for (int j = height; IsInMatrix(j, i - j, width, height); j--)
                    {
                        arr.Add(mat[j][i - j]);
                    }
                }
                else if (i % 2 != 0 && i < width) //坐标为奇数
                {
                    for (int j = 0; IsInMatrix(j, i - j, width, height); j++)
                    {
                        arr.Add(mat[j][i - j]);
                    }
                }
                else
                {
                    for (int j = width; IsInMatrix(i - j, j, width, height); j--)
                    {
                        arr.Add(mat[i - j][j]);
                    }
                }
            }
            return arr.ToArray();
        }
        private bool IsInMatrix(int i, int j, int w, int h)
        {
            return i >= 0 && i <= h && j >= 0 && j <= w;
        }
        #endregion
    }

    public class StringIntruduction
    {
        /// <summary>
        /// 翻转字符串里的单词
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string ReverseWords(string s)
        {
            return string.Join(" ", s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Reverse());
        }
    }
    public class DualPointer
    {
        public int ArrayPairSum(int[] nums)
        {
            var orderedNums = nums.OrderBy(p => p);
            bool even = false;
            int sum = 0;
            foreach (var num in orderedNums)
            {
                if (even)
                    sum += num;
                even = !even;
            }
            return sum;
        }
        /// <summary>
        /// 两数之和|| - 输入有序数组
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int[] TwoSum(int[] numbers, int target)
        {
            int leftIndex = 0, rightIndex = numbers.Length - 1;
            while (leftIndex < rightIndex)
            {
                var sum = numbers[leftIndex] + numbers[rightIndex];
                if (sum > target)
                {
                    rightIndex--;
                }
                else if (sum < target)
                {
                    leftIndex++;
                }
                else
                {
                    return new[] { leftIndex + 1, rightIndex + 1 };
                }
            }
            return new[] { -1, -1 };
        }
        /// <summary>
        /// 移除元素  （不使用额外空间）
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public int RemoveElement(int[] nums, int val)
        {
            int slow = 0;
            for (int fast = 0; fast < nums.Length; fast++)
            {
                if (nums[fast] != val)
                {
                    nums[slow] = nums[fast];
                    slow++;
                }
            }
            return slow;
        }
        /// <summary>
        /// 最大连续1的个数
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int FindMaxConsecutiveOnes(int[] nums)
        {
            int target = 1;
            int slow = 0, fast = 0, maxLength = 0;
            while (fast < nums.Length)
            {
                if (nums[fast] == target)
                {
                    var count = fast - slow + 1;
                    if (maxLength < count)
                        maxLength = count;
                    fast++;
                }
                else
                {
                    fast++;
                    slow = fast;
                }
            }
            return maxLength;
        }
        /// <summary>
        /// 长度最小的子数组
        /// </summary>
        /// <param name="target"></param>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int MinSubArrayLen(int target, int[] nums)
        {
            int slow = 0, fast = 0, sum = nums[0], minLength = int.MaxValue;
            while (true)
            {
                if (sum >= target)
                {
                    //
                    minLength = Math.Min(fast - slow + 1, minLength);
                    sum -= nums[slow];
                    slow++;
                    if (slow > fast)
                    {
                        fast++;
                        if (fast >= nums.Length)
                            return minLength == int.MaxValue ? 0 : minLength;
                    }

                }
                else
                {
                    //
                    fast++;
                    if (fast >= nums.Length)
                    {
                        return minLength == int.MaxValue ? 0 : minLength;
                    }
                    sum += nums[fast];
                }
            }
        }
    }
    public class Summary
    {
        public static Summary Instance = new Summary();

        /// <summary>
        /// 杨辉三角||
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public IList<int> GetRow(int rowIndex)
        {
            if (rowIndex == 0)
                return new List<int> { 1 };
            if (rowIndex == 1)
                return new List<int> { 1, 1 };

            List<int> result = new List<int>(rowIndex+1);
            for (int i = 0; i < rowIndex; i++)
                result.Add(0);
            result[0] = result[1] = 1;
            for(int i = 2; i <= rowIndex; i++)
            {
                for(int j = i; j > 0; j--)
                {
                    result[j] = result[j] + result[j-1];
                }
            }
            return result;
        }

        /// <summary>
        /// 反转字符串中的单词 III
        /// 输入：s = "Let's take LeetCode contest"
        /// 输出："s'teL ekat edoCteeL tsetnoc"
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>

        public string ReverseWords(string s)
        {
            var words = s.Split(' ');
            var rWords = new List<string>();
            foreach (var word in words)
            {
                var rs = Reverse(word);
                rWords.Add(rs);
            }
            return string.Join(" ", rWords);
        }

        public string Reverse(string s)
        {
            if (s.Length == 0)
                return "";
            var arr = s.ToCharArray();
            int left = 0, right = s.Length - 1;
            while (left < right)
            {
                var t = arr[left];
                arr[left] = arr[right];
                arr[right] = t;
                left++;
                right--;
            }
            return new string(arr);
        }

        /// <summary>
        /// 寻找旋转排序数组中的最小值
        /// 思路：通SearchAndSort.Search。 二分查找
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int FindMin(int[] nums)
        {
            /*
            如果 nums[0]<nums[len-1]，说明有序，返回[0]
            [3,4,5,6,7,8,0,1,2]
            7  left: 3<7   right 7>2 （二分查right,如果left>7，则二分查left）
            0  
            */
            int left = 0, right = nums.Length - 1;
            if (nums[left] <= nums[right])
                return nums[0];
            if (nums.Length > 1 && nums[right] < nums[right - 1])
                return nums[right];

            int mid = left + (right - left) / 2;
            while (!isMin(nums, mid))
            {
                mid = left + (right - left) / 2;
                int midVal = nums[mid];
                if (nums[right] < midVal)
                {
                    left = mid;
                }
                else
                {
                    right = mid;
                }
            }
            return nums[mid];
        }
        bool isMin(int[] nums, int index)
        {
            return nums[index] < nums[index - 1] && nums[index] < nums[index + 1];
        }

    }
}
