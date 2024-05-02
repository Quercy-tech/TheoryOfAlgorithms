using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Laba7_Binary_Trees_
{
    internal class RBNode
    {
        public int Value { get; set; } // Потужність
        public bool IsRed { get; set; } // Колір вузола
        public RBNode LeftChild { get; set; }
        public RBNode RightChild { get; set; }

        public int height; // Висота піддерева
        public RBNode(int value, bool isRed)
        {
            Value = value;
            IsRed = isRed;
            height = 1;
        }
    }


    // Вузли АВЛ-дерева
    internal class AVLNode
    {
        public int value; // Потужність
        public int height;  // Висота піддерева
        public AVLNode left;
        public AVLNode right;

        public AVLNode(int value)
        {
            this.value = value;
            height = 1;
        }
    }
}

