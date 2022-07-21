using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            Day1 d1 = new Day1();
            Day2 d2 = new Day2();
            d2.PrintKMoves(2);
        }
    }
    public class Day1
    {
        /// <summary>
        /// #566. 重塑矩阵
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="r"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public int[][] MatrixReshape(int[][] mat, int r, int c)
        {
            if (mat.Length * mat[0].Length != r * c)
                return mat;

            List<int[]> result = new List<int[]>();
            List<int> cur = new List<int>();
            for (int i = 0; i < mat.Length; i++)
            {
                for (int j = 0; j < mat[0].Length; j++)
                {
                    cur.Add(mat[i][j]);
                    if (cur.Count >= c)
                    {
                        result.Add(cur.ToArray());
                        cur = new List<int>();
                    }
                }
            }
            return result.ToArray();
        }


    }
}
