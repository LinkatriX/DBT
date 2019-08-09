using System;
using System.Collections.Generic;

namespace DBT.Dynamicity
{
    public class Node<T> : ICloneable where T : IHasParents<T>
    {
        protected readonly List<Node<T>> parents = new List<Node<T>>();
        protected readonly List<Node<T>> children = new List<Node<T>>();

        public Node(T value)
        {
            Value = value;
        }

        internal void AddParent(Node<T> parent)
        {
            parents.Add(parent);
            parent.children.Add(this);
        }

        private void RecursiveAction(Node<T> node, Action<Node<T>> action)
        {
            for (int i = 0; i < node.children.Count; i++)
            {
                action(node.children[i]);
                RecursiveAction(node.children[i], action);
            }
        }

        public object Clone() => MemberwiseClone();

        public T Value { get; }

        public IReadOnlyList<Node<T>> Parents => parents.AsReadOnly();
        public IReadOnlyList<Node<T>> Children => children.AsReadOnly();
    }
}
