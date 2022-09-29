using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructure
{
    public class Day5
    {
        /// <summary>
        /// 2078. 两栋颜色不同且距离最远的房子
        /// 先纸上演算一下： 中间点到中间点，肯定不是最大距离。。 只能是钉住两头，找中间的点。
        /// </summary>
        /// <param name="colors"></param>
        /// <returns></returns>
        public int MaxDistance(int[] colors)
        {
            int rightDistance = 0;
            int rightColor = colors[colors.Length - 1];
            for (int i = 1; i < colors.Length; i++)
            {
                if (colors[i] != rightColor)
                {
                    rightDistance = colors.Length - 1 - i;
                    break;
                }
            }
            int leftDistance = 0;
            for (int i = colors.Length - 1; i > 0; i--)
            {
                if (colors[i] != colors[0])
                {
                    leftDistance = i;
                    break;
                }
            }
            return Math.Max(leftDistance, rightDistance);
        }

        #region #1260. 二维网格迁移
        /// <summary>
        /// #1260. 二维网格迁移
        /// 先纸上演算一下， 把最后一列整体下移，完毕后，再对每一行 逐行右移
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public IList<IList<int>> ShiftGrid(int[][] grid, int k)
        {
            for (int i = 0; i < k; i++)
            {
                ShiftGrid(grid);
            }
            return grid.Select(p => p.ToList() as IList<int>).ToList();
        }
        public void ShiftGrid(int[][] grid)
        {
            //先把最右列逐个向下移
            int m = grid.Length;
            int n = grid[0].Length;
            int temp = grid[m - 1][n - 1];
            for (int i = m - 2; i >= 0; i--)
            {
                grid[i + 1][n - 1] = grid[i][n - 1];
            }
            grid[0][n - 1] = temp;
            //再对于每行，逐个向右移
            for (int i = 0; i < m; i++)
            {
                temp = grid[i][n - 1];
                for (int j = n - 2; j >= 0; j--)
                {
                    grid[i][j + 1] = grid[i][j];
                }
                grid[i][0] = temp;
            }
        }
        #endregion

        #region #12. 整数转罗马数字
        /// <summary>
        /// #12. 整数转罗马数字
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string IntToRoman(int num)
        {
            StringBuilder sb = new StringBuilder();
            var thousand = num / 1000;
            var thousandRoman = Build(thousand, "M", "", "");
            var hundred = (num % 1000) / 100;
            var hundredRoman = Build(hundred, "C", "D", "M");
            var ten = (num % 100) / 10;
            var tenRoman = Build(ten, "X", "L", "C");
            var one = (num % 10);
            var oneRoman = Build(one, "I", "V", "X");
            return $"{thousandRoman}{hundredRoman}{tenRoman}{oneRoman}";
        }
        public string Build(int num, string one, string five, string ten)
        {
            StringBuilder sb = new StringBuilder();
            switch (num)
            {
                case 1:
                case 2:
                case 3:
                    for (int i = 0; i < num; i++)
                    {
                        sb.Append(one);
                    }
                    break;
                case 4:
                    sb.Append($"{one}{five}"); break;
                case 5:
                    sb.Append(five); break;
                case 6:
                case 7:
                case 8:
                    sb.Append(five);
                    for (int i = 0; i < num - 5; i++)
                    {
                        sb.Append(one);
                    }
                    break;
                case 9:
                    sb.Append($"{one}{ten}"); break;
            }
            return sb.ToString();
        }
        #endregion


        //#4.寻找两个正序数组的中位数 TODO:没做完
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            var k = (int)((nums1.Length + nums2.Length) / 2);
            int left1 = -1, left2 = -1;
            int half = (int)(k / 2) + 1;
            int middle = 0;
            while (half<=k)
            {
                int num1 = int.MaxValue, take1 = 0;
                int num2 = int.MaxValue, take2 = 0;
                if (left1 + half < nums1.Length)
                {
                    num1 = nums1[left1 + half];
                    take1 = half;
                }
                else if(left1< nums1.Length)
                {
                    num1 = nums1[nums1.Length - 1];
                    take1 = nums1.Length - left1;
                }
                if (left2 + half < nums2.Length)
                {
                    num2 = nums2[left2 + half];
                    take2 = half;
                }
                else if (left2 < nums2.Length)
                {
                    num2 = nums2[nums2.Length - 1];
                    take2 = nums2.Length - left2;
                }

                if (num1 < num2)
                {
                    left1 += take1;
                    left2 += half - take1;
                    middle = take1 == half ? nums1[left1] : nums2[left2];
                }
                else
                {
                    left2 += take2;
                    left1 += half - take1;
                    middle = take2 == half ? nums2[left2] : nums1[left1];
                }
                half = (int)(half / 2)+1;
            }

            if ((nums1.Length + nums2.Length) % 2 == 0)
            {
                var num1 = left1 + 1 > nums1.Length ? int.MaxValue : nums1[left1 + 1];
                var num2 = left2 + 1 > nums2.Length ? int.MaxValue : nums2[left2 + 1];
                var next = Math.Min(num1, num2);
                return (middle + next) / 2;
            }
            return middle;
        }

        /// <summary>
        /// #6. Z 字形变换
        /// </summary>
        /// <param name="s"></param>
        /// <param name="numRows"></param>
        /// <returns></returns>
        public string Convert(string s, int numRows)
        {
            if (s.Length == 1 || numRows == 1)
                return s;
            int length = s.Length;
            int loops = ((int)(length / (2 * numRows - 2))) + 1;
            var columns = (numRows - 1) * loops;
            char[,] matrix = new char[numRows, columns];
            for (int i = 0; i < numRows; i++)
                for (int j = 0; j < columns; j++)
                    matrix[i, j] = '-';
            bool downing = true;
            int x = 0, y = 0;
            for (int i = 0; i < s.Length; i++)
            {
                matrix[x, y] = s[i];
                if (downing)
                {
                    if (x == numRows - 1)
                    {
                        downing = false;
                        y++; x--;
                    }
                    else
                    {
                        x++;
                    }
                }
                else
                {
                    if (x == 0)
                    {
                        downing = true;
                        x++;
                    }
                    else
                    {
                        y++; x--;
                    }
                }
            }
            List<char> text = new List<char>(s.Length);
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (matrix[i, j] != '-')
                    {
                        text.Add(matrix[i, j]);
                    }
                }
            }
            return new string(text.ToArray());
        }

        /// <summary>
        /// #11. 盛最多水的容器
        /// 双指针。从两头开始，向中间挤。 谁小移动谁（用未来可能的高度换宽度）
        /// 例如：[1,8,6,2,5,4,8,3,7]
        /// 一开始定位在 1,7， 那么容积是 min(1,7)*8 = 8
        /// 此时，应该移动小的。
        /// 因为如果移动7，那么宽度肯定是缩小了，而高度=min(1,X)肯定只会比1小，因此得不到最大容积。
        /// 所以只能移动小的。
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public int MaxArea(int[] height)
        {
            int left = 0, right = height.Length - 1;
            int max = 0;
            while (left < right)
            {
                var volumn = Math.Min(height[left], height[right]) * (right - left);
                max = Math.Max(max, volumn);
                if (height[left] < height[right])
                    left++;
                else
                    right--;
            }
            return max;
        }


        #region #31. 下一个排列
        /// <summary>
        /// 31. 下一个排列
        ///例如，以下数列就是逐渐下一个排列：
        ///12345
        ///12345
        ///12435
        ///12453
        ///12534
        ///12543
        ///13245
        ///13254
        ///13452
        ///13524
        ///13542
        ///14235
        ///14253....
        ///....
        ///15432
        ///21345
        ///21354
        ///....
        ///
        ///1.从尾向前遍历，需要 逐渐递增。 一旦不递增，就是要修改该点
        ///2.从末尾找第一个比该点大的数，拿过来（放到该点位置）
        ///3.剩下的数字递增
        /// </summary>
        /// <param name="nums"></param>
        public void NextPermutation(int[] nums)
        {
            
            var pos = nums.Length - 1;
            bool max = true;
            for (int i = nums.Length - 2; i >= 0; i--)
            {
                if (nums[i] < nums[pos])
                {
                    pos = i; max = false; break;
                }
                pos--;
            }
            if (max)
            {
                PartialyReverse(nums, 0, nums.Length - 1);
                return;
            }
            for (int i = nums.Length - 1; i >= 0; i--)
            {
                if (nums[i] > nums[pos])
                {
                    Swap(nums, i, pos);
                    break;
                }
            }
            PartialyReverse(nums, pos + 1, nums.Length - 1);
        }
        public void PartialyReverse(int[] nums, int start, int end)
        {
            while (start < end)
            {
                Swap(nums, start, end);
                start++; end--;
            }
        }
        public void Swap(int[] nums, int a, int b)
        {
            int t = nums[a];
            nums[a] = nums[b];
            nums[b] = t;
        }
        #endregion



        #region 43 字符串相乘、  415 字符串相加

        public void TestAddString_Multiply()
        {
            var res1 = AddString("96", "109") == "205";
            var res2 = AddString("956", "79") == "1035";
            var res3 = MultiplyDigit("395", 3) == "1185";
            var res4 = Multiply("359", "1680") == "603120";
        }
        public string Multiply(string num1, string num2)
        {
            if (num1 == "0" || num2 == "0")
                return "0";
            List<string> midResults = new List<string>();
            for (int i = 0; i < num2.Length; i++)
            {
                var factor2 = num2[num2.Length - 1 - i] - '0';
                var midRes = MultiplyDigit(num1, factor2);
                midRes = midRes + new string('0', i);
                midResults.Add(midRes);
            }
            string result = "0";
            foreach (var midRes in midResults)
            {
                result = AddString(result, midRes);
            }
            return result;
        }
        public string MultiplyDigit(string num1, int factor)
        {
            StringBuilder builder = new StringBuilder();
            int carry = 0;
            for (int i = 0; i < num1.Length; i++)
            {
                var factor1 = num1[num1.Length - 1 - i] - '0';
                var square = factor1 * factor + carry;
                var sum = square % 10;
                carry = square / 10;
                builder.Insert(0, (char)(sum + '0'));
            }
            if (carry > 0)
                builder.Insert(0, carry);
            return builder.ToString();
        }

        //415. 字符串相加
        private string AddString(string num1, string num2)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var maxLength = Math.Max(num1.Length, num2.Length);
            bool carry = false;
            for (int i = 0; i < maxLength; i++)
            {
                var index1 = num1.Length - 1 - i;
                var index2 = num2.Length - 1 - i;

                var digit1 = index1 >= 0 ? num1[index1] : '0';
                var digit2 = index2 >= 0 ? num2[index2] : '0';

                int sum = (digit1 - '0') + (digit2 - '0') + (carry ? 1 : 0);
                carry = sum >= 10 ? true : false;
                char cur = sum >= 10 ? (char)(sum - 10 + '0') : (char)(sum + '0');
                stringBuilder.Insert(0, cur);
            }
            if (carry)
                stringBuilder.Insert(0, '1');
            return stringBuilder.ToString();
        }
        #endregion
    }
}
