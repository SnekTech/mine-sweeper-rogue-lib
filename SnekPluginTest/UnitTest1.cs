using FluentAssertions;
using SnekPlugin.AsepriteJson;

namespace SnekPluginTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        const string jsonPath =
            "D:\\AsgardProjects\\GameAssets\\AsepriteWorkSpace\\mine-sweeper-rogue\\flag\\flag-anim.json";

        string jsonStr = File.ReadAllText(jsonPath);
        var aseprite = AsepriteJsonProcessor.FromJson(jsonStr);

        aseprite.Should().NotBeNull();
        aseprite.Meta.FrameTags.Should().NotBeNull();
    }
}