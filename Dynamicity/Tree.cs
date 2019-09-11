using System;
using System.Collections.Generic;

namespace DBT.Dynamicity
{
    public class Tree<T> : ICloneable where T : IHasParents<T>
    {
        protected readonly List<Node<T>> nodes = new List<Node<T>>();
        protected readonly Dictionary<T, Node<T>> itemsToNodes = new Dictionary<T, Node<T>>();

        public Tree(IEnumerable<T> items)
        {
            Nodify(items);
        }

        private void Nodify(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                Node<T> node = new Node<T>(item);

                nodes.Add(node);
                itemsToNodes.Add(item, node);
            }

            foreach (Node<T> node in nodes)
            {
                for (int i = 0; i < node.Value.Parents.Length; i++)
                    node.AddParent(itemsToNodes[node.Value.Parents[i]]);
            }
        }

        public object Clone() => MemberwiseClone();

        public IReadOnlyList<Node<T>> Nodes => nodes.AsReadOnly();
    }
}
