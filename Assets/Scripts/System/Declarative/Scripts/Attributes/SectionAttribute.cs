using JetBrains.Annotations;

namespace System.Declarative.Scripts.Attributes
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class SectionAttribute : Attribute
    {
    }
}