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
                int count = 0;
                bool result = CheckForSuperBalance(tree, ref count);

                Console.WriteLine("Tree result: " + result.ToString());
                Console.WriteLine("Number of nodes checked: " + count.ToString());


                Console.WriteLine("Try again? TRUE or FALSE");

                correctInput = false;
                while (!correctInput)
                {
                    correctInput = bool.TryParse(Console.ReadLine(), out repeat);
                }
            }
        }

        private static bool CheckForSuperBalance(BinaryTreeNode tree, ref int count)
        {
            count = 0;
            List<int> depths = new List<int>();
            CheckIfLeaf(tree, 0, ref depths, ref count);
            bool result;
            if (depths.Count == 0)
                result = true;
            else
            {
                result = CheckLeafDepthMinMax(depths);
            }
            return result;
        }

        private static void CheckIfLeaf(BinaryTreeNode n, int depth, ref List<int> depths, ref int count)
        {
            count++;
            
            //check if node is leaf
            if ((n.Left == null) && (n.Right == null))
            {
                if (!depths.Contains(depth))
                    depths.Add(depth);

                //new depth added so check if our fail condition has been met
                if (!CheckLeafDepthMinMax(depths))
                    return;

            }
            else
            {
                if (n.Left != null)
                    CheckIfLeaf(n.Left, depth + 1, ref depths, ref count);

                //check if our fail condition has been met when we traversed the left path
                if (!CheckLeafDepthMinMax(depths))
                    return;

                if (n.Right != null)
                    CheckIfLeaf(n.Right, depth + 1, ref depths, ref count);
            }
        }

        private static bool CheckLeafDepthMinMax(List<int> depths)
        {
            int min, max;
            if (depths.Count == 0)
            {
                min = 0;
                max = 0;
            }
            else
            {
                min = depths.Min();
                max = depths.Max();
            }
            return ((max - min == 0) || (max - min == 1));
        }
    }
}
