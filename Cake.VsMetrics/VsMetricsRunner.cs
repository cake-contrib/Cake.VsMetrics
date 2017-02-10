using System.Collections.Generic;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.VsMetrics
{
    public sealed class VsMetricsRunner : Tool<VsMetricsSettings>
    {
        public VsMetricsRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
        }

        public void Run()
        {
            Run(new VsMetricsSettings(), new ProcessArgumentBuilder());
        }

        protected override string GetToolName()
        {
            return "Metrics";
        }

        protected override IEnumerable<string> GetToolExecutableNames()
        {
            return new[] { "metrics.exe" };
        }
    }
}