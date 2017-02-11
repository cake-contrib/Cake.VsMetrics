using System.Collections.Generic;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.VsMetrics
{
    /// <summary>
    /// The VsMetrics runner.
    /// </summary>
    public sealed class VsMetricsRunner : Tool<VsMetricsSettings>
    {
        private readonly ICakeEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="VsMetricsRunner" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="processRunner">The process runner.</param>
        /// <param name="tools">The tool locator</param>
        public VsMetricsRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
            _environment = environment;
        }

        // TODO fw
        // - Tests
        // - Cake nuget automation
        // - Can I make it easier to resolve the metrics path?

        /// <summary>
        /// Calculates code metrics for a collection of input files and saves the report in an output file.
        /// </summary>
        /// <param name="inputFilePaths">The input filepaths.</param>
        /// <param name="outputFilePath">The output file path.</param>
        /// <param name="settings">The settings.</param>
        public void Run(IEnumerable<FilePath> inputFilePaths, FilePath outputFilePath, VsMetricsSettings settings)
        {
            Check.RequireNonNull(inputFilePaths, nameof(inputFilePaths));
            Check.RequireNonNull(outputFilePath, nameof(outputFilePath));
            settings = settings ?? new VsMetricsSettings();

            Run(settings, GetArguments(inputFilePaths, outputFilePath, settings));
        }

        /// <summary>
        /// Gets the name of the tool.
        /// </summary>
        /// <returns>The name of the tool.</returns>
        protected override string GetToolName()
        {
            return "Metrics";
        }

        /// <summary>
        /// Gets the name of the tool executable.
        /// </summary>
        /// <returns>The tool executable name.</returns>
        protected override IEnumerable<string> GetToolExecutableNames()
        {
            return new[] { "metrics.exe" };
        }

        private ProcessArgumentBuilder GetArguments(IEnumerable<FilePath> inputFilePaths, FilePath outputFilePath, VsMetricsSettings settings)
        {
            var builder = new ProcessArgumentBuilder();

            Append(builder, "/f:", inputFilePaths);
            Append(builder, "/o:", outputFilePath);
            Append(builder, "/d:", settings.AssemblyDirectories);
            Append(builder, "/plat:", settings.AssemblyPlatforms);
            Append(builder, "/ref:", settings.AssemblyReferences);

            Append(builder, "/gac", settings.SearchGac);
            Append(builder, "/iit", settings.IgnoreInvalidTargets);
            Append(builder, "/igc", settings.IgnoreGeneratedCode);
            Append(builder, "/sf", settings.SuccessFile);
            Append(builder, "/q", settings.Quiet);

            return builder;
        }

        private void Append(ProcessArgumentBuilder builder, string arg, IEnumerable<DirectoryPath> directoryPaths)
        {
            if (directoryPaths == null)
            {
                return;
            }

            foreach (var directoryPath in directoryPaths)
            {
                Append(builder, arg, directoryPath);
            }
        }

        private void Append(ProcessArgumentBuilder builder, string arg, DirectoryPath directoryPath)
        {
            builder.Append($"{arg}\"{directoryPath.MakeAbsolute(_environment).FullPath}\"");
        }

        private void Append(ProcessArgumentBuilder builder, string arg, IEnumerable<FilePath> filePaths)
        {
            if (filePaths == null)
            {
                return;
            }

            foreach (var filePath in filePaths)
            {
                Append(builder, arg, filePath);
            }
        }

        private void Append(ProcessArgumentBuilder builder, string arg, FilePath filePath)
        {
            builder.Append($"{arg}\"{filePath.MakeAbsolute(_environment).FullPath}\"");
        }

        private void Append(ProcessArgumentBuilder builder, string arg, bool expression)
        {
            if (expression)
            {
                builder.Append(arg);
            }
        }
    }
}