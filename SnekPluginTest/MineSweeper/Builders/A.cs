using SnekPluginTest.MineSweeper.Builders.Mock;

namespace SnekPluginTest.MineSweeper.Builders;

public static class A
{
    public static IChooseBetweenArrayOrGridDataStage BombMatrix => new BombMatrixBuilder();
    public static GridBuilder Grid => new GridBuilder();
    public static ISetParentMatrixStage Cell => new CellBuilder();
    
    public static MockGridDataBuilder MockGridDataBuilder => new MockGridDataBuilder();
    public static MockBombGeneratorBuilder MockBombGeneratorBuilder => new MockBombGeneratorBuilder();
    public static MockHumbleGridBuilder MockHumbleGridBuilder => new MockHumbleGridBuilder();
    public static MockHumbleCellBuilder MockHumbleCellBuilder => new MockHumbleCellBuilder();
}