using System.Collections.Generic;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.VsMetrics
{
    public sealed class VsMetricsRunner : Tool<VsMetricsSettings>
    {
        private ICakeEnvironment _environment;

        public VsMetricsRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
            _environment = environment;
        }

        public void Run(IEnumerable<FilePath> inputFilePaths, FilePath outputFilePath, VsMetricsSettings settings)
        {
            Contract.RequireNonNull(inputFilePaths, nameof(inputFilePaths));
            Contract.RequireNonNull(outputFilePath, nameof(outputFilePath));
            settings = settings ?? new VsMetricsSettings();

            Run(settings, GetArguments(inputFilePaths, outputFilePath, settings));
        }

        protected override string GetToolName()
        {
            return "Metrics";
        }

        protected override IEnumerable<string> GetToolExecutableNames()
        {
            return new[] { "metrics.exe" };
        }

        private ProcessArgumentBuilder GetArguments(IEnumerable<FilePath> inputFilePaths, FilePath outputFilePath, VsMetricsSettings settings)
        {
            var builder = new ProcessArgumentBuilder();

            foreach (var inputFilePath in inputFilePaths)
            {
                builder.Append($"/f:\"{inputFilePath.MakeAbsolute(_environment).FullPath}\"");
            }

            builder.Append($"/o:\"{outputFilePath.MakeAbsolute(_environment).FullPath}\"");

            return builder;
        }
    }
}