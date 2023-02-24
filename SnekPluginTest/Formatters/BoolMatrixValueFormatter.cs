using System.Text;
using FluentAssertions.Formatting;
using SnekPlugin.Core.CustomExtensions;
using SnekPlugin.MineSweeper;

namespace SnekPluginTest.Formatters;

public class BoolMatrixValueFormatter : IValueFormatter
{
    public bool CanHandle(object value)
    {
        return value is BoolMatrix;
    }

    public void Format(object value, FormattedObjectGraph formattedGraph, FormattingContext context, FormatChild formatChild)
    {
        var boolMatrix = (BoolMatrix) value;
        var matrixString = boolMatrix.ToString();

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