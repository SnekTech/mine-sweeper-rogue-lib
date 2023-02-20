using System.Text;
using FluentAssertions.Formatting;
using SnekPlugin.Core.CustomExtensions;
using SnekPlugin.MineSweeper;

namespace SnekPluginTest.Formatters;

public class IntMatrixValueFormatter : IValueFormatter
{
    public bool CanHandle(object value)
    {
        return value is int[,];
    }

    public void Format(object value, FormattedObjectGraph formattedGraph, FormattingContext context, FormatChild formatChild)
    {
        var matrix = (int[,]) value;
        var matrixString = matrix.ToStringBinary();

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