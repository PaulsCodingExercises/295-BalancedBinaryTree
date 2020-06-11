using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class BinaryTreeNode
    {
        public int Value { get; }

        public BinaryTreeNode Left { get; private set; }

        public BinaryTreeNode Right { get; private set; }

        public BinaryTreeNode(int value)
        {
            Value = value;
        }

        public BinaryTreeNode InsertLeft(int leftValue)
        {
            Left = new BinaryTreeNode(leftValue);
            return Left;
        }

        public BinaryTreeNode InsertRight(int rightValue)
        {
            Right = new BinaryTreeNode(rightValue);
            return Right;
        }

        public static BinaryTreeNode InitialiseWithData(bool pass)
        {
            /*
                   1
                 /   \
                2     3
               /     / \  
              4     5    6
                     \    \
                      7     8
            */

            //Setup base tree
            BinaryTreeNode n1 = new BinaryTreeNode(1);
            n1.InsertLeft(2);
            n1.InsertRight(3);

            BinaryTreeNode n2 = n1.Left;
            n2.InsertLeft(4);

            BinaryTreeNode n3 = n1.Right;
            n3.InsertLeft(5);
            n3.InsertRight(6);

            BinaryTreeNode n5 = n3.Left;
            n5.InsertRight(7);

            BinaryTreeNode n6 = n3.Right;
            n6.InsertRight(8);

            if (!pass)
            {
                BinaryTreeNode n4 = n2.Left;
                n4.InsertLeft(11);

                BinaryTreeNode n11 = n4.Left;
                n11.InsertRight(12);

                BinaryTreeNode n12 = n11.Right;
                n12.InsertRight(13);

                BinaryTreeNode n13 = n12.Right;
                n13.InsertRight(14);

                BinaryTreeNode n8 = n6.Right;
                n8.InsertLeft(9);

                BinaryTreeNode n9 = n8.Left;
                n9.InsertRight(10);

            }

            return n1;
        }
    }

    //shamelessly lifted from StackOverflow - https://stackoverflow.com/questions/36311991/c-sharp-display-a-binary-search-tree-in-console
    public static class BTreePrinter
    {
        class NodeInfo
        {
            public BinaryTreeNode Node;
            public string Text;
            public int StartPos;
            public int Size { get { return Text.Length; } }
            public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
            public NodeInfo Parent, Left, Right;
        }

        public static void Print(this BinaryTreeNode root, int topMargin = 2, int leftMargin = 2)
        {
            if (root == null) return;
            int rootTop = Console.CursorTop + topMargin;
            var last = new List<NodeInfo>();
            var next = root;
            for (int level = 0; next != null; level++)
            {
                var item = new NodeInfo { Node = next, Text = next.Value.ToString(" 0 ") };
                if (level < last.Count)
                {
                    item.StartPos = last[level].EndPos + 1;
                    last[level] = item;
                }
                else
                {
                    item.StartPos = leftMargin;
                    last.Add(item);
                }
                if (level > 0)
                {
                    item.Parent = last[level - 1];
                    if (next == item.Parent.Node.Left)
                    {
                        item.Parent.Left = item;
                        item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos);
                    }
                    else
                    {
                        item.Parent.Right = item;
                        item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos);
                    }
                }
                next = next.Left ?? next.Right;
                for (; next == null; item = item.Parent)
                {
                    Print(item, rootTop + 2 * level);
                    if (--level < 0) break;
                    if (item == item.Parent.Left)
                    {
                        item.Parent.StartPos = item.EndPos;
                        next = item.Parent.Node.Right;
                    }
                    else
                    {
                        if (item.Parent.Left == null)
                            item.Parent.EndPos = item.StartPos;
                        else
                            item.Parent.StartPos += (item.StartPos - item.Parent.EndPos) / 2;
                    }
                }
            }
            Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
        }

        private static void Print(NodeInfo item, int top)
        {
            SwapColors();
            Print(item.Text, top, item.StartPos);
            SwapColors();
            if (item.Left != null)
                PrintLink(top + 1, "┌", "┘", item.Left.StartPos + item.Left.Size / 2, item.StartPos);
            if (item.Right != null)
                PrintLink(top + 1, "└", "┐", item.EndPos - 1, item.Right.StartPos + item.Right.Size / 2);
        }

        private static void PrintLink(int top, string start, string end, int startPos, int endPos)
        {
            Print(start, top, startPos);
            Print("─", top, startPos + 1, endPos);
            Print(end, top, endPos);
        }

        private static void Print(string s, int top, int left, int right = -1)
        {
            Console.SetCursorPosition(left, top);
            if (right < 0) right = left + s.Length;
            while (Console.CursorLeft < right) Console.Write(s);
        }

        private static void SwapColors()
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = Console.BackgroundColor;
            Console.BackgroundColor = color;
        }
    }


}
