using System;
using System.Collections.Generic;

namespace SnekPlugin.MineSweeper.Cell.StateMachine;

public static class CellStateExtensions
{
    public static CellStateValue ToCellState(string cellEmoji)
    {
        if (!stateValueByEmoji.ContainsKey(cellEmoji))
        {
            throw new ArgumentException($"no matching state for {nameof(cellEmoji)}: {cellEmoji}");
        }

        return stateValueByEmoji[cellEmoji];
    }

    private static readonly Dictionary<string, CellStateValue> stateValueByEmoji =
        new Dictionary<string, CellStateValue>
        {
            {CellExtensions.Emoji.Covered, CellStateValue.Covered},
            {CellExtensions.Emoji.Flagged, CellStateValue.Flagged},
            {CellExtensions.Emoji.Revealed, CellStateValue.Revealed},
        };
}