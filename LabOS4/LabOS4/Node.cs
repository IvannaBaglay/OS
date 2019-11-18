using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using LabOS4;
using MindFusion.Diagramming.Wpf;

namespace OSLab4.AVLTree
{
    public class AVLNode<TKey, TValue>
    {
        public AVLNode(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
            this.LeftChild = null;
            this.RightChild = null;
            this.Parent = null;
            this.Balance = 0;
        }

        public AVLNode(TKey key, TValue value, AVLNode<TKey, TValue> parent)
            : this(key, value)
        {
            this.Parent = parent;
        }

        public AVLNode(AVLNode<TKey, TValue> node)
            : this(node.Key, node.Value, node.Parent)
        {
            this.LeftChild = node.LeftChild;
            this.RightChild = node.RightChild;
        }

        public ShapeNode RBuildTree(Diagram parent)
        {
            ShapeNode node = new ShapeNode(parent)
            {
                Bounds = new Rect(new Point(), MainWindow.NodeSize),
                Text = Key.ToString(),
                Tag = this
            };
            parent.Nodes.Add(node);

            if (LeftChild != null)
            {
                var childNode = LeftChild.RBuildTree(parent);
                childNode.AttachTo(node, AttachToNode.TopLeft);


                var link = new DiagramLink(parent, node, childNode);
                parent.Links.Add(link);
            }

            if (RightChild != null)
            {
                var childNode = RightChild.RBuildTree(parent);
                childNode.AttachTo(node, AttachToNode.TopRight);

                var link = new DiagramLink(parent, node, childNode);
                parent.Links.Add(link);
            }

            return node;
        }

        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public AVLNode<TKey, TValue> Parent { get; set; }
        public AVLNode<TKey, TValue> LeftChild { get; set; }
        public AVLNode<TKey, TValue> RightChild { get; set; }
        public int Balance { get; set; }


        public override int GetHashCode()
        {
            return (this.Key.GetHashCode() << 16) | (this.Value.GetHashCode() & 0xFFFF);
        }

        //Two nodes are equal if their both keys and values match.
        public override bool Equals(object obj)
        {
            AVLNode<TKey, TValue> node = obj as AVLNode<TKey, TValue>;
            if (node == null)
            {
                return false;
            }
            else
            {
                if (this.Key.Equals(node.Key) && this.Value.Equals(node.Value))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public override string ToString()
        {
            return String.Format("({0} {1})", this.Key, this.Value);
        }
    }
}



