using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Console app will create a tree and then check whether it is superbalanced");

            bool repeat = true;
            while (repeat)
            {
                Console.WriteLine("Tree data will be initialised. Should tree pass check? TRUE or FALSE");

                bool correctInput = false;
                bool pass = true;
                while (!correctInput)
                {
                    correctInput = bool.TryParse(Console.ReadLine(), out pass);
                }

                var tree = BinaryTreeNode.InitialiseWithData(pass);
                Console.WriteLine("Data initialised");
                BTreePrinter.Print(tree);

                Console.WriteLine("Checking whether tree is superbalanced");
                bool result = CheckForSuperBalance(tree);

                Console.WriteLine("Tree result: " + result.ToString());

                Console.WriteLine("Try again? TRUE or FALSE");

                correctInput = false;
                while (!correctInput)
                {
                    correctInput = bool.TryParse(Console.ReadLine(), out repeat);
                }
            }
        }

        private static bool CheckForSuperBalance(BinaryTreeNode tree)
        {
            List<int> depths = new List<int>();
            CheckIfLeaf(tree, 0, ref depths);
            bool result;
            if (depths.Count == 0)
                result = true;
            else
            {
                int min = depths.Min();
                int max = depths.Max();
                result = ((max - min == 0) || (max - min == 1));
            }
            return result;
        }

        private static void CheckIfLeaf(BinaryTreeNode n, int depth, ref List<int> depths)
        {
            //check if node is leaf
            if ((n.Left == null) && (n.Right == null))
            {
                if (!depths.Contains(depth))
                    depths.Add(depth);
            }
            else
            {
                if (n.Left != null)
                    CheckIfLeaf(n.Left, depth + 1, ref depths);
                if (n.Right != null)
                    CheckIfLeaf(n.Right, depth + 1, ref depths);
            }
        }
    }
}
