using System.Collections.Generic;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.VsMetrics
{
    public sealed class VsMetricsSettings : ToolSettings
    {
        public IEnumerable<DirectoryPath> AssemblyDirectories { get; set; }

        public IEnumerable<DirectoryPath> AssemblyPlatforms { get; set; }

        public IEnumerable<FilePath> AssemblyReferences { get; set; }

        public bool SearchGac { get; set; }

        public bool IgnoreInvalidTargets { get; set; }

        public bool IgnoreGeneratedCode { get; set; }

        public bool SuccessFile { get; set; }

        public bool Quiet { get; set; }
    }
}
