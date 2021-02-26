using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBotInstaller.AcfParsing
{
    public static class AcfParser
    {
        public static bool TryParseAcf(string[] fileLines, out AcfTree acfTree)
        {
            acfTree = null;
            AcfBranchNode currentParentNode = null;

            foreach (string line in fileLines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string trimmed = line.Trim();
                if (trimmed.StartsWith("\""))
                {
                    int keyQuotesEndIndex = trimmed.IndexOf('\"', 1);
                    if (keyQuotesEndIndex == -1)
                    {
                        acfTree = null;
                        return false;
                    }

                    string nodeKey = trimmed.Substring(1, keyQuotesEndIndex - 1); // Get the key between the quotes
                    trimmed = trimmed.Remove(0, nodeKey.Length + 2).Trim(); // Remove key and double quotes, and trim the result

                    if (string.IsNullOrWhiteSpace(trimmed)) // If the remaining line is empty, it is a branch node
                    {
                        if (acfTree == null)
                        {
                            acfTree = new AcfTree(nodeKey, null);
                            currentParentNode = acfTree;
                        }
                        else
                        {
                            AcfBranchNode branchNode = new AcfBranchNode(nodeKey, currentParentNode);
                            currentParentNode = branchNode;
                        }
                    }
                    else
                    {
                        int valueQuotesEndIndex = trimmed.IndexOf('\"', 1);
                        if (valueQuotesEndIndex == -1)
                        {
                            acfTree = null;
                            return false;
                        }

                        string nodeValue = trimmed.Substring(1, valueQuotesEndIndex - 1);

                        AcfValueNode valueNode = new AcfValueNode(nodeKey, nodeValue, currentParentNode);
                    }
                }
                else if (trimmed.StartsWith("}"))
                {
                    if (currentParentNode != null)
                        currentParentNode = currentParentNode.Parent;
                }
            }

            return true;
        }
    }
}
