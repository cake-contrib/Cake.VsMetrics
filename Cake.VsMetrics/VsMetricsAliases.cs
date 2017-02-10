using System;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.VsMetrics
{
    [CakeAliasCategory("VsMetrics")]
    public static class VsMetricsAliases
    {
        [CakeMethodAlias]
        public static void VsMetrics(this ICakeContext context)
        {
            if (context == null)
            {
                throw new ArgumentException(nameof(context));
            }

            var runner = new VsMetricsRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Run();
        }
    }
}