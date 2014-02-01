﻿#region usings

using System.Collections.Generic;

#endregion

namespace nsplit.CodeAnalyzis.DataStructures.TypeTree
{
    internal class Tree
    {
        private readonly NodeFactory m_NodeFactory;

        private readonly Node m_Root;

        public Tree(NodeFactory nodeFactory)
        {
            m_NodeFactory = nodeFactory;
            m_Root = m_NodeFactory.CreateNode(string.Empty, null);
        }

        public INode Add(string fullName)
        {
            var qualifiedName = QualifiedName.Parse(fullName);
            var names = new Queue<string>(qualifiedName.Nodes);
            var node = m_Root.GetOrCreate(names, m_NodeFactory);
            var leaf = m_NodeFactory.CreateLeaf(qualifiedName.Leaf, node);
            node.AddLeaf(leaf);
            return leaf;
        }

        public bool TryGet(string fullName, out INode leaf)
        {
            var qualifiedName = QualifiedName.Parse(fullName);
            var names = new Queue<string>(qualifiedName.Nodes);
            Node node;
            bool nodeFound = m_Root.TryGetNode(names, out node);
            if (!nodeFound)
            {
                leaf = null;
                return false;
            }

            Leaf result;
            bool isOk = node.TryGetLeaf(qualifiedName.Leaf, out result);
            leaf = result;
            return isOk;
        }

        public int Count
        {
            get
            {
                return m_NodeFactory.Count;
            }
        }

        public IEnumerable<INode> Nodes
        {
            get { return m_NodeFactory.Nodes; }
        }
    }
}