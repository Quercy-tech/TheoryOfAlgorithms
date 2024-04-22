using System;

namespace Laba7Console
{
    class Node
    {
        public int Power { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(int power)
        {
            Power = power;
            Left = null;
            Right = null;
        }
    }

    class BinarySearchTree
    {
        private Node root;

        public void Insert(int power)
        {
            Console.WriteLine($"root : {root?.Power}");
            root = Insert(root, power);
        }

        private Node Insert(Node node, int power)
        {
            if (node == null)
            {
                Console.WriteLine($"Creating a device with {power} power");
                return Insert(new Node(power), power);
            }

            if (power < node.Power)
            {
                Console.WriteLine($"<- {node.Power}");
                node.Left = Insert(node.Left, power);
            }
            else if (power > node.Power)
            {
                Console.WriteLine($"{node.Power} -> ");
                node.Right = Insert(node.Right, power);
            }

            return node;
        }

        public Node Find(int power)
        {
            return Find(root, power);
        }

        private Node Find(Node node, int power)
        {
            if (node == null)
            {
                Console.WriteLine($"No device found, creating a new device with power {power}");
                return Insert(node,power);
                
            }

            if (node.Power == power)
            {
                Console.WriteLine($"Device with power {power} found");
                return node;
            }

            

            if (power < node.Power)
            {
                Console.WriteLine($"<- {node.Power}");
                return Find(node.Left, power);
            }
            else
            {
                Console.WriteLine($"{node.Power} -> ");
                return Find(node.Right, power);
            }
        }


    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var bst = new BinarySearchTree();

            bst.Insert(1000);
            bst.Insert(500);
            bst.Insert(1500);

            Console.WriteLine("Enter required power for the robot:");
            int requiredPower = int.Parse(Console.ReadLine());

            var device = bst.Find(requiredPower);


            if (device != null) Console.WriteLine("Found suitable device with power: " + device.Power);

        }
    }
}
