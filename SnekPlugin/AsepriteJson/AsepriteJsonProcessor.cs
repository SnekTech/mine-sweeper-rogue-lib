using System.Text.Json;

namespace SnekPlugin.AsepriteJson
{
    public static class AsepriteJsonProcessor
    {
        public static AsepriteJsonData FromJson(string json)
        {
            var aseprite = JsonSerializer.Deserialize<AsepriteJsonData>(json);
            return aseprite!;
        }
    }
}
