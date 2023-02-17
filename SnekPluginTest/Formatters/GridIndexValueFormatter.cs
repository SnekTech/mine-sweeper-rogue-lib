using FluentAssertions.Formatting;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPluginTest.Formatters;

public class GridIndexValueFormatter : IValueFormatter
{
    public bool CanHandle(object value)
    {
        return value is GridIndex;
    }

    public void Format(object value, FormattedObjectGraph formattedGraph, FormattingContext context, FormatChild formatChild)
    {
        var gridIndex = (GridIndex) value;
        var stringToRender = $"{nameof(gridIndex)}(i: {gridIndex.RowIndex}, j: {gridIndex.ColumnIndex})";

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