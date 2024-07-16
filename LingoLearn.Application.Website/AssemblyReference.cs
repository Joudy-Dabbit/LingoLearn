using System.Reflection;

namespace LingoLearn.Application.Website;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}