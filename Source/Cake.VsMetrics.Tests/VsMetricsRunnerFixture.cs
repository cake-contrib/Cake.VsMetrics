using System.Collections.Generic;
using Cake.Core.IO;
using Cake.Testing.Fixtures;

namespace Cake.VsMetrics.Tests
{
    internal sealed class VsMetricsRunnerFixture : ToolFixture<VsMetricsSettings>
    {
        public VsMetricsRunnerFixture()
            : base("metrics.exe")
        {
            InputFilePaths = new FilePath[] { "c:/tool.exe" };
            OutputFilePath = "metrics_result.xml";
        }

        public IEnumerable<FilePath> InputFilePaths { get; set; }

        public FilePath OutputFilePath { get; set; }

        protected override void RunTool()
        {
            var tool = new VsMetricsRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Run(InputFilePaths, OutputFilePath, Settings);
        }
    }
}
