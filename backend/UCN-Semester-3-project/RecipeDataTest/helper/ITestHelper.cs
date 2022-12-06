using System.Diagnostics.CodeAnalysis;

public interface ITestHelper{
    void AssertNotNull([NotNull] Object? arg);
}