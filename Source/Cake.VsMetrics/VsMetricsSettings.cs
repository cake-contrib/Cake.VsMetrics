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
        /// Initializes a new instance of the <see cref="VsMetricsSettings"/> class.
        /// </summary>
        public VsMetricsSettings()
        {
            ToolVersion = VsMetricsToolVersion.Default;
        }

        /// <summary>
        /// Gets or sets the Assembly directory paths.
        /// </summary>
        /// <value>A collection of directory paths to assemblies.</value>
        public IEnumerable<DirectoryPath> AssemblyDirectories { get; set; }

        /// <summary>
        /// Gets or sets the Assembly platform paths.
        /// </summary>
        /// <value>A collection of directory paths to assembly platforms.</value>
        public IEnumerable<DirectoryPath> AssemblyPlatforms { get; set; }

        /// <summary>
        /// Gets or sets the Assembly references file paths.
        /// </summary>
        /// <value>A collection of file paths to assembly references.</value>
        public IEnumerable<FilePath> AssemblyReferences { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the GAC should be searched for assemblies.
        /// </summary>
        /// <value>The boolean value indicating if GAC should be searched for assemblies.</value>
        public bool SearchGac { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether invalid targets should be analysed.
        /// </summary>
        /// <value>The boolean value indicating if invalid targets should be ignored.</value>
        public bool IgnoreInvalidTargets { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether generated code should be analysed.
        /// </summary>
        /// <value>The boolean value indicating if generated code should be ignored.</value>
        public bool IgnoreGeneratedCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a success file should be created.
        /// </summary>
        /// <value>The boolean value indicating if a success file should be created.</value>
        public bool SuccessFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether metric.exe's output should be shown.
        /// </summary>
        /// <value>The boolean value indicating if metric.exe's output should be shown.</value>
        public bool Quiet { get; set; }

        /// <summary>
        /// Gets or sets the tool version.
        /// </summary>
        /// <value>The tool version.</value>
        public VsMetricsToolVersion ToolVersion { get; set; }
    }
}
