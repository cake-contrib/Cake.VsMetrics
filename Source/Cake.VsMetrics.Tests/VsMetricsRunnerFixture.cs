using System.Collections.Generic;
using Cake.Core;
using Cake.Core.IO;
using Cake.Testing;
using Cake.Testing.Fixtures;

namespace Cake.VsMetrics.Tests
{
    internal sealed class VsMetricsRunnerFixture : ToolFixture<VsMetricsSettings>
    {
        public VsMetricsRunnerFixture(bool defaultToolExist = true, PlatformFamily platform = PlatformFamily.Windows)
            : base("/Working/tools/metrics.exe")
        {
            InputFilePaths = new FilePath[] { "c:/tool.exe" };
            OutputFilePath = "metrics_result.xml";

            if (defaultToolExist)
            {
                FileSystem.CreateFile("/Working/tools/metrics.exe");
            }

            Environment = new FakeEnvironment(platform);
            Environment.WorkingDirectory = "/Working";
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
