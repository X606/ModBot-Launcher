using System.Collections.Generic;

namespace ModBotInstaller.AcfParsing
{
    public class AcfBranchNode : AcfNode
    {
        public readonly List<AcfNode> Children;

        public AcfBranchNode(string key, AcfBranchNode parent) : base(key, parent)
        {
            Children = new List<AcfNode>();
        }

        public bool TryGetChildWithKey(string key, out AcfNode child)
        {
            foreach (AcfNode childNode in Children)
            {
                if (childNode.Key == key)
                {
                    child = childNode;
                    return true;
                }
            }

            child = null;
            return false;
        }

        public void AddChild(AcfNode node)
        {
            Children.Add(node);
        }
    }
}
