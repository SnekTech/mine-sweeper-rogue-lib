using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SnekTech.MineSweeperRogue.GridSystem.CellSystem;

namespace SnekTech.MineSweeperRogue.GridSystem;

public static class GridExtensions
{
    public static (int rows, int columns) GetDimension(IReadOnlyList<string> gridRows)
    {
        var rows = gridRows.Count;
        if (rows == 0)
        {
            return (0, 0);
        }

        var firstUnit = StringInfo.GetNextTextElement(gridRows[0]);
        var emojiLength = firstUnit.Length;
        var columns = gridRows[0].Length / emojiLength;

        return (rows, columns);
    }

    public static CellStateValue[,] CreateStateMatrix(string[] gridRows)
    {
        var (rows, columns) = GetDimension(gridRows);
        if (rows == 0 || columns == 0)
        {
            throw new ArgumentException("cannot create cell state matrix from empty gridRows");
        }

        var result = new CellStateValue[rows, columns];

        for (var i = 0; i < rows; i++)
        {
            var gridRow = gridRows[i];
            var emojiRow = SplitEmojiRow(gridRow);

            for (var j = 0; j < columns; j++)
            {
                var emoji = emojiRow[j];
                result[i, j] = CellExtensions.ToCellState(emoji);
            }
        }

        return result;
    }

    public static string[] SplitEmojiRow(string str)
    {
        IEnumerable<string> Iterate()
        {
            for (var en = StringInfo.GetTextElementEnumerator(str); en.MoveNext();)
                yield return en.GetTextElement();
        }

        return Iterate().ToArray();
    }
}