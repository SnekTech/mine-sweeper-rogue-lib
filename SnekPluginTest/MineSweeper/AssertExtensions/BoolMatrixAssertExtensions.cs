using FluentAssertions;
using FluentAssertions.Execution;
using SnekPlugin.Core.CustomExtensions;
using SnekPlugin.MineSweeper;

namespace SnekPluginTest.MineSweeper.AssertExtensions;

public static class BoolMatrixAssertExtensions
{
    public static BoolMatrixAssertions Should(this BoolMatrix matrix)
    {
        return new BoolMatrixAssertions(matrix);
    }
}

public class BoolMatrixAssertions
{
    private readonly BoolMatrix _subject;
    
    public BoolMatrixAssertions(BoolMatrix matrix)
    {
        _subject = matrix;
    }

    [CustomAssertion]
    public AndConstraint<BoolMatrixAssertions> BeEquivalentTo(BoolMatrix expected, string because = "", params object[] becauseArgs)
    {
        var subjectMatrix = _subject.Matrix;
        var expectedMatrix = expected.Matrix;
        
        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .ForCondition(subjectMatrix.Size() == expectedMatrix.Size())
            .FailWith("BoolMatrix size mismatch, subject has size {0}, but expects size {1}", subjectMatrix.Size(),
                expectedMatrix.Size())
            .Then
            .Given(() => subjectMatrix.FindMissMatch(expectedMatrix))
            .ForCondition(missMatchIndex => missMatchIndex == (-1, -1))
            .FailWith(
                "Expect matrix {context} {0} to match matrix {1} {reason}, but found element miss match at : {2}",
                _ => _subject, _ => expected, missMatchIndex => missMatchIndex);

        return new AndConstraint<BoolMatrixAssertions>(this);
    }
}