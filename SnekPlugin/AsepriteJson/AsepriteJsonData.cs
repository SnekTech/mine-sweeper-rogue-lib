using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SnekPlugin.AsepriteJson
{
    public class AsepriteJsonData
    {
        [JsonPropertyName("meta")]
        public Meta Meta { get; set; } = null!;
    }

    public class Meta
    {
        [JsonPropertyName("app")]
        public string App { get; set; } = null!;

        [JsonPropertyName("version")]
        public string Version { get; set; } = null!;

        [JsonPropertyName("frameTags")]
        public List<FrameTag> FrameTags { get; set; } = null!;
    }

    public class FrameTag
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("from")]
        public int FromIndex { get; set; }

        [JsonPropertyName("to")]
        public int ToIndex { get; set; }

        [JsonPropertyName("direction")]
        public string Direction { get; set; } = null!;
    }
}