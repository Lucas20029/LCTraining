using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCTraining
{
    public class BackTrack
    {
        public static BackTrack Instance = new BackTrack();


        #region 括号生成
        //思路：逐个字符地向Str中追加。人脑模拟一次：
        /*
         * ( 
         *   (( 
         *      ((( --> ((()))
         *      (()
         *          (()(  --> (()())
         *          (())
         *               (())( --> (())()
         *   ()
         *      ()(
         *          ()((  --> ()(())
         *          ()()
         *               ()()()
         */
         //总结规律：
        //  每次追加，都有2个选择，左括号、右括号。 
         //   限制是：只要还有剩余 左括号，就可以加左括号
         //   如果 是： ()、() ()、(())、这种情况，就只能加左括号，不能加右括号。
        private List<string> parenthesisResult = new List<string>();
        public IList<string> GenerateParenthesis(int n)
        {
            ParenthesisLevel(n, 0, 0, "");
            return parenthesisResult;
        }
        private void ParenthesisLevel(int n, int leftCount, int rightCount, string lastStr)
        {
            if (leftCount == n)
            {
                for (int i = rightCount; i < n; i++)
                    lastStr += ")";
                parenthesisResult.Add(lastStr);
                return;
            }
            //增加左节点
            ParenthesisLevel(n, leftCount+1, rightCount, lastStr + '(');
            //增加右节点
            if (leftCount!=rightCount)
            {
                ParenthesisLevel(n, leftCount, rightCount+1, lastStr + ')');
            }
        }
        #endregion

        #region 全排列
        //思路：逐个向结果中添加 数字。 每一步遍历所有可能
        List<IList<int>> permuteResult = new List<IList<int>>();
        public IList<IList<int>> Permute(int[] nums)
        {
            PermuteSub(nums, new List<int>());
            return permuteResult;
        }
        public void PermuteSub(int[] nums, List<int> lastCreated)
        {
            var notUsed = nums.Except(lastCreated).ToList();
            if (!notUsed.Any())
                permuteResult.Add(lastCreated);
            foreach (var num in notUsed)
            {
                var newArr = lastCreated.Select(p => p).Union(new[] { num }).ToList();
                PermuteSub(nums, newArr);
            }
        }

        #endregion

        #region 子集
        public IList<IList<int>> Subsets(int[] nums)
        {
            List<IList<int>> result = new List<IList<int>>();
            foreach (var num in nums)
            {
                List<List<int>> newlsts = new List<List<int>>() { new List<int> { num } };

                foreach(var lst in result)
                {
                    var newlst = lst.Select(p => p).ToList();
                    newlst.Add(num);
                    newlsts.Add(newlst);
                }
                result.AddRange(newlsts);
            }
            result.Add(new List<int>());
            return result;
        }
        #endregion

        #region 
        //public bool Exist(char[][] board, string word)
        //{

        //}
        #endregion


        #region

        public IList<string> LetterCombinations(string digits)
        {
            if (string.IsNullOrEmpty(digits))
                return new List<string>();
            Dictionary<char, string> Lookup = new Dictionary<char, string>
            {
                { '2', "abc" },
                { '3', "def" },
                { '4', "ghi" },
                { '5', "jkl" },
                { '6', "mno" },
                { '7', "pqrs" },
                { '8', "tuv" },
                { '9', "wxyz" },
            };
            var elements = digits.Select(d => Lookup[d]).ToList();
            var sbs = elements[0].Select(c => new StringBuilder(c.ToString())).ToList();

            for (int i = 1; i < elements.Count; i++)
            {
                AppendCharToTail(sbs, elements[i]);
            }
            return sbs.Select(sb => sb.ToString()).ToList();
        }
        public List<StringBuilder> AppendCharToTail(List<StringBuilder> sbs, string c)
        {
            int count = sbs.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = 1; j < c.Length; j++)
                {
                    var newsb = Clone(sbs[i]);
                    newsb.Append(c[j]);
                    sbs.Add(newsb);
                }
                sbs[i].Append(c[0]);
            }
            return sbs;
        }
        public StringBuilder Clone(StringBuilder sb)
        {
            StringBuilder newsb = new StringBuilder();
            for (int i = 0; i < sb.Length; i++)
                newsb.Append(sb[i]);
            return newsb;
        }

        #endregion

        #region 39 组合总和
        /*
        以 [2,3,5], target=7为例：
                []
               2       3         5
           22 23 25 32 33 35  52 53 55 
        就是以树的形式遍历所有可能。 
        
        剪枝策略：
        1. 计算和如果超过 target，  则不再遍历其子节点
        难点：如何去重？
        在纸上画一下，然后注意观察。 

        规律就是：**从每一层的第 2 个结点开始，都不能再搜索产生同一层结点已经使用过的 candidate 里的元素。**
         */
        public void Test_CombinationSum()
        {
            var res1 = CombinationSum(new[] { 2, 3, 5, 6 }, 7);
            var res2 = CombinationSum(new[] { 2, 3, 5, 6 }, 5);
            var res3 = CombinationSum(new[] { 2, 3, 5, 6 }, 1);
            var res4 = CombinationSum(new[] { 2, 3, 5 }, 8);
            var res5 = CombinationSum(new[] { 2 }, 1);
        }
        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            CombinationSub(candidates, new List<int>(), target);
            return result_CombinationSum;
        }
        List<IList<int>> result_CombinationSum = new List<IList<int>>();
        public void CombinationSub(int[] candidates, List<int> combination, int target)
        {
            int sum = combination.Any() ? combination.Sum() : 0;
            List<int> searchedNums = new List<int>();
            foreach (var candidate in candidates)
            {
                if (sum + candidate == target)
                {
                    var finalCombination = combination.ToList();
                    finalCombination.Add(candidate);
                    result_CombinationSum.Add(finalCombination);
                }
                else if (sum + candidate < target)
                {
                    var newCandidates = candidates.Except(searchedNums).ToArray();
                    var newCombination = combination.ToList();
                    newCombination.Add(candidate);
                    CombinationSub(newCandidates, newCombination, target);
                }
                searchedNums.Add(candidate);
            }
        }

        #endregion
    }
}
