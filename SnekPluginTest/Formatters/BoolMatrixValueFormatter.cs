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
        var matrixString = ToStringBinary(boolMatrix);

        if (context.UseLineBreaks)
        {
            formattedGraph.AddLine(matrixString);
        }
        else
        {
            formattedGraph.AddFragment(matrixString);
        }
    }

    public static string ToStringBinary(BoolMatrix matrix, char truthyChar = '✅', char falsyChar = '❌')
    {
        var sBuilder = new StringBuilder("\n");
        var (rows, columns) = matrix.Matrix.Size();
        
        
        for (var i = 0; i < rows; i++)
        {
            sBuilder.Append($"{i}");
            for (var j = 0; j < columns; j++)
            {
                sBuilder.Append(matrix[i, j] ? truthyChar : falsyChar);
            }

            sBuilder.Append('\n');
        }

        return sBuilder.ToString();
    }
}