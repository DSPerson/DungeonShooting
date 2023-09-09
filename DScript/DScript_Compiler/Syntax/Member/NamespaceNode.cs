﻿using System.Collections.Generic;

namespace DScript.Compiler
{
    /// <summary>
    /// 命名空间节点
    /// </summary>
    public class NamespaceNode : NodeBase
    {
        /// <summary>
        /// 根据命名空间名称创建或者获创建名空间对象
        /// </summary>
        /// <param name="syntaxTree">语法树对象</param>
        /// <param name="fullName">命名空间全称</param>
        public static NamespaceNode FromNamespace(SyntaxTree syntaxTree, string fullName)
        {
            var ns = fullName.Split('.');
            if (ns == null || ns.Length == 0)
            {
                return null;
            }

            NamespaceNode tempNode = syntaxTree.Root;
            string fName = null;
            for (int i = (ns[0] == tempNode.Name ? 1 : 0); i < ns.Length; i++)
            {
                var name = ns[i];
                fName = (fName == null) ? name : (fName + "." + name);
                var child = tempNode.GetChild(name);
                if (child == null)
                {
                    var namespaceNode = new NamespaceNode(fName, name, tempNode);
                    tempNode.AddChild(namespaceNode);
                    tempNode = namespaceNode;
                }
                else if (child is NamespaceNode namespaceNode)
                {
                    tempNode = namespaceNode;
                }
                else
                {
                    //已经声明了该名称的成员了, 不能再次声明为命名空间
                    throw new System.Exception("xxx");
                }
            }

            return tempNode;
        }

        /// <summary>
        /// 根据命名空间名称获取命名空间对象, 如果没有找到该命名空间, 则返回 null
        /// </summary>
        /// <param name="syntaxTree">语法树对象</param>
        /// <param name="fullName">命名空间全称</param>
        public static NamespaceNode GetNamespaceNode(SyntaxTree syntaxTree, string fullName)
        {
            var ns = fullName.Split('.');
            if (ns == null || ns.Length == 0)
            {
                return null;
            }

            NamespaceNode tempNode = syntaxTree.Root;
            string fName = null;
            for (int i = (ns[0] == tempNode.Name ? 1 : 0); i < ns.Length; i++)
            {
                var name = ns[i];
                fName = (fName == null) ? name : (fName + "." + name);
                var child = tempNode.GetChild(name);
                if (child is NamespaceNode namespaceNode)
                {
                    tempNode = namespaceNode;
                }
                else
                {
                    return null;
                }
            }

            return tempNode;
        }

        /// <summary>
        /// 命名空间包含的成员
        /// </summary>
        public readonly Dictionary<string, NodeBase> Children = new Dictionary<string, NodeBase>();

        /// <summary>
        /// 当前命名空间全名称
        /// </summary>
        public readonly string FullName;
        
        /// <summary>
        /// 父级
        /// </summary>
        public readonly NamespaceNode Parent;

        /// <summary>
        /// 不要直接使用构造创建 NamespaceNode, 请使用 NamespaceNode.FromNamespace()
        /// </summary>
        internal NamespaceNode(string fullName, string name, NamespaceNode parent) : base(name)
        {
            FullName = fullName;
            Parent = parent;
        }

        /// <summary>
        /// 往命名空间中添加成员
        /// </summary>
        /// <param name="nodeBase">成员对象</param>
        public void AddChild(NodeBase nodeBase)
        {
            if (Children.ContainsKey(nodeBase.Name))
            {
                //命名空间下已经包含相同的成员了
                throw new System.Exception("xxx");
            }
            Children.Add(nodeBase.Name, nodeBase);
        }
        
        /// <summary>
        /// 根据名称获取成员节点对象, 如果找不到指定成员, 就返回 null
        /// </summary>
        /// <param name="name">成员名称</param>
        public NodeBase GetChild(string name)
        {
            Children.TryGetValue(name, out var nodeBase);
            return nodeBase;
        }
    }
}