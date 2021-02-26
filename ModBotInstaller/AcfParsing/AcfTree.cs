namespace ModBotInstaller.AcfParsing
{
    public class AcfTree : AcfBranchNode
    {
        public AcfTree(string key, AcfBranchNode parent) : base(key, parent)
        {
        }

        public bool TryGetNodeValue(string nodeKeyPath, out string value)
        {
            AcfNode currentNode = this;

            string[] keys = nodeKeyPath.Split('/');
            foreach (string nodeKey in keys)
            {
                if (currentNode is AcfBranchNode branchNode && branchNode.TryGetChildWithKey(nodeKey, out AcfNode childNode))
                {
                    currentNode = childNode;
                }
                else
                {
                    value = null;
                    return false;
                }
            }

            if (currentNode is AcfValueNode valueNode)
            {
                value = valueNode.Value;
                return true;
            }

            value = null;
            return false;
        }
    }
}
