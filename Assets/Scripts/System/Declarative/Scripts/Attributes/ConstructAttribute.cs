using JetBrains.Annotations;

namespace System.Declarative.Scripts.Attributes
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ConstructAttribute : Attribute
    {
    }
}