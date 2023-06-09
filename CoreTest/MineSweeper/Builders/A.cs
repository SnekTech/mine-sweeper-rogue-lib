﻿using SnekPluginTest.MineSweeper.Builders.Mock;

namespace SnekPluginTest.MineSweeper.Builders;

public static class A
{
    public static IChooseBetweenBoolArrayOrGridDataStage BombMatrix => new BombMatrixBuilder();
    public static GridBuilder GridBuilder => new GridBuilder();
    public static ISetCellBuilderFirstStage CellBuilder => new CellBuilder();
    
    public static MockGridDataBuilder MockGridDataBuilder => new MockGridDataBuilder();
    public static MockBombGeneratorBuilder MockBombGeneratorBuilder => new MockBombGeneratorBuilder();
    public static MockHumbleGridBuilder MockHumbleGridBuilder => new MockHumbleGridBuilder();
    public static MockHumbleCellBuilder MockHumbleCellBuilder => new MockHumbleCellBuilder();
}