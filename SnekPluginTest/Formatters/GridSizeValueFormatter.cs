using FluentAssertions.Formatting;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPluginTest.Formatters;

public class GridSizeValueFormatter : IValueFormatter
{
    public bool CanHandle(object value)
    {
        return value is GridSize;
    }

    public void Format(object value, FormattedObjectGraph formattedGraph, FormattingContext context, FormatChild formatChild)
    {
        var gridSize = (GridSize) value;
        var stringToRender = $"{nameof(gridSize)}({gridSize.RowCount} rows, {gridSize.ColumnCount} columns)";

        if (context.UseLineBreaks)
        {
            formattedGraph.AddLine(stringToRender);
        }
        else
        {
            formattedGraph.AddFragment(stringToRender);
        }
    }
}