using FluentAssertions;
using FluentAssertions.Execution;
using SnekPlugin.Core.CustomExtensions;
using SnekPlugin.MineSweeper;
using SnekPlugin.MineSweeper.Grid;

namespace SnekPluginTest.MineSweeper.AssertExtensions;

public static class MatrixAssertExtensions
{
    public static MatrixAssertions<T> Should<T>(this T [,] matrix) where T : IComparable<T>
    {
        return new MatrixAssertions<T>(matrix);
    }
}

public class MatrixAssertions<T> where T : IComparable<T>
{
    private readonly T[,] _subjectMatrix;
    
    public MatrixAssertions(T[,] subjectMatrix)
    {
        _subjectMatrix = subjectMatrix;
    }

    [CustomAssertion]
    public AndConstraint<MatrixAssertions<T>> BeEquivalentTo(T[,] expected, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .ForCondition(_subjectMatrix.Size() == expected.Size())
            .FailWith("matrix size mismatch, subject has size {0}, but expects size {1}", _subjectMatrix.Size(),
                expected.Size())
            .Then
            .Given(() => _subjectMatrix.FindMissMatch(expected))
            .ForCondition(missMatchIndex => missMatchIndex == GridIndex.Invalid)
            .FailWith(
                "Expect {context} matrix {0} to match matrix {1}, but found element miss match at : {2}",
                _ => _subjectMatrix, _ => expected, missMatchIndex => missMatchIndex);

        return new AndConstraint<MatrixAssertions<T>>(this);
    }
}