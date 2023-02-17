using SnekPluginTest.MineSweeper.Builders.Mock;

namespace SnekPluginTest.MineSweeper.Builders;

public static class A
{
    public static GridBuilder Grid => new GridBuilder();
    public static BombMatrixBuilder BombMatrix => new BombMatrixBuilder();
    
    public static MockGridDataBuilder MockGridDataBuilder => new MockGridDataBuilder();
    public static MockBombGeneratorBuilder MockBombGeneratorBuilder => new MockBombGeneratorBuilder();
    public static MockHumbleGridBuilder MockHumbleGridBuilder => new MockHumbleGridBuilder();
    public static MockHumbleCellBuilder MockHumbleCellBuilder => new MockHumbleCellBuilder();
}