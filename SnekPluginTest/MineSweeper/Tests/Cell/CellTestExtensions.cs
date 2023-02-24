using SnekPlugin.MineSweeper.Cell.StateMachine;

namespace SnekPluginTest.MineSweeper.Tests;

public static class CellTestExtensions
{

    public static string Emoji(this CellStateValue cellStateValue)
    {
        var cellEmojis = CellStateExtensions.CellEmojis;
        
        return cellStateValue switch
        {
            CellStateValue.Covered => cellEmojis.covered,
            CellStateValue.Flagged => cellEmojis.flagged,
            CellStateValue.Revealed => cellEmojis.revealed,
            _ => throw new ArgumentOutOfRangeException(nameof(cellStateValue), cellStateValue, null),
        };
    }

}