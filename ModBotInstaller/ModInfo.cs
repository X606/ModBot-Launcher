using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBotInstaller
{
    [Serializable]
    public class ModInfo
    {
        [JsonRequired]
        [JsonProperty]
        public string DisplayName { get; internal set; }

        [JsonRequired]
        [JsonProperty]
        public string UniqueID { get; internal set; }

        [JsonRequired]
        [JsonProperty]
        public string MainDLLFileName { get; internal set; }

        [JsonRequired]
        [JsonProperty]
        public string Author { get; internal set; }

        [JsonRequired]
        [JsonProperty]
        public uint Version { get; internal set; }

        [JsonProperty]
        public string ImageFileName { get; internal set; }

        [JsonProperty]
        public string Description { get; internal set; }

        [JsonProperty]
        public string[] ModDependencies { get; internal set; }

        [JsonProperty]
        public string[] Tags { get; internal set; }

        [JsonProperty]
        public PersistentFolderData[] PersistentFolders { get; internal set; }

        [JsonIgnore]
        public string ModFolderPath;
    }
}
