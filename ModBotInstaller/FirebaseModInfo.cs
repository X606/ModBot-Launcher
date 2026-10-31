using Newtonsoft.Json;
using System;

namespace ModBotInstaller
{
    [Serializable]
    public class FirebaseModInfo
    {
        [JsonProperty("checked")]
        public bool Checked;

        [JsonProperty("creatorID")]
        public string Creator;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("downloadLink")]
        public string DownloadLink;

        [JsonProperty("imageLink")]
        public string ImageDownloadLink;

        [JsonProperty("name")]
        public string ModName;

        [JsonProperty("uniqueId")]
        public string ModID;
    }
}
