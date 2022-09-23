using ConsoleApp1;
using ConsoleApp1.MathAlgorithm;
using LCTraining.Design;
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
            var canJumpRes = DP.Instance.CanJump(new[] { 2, 3, 1, 1, 4 });
            var maxSubArrayRes = DP.Instance.MaxSubArray(new[] { 2, 4, -5, 1, 3, -6, 7, -5, -3 });
            var subsetResult = BackTrack.Instance.Subsets(new[] { 1,2,3,4,5});
            var permuteResult = BackTrack.Instance.Permute(new[] {1,2,3,4,5 });
            var parenthesisResult = BackTrack.Instance.GenerateParenthesis(3);
            maxSubArrayRes = DP.Instance.MaxSubArray(new[] { -2, -4, -5, 6, -1 });
            maxSubArrayRes = DP.Instance.MaxSubArray(new[] { -2, -4, -5, -3 });
            var node = Design.BinarySerilizor.Test();
            var str = Design.BinarySerilizor.Serialize(node);
            var root = Design.BinarySerilizor.Deserialize(str);

            var maxProfitResult = DP.Instance.MaxProfit(new[] { 7, 1, 5, 3, 6, 4 });
            var robResult = DP.Instance.Rob(new[] { 1, 2, 3, 1 });
            var sortedArrayToBSTResult = BinaryTree.Instance.SortedArrayToBST(new[] { 1, 2, 3, 4, 5,6,7,8,9 });
            int[][] matrixRoate90Param = { new int[] { 1, 2, 3,4 }, new int[] {  5, 6,7,8 }, new int[] { 9,10,11,12 }, new int[] { 13, 14, 15, 16 } };
            ArrStrLink.RotateMatrix90(matrixRoate90Param);
            var sss = ArrStrLink.MyAtoI("  -42");
            var ss = BinaryTree.Instance.BuildTree(new[] { 3, 9, 20, 15, 7 }, new[] { 9, 3, 15, 20, 7 });
            var rrr0 = Mathimatics.Instance.RomanToInt("MCMXCIV");
            var rrr1 = Other.Instance.GetSum(7, 7);
            var result0 = BackTrack.Instance.LetterCombinations("253");
            var result1 = Other.Instance.IsValidBrakets("[{()()[()]}]");
            var result2  = Other.Instance.GenerateYanghuiTriangle(6);
            int resInt = 0;

            resInt = Other.Instance.EvalRPN(new[] { "2", "1", "+", "3", "*" });
            resInt = Other.Instance.EvalRPN(new[] { "4", "13", "5", "/", "+" });
            resInt = Other.Instance.EvalRPN(new[] { "10", "6", "9", "3", "+", "-11", "*", "/", "*", "17", "+", "5", "+" });

            resInt = Mathimatics.Instance.MySqrt(2147395599);
            resInt = Mathimatics.Instance.MySqrt(9);
            resInt = Mathimatics.Instance.MySqrt(24);

            resInt = Mathimatics.Instance.TitleToNumber("A");
            resInt = Mathimatics.Instance.TitleToNumber("AB");
            resInt = Mathimatics.Instance.TitleToNumber("ZY");

            var res0 = Mathimatics.Instance.IsHappy(0);
            var res1 = Mathimatics.Instance.IsHappy(1);
            var res2 = Mathimatics.Instance.IsHappy(2);
            var res3 = Mathimatics.Instance.IsHappy(49);


            //var res = SortAndSearch.Search(new[] { 4, 5, 6, 7, 0, 1, 2 }, 5);
            var matrix = new int[1][];
            matrix[0] = new[] { -5 };
            var res = SortAndSearch.SearchMatrix(matrix, -5);
        }
    }   
}
