using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class BinaryTree
    {
        public static BinaryTree Instance = new BinaryTree();

        #region 辅助方法
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }
        public class NodeWithNext
        {
            public int val;
            public NodeWithNext left;
            public NodeWithNext right;
            public NodeWithNext next;

            public NodeWithNext() { }

            public NodeWithNext(int _val)
            {
                val = _val;
            }

            public NodeWithNext(int _val, NodeWithNext _left, NodeWithNext _right, NodeWithNext _next)
            {
                val = _val;
                left = _left;
                right = _right;
                next = _next;
            }
        }
        #endregion
        #region 二叉树 -- 初级
        # region 求一个二叉树最大深度
        public int MaxDepth(TreeNode root)
        {
            if (root == null)
                return 0;
            return Math.Max(MaxDepth(root.left), MaxDepth(root.right)) + 1;
        }
        #endregion
        #region 111 二叉树的最小深度
        public int MinDepth(TreeNode root)
        {
            if (root == null)
                return 0;
            int min = int.MaxValue;
            MinDepth_Sub(root, 0, ref min);
            return min;
        }
        public void MinDepth_Sub(TreeNode node, int depth, ref int minDepth)
        {
            if (node.left == null && node.right == null)
                minDepth = Math.Min(depth, minDepth);
            if (node.left != null)
                MinDepth_Sub(node.left, depth + 1, ref minDepth);
            if (node.right != null)
                MinDepth_Sub(node.right, depth + 1, ref minDepth);
        }
        #endregion

        #region 验证二叉搜索树
        //解法1：递归
        public bool IsValidBST_Recrusive(TreeNode root)
        {
            if (root == null)
                return true;
            return IsValidBST_Recrusive(root, long.MinValue, long.MaxValue);
        }
        public bool IsValidBST_Recrusive(TreeNode root, long min, long max)
        {
            if (root == null)
                return true;
            if (root.val >= max || root.val <= min)
                return false;

            return IsValidBST_Recrusive(root.left, min, root.val) && IsValidBST_Recrusive(root.right, root.val, max);
        }


        //解法2：中序遍历
        //中序遍历。 顶节点在中间访问。 即访问顺序是： left-> root -> right
        //前序遍历： 顶节点在前面访问。 即访问顺序是： root-> left -> right
        //后序同理。
        //中序遍历二叉搜索树，遍历的结果一定是有序的
        TreeNode prev;
        public bool IsValidBST_MiddleFirst(TreeNode root)
        {
            if (root == null)
                return true;
            if (!IsValidBST_MiddleFirst(root.left))
                return false;
            //访问根节点。
            //判断：根节点的左右节点是否成立
            if (prev != null && prev.val >= root.val)
                return false;

            prev = root;
            if (!IsValidBST_MiddleFirst(root.right))
                return false;
            return true;

        }
        #endregion

        #region 验证二叉树对称
        //解法1： 递归法。把root的左右分成两个子树。
        //左子树， 深度优先，遍历顺序：Left->Node->Right
        //右子树， 深度优先，遍历顺序：Right->Node->Left
        //要求递归的每一步，左右子树分别执行一次。（可以左右手同时比划：左手往左走，右手往右走）
        // 每一步都要相等，或者全部为null。否则返回false。
        //一旦子递归返回false，上层立刻返回false。只有为true，才向下进行。
        public bool IsSymmetric(TreeNode root)
        {
            if (root == null)
                return true;
            return SearchDFS(root.left, root.right);
        }

        public bool SearchDFS(TreeNode left, TreeNode right)
        {
            if (left == null && right == null)
                return true;
            if ((left != null && right == null) || (left == null && right != null))
                return false;
            if (!SearchDFS(left.left, right.right))
                return false;
            if (left.val != right.val)
                return false;
            if (!SearchDFS(left.right, right.left))
                return false;
            return true;
        }

        //解法2：队列+循环。 其实本质思路类似于 上面的递归
        public bool IsSymmetric_Queue(TreeNode node)
        {
            if (node == null)
                return true;
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(node.left);
            queue.Enqueue(node.right);
            while (queue.Count > 0)
            {
                var left = queue.Dequeue();
                var right = queue.Dequeue();
                if (left == null && right == null)
                    continue;
                if ((left == null && right != null) || (left != null && right == null) || (left.val != right.val))
                    return false;
                queue.Enqueue(left.left);
                queue.Enqueue(right.right);
                queue.Enqueue(left.right);
                queue.Enqueue(right.left);
            }
            return true;
        }

        #endregion

        #region 二叉树层序遍历
        //思路：递归
        List<IList<TreeNode>> All = new List<IList<TreeNode>>();
        public IList<IList<int>> LevelOrder(TreeNode root)
        {
            if (root == null)
                return Map(All);
            All.Add(new List<TreeNode> { root });
            GetNextLevelNodes(new List<TreeNode> { root });
            return Map(All);
        }
        public void GetNextLevelNodes(List<TreeNode> nodes)
        {
            List<TreeNode> nextLevel = new List<TreeNode>();
            foreach (var node in nodes)
            {
                if (node.left != null)
                    nextLevel.Add(node.left);
                if (node.right != null)
                    nextLevel.Add(node.right);
            }
            if (nextLevel.Any())
            {
                All.Add(nextLevel);
                GetNextLevelNodes(nextLevel);
            }
        }
        public List<IList<int>> Map(List<IList<TreeNode>> nodes)
        {
            List<IList<int>> result = new List<IList<int>>();
            foreach (var nodelist in nodes)
            {
                result.Add(nodelist.Select(p => p.val).ToList());
            }
            return result;
        }
        #endregion

        #region 将有序数组转为二叉搜索树
        public TreeNode SortedArrayToBST(int[] nums)
        {
            if (nums == null || !nums.Any())
                return null;
            if (nums.Length == 1)
                return new TreeNode(nums[0]);


            int mid = nums.Length / 2;
            var node = new TreeNode(nums[mid]);
            var leftArr = nums.Take(mid).ToArray();
            var rightArr = nums.Skip(mid + 1).Take(nums.Length - 1 - mid).ToArray();
            node.left = SortedArrayToBST(leftArr);
            node.right = SortedArrayToBST(rightArr);
            return node;
        }
        #endregion

        #region 100 相同的树
        public void Test_IsSameTree()
        {
            var p = new TreeNode { val = 1, left = new TreeNode { val = 2 }, right = new TreeNode { val = 3 } };
            var q = new TreeNode { val = 1, left = new TreeNode { val = 2 }, right = new TreeNode { val = 3 } };
            var res = IsSameTree(p, q);

            var p1 = new TreeNode { val = 1, right = new TreeNode { val = 3 } };
            var q1 = new TreeNode { val = 1, left = new TreeNode { val = 3 } };
            var res1 = IsSameTree(p1, q1);
        }
        public bool IsSameTree(TreeNode p, TreeNode q)
        {
            return IsSameTree_Sub(p, q);
        }
        private bool IsSameTree_Sub(TreeNode pnode, TreeNode qnode)
        {
            if (pnode == null && qnode == null)
                return true;
            if (pnode?.val != qnode?.val)
                return false;
            if (!IsSameTree_Sub(pnode.left, qnode.left))
                return false;
            if (!IsSameTree_Sub(pnode.right, qnode.right))
                return false;
            return true;
        }
        #endregion

        #region 112 路径总和
        /*示例：   1->(2,3)  2->(6,7)  寻找目标 11，  即 1->3->7，返回true，存在该路径
         * 前序遍历， 向递归内函数传递当前累加的值， 到叶子节点终止递归*/
        public void Test_HasPathSum()
        {
            var node = Build();
            var res1 = HasPathSum(node, 16);
            var res2 = HasPathSum(node, 20);
        }
        public bool HasPathSum(TreeNode root, int targetSum)
        {
            if (root == null)
                return false;
            return HasPathSum_CalcSub(root, targetSum, 0);
        }
        public bool HasPathSum_CalcSub(TreeNode node, int targetSum, int curSum)
        {
            var sum = curSum + node.val;
            if (node.left == null && node.right == null)
            {
                if (sum == targetSum)
                    return true;
                else
                    return false;
            }
            if (node.left != null)
            {
                var leftResult = HasPathSum_CalcSub(node.left, targetSum, sum);
                if (leftResult == true)
                    return true;
            }
            if (node.right != null)
            {
                var rightResult = HasPathSum_CalcSub(node.right, targetSum, sum);
                if (rightResult == true)
                    return true;
            }
            return false;
        }
        #endregion
        #endregion

        #region 二叉树 -- 中级
        List<int> result = new List<int>();
        public IList<int> InorderTraversal(TreeNode root)
        {
            InorderTraversalCore(root);
            return result;
        }
        public void InorderTraversalCore(TreeNode root)
        {
            if (root == null)
                return;
            InorderTraversalCore(root.left);
            result.Add(root.val);
            InorderTraversalCore(root.right);
        }

        #region 二叉树锯齿形遍历
        public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            List<IList<int>> result = new List<IList<int>>();
            if (root == null)
                return result;

            bool direction = false;
            Stack<TreeNode> stack = new Stack<TreeNode>();
            Stack<TreeNode> stackNext = new Stack<TreeNode>();
            stack.Push(root);
            result.Add(new List<int>());
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                result.Last().Add(node.val);
                if (!direction)
                {
                    if (node.left != null)
                        stackNext.Push(node.left);
                    if (node.right != null)
                        stackNext.Push(node.right);
                }
                else
                {
                    if (node.right != null)
                        stackNext.Push(node.right);
                    if (node.left != null)
                        stackNext.Push(node.left);
                }
                if (stack.Count == 0)
                {
                    direction = !direction;
                    if (stackNext.Count > 0)
                        result.Add(new List<int>());
                    stack = stackNext;
                    stackNext = new Stack<TreeNode>();
                }
            }
            return result;
        }
        #endregion


        #region 从前序、中序遍历序列构造二叉树
        //思路： 前序的第一个元素必定时根。 中序里，根左边的元素，必定是在左子树上，右边的元素，必定在右子树上。 利用这种规律进行递归。
        //举例： 1->(2,3)， 2->(4,5)， 3->(6,7)。
        //       那么，此树的前序是： 1245367， 中序是4251637。 依据这两个序列，进行分析：
        //       1. 前序[0]，即1 是根节点
        //       2. 中序里， 1的左边：425在左子树上； 1的右边：637在右子树上。
        //       3. 对于左子树，其中序为：425，前序为：245
        //       4. 对于右子树，其中序为：637，前序为：367
        //       递归继续构造左子树、右子树。
        public TreeNode BuildTree(int[] preorder, int[] inorder)
        {
            TreeNode root = new TreeNode(preorder[0]);
            var rootInIndex = IndexOf(inorder, preorder[0]);
            var leftCount = rootInIndex;
            var rightCount = inorder.Length - leftCount - 1;
            var leftInorder = inorder.Take(leftCount).ToArray();
            var rightInorder = inorder.Skip(leftCount + 1).Take(rightCount).ToArray();
            var leftPreOrder = preorder.Skip(1).Take(leftCount).ToArray();
            var rightPreOrder = preorder.Skip(1 + leftCount).Take(rightCount).ToArray();

            if (leftCount > 0)
                root.left = BuildTree(leftPreOrder, leftInorder);
            if (rightCount > 0)
                root.right = BuildTree(rightPreOrder, rightInorder);

            return root;
        }
        private int IndexOf(int[] seq, int num)
        {
            for (int i = 0; i < seq.Length; i++)
            {
                if (seq[i] == num)
                    return i;
            }
            return -1;
        }
        #endregion

        #region 完美二叉树 - 填充Next指针指向右侧节点

        public NodeWithNext Connect(NodeWithNext root)
        {
            if (root == null)
                return root;
            Queue<NodeWithNext> queue = new Queue<NodeWithNext>();
            Queue<NodeWithNext> nextQueue = new Queue<NodeWithNext>();
            NodeWithNext prevNode = null;
            queue.Enqueue(root);
            while (queue.Any() || nextQueue.Any())
            {
                if (!queue.Any())
                {
                    queue = nextQueue;
                    nextQueue = new Queue<NodeWithNext>();
                    prevNode = null;
                }
                var node = queue.Dequeue();
                if (prevNode != null)
                {
                    prevNode.next = node;
                }
                prevNode = node;
                if (node.left != null)
                    nextQueue.Enqueue(node.left);
                if (node.right != null)
                    nextQueue.Enqueue(node.right);
            }
            return root;
        }


        #endregion

        #region 二叉搜索树-第K个最小元素
        List<int> arr = new List<int>();
        public int KthSmallest(TreeNode root, int k)
        {
            InOrder(root);
            return arr[k - 1];
        }
        public void InOrder(TreeNode node)
        {
            if (node == null)
                return;
            InOrder(node.left);
            arr.Add(node.val);
            InOrder(node.right);
        }


        int finalResult = 0;
        int count = 0;
        int target = 0;
        public int KthSmallest_1(TreeNode root, int k)
        {
            target = k;
            InOrderTravelCount(root);
            return finalResult;
        }
        public void InOrderTravelCount(TreeNode node)
        {
            if (node == null)
                return; ;
            InOrderTravelCount(node.left);
            count++;
            if (count == target)
                finalResult = node.val;
            InOrderTravelCount(node.right);
        }
        #endregion

        #region 岛屿数量 (矩阵四邻元素)
        //前提：能修改入参数组元素的值。
        //思路：1. 深度优先或者广度优先 遍历找到的节点，就能抓出这个节点所在整个岛屿。
        //      2. 遍历到哪个元素，就把它的值改为0，避免遍历后续节点重复统计
        int islandCount = 0;
        public int NumIslands(char[][] grid)
        {
            if (grid == null || grid.Length == 0)
                return 0;
            var m = grid.Length;
            var n = grid[0].Length;
            for (var i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] != '0')
                    {
                        islandCount++;
                        DFSTravel(grid, i, j, m, n);
                    }
                }
            }
            return islandCount;
        }
        public void DFSTravel(char[][] grid, int i, int j, int m, int n)
        {
            if (grid[i][j] == '0')
                return;
            if (grid[i][j] == '1')
            {
                grid[i][j] = '0';
                if (i != 0)
                    DFSTravel(grid, i - 1, j, m, n);
                if (i != m - 1)
                    DFSTravel(grid, i + 1, j, m, n);
                if (j != 0)
                    DFSTravel(grid, i, j - 1, m, n);
                if (j != n - 1)
                    DFSTravel(grid, i, j + 1, m, n);
            }
        }

        #endregion

        #region 79 单词搜索
        /*
         出错的点：
         1. 计算矩阵坐标 x、y， 容易搞反。  board[x][y] 哪是横坐标、哪是纵坐标。。 遍历、检查边界是， 哪个对于board.length, 哪个对应 board[0].length
         2. 没有检查 [['a']] 的情况
         3. 不要用Clone这种浅复制，没用。 只是复制了二维数组的竖向指针。
         4. 出现不匹配的，记得要把值设置回去。让其他分支还能用
             */
        public void Test_Exist()
        {
            //char[][] word = new[]
            //{
            //    new[] { 'a', 'd', 'c', 'd', 'e', 'f' },
            //    new[] { 'e', 'd', 'a', 'f', 'z', 'x' },
            //    new[] { 'n', 'm', 'k', 'f', 't', 'y' },
            //    new[] { 'g', 'h', 'k', 'e', 'p', 'v' },
            //};
            char[][] word = new[]
            {
                new[] { 'a', 'b', 'c', 'e' },
                new[] { 's', 'f', 'c', 's' },
                new[] { 'a', 'd', 'e', 'e' }
            };
            var result = Exist(word, "abceseecfs");
            char[][] word1 = new[] { new[] { 'a' } };
            var result1 = Exist(word1, "a");
            char[][] word2 = new[]
            {
                new []{ 'c','a','a' },
                new []{ 'a','a','a' },
                new []{ 'b','c','d' },
            };
            var result2 = Exist(word2, "aab");
        }

        public bool Exist(char[][] board, string word)
        {
            int width = board[0].Length;
            int height = board.Length;
            if (width == 1 && height == 1)
            {
                return word.Length==1&& board[0][0]==word[0];
            }
            var newboard = new char[height][];
            for(int i =0;i<height;i++)
                newboard[i]= new char[width];

            for(int x = 0; x < height; x++)
            {
                for(int y = 0; y < width; y++)
                {
                    if (board[x][y] == word[0])
                    {

                        DeepCopy(board, newboard);
                        if(Exist_FindFromCell(newboard, x, y, word,0))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public void DeepCopy(char[][] board, char[][] newboard)
        {
            for(int i = 0; i < board.Length; i++)
            {
                for(int j = 0; j < board[0].Length; j++)
                {
                    newboard[i][j] = board[i][j];
                }
            }
        }
        public bool Exist_FindFromCell(char[][] board, int x, int y, string word, int wordIdx)
        {
            if (wordIdx >= word.Length)
                return true;
            if (board[x][y] != word[wordIdx])
                return false;
            char temp = board[x][y];
            board[x][y] = '-';

            bool subResult = false;
            if (x > 0)
            {
                subResult |= Exist_FindFromCell(board, x - 1, y, word, wordIdx + 1);
            }
            if (x < board.Length-1)
            {
                subResult |= Exist_FindFromCell(board, x + 1, y, word, wordIdx + 1);
            }
            if (y > 0)
            {
                subResult |= Exist_FindFromCell(board, x, y - 1, word, wordIdx + 1);
            }
            if(y< board[0].Length-1)
            {
                subResult |= Exist_FindFromCell(board, x, y + 1, word, wordIdx + 1);
            }
            if (subResult == false)
                board[x][y] = temp;

            return subResult;

        }
        #endregion

        #region 129.求根节点到叶节点数字之和
        /*
         * 测试用例：   1->(2,3)   2->(4,5)  路径数字为： 124,125,13， 累加起来 262
         * 前序遍历，把计算好的数据传入子节点递归函数中
         */
        public void Test_SumNumbers()
        {
            var node = Build();
            var res1 = SumNumbers(node); //
        }
        public int SumNumbers(TreeNode root)
        {
            if (root == null)
                return 0;
            SumNumbers_SubTree(root, 0);
            return TempTreeNumbers.Sum();
        }
        List<int> TempTreeNumbers = new List<int>();
        public void SumNumbers_SubTree(TreeNode current, int parentNumber)
        {
            var number = parentNumber * 10 + current.val;
            if (current.left==null && current.right == null) //到达叶子节点
            {
                TempTreeNumbers.Add(number);
                return;
            }
            if (current.left != null)
            {
                SumNumbers_SubTree(current.left, number);
            }
            if (current.right != null)
            {
                SumNumbers_SubTree(current.right, number);
            }
        }
        #endregion


        #region 99. 恢复二叉搜索树
        /*
        先中序遍历，获取顺序。然后检查列表顺序，找出不符合顺序的节点，交换值 
        */
        public void Test_RecoverTree()
        {
            TreeNode root = new TreeNode(6) 
            {
                left = new TreeNode(3) { left = new TreeNode(2), right=new TreeNode(4) } ,
                right = new TreeNode(7) { left = new TreeNode(5), right= new TreeNode(9)}
            };
            RecoverTree(root);
        }
        public void RecoverTree(TreeNode root)
        {
            var treeNodes = new List<TreeNode>();
            RecoverTree_BSF(root, treeNodes);

            TreeNode firstNode=null, secondNode = null;
            for(int i = 0; i < treeNodes.Count - 1; i++)
            {
                if (treeNodes[i].val > treeNodes[i + 1].val)
                {
                    if (firstNode == null)
                    {
                        firstNode = treeNodes[i];
                        secondNode = treeNodes[i + 1];
                    }
                    else
                    {
                        secondNode = treeNodes[i + 1];
                    }
                }
            }

            if(firstNode!=null && secondNode != null)
            {
                int temp = firstNode.val;
                firstNode.val = secondNode.val;
                secondNode.val = temp;
            }
        }

        public void RecoverTree_BSF(TreeNode node, List<TreeNode> treeNodes)
        {
            if (node.left != null)
                RecoverTree_BSF(node.left, treeNodes);

            treeNodes.Add(node);

            if (node.right != null)
                RecoverTree_BSF(node.right, treeNodes);
        }
        #endregion

        #region Test
        public TreeNode Build()
        {
            TreeNode node1 = new TreeNode(1,null,null);
            TreeNode node2 = new TreeNode(2, null, null);
            TreeNode node3 = new TreeNode(3, null, null);
            TreeNode node4 = new TreeNode(4, null, null);

            TreeNode node5 = new TreeNode(5, node1, node2);
            TreeNode node6 = new TreeNode(6, node3, node4);

            TreeNode node7 = new TreeNode(7, node5, node6);
            return node7;
        }
        #endregion
        #endregion
    }


}
