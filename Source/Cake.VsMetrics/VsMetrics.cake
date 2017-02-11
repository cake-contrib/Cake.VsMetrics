#r "Cake.VsMetrics.dll"

try
{
    EnsureDirectoryExists("BuildArtifacts/Metrics");
    var projects = GetFiles("Source/Cake.VsMetrics/bin/Debug/*.dll");
    var settings = new VsMetricsSettings()
    {
        SuccessFile = true,
        IgnoreGeneratedCode = true,
    };

    VsMetrics(projects, "BuildArtifacts/Metrics/metrics.xml", settings);
}
catch(Exception ex)
{
    Error("{0}", ex);
}