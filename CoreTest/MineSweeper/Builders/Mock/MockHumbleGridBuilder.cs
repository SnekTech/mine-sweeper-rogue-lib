﻿using NSubstitute;
using SnekTech.MineSweeperRogue.GridSystem;
using SnekTech.MineSweeperRogue.GridSystem.CellSystem;

namespace SnekPluginTest.MineSweeper.Builders.Mock;

public class MockHumbleGridBuilder
{
    private Func<IHumbleCell> _humbleCellProvider = () => A.MockHumbleCellBuilder.Build();

    public MockHumbleGridBuilder WithHumbleCellProvider(Func<IHumbleCell> newHumbleCellProvider)
    {
        _humbleCellProvider = newHumbleCellProvider;
        return this;
    }

    public IHumbleGrid Build()
    {
        var mock = Substitute.For<IHumbleGrid>();


        mock.InstantiateHumbleCells(Arg.Any<int>()).Returns(info => CreateHumbleCellsOfNumber(info.Arg<int>()));

        return mock;
    }

    private List<IHumbleCell> CreateHumbleCellsOfNumber(int n)
    {
        var humbleCells = new List<IHumbleCell>();
        humbleCells.Clear();

        for (var i = 0; i < n; i++)
        {
            var mockHumbleCell = _humbleCellProvider();
            humbleCells.Add(mockHumbleCell);
        }

        return humbleCells;
    }
}