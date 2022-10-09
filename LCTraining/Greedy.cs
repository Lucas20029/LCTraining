using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCTraining.Design 
{
    

    public class Greedy
    {
        public static Greedy Instance = new Greedy();
        #region 134 加油站
        public void Test_CanCompleteCircuit()
        {
            var gas = new[] {1, 1, 2, 3, 4, 5 };
            var cost = new[] {1, 3, 4, 5, 1, 2 };

            var res = CanCompleteCircuit(gas, cost);
        }
        public int CanCompleteCircuit(int[] gas, int[] cost)
        {
            if (gas.Sum() < cost.Sum())
                return -1;
            int sum = 0;
            int index = 0;
            for(int i = 0; i < gas.Length * 2; i++)
            {
                if (i - index >= gas.Length)
                    break;
                if (sum == 0)
                    index = i % gas.Length;
                sum += gas[i % gas.Length]-cost[i%gas.Length];
                if (sum > 0)
                    continue;
                else
                    sum = 0;
            }
            return index % gas.Length;
        }
        #endregion

    }
}
