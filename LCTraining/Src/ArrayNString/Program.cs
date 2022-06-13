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
            var testMatrix = Matrix.Instance.BuildMatrix();
            var findDiagonalOrder = Matrix.Instance.FindDiagonalOrder(testMatrix);
            var pivotIndexRes1 = ArrayIntroduction.Instance.PivotIndex(new[] { 2, 1, -1 });
        }
    }

    public class ArrayIntroduction
    {
        public static ArrayIntroduction Instance = new ArrayIntroduction();

        #region 寻找数组中心索引
        public int PivotIndex(int[] nums)
        {
            int sum = nums.Sum();
            int left = 0;
            int right = sum;
            for(int i = 0; i < nums.Length; i++)
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
            while (left<right)
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
                if (i % 2 == 0 && i<height) //坐标和为偶数
                {
                    for(int j =i; IsInMatrix(j,i-j,width,height); j--)
                    {
                        arr.Add(mat[j][i-j]);
                    }
                }
                else if(i%2==0 && i >= height)
                {
                    for (int j = height; IsInMatrix(j, i - j, width, height); j--)
                    {
                        arr.Add(mat[j][i - j]);
                    }
                }
                else if( i%2!=0 && i < width) //坐标为奇数
                {
                    for(int j = 0; IsInMatrix(j,i-j,width,height); j++)
                    {
                        arr.Add(mat[j][i - j]);
                    }
                }
                else
                {
                    for (int j = width; IsInMatrix(i-j, j, width, height); j--)
                    {
                        arr.Add(mat[i-j][j]);
                    }
                }
            }
            return arr.ToArray();
        }
        private bool IsInMatrix(int i ,int j, int w,int h)
        {
            return i >= 0 && i <= h && j >= 0 && j <= w;
        }
        #endregion
    }
}
