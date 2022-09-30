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


        #region 22. 括号生成
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

        #region 46. 全排列
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

        #region 78. 子集
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

        #region 17 电话号码
        /* 题目：
         给定一个仅包含数字 2-9 的字符串，返回所有它能表示的字母组合。答案可以按 任意顺序 返回

         输入：digits = "23"
         输出：["ad","ae","af","bd","be","bf","cd","ce","cf"]
         */
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


    }
}
