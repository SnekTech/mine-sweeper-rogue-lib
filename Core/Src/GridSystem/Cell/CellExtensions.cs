using System;
using System.Collections.Generic;
using MineSweeperRogue.GridSystem.Cell.StateMachine;

namespace MineSweeperRogue.GridSystem.Cell;

public static class CellExtensions
{
    public static class Emoji
    {
        public const string Covered = "🔳";
        public const string Flagged = "⛳";
        public const string Revealed = "💢";
        public const string Bomb = "💣";
    }
    
    public static CellStateValue ToCellState(string cellEmoji)
    {
        if (!stateValueByEmoji.ContainsKey(cellEmoji))
        {
            throw new ArgumentException($"no matching state for {nameof(cellEmoji)}: {cellEmoji}");
        }

        return stateValueByEmoji[cellEmoji];
    }

    public static string ToEmoji(this CellStateValue state)
    {
        if (!emojiByStateValue.ContainsKey(state))
        {
            throw new ArgumentException($"no matching emoji for {nameof(state)}: {state}");
        }

        return emojiByStateValue[state];
    }

    private static readonly Dictionary<string, CellStateValue> stateValueByEmoji =
        new Dictionary<string, CellStateValue>
        {
            {Emoji.Covered, CellStateValue.Covered},
            {Emoji.Flagged, CellStateValue.Flagged},
            {Emoji.Revealed, CellStateValue.Revealed},
        };

    private static readonly Dictionary<CellStateValue, string> emojiByStateValue =
        new Dictionary<CellStateValue, string>
        {
            {CellStateValue.Covered, Emoji.Covered},
            {CellStateValue.Flagged, Emoji.Flagged},
            {CellStateValue.Revealed, Emoji.Revealed},
        };
}