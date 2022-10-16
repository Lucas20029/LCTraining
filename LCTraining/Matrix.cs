using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCTraining.Design 
{

    public class Matrix
    {
        public static Matrix Instance = new Matrix();

        #region 54 螺旋矩阵
        /*
        一圈圈地递归 
        */
        public void Test_SpiralOrder()
        {
            int[][] matrix = new[] { new[] { 1,2,3 }, new[] { 4,5,6 }, new[] { 7,8,9 } };
            var result = SpiralOrder(matrix);
            spiralOrderResult.Clear();

            int[][] matrix1 = new[] { new[] { 1, 2, 3 }, new[] { 4, 5, 6 }, new[] { 7, 8, 9 }, new[] {10,11,12 } };
            var result1 = SpiralOrder(matrix1);
            spiralOrderResult.Clear();

        }

        List<int> spiralOrderResult = new List<int>();

        public IList<int> SpiralOrder(int[][] matrix)
        {
            List<List<int>> newMatrix = new List<List<int>>();
            foreach(var arr in matrix)
            {
                newMatrix.Add(arr.ToList());
            }

            SpiralBoard(newMatrix);
            return spiralOrderResult;

        }
        public void SpiralBoard(List<List<int>> matrix)
        {
            //
            if (matrix.Count == 0 || matrix[0].Count==0)
                return;
            if (matrix.Count == 1)
            {
                spiralOrderResult.AddRange(matrix[0]);
                return;
            }
            if (matrix[0].Count == 1)
            {
                matrix.ForEach(n => spiralOrderResult.Add(n[0]));
                return;
            }

            int w = matrix[0].Count;
            int h = matrix.Count;
            
            for (int i = 0; i < w-1; i++)
                spiralOrderResult.Add(matrix[0][i]);
            for (int i = 0; i< h - 1; i++)
                spiralOrderResult.Add(matrix[i][w-1]);
            for (int i = w - 1; i > 0; i--)
                spiralOrderResult.Add(matrix[h - 1][i]);
            for (int i = h - 1; i > 0; i--)
                spiralOrderResult.Add(matrix[i][0]);

            matrix.RemoveAt(h-1);
            matrix.RemoveAt(0);
            foreach(var arr in matrix)
            {
                arr.RemoveAt(arr.Count - 1);
                arr.RemoveAt(0);
            }

            SpiralBoard(matrix);
        }
        #endregion

        #region 59 螺旋矩阵II
        public void Test_GenerateMatrix()
        {
            var res0= GenerateMatrix(5);
            var res0str= tostring(res0);
            var res1= GenerateMatrix(4);
            var res1str = tostring(res1);
            var res2 = GenerateMatrix(1);
            var res2str = tostring(res2);
        }
        public string tostring(int[][] matrix)
        {
            string res = "";
            foreach (var vec in matrix)
                res += string.Join(" ", vec) + "\r\n";
            return res;
        }

        public int[][] GenerateMatrix(int n)
        {
            List<int[]> lst = new List<int[]>();
            for (int i = 0; i < n; i++)
                lst.Add(new int[n]);
            var matrix = lst.ToArray();

            int num = 1;
            int round = (int)Math.Ceiling( (double)n / 2);
            for(int i = 0; i < round; i++)
            {
                GenerateMatrix_SetValue(matrix, n, i, ref num);
            }
            return matrix;
        }

        public void GenerateMatrix_SetValue(int[][] matrix, int n, int seq,ref  int start)
        {
            int subLeftTop = seq;
            int subRightBottom = n - seq-1;
            if (subRightBottom - subLeftTop < 1)
            {
                matrix[subLeftTop][subLeftTop] = start;
                return;
            }
            for(int i = subLeftTop; i < subRightBottom; i++)
            {
                matrix[subLeftTop][i] = start++;
            }
            for(int i = subLeftTop; i < subRightBottom; i++)
            {
                matrix[i][subRightBottom] = start++;
            }
            for(int i = subRightBottom; i > subLeftTop; i--)
            {
                matrix[subRightBottom][i] = start++;
            }
            for(int i = subRightBottom; i > subLeftTop; i--)
            {
                matrix[i][subLeftTop] = start++;
            }
        }

        #endregion

        #region 74 搜索二维矩阵
        //二分查找 矩阵。 矩阵从左到右、从上到下，元素递增。 
        //思路： 把矩阵看成一维数组进行二分查找。 额外增加 index <--> 坐标 的互转
        public void Test_SearchMatrix()
        {
            var matrix = new[] { new[] { 1, 3, 5, 7 }, new[] { 10, 12, 16, 20 }, new[] { 22, 26, 27, 29 } };
            var res1 = SearchMatrix(matrix, 6);
            var res2 = SearchMatrix(matrix, 16);
            var res3 = SearchMatrix(matrix, 29);
        }
        public bool SearchMatrix(int[][] matrix, int target)
        {
            int width = matrix[0].Length;
            int start = CoorToIdx(0, 0, width);
            int end = CoorToIdx(matrix.Length - 1, width - 1,  width);
            while (true)
            {
                if (end - start <= 1)
                {
                    var endCoor = IdxToCoordinate(end,width);
                    var startCoor = IdxToCoordinate(start,width);
                    if (matrix[endCoor.Item1][endCoor.Item2] == target || matrix[startCoor.Item1][startCoor.Item2] == target)
                        return true;
                    else
                        return false;
                }
                var mid = start + (end - start) / 2;
                
                var midCoor = IdxToCoordinate(mid, width);
                var midVal= matrix[midCoor.Item1][midCoor.Item2];
                if ( midVal> target)
                {
                    end = mid; continue;
                }
                if (midVal < target)
                {
                    start = mid; continue;
                }
                return true;
            }
        }
        public int CoorToIdx(int x, int y ,int n)
        {
            return x * n + y;
        }
        public Tuple<int,int> IdxToCoordinate(int idx, int n)
        {
            return new Tuple<int, int>(idx / n, idx % n);
        }


        #endregion

        #region 59螺旋矩阵II
        public int[][] GenerateMatrix(int n)
        {
            return null;
        }
        #endregion
    }



}
