namespace ProjectArchive
{
    internal class ProjectReflectionInfo(Type type, ProjectAttribute? details)
    {
        internal Type Type { get; } = type;
        internal ProjectAttribute? Details { get; } = details;
    }
}
