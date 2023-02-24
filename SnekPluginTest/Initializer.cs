using System.Runtime.CompilerServices;
using SnekPluginTest.Formatters;

namespace SnekPluginTest;

public static class Initializer
{
    [ModuleInitializer]
    public static void SetDefaults()
    {
        FluentAssertions.Formatting.Formatter.AddFormatter(new CellStateMatrixValueFormatter());
    }
}