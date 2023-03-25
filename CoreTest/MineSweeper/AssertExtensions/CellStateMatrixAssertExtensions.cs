using FluentAssertions;
using FluentAssertions.Execution;
using SnekPluginTest.MineSweeper.Tests;
using SnekTech.MineSweeperRogue.Core.CustomExtensions;

namespace SnekPluginTest.MineSweeper.AssertExtensions;

public static class CellStateMatrixAssertExtensions
{
    public static CellStateMatrixAssertions Should(this CellStateMatrix cellStateMatrix)
    {
        return new CellStateMatrixAssertions(cellStateMatrix);
    }
}

public class CellStateMatrixAssertions
{
    private readonly CellStateMatrix _subject;
    public CellStateMatrixAssertions(CellStateMatrix matrix)
    {
        _subject = matrix;
    }

    public AndConstraint<CellStateMatrixAssertions> BeEqualTo(CellStateMatrix expected, string because = "",
        params object[] becauseArgs)
    {
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(_subject.Matrix.Size() == expected.Matrix.Size())
            .FailWith("{context} size miss match")
            .Then
            .BecauseOf(because, becauseArgs)
            .ForCondition(FindMissMatch(_subject, expected) == (-1, -1))
            .FailWith("expect matrix like :{0}, but got: {1}", expected, _subject);

        return new AndConstraint<CellStateMatrixAssertions>(this);
    }

    private static (int i, int j) FindMissMatch(CellStateMatrix self, CellStateMatrix target)
    {
        foreach (var (i, j, selfValue) in self.GetEnumerable())
        {
            if (selfValue != target[i, j])
            {
                return (i, j);
            }
        }
        
        return (-1, -1);
    }
}