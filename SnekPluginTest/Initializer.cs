using System.Runtime.CompilerServices;
using SnekPluginTest.Formatters;

namespace SnekPluginTest;

public static class Initializer
{
    [ModuleInitializer]
    public static void SetDefaults()
    {
        FluentAssertions.Formatting.Formatter.AddFormatter(new GridIndexValueFormatter());
        FluentAssertions.Formatting.Formatter.AddFormatter(new GridSizeValueFormatter());
        FluentAssertions.Formatting.Formatter.AddFormatter(new IntMatrixValueFormatter());
    }
}