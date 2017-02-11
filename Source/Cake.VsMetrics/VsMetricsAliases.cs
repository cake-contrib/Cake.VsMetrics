using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.VsMetrics
{
    /// <summary>
    /// <para>Contains functionality related to Visual studio's metrics.exe command line tool.</para>
    /// <para>
    /// In order to use the commands for this addin, you will need to register metrics.exe in your cake build file after you have installed the metrics power tool:
    /// <code>
    /// Setup(context => {
    ///     context.Tools.RegisterFile("C:/Program Files (x86)/Microsoft Visual Studio 14.0/Team Tools/Static Analysis Tools/FxCop/metrics.exe");
    /// });
    /// </code>
    /// In addition, you will need to include the following:
    /// <code>
    /// #addin Cake.VsMetrics
    /// </code>
    /// </para>
    /// </summary>
    [CakeAliasCategory("VsMetrics")]
    public static class VsMetricsAliases
    {
        /// <summary>
        /// Runs metrics.exe against the specified collection of input FilePath, and outputs to specified output FilePath
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="inputFilePaths">The input file paths.</param>
        /// <param name="outputFilePath">The output file path.</param>
        /// <example>
        /// <code>
        /// var projects = GetFiles("./bin/Debug/*.dll");
        /// VsMetrics(projects, "metrics_result.xml");
        /// </code>
        /// </example>
        [CakeMethodAlias]
        public static void VsMetrics(this ICakeContext context, IEnumerable<FilePath> inputFilePaths, FilePath outputFilePath)
        {
            VsMetrics(context, inputFilePaths, outputFilePath, new VsMetricsSettings());
        }

        /// <summary>
        /// Runs metrics.exe against the specified collection of input FilePath, and outputs to specified output FilePath with the specified VsMetricsSettings
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="inputFilePaths">The input file paths.</param>
        /// <param name="outputFilePath">The output file path.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        /// var projects = GetFiles("./bin/Debug/*.dll");
        /// var settings = new VsMetricsSettings()
        /// {
        ///     SuccessFile = true,
        ///     IgnoreGeneratedCode = true,
        /// };
        ///
        /// VsMetrics(projects, "metrics_result.xml", settings);
        /// </code>
        /// </example>
        [CakeMethodAlias]
        public static void VsMetrics(this ICakeContext context, IEnumerable<FilePath> inputFilePaths, FilePath outputFilePath, VsMetricsSettings settings)
        {
            Check.RequireNonNull(context, nameof(context));

            var runner = new VsMetricsRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Run(inputFilePaths, outputFilePath, settings);
        }
    }
}