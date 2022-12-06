using System.Diagnostics.CodeAnalysis;

public class TestHelper : ITestHelper
{
    public void AssertNotNull([NotNull] object? arg)
    {
        Assert.NotNull(arg);
        if (arg is null)
        {
            throw new ArgumentNullException(nameof(arg), "Unexpected call from Assert.Multiple context");
        }
    }
}