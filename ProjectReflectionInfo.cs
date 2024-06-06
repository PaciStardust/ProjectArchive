using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectArchive
{
    internal class ProjectReflectionInfo(Type type, ProjectAttribute? details)
    {
        internal Type Type { get; } = type;
        internal ProjectAttribute? Details { get; } = details;
    }
}
