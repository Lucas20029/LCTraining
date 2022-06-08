using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1.Design
{
    #region 初级
    //构造方法： 初始化输入一个 int 数组
    //支持Reset方法：返回初始化的入参数组
    //支持Shuffle方法：把数组随机打乱
    /**
     * Your Solution object will be instantiated and called as such:
     * Solution obj = new Solution(nums);
     * int[] param_1 = obj.Reset();
     * int[] param_2 = obj.Shuffle();
     */
    public class ShuffleSolution
    {
        private int[] Nums;
        public ShuffleSolution(int[] nums)
        { 
            Nums = nums;
        }

        public int[] Reset()
        {
            return Nums;
        }

        public int[] Shuffle()
        {
            var tempNums = Nums.Clone() as int[];
            Random ran = new Random();
            for(int i=0;i<tempNums.Length; i++)
            {
                var index = ran.Next(tempNums.Length-1);
                Swap(tempNums, i, index);
            }
            return tempNums;
        }
        private void Swap(int [] nums ,int left, int right)
        {
            int temp = nums[left];
            nums[left] = nums[right];
            nums[right] = temp;
        }
    }

    
    public class Test_MinStack
    {
        public void Test()
        {
            MinStack ms = new MinStack();
            ms.Push(-1);
            ms.Push(0);
            ms.Push(-2);
            var t1 = ms.Top();
            var t2 = ms.GetMin();
        }
    }
    //常数时间内，可以返回整个栈最小值的栈
    public class MinStack
    {
        List<int> Nums = new List<int>();
        List<int> MinValues = new List<int>();
        int TopIndex = -1;
        public MinStack()
        {

        }

        public void Push(int val)
        {
            var min = TopIndex == -1? val: Math.Min(MinValues[TopIndex], val);
            if (TopIndex == Nums.Count - 1)
            {
                Nums.Add(val);
                MinValues.Add(min);
                
            }
            else
            {
                Nums[TopIndex + 1] = val;
                MinValues[TopIndex + 1] = min;
            }
            TopIndex++;
        }

        public void Pop()
        {
            if(TopIndex>=0)
                TopIndex--;
        }

        public int Top()
        {
            if(TopIndex>=0)
                return Nums[TopIndex];
            throw new Exception("Stack Empty");
        }

        public int GetMin()
        {
            if(TopIndex>=0)
                return MinValues[TopIndex];
            throw new Exception("Stack Empty");
        }
    }

    /**
     * Your MinStack object will be instantiated and called as such:
     * MinStack obj = new MinStack();
     * obj.Push(val);
     * obj.Pop();
     * int param_3 = obj.Top();
     * int param_4 = obj.GetMin();
     */

    #endregion

    #region 中级
    #region 二叉树序列化

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
            TreeNode node5 = new TreeNode(5) { left = node3, right = node4 };
            TreeNode node6 = new TreeNode(6) { right = node5 };
            return node6;
        }
    }

    #endregion

    #region 常数时间插入删除获取随机的集合
    public class RandomizedSet
    {
        HashSet<int> set = new HashSet<int>();
        Random random = new Random();
        public RandomizedSet()
        {

        }

        public bool Insert(int val)
        {
            if (set.Contains(val))
                return false;
            set.Add(val);
            return true;
        }

        public bool Remove(int val)
        {
            if (!set.Contains(val))
                return false;
            set.Remove(val);
            return true;
        }

        public int GetRandom()
        {
            var index =  random.Next(set.Count);
            return set.ElementAtOrDefault(index);
        }
    }
    #endregion
    #endregion
}
