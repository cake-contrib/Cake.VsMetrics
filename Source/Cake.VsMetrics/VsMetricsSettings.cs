using System.Collections.Generic;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.VsMetrics
{
    /// <summary>
    /// Contains settings used by <see cref="VsMetricsRunner"/>.
    /// </summary>
    public sealed class VsMetricsSettings : ToolSettings
    {
        /// <summary>
        /// Gets or sets the Assembly directory paths.
        /// </summary>
        public IEnumerable<DirectoryPath> AssemblyDirectories { get; set; }

        /// <summary>
        /// Gets or sets the Assembly platform paths.
        /// </summary>
        public IEnumerable<DirectoryPath> AssemblyPlatforms { get; set; }

        /// <summary>
        /// Gets or sets the Assembly references file paths.
        /// </summary>
        public IEnumerable<FilePath> AssemblyReferences { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the GAC should be searched for assembleis
        /// </summary>
        public bool SearchGac { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether invalid targets should be analysed
        /// </summary>
        public bool IgnoreInvalidTargets { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether generated code should be analysed
        /// </summary>
        public bool IgnoreGeneratedCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a success file should be created
        /// </summary>
        public bool SuccessFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether metric.exe's output should be shown
        /// </summary>
        public bool Quiet { get; set; }
    }
}
