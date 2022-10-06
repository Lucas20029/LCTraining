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
    }



}
