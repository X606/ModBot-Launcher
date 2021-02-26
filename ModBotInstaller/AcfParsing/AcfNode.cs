namespace ModBotInstaller.AcfParsing
{
    public class AcfNode
    {
        public readonly string Key;
        public readonly AcfBranchNode Parent;

        public AcfNode(string key, AcfBranchNode parent)
        {
            Key = key;

            Parent = parent;
            if (parent != null)
                parent.AddChild(this);
        }
    }
}
