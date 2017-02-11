#r "Cake.VsMetrics.dll"

try
{
    Information("Hello cake world!");
    EnsureDirectoryExists("BuildArtifacts");
    var projects = GetFiles("Source/Cake.VsMetrics/bin/Debug/*.dll");
    var settings = new VsMetricsSettings()
    {
        SuccessFile = true,
        IgnoreGeneratedCode = true,
    };

    VsMetrics(projects, "BuildArtifacts/metrics.xml", settings);
}
catch(Exception ex)
{
    Error("{0}", ex);
}