using System.Collections.Generic;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.VsMetrics
{
    public sealed class VsMetricsSettings : ToolSettings
    {
        public IEnumerable<FilePath> AssemblyDirectories { get; set; } = new FilePath[] { };

        public IEnumerable<FilePath> AssemblyPlatforms { get; set; } = new FilePath[] { };

        public IEnumerable<FilePath> AssemblyReferences { get; set; } = new FilePath[] { };

        public bool SearchGac { get; set; }

        public bool IgnoreInvalidTargets { get; set; }

        public bool IgnoreGeneratedCode { get; set; }

        public bool SuccessFile { get; set; }

        public bool Quiet { get; set; }
    }
}
