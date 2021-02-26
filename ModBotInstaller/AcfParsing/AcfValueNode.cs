namespace ModBotInstaller.AcfParsing
{
    public class AcfValueNode : AcfNode
    {
        public readonly string Value;

        public AcfValueNode(string key, string value, AcfBranchNode parent) : base(key, parent)
        {
            Value = value;
        }
    }
}
