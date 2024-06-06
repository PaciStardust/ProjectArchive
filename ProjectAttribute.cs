namespace ProjectArchive
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class ProjectAttribute(string name, ulong timeStart, ulong timeEnd, string description) : Attribute
    {
        internal string Name { get; } = name;
        internal ulong TimeStart { get; } = timeStart;
        internal ulong TimeEnd { get; } = timeEnd;
        internal string Description { get; } = description;
    }
}
