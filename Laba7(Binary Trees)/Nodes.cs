using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Laba7_Binary_Trees_
{

    // Вузли червоно-чорного дерева


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

