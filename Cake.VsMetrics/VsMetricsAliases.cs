using System;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;

namespace Cake.VsMetrics
{
    [CakeAliasCategory("VsMetrics")]
    public static class VsMetricsAliases
    {
        [CakeMethodAlias]
        public static void VsMetrics(this ICakeContext context, FilePath inputFilePath, FilePath outputFilePath)
        {
            VsMetrics(context, inputFilePath, outputFilePath, new VsMetricsSettings());
        }

        [CakeMethodAlias]
        public static void VsMetrics(this ICakeContext context, FilePath inputFilePath, FilePath outputFilePath, VsMetricsSettings settings)
        {
            Contract.RequireNonNull(context, nameof(context));

            var runner = new VsMetricsRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Run(inputFilePath, outputFilePath, settings);
        }
    }
}