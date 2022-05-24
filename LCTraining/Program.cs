using System;
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
            var node = Design.BinarySerilizor.Test();
            var str = Design.BinarySerilizor.Serialize(node);
            var root = Design.BinarySerilizor.Deserialize(str);
            //var res = SortAndSearch.Search(new[] { 4, 5, 6, 7, 0, 1, 2 }, 5);
            var matrix = new int[1][];
            matrix[0] = new[] { -5 };
            var res =SortAndSearch.SearchMatrix(matrix, -5);
        }
    }

    public class Design
    {
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) { val = x; }
        }
        public class BinarySerilizor
        {
            //思路：层序遍历。使用N标识空节点。 除了N，每个节点都存其左右子节点。
            //整个树序列化为： 6,N,5,3,4,N,2,N,N,1,N,N,N。 就是 6->(Left:N,Right:5)， 5->(3,4)，  3->(N,2)，4->(N,N)，2->(1,N)，1->(N,N)
            public static string Serialize(TreeNode root)
            {
                if (root == null)
                    return null;

                Queue<TreeNode> queue = new Queue<TreeNode>();
                queue.Enqueue(root);
                StringBuilder sb = new StringBuilder();
                while (queue.Any())
                {
                    var node = queue.Dequeue();
                    if (node == null) //为空，不继续输出。减少空间浪费
                        sb.Append('N');
                    else
                    { //只有不为空，才向Buffer中输出子节点
                        sb.Append(node.val);
                        queue.Enqueue(node.left);
                        queue.Enqueue(node.right);
                    }
                    sb.Append(',');
                }
                return sb.ToString();
            }

            public static TreeNode Deserialize(string data)
            {
                if (data == null)
                    return null;
                var items = data.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                //队列，只记不为空的节点
                Queue<TreeNode> noNullNodes = new Queue<TreeNode>();
                bool isleft = true;

                var root = new TreeNode(int.Parse(items[0]));
                noNullNodes.Enqueue(root);
                var parentNode = new TreeNode(0);

                for (int i = 1; i < items.Length - 1; i++)
                {
                    var item = items[i];
                    TreeNode node;
                    if (item == "N")//对于空节点，还是要实例化，并绑定到Parent的左/右子节点上，但是不需要入队，因为后续没记它的子节点（也不用记）
                        node = null;
                    else
                        node = new TreeNode(int.Parse(item));

                    //绑定Parent的左/右子节点
                    if (isleft)
                    {
                        parentNode = noNullNodes.Dequeue();
                        parentNode.left = node;
                    }
                    else
                    {
                        parentNode.right = node;
                    }
                    isleft = !isleft;

                    //只有不为空的节点入队。 后面的item会向队列中的节点后追加子节点
                    if (node != null)
                        noNullNodes.Enqueue(node);

                }
                return root;
            }

            #region 第一遍的思路
            //直接按层无脑输出节点，每层都是  2的n次方，
            //问题：这样空间太大，容易超时
            //
            // Encodes a tree to a single string.
            //public static string Serialize(TreeNode root)
            //{
            //    if (root == null)
            //        return null;

            //    List<List<string>> vals = new List<List<string>>();
            //    Queue<TreeNode> currentQueue = new Queue<TreeNode>();
            //    Queue<TreeNode> nextQueue = new Queue<TreeNode>();
            //    currentQueue.Enqueue(root);
            //    vals.Add(new List<string>());

            //    while (currentQueue.Any())
            //    {
            //        var node = currentQueue.Dequeue();
            //        vals.Last().Add(node?.val.ToString() ??"N");
            //        nextQueue.Enqueue(node?.left);
            //        nextQueue.Enqueue(node?.right);
            //        if(!currentQueue.Any())
            //        {
            //            if (vals.Last().Any(p => p != "N"))
            //            {
            //                currentQueue = nextQueue;
            //                nextQueue = new Queue<TreeNode>();
            //                vals.Add(new List<string>());
            //            }
            //            else
            //            {
            //                vals.RemoveAt(vals.Count - 1);
            //                break;
            //            }
            //        }
            //    }
            //    var rowVals = vals.Select(p => string.Join(",", p));
            //    return string.Join("#", rowVals);
            //}
            /*
              Queue<TreeNode> queue = new LinkedList<>();
        queue.add(root);
        StringBuffer sb = new StringBuffer();
        while (!queue.isEmpty()) {
            TreeNode node = queue.poll();
            if (node == null) {
                sb.append(Integer.MAX_VALUE);
            } else {
                sb.append(node.val);
                queue.add(node.left);
                queue.add(node.right);
            }
            sb.append(',');
        }
        sb.deleteCharAt(sb.length() - 1);
        return sb.toString();

                 
                 */


            ////Decodes your encoded data to tree.
            //public static TreeNode Deserialize(string data)
            //{
            //    if (data == null)
            //        return null;
            //    var paths = data.Split('#').Select(p => p.Split(',').ToList()).ToList();
            //    TreeNode root = new TreeNode(int.Parse(paths[0][0]));
            //    Queue<TreeNode> lastLevel = new Queue<TreeNode>();
            //    lastLevel.Enqueue(root);
            //    for(int i = 1; i < paths.Count; i++)
            //    {
            //        for(int j = 0; j < paths[i].Count; j+=2)
            //        {
            //            var parentNode = lastLevel.Dequeue();
            //            if (parentNode != null)
            //            {
            //                if (paths[i][j] != "N")
            //                {
            //                    parentNode.left = new TreeNode(int.Parse(paths[i][j]));
            //                }
            //                if (paths[i][j + 1] != "N")
            //                {
            //                    parentNode.right = new TreeNode(int.Parse(paths[i][j + 1]));
            //                }
            //            }
            //            lastLevel.Enqueue(parentNode?.left);
            //            lastLevel.Enqueue(parentNode?.right);
            //        }
            //    }
            //    return root;
            //}
            #endregion

            public static TreeNode Test()
            {
                TreeNode node1 = new TreeNode(1);
                TreeNode node2 = new TreeNode(2) { left = node1 };
                TreeNode node3 = new TreeNode(3) { right = node2 };
                TreeNode node4 = new TreeNode(4);
                TreeNode node5 = new TreeNode(5) { left = node3, right =node4};
                TreeNode node6 = new TreeNode(6) { right = node5};
                return node6;
            }
        }
    }
    public class SortAndSearch
    {
        #region 合并区间
        public int[][] Merge(int[][] intervals)
        {
            var sortedIntervals = intervals.OrderBy(p => p[0]).ToArray();
            List<int[]> result = new List<int[]>();
            foreach (var interval in sortedIntervals)
            {
                if (!result.Any())
                {
                    result.Add(interval);
                    continue;
                }
                if((interval[0]>= result.Last()[0]) && (interval[0] <= result.Last()[1]))
                {
                    if (result.Last()[1] < interval[1])
                        result.Last()[1] = interval[1];
                }
                else
                {
                    result.Add(interval);
                }
            }
            return result.ToArray();
        }
        #endregion

        #region 在排序数组旋转后的数组中，搜索目标
        //思路：二分的思想就是：根据有序元素段判断元素位置以不断收缩边界选择搜索空间。 这里虽然不是全局有序，但可以分成几个有序的区段。
        //然后这几个区段都有明显的特点。
        //例如：8 9 1 2 3 4 5， mid=2，必然 8>2
        //整个数组可以分为3个区段，  left~断点：8 9 ， 断点~mid：1 2， mid~right：3 4 5
        public static int Search(int[] nums, int target)
        {
            int left = 0, right = nums.Length - 1;
            while (left<=right)
            {
                var mid = left + (right - left) / 2;
                if (nums[mid] == target)
                    return mid;
                if(nums[left]==target)
                    return left;
                if (nums[right] == target)
                    return right;
                //如果旋转点在左边，那么 肯定 nums[mid]<nums[left]。  
                if (nums[mid] < nums[left])
                {
                    //Target 属于 left~断点
                    if (target > nums[left])
                        right = mid - 1;
                    //Target 属于 断点~mid
                    else if (target < nums[mid])
                        left = left + 1;
                    //Target 属于 mid~right  （正常区段）
                    else
                        left = mid + 1;
                }
                //如果旋转点在右边， 那么，肯定 nums[mid]>nums[right]
                else if (nums[mid]> nums[right])
                {
                    if (target < nums[right])
                        left = mid + 1;
                    else if (target > nums[mid])
                        right = right - 1;
                    else
                        right = mid - 1;
                }
                else //是顺子的情况， 数组是： 0 1 2 3 4 5 6 7，断点任意。用正常二分法即可
                {
                    if (target < nums[mid])
                        right = mid - 1;
                    else
                        left = mid + 1;
                }
            }
            return -1;
        }
        #endregion


        #region 搜索二维矩阵
        //从给的矩阵中高效搜索元素。矩阵特点：
        //  - 每行的元素从左到右升序排列。
        //  - 每列的元素从上到下升序排列。
        //思路：从 右上角、左下角观察，这种矩阵就是天然的 二叉搜索树。（左子节点<Root<右子节点）
        public static bool SearchMatrix(int[][] matrix, int target)
        {
            int i = 0, j = matrix[0].Length - 1;

            while (i<matrix.Length && j>=0)
            {
                if (matrix[i][j] > target)
                    j--;
                else if (matrix[i][j] < target)
                    i++;
                else
                    return true;
            }
            return false;
        }
        #endregion
    }
}
