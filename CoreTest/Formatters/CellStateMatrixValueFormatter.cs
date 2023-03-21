using FluentAssertions.Formatting;
using SnekPluginTest.MineSweeper.Tests;

namespace SnekPluginTest.Formatters;

public class CellStateMatrixValueFormatter : IValueFormatter
{
    public bool CanHandle(object value)
    {
        return value is CellStateMatrix;
    }

    public void Format(object value, FormattedObjectGraph formattedGraph, FormattingContext context, FormatChild formatChild)
    {
        var cellStateMatrix = (CellStateMatrix) value;
        var matrixString = cellStateMatrix.Format();

        if (context.UseLineBreaks)
        {
            formattedGraph.AddLine(matrixString);
        }
        else
        {
            formattedGraph.AddFragment(matrixString);
        }
    }
    
}