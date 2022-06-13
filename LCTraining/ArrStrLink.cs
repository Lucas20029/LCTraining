using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCTraining
{
    class ArrStrLink
    {



        #region 数组 -- 简单

        #region 原地删除排序数组中的重复项
        public static int RemoveDuplicates(int[] nums)
        {
            int tailIndex = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[tailIndex] != nums[i])
                {
                    tailIndex++;
                    nums[tailIndex] = nums[i];
                }
            }
            tailIndex++;
            return tailIndex;
        }
        #endregion
        #region 股票最大收益
        /// <summary>
        /// 股票最大收益
        /// </summary>
        /// <param name="prices"></param>
        /// <returns></returns>
        public static int MaxProfit_Tanxin(int[] prices)
        {
            int profit = 0;
            int startIndex = 0;
            for (int i = 0; i < prices.Length - 1; i++)
            {
                if (i + 1 == prices.Length)
                {

                }
                //明天的价格 比 今天高， 持有
                if (prices[i + 1] >= prices[i])
                {
                    continue;
                }
                else
                {
                    profit += prices[i] - prices[startIndex]; //累加收益
                    startIndex = i + 1;
                }
            }
            if (startIndex < prices.Length)
            {
                profit += prices[prices.Length - 1] - prices[startIndex];
            }
            return profit;
        }

        /// <summary>
        /// 如果 明天-今天 为正数，就累加结果
        /// </summary>
        /// <param name="prices"></param>
        /// <returns></returns>
        public static int MaxProfit_SumPositiveDiff(int[] prices)
        {
            int sum = 0;
            for (int i = 0; i < prices.Length - 1; i++)
            {
                if (prices[i] < prices[i + 1])
                {
                    sum += prices[i + 1] - prices[i];
                }
            }
            return sum;
        }
        #endregion

        #region 数组旋转
        /// <summary>
        /// 直接切割
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        public static void Rotate_Split(int[] nums, int k)
        {
            k = k % nums.Length;
            var splitIndex = nums.Length - k;
            var left = nums.Take(splitIndex).ToArray();
            var right = nums.Skip(splitIndex).Take(k).ToArray();
            for (int i = 0; i < right.Length; i++)
            {
                nums[i] = right[i];
            }
            for (int i = 0; i < left.Length; i++)
            {
                nums[i + right.Length] = left[i];
            }
        }
        /// <summary>
        /// 原地旋转（这是一开始没想到的）
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        public static void Rotate_RingArray(int[] nums, int k)
        {
            k = k % nums.Length;
            Reverse(nums, 0, nums.Length - 1);
            Reverse(nums, 0, k - 1);
            Reverse(nums, k, nums.Length - 1);
        }
        private static void Reverse(int[] nums, int start, int end)
        {
            while (start < end)
            {
                int temp = nums[start];
                nums[start] = nums[end];
                nums[end] = temp;
                start++;
                end--;
            }
        }
        #endregion
        #region 存在重复元素
        /// <summary>
        /// 占用空间。
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public bool ContainsDuplicate_Hash(int[] nums)
        {
            var set = new HashSet<int>();
            foreach (var num in nums)
            {
                if (!set.Add(num))
                    return true;
            }
            return false;
        }
        //还可以先排序，再比较 i == i+1 （这是一开始没想到的）
        #endregion

        #region 只出现一次的数字
        //解法1：可以先排序，再比较 i==i+1， 但时间复杂度高
        //解法2：可以放HashSet中，如果Add==false，有重复，则删除该元素。 最后剩的就是不重复的
        //异或版本（一开始没想到）
        //a^a = 0
        //a^0 = a
        //a^b^a = a^a^b = 0^b = b
        public int SingleNumber(int[] nums)
        {
            int result = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                result = result ^ nums[i];
            }
            return result;
        }
        #endregion

        #region 两数组交集
        //一开始思路大致有，但代码没写对。处理边界条件挂了
        //排序、双指针。谁小就向后移动谁。 相等就放入临时数组
        public static int[] Intersect(int[] nums1, int[] nums2)
        {
            List<int> arr = new List<int>();
            nums1 = nums1.OrderBy(p => p).ToArray();
            nums2 = nums2.OrderBy(p => p).ToArray();
            int index1 = 0;
            int index2 = 0;
            while (index1 < nums1.Length && index2 < nums2.Length)
            {
                if (nums1[index1] < nums2[index2])
                {
                    index1++;
                }
                else if (nums1[index1] > nums2[index2])
                {
                    index2++;
                }
                else
                {
                    arr.Add(nums1[index1]);
                    index1++;
                    index2++;
                }
            }
            return arr.ToArray();
        }
        #endregion
        #region 加1
        /// <summary>
        /// 第一次的思路。 边界值处理有点问题
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static int[] PlusOne(int[] digits)
        {
            var shift = true;
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                if (shift)
                {
                    if (digits[i] + 1 >= 10)
                    {
                        digits[i] = (digits[i] + 1) % 10;
                    }
                    else
                    {
                        digits[i] = digits[i] + 1;
                        shift = false;
                        return digits;
                    }
                }
            }
            if (shift)
            {
                var temp = digits.ToList();
                temp.Insert(0, 1);
                return temp.ToArray();
            }
            return digits;
        }
        #endregion
        #region 移动0
        /// <summary>
        /// 第一次是想冒泡思路。后来想到 快慢双指针 一次遍历即可。代码一次成功
        /// </summary>
        /// <param name="nums"></param>
        public void MoveZeroes(int[] nums)
        {
            int slow = 0, fast = 0;
            while (fast < nums.Length)
            {
                if (nums[fast] != 0)
                {
                    nums[slow] = nums[fast];
                    slow++;
                }
                fast++;
            }
            for (; slow < nums.Length; slow++)
            {
                nums[slow] = 0;
            }
        }
        #endregion
        #region 两数之和
        public int[] TwoSum(int[] nums, int target)
        {
            //var set = new HashSet<int>(nums);注意：不能一次全部放入set，会去重。只能一个个放。
            var set = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                var diff = target - nums[i];
                if (set.ContainsKey(diff))
                    return new[] { i, set[diff] };
                else
                    set[nums[i]] = i;
            }
            return null;

        }
        #endregion
        #region 有效数独的判断
        //思路：把原数组值转为新数组索引。 例如，原数组是：[3,2,5,1]， 新数组：[0,1,1,1,0,1,0,0,0,0]。 即：原来数组存了哪个数，新数组对应位就是1。
        public static bool IsValidSudoku(char[][] board)
        {
            int side = board.Length;
            int[,] rowIndex = new int[9, 9];
            int[,] columnIndex = new int[9, 9];
            int[,] cellIndex = new int[9, 9];
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board.Length; j++)
                {
                    if (board[i][j] == '.')
                        continue;
                    var k = ((int)(i / 3)) * 3 + (int)(j / 3);
                    var num = board[i][j] - '0' - 1; //数独是1~9，因此，这里减1，只是为了让 临时数组 低位使用上
                    if (rowIndex[i, num] != 0 || columnIndex[j, num] != 0 || cellIndex[k, num] != 0)
                        return false;
                    else
                    {
                        rowIndex[i, num] = 1;
                        columnIndex[j, num] = 1;
                        cellIndex[k, num] = 1;
                    }
                }
            }
            return true;
        }

        #endregion

        #region 矩阵旋转90度
        //思路：一圈圈转，找规律。先拿5*5的矩阵转，分析坐标变化。为了看起来省事，下面的 n 代表5-1=4
        /* 例如：
         * 第一个元素： （0,0） -> (0,4) -> (4,4) -> (4,0) -> (0,0)
         * 第二个元素：     (0,1)->(1,4)->(4,3)->(3,0)
         *     泛化后：     (0,1)->(1,n)->(n,n-1)->(n-1,0)->(0,1)，
         *     进一步泛化： (0,j)->(j,n)->(n,n-j)->(n-j,0)->(0,j)
         * 再拿(1,2)分析，便于进一步泛化： (1,2)->(2,4)->(4,3)->(3,1)
         *                        泛化后： (i,j)->(j,n-i)->(n-i,n-j)->(n-j,i)->(i,j) 得到通项公式
         * 
         * 再分析需要旋转哪些元素：
         * 第一行： 0~3
         * 第二行： 1~2
         * 第三行： 不用转
         * 再拿一个6*6矩阵分析：
         * 第一行： 0~5
         * 第二行： 1~3
         * 第三行： 2
         * 得到通项公式： i~n-i-1。
         */
        public static void RotateMatrix90(int[][] matrix)
        {
            var n = matrix.Length-1;

            for(int i = 0; i <= n/2; i++)
            {
                for(int j = i; j<= n - i - 1; j++)
                {
                    int temp = matrix[i][j];
                    matrix[i][j] = matrix[n - j][i];
                    matrix[n - j][i] = matrix[n - i][n - j];
                    matrix[n - i][n - j] = matrix[j][n - i];
                    matrix[j][n - i] = temp;
                }
            }
        }
        #endregion

        #endregion

        #region 字符串 -- 简单

        #region 字符串反转
        public void ReverseString(char[] s)
        {
            int i = 0, j = s.Length - 1;
            while (i < j)
            {
                var temp = s[i];
                s[i] = s[j];
                s[j] = temp;
                i++;
                j--;
            }
        }
        #endregion

        #region 整数反转
        //入参拆成字符数组，从末尾 *10累加。 注意int的边界值，以及 不能进行 Abs(int.minValue)，会抛出异常。 abs(-2147483648) 会导致超出int最大值+2147483647
        public static int Reverse(int x)
        {
            long result = 0;
            var arr = x.ToString().Trim('-').ToCharArray();
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                var num = arr[i] - '0';
                result = result * 10 + num;
            }
            result = x < 0 ? -result : result;
            if (result < int.MinValue || result > int.MaxValue)
            {
                return 0;
            }
            return (int)result;
        }
        #endregion

        #region 查找第一个唯一的字符
        public static int FirstUniqueChar(string s)
        {
            int[] summary = new int[26];
            for (int i = 0; i < s.Length; i++)
            {
                var index = s[i] - 'a';
                summary[index] += 1;
            }
            for (int i = 0; i < s.Length; i++)
            {
                var index = s[i] - 'a';
                if (summary[index] == 1)
                    return i;
            }
            return -1;
        }
        #endregion

        #region 字母异位词
        //用个索引数组记录单词出现频率
        public static bool IsAnagram(string s, string t)
        {
            int[] summary = new int[26];
            for (int i = 0; i < s.Length; i++)
            {
                var index = s[i] - 'a';
                summary[index] += 1;
            }
            for (int i = 0; i < t.Length; i++)
            {
                var index = t[i] - 'a';
                summary[index] -= 1;
            }
            foreach (var num in summary)
            {
                if (num != 0)
                    return false;
            }
            return true;
        }
        #endregion

        #region 字符串公共前缀
        public string longestPrefix(string[] strs)
        {
            List<char> arr = new List<char>();
            for (int i = 0; i < strs[0].Length; i++)
            {
                for (int j = 1; j < strs.Length; j++)
                {
                    if (i >= strs[j].Length)
                        return new string(arr.ToArray());
                    if (strs[j][i] != strs[0][i])
                        return new string(arr.ToArray());
                }
                arr.Add(strs[0][i]);
            }
            return new string(arr.ToArray());
        }
        #endregion
        #region 外观数列
        public static string CountAndSay(int n)
        {
            string prev = "1";
            for (int i = 1; i < n; i++)
            {
                prev = FromPrev(prev);
            }
            return prev;
        }
        private static string FromPrev(string prev)
        {
            StringBuilder sb = new StringBuilder();
            int count = 1, num = prev[0] - '0';
            for (int i = 1; i < prev.Length; i++)
            {
                if (prev[i] == prev[i - 1])
                {
                    count++;
                }
                else
                {
                    sb.Append(count);
                    sb.Append(num);
                    count = 1;
                    num = prev[i] - '0';
                }
            }
            sb.Append(count);
            sb.Append(num);
            return sb.ToString();
        }
        #endregion

        #region AToI
        public static int MyAtoI(string s)
        {
            s = s.Trim();
            if (string.IsNullOrEmpty(s))
                return 0;

            long result = 0;
            bool negative = false;
            if (s[0] == '-')
            {
                negative = true;
                s = s.Remove(0, 1);
            }
            else if (s[0] == '+')
            {
                s = s.Remove(0, 1);
            }
            foreach(var c in s)
            {
                if (c > '9' || c < '0')
                    return (int)(negative ? -result : result);

                long temp = result * 10 + c - '0';
                var signTemp = negative ? -temp : temp;
                if (signTemp > int.MaxValue)
                    return int.MaxValue;
                else if (signTemp < int.MinValue)
                    return int.MinValue;
                else
                    result = temp;
            }
            return (int)(negative ? -result : result);
        }


        #endregion

        #region 实现 strStr()， 即C#：indexOf(subStr)
        //双重循环
        public int StrStr(string haystack, string needle)
        {
            if (string.IsNullOrEmpty(needle))
                return 0;
            bool found = false;
            for (int i = 0; i <= haystack.Length - needle.Length; i++)
            {
                found = true;
                for (int j = 0; j < needle.Length; j++)
                {
                    if (haystack[i + j] != needle[j])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                    return i;
            }
            return -1;
        }
        #endregion

        #region 是否回文
        public static bool IsPalindrome(string s)
        {
            s = s.ToLower();
            int i = 0, j = s.Length - 1;
            while (i < j)
            {
                if (!isValidCharacter(s[i]))
                {
                    i++;
                    continue;
                }
                if (!isValidCharacter(s[j]))
                {
                    j--;
                    continue;
                }
                if (s[i] == s[j])
                {
                    i++;
                    j--;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        static bool isValidCharacter(char s)
        {
            if (s >= '0' && s <= '9')
                return true;
            if (s >= 'a' && s <= 'z')
                return true;
            if (s >= 'A' && s <= 'Z')
                return true;
            return false;
        }
        #endregion

        #endregion

        #region 数组--中级
        #region 三数之和
        //先排序，外循环遍历数组， 内循环 双指针找指定和
        //去重是很费劲的。 排序后，重复的数字会相邻。 可以判断，如果相邻数字重复，就跳过
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            List<IList<int>> result = new List<IList<int>>();

            nums = nums.OrderBy(p => p).ToArray();
            for (int i = 0; i < nums.Length; i++)
            {
                if (i > 0 && nums[i] == nums[i - 1])
                    continue;
                var target = 0 - nums[i];
                FindSum(nums, i + 1, target, nums[i], ref result);
            }
            return result;
        }
        public static void FindSum(int[] nums, int startIndex, int target, int cur, ref List<IList<int>> result)
        {
            int left = startIndex, right = nums.Length - 1;
            var trimSame = false;
            while (left < right)
            {
                if (trimSame && nums[left] == nums[left - 1])
                {
                    left++;
                    continue;
                }
                if (trimSame && nums[right] == nums[right + 1])
                {
                    right--;
                    continue;
                }
                trimSame = false;
                var temp = nums[left] + nums[right];
                if (temp < target)
                    left++;
                else if (temp > target)
                    right--;
                else
                {
                    result.Add(new List<int> { cur, nums[left], nums[right] });
                    left++; right--;
                    trimSame = true;
                }

            }
        }
        #endregion

        #region 矩阵原地置零
        //使用第一行、第一列 作为标记列，先扫描 m-1,n-1 的子矩阵，把0的坐标标记到 第一行、第一列。 然后遍历第一行、第一列 置零
        //注意点：需要先检查第一行第一列是否包含0，用于最后依据此，对第一行、第一列是否置零作为判断依据。
        static int[][] SetZeros_TC1 = new int[][] { new[] { 1, 0 } };
        public static void SetZeroes(int[][] matrix)
        {
            bool hasZero1Row = false, hasZero1Column = false;
            for (int i = 0; i < matrix.Length; i++)
            {
                if (matrix[i][0] == 0)
                    hasZero1Column = true;
            }
            for (int j = 0; j < matrix[0].Length; j++)
            {
                if (matrix[0][j] == 0)
                    hasZero1Row = true;
            }

            for (int i = 1; i < matrix.Length; i++)
            {
                for (int j = 1; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == 0)
                    {
                        matrix[0][j] = 0;
                        matrix[i][0] = 0;
                    }
                }
            }
            for (int i = 1; i < matrix.Length; i++)
            {
                if (matrix[i][0] != 0)
                    continue;
                for (int j = 1; j < matrix[i].Length; j++)
                    matrix[i][j] = 0;
            }
            for (int j = 1; j < matrix[0].Length; j++)
            {
                if (matrix[0][j] != 0)
                    continue;
                for (int m = 1; m < matrix.Length; m++)
                    matrix[m][j] = 0;
            }
            if (hasZero1Row)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                    matrix[0][j] = 0;
            }
            if (hasZero1Column)
            {
                for (int m = 0; m < matrix.Length; m++)
                    matrix[m][0] = 0;
            }
        }
        #endregion

        #region 字母异位词分组
        //思路1：排序 + 记录下标
        //思路2： 字母的索引数组
        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            var result = new List<IList<string>>();
            var orderedStrs = strs.Select(p => new string(p.OrderBy(c => c).ToArray())).ToList();
            var removedSet = new HashSet<int>();
            var indexResult = new List<List<int>>();
            for (int i = 0; i < orderedStrs.Count; i++)
            {
                if (removedSet.Contains(i))
                    continue;
                indexResult.Add(new List<int> { i });
                for (int j = i + 1; j < orderedStrs.Count; j++)
                {
                    if (AreSame(orderedStrs[i], orderedStrs[j]))
                    {
                        removedSet.Add(j);
                        indexResult.Last().Add(j);
                    }
                }
            }
            foreach (var group in indexResult)
            {
                result.Add(group.Select(p => strs[p]).ToList());
            }
            return result;
        }
        public bool AreSame(string str1, string str2)
        {
            if (str1.Length != str2.Length)
                return false;
            for (int i = 0; i < str1.Length; i++)
            {
                if (str1[i] != str2[i])
                    return false;
            }
            return true;
        }
        #endregion

        #region 无重复字符的最长子串
        //Hashset 、双指针。
        //left、i 指针中间的，即为当前在分析的子串。 把它放入Hashset中，用于快速判断下个字符是否重复。
        //如果不重复，则i向后移。 如果重复，则left向前移，直到碰到跟s[i]相同的元素
        public static int LengthOfLongestSubstring(string s)
        {
            int left = 0, length = 0, maxLength = 0; ;
            var set = new HashSet<char>();
            for (int i = 0; i < s.Length; i++)
            {
                //如果重复
                if (!set.Contains(s[i]))
                {
                    length++;
                    if (maxLength < length)
                        maxLength = length;
                    set.Add(s[i]);
                    continue;
                }
                if (maxLength < length)
                    maxLength = length;


                while (left <= i)
                {
                    if (s[i] != s[left])
                    {
                        set.Remove(s[left]);
                        length--;
                        left++;
                    }
                    else
                    {
                        set.Remove(s[left]);
                        length--;
                        left++;
                        break;
                    }
                }
                length++;
                set.Add(s[i]);
            }
            return maxLength;
        }
        #endregion

        #region 最长回文
        //思路：中心扩散法。 从center，向两边逐个扩散，检查字符是否相等。相等，则继续；不相等，则结束。 每次相等的话，就判断是否更新 Max
        //需要两种情况区分对待： 奇数，如aba； 偶数，如 abba
        public static string LongestPalindrome(string s)
        {
            int centerIndex = 0;
            int maxLength = 0, maxLeft = 0, maxright = 0;
            while (centerIndex < s.Length)
            {
                int left = centerIndex, right = centerIndex, curlength = 1;
                while (left >= 0 && right < s.Length)
                {
                    if (s[left] == s[right])
                    {
                        curlength = right - left + 1;
                        if (maxLength < curlength) //记录当前最大
                        {
                            maxLeft = left;
                            maxright = right;
                            maxLength = curlength;
                        }
                        left--; right++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (centerIndex < s.Length - 1 && s[centerIndex] == s[centerIndex + 1])
                {//检查双位
                    left = centerIndex; right = centerIndex + 1;
                    while (left >= 0 && right < s.Length)
                    {
                        if (s[left] == s[right]) //还是回文
                        {
                            curlength = right - left + 1;
                            if (maxLength < curlength) //记录当前最大
                            {
                                maxLeft = left;
                                maxright = right;
                                maxLength = curlength;
                            }
                            left--; right++;
                        }
                        else //打破回文了
                        {
                            break;
                        }
                    }
                }
                centerIndex++;
            }
            var result = s.Substring(maxLeft, maxright - maxLeft + 1);
            return result;

        }
        #endregion

        #region  递增三元子序列
        //思路：只需要2个指针，一个指向 min，一个指向次小(middle)。 逐个遍历。
        //存在递增三元子序列条件： 只要一个元素 > middle 即可。 注意，middle存在的意义是：它左边肯定存在一个比它小的元素。
        //更新时，只要 元素<min， 就修改min指向它。 如果  min<元素<middle， 则更新middle为当前元素。
        //举例1： 3,5,1,0,7
        //       遍历到1， min=3, middle=5， 此时1<3， 则 min=1, middle还是=5。
        //       遍历到0， 由于 0<1， 则 min=0， middle还是=5
        //       遍历到7， 由于 7>5（middle），结束条件成立，return true
        //  --可以发现，虽然min更新到middle前面了，但我们只检查middle，所以，min并不影响。
        //举例2： 3，5，1，2，7
        //       遍历到1， min=3, middle=5， 此时1<3， 则 min=1, middle还是=5。
        //       遍历到2， 由于 1（min）<2<5（middle）， 则更新middle=2； 此时 min=1， middle=2
        //       遍历到7， 由于 7>2（middle），结束条件成立，return true
        public bool IncreasingTriplet(int[] nums)
        {
            if (nums.Length <= 2)
                return false;
            int min = int.MaxValue;
            int mid = int.MaxValue;
            for(int i = 0; i < nums.Length; i++)
            {
                if (nums[i] > mid)
                    return true;
                if (nums[i] < min)
                    min = nums[i];
                else if (nums[i] < mid && nums[i]>min)
                    mid = nums[i];
            }
            return false;
        }
        #endregion
        #endregion

        #region 链表--初级
        #region 移除倒数第N个节点
        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            if (head == null)
                return head;
            ListNode left = head, right = head;
            for (int i = 0; i < n; i++)
            {
                if (right == null)
                    break;
                right = right.next;
            }
            if (right == null)
            {
                return head.next;
            }
            while (right.next != null)
            {
                left = left.next;
                right = right.next;
            }
            left.next = left.next.next;
            return head;
        }
        #endregion
        #region 链表逆序
        //方法：画图，用两个指针来回倒腾
        public ListNode ReverseList(ListNode head)
        {
            if (head == null || head.next == null)
                return head;
            ListNode prev = null, current = head;

            while (current != null)
            {
                ListNode next = current.next;
                current.next = prev;
                prev = current;
                current = next;
            }
            return prev;
        }
        #endregion

        #region 合并两个升序链表（无环）
        //代码有点长
        public ListNode MergeTwoLists(ListNode list1, ListNode list2)
        {
            if (list1 == null && list2 == null)
                return null;
            if (list1 == null)
                return list2;
            if (list2 == null)
                return list1;

            ListNode current1 = list1;
            ListNode current2 = list2;
            ListNode head;
            if (list1.val < list2.val)
            {
                head = list1;
                current1 = current1.next;
            }
            else
            {
                head = list2;
                current2 = current2.next;
            }
            ListNode currentFinal = head;
            while (current1 != null || current2 != null)
            {
                if (current1 == null)
                {
                    currentFinal.next = current2;
                    return head;
                }
                if (current2 == null)
                {
                    currentFinal.next = current1;
                    return head;
                }
                if (current1.val < current2.val)
                {
                    currentFinal.next = current1;
                    currentFinal = current1;
                    current1 = current1.next;
                }
                else
                {
                    currentFinal.next = current2;
                    currentFinal = current2;
                    current2 = current2.next;
                }
            }
            return head;
        }
        #endregion

        #region 判断回文链表
        //思路1：借用栈。前半入栈，后半遍历
        //思路2：中间切开，前半截链表逆序
        public bool IsPalindromeList(ListNode head)
        {
            var length = GetLength(head);
            var current = head;
            int middle = length / 2;
            Stack<int> nodes = new Stack<int>();
            for (int i = 0; i < middle; i++)
            {
                nodes.Push(current.val);
                current = current.next;
            }
            if (length % 2 != 0)
                current = current.next;

            while (current != null)
            {
                if (current.val != nodes.Pop())
                    return false;
                current = current.next;
            }
            return true;
        }
        private int GetLength(ListNode node)
        {
            int length = 0;
            while (node != null)
            {
                node = node.next;
                length++;
            }
            return length;
        }
        #endregion


        #region 链表是否有环
        public bool HasCycle(ListNode head)
        {
            if (head == null || head.next == null)
                return false;
            ListNode slow = head, fast = head;
            while (fast != null)
            {
                slow = slow.next;
                fast = fast.next?.next;
                if (slow == fast)
                    return true;
            }
            return false;
        }
        #endregion
        #endregion

        #region 链表--中级
        #region 奇偶链表
        //找出奇数链表、偶数链表，然后把偶数链表挂到奇数链表末尾。
        //注意:偶数链表末尾next需要改为null
        public static ListNode OddEvenList(ListNode head)
        {
            if (head == null)
                return null;
            if (head.next == null)
                return head;
            var evenHead = head.next;
            var odd = head;
            var even = head.next;
            bool go = true;
            while (go)
            {
                if (odd.next == null || odd.next.next == null)
                    go = false;
                else
                {
                    odd.next = odd.next.next;
                    odd = odd.next;
                }
                if (even.next == null || even.next.next == null)
                    go = false;
                else
                {
                    even.next = even.next.next;
                    even = even.next;
                }
            }
            odd.next = evenHead;
            even.next = null;

            return head;
        }
        //参考别人答案，改进了终止的判断条件
        public static ListNode OddEvenList_1(ListNode head)
        {
            if (head == null)
                return null;
            ListNode odd = head;
            ListNode even = head.next;
            ListNode evenHead = head.next; ;
            while (even != null && even.next != null) //始终是偶数在后面，因此2种情况， odd指向末尾，even指向null； odd指向倒数第2个，even指向末尾，even.next=null
            {
                odd.next = odd.next.next;
                odd = odd.next;
                even.next = even.next.next;
                even = even.next;
            }
            odd.next = evenHead;
            return head;
        }
        private static ListNode BuildOddEvenList()
        {
            var node5 = new ListNode(5, null);
            var node4 = new ListNode(4, node5);
            var node3 = new ListNode(3, node4);
            var node2 = new ListNode(2, node3);
            var node1 = new ListNode(1, node2);
            return node1;
        }
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }
        #endregion
        #region 相交链表
        //参考了答案思路：先计算长度差值，让长的链表先走N（N=差值）步。然后再一起走
        public static ListNode GetIntersectionNode(ListNode headA, ListNode headB)
        {
            var lengthA = GetListNodeLength(headA);
            var lengthB = GetListNodeLength(headB);
            var diff = lengthA - lengthB;
            var ptrA = headA;
            var ptrB = headB;
            if (diff > 0)
            {
                for (int i = 0; i < diff; i++)
                    ptrA = ptrA.next;
            }
            else
            {
                diff = -diff;
                for (int i = 0; i < diff; i++)
                    ptrB = ptrB.next;
            }
            while (ptrA != ptrB && ptrB != null && ptrA != null)
            {
                ptrA = ptrA.next;
                ptrB = ptrB.next;
            }
            return ptrA;
        }
        private static int GetListNodeLength(ListNode head)
        {
            int length = 0;
            ListNode node = head;
            while (node != null)
            {
                node = node.next;
                length++;
            }
            return length;
        }
        #endregion
        #endregion


    }
}
